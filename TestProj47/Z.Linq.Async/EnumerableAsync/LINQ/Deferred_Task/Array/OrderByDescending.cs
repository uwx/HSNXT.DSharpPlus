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
        public static Task<IOrderedEnumerable<TSource>> OrderByDescending<TSource, TKey>(this Task<TSource[]> source, Func<TSource, TKey> keySelector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, keySelector, Enumerable.OrderByDescending, cancellationToken);
        }

        public static Task<IOrderedEnumerable<TSource>> OrderByDescending<TSource, TKey>(this Task<TSource[]> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, keySelector, comparer, Enumerable.OrderByDescending, cancellationToken);
        }
    }
}