using Microsoft.AspNetCore.Http;
using System;

namespace samsung.api.Extensions
{
    public static class HttpContextExtensions
    {
        public static bool TryGetEnumQueryValue<T>(this HttpContext context, string key, out T result)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            result = default;

            try
            {
                if (!context?.Request?.Query?.TryGetValue(key, out var value) ?? false)
                    return false;

                if (!Enum.TryParse(value, true, out result))
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}