using System;
using System.Collections.Generic;

namespace Abyssal.Common
{
    /// <summary>
    ///     Represents a key-value store that maps <see cref="TKey"/> instances to cachable instances of
    ///     <see cref="TValue"/>.
    /// </summary>
    /// <typeparam name="TKey">The key of this dictionary.</typeparam>
    /// <typeparam name="TValue">The cachable value.</typeparam>
    public class CachableDictionary<TKey, TValue>: Dictionary<TKey, Cachable<TValue>>
        where TKey: notnull
    {
        private readonly Dictionary<TKey, Cachable<TValue>> _dictionary = new();
        private readonly TimeSpan _expiry;
        
        private CachableDictionary(TimeSpan expiry)
        {
            _expiry = expiry;
        }

        /// <summary>
        ///     Creates a new cachable dictionary.
        /// </summary>
        /// <param name="expiry">The default expiry of cachable items inside.</param>
        public static CachableDictionary<TKey, TValue> Empty(TimeSpan expiry)
        {
            return new(expiry);
        }

        /// <summary>
        ///     Gets the internal <see cref="Cachable{T}"/> for a key.
        /// </summary>
        public Cachable<TValue> GetCachable(TKey key)
        {
            var internalCache = this.GetValueOrDefault(key) ?? Cachable<TValue>.Empty(_expiry); // TODO: ???
            this[key] = internalCache;
            return internalCache;
        }
        
        /// <summary>
        ///     Sets the value for a key.
        /// </summary>
        public void SetCachedValue(TKey key, TValue value)
        {
            var cachable = GetCachable(key);
            cachable.Set(value);
        }

        /// <summary>
        ///     Gets the value for a cached key.
        /// </summary>
        public TValue? GetCachedValue(TKey key)
        {
            GetCachable(key).TryGetValue(out var result);
            return result;
        }
        
        /// <summary>
        ///     Gets the value for a cached key.
        /// </summary>
        public bool TryGetCachedValue(TKey key, out TValue? value)
        {
            return GetCachable(key).TryGetValue(out value);
        }
    }
}