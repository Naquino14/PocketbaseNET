// This is single handedly the most cursed C# class I have ever written
using PocketbaseNET.utils;

namespace PocketbaseNETTests.models.utils
{
    /// <summary>
    /// Represents a collection of keys and nullable values.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public sealed class NullableDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull
    {
        /// <summary>
        /// Gets or sets the specified element with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <returns>The element with the specified key.</returns>
        public new TValue? this[TKey key] 
        {
            get => ContainsKey(key) ? base[key] : default;
            set => base[key] = value!;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullableDictionary{TKey, TValue}"/> class that is empty, 
        /// has the default initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        public NullableDictionary() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullableDictionary{TKey, TValue}"/> class that 
        /// contains elements copied from the specified <see cref="IDictionary{TKey, TValue}"/> and uses the default
        /// equality comparer for the key type.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="dictionary"/> contains one or more duplicate keys.</exception>
        /// <param name="dictionary"></param>
        public NullableDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullableDictionary{TKey, TValue}"/> class that is empty, 
        /// has the specified initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is less than 0.</exception>
        /// <param name="capacity"></param>
        public NullableDictionary(int capacity) : base(capacity) { }

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        public new void Add(TKey key, TValue? value) 
            => base.Add(key, (value ?? default) ?? (TValue)new object());
    }

    /// <summary>
    /// Utility class for <see cref="NullableDictionary{TKey, TValue}"/>.
    /// </summary>
    public static class NullableDictionary
    {
        /// <summary>
        /// Convert from a <see cref="NullableDictionary{TKey, TValue}"/> to a <see cref="Dictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="dictionary">The source dictionary</param>
        /// <returns>The <see cref="NullableDictionary{TKey, TValue}"/> derived from the source.</returns>
        public static NullableDictionary<TKey, TValue> FromDictToNullableDict<TKey, TValue>(Dictionary<TKey, TValue> dictionary) where TKey : notnull
        {
            NullableDictionary<TKey, TValue> nullableDict = new();
            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
                nullableDict[pair.Key] = Cloner.ReflectiveClone<TValue>(pair.Value)!;
            return nullableDict;
        }
    }
}
