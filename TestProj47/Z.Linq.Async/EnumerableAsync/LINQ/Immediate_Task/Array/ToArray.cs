﻿// Description: Async extension methods for LINQ (Language Integrated Query).
// Website & Documentation: https://github.com/zzzprojects/LINQ-Async
// Forum: https://github.com/zzzprojects/LINQ-Async/issues
// License: http://www.zzzprojects.com/license-agreement/
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2015 ZZZ Projects. All rights reserved.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using HSNXT.Linq;
namespace HSNXT
{
    public static partial class Extensions
    {
        public static Task<TSource[]> ToArray<TSource>(this Task<TSource[]> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Factory.FromTaskEnumerable(source, Enumerable.ToArray, cancellationToken);
        }
    }
}