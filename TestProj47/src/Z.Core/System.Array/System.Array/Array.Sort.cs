// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;
using System.Collections;

public static partial class Extensions
{
    /// <summary>
    ///     Sorts the elements in an entire one-dimensional  using the  implementation of each element of the .
    /// </summary>
    /// <param name="array">The one-dimensional  to sort.</param>
    public static void Sort(this Array array)
    {
        Array.Sort(array);
    }

    /// <summary>
    ///     Sorts a pair of one-dimensional  objects (one contains the keys and the other contains the corresponding
    ///     items) based on the keys in the first  using the  implementation of each key.
    /// </summary>
    /// <param name="array">The one-dimensional  to sort.</param>
    /// <param name="items">
    ///     The one-dimensional  that contains the items that correspond to each of the keys in the .-or-
    ///     null to sort only the .
    /// </param>
    /// ###
    /// <param name="keys">The one-dimensional  that contains the keys to sort.</param>
    public static void Sort(this Array array, Array items)
    {
        Array.Sort(array, items);
    }

    /// <summary>
    ///     Sorts the elements in a range of elements in a one-dimensional  using the  implementation of each element of
    ///     the .
    /// </summary>
    /// <param name="array">The one-dimensional  to sort.</param>
    /// <param name="index">The starting index of the range to sort.</param>
    /// <param name="length">The number of elements in the range to sort.</param>
    public static void Sort(this Array array, Int32 index, Int32 length)
    {
        Array.Sort(array, index, length);
    }

    /// <summary>
    ///     Sorts a range of elements in a pair of one-dimensional  objects (one contains the keys and the other contains
    ///     the corresponding items) based on the keys in the first  using the  implementation of each key.
    /// </summary>
    /// <param name="array">The one-dimensional  to sort.</param>
    /// <param name="items">
    ///     The one-dimensional  that contains the items that correspond to each of the keys in the .-or-
    ///     null to sort only the .
    /// </param>
    /// <param name="index">The starting index of the range to sort.</param>
    /// <param name="length">The number of elements in the range to sort.</param>
    /// ###
    /// <param name="keys">The one-dimensional  that contains the keys to sort.</param>
    public static void Sort(this Array array, Array items, Int32 index, Int32 length)
    {
        Array.Sort(array, items, index, length);
    }

    /// <summary>
    ///     Sorts the elements in a one-dimensional  using the specified .
    /// </summary>
    /// <param name="array">The one-dimensional  to sort.</param>
    /// <param name="comparer">
    ///     The  implementation to use when comparing elements.-or-null to use the  implementation of
    ///     each element.
    /// </param>
    public static void Sort(this Array array, IComparer comparer)
    {
        Array.Sort(array, comparer);
    }

    /// <summary>
    ///     Sorts a pair of one-dimensional  objects (one contains the keys and the other contains the corresponding
    ///     items) based on the keys in the first  using the specified .
    /// </summary>
    /// <param name="array">The one-dimensional  to sort.</param>
    /// <param name="items">
    ///     The one-dimensional  that contains the items that correspond to each of the keys in the .-or-
    ///     null to sort only the .
    /// </param>
    /// <param name="comparer">
    ///     The  implementation to use when comparing elements.-or-null to use the  implementation of
    ///     each element.
    /// </param>
    /// ###
    /// <param name="keys">The one-dimensional  that contains the keys to sort.</param>
    public static void Sort(this Array array, Array items, IComparer comparer)
    {
        Array.Sort(array, items, comparer);
    }

    /// <summary>
    ///     Sorts the elements in a range of elements in a one-dimensional  using the specified .
    /// </summary>
    /// <param name="array">The one-dimensional  to sort.</param>
    /// <param name="index">The starting index of the range to sort.</param>
    /// <param name="length">The number of elements in the range to sort.</param>
    /// <param name="comparer">
    ///     The  implementation to use when comparing elements.-or-null to use the  implementation of
    ///     each element.
    /// </param>
    public static void Sort(this Array array, Int32 index, Int32 length, IComparer comparer)
    {
        Array.Sort(array, index, length, comparer);
    }

    /// <summary>
    ///     Sorts a range of elements in a pair of one-dimensional  objects (one contains the keys and the other contains
    ///     the corresponding items) based on the keys in the first  using the specified .
    /// </summary>
    /// <param name="array">The one-dimensional  to sort.</param>
    /// <param name="items">
    ///     The one-dimensional  that contains the items that correspond to each of the keys in the .-or-
    ///     null to sort only the .
    /// </param>
    /// <param name="index">The starting index of the range to sort.</param>
    /// <param name="length">The number of elements in the range to sort.</param>
    /// <param name="comparer">
    ///     The  implementation to use when comparing elements.-or-null to use the  implementation of
    ///     each element.
    /// </param>
    /// ###
    /// <param name="keys">The one-dimensional  that contains the keys to sort.</param>
    public static void Sort(this Array array, Array items, Int32 index, Int32 length, IComparer comparer)
    {
        Array.Sort(array, items, index, length, comparer);
    }
}