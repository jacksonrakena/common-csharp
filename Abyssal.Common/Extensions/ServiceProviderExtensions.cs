using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Abyssal.Common
{
    /// <summary>
    ///     A set of extensions for the <see cref="IServiceProvider"/> class.
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        ///     Attempts to create an object of type <typeparamref name="T"/>, injecting constructor parameters
        ///     from services in <paramref name="serviceProvider"/>.
        /// </summary>
        /// <typeparam name="T">The type of the object to instantiate.</typeparam>
        /// <param name="serviceProvider">The service provider to pull services from.</param>
        /// <returns>An object of type <typeparamref name="T"/>.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if:
        ///     <typeparamref name="T"/> is an interface, or <see langword="abstract"/>.
        ///     -or-
        ///     There are no public, instance-level constructors for <typeparamref name="T"/>.
        ///     -or-
        ///     There is more than one public, instance-level constructor for <typeparamref name="T"/>.
        ///     -or-
        ///     The method could not find a service for a type that is required by the constructor.
        /// </exception>
        public static T Create<T>(this IServiceProvider serviceProvider)
        {
            return (T) Create(serviceProvider, typeof(T));
        }

        /// <summary>
        ///     Attempts to create an object of the provided type, injecting constructor parameters
        ///     from services in <paramref name="serviceProvider"/>.
        /// </summary>
        /// <param name="serviceProvider">The service provider to pull services from.</param>
        /// <param name="type">The type of the object to instantiate.</param>
        /// <returns>An object of the provvided type..</returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if:
        ///     The provided <see cref="Type" /> is an interface, or <see langword="abstract"/>.
        ///     -or-
        ///     There are no public, instance-level constructors for the provided <see cref="Type" />.
        ///     -or-
        ///     There is more than one public, instance-level constructor for the provided <see cref="Type"/>.
        ///     -or-
        ///     The method could not find a service for a type that is required by the constructor.
        /// </exception>
        public static object Create(this IServiceProvider serviceProvider, Type type)
        {
            if (type.IsInterface || type.IsAbstract) throw new InvalidOperationException("Cannot instantiate an abstract type, or an interface type.");

            var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            if (constructors.Length == 0) throw new InvalidOperationException($"There are no public, instance-level constructors for {type.Name}.");
            if (constructors.Length > 1) throw new InvalidOperationException($"There are more than one public, instance-level constructors for {type.Name}.");
            var constructor = constructors[0];

            return Inject(serviceProvider, constructor);
        }

        /// <summary>
        ///     Attempts to inject services from a service provider into a constructor, and constructs the object.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="constructor">The constructor to inject services into.</param>
        /// <returns>A constructed object.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if:
        ///     The method could not find a service for a type that is required by the constructor.
        /// </exception>
        public static object Inject(this IServiceProvider serviceProvider, ConstructorInfo constructor)
        {
            var parametersToExecute = new List<object>();
            var serviceProviderType = typeof(IServiceProvider);
            foreach (var parameter in constructor.GetParameters())
            {
                if (parameter.ParameterType.IsAssignableFrom(serviceProviderType))
                {
                    parametersToExecute.Add(serviceProvider);
                    continue;
                }

                var service = serviceProvider.GetService(parameter.ParameterType);
                if (service == null)
                {
                    throw new InvalidOperationException($"Could not find a service for {parameter.ParameterType.Name}, while building an object of type {parameter.Member.DeclaringType.Name}.");
                }

                parametersToExecute.Add(service);
            }
            return constructor.Invoke(parametersToExecute.ToArray());
        }

        /// <summary>
        ///     Attempts to locate a service that is of the provided type.
        /// </summary>
        /// <param name="provider">The service provider to locate from.</param>
        /// <param name="type">The type to locate.</param>
        /// <param name="result">The output, should the operation be successful.</param>
        /// <returns>A boolean representing whether the operation succeeded.</returns>
        public static bool TryGetService(this IServiceProvider provider, Type type, [NotNullWhen(true)] out object? result)
        {
            var query = provider.GetService(type);
            if (query != null)
            {
                result = query;
                return true;
            }

            result = null;
            return false;
        }

        /// <summary>
        ///     Attempts to locate a service that is of the provided type.
        /// </summary>
        /// <typeparam name="T">The type to locate.</typeparam>
        /// <param name="provider">The provider to locate from.</param>
        /// <param name="result">The output, should the operation be successful.</param>
        /// <returns>A boolean representing whether the operation succeeded.</returns>
        public static bool TryGetService<T>(this IServiceProvider provider, [NotNullWhen(true)] out T? result) where T : class
        {
            var query = provider.GetService<T>();
            if (query != null)
            {
                result = query;
                return true;
            }

            result = default;
            return false;
        }
    }
}
