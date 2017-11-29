﻿// Description: Async extension methods for LINQ (Language Integrated Query).
// Website & Documentation: https://github.com/zzzprojects/LINQ-Async
// Forum: https://github.com/zzzprojects/LINQ-Async/issues
// License: http://www.zzzprojects.com/license-agreement/
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2015 ZZZ Projects. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HSNXT.Linq
{
    public static partial class TaskFactoryExtensions
    {
        public static Task<TResult> FromTaskEnumerable<T, TResult>(this TaskFactory taskFactory, Task<T[]> task, Func<IEnumerable<T>, TResult> func, CancellationToken cancellationToken = default(CancellationToken))
        {
            return FromTaskEnumerable(taskFactory, task, func, AsyncEnumerable<T>.CreateFrom, cancellationToken);
        }

        public static Task<TResult> FromTaskEnumerable<T, TP1, TResult>(this TaskFactory taskFactory, Task<T[]> task, TP1 p1, Func<IEnumerable<T>, TP1, TResult> func, CancellationToken cancellationToken = default(CancellationToken))
        {
            return FromTaskEnumerable(taskFactory, task, enums => func(enums, p1), AsyncEnumerable<T>.CreateFrom, cancellationToken);
        }

        public static Task<TResult> FromTaskEnumerable<T, TP1, TP2, TResult>(this TaskFactory taskFactory, Task<T[]> task, TP1 p1, TP2 p2, Func<IEnumerable<T>, TP1, TP2, TResult> func, CancellationToken cancellationToken = default(CancellationToken))
        {
            return FromTaskEnumerable(taskFactory, task, enums => func(enums, p1, p2), AsyncEnumerable<T>.CreateFrom, cancellationToken);
        }

        public static Task<TResult> FromTaskEnumerable<T, TP1, TP2, TP3, TResult>(this TaskFactory taskFactory, Task<T[]> task, TP1 p1, TP2 p2, TP3 p3, Func<IEnumerable<T>, TP1, TP2, TP3, TResult> func, CancellationToken cancellationToken = default(CancellationToken))
        {
            return FromTaskEnumerable(taskFactory, task, enums => func(enums, p1, p2, p3), AsyncEnumerable<T>.CreateFrom, cancellationToken);
        }

        public static Task<TResult> FromTaskEnumerable<T, TP1, TP2, TP3, TP4, TResult>(this TaskFactory taskFactory, Task<T[]> task, TP1 p1, TP2 p2, TP3 p3, TP4 p4, Func<IEnumerable<T>, TP1, TP2, TP3, TP4, TResult> func, CancellationToken cancellationToken = default(CancellationToken))
        {
            return FromTaskEnumerable(taskFactory, task, enums => func(enums, p1, p2, p3, p4), AsyncEnumerable<T>.CreateFrom, cancellationToken);
        }

        public static Task<TResult> FromTaskEnumerable<T, TP1, TP2, TP3, TP4, TP5, TResult>(this TaskFactory taskFactory, Task<T[]> task, TP1 p1, TP2 p2, TP3 p3, TP4 p4, TP5 p5, Func<IEnumerable<T>, TP1, TP2, TP3, TP4, TP5, TResult> func, CancellationToken cancellationToken = default(CancellationToken))
        {
            return FromTaskEnumerable(taskFactory, task, enums => func(enums, p1, p2, p3, p4, p5), AsyncEnumerable<T>.CreateFrom, cancellationToken);
        }
    }
}