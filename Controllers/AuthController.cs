using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.Auth;
using gb_prod_api.Common;
using gb_prod_api.DTOs.Auth;
using gb_prod_api.DTOs.User;
using gb_prod_api.Mappers;
using gb_prod_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gb_prod_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthService authService, UserService userService) : ControllerBase
    {
        private readonly AuthService _authService = authService;
        private readonly UserService _userService = userService;

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (!result.IsSuccess)
            {
                return result.ToErrorActionResult(this);
            }

            return AuthMapper.ToResponse(result.Data!);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserResponse>> Me()
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserByIdAsync(userId.Value);
            if (user == null)
            {
                return Unauthorized();
            }

            return UserMapper.ToResponse(user);
        }
    }
}
