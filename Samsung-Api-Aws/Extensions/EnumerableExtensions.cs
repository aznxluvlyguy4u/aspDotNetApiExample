using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        ///     Determines whether the collection is null or contains no elements.
        /// </summary>
        /// <typeparam name="T">The IEnumerable type.</typeparam>
        /// <param name="enumerable">The enumerable, which may be null or empty.</param>
        /// <returns>
        ///     <c>true</c> if the IEnumerable is null or empty; otherwise, <c>false</c>.
        /// </returns>
        /// <source>https://stackoverflow.com/a/8582374/2030635</source>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null) return true;

            // If this is a list, use the Count property for efficiency.
            // The Count property is O(1) while IEnumerable.Count() is O(N).
            if (enumerable is ICollection<T> collection) return collection.Count < 1;

            return !enumerable.Any();
        }
    }
}