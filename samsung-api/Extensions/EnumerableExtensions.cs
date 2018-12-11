using System;
using System.Collections.Generic;

namespace samsung.api.Extensions
{
    public static class EnumerableExtensions
    {
        // Source: https://stackoverflow.com/a/521894/2030635
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            foreach (var e in ie)
                action(e);
        }

        // Source: https://stackoverflow.com/a/521894/2030635
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in ie)
                action(e, i++);
        }

        // Source: https://stackoverflow.com/a/521894/2030635
        public static bool ForEach<T>(this IEnumerable<T> ie, Func<T, int, bool> action)
        {
            int i = 0;
            foreach (T e in ie)
            {
                if (!action(e, i++))
                    return false;
            }

            return true;
        }
    }
}