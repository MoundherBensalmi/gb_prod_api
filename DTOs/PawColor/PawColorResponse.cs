using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.DTOs.Grade;

namespace gb_prod_api.DTOs.PawColor
{
    public class PawColorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}