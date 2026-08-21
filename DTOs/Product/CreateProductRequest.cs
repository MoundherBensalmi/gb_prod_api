using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gb_prod_api.DTOs.Product
{
    public class CreateProductRequest
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public string? Description { get; set; }
    }
}