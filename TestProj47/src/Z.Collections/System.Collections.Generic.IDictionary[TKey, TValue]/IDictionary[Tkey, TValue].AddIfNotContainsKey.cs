// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;
using System.Collections.Generic;

public static partial class Extensions
{
    /// <summary>
    ///     An IDictionary&lt;TKey,TValue&gt; extension method that adds if not contains key.
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool AddIfNotContainsKey<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue value)
    {
        if (!@this.ContainsKey(key))
        {
            @this.Add(key, value);
            return true;
        }

        return false;
    }

    /// <summary>
    ///     An IDictionary&lt;TKey,TValue&gt; extension method that adds if not contains key.
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="key">The key.</param>
    /// <param name="valueFactory">The value factory.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool AddIfNotContainsKey<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TValue> valueFactory)
    {
        if (!@this.ContainsKey(key))
        {
            @this.Add(key, valueFactory());
            return true;
        }

        return false;
    }

    /// <summary>
    ///     An IDictionary&lt;TKey,TValue&gt; extension method that adds if not contains key.
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="key">The key.</param>
    /// <param name="valueFactory">The value factory.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool AddIfNotContainsKey<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TKey, TValue> valueFactory)
    {
        if (!@this.ContainsKey(key))
        {
            @this.Add(key, valueFactory(key));
            return true;
        }

        return false;
    }
}