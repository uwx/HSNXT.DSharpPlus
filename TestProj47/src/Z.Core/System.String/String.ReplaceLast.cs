// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;
using System.Collections.Generic;
using System.Linq;

public static partial class Extensions
{
    /// <summary>
    ///     A string extension method that replace last occurence.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    /// <returns>The string with the last occurence of old value replace by new value.</returns>
    public static string ReplaceLast(this string @this, string oldValue, string newValue)
    {
        int startindex = @this.LastIndexOf(oldValue);

        if (startindex == -1)
        {
            return @this;
        }

        return @this.Remove(startindex, oldValue.Length).Insert(startindex, newValue);
    }

    /// <summary>
    ///     A string extension method that replace last numbers occurences.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="number">Number of.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    /// <returns>The string with the last numbers occurences of old value replace by new value.</returns>
    public static string ReplaceLast(this string @this, int number, string oldValue, string newValue)
    {
        List<string> list = @this.Split(oldValue).ToList();
        int old = Math.Max(0, list.Count - number - 1);
        IEnumerable<string> listStart = list.Take(old);
        IEnumerable<string> listEnd = list.Skip(old);

        return string.Join(oldValue, listStart) +
               (old > 0 ? oldValue : "") +
               string.Join(newValue, listEnd);
    }
}