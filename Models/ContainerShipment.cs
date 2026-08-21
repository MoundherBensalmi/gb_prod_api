using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gb_prod_api.Models
{
    public class ContainerShipment
    {
        public long Id { get; set; }
        public DateTime ShippedAt { get; set; }
        public string ContainerNumber { get; set; } = null!;
        public string? Notes { get; set; }

        public ICollection<ContainerShipmentItem> ContainerShipmentItems { get; set; } = [];
    }
}