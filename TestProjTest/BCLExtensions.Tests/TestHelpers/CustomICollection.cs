using System;
using System.Collections;
using System.Collections.Generic;

namespace BCLExtensions.Tests.TestHelpers
{
    public sealed class CustomICollection<T> : ICollection, IEnumerable<T>
    {
        private readonly ICollection<T> _innerCollection;

        public CustomICollection(ICollection<T> innerCollection)
        {
            _innerCollection = innerCollection;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _innerCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection)_innerCollection).CopyTo(array, index);
        }

        public int Count
        {
            get { return _innerCollection.Count; }
        }

        public object SyncRoot
        {
            get { return ((ICollection)_innerCollection).SyncRoot; }
        }

        public bool IsSynchronized
        {
            get { return ((ICollection)_innerCollection).IsSynchronized; }
        }

        public void Add(T item)
        {
            _innerCollection.Add(item);
        }
    }
}