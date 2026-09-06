using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.DTOs.User;

namespace gb_prod_api.DTOs.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; } = null!;
        public UserResponse User { get; set; } = null!;
    }
}
