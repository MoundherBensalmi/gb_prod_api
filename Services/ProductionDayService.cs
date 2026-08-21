using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.Data;
using gb_prod_api.Models;
using Microsoft.EntityFrameworkCore;

namespace gb_prod_api.Services
{
    public class ProductionDayService(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<List<ProductionDay>> GetProductionDaysAsync(DateOnly startDate, DateOnly endDate)
        {
            var productionDays = await _dbContext.ProductionDays
                .Where(pd => pd.Date >= startDate && pd.Date <= endDate)
                .ToListAsync();

            return productionDays;
        }

        public async Task<ProductionDay?> GetProductionDayByIdAsync(long id)
        {
            var productionDay = await _dbContext.ProductionDays
                .Include(pd => pd.ProductionRecords)
                    .ThenInclude(pr => pr.PawGrade)
                        .ThenInclude(pg => pg.PawColor)
                .Include(pd => pd.ProductionRecords)
                    .ThenInclude(pr => pr.Tunnel)
                .FirstOrDefaultAsync(pd => pd.Id == id);

            return productionDay;
        }
    }
}