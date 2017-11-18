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

namespace HSNXT.C5
{
    /// <summary>
    /// A priority queue class based on an interval heap data structure.
    /// </summary>
    /// <typeparam name="T">The item type</typeparam>
    [Serializable]
    public class IntervalHeap<T> : CollectionValueBase<T>, IPriorityQueue<T>
    {
        #region Events

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public override EventTypeEnum ListenableEvents => EventTypeEnum.Basic;

        #endregion

        #region Fields

        private struct Interval
        {
            internal T first, last;
            internal Handle firsthandle, lasthandle;


            public override string ToString()
            {
                return $"[{first}; {last}]";
            }
        }

        private int stamp;

        private readonly SCG.IComparer<T> comparer;
        private readonly SCG.IEqualityComparer<T> itemequalityComparer;

        private Interval[] heap;

        private int size;

        #endregion

        #region Util

        // heapifyMin and heapifyMax and their auxiliaries

        private void swapFirstWithLast(int cell1, int cell2)
        {
            var first = heap[cell1].first;
            var firsthandle = heap[cell1].firsthandle;
            updateFirst(cell1, heap[cell2].last, heap[cell2].lasthandle);
            updateLast(cell2, first, firsthandle);
        }

        private void swapLastWithLast(int cell1, int cell2)
        {
            var last = heap[cell2].last;
            var lasthandle = heap[cell2].lasthandle;
            updateLast(cell2, heap[cell1].last, heap[cell1].lasthandle);
            updateLast(cell1, last, lasthandle);
        }

        private void swapFirstWithFirst(int cell1, int cell2)
        {
            var first = heap[cell2].first;
            var firsthandle = heap[cell2].firsthandle;
            updateFirst(cell2, heap[cell1].first, heap[cell1].firsthandle);
            updateFirst(cell1, first, firsthandle);
        }

        private bool heapifyMin(int cell)
        {
            var swappedroot = false;
            // If first > last, swap them
            if (2 * cell + 1 < size && comparer.Compare(heap[cell].first, heap[cell].last) > 0)
            {
                swappedroot = true;
                swapFirstWithLast(cell, cell);
            }

            int currentmin = cell, l = 2 * cell + 1, r = l + 1;
            if (2 * l < size && comparer.Compare(heap[l].first, heap[currentmin].first) < 0)
                currentmin = l;
            if (2 * r < size && comparer.Compare(heap[r].first, heap[currentmin].first) < 0)
                currentmin = r;

            if (currentmin != cell)
            {
                // cell has at least one daughter, and it contains the min
                swapFirstWithFirst(currentmin, cell);
                heapifyMin(currentmin);
            }
            return swappedroot;
        }


        private bool heapifyMax(int cell)
        {
            var swappedroot = false;
            if (2 * cell + 1 < size && comparer.Compare(heap[cell].last, heap[cell].first) < 0)
            {
                swappedroot = true;
                swapFirstWithLast(cell, cell);
            }

            int currentmax = cell, l = 2 * cell + 1, r = l + 1;
            var firstmax = false; // currentmax's first field holds max
            if (2 * l + 1 < size)
            {
                // both l.first and l.last exist
                if (comparer.Compare(heap[l].last, heap[currentmax].last) > 0)
                    currentmax = l;
            }
            else if (2 * l + 1 == size)
            {
                // only l.first exists
                if (comparer.Compare(heap[l].first, heap[currentmax].last) > 0)
                {
                    currentmax = l;
                    firstmax = true;
                }
            }

            if (2 * r + 1 < size)
            {
                // both r.first and r.last exist
                if (comparer.Compare(heap[r].last, heap[currentmax].last) > 0)
                    currentmax = r;
            }
            else if (2 * r + 1 == size)
            {
                // only r.first exists
                if (comparer.Compare(heap[r].first, heap[currentmax].last) > 0)
                {
                    currentmax = r;
                    firstmax = true;
                }
            }

            if (currentmax != cell)
            {
                // The cell has at least one daughter, and it contains the max
                if (firstmax)
                    swapFirstWithLast(currentmax, cell);
                else
                    swapLastWithLast(currentmax, cell);
                heapifyMax(currentmax);
            }
            return swappedroot;
        }

        private void bubbleUpMin(int i)
        {
            if (i > 0)
            {
                T min = heap[i].first, iv = min;
                var minhandle = heap[i].firsthandle;
                var p = (i + 1) / 2 - 1;

                while (i > 0)
                {
                    if (comparer.Compare(iv, min = heap[p = (i + 1) / 2 - 1].first) < 0)
                    {
                        updateFirst(i, min, heap[p].firsthandle);
                        min = iv;
                        i = p;
                    }
                    else
                        break;
                }

                updateFirst(i, iv, minhandle);
            }
        }


        private void bubbleUpMax(int i)
        {
            if (i > 0)
            {
                T max = heap[i].last, iv = max;
                var maxhandle = heap[i].lasthandle;
                var p = (i + 1) / 2 - 1;

                while (i > 0)
                {
                    if (comparer.Compare(iv, max = heap[p = (i + 1) / 2 - 1].last) > 0)
                    {
                        updateLast(i, max, heap[p].lasthandle);
                        max = iv;
                        i = p;
                    }
                    else
                        break;
                }

                updateLast(i, iv, maxhandle);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create an interval heap with natural item comparer and default initial capacity (16)
        /// </summary>
        public IntervalHeap(MemoryType memoryType = MemoryType.Normal) : this(16, memoryType)
        {
        }


        /// <summary>
        /// Create an interval heap with external item comparer and default initial capacity (16)
        /// </summary>
        /// <param name="comparer">The external comparer</param>
        /// <param name = "memoryType"></param>
        public IntervalHeap(SCG.IComparer<T> comparer, MemoryType memoryType = MemoryType.Normal) : this(16, comparer,
            memoryType)
        {
        }


        //TODO: maybe remove
        /// <summary>
        /// Create an interval heap with natural item comparer and prescribed initial capacity
        /// </summary>
        /// <param name="capacity">The initial capacity</param>
        /// <param name = "memoryType"></param>
        public IntervalHeap(int capacity, MemoryType memoryType = MemoryType.Normal) : this(capacity,
            SCG.Comparer<T>.Default, EqualityComparer<T>.Default, memoryType)
        {
        }


        /// <summary>
        /// Create an interval heap with external item comparer and prescribed initial capacity
        /// </summary>
        /// <param name="comparer">The external comparer</param>
        /// <param name="capacity">The initial capacity</param>
        /// <param name = "memoryType"></param>
        public IntervalHeap(int capacity, SCG.IComparer<T> comparer, MemoryType memoryType = MemoryType.Normal) : this(
            capacity, comparer, new ComparerZeroHashCodeEqualityComparer<T>(comparer), memoryType)
        {
        }

        private IntervalHeap(int capacity, SCG.IComparer<T> comparer, SCG.IEqualityComparer<T> itemequalityComparer,
            MemoryType memoryType = MemoryType.Normal)
        {
            if (comparer == null)
                throw new NullReferenceException("Item comparer cannot be null");
            if (itemequalityComparer == null)
                throw new NullReferenceException("Item equality comparer cannot be null");

            if (memoryType != MemoryType.Normal)
                throw new Exception("IntervalHeap still doesn't support MemoryType Strict or Safe");

            this.comparer = comparer;
            this.itemequalityComparer = itemequalityComparer;
            var length = 1;
            while (length < capacity) length <<= 1;
            heap = new Interval[length];
        }

        #endregion

        #region IPriorityQueue<T> Members

        /// <summary>
        /// Find the current least item of this priority queue.
        /// <exception cref="NoSuchItemException"/> if queue is empty
        /// </summary>
        /// <returns>The least item.</returns>
        public T FindMin()
        {
            if (size == 0)
                throw new NoSuchItemException();

            return heap[0].first;
        }


        /// <summary>
        /// Remove the least item from this  priority queue.
        /// <exception cref="NoSuchItemException"/> if queue is empty
        /// </summary>
        /// <returns>The removed item.</returns>
        public T DeleteMin()
        {
            IPriorityQueueHandle<T> handle = null;
            return DeleteMin(out handle);
        }


        /// <summary>
        /// Find the current largest item of this priority queue.
        /// <exception cref="NoSuchItemException"/> if queue is empty
        /// </summary>
        /// <returns>The largest item.</returns>
        public T FindMax()
        {
            if (size == 0)
                throw new NoSuchItemException("Heap is empty");
            if (size == 1)
                return heap[0].first;
            return heap[0].last;
        }


        /// <summary>
        /// Remove the largest item from this  priority queue.
        /// <exception cref="NoSuchItemException"/> if queue is empty
        /// </summary>
        /// <returns>The removed item.</returns>
        public T DeleteMax()
        {
            IPriorityQueueHandle<T> handle = null;
            return DeleteMax(out handle);
        }


        /// <summary>
        /// The comparer object supplied at creation time for this collection
        /// </summary>
        /// <value>The comparer</value>
        public SCG.IComparer<T> Comparer => comparer;

        #endregion

        #region IExtensible<T> Members

        /// <summary>
        /// If true any call of an updating operation will throw an
        /// <code>ReadOnlyCollectionException</code>
        /// </summary>
        /// <value>True if this collection is read-only.</value>
        public bool IsReadOnly => false;

        /// <summary>
        /// 
        /// </summary>
        /// <value>True since this collection has bag semantics</value>
        public bool AllowsDuplicates => true;

        /// <summary>
        /// Value is null since this collection has no equality concept for its items. 
        /// </summary>
        /// <value></value>
        public virtual SCG.IEqualityComparer<T> EqualityComparer => itemequalityComparer;

        /// <summary>
        /// By convention this is true for any collection with set semantics.
        /// </summary>
        /// <value>True if only one representative of a group of equal items 
        /// is kept in the collection together with the total count.</value>
        public virtual bool DuplicatesByCounting => false;


        /// <summary>
        /// Add an item to this priority queue.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>True</returns>
        public bool Add(T item)
        {
            stamp++;
            if (add(null, item))
            {
                raiseItemsAdded(item, 1);
                raiseCollectionChanged();
                return true;
            }
            return false;
        }

        private bool add(Handle itemhandle, T item)
        {
            if (size == 0)
            {
                size = 1;
                updateFirst(0, item, itemhandle);
                return true;
            }

            if (size == 2 * heap.Length)
            {
                var newheap = new Interval[2 * heap.Length];

                Array.Copy(heap, newheap, heap.Length);
                heap = newheap;
            }

            if (size % 2 == 0)
            {
                int i = size / 2, p = (i + 1) / 2 - 1;
                var tmp = heap[p].last;

                if (comparer.Compare(item, tmp) > 0)
                {
                    updateFirst(i, tmp, heap[p].lasthandle);
                    updateLast(p, item, itemhandle);
                    bubbleUpMax(p);
                }
                else
                {
                    updateFirst(i, item, itemhandle);

                    if (comparer.Compare(item, heap[p].first) < 0)
                        bubbleUpMin(i);
                }
            }
            else
            {
                var i = size / 2;
                var other = heap[i].first;

                if (comparer.Compare(item, other) < 0)
                {
                    updateLast(i, other, heap[i].firsthandle);
                    updateFirst(i, item, itemhandle);
                    bubbleUpMin(i);
                }
                else
                {
                    updateLast(i, item, itemhandle);
                    bubbleUpMax(i);
                }
            }
            size++;

            return true;
        }

        private void updateLast(int cell, T item, Handle handle)
        {
            heap[cell].last = item;
            if (handle != null)
                handle.index = 2 * cell + 1;
            heap[cell].lasthandle = handle;
        }

        private void updateFirst(int cell, T item, Handle handle)
        {
            heap[cell].first = item;
            if (handle != null)
                handle.index = 2 * cell;
            heap[cell].firsthandle = handle;
        }


        /// <summary>
        /// Add the elements from another collection with a more specialized item type 
        /// to this collection. 
        /// </summary>
        /// <param name="items">The items to add</param>
        public void AddAll(SCG.IEnumerable<T> items)
        {
            stamp++;
            var oldsize = size;
            foreach (var item in items)
                add(null, item);
            if (size != oldsize)
            {
                if ((ActiveEvents & EventTypeEnum.Added) != 0)
                    foreach (var item in items)
                        raiseItemsAdded(item, 1);
                raiseCollectionChanged();
            }
        }

        #endregion

        #region ICollection<T> members

        /// <summary>
        /// 
        /// </summary>
        /// <value>True if this collection is empty.</value>
        public override bool IsEmpty => size == 0;

        /// <summary>
        /// 
        /// </summary>
        /// <value>The size of this collection</value>
        public override int Count => size;


        /// <summary>
        /// The value is symbolic indicating the type of asymptotic complexity
        /// in terms of the size of this collection (worst-case or amortized as
        /// relevant).
        /// </summary>
        /// <value>A characterization of the speed of the 
        /// <code>Count</code> property in this collection.</value>
        public override Speed CountSpeed => Speed.Constant;

        /// <summary>
        /// Choose some item of this collection. 
        /// </summary>
        /// <exception cref="NoSuchItemException">if collection is empty.</exception>
        /// <returns></returns>
        public override T Choose()
        {
            if (size == 0)
                throw new NoSuchItemException("Collection is empty");
            return heap[0].first;
        }


        /// <summary>
        /// Create an enumerator for the collection
        /// <para>Note: the enumerator does *not* enumerate the items in sorted order, 
        /// but in the internal table order.</para>
        /// </summary>
        /// <returns>The enumerator(SIC)</returns>
        public override SCG.IEnumerator<T> GetEnumerator()
        {
            var mystamp = stamp;
            for (var i = 0; i < size; i++)
            {
                if (mystamp != stamp) throw new CollectionModifiedException();
                yield return i % 2 == 0 ? heap[i >> 1].first : heap[i >> 1].last;
            }
        }

        #endregion

        #region Diagnostics

        // Check invariants: 
        // * first <= last in a cell if both are valid
        // * a parent interval (cell) contains both its daughter intervals (cells)
        // * a handle, if non-null, points to the cell it is associated with
        private bool check(int i, T min, T max)
        {
            var retval = true;
            var interval = heap[i];
            T first = interval.first, last = interval.last;

            if (2 * i + 1 == size)
            {
                if (comparer.Compare(min, first) > 0)
                {
                    Logger.Log($"Cell {i}: parent.first({min}) > first({first})  [size={size}]");
                    retval = false;
                }

                if (comparer.Compare(first, max) > 0)
                {
                    Logger.Log(
                        $"Cell {i}: first({first}) > parent.last({max})  [size={size}]");
                    retval = false;
                }
                if (interval.firsthandle != null && interval.firsthandle.index != 2 * i)
                {
                    Logger.Log(
                        $"Cell {i}: firsthandle.index({interval.firsthandle.index}) != 2*cell({2 * i})  [size={size}]");
                    retval = false;
                }

                return retval;
            }
            if (comparer.Compare(min, first) > 0)
            {
                Logger.Log($"Cell {i}: parent.first({min}) > first({first})  [size={size}]");
                retval = false;
            }

            if (comparer.Compare(first, last) > 0)
            {
                Logger.Log($"Cell {i}: first({first}) > last({last})  [size={size}]");
                retval = false;
            }

            if (comparer.Compare(last, max) > 0)
            {
                Logger.Log($"Cell {i}: last({last}) > parent.last({max})  [size={size}]");
                retval = false;
            }
            if (interval.firsthandle != null && interval.firsthandle.index != 2 * i)
            {
                Logger.Log(
                    $"Cell {i}: firsthandle.index({interval.firsthandle.index}) != 2*cell({2 * i})  [size={size}]");
                retval = false;
            }
            if (interval.lasthandle != null && interval.lasthandle.index != 2 * i + 1)
            {
                Logger.Log(
                    $"Cell {i}: lasthandle.index({interval.lasthandle.index}) != 2*cell+1({2 * i + 1})  [size={size}]");
                retval = false;
            }

            int l = 2 * i + 1, r = l + 1;

            if (2 * l < size)
                retval = retval && check(l, first, last);

            if (2 * r < size)
                retval = retval && check(r, first, last);

            return retval;
        }


        /// <summary>
        /// Check the integrity of the internal data structures of this collection.
        /// Only available in DEBUG builds???
        /// </summary>
        /// <returns>True if check does not fail.</returns>
        public bool Check()
        {
            if (size == 0)
                return true;

            if (size == 1)
                return heap[0].first != null;

            return check(0, heap[0].first, heap[0].last);
        }

        #endregion

        #region IPriorityQueue<T> Members

        [Serializable]
        private class Handle : IPriorityQueueHandle<T>
        {
            /// <summary>
            /// To save space, the index is 2*cell for heap[cell].first, and 2*cell+1 for heap[cell].last
            /// </summary>
            internal int index = -1;

            public override string ToString()
            {
                return $"[{index}]";
            }
        }

        /// <summary>
        /// Get or set the item corresponding to a handle. 
        /// </summary>
        /// <exception cref="InvalidPriorityQueueHandleException">if the handle is invalid for this queue</exception>
        /// <param name="handle">The reference into the heap</param>
        /// <returns></returns>
        public T this[IPriorityQueueHandle<T> handle]
        {
            get
            {
                int cell;
                bool isfirst;
                checkHandle(handle, out cell, out isfirst);

                return isfirst ? heap[cell].first : heap[cell].last;
            }
            set => Replace(handle, value);
        }


        /// <summary>
        /// Check safely if a handle is valid for this queue and if so, report the corresponding queue item.
        /// </summary>
        /// <param name="handle">The handle to check</param>
        /// <param name="item">If the handle is valid this will contain the corresponding item on output.</param>
        /// <returns>True if the handle is valid.</returns>
        public bool Find(IPriorityQueueHandle<T> handle, out T item)
        {
            var myhandle = handle as Handle;
            if (myhandle == null)
            {
                item = default;
                return false;
            }
            var toremove = myhandle.index;
            var cell = toremove / 2;
            var isfirst = toremove % 2 == 0;
            {
                if (toremove == -1 || toremove >= size)
                {
                    item = default;
                    return false;
                }
                var actualhandle = isfirst ? heap[cell].firsthandle : heap[cell].lasthandle;
                if (actualhandle != myhandle)
                {
                    item = default;
                    return false;
                }
            }
            item = isfirst ? heap[cell].first : heap[cell].last;
            return true;
        }


        /// <summary>
        /// Add an item to the priority queue, receiving a 
        /// handle for the item in the queue, 
        /// or reusing an already existing handle.
        /// </summary>
        /// <param name="handle">On output: a handle for the added item. 
        /// On input: null for allocating a new handle, an invalid handle for reuse. 
        /// A handle for reuse must be compatible with this priority queue, 
        /// by being created by a priority queue of the same runtime type, but not 
        /// necessarily the same priority queue object.</param>
        /// <param name="item">The item to add.</param>
        /// <returns>True since item will always be added unless the call throws an exception.</returns>
        public bool Add(ref IPriorityQueueHandle<T> handle, T item)
        {
            stamp++;
            var myhandle = (Handle) handle;
            if (myhandle == null)
                handle = myhandle = new Handle();
            else if (myhandle.index != -1)
                throw new InvalidPriorityQueueHandleException("Handle not valid for reuse");
            if (add(myhandle, item))
            {
                raiseItemsAdded(item, 1);
                raiseCollectionChanged();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Delete an item with a handle from a priority queue.
        /// </summary>
        /// <exception cref="InvalidPriorityQueueHandleException">if the handle is invalid</exception>
        /// <param name="handle">The handle for the item. The handle will be invalidated, but reusable.</param>
        /// <returns>The deleted item</returns>
        public T Delete(IPriorityQueueHandle<T> handle)
        {
            stamp++;
            int cell;
            bool isfirst;
            var myhandle = checkHandle(handle, out cell, out isfirst);

            T retval;
            myhandle.index = -1;
            var lastcell = (size - 1) / 2;

            if (cell == lastcell)
            {
                if (isfirst)
                {
                    retval = heap[cell].first;
                    if (size % 2 == 0)
                    {
                        updateFirst(cell, heap[cell].last, heap[cell].lasthandle);
                        heap[cell].last = default;
                        heap[cell].lasthandle = null;
                    }
                    else
                    {
                        heap[cell].first = default;
                        heap[cell].firsthandle = null;
                    }
                }
                else
                {
                    retval = heap[cell].last;
                    heap[cell].last = default;
                    heap[cell].lasthandle = null;
                }
                size--;
            }
            else if (isfirst)
            {
                retval = heap[cell].first;

                if (size % 2 == 0)
                {
                    updateFirst(cell, heap[lastcell].last, heap[lastcell].lasthandle);
                    heap[lastcell].last = default;
                    heap[lastcell].lasthandle = null;
                }
                else
                {
                    updateFirst(cell, heap[lastcell].first, heap[lastcell].firsthandle);
                    heap[lastcell].first = default;
                    heap[lastcell].firsthandle = null;
                }

                size--;
                if (heapifyMin(cell))
                    bubbleUpMax(cell);
                else
                    bubbleUpMin(cell);
            }
            else
            {
                retval = heap[cell].last;

                if (size % 2 == 0)
                {
                    updateLast(cell, heap[lastcell].last, heap[lastcell].lasthandle);
                    heap[lastcell].last = default;
                    heap[lastcell].lasthandle = null;
                }
                else
                {
                    updateLast(cell, heap[lastcell].first, heap[lastcell].firsthandle);
                    heap[lastcell].first = default;
                    heap[lastcell].firsthandle = null;
                }

                size--;
                if (heapifyMax(cell))
                    bubbleUpMin(cell);
                else
                    bubbleUpMax(cell);
            }

            raiseItemsRemoved(retval, 1);
            raiseCollectionChanged();

            return retval;
        }

        private Handle checkHandle(IPriorityQueueHandle<T> handle, out int cell, out bool isfirst)
        {
            var myhandle = (Handle) handle;
            var toremove = myhandle.index;
            cell = toremove / 2;
            isfirst = toremove % 2 == 0;
            {
                if (toremove == -1 || toremove >= size)
                    throw new InvalidPriorityQueueHandleException("Invalid handle, index out of range");
                var actualhandle = isfirst ? heap[cell].firsthandle : heap[cell].lasthandle;
                if (actualhandle != myhandle)
                    throw new InvalidPriorityQueueHandleException("Invalid handle, doesn't match queue");
            }
            return myhandle;
        }


        /// <summary>
        /// Replace an item with a handle in a priority queue with a new item. 
        /// Typically used for changing the priority of some queued object.
        /// </summary>
        /// <param name="handle">The handle for the old item</param>
        /// <param name="item">The new item</param>
        /// <returns>The old item</returns>
        public T Replace(IPriorityQueueHandle<T> handle, T item)
        {
            stamp++;
            int cell;
            bool isfirst;
            checkHandle(handle, out cell, out isfirst);
            if (size == 0)
                throw new NoSuchItemException();

            T retval;

            if (isfirst)
            {
                retval = heap[cell].first;
                heap[cell].first = item;
                if (size == 1)
                {
                }
                else if (size == 2 * cell + 1) // cell == lastcell
                {
                    var p = (cell + 1) / 2 - 1;
                    if (comparer.Compare(item, heap[p].last) > 0)
                    {
                        var thehandle = heap[cell].firsthandle;
                        updateFirst(cell, heap[p].last, heap[p].lasthandle);
                        updateLast(p, item, thehandle);
                        bubbleUpMax(p);
                    }
                    else
                        bubbleUpMin(cell);
                }
                else if (heapifyMin(cell))
                    bubbleUpMax(cell);
                else
                    bubbleUpMin(cell);
            }
            else
            {
                retval = heap[cell].last;
                heap[cell].last = item;
                if (heapifyMax(cell))
                    bubbleUpMin(cell);
                else
                    bubbleUpMax(cell);
            }

            raiseItemsRemoved(retval, 1);
            raiseItemsAdded(item, 1);
            raiseCollectionChanged();

            return retval;
        }

        /// <summary>
        /// Find the current least item of this priority queue.
        /// </summary>
        /// <param name="handle">On return: the handle of the item.</param>
        /// <returns>The least item.</returns>
        public T FindMin(out IPriorityQueueHandle<T> handle)
        {
            if (size == 0)
                throw new NoSuchItemException();
            handle = heap[0].firsthandle;

            return heap[0].first;
        }

        /// <summary>
        /// Find the current largest item of this priority queue.
        /// </summary>
        /// <param name="handle">On return: the handle of the item.</param>
        /// <returns>The largest item.</returns>
        public T FindMax(out IPriorityQueueHandle<T> handle)
        {
            if (size == 0)
                throw new NoSuchItemException();
            if (size == 1)
            {
                handle = heap[0].firsthandle;
                return heap[0].first;
            }
            handle = heap[0].lasthandle;
            return heap[0].last;
        }

        /// <summary>
        /// Remove the least item from this priority queue.
        /// </summary>
        /// <param name="handle">On return: the handle of the removed item.</param>
        /// <returns>The removed item.</returns>
        public T DeleteMin(out IPriorityQueueHandle<T> handle)
        {
            stamp++;
            if (size == 0)
                throw new NoSuchItemException();

            var retval = heap[0].first;
            var myhandle = heap[0].firsthandle;
            handle = myhandle;
            if (myhandle != null)
                myhandle.index = -1;

            if (size == 1)
            {
                size = 0;
                heap[0].first = default;
                heap[0].firsthandle = null;
            }
            else
            {
                var lastcell = (size - 1) / 2;

                if (size % 2 == 0)
                {
                    updateFirst(0, heap[lastcell].last, heap[lastcell].lasthandle);
                    heap[lastcell].last = default;
                    heap[lastcell].lasthandle = null;
                }
                else
                {
                    updateFirst(0, heap[lastcell].first, heap[lastcell].firsthandle);
                    heap[lastcell].first = default;
                    heap[lastcell].firsthandle = null;
                }

                size--;
                heapifyMin(0);
            }

            raiseItemsRemoved(retval, 1);
            raiseCollectionChanged();
            return retval;
        }

        /// <summary>
        /// Remove the largest item from this priority queue.
        /// </summary>
        /// <param name="handle">On return: the handle of the removed item.</param>
        /// <returns>The removed item.</returns>
        public T DeleteMax(out IPriorityQueueHandle<T> handle)
        {
            stamp++;
            if (size == 0)
                throw new NoSuchItemException();

            T retval;
            Handle myhandle;

            if (size == 1)
            {
                size = 0;
                retval = heap[0].first;
                myhandle = heap[0].firsthandle;
                if (myhandle != null)
                    myhandle.index = -1;
                heap[0].first = default;
                heap[0].firsthandle = null;
            }
            else
            {
                retval = heap[0].last;
                myhandle = heap[0].lasthandle;
                if (myhandle != null)
                    myhandle.index = -1;

                var lastcell = (size - 1) / 2;

                if (size % 2 == 0)
                {
                    updateLast(0, heap[lastcell].last, heap[lastcell].lasthandle);
                    heap[lastcell].last = default;
                    heap[lastcell].lasthandle = null;
                }
                else
                {
                    updateLast(0, heap[lastcell].first, heap[lastcell].firsthandle);
                    heap[lastcell].first = default;
                    heap[lastcell].firsthandle = null;
                }

                size--;
                heapifyMax(0);
            }
            raiseItemsRemoved(retval, 1);
            raiseCollectionChanged();
            handle = myhandle;
            return retval;
        }

        #endregion
    }
}