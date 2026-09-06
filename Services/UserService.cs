using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.Common;
using gb_prod_api.Data;
using gb_prod_api.DTOs.User;
using gb_prod_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace gb_prod_api.Services
{
    public class UserService(AppDbContext dbContext, IPasswordHasher<User> passwordHasher)
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

        public async Task<List<User>> GetUsersAsync()
        {
            var users = await _dbContext.Users
                .Include(u => u.UserPermissions)
                .OrderBy(u => u.Username)
                .ToListAsync();

            return users;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await _dbContext.Users
                .Include(u => u.UserPermissions)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<Result<User>> CreateUserAsync(CreateUserRequest request)
        {
            var usernameTaken = await _dbContext.Users
                .AnyAsync(u => u.Username == request.Username);

            if (usernameTaken)
            {
                return AppError.Conflict("user.username.alreadyTaken");
            }

            var user = new User
            {
                Username = request.Username,
                Role = request.Role,
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            user.UserPermissions = request.Permissions
                .Distinct()
                .Select(permission => new UserPermission { Permission = permission })
                .ToList();

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return Result<User>.Success(user);
        }

        public async Task<Result<User>> SetPermissionsAsync(int id, List<Permission> permissions)
        {
            var user = await GetUserByIdAsync(id);

            if (user == null)
            {
                return AppError.NotFound("user.notFound");
            }

            _dbContext.UserPermissions.RemoveRange(user.UserPermissions);

            user.UserPermissions = permissions
                .Distinct()
                .Select(permission => new UserPermission { UserId = user.Id, Permission = permission })
                .ToList();

            await _dbContext.SaveChangesAsync();

            return Result<User>.Success(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return false;
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
