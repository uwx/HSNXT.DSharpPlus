﻿//////////////////////////////////////////////////////////////////////
// BCLExtensions is (c) 2010 Solutions Design. All rights reserved.
// http://www.sd.nl
//////////////////////////////////////////////////////////////////////
// COPYRIGHTS:
// Copyright (c) 2010 Solutions Design. All rights reserved.
// 
// The BCLExtensions library sourcecode and its accompanying tools, tests and support code
// are released under the following license: (BSD2)
// ----------------------------------------------------------------------
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met: 
//
// 1) Redistributions of source code must retain the above copyright notice, this list of 
//    conditions and the following disclaimer. 
// 2) Redistributions in binary form must reproduce the above copyright notice, this list of 
//    conditions and the following disclaimer in the documentation and/or other materials 
//    provided with the distribution. 
// 
// THIS SOFTWARE IS PROVIDED BY SOLUTIONS DESIGN ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES, 
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL SOLUTIONS DESIGN OR CONTRIBUTORS BE LIABLE FOR 
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
// NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR 
// BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE 
// USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
//
// The views and conclusions contained in the software and documentation are those of the authors 
// and should not be interpreted as representing official policies, either expressed or implied, 
// of Solutions Design. 
//
//////////////////////////////////////////////////////////////////////
// Contributers to the code:
//		- Frans Bouma [FB]
//////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        /// Converts the enumerable to a ReadOnlyCollection.
        /// </summary>
        /// <param name="source">the enumerable to convert</param>
        /// <returns>A ReadOnlyCollection with the elements of the passed in enumerable, or an empty ReadOnlyCollection if source is null or empty</returns>
        public static ReadOnlyCollection<TDestination> ToReadOnlyCollection<TDestination>(this IEnumerable source)
        {
            var sourceAsDestination = new List<TDestination>();
            if (source != null)
            {
                foreach (TDestination toAdd in source)
                {
                    sourceAsDestination.Add(toAdd);
                }
            }
            return new ReadOnlyCollection<TDestination>(sourceAsDestination);
        }

        /// <summary>
        /// Checks whether the enumerable to compare with is equal to the source enumerable, element wise. If elements are in a different order, the
        /// method will still return true. This is different from SequenceEqual which does take order into account
        /// </summary>
        /// <typeparam name="T">type of the element in the sequences to compare</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="toCompareWith">the sequence to compare with.</param>
        /// <returns>true if the source and the sequence to compare with have the same elements, regardless of ordering</returns>
        public static bool SetEqual<T>(this IEnumerable<T> source, IEnumerable<T> toCompareWith)
        {
            if (source == null || toCompareWith == null)
            {
                return false;
            }
            return source.SetEqual(toCompareWith, null);
        }


        /// <summary>
        /// Checks whether the enumerable to compare with is equal to the source enumerable, element wise. If elements are in a different order, the
        /// method will still return true. This is different from SequenceEqual which does take order into account
        /// </summary>
        /// <typeparam name="T">type of the element in the sequences to compare</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="toCompareWith">the sequence to compare with.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>
        /// true if the source and the sequence to compare with have the same elements, regardless of ordering
        /// </returns>
        public static bool SetEqual<T>(this IEnumerable<T> source, IEnumerable<T> toCompareWith,
            IEqualityComparer<T> comparer)
        {
            if (source == null || toCompareWith == null)
            {
                return false;
            }
            var arrSource = source as T[] ?? source.ToArray();
            var countSource = arrSource.Length;
            var arrCompareWith = toCompareWith as T[] ?? toCompareWith.ToArray();
            var countToCompareWith = arrCompareWith.Length;
            if (countSource != countToCompareWith)
            {
                return false;
            }
            if (countSource == 0)
            {
                return true;
            }

            var comparerToUse = comparer ?? EqualityComparer<T>.Default;
            // check whether the intersection of both sequences has the same number of elements
            return arrSource.Intersect(arrCompareWith, comparerToUse).Count() == countSource;
        }
    }
}