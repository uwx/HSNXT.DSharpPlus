// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;
using System.Data;

public static partial class Extensions
{
    /// <summary>
    ///     An IDataReader extension method that gets value to or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="index">Zero-based index of the.</param>
    /// <returns>The value to or default.</returns>
    public static T GetValueToOrDefault<T>(this IDataReader @this, int index)
    {
        try
        {
            return @this.GetValue(index).To<T>();
        }
        catch
        {
            return default(T);
        }
    }

    /// <summary>
    ///     An IDataReader extension method that gets value to or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="index">Zero-based index of the.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The value to or default.</returns>
    public static T GetValueToOrDefault<T>(this IDataReader @this, int index, T defaultValue)
    {
        try
        {
            return @this.GetValue(index).To<T>();
        }
        catch
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An IDataReader extension method that gets value to or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="index">Zero-based index of the.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The value to or default.</returns>
    public static T GetValueToOrDefault<T>(this IDataReader @this, int index, Func<IDataReader, int, T> defaultValueFactory)
    {
        try
        {
            return @this.GetValue(index).To<T>();
        }
        catch
        {
            return defaultValueFactory(@this, index);
        }
    }

    /// <summary>
    ///     An IDataReader extension method that gets value to or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="columnName">Name of the column.</param>
    /// <returns>The value to or default.</returns>
    public static T GetValueToOrDefault<T>(this IDataReader @this, string columnName)
    {
        try
        {
            return @this.GetValue(@this.GetOrdinal(columnName)).To<T>();
        }
        catch
        {
            return default(T);
        }
    }

    /// <summary>
    ///     An IDataReader extension method that gets value to or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="columnName">Name of the column.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The value to or default.</returns>
    public static T GetValueToOrDefault<T>(this IDataReader @this, string columnName, T defaultValue)
    {
        try
        {
            return @this.GetValue(@this.GetOrdinal(columnName)).To<T>();
        }
        catch
        {
            return defaultValue;
        }
    }

    /// <summary>
    ///     An IDataReader extension method that gets value to or default.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="columnName">Name of the column.</param>
    /// <param name="defaultValueFactory">The default value factory.</param>
    /// <returns>The value to or default.</returns>
    public static T GetValueToOrDefault<T>(this IDataReader @this, string columnName, Func<IDataReader, string, T> defaultValueFactory)
    {
        try
        {
            return @this.GetValue(@this.GetOrdinal(columnName)).To<T>();
        }
        catch
        {
            return defaultValueFactory(@this, columnName);
        }
    }
}