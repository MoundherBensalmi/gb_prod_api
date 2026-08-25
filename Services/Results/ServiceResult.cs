using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gb_prod_api.Services.Results
{
    public class ServiceResult<T>
    {
        public bool Success { get; init; } = true;
        public T? Data {get; init;}
        public ServiceError? Error {get; init; }

        public static ServiceResult<T> Ok(T data)
        {
            return new ServiceResult<T>
            {
                Success = true,
                Data = data
            };
        }

        public static ServiceResult<T> Fail(ServiceError error)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Error = error
            };
        }
    }

    public class ServiceError
    {
        public string Code { get; init; } = "";
        public string Message { get; init; } = "";
        public string Field
        {
            get => _field;
            init => _field = ToCamelCase(value);
        }

        private string _field = "";

        private static string ToCamelCase(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return char.ToLowerInvariant(value[0]) + value[1..];
        }

    }
    
    
}