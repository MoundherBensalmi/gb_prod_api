using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gb_prod_api.Models
{
    public class PawGrade
    {
        public int Id { get; set; }
        public int PawColorId { get; set; }
        public PawColor PawColor { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}