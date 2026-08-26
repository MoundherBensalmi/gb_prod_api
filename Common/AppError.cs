using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gb_prod_api.Common
{
    public sealed record AppError(AppErrorType Type, string Message, string? Field = null)
    {
        public static AppError NotFound(string message) => new(AppErrorType.NotFound, message);
        public static AppError Validation(string message, string? field = null) => new(AppErrorType.Validation, message, field != null ? StringCases.ToCamelCase(field) : null);
        public static AppError Conflict(string message) => new(AppErrorType.Conflict, message);
        public static AppError Unexpected(string message) => new(AppErrorType.Unexpected, message);

    }
}