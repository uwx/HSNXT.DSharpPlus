/*
 Copyright (c) 2003-2017 Niels Kokholm, Peter Sestoft, and Rasmus Lystrøm
 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to deal
 in the Software without restriction, including without limitation the rights
 to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:
 
 The above copyright notice and this permission notice shall be included in
 all copies or substantial portions of the Software.
 
 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 SOFTWARE.
*/

using System;
using System.Linq;
using SCG = System.Collections.Generic;

namespace HSNXT.C5
{
    [Serializable]
    internal abstract class MappedDirectedCollectionValue<T, V> : DirectedCollectionValueBase<V>,
        IDirectedCollectionValue<V>
    {
        private IDirectedCollectionValue<T> directedcollectionvalue;

        public abstract V Map(T item);

        protected MappedDirectedCollectionValue(IDirectedCollectionValue<T> directedcollectionvalue)
        {
            this.directedcollectionvalue = directedcollectionvalue;
        }

        public override V Choose()
        {
            return Map(directedcollectionvalue.Choose());
        }

        public override bool IsEmpty => directedcollectionvalue.IsEmpty;

        public override int Count => directedcollectionvalue.Count;

        public override Speed CountSpeed => directedcollectionvalue.CountSpeed;

        public override IDirectedCollectionValue<V> Backwards()
        {
            var retval = (MappedDirectedCollectionValue<T, V>) MemberwiseClone();
            retval.directedcollectionvalue = directedcollectionvalue.Backwards();
            return retval;
            //If we made this classs non-abstract we could do
            //return new MappedDirectedCollectionValue<T,V>(directedcollectionvalue.Backwards());;
        }


        public override SCG.IEnumerator<V> GetEnumerator()
        {
            return directedcollectionvalue.Select(Map).GetEnumerator();
        }

        public override EnumerationDirection Direction => directedcollectionvalue.Direction;

        IDirectedEnumerable<V> IDirectedEnumerable<V>.Backwards()
        {
            return Backwards();
        }
    }

    [Serializable]
    internal abstract class MappedCollectionValue<T, V> : CollectionValueBase<V>, ICollectionValue<V>
    {
        private readonly ICollectionValue<T> collectionvalue;

        public abstract V Map(T item);

        public MappedCollectionValue(ICollectionValue<T> collectionvalue)
        {
            this.collectionvalue = collectionvalue;
        }

        public override V Choose()
        {
            return Map(collectionvalue.Choose());
        }

        public override bool IsEmpty => collectionvalue.IsEmpty;

        public override int Count => collectionvalue.Count;

        public override Speed CountSpeed => collectionvalue.CountSpeed;

        public override SCG.IEnumerator<V> GetEnumerator()
        {
            foreach (var item in collectionvalue)
                yield return Map(item);
        }
    }

    [Serializable]
    internal class MultiplicityOne<K> : MappedCollectionValue<K, KeyValuePair<K, int>>
    {
        public MultiplicityOne(ICollectionValue<K> coll) : base(coll)
        {
        }

        public override KeyValuePair<K, int> Map(K k)
        {
            return new KeyValuePair<K, int>(k, 1);
        }
    }

    [Serializable]
    internal class DropMultiplicity<K> : MappedCollectionValue<KeyValuePair<K, int>, K>
    {
        public DropMultiplicity(ICollectionValue<KeyValuePair<K, int>> coll) : base(coll)
        {
        }

        public override K Map(KeyValuePair<K, int> kvp)
        {
            return kvp.Key;
        }
    }

    [Serializable]
    internal abstract class MappedDirectedEnumerable<T, V> : EnumerableBase<V>, IDirectedEnumerable<V>
    {
        private IDirectedEnumerable<T> directedenumerable;

        public abstract V Map(T item);

        public MappedDirectedEnumerable(IDirectedEnumerable<T> directedenumerable)
        {
            this.directedenumerable = directedenumerable;
        }

        public IDirectedEnumerable<V> Backwards()
        {
            var retval = (MappedDirectedEnumerable<T, V>) MemberwiseClone();
            retval.directedenumerable = directedenumerable.Backwards();
            return retval;
            //If we made this classs non-abstract we could do
            //return new MappedDirectedCollectionValue<T,V>(directedcollectionvalue.Backwards());;
        }


        public override SCG.IEnumerator<V> GetEnumerator()
        {
            foreach (var item in directedenumerable)
                yield return Map(item);
        }

        public EnumerationDirection Direction => directedenumerable.Direction;
    }
}