using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace gb_prod_api.Models
{
    [Index(nameof(UserId), nameof(Permission), IsUnique = true)]
    public class UserPermission
    {
        public long Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public Permission Permission { get; set; }
    }
}
