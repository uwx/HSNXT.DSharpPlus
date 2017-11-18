using System;

//using System.Data.SqlServerCe;

// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Concatenates the invocation lists of two delegates.
        /// </summary>
        /// <param name="a">The delegate whose invocation list comes first.</param>
        /// <param name="b">The delegate whose invocation list comes last.</param>
        /// ###
        /// <returns>
        ///     A new delegate with an invocation list that concatenates the invocation lists of  and  in that order. Returns
        ///     if  is null, returns  if  is a null reference, and returns a null reference if both  and  are null references.
        /// </returns>
        public static Delegate Combine(this Delegate a, Delegate b)
        {
            return Delegate.Combine(a, b);
        }

        /// <summary>
        ///     Removes the last occurrence of the invocation list of a delegate from the invocation list of another delegate.
        /// </summary>
        /// <param name="source">The delegate from which to remove the invocation list of .</param>
        /// <param name="value">The delegate that supplies the invocation list to remove from the invocation list of .</param>
        /// ###
        /// <returns>
        ///     A new delegate with an invocation list formed by taking the invocation list of  and removing the last
        ///     occurrence of the invocation list of , if the invocation list of  is found within the invocation list of .
        ///     Returns  if  is null or if the invocation list of  is not found within the invocation list of . Returns a
        ///     null reference if the invocation list of  is equal to the invocation list of  or if  is a null reference.
        /// </returns>
        public static Delegate Remove(this Delegate source, Delegate value)
        {
            return Delegate.Remove(source, value);
        }

        /// <summary>
        ///     Removes all occurrences of the invocation list of a delegate from the invocation list of another delegate.
        /// </summary>
        /// <param name="source">The delegate from which to remove the invocation list of .</param>
        /// <param name="value">The delegate that supplies the invocation list to remove from the invocation list of .</param>
        /// ###
        /// <returns>
        ///     A new delegate with an invocation list formed by taking the invocation list of  and removing all occurrences
        ///     of the invocation list of , if the invocation list of  is found within the invocation list of . Returns  if
        ///     is null or if the invocation list of  is not found within the invocation list of . Returns a null reference
        ///     if the invocation list of  is equal to the invocation list of , if  contains only a series of invocation
        ///     lists that are equal to the invocation list of , or if  is a null reference.
        /// </returns>
        public static Delegate RemoveAll(this Delegate source, Delegate value)
        {
            return Delegate.RemoveAll(source, value);
        }
    }
}