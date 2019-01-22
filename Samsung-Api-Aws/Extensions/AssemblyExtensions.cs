using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SamsungApiAws.Extensions
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Get the loadable types.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>The <see cref="T:IEnumerable{Type}"/>.</returns>
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
    }
}
