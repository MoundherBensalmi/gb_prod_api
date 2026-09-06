using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.Auth;
using gb_prod_api.Common;
using gb_prod_api.DTOs.User;
using gb_prod_api.Mappers;
using gb_prod_api.Models;
using gb_prod_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace gb_prod_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [HasPermission(Permission.ManageUsers)]
    public class UserController(UserService userService) : ControllerBase
    {
        private readonly UserService _userService = userService;

        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return UserMapper.ToResponse(users);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserResponse>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return UserMapper.ToResponse(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> CreateUser([FromBody] CreateUserRequest request)
        {
            var result = await _userService.CreateUserAsync(request);
            if (!result.IsSuccess)
            {
                return result.ToErrorActionResult(this);
            }

            return CreatedAtAction(
                nameof(GetUserById),
                new { id = result.Data!.Id },
                UserMapper.ToResponse(result.Data!)
            );
        }

        [HttpPut("{id:int}/permissions")]
        public async Task<ActionResult<UserResponse>> SetUserPermissions(int id, [FromBody] SetUserPermissionsRequest request)
        {
            var result = await _userService.SetPermissionsAsync(id, request.Permissions);
            if (!result.IsSuccess)
            {
                return result.ToErrorActionResult(this);
            }

            return UserMapper.ToResponse(result.Data!);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);

            return deleted ? NoContent() : NotFound();
        }
    }
}
