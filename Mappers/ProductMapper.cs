using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.DTOs.Product;
using gb_prod_api.Models;
using Riok.Mapperly.Abstractions;

namespace gb_prod_api.Mappers
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public static partial class ProductMapper
    {
        public static partial ProductResponse ToResponse(Product product);

        public static partial List<ProductResponse> ToResponse(
            List<Product> products);
    }
}