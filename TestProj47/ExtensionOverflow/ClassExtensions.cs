//----------------------------------------------------------------------------
//
// ClassExtensions.cs - Extension Methods to be used on Classes.
//
// Copyright (c) 2008 Extension Overflow
//
//----------------------------------------------------------------------------

using System;

namespace HSNXT
{
    /// <summary>
    /// Extension Methods to be used by Classes
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Throws an exception if the object called upon is null.
        /// </summary>
        /// <typeparam name="T">The calling class</typeparam>
        /// <param name="obj">The This object</param>
        /// <param name="text">The text to be written on the ArgumentNullException: [text] not allowed to be null</param>
        public static void ThrowIfArgumentIsNull<T>(this T obj, string text) where T : class
        {
            if (obj == null)
                throw new ArgumentNullException(text + " not allowed to be null");
        }
    }
}