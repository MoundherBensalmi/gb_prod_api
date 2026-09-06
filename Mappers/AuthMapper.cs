using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.DTOs.Auth;
using gb_prod_api.Services;
using Riok.Mapperly.Abstractions;

namespace gb_prod_api.Mappers
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    [UseStaticMapper(typeof(UserMapper))]
    public static partial class AuthMapper
    {
        public static partial LoginResponse ToResponse(LoginResult loginResult);
    }
}
