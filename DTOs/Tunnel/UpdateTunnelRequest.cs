using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gb_prod_api.DTOs.Tunel
{
    public class UpdateTunnelRequest
    {
        public string Name { get; set; } = null!;

        public string ArabicName { get; set; } = null!;

        [Range(0, long.MaxValue)]
        public long Capacity { get; set; }
    }
}
