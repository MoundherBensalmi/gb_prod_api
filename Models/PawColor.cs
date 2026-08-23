using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gb_prod_api.Models
{
    public class PawColor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ArabicName { get; set; } = null!;

        public ICollection<PawGrade> PawGrades { get; set; } = [];
    }
}