using System;
using System.Diagnostics.CodeAnalysis;

namespace Abyssal.Common
{
    /// <summary>
    ///     A utility class for representing a cache state for <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Cachable<T>
    {
        /// <summary>
        ///     The current value of the <see cref="T"/> stored in this cachable.
        /// </summary>
        public T? Value { get; private set; }
        
        /// <summary>
        ///     The last time that the value of <see cref="T"/> was set.
        /// </summary>
        public DateTimeOffset LastUpdated { get; private set; }
        
        /// <summary>
        ///     Indicates the lifespan of the value of <see cref="T"/>.
        /// </summary>
        public TimeSpan Expiry { get; }

        /// <summary>
        ///     Creates a new <see cref="Cachable{T}"/> instance.
        /// </summary>
        /// <param name="value">The initial value of the internal store.</param>
        /// <param name="lastUpdated">When the initial value was created. Usually <code>DateTimeOffset.Now</code>.</param>
        /// <param name="expiry">When to expire the value.</param>
        public Cachable(T? value, DateTimeOffset lastUpdated, TimeSpan expiry)
        {
            Value = value;
            LastUpdated = lastUpdated;
            Expiry = expiry;
        }

        /// <summary>
        ///     Creates a new <see cref="Cachable{T}"/> instance, with no initial value.
        ///     Note that this instance will start as an expired instance.
        /// </summary>
        /// <param name="expiry">When to expire the value.</param>
        /// <returns>The created <see cref="Cachable{T}"/>.</returns>
        public static Cachable<T?> Empty(TimeSpan expiry)
        {
            return new(default, DateTimeOffset.UnixEpoch, expiry);
        }

        /// <summary>
        ///     Validates whether the internal value has expired, using <see cref="LastUpdated"/> and <see cref="Expiry"/>.
        /// </summary>
        [MemberNotNullWhen(false, "Value")]
        public bool IsExpired()
        {
            return Value == null || DateTimeOffset.Now.ToUnixTimeSeconds() >
                LastUpdated.ToUnixTimeSeconds() + Expiry.TotalSeconds;
        }

        /// <summary>
        ///     Updates the value of this <see cref="Cachable{T}"/>.
        /// </summary>
        /// <param name="newValue">The new value to set.</param>
        public void Set(T newValue)
        {
            Value = newValue;
            LastUpdated = DateTimeOffset.Now;
        }

        /// <summary>
        ///     Attempts to return the current value of this <see cref="Cachable{T}"/>, returning
        ///     false if the internal value is expired or null.
        /// </summary>
        public bool TryGetValue([NotNullWhen(true)] out T? value)
        {
            if (IsExpired())
            {
                value = default;
                return false;
            }

            value = Value;
            return true;
        }
    }
}