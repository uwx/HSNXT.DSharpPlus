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
        ///     Hides the enumerable sequence object identity.
        /// </summary>
        /// <typeparam name="TSource">Source sequence element type.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <returns>Enumerable sequence with the same behavior as the original, but hiding the source object identity.</returns>
        /// <remarks>
        ///     AsEnumerable doesn't hide the object identity, and simply acts as a cast to the IEnumerable&lt;TSource&gt;
        ///     interface.
        /// </remarks>
        public static IEnumerable<TSource> Hide<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Hide_();
        }

        private static IEnumerable<TSource> Hide_<TSource>(this IEnumerable<TSource> source)
        {
            foreach (var item in source)
                yield return item;
        }
    }
}