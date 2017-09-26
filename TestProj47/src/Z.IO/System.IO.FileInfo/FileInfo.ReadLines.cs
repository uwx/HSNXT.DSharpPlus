// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public static partial class Extensions
{
    /// <summary>
    ///     Reads the lines of a file.
    /// </summary>
    /// <param name="this">The file to read.</param>
    /// <returns>All the lines of the file, or the lines that are the result of a query.</returns>
    /// ###
    /// <exception cref="T:System.ArgumentException">
    ///     <paramref name="this" /> is a zero-length string, contains only
    ///     white space, or contains one or more invalid characters defined by the
    ///     <see
    ///         cref="M:System.IO.Path.GetInvalidPathChars" />
    ///     method.
    /// </exception>
    /// ###
    /// <exception cref="T:System.ArgumentNullException">
    ///     <paramref name="this" /> is null.
    /// </exception>
    /// ###
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///     <paramref name="this" /> is invalid (for example, it
    ///     is on an unmapped drive).
    /// </exception>
    /// ###
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///     The file specified by <paramref name="this" /> was not
    ///     found.
    /// </exception>
    /// ###
    /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
    /// ###
    /// <exception cref="T:System.IO.PathTooLongException">
    ///     <paramref name="this" /> exceeds the system-defined
    ///     maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names
    ///     must be less than 260 characters.
    /// </exception>
    /// ###
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
    /// ###
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///     <paramref name="this" /> specifies a file that is
    ///     read-only.-or-This operation is not supported on the current platform.-or-
    ///     <paramref
    ///         name="this" />
    ///     is a directory.-or-The caller does not have the required permission.
    /// </exception>
    public static IEnumerable<String> ReadLines(this FileInfo @this)
    {
        return File.ReadLines(@this.FullName);
    }

    /// <summary>
    ///     Read the lines of a file that has a specified encoding.
    /// </summary>
    /// <param name="this">The file to read.</param>
    /// <param name="encoding">The encoding that is applied to the contents of the file.</param>
    /// <returns>All the lines of the file, or the lines that are the result of a query.</returns>
    /// ###
    /// <exception cref="T:System.ArgumentException">
    ///     <paramref name="this" /> is a zero-length string, contains only
    ///     white space, or contains one or more invalid characters as defined by the
    ///     <see
    ///         cref="M:System.IO.Path.GetInvalidPathChars" />
    ///     method.
    /// </exception>
    /// ###
    /// <exception cref="T:System.ArgumentNullException">
    ///     <paramref name="this" /> is null.
    /// </exception>
    /// ###
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///     <paramref name="this" /> is invalid (for example, it
    ///     is on an unmapped drive).
    /// </exception>
    /// ###
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///     The file specified by <paramref name="this" /> was not
    ///     found.
    /// </exception>
    /// ###
    /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
    /// ###
    /// <exception cref="T:System.IO.PathTooLongException">
    ///     <paramref name="this" /> exceeds the system-defined
    ///     maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names
    ///     must be less than 260 characters.
    /// </exception>
    /// ###
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
    /// ###
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///     <paramref name="this" /> specifies a file that is
    ///     read-only.-or-This operation is not supported on the current platform.-or-
    ///     <paramref
    ///         name="this" />
    ///     is a directory.-or-The caller does not have the required permission.
    /// </exception>
    public static IEnumerable<String> ReadLines(this FileInfo @this, Encoding encoding)
    {
        return File.ReadLines(@this.FullName, encoding);
    }
}