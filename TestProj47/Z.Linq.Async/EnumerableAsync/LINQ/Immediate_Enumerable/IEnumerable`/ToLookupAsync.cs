﻿// Description: Async extension methods for LINQ (Language Integrated Query).
// Website & Documentation: https://github.com/zzzprojects/LINQ-Async
// Forum: https://github.com/zzzprojects/LINQ-Async/issues
// License: http://www.zzzprojects.com/license-agreement/
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2015 ZZZ Projects. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using HSNXT.Linq;
namespace HSNXT
{
    public static partial class Extensions
    {
        public static Task<ILookup<TKey, TSource>> ToLookupAsync<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, keySelector, Enumerable.ToLookup, cancellationToken);
        }

        public static Task<ILookup<TKey, TSource>> ToLookupAsync<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, keySelector, comparer, Enumerable.ToLookup, cancellationToken);
        }

        public static Task<ILookup<TKey, TElement>> ToLookupAsync<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, keySelector, elementSelector, Enumerable.ToLookup, cancellationToken);
        }

        public static Task<ILookup<TKey, TElement>> ToLookupAsync<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, keySelector, elementSelector, comparer, Enumerable.ToLookup, cancellationToken);
        }
    }
}