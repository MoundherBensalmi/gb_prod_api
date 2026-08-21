using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gb_prod_api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }

        public string? Description { get; set; }
    }
}