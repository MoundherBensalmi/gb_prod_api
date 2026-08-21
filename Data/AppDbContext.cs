using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.Models;
using Microsoft.EntityFrameworkCore;

namespace gb_prod_api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}