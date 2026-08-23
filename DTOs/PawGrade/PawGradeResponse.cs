using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.DTOs.PawColor;

namespace gb_prod_api.DTOs.Grade
{
    public class PawGradeResponse
    {
        public int Id { get; set; }
        public int PawColorId { get; set; }
        public PawColorResponse PawColor { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string ArabicName { get; set; } = null!;
        public string? Description { get; set; }
    }
}