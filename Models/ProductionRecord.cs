using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace gb_prod_api.Models
{
    [Index(nameof(ProductionDayId))]
    [Index(nameof(PawGradeId), nameof(MovedOutAt))]
    public class ProductionRecord
    {
        public long Id { get; set; }
        public int ProductionDayId { get; set; }
        public ProductionDay ProductionDay { get; set; } = null!;

        public int PawGradeId { get; set; }
        public PawGrade PawGrade { get; set; } = null!;

        public int? TunnelId { get; set; }
        public Tunnel? Tunnel { get; set; }

        public DateTime ProducedAt { get; set; }

        [Precision(18, 2)]
        public decimal QuantityKg { get; set; }

        public DateTime? MovedOutAt { get; set; }
        public ProductionRecordStatus Status { get; set; }

        public string? Notes { get; set; }
    }
}