using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.DTOs.ProductionDay;
using gb_prod_api.Models;
using Riok.Mapperly.Abstractions;

namespace gb_prod_api.Mappers
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public static partial class ProductionDayMapper
    {
        public static partial ProductionDayResponse ToResponse(ProductionDay productionDay);

        public static partial List<ProductionDayResponse> ToResponse(
            List<ProductionDay> productionDays);
    }
}