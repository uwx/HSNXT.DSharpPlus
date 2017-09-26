// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;
using System.Reflection;

public static partial class Extensions
{
    /// <summary>An object extension method that gets custom attributes.</summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>An array of object.</returns>
    public static Attribute[] GetCustomAttributes(this object @this)
    {
        var type = @this.GetType();

        return type.IsEnum ?
            type.GetField(@this.ToString()).GetCustomAttributes() :
            type.GetCustomAttributes();
    }

    /// <summary>
    ///     An object extension method that gets custom attributes.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="inherit">true to inherit.</param>
    /// <returns>An array of object.</returns>
    public static object[] GetCustomAttributes(this object @this, bool inherit)
    {
        var type = @this.GetType();

        return type.IsEnum ?
            type.GetField(@this.ToString()).GetCustomAttributes(inherit) :
            type.GetCustomAttributes(inherit);
    }

    /// <summary>An object extension method that gets custom attributes.</summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <returns>An array of object.</returns>
    public static T[] GetCustomAttributes<T>(this object @this) where T : Attribute
    {
        var type = @this.GetType();

        return (T[]) (type.IsEnum ?
            type.GetField(@this.ToString()).GetCustomAttributes(typeof (T)) :
            type.GetCustomAttributes(typeof (T)));
    }

    /// <summary>
    ///     An object extension method that gets custom attributes.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="inherit">true to inherit.</param>
    /// <returns>An array of object.</returns>
    public static T[] GetCustomAttributes<T>(this object @this, bool inherit) where T : Attribute
    {
        var type = @this.GetType();

        return (T[]) (type.IsEnum ?
            type.GetField(@this.ToString()).GetCustomAttributes(typeof (T), inherit) :
            type.GetCustomAttributes(typeof (T), inherit));
    }

    /// <summary>An object extension method that gets custom attributes.</summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <returns>An array of object.</returns>
    public static T[] GetCustomAttributes<T>(this MemberInfo @this) where T : Attribute
    {
        return (T[]) @this.GetCustomAttributes(typeof (T));
    }

    /// <summary>An object extension method that gets custom attributes.</summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="inherit">true to inherit.</param>
    /// <returns>An array of object.</returns>
    public static T[] GetCustomAttributes<T>(this MemberInfo @this, bool inherit) where T : Attribute
    {
        return (T[]) @this.GetCustomAttributes(typeof (T), inherit);
    }
}