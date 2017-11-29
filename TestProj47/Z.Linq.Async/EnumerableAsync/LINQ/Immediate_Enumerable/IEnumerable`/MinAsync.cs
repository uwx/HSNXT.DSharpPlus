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
        public static Task<int> MinAsync(this IEnumerable<int> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, Enumerable.Min, cancellationToken);
        }

        public static Task<int?> MinAsync(this IEnumerable<int?> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, Enumerable.Min, cancellationToken);
        }

        public static Task<long> MinAsync(this IEnumerable<long> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, Enumerable.Min, cancellationToken);
        }

        public static Task<long?> MinAsync(this IEnumerable<long?> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, Enumerable.Min, cancellationToken);
        }

        public static Task<float> MinAsync(this IEnumerable<float> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, Enumerable.Min, cancellationToken);
        }

        public static Task<float?> MinAsync(this IEnumerable<float?> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, Enumerable.Min, cancellationToken);
        }

        public static Task<double> MinAsync(this IEnumerable<double> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, Enumerable.Min, cancellationToken);
        }

        public static Task<double?> MinAsync(this IEnumerable<double?> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, Enumerable.Min, cancellationToken);
        }

        public static Task<decimal> MinAsync(this IEnumerable<decimal> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, Enumerable.Min, cancellationToken);
        }

        public static Task<decimal?> MinAsync(this IEnumerable<decimal?> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, Enumerable.Min, cancellationToken);
        }

        public static Task<TSource> MinAsync<TSource>(this IEnumerable<TSource> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, Enumerable.Min, cancellationToken);
        }

        public static Task<int> MinAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, selector, Enumerable.Min, cancellationToken);
        }

        public static Task<int?> MinAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, selector, Enumerable.Min, cancellationToken);
        }

        public static Task<long> MinAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, selector, Enumerable.Min, cancellationToken);
        }

        public static Task<long?> MinAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, selector, Enumerable.Min, cancellationToken);
        }

        public static Task<float> MinAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, selector, Enumerable.Min, cancellationToken);
        }

        public static Task<float?> MinAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, selector, Enumerable.Min, cancellationToken);
        }

        public static Task<double> MinAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, selector, Enumerable.Min, cancellationToken);
        }

        public static Task<double?> MinAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, selector, Enumerable.Min, cancellationToken);
        }

        public static Task<decimal> MinAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, selector, Enumerable.Min, cancellationToken);
        }

        public static Task<decimal?> MinAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, selector, Enumerable.Min, cancellationToken);
        }

        public static Task<TResult> MinAsync<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromEnumerable(source, selector, Enumerable.Min, cancellationToken);
        }
    }
}