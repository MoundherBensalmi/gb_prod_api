using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.DTOs.ProductionRecord;

namespace gb_prod_api.DTOs.ProductionDay
{
    public class ProductionDayResponse
    {
        public long Id { get; set; }

        public DateOnly Date { get; set; }

        public bool IsClosed { get; set; }

        public List<ProductionRecordResponse> ProductionRecords { get; set; } = [];
    }
}