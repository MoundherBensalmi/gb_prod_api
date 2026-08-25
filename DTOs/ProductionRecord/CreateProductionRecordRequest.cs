using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gb_prod_api.DTOs.ProductionRecord
{
    public class CreateProductionRecordRequest
    {
        public required int ProductionDayId { get; set; }
        public required int PawGradeId { get; set; }
        public required int TunnelId { get; set; }
        public required DateTime ProducedAt { get; set; }
        public required decimal QuantityKg { get; set; }
        public required DateTime? MovedOutAt { get; set; }
        public string? Notes { get; set; }
    }
}