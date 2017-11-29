﻿// Description: Async extension methods for LINQ (Language Integrated Query).
// Website & Documentation: https://github.com/zzzprojects/LINQ-Async
// Forum: https://github.com/zzzprojects/LINQ-Async/issues
// License: http://www.zzzprojects.com/license-agreement/
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2015 ZZZ Projects. All rights reserved.

using System.Threading;
using System.Threading.Tasks;
using HSNXT.Linq.Async;

using HSNXT.Linq;
namespace HSNXT
{
    public static partial class Extensions
    {
        public static Task<AsyncWhereEnumerable<TSource>> OrderByPredicateCompletion<TSource>(this Task<AsyncWhereEnumerable<TSource>> source, bool value = true, CancellationToken cancellationToken = default(CancellationToken))
        {
            var sourceState = (AsyncWhereEnumerable<TSource>) source.AsyncState;
            sourceState.OrderByPredicateCompletion = value;
            return source;
        }
    }
}