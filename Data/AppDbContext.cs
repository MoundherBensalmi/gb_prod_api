using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.Models;
using Microsoft.EntityFrameworkCore;

namespace gb_prod_api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<PawColor> PawColors => Set<PawColor>();
        public DbSet<PawGrade> PawGrades => Set<PawGrade>();

        public DbSet<ProductionDay> ProductionDays => Set<ProductionDay>();
        public DbSet<ProductionRecord> ProductionRecords => Set<ProductionRecord>();

        public DbSet<Tunnel> Tunnels => Set<Tunnel>();

        public DbSet<ContainerShipment> ContainerShipments => Set<ContainerShipment>();
        public DbSet<ContainerShipmentItem> ContainerShipmentItems => Set<ContainerShipmentItem>();

        public DbSet<StockAdjustment> StockAdjustments => Set<StockAdjustment>();
    }
}