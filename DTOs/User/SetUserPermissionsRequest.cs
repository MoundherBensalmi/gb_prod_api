using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.Models;

namespace gb_prod_api.DTOs.User
{
    public class SetUserPermissionsRequest
    {
        public List<Permission> Permissions { get; set; } = [];
    }
}
