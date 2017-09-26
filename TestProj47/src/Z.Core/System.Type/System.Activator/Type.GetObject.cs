// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;

public static partial class Extensions
{
    /// <summary>
    ///     Creates a proxy for the well-known object indicated by the specified type and URL.
    /// </summary>
    /// <param name="type">The type of the well-known object to which you want to connect.</param>
    /// <param name="url">The URL of the well-known object.</param>
    /// <returns>A proxy that points to an endpoint served by the requested well-known object.</returns>
    public static Object GetObject(this Type type, String url)
    {
        return Activator.GetObject(type, url);
    }

    /// <summary>
    ///     Creates a proxy for the well-known object indicated by the specified type, URL, and channel data.
    /// </summary>
    /// <param name="type">The type of the well-known object to which you want to connect.</param>
    /// <param name="url">The URL of the well-known object.</param>
    /// <param name="state">Channel-specific data or null.</param>
    /// <returns>A proxy that points to an endpoint served by the requested well-known object.</returns>
    public static Object GetObject(this Type type, String url, Object state)
    {
        return Activator.GetObject(type, url, state);
    }
}