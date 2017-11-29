﻿// Description: Async extension methods for LINQ (Language Integrated Query).
// Website & Documentation: https://github.com/zzzprojects/LINQ-Async
// Forum: https://github.com/zzzprojects/LINQ-Async/issues
// License: http://www.zzzprojects.com/license-agreement/
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2015 ZZZ Projects. All rights reserved.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using HSNXT.Linq;
namespace HSNXT
{
    public static partial class Extensions
    {
        public static Task<int> Max(this Task<IOrderedEnumerable<int>> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, Enumerable.Max, cancellationToken);
        }

        public static Task<int?> Max(this Task<IOrderedEnumerable<int?>> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, Enumerable.Max, cancellationToken);
        }

        public static Task<long> Max(this Task<IOrderedEnumerable<long>> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, Enumerable.Max, cancellationToken);
        }

        public static Task<long?> Max(this Task<IOrderedEnumerable<long?>> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, Enumerable.Max, cancellationToken);
        }

        public static Task<double> Max(this Task<IOrderedEnumerable<double>> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, Enumerable.Max, cancellationToken);
        }

        public static Task<double?> Max(this Task<IOrderedEnumerable<double?>> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, Enumerable.Max, cancellationToken);
        }

        public static Task<float> Max(this Task<IOrderedEnumerable<float>> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, Enumerable.Max, cancellationToken);
        }

        public static Task<float?> Max(this Task<IOrderedEnumerable<float?>> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, Enumerable.Max, cancellationToken);
        }

        public static Task<decimal> Max(this Task<IOrderedEnumerable<decimal>> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, Enumerable.Max, cancellationToken);
        }

        public static Task<decimal?> Max(this Task<IOrderedEnumerable<decimal?>> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, Enumerable.Max, cancellationToken);
        }

        public static Task<TSource> Max<TSource>(this Task<IOrderedEnumerable<TSource>> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, Enumerable.Max, cancellationToken);
        }

        public static Task<int> Max<TSource>(this Task<IOrderedEnumerable<TSource>> source, Func<TSource, int> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, selector, Enumerable.Max, cancellationToken);
        }

        public static Task<int?> Max<TSource>(this Task<IOrderedEnumerable<TSource>> source, Func<TSource, int?> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, selector, Enumerable.Max, cancellationToken);
        }

        public static Task<long> Max<TSource>(this Task<IOrderedEnumerable<TSource>> source, Func<TSource, long> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, selector, Enumerable.Max, cancellationToken);
        }

        public static Task<long?> Max<TSource>(this Task<IOrderedEnumerable<TSource>> source, Func<TSource, long?> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, selector, Enumerable.Max, cancellationToken);
        }

        public static Task<float> Max<TSource>(this Task<IOrderedEnumerable<TSource>> source, Func<TSource, float> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, selector, Enumerable.Max, cancellationToken);
        }

        public static Task<float?> Max<TSource>(this Task<IOrderedEnumerable<TSource>> source, Func<TSource, float?> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, selector, Enumerable.Max, cancellationToken);
        }

        public static Task<double> Max<TSource>(this Task<IOrderedEnumerable<TSource>> source, Func<TSource, double> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, selector, Enumerable.Max, cancellationToken);
        }

        public static Task<double?> Max<TSource>(this Task<IOrderedEnumerable<TSource>> source, Func<TSource, double?> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, selector, Enumerable.Max, cancellationToken);
        }

        public static Task<decimal> Max<TSource>(this Task<IOrderedEnumerable<TSource>> source, Func<TSource, decimal> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, selector, Enumerable.Max, cancellationToken);
        }

        public static Task<decimal?> Max<TSource>(this Task<IOrderedEnumerable<TSource>> source, Func<TSource, decimal?> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, selector, Enumerable.Max, cancellationToken);
        }

        public static Task<TResult> Max<TSource, TResult>(this Task<IOrderedEnumerable<TSource>> source, Func<TSource, TResult> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, selector, Enumerable.Max, cancellationToken);
        }
    }
}