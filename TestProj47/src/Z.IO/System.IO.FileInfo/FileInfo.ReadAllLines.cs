// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;
using System.IO;
using System.Text;

public static partial class Extensions
{
    /// <summary>
    ///     Opens a text file, reads all lines of the file, and then closes the file.
    /// </summary>
    /// <param name="this">The file to open for reading.</param>
    /// <returns>A string array containing all lines of the file.</returns>
    /// ###
    /// <exception cref="T:System.ArgumentException">
    ///     <paramref name="this" /> is a zero-length string, contains only
    ///     white space, or contains one or more invalid characters as defined by
    ///     <see
    ///         cref="F:System.IO.Path.InvalidPathChars" />
    ///     .
    /// </exception>
    /// ###
    /// <exception cref="T:System.ArgumentNullException">
    ///     <paramref name="this" /> is null.
    /// </exception>
    /// ###
    /// <exception cref="T:System.IO.PathTooLongException">
    ///     The specified @this, file name, or both exceed the system-
    ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file
    ///     names must be less than 260 characters.
    /// </exception>
    /// ###
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///     The specified @this is invalid (for example, it is on
    ///     an unmapped drive).
    /// </exception>
    /// ###
    /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
    /// ###
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///     <paramref name="this" /> specified a file that is
    ///     read-only.-or- This operation is not supported on the current platform.-or-
    ///     <paramref
    ///         name="this" />
    ///     specified a directory.-or- The caller does not have the required permission.
    /// </exception>
    /// ###
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///     The file specified in <paramref name="this" /> was not
    ///     found.
    /// </exception>
    /// ###
    /// <exception cref="T:System.NotSupportedException">
    ///     <paramref name="this" /> is in an invalid format.
    /// </exception>
    /// ###
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
    public static String[] ReadAllLines(this FileInfo @this)
    {
        return File.ReadAllLines(@this.FullName);
    }

    /// <summary>
    ///     Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
    /// </summary>
    /// <param name="this">The file to open for reading.</param>
    /// <param name="encoding">The encoding applied to the contents of the file.</param>
    /// <returns>A string array containing all lines of the file.</returns>
    /// ###
    /// <exception cref="T:System.ArgumentException">
    ///     <paramref name="this" /> is a zero-length string, contains only
    ///     white space, or contains one or more invalid characters as defined by
    ///     <see
    ///         cref="F:System.IO.Path.InvalidPathChars" />
    ///     .
    /// </exception>
    /// ###
    /// <exception cref="T:System.ArgumentNullException">
    ///     <paramref name="this" /> is null.
    /// </exception>
    /// ###
    /// <exception cref="T:System.IO.PathTooLongException">
    ///     The specified @this, file name, or both exceed the system-
    ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file
    ///     names must be less than 260 characters.
    /// </exception>
    /// ###
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///     The specified @this is invalid (for example, it is on
    ///     an unmapped drive).
    /// </exception>
    /// ###
    /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
    /// ###
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///     <paramref name="this" /> specified a file that is
    ///     read-only.-or- This operation is not supported on the current platform.-or-
    ///     <paramref
    ///         name="this" />
    ///     specified a directory.-or- The caller does not have the required permission.
    /// </exception>
    /// ###
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///     The file specified in <paramref name="this" /> was not
    ///     found.
    /// </exception>
    /// ###
    /// <exception cref="T:System.NotSupportedException">
    ///     <paramref name="this" /> is in an invalid format.
    /// </exception>
    /// ###
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
    public static String[] ReadAllLines(this FileInfo @this, Encoding encoding)
    {
        return File.ReadAllLines(@this.FullName, encoding);
    }
}