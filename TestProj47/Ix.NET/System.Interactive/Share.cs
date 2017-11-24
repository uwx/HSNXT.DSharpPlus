﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information. 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.Linq
{
    public static partial class EnumerableEx
    {
        /// <summary>
        ///     Creates a buffer with a shared view over the source sequence, causing each enumerator to fetch the next element
        ///     from the source sequence.
        /// </summary>
        /// <typeparam name="TSource">Source sequence element type.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <returns>Buffer enabling each enumerator to retrieve elements from the shared source sequence.</returns>
        /// <example>
        ///     var rng = Enumerable.Range(0, 10).Share();
        ///     var e1 = rng.GetEnumerator();    // Both e1 and e2 will consume elements from
        ///     var e2 = rng.GetEnumerator();    // the source sequence.
        ///     Assert.IsTrue(e1.MoveNext());
        ///     Assert.AreEqual(0, e1.Current);
        ///     Assert.IsTrue(e1.MoveNext());
        ///     Assert.AreEqual(1, e1.Current);
        ///     Assert.IsTrue(e2.MoveNext());    // e2 "steals" element 2
        ///     Assert.AreEqual(2, e2.Current);
        ///     Assert.IsTrue(e1.MoveNext());    // e1 can't see element 2
        ///     Assert.AreEqual(3, e1.Current);
        /// </example>
        public static IBuffer<TSource> Share<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return new SharedBuffer<TSource>(source.GetEnumerator());
        }

        /// <summary>
        ///     Shares the source sequence within a selector function where each enumerator can fetch the next element from the
        ///     source sequence.
        /// </summary>
        /// <typeparam name="TSource">Source sequence element type.</typeparam>
        /// <typeparam name="TResult">Result sequence element type.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="selector">Selector function with shared access to the source sequence for each enumerator.</param>
        /// <returns>Sequence resulting from applying the selector function to the shared view over the source sequence.</returns>
        public static IEnumerable<TResult> Share<TSource, TResult>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return Create(() => selector(source.Share())
                              .GetEnumerator());
        }

        private class SharedBuffer<T> : IBuffer<T>
        {
            private bool _disposed;
            private IEnumerator<T> _source;

            public SharedBuffer(IEnumerator<T> source)
            {
                _source = source;
            }

            public IEnumerator<T> GetEnumerator()
            {
                if (_disposed)
                    throw new ObjectDisposedException("");

                return GetEnumerator_();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                if (_disposed)
                    throw new ObjectDisposedException("");

                return GetEnumerator();
            }

            public void Dispose()
            {
                lock (_source)
                {
                    if (!_disposed)
                    {
                        _source.Dispose();
                        _source = null;
                    }

                    _disposed = true;
                }
            }

            private IEnumerator<T> GetEnumerator_()
            {
                while (true)
                {
                    if (_disposed)
                        throw new ObjectDisposedException("");

                    var hasValue = default(bool);
                    var current = default(T);

                    lock (_source)
                    {
                        hasValue = _source.MoveNext();
                        if (hasValue)
                            current = _source.Current;
                    }

                    if (hasValue)
                        yield return current;
                    else
                        break;
                }
            }
        }
    }
}