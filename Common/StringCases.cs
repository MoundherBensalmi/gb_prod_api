using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gb_prod_api.Common
{
    public static class StringCases
    {
        public static string ToCamelCase(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return char.ToLowerInvariant(value[0]) + value[1..];
        }
    }
}