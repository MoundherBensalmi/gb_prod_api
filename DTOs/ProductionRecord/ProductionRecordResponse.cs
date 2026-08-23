using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.DTOs.Grade;
using gb_prod_api.DTOs.PawColor;
using gb_prod_api.DTOs.Tunel;
using gb_prod_api.Models;

namespace gb_prod_api.DTOs.ProductionRecord
{
    public class ProductionRecordResponse
    {
        public long Id { get; set; }

        public DateTime ProducedAt { get; set; }

        public decimal QuantityKg { get; set; }

        public ProductionRecordStatus Status { get; set; }
        public DateTime? MovedOutAt { get; set; }

        public long PawGradeId { get; set; }
        public PawGradeResponse PawGrade { get; set; } = null!;

        public int TunnelId { get; set; }
        public TunnelResponse Tunnel { get; set; } = null!;

        public string? Notes { get; set; }
    }
}