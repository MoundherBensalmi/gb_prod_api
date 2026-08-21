using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace gb_prod_api.Models
{
    [Index(nameof(PawGradeId), nameof(ContainerShipmentId), IsUnique = true)]
    public class ContainerShipmentItem
    {
        public long Id { get; set; }

        public long ContainerShipmentId { get; set; }
        public ContainerShipment ContainerShipment { get; set; } = null!;

        public int PawGradeId { get; set; }
        public PawGrade PawGrade { get; set; } = null!;

        [Precision(18, 2)]
        public decimal QuantityKg { get; set; }
    }
}