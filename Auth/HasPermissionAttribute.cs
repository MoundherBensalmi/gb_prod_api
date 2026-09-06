using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace gb_prod_api.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HasPermissionAttribute(Permission permission) : Attribute, IAuthorizationFilter
    {
        private readonly Permission _permission = permission;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user.Identity?.IsAuthenticated != true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (user.IsInRole(nameof(UserRole.Admin)))
            {
                return;
            }

            var hasPermission = user.HasClaim(AppClaims.Permission, _permission.ToString());

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
