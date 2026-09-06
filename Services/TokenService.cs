using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using gb_prod_api.Auth;
using gb_prod_api.Models;
using Microsoft.IdentityModel.Tokens;

namespace gb_prod_api.Services
{
    public class TokenService(JwtOptions jwtOptions)
    {
        private readonly JwtOptions _jwtOptions = jwtOptions;

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(AppClaims.UserId, user.Id.ToString()),
                new(AppClaims.Username, user.Username),
                new(AppClaims.Role, user.Role.ToString()),
            };

            claims.AddRange(user.UserPermissions
                .Select(up => new Claim(AppClaims.Permission, up.Permission.ToString())));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresInMinutes),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
