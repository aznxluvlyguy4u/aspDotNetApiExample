using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using samsung.api.Extensions;
using samsung_api.Services.Logger;
using System;
using System.Linq;
using System.Reflection;

namespace SamsungApiAws.Extensions
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// A crude verification function to validate all necessities have been injected
        /// </summary>
        /// <param name="services">The services.</param>
        /// <exception cref="FunctionInvocationException"></exception>
        public static void Verify(this IServiceCollection services, ILogger logger = null)
        {
            var asm = Assembly.GetCallingAssembly();
            var loadableTypes = asm.GetLoadableTypes()
                .Where(x =>
                    !x.Name.StartsWith('<') // Dynamic types~
                    && !x.FullName.Contains(".Models") // No need for Models
                )
                .ToList()
            ;
            // Exception to verification
            var count = services.Count;

            // Iterate through all loadable types
            foreach (var loadableType in loadableTypes)
            {
                var constructors = loadableType.GetConstructors();
                // Check if there is more than the default .ctor
                if (constructors.Length > 1)
                    continue;

                foreach (var constructor in constructors)
                {
                    // Assume constructor is valid, till proven otherwise
                    var validConstructor = true;

                    var parameters = constructor.GetParameters();

                    // If constructor does not take arguments, it is self defined .ctor
                    // This means it has no injection dependencies and can be safely ignored.
                    if (parameters.Length == 0) break;
                    foreach (var param in parameters)
                    {
                        // If any of the constructor parameters is not an interface, assume it is not injectable.
                        // These are most likely injected through factories, or should not be injected all together.
                        if (!param.ParameterType.IsInterface)
                        {
                            validConstructor = false;
                            break;
                        }
                    }
                    if (!validConstructor) break;

                    var interfaces = loadableType.GetInterfaces();

                    // Limit interfaces to the project namespaces.
                    // This to prevent rougue interfaces from intervering
                    interfaces = interfaces.Where(x =>
                        x.Namespace.StartsWith("Samsung")
                    ).ToArray();

                    // If any interface is left afterwards, attempt to add it, to see if it is already in there.
                    // This does assume, the first interface is the implementation interface.
                    if (!interfaces.IsNullOrEmpty())
                    {
                        var implementationInterface = interfaces.FirstOrDefault();
                        bool found = false;

                        // Determine implementation type
                        foreach (var implementationType in loadableTypes)
                        {
                            if (implementationType.GetInterfaces().FirstOrDefault(x => x == implementationInterface) == default)
                                continue;

                            services.TryAddTransient(implementationInterface, implementationType);
                            if (services.Count > count)
                            {
                                count++;
                                // Successfully added, continue;
                                if (logger != null)
                                    logger.LogWarningAsync($"{loadableType.FullName} was automatically injected as transient").GetAwaiter().GetResult();
                                else if (!found)
                                {
                                    throw new Exception(
                                        $"{loadableType.FullName} is not injected, but probably should be injected," +
                                        "Double check that it is injected, and that all it's dependencies are injected.");
                                }
                                break;
                            }

                            // Already existed, no need for further iterations.
                            break;
                        }
                    }
                }
            }
        }
    }
}