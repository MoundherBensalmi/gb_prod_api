using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.DTOs.ProductionRecord;
using gb_prod_api.Models;
using Riok.Mapperly.Abstractions;

namespace gb_prod_api.Mappers
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public static partial class ProductionRecordMapper
    {
        public static partial ProductionRecordResponse ToResponse(ProductionRecord productionRecord);

        public static partial List<ProductionRecordResponse> ToResponse(
            List<ProductionRecord> productionRecords);
    }
}