﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information. 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Linq;
using HSNXT.Linq;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Creates an enumerable sequence based on an enumerator factory function.
        /// </summary>
        /// <typeparam name="TResult">Result sequence element type.</typeparam>
        /// <param name="getEnumerator">Enumerator factory function.</param>
        /// <returns>Sequence that will invoke the enumerator factory upon a call to GetEnumerator.</returns>
        public static IEnumerable<TResult> Create<TResult>(Func<IEnumerator<TResult>> getEnumerator)
        {
            if (getEnumerator == null)
                throw new ArgumentNullException(nameof(getEnumerator));

            return new AnonymousEnumerable<TResult>(getEnumerator);
        }

        /// <summary>
        ///     Creates an enumerable sequence based on an asynchronous method that provides a yielder.
        /// </summary>
        /// <typeparam name="T">Result sequence element type.</typeparam>
        /// <param name="create">
        ///     Delegate implementing an asynchronous method that can use the specified yielder to yield return
        ///     values.
        /// </param>
        /// <returns>Sequence that will use the asynchronous method to obtain its elements.</returns>
        public static IEnumerable<T> Create<T>(Action<IYielder<T>> create)
        {
            if (create == null)
                throw new ArgumentNullException(nameof(create));

            foreach (var x in new Yielder<T>(create))
                yield return x;
        }

        private class AnonymousEnumerable<TResult> : IEnumerable<TResult>
        {
            private readonly Func<IEnumerator<TResult>> _getEnumerator;

            public AnonymousEnumerable(Func<IEnumerator<TResult>> getEnumerator)
            {
                _getEnumerator = getEnumerator;
            }

            public IEnumerator<TResult> GetEnumerator()
            {
                return _getEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}