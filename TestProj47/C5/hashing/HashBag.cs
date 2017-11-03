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
using SCG = System.Collections.Generic;

namespace TestProj47.C5
{
    /// <summary>
    /// A bag collection based on a hash table of (item,count) pairs. 
    /// </summary>
    [Serializable]
    public class HashBag<T> : CollectionBase<T>, ICollection<T>
    {
        #region Fields

        private HashSet<KeyValuePair<T, int>> dict;
        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public override EventTypeEnum ListenableEvents => EventTypeEnum.Basic;

        #endregion

        #region Constructors
        /// <summary>
        /// Create a hash bag with the default item equalityComparer.
        /// </summary>
		public HashBag(MemoryType memoryType = MemoryType.Normal) : this(EqualityComparer<T>.Default, memoryType) { }

        /// <summary>
        /// Create a hash bag with an external item equalityComparer.
        /// </summary>
        /// <param name="itemequalityComparer">The external item equalityComparer.</param>
		/// <param name = "memoryType"></param>
		public HashBag(SCG.IEqualityComparer<T> itemequalityComparer, MemoryType memoryType = MemoryType.Normal)
			: base(itemequalityComparer, memoryType)
        {
			if (memoryType != MemoryType.Normal) 
				throw new Exception("HashBag doesn't still support Safe and Strict memory type.");
			
			dict = new HashSet<KeyValuePair<T, int>>(new KeyValuePairEqualityComparer<T, int>(itemequalityComparer), memoryType);
        }

        /// <summary>
        /// Create a hash bag with external item equalityComparer, prescribed initial table size and default fill threshold (66%)
        /// </summary>
        /// <param name="capacity">Initial table size (rounded to power of 2, at least 16)</param>
        /// <param name="itemequalityComparer">The external item equalitySCG.Comparer</param>
		/// <param name = "memoryType"></param>
		public HashBag(int capacity, SCG.IEqualityComparer<T> itemequalityComparer, MemoryType memoryType = MemoryType.Normal)
			: base(itemequalityComparer, memoryType)
        {
			if (memoryType != MemoryType.Normal) 
				throw new Exception("HashBag doesn't still support Safe and Strict memory type.");
			
			dict = new HashSet<KeyValuePair<T, int>>(capacity, new KeyValuePairEqualityComparer<T, int>(itemequalityComparer), memoryType);
        }


        /// <summary>
        /// Create a hash bag with external item equalityComparer, prescribed initial table size and fill threshold.
        /// </summary>
        /// <param name="capacity">Initial table size (rounded to power of 2, at least 16)</param>
        /// <param name="fill">Fill threshold (valid range 10% to 90%)</param>
        /// <param name="itemequalityComparer">The external item equalitySCG.Comparer</param>
		/// <param name = "memoryType"></param>
		public HashBag(int capacity, double fill, SCG.IEqualityComparer<T> itemequalityComparer, MemoryType memoryType = MemoryType.Normal)
			: base(itemequalityComparer, memoryType)
        {
			if (memoryType != MemoryType.Normal) 
				throw new Exception("HashBag doesn't still support Safe and Strict memory type.");
			
			dict = new HashSet<KeyValuePair<T, int>>(capacity, fill, new KeyValuePairEqualityComparer<T, int>(itemequalityComparer), memoryType);
        }

        #endregion

        #region IEditableCollection<T> Members

        /// <summary>
        /// The complexity of the Contains operation
        /// </summary>
        /// <value>Always returns Speed.Constant</value>
        public virtual Speed ContainsSpeed => Speed.Constant;

        /// <summary>
        /// Check if an item is in the bag 
        /// </summary>
        /// <param name="item">The item to look for</param>
        /// <returns>True if bag contains item</returns>
        public virtual bool Contains(T item)
        {
            return dict.Contains(new KeyValuePair<T, int>(item, 0));
        }


        /// <summary>
        /// Check if an item (collection equal to a given one) is in the bag and
        /// if so report the actual item object found.
        /// </summary>
        /// <param name="item">On entry, the item to look for.
        /// On exit the item found, if any</param>
        /// <returns>True if bag contains item</returns>
        public virtual bool Find(ref T item)
        {
            var p = new KeyValuePair<T, int>(item, 0);

            if (dict.Find(ref p))
            {
                item = p.Key;
                return true;
            }

            return false;
        }


        /// <summary>
        /// Check if an item (collection equal to a given one) is in the bag and
        /// if so replace the item object in the bag with the supplied one.
        /// </summary>
        /// <param name="item">The item object to update with</param>
        /// <returns>True if item was found (and updated)</returns>
        public virtual bool Update(T item)
        { T olditem = default; return Update(item, out olditem); }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="olditem"></param>
        /// <returns></returns>
        public virtual bool Update(T item, out T olditem)
        {
            var p = new KeyValuePair<T, int>(item, 0);

            updatecheck();

            //Note: we cannot just do dict.Update: we have to lookup the count before we 
            //know what to update with. There is of course a way around if we use the 
            //implementation of hashset -which we do not want to do.
            //The hashbag is moreover mainly a proof of concept
            if (dict.Find(ref p))
            {
                olditem = p.Key;
                p.Key = item;
                dict.Update(p);
                if (ActiveEvents != 0)
                    raiseForUpdate(item, olditem, p.Value);
                return true;
            }

            olditem = default;
            return false;
        }


        /// <summary>
        /// Check if an item (collection equal to a given one) is in the bag.
        /// If found, report the actual item object in the bag,
        /// else add the supplied one.
        /// </summary>
        /// <param name="item">On entry, the item to look for or add.
        /// On exit the actual object found, if any.</param>
        /// <returns>True if item was found</returns>
        public virtual bool FindOrAdd(ref T item)
        {
            updatecheck();
            if (Find(ref item))
                return true;

            Add(item);
            return false;
        }


        /// <summary>
        /// Check if an item (collection equal to a supplied one) is in the bag and
        /// if so replace the item object in the set with the supplied one; else
        /// add the supplied one.
        /// </summary>
        /// <param name="item">The item to look for and update or add</param>
        /// <returns>True if item was updated</returns>
        public virtual bool UpdateOrAdd(T item)
        {
            updatecheck();
            if (Update(item))
                return true;

            Add(item);
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="olditem"></param>
        /// <returns></returns>
        public virtual bool UpdateOrAdd(T item, out T olditem)
        {
            updatecheck();
            if (Update(item, out olditem))
                return true;

            Add(item);
            return false;
        }

        /// <summary>
        /// Remove one copy of an item from the bag
        /// </summary>
        /// <param name="item">The item to remove</param>
        /// <returns>True if item was (found and) removed </returns>
        public virtual bool Remove(T item)
        {
            var p = new KeyValuePair<T, int>(item, 0);

            updatecheck();
            if (dict.Find(ref p))
            {
                size--;
                if (p.Value == 1)
                    dict.Remove(p);
                else
                {
                    p.Value--;
                    dict.Update(p);
                }
                if (ActiveEvents != 0)
                    raiseForRemove(p.Key);
                return true;
            }

            return false;
        }


        /// <summary>
        /// Remove one copy of an item from the bag, reporting the actual matching item object.
        /// </summary>
        /// <param name="item">The value to remove.</param>
        /// <param name="removeditem">The removed value.</param>
        /// <returns>True if item was found.</returns>
        public virtual bool Remove(T item, out T removeditem)
        {
            updatecheck();
            var p = new KeyValuePair<T, int>(item, 0);
            if (dict.Find(ref p))
            {
                removeditem = p.Key;
                size--;
                if (p.Value == 1)
                    dict.Remove(p);
                else
                {
                    p.Value--;
                    dict.Update(p);
                }
                if (ActiveEvents != 0)
                    raiseForRemove(removeditem);

                return true;
            }

            removeditem = default;
            return false;
        }

        /// <summary>
        /// Remove all items in a supplied collection from this bag, counting multiplicities.
        /// </summary>
        /// <param name="items">The items to remove.</param>
        public virtual void RemoveAll(SCG.IEnumerable<T> items)
        {
//#warning Improve if items is a counting bag
            updatecheck();
            var mustRaise = (ActiveEvents & (EventTypeEnum.Changed | EventTypeEnum.Removed)) != 0;
            var raiseHandler = mustRaise ? new RaiseForRemoveAllHandler(this) : null;
            foreach (var item in items)
            {
                var p = new KeyValuePair<T, int>(item, 0);
                if (dict.Find(ref p))
                {
                    size--;
                    if (p.Value == 1)
                        dict.Remove(p);
                    else
                    {
                        p.Value--;
                        dict.Update(p);
                    }
                    if (mustRaise)
                        raiseHandler.Remove(p.Key);
                }
            }
            if (mustRaise)
                raiseHandler.Raise();
        }

        /// <summary>
        /// Remove all items from the bag, resetting internal table to initial size.
        /// </summary>
        public virtual void Clear()
        {
            updatecheck();
            if (size == 0)
                return;
            dict.Clear();
            var oldsize = size;
            size = 0;
            if ((ActiveEvents & EventTypeEnum.Cleared) != 0)
                raiseCollectionCleared(true, oldsize);
            if ((ActiveEvents & EventTypeEnum.Changed) != 0)
                raiseCollectionChanged();
        }


        /// <summary>
        /// Remove all items *not* in a supplied collection from this bag,
        /// counting multiplicities.
        /// </summary>
        /// <param name="items">The items to retain</param>
        public virtual void RetainAll(SCG.IEnumerable<T> items)
        {
            updatecheck();

            var res = new HashBag<T>(itemequalityComparer);

            foreach (var item in items)
            {
                var p = new KeyValuePair<T, int>(item);
                if (dict.Find(ref p))
                {
                    var q = p;
                    if (res.dict.Find(ref q))
                    {
                        if (q.Value < p.Value)
                        {
                            q.Value++;
                            res.dict.Update(q);
                            res.size++;
                        }
                    }
                    else
                    {
                        q.Value = 1;
                        res.dict.Add(q);
                        res.size++;
                    }
                }
            }

            if (size == res.size)
                return;

            CircularQueue<T> wasRemoved = null;
            if ((ActiveEvents & EventTypeEnum.Removed) != 0)
            {
                wasRemoved = new CircularQueue<T>();
                foreach (var p in dict)
                {
                    var removed = p.Value - res.ContainsCount(p.Key);
                    if (removed > 0)
//#warning We could send bag events here easily using a CircularQueue of (should?)
                        for (var i = 0; i < removed; i++)
                            wasRemoved.Enqueue(p.Key);
                }
            }
            dict = res.dict;
            size = res.size;

            if ((ActiveEvents & EventTypeEnum.Removed) != 0)
                raiseForRemoveAll(wasRemoved);
            else if ((ActiveEvents & EventTypeEnum.Changed) != 0)
                raiseCollectionChanged();
        }

        /// <summary>
        /// Check if all items in a supplied collection is in this bag
        /// (counting multiplicities). 
        /// </summary>
        /// <param name="items">The items to look for.</param>
        /// <returns>True if all items are found.</returns>
        public virtual bool ContainsAll(SCG.IEnumerable<T> items)
        {
            var res = new HashBag<T>(itemequalityComparer);

            foreach (var item in items)
                if (res.ContainsCount(item) < ContainsCount(item))
                    res.Add(item);
                else
                    return false;

            return true;
        }


        /// <summary>
        /// Create an array containing all items in this bag (in enumeration order).
        /// </summary>
        /// <returns>The array</returns>
        public override T[] ToArray()
        {
            var res = new T[size];
            var ind = 0;

            foreach (var p in dict)
                for (var i = 0; i < p.Value; i++)
                    res[ind++] = p.Key;

            return res;
        }


        /// <summary>
        /// Count the number of times an item is in this set.
        /// </summary>
        /// <param name="item">The item to look for.</param>
        /// <returns>The count</returns>
        public virtual int ContainsCount(T item)
        {
            var p = new KeyValuePair<T, int>(item, 0);

            if (dict.Find(ref p))
                return p.Value;

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual ICollectionValue<T> UniqueItems() { return new DropMultiplicity<T>(dict); }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual ICollectionValue<KeyValuePair<T, int>> ItemMultiplicities()
        {
            return new GuardedCollectionValue<KeyValuePair<T, int>>(dict);
        }

        /// <summary>
        /// Remove all copies of item from this set.
        /// </summary>
        /// <param name="item">The item to remove</param>
        public virtual void RemoveAllCopies(T item)
        {
            updatecheck();

            var p = new KeyValuePair<T, int>(item, 0);

            if (dict.Find(ref p))
            {
                size -= p.Value;
                dict.Remove(p);
                if ((ActiveEvents & EventTypeEnum.Removed) != 0)
                    raiseItemsRemoved(p.Key, p.Value);
                if ((ActiveEvents & EventTypeEnum.Changed) != 0)
                    raiseCollectionChanged();
            }
        }

        #endregion

        #region ICollection<T> Members


        /// <summary>
        /// Copy the items of this bag to part of an array.
        /// <exception cref="ArgumentOutOfRangeException"/> if i is negative.
        /// <exception cref="ArgumentException"/> if the array does not have room for the items.
        /// </summary>
        /// <param name="array">The array to copy to</param>
        /// <param name="index">The starting index.</param>
        public override void CopyTo(T[] array, int index)
        {
            if (index < 0 || index + Count > array.Length)
                throw new ArgumentOutOfRangeException();

            foreach (var p in dict)
                for (var j = 0; j < p.Value; j++)
                    array[index++] = p.Key;
        }

        #endregion

        #region IExtensible<T> Members

        /// <summary>
        /// Report if this is a set collection.
        /// </summary>
        /// <value>Always true</value>
        public virtual bool AllowsDuplicates => true;

        /// <summary>
        /// By convention this is true for any collection with set semantics.
        /// </summary>
        /// <value>True if only one representative of a group of equal items 
        /// is kept in the collection together with the total count.</value>
        public virtual bool DuplicatesByCounting => true;

        /// <summary>
        /// Add an item to this bag.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>Always true</returns>
        public virtual bool Add(T item)
        {
            updatecheck();
            add(ref item);
            if (ActiveEvents != 0)
                raiseForAdd(item);
            return true;
        }

        /// <summary>
        /// Add an item to this bag.
        /// </summary>
        /// <param name="item">The item to add.</param>
        void SCG.ICollection<T>.Add(T item)
        {
            Add(item);
        }

        private void add(ref T item)
        {
            var p = new KeyValuePair<T, int>(item, 1);
            if (dict.Find(ref p))
            {
                p.Value++;
                dict.Update(p);
                item = p.Key;
            }
            else
                dict.Add(p);
            size++;
        }

        /// <summary>
        /// Add the elements from another collection with a more specialized item type 
        /// to this collection. 
        /// </summary>
        /// <param name="items">The items to add</param>
        public virtual void AddAll(SCG.IEnumerable<T> items)
        {
            updatecheck();
//#warning We could easily raise bag events
            var mustRaiseAdded = (ActiveEvents & EventTypeEnum.Added) != 0;
            var wasAdded = mustRaiseAdded ? new CircularQueue<T>() : null;
            var wasChanged = false;
            foreach (var item in items)
            {
                var jtem = item;
                add(ref jtem);
                wasChanged = true;
                if (mustRaiseAdded)
                    wasAdded.Enqueue(jtem);
            }
            if (!wasChanged)
                return;
            if (mustRaiseAdded)
                foreach (var item in wasAdded)
                    raiseItemsAdded(item, 1);
            if ((ActiveEvents & EventTypeEnum.Changed) != 0)
                raiseCollectionChanged();
        }

        #endregion

        #region IEnumerable<T> Members


        /// <summary>
        /// Choose some item of this collection. 
        /// </summary>
        /// <exception cref="NoSuchItemException">if collection is empty.</exception>
        /// <returns></returns>
        public override T Choose()
        {
            return dict.Choose().Key;
        }

        /// <summary>
        /// Create an enumerator for this bag.
        /// </summary>
        /// <returns>The enumerator</returns>
        public override SCG.IEnumerator<T> GetEnumerator()
        {
            int left;
            var mystamp = stamp;

            foreach (var p in dict)
            {
                left = p.Value;
                while (left > 0)
                {
                    if (mystamp != stamp)
                        throw new CollectionModifiedException();

                    left--;
                    yield return p.Key;
                }
            }
        }
        #endregion

        #region Diagnostics
        /// <summary>
        /// Test internal structure of data (invariants)
        /// </summary>
        /// <returns>True if pass</returns>
        public virtual bool Check()
        {
            var retval = dict.Check();
            var count = 0;

            foreach (var p in dict)
                count += p.Value;

            if (count != size)
            {
                Logger.Log(string.Format("count({0}) != size({1})", count, size));
                retval = false;
            }

            return retval;
        }
        #endregion
    }
}