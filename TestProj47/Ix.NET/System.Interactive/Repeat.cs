﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.Linq
{
    public static partial class EnumerableEx
    {
        /// <summary>
        ///     Generates a sequence by repeating the given value infinitely.
        /// </summary>
        /// <typeparam name="TResult">Result sequence element type.</typeparam>
        /// <param name="value">Value to repreat in the resulting sequence.</param>
        /// <returns>Sequence repeating the given value infinitely.</returns>
        public static IEnumerable<TResult> Repeat<TResult>(TResult value)
        {
            while (true)
                yield return value;
        }

        /// <summary>
        ///     Generates a sequence that contains one repeated value.
        /// </summary>
        /// <typeparam name="TResult">Result sequence element type.</typeparam>
        /// <param name="element">The value to be repeated.</param>
        /// <param name="count">The number of times to repeat the value in the generated sequence.</param>
        /// <returns>Sequence that contains a repeated value.</returns>
        public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
        {
            return Enumerable.Repeat(element, count);
        }

        /// <summary>
        ///     Repeats and concatenates the source sequence infinitely.
        /// </summary>
        /// <typeparam name="TSource">Source sequence element type.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <returns>Sequence obtained by concatenating the source sequence to itself infinitely.</returns>
        public static IEnumerable<TSource> Repeat<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Repeat_(source);
        }

        /// <summary>
        ///     Repeats and concatenates the source sequence the given number of times.
        /// </summary>
        /// <typeparam name="TSource">Source sequence element type.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="count">Number of times to repeat the source sequence.</param>
        /// <returns>Sequence obtained by concatenating the source sequence to itself the specified number of times.</returns>
        public static IEnumerable<TSource> Repeat<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            return Repeat_(source, count);
        }

        private static IEnumerable<TSource> Repeat_<TSource>(IEnumerable<TSource> source)
        {
            while (true)
                foreach (var item in source)
                    yield return item;
        }

        private static IEnumerable<TSource> Repeat_<TSource>(IEnumerable<TSource> source, int count)
        {
            for (var i = 0; i < count; i++)
                foreach (var item in source)
                    yield return item;
        }
    }
}