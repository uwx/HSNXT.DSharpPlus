// Decompiled with JetBrains decompiler
// Type: TestProj47.JsonObject
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System;
using System.Collections;
using SGC = System.Collections.Generic;

namespace HSNXT
{
    /// <inheritdoc />
    /// <summary>Represents the JSON object.</summary>
    internal class JsonObject : SGC.IDictionary<string, object>
    {
        /// <summary>The internal member dictionary.</summary>
        private readonly SGC.Dictionary<string, object> _members;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:HSNXT.JsonObject" /> class.
        /// </summary>
        public JsonObject()
        {
            this._members = new SGC.Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:HSNXT.JsonObject" /> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public JsonObject(SGC.IEqualityComparer<string> comparer)
        {
            this._members = new SGC.Dictionary<string, object>(comparer);
        }

        /// <summary>Gets the count.</summary>
        /// <value>The count.</value>
        public int Count => this._members.Count;

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly => false;

        /// <summary>Gets the keys.</summary>
        /// <value>The keys.</value>
        public SGC.ICollection<string> Keys => this._members.Keys;

        /// <summary>Gets the values.</summary>
        /// <value>The values.</value>
        public SGC.ICollection<object> Values => this._members.Values;

        /// <summary>
        /// Gets the <see cref="T:System.Object" /> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The object.</returns>
        public object this[int index] => GetAtIndex(this._members, index);

        /// <summary>
        /// Gets or sets the <see cref="T:System.Object" /> with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The object.</returns>
        public object this[string key]
        {
            get => this._members[key];
            set => this._members[key] = value;
        }

        /// <summary>Adds the specified key.</summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(string key, object value)
        {
            this._members.Add(key, value);
        }

        /// <summary>Adds the specified item.</summary>
        /// <param name="item">The item.</param>
        public void Add(SGC.KeyValuePair<string, object> item)
        {
            this._members.Add(item.Key, item.Value);
        }

        /// <summary>Clears this instance.</summary>
        public void Clear()
        {
            this._members.Clear();
        }

        /// <summary>Determines whether [contains] [the specified item].</summary>
        /// <param name="item">The item.</param>
        /// <returns>true if contains the specified item; otherwise, false.</returns>
        public bool Contains(SGC.KeyValuePair<string, object> item)
        {
            if (this._members.ContainsKey(item.Key))
                return this._members[item.Key] == item.Value;
            return false;
        }

        /// <summary>Determines whether the specified key contains key.</summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if the specified key contains key; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(string key)
        {
            return this._members.ContainsKey(key);
        }

        /// <summary>Copies SGC.KeyValuePair to array.</summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(SGC.KeyValuePair<string, object>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            var count = this.Count;
            foreach (var keyValuePair in this)
            {
                array[arrayIndex++] = keyValuePair;
                if (--count <= 0)
                    break;
            }
        }

        /// <summary>Gets the enumerator.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public SGC.IEnumerator<SGC.KeyValuePair<string, object>> GetEnumerator()
        {
            return (SGC.IEnumerator<SGC.KeyValuePair<string, object>>) ((IEnumerable) this).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._members.GetEnumerator();
        }

        /// <summary>Removes the specified key.</summary>
        /// <param name="key">The key.</param>
        /// <returns>true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
        public bool Remove(string key)
        {
            return this._members.Remove(key);
        }

        /// <summary>Removes the specified item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        public bool Remove(SGC.KeyValuePair<string, object> item)
        {
            return this._members.Remove(item.Key);
        }

        /// <summary>
        /// Returns a json <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>A json <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        /// <summary>Tries the get value.</summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
        public bool TryGetValue(string key, out object value)
        {
            return this._members.TryGetValue(key, out value);
        }

        /// <summary>Gets at index.</summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="index">The index.</param>
        /// <returns>The object.</returns>
        internal static object GetAtIndex(SGC.IDictionary<string, object> dictionary, int index)
        {
            if (dictionary == null)
                throw new ArgumentNullException("obj");
            if (index >= dictionary.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            var num = 0;
            foreach (var keyValuePair in dictionary)
            {
                if (num++ == index)
                    return keyValuePair.Value;
            }
            return null;
        }
    }
}