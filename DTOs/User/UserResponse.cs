using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.Models;

namespace gb_prod_api.DTOs.User
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public UserRole Role { get; set; }
        public List<Permission> Permissions { get; set; } = [];
    }
}
