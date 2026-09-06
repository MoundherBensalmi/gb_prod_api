using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.Common;
using gb_prod_api.Data;
using gb_prod_api.DTOs.Auth;
using gb_prod_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace gb_prod_api.Services
{
    public class AuthService(AppDbContext dbContext, IPasswordHasher<User> passwordHasher, TokenService tokenService)
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
        private readonly TokenService _tokenService = tokenService;

        public async Task<Result<LoginResult>> LoginAsync(LoginRequest request)
        {
            var user = await _dbContext.Users
                .Include(u => u.UserPermissions)
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
            {
                return AppError.Unauthorized("auth.invalidCredentials");
            }

            var verification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (verification == PasswordVerificationResult.Failed)
            {
                return AppError.Unauthorized("auth.invalidCredentials");
            }

            var token = _tokenService.GenerateToken(user);

            return Result<LoginResult>.Success(new LoginResult(user, token));
        }
    }
}
