using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable ForCanBeConvertedToForeach

namespace HSNXT.DSharpPlus.CNextNotifier
{
    public class CustomDictionary<TK, TV> : IDictionary<TK, TV>, IDictionary
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
            public TK Key;
            public int Next;
            public TV Value;
            public uint Hashcode;
        }

        public CustomDictionary()
        {
            Initialize();
        }

        public int InitOrGetPosition(TK key)
        {
            return Add(key, default, false);
        }

        public TV GetAtPosition(int pos)
        {
            return _entries[pos].Value;
        }

        public void StoreAtPosition(int pos, TV value)
        {
            _entries[pos].Value = value;
        }

        public int Add(TK key, TV value, bool overwrite)
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

            for (var i = 0; i < PrimeSizes.Length; i++)
            {
                if (PrimeSizes[i] >= roughsize)
                    return PrimeSizes[i];
            }

            throw new ArgumentOutOfRangeException(nameof(PrimeSizes), "Too large array");
        }

        public TV Get(TK key)
        {
            var pos = GetPosition(key);

            if (pos == -1)
                throw new ArgumentException("Key does not exist", nameof(key));

            return _entries[pos].Value;
        }

        public int GetPosition(TK key)
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

        public bool ContainsKey(TK key)
        {
            return GetPosition(key) != -1;
        }

        public ICollection<TK> Keys => throw new NotImplementedException();

        public bool Remove(TK key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TK key, out TV value)
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

        public ICollection<TV> Values => throw new NotImplementedException();

        public TV this[TK key]
        {
            get => Get(key);
            set => Add(key, value, true);
        }

        public void Add(KeyValuePair<TK, TV> item)
        {
            var pos = Add(item.Key, item.Value, false);

            if (pos + 1 != Count)
                throw new Exception("Key already exists");
        }

        void IDictionary<TK, TV>.Add(TK key, TV value)
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

        public bool Contains(KeyValuePair<TK, TV> item)
        {
            if (item.Key == null)
                return false;

            return TryGetValue(item.Key, out var value) && item.Value.Equals(value);
        }

        public void CopyTo(KeyValuePair<TK, TV>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public bool Remove(KeyValuePair<TK, TV> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return new KeyValuePair<TK, TV>(_entries[i].Key, _entries[i].Value);
            }
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return new KeyValuePair<TK, TV>(_entries[i].Key, _entries[i].Value);
            }
        }

        public void Add(object key, object value)
        {
            var pos = Add((TK) key, (TV) value, false);

            if (pos + 1 != Count)
                throw new Exception("Key already exists");
        }

        public bool Contains(object key)
        {
            return ContainsKey((TK) key);
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
            get => this[(TK) key];
            set => this[(TK) key] = (TV) value;
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();
    }
}