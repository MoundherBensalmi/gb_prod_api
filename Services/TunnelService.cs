using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.Common;
using gb_prod_api.Data;
using gb_prod_api.Models;
using Microsoft.EntityFrameworkCore;

namespace gb_prod_api.Services
{
    public class TunnelService(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<List<Tunnel>> GetTunnelsAsync()
        {
            var tunnels = await _dbContext.Tunnels
                .OrderBy(t => t.Name)
                .ToListAsync();

            return tunnels;
        }

        public async Task<Tunnel?> GetTunnelByIdAsync(int id)
        {
            var tunnel = await _dbContext.Tunnels
                .FirstOrDefaultAsync(t => t.Id == id);

            return tunnel;
        }

        public async Task<Tunnel> CreateTunnelAsync(string name, string arabicName, long capacity)
        {
            var tunnel = new Tunnel
            {
                Name = name,
                ArabicName = arabicName,
                Capacity = capacity
            };

            _dbContext.Tunnels.Add(tunnel);
            await _dbContext.SaveChangesAsync();

            return tunnel;
        }

        public async Task<Tunnel?> UpdateTunnelAsync(int id, string name, string arabicName, long capacity)
        {
            var tunnel = await _dbContext.Tunnels
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tunnel == null)
            {
                return null;
            }

            tunnel.Name = name;
            tunnel.ArabicName = arabicName;
            tunnel.Capacity = capacity;

            await _dbContext.SaveChangesAsync();

            return tunnel;
        }

        public async Task<Result<bool>> DeleteTunnelAsync(int id)
        {
            var tunnel = await _dbContext.Tunnels
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tunnel == null)
            {
                return AppError.NotFound(message: "tunnel.notFound");
            }

            // The ProductionRecords -> Tunnels FK has no cascade, so deleting a
            // referenced tunnel would fail at the database level.
            var isInUse = await _dbContext.ProductionRecords
                .AnyAsync(pr => pr.TunnelId == id);

            if (isInUse)
            {
                return AppError.Conflict(message: "tunnel.inUse");
            }

            _dbContext.Tunnels.Remove(tunnel);
            await _dbContext.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
