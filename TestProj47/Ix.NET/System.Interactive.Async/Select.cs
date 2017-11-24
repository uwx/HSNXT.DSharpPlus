﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information. 

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<TResult> Select<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            if (source is AsyncIterator<TSource> iterator)
            {
                return iterator.Select(selector);
            }

            if (source is IList<TSource> ilist)
            {
                return new SelectIListIterator<TSource, TResult>(ilist, selector);
            }

            return new SelectEnumerableAsyncIterator<TSource, TResult>(source, selector);
        }

        public static IAsyncEnumerable<TResult> Select<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return new SelectEnumerableWithIndexAsyncIterator<TSource, TResult>(source, selector);
        }

        private static Func<TSource, TResult> CombineSelectors<TSource, TMiddle, TResult>(Func<TSource, TMiddle> selector1, Func<TMiddle, TResult> selector2)
        {
            return x => selector2(selector1(x));
        }

        internal sealed class SelectEnumerableAsyncIterator<TSource, TResult> : AsyncIterator<TResult>
        {
            private readonly Func<TSource, TResult> selector;
            private readonly IAsyncEnumerable<TSource> source;

            private IAsyncEnumerator<TSource> enumerator;

            public SelectEnumerableAsyncIterator(IAsyncEnumerable<TSource> source, Func<TSource, TResult> selector)
            {
                Debug.Assert(source != null);
                Debug.Assert(selector != null);

                this.source = source;
                this.selector = selector;
            }

            public override AsyncIterator<TResult> Clone()
            {
                return new SelectEnumerableAsyncIterator<TSource, TResult>(source, selector);
            }

            public override void Dispose()
            {
                if (enumerator != null)
                {
                    enumerator.Dispose();
                    enumerator = null;
                }

                base.Dispose();
            }

            public override IAsyncEnumerable<TResult1> Select<TResult1>(Func<TResult, TResult1> selector)
            {
                return new SelectEnumerableAsyncIterator<TSource, TResult1>(source, CombineSelectors(this.selector, selector));
            }

            protected override async Task<bool> MoveNextCore(CancellationToken cancellationToken)
            {
                switch (state)
                {
                    case AsyncIteratorState.Allocated:
                        enumerator = source.GetEnumerator();
                        state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        if (await enumerator.MoveNext(cancellationToken)
                                            .ConfigureAwait(false))
                        {
                            current = selector(enumerator.Current);
                            return true;
                        }

                        Dispose();
                        break;
                }

                return false;
            }
        }

        internal sealed class SelectEnumerableWithIndexAsyncIterator<TSource, TResult> : AsyncIterator<TResult>
        {
            private readonly Func<TSource, int, TResult> selector;
            private readonly IAsyncEnumerable<TSource> source;
            private IAsyncEnumerator<TSource> enumerator;
            private int index;

            public SelectEnumerableWithIndexAsyncIterator(IAsyncEnumerable<TSource> source, Func<TSource, int, TResult> selector)
            {
                Debug.Assert(source != null);
                Debug.Assert(selector != null);

                this.source = source;
                this.selector = selector;
            }

            public override AsyncIterator<TResult> Clone()
            {
                return new SelectEnumerableWithIndexAsyncIterator<TSource, TResult>(source, selector);
            }

            public override void Dispose()
            {
                if (enumerator != null)
                {
                    enumerator.Dispose();
                    enumerator = null;
                }

                base.Dispose();
            }

            protected override async Task<bool> MoveNextCore(CancellationToken cancellationToken)
            {
                switch (state)
                {
                    case AsyncIteratorState.Allocated:
                        enumerator = source.GetEnumerator();
                        index = -1;
                        state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        if (await enumerator.MoveNext(cancellationToken)
                                            .ConfigureAwait(false))
                        {
                            checked
                            {
                                index++;
                            }
                            current = selector(enumerator.Current, index);
                            return true;
                        }

                        Dispose();
                        break;
                }

                return false;
            }
        }

        internal sealed class SelectIListIterator<TSource, TResult> : AsyncIterator<TResult>
        {
            private readonly Func<TSource, TResult> selector;
            private readonly IList<TSource> source;
            private IEnumerator<TSource> enumerator;

            public SelectIListIterator(IList<TSource> source, Func<TSource, TResult> selector)
            {
                Debug.Assert(source != null);
                Debug.Assert(selector != null);

                this.source = source;
                this.selector = selector;
            }

            public override AsyncIterator<TResult> Clone()
            {
                return new SelectIListIterator<TSource, TResult>(source, selector);
            }

            public override void Dispose()
            {
                if (enumerator != null)
                {
                    enumerator.Dispose();
                    enumerator = null;
                }

                base.Dispose();
            }

            public override IAsyncEnumerable<TResult1> Select<TResult1>(Func<TResult, TResult1> selector)
            {
                return new SelectIListIterator<TSource, TResult1>(source, CombineSelectors(this.selector, selector));
            }

            protected override Task<bool> MoveNextCore(CancellationToken cancellationToken)
            {
                switch (state)
                {
                    case AsyncIteratorState.Allocated:
                        enumerator = source.GetEnumerator();
                        state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        if (enumerator.MoveNext())
                        {
                            current = selector(enumerator.Current);
                            return Task.FromResult(true);
                        }

                        Dispose();
                        break;
                }

                return Task.FromResult(false);
            }
        }
    }
}