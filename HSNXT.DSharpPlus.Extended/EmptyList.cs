using System;
using System.Collections;
using System.Collections.Generic;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.Extended
{
    internal readonly struct EmptyList<T> : IReadOnlyList<T>, IList<T>
    {
        private readonly struct EmptyEnumerator : IEnumerator<T>
        {
            public void Dispose()
            {
            }

            public bool MoveNext() => false;

            public void Reset()
            {
            }

            public T Current => default;

            object IEnumerator.Current => default;
        }

        public IEnumerator<T> GetEnumerator() => new EmptyEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public void Add(T item) => throw new InvalidOperationException();
        public void Clear() => throw new InvalidOperationException();
        public bool Contains(T item) => false;
        public void CopyTo(T[] array, int arrayIndex)
        {
        }
        public bool Remove(T item) => throw new InvalidOperationException();
        public int IndexOf(T item) => -1;
        public void Insert(int index, T item) => throw new InvalidOperationException();
        public void RemoveAt(int index) => throw new InvalidOperationException();

        public int Count => 0;
        public bool IsReadOnly => true;
        
        public T this[int index]
        {
            get => throw new IndexOutOfRangeException();
            set => throw new InvalidOperationException();
        }
    }
}