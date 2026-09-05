using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace gb_prod_api.Models
{
    [Index(nameof(Date), IsUnique = true)]
    public class ProductionDay
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public bool IsClosed { get; set; } = false;

        public ICollection<ProductionRecord> ProductionRecords { get; set; } = [];
    }
}