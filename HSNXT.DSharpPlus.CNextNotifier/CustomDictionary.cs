using System;
using System.Collections;
using System.Collections.Generic;

namespace HSNXT.DSharpPlus.CNextNotifier
{
    // From http://blog.teamleadnet.com/2012/07/ultra-fast-hashtable-dictionary-with.html
    public class CustomDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary
    {
        private int[] _hashes;
        private DictionaryEntry[] _entries;

        private const int InitialSize = 89;
        //private const float Loadfactor = 1f;

        // ReSharper disable once StaticMemberInGenericType
        private static readonly uint[] PrimeSizes =
        {
            89, 179, 359, 719, 1439, 2879, 5779, 11579, 23159, 46327,
            92657, 185323, 370661, 741337, 1482707, 2965421, 5930887, 11861791,
            23723599, 47447201, 94894427, 189788857, 379577741, 759155483
        };

        //int maxitems = (int)( initialsize * loadfactor );

        private struct DictionaryEntry
        {
            public TKey Key;
            public int Next;
            public TValue Value;
            public uint Hashcode;
        }

        public CustomDictionary()
        {
            Initialize();
        }

        public int InitOrGetPosition(TKey key)
        {
            return Add(key, default, false);
        }

        public TValue GetAtPosition(int pos)
        {
            return _entries[pos].Value;
        }

        public void StoreAtPosition(int pos, TValue value)
        {
            _entries[pos].Value = value;
        }

        public int Add(TKey key, TValue value, bool overwrite)
        {
            if (Count >= _entries.Length)
                Resize();

            var hash = (uint) key.GetHashCode();

            var hashPos = hash % (uint) _hashes.Length;

            var entryLocation = _hashes[hashPos];

            var storePos = Count;


            if (entryLocation != -1) // already there
            {
                var currEntryPos = entryLocation;

                do
                {
                    var entry = _entries[currEntryPos];

                    // same key is in the dictionary
                    if (key.Equals(entry.Key))
                    {
                        if (!overwrite)
                            return currEntryPos;

                        storePos = currEntryPos;
                        break; // do not increment nextfree - overwriting the value
                    }

                    currEntryPos = entry.Next;
                } while (currEntryPos > -1);

                Count++;
            }
            else // new value
            {
                //hashcount++;
                Count++;
            }

            _hashes[hashPos] = storePos;

            _entries[storePos].Next = entryLocation;
            _entries[storePos].Key = key;
            _entries[storePos].Value = value;
            _entries[storePos].Hashcode = hash;

            return storePos;
        }

        private void Resize()
        {
            var newsize = FindNewSize();
            var newhashes = new int[newsize];
            var newentries = new DictionaryEntry[newsize];

            Array.Copy(_entries, newentries, Count);

            for (var i = 0; i < newsize; i++)
            {
                newhashes[i] = -1;
            }

            for (var i = 0; i < Count; i++)
            {
                var pos = newentries[i].Hashcode % newsize;
                var prevpos = newhashes[pos];
                newhashes[pos] = i;

                if (prevpos != -1)
                    newentries[i].Next = prevpos;
            }

            _hashes = newhashes;
            _entries = newentries;

            //maxitems = (int) (newsize * loadfactor );
        }

        private uint FindNewSize()
        {
            var roughsize = (uint) _hashes.Length * 2 + 1;

            foreach (var t in PrimeSizes)
            {
                if (t >= roughsize)
                    return t;
            }

            throw new ArgumentOutOfRangeException(nameof(PrimeSizes), "Too large array");
        }

        public TValue Get(TKey key)
        {
            var pos = GetPosition(key);

            if (pos == -1)
                throw new ArgumentException("Key does not exist", nameof(key));

            return _entries[pos].Value;
        }

        public int GetPosition(TKey key)
        {
            var hash = (uint) key.GetHashCode();

            var pos = hash % (uint) _hashes.Length;

            var entryLocation = _hashes[pos];

            if (entryLocation == -1)
                return -1;

            var nextpos = entryLocation;

            do
            {
                var entry = _entries[nextpos];

                if (key.Equals(entry.Key))
                    return nextpos;

                nextpos = entry.Next;
            } while (nextpos != -1);

            return -1;
        }

        public bool ContainsKey(TKey key)
        {
            return GetPosition(key) != -1;
        }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var pos = GetPosition(key);

            if (pos == -1)
            {
                value = default;
                return false;
            }

            value = _entries[pos].Value;

            return true;
        }

        public ICollection<TValue> Values => throw new NotImplementedException();

        public TValue this[TKey key]
        {
            get => Get(key);
            set => Add(key, value, true);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            var pos = Add(item.Key, item.Value, false);

            if (pos + 1 != Count)
                throw new Exception("Key already exists");
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            var pos = Add(key, value, false);

            if (pos + 1 != Count)
                throw new Exception("Key already exists");
        }

        public void Clear()
        {
            Initialize();
        }

        private void Initialize()
        {
            this._hashes = new int[InitialSize];
            this._entries = new DictionaryEntry[InitialSize];
            Count = 0;

            for (var i = 0; i < _entries.Length; i++)
            {
                _hashes[i] = -1;
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null)
                return false;

            return TryGetValue(item.Key, out var value) && item.Value.Equals(value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return new KeyValuePair<TKey, TValue>(_entries[i].Key, _entries[i].Value);
            }
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return new KeyValuePair<TKey, TValue>(_entries[i].Key, _entries[i].Value);
            }
        }

        public void Add(object key, object value)
        {
            var pos = Add((TKey) key, (TValue) value, false);

            if (pos + 1 != Count)
                throw new Exception("Key already exists");
        }

        public bool Contains(object key)
        {
            return ContainsKey((TKey) key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool IsFixedSize => throw new NotImplementedException();

        ICollection IDictionary.Keys => throw new NotImplementedException();

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }

        ICollection IDictionary.Values => throw new NotImplementedException();

        public object this[object key]
        {
            get => this[(TKey) key];
            set => this[(TKey) key] = (TValue) value;
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();
    }
}