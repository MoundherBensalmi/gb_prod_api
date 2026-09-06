using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.DTOs.User;
using gb_prod_api.Models;
using Riok.Mapperly.Abstractions;

namespace gb_prod_api.Mappers
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public static partial class UserMapper
    {
        [MapProperty(nameof(User.UserPermissions), nameof(UserResponse.Permissions))]
        public static partial UserResponse ToResponse(User user);

        public static partial List<UserResponse> ToResponse(List<User> users);

        private static Permission ToPermission(UserPermission userPermission) => userPermission.Permission;
    }
}
