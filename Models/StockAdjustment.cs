using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace gb_prod_api.Models
{
    [Index(nameof(PawGradeId), nameof(CreatedAt))]
    public class StockAdjustment
    {
        public long Id { get; set; }

        public int PawGradeId { get; set; }
        public PawGrade PawGrade { get; set; } = null!;

        [Precision(18, 2)]
        public decimal QuantityKg { get; set; }
        public string Reason { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}