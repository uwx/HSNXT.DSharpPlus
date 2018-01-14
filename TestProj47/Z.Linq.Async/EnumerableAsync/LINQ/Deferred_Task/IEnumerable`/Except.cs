﻿// Description: Async extension methods for LINQ (Language Integrated Query).
// Website & Documentation: https://github.com/zzzprojects/LINQ-Async
// Forum: https://github.com/zzzprojects/LINQ-Async/issues
// License: http://www.zzzprojects.com/license-agreement/
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2015 ZZZ Projects. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using HSNXT.Linq;
namespace HSNXT
{
    public static partial class Extensions
    {
        public static Task<IEnumerable<TSource>> Except<TSource>(this Task<IEnumerable<TSource>> first, IEnumerable<TSource> second, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(first, second, Enumerable.Except, cancellationToken);
        }

        public static Task<IEnumerable<TSource>> Except<TSource>(this Task<IEnumerable<TSource>> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(first, second, comparer, Enumerable.Except, cancellationToken);
        }
    }
}