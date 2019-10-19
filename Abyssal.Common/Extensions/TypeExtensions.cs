using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Abyssal.Common
{
    /// <summary>
    ///     A set of extensions for CLR types.
    /// </summary>
    public static class TypeExtensions
    { 
        /// <summary>
        ///     Determines whether the provided type has a specified custom attribute.
        /// </summary>
        /// <typeparam name="TAttributeType">The attribute type to check exists on the provided type.</typeparam>
        /// <param name="type">The type to check for the provided attribute.</param>
        /// <param name="attribute">The attribute, if it exists. This will be null if the attribute doesn't exist.</param>
        /// <returns>A boolean indicating whether the provided type has an attribute of the provided attribute type.</returns>
        public static bool HasCustomAttribute<TAttributeType>(this Type type, [NotNullWhen(true)] out TAttributeType? attribute) where TAttributeType: Attribute
        {
            var attr = type.GetCustomAttributes(typeof(TAttributeType), true).FirstOrDefault();
            if (attr != null)
            {
                attribute = (TAttributeType) attr;
                return true;
            }
            attribute = default;
            return false;
        }

        /// <summary>
        ///     Determines whether the provided type has a specified custom attribute.
        /// </summary>
        /// <typeparam name="TAttributeType">The attribute type to check exists on the provided type.</typeparam>
        /// <param name="type">The type to check for the provided attribute.</param>
        /// <returns>A boolean indicating whether the provided type has an attribute of the provided attribute type.</returns>
        public static bool HasCustomAttribute<TAttributeType>(this Type type) where TAttributeType : Attribute
        {
            return HasCustomAttribute<TAttributeType>(type, out _);
        }
    }
}
