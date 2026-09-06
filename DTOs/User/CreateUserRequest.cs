using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.Models;

namespace gb_prod_api.DTOs.User
{
    public class CreateUserRequest
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;

        public UserRole Role { get; set; } = UserRole.User;

        public List<Permission> Permissions { get; set; } = [];
    }
}
