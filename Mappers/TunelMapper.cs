using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.DTOs.Tunel;
using gb_prod_api.Models;
using Riok.Mapperly.Abstractions;

namespace gb_prod_api.Mappers
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public static partial class TunelMapper
    {
        public static partial TunnelResponse ToResponse(Tunnel tunnel);
        public static partial List<TunnelResponse> ToResponse(List<Tunnel> tunnels);
    }
}