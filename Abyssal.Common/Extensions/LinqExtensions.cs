using System;
using System.Collections.Generic;
using System.Text;

namespace Abyssal.Common
{
    /// <summary>
    ///     A set of LINQ extensions.
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        ///     Selects a random element from the provided array.
        /// </summary>
        /// <typeparam name="T">The type of the members of the provided array.</typeparam>
        /// <param name="source">The array to select the element from.</param>
        /// <param name="random">The random number generator engine to use. It will default to a new instance of <see cref="System.Random"/>.</param>
        /// <returns>The selected element.</returns>
        public static T Random<T>(this T[] source, Random? random = null)
        {
            random ??= new Random();
            return source[random.Next(0, source.Length)];
        }

        /// <summary>
        ///     Selects a random element from the provided list.
        /// </summary>
        /// <typeparam name="T">The type of the members of the provided list.</typeparam>
        /// <param name="source">The list to select the element from.</param>
        /// <param name="random">The random number generator engine to use. It will default to a new instance of <see cref="System.Random"/>.</param>
        /// <returns>The selected element.</returns>
        public static T Random<T>(this IList<T> source, Random? random = null)
        {
            random ??= new Random();
            return source[random.Next(0, source.Count)];
        }
    }
}
