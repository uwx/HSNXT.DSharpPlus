// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

public static partial class Extensions
{
    /// <summary>
    ///     A string extension method that deserialize a Json string to object.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <returns>The object deserialized.</returns>
    public static T DeserializeJson<T>(this string @this)
    {
        var serializer = new DataContractJsonSerializer(typeof (T));

        using (var stream = new MemoryStream(Encoding.Default.GetBytes(@this)))
        {
            return (T) serializer.ReadObject(stream);
        }
    }

    /// <summary>
    ///     A string extension method that deserialize a Json string to object.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns>The object deserialized.</returns>
    public static T DeserializeJson<T>(this string @this, Encoding encoding)
    {
        var serializer = new DataContractJsonSerializer(typeof (T));

        using (var stream = new MemoryStream(encoding.GetBytes(@this)))
        {
            return (T) serializer.ReadObject(stream);
        }
    }
}