using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gb_prod_api.Services
{
    public enum DeleteTunnelResult
    {
        Deleted,
        NotFound,
        InUse
    }
}
