using System.Collections.Generic;
using System.IO;
using System.Text;
#if NetFX
using System.Security.AccessControl;
#endif

//using System.Data.SqlServerCe;

// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A FileInfo extension method that appends all lines.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="contents">The contents.</param>
        public static void AppendAllLines(this FileInfo @this, IEnumerable<string> contents)
        {
            File.AppendAllLines(@this.FullName, contents);
        }

        /// <summary>
        ///     A FileInfo extension method that appends all lines.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="encoding">The encoding.</param>
        public static void AppendAllLines(this FileInfo @this, IEnumerable<string> contents, Encoding encoding)
        {
            File.AppendAllLines(@this.FullName, contents, encoding);
        }

        /// <summary>
        ///     Opens a file, appends the specified string to the file, and then closes the file. If the file does not exist,
        ///     this method creates a file, writes the specified string to the file, then closes the file.
        /// </summary>
        /// <param name="this">The file to append the specified string to.</param>
        /// <param name="contents">The string to append to the file.</param>
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
        ///     The specified @this is invalid (for example, the
        ///     directory doesn?t exist or it is on an unmapped drive).
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
        public static void AppendAllText(this FileInfo @this, string contents)
        {
            File.AppendAllText(@this.FullName, contents);
        }

        /// <summary>
        ///     Appends the specified string to the file, creating the file if it does not already exist.
        /// </summary>
        /// <param name="this">The file to append the specified string to.</param>
        /// <param name="contents">The string to append to the file.</param>
        /// <param name="encoding">The character encoding to use.</param>
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
        ///     The specified @this is invalid (for example, the
        ///     directory doesn?t exist or it is on an unmapped drive).
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
        public static void AppendAllText(this FileInfo @this, string contents, Encoding encoding)
        {
            File.AppendAllText(@this.FullName, contents, encoding);
        }

        /// <summary>
        ///     Changes the extension of a @this string.
        /// </summary>
        /// <param name="this">
        ///     The @this information to modify. The @this cannot contain any of the characters defined in
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </param>
        /// <param name="extension">
        ///     The new extension (with or without a leading period). Specify null to remove an existing
        ///     extension from
        ///     <paramref
        ///         name="this" />
        ///     .
        /// </param>
        /// <returns>
        ///     The modified @this information.On Windows-based desktop platforms, if <paramref name="this" /> is null or an
        ///     empty string (""), the @this information is returned unmodified. If
        ///     <paramref
        ///         name="extension" />
        ///     is null, the returned string contains the specified @this with its extension removed. If
        ///     <paramref
        ///         name="this" />
        ///     has no extension, and <paramref name="extension" /> is not null, the returned @this string contains
        ///     <paramref
        ///         name="extension" />
        ///     appended to the end of <paramref name="this" />.
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> contains one or more of the invalid
        ///     characters defined in
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </exception>
        public static string ChangeExtension(this FileInfo @this, string extension)
        {
            return Path.ChangeExtension(@this.FullName, extension);
        }

        /// <summary>
        ///     Creates all directories and subdirectories in the specified @this.
        /// </summary>
        /// <param name="this">The directory @this to create.</param>
        /// <returns>An object that represents the directory for the specified @this.</returns>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     The directory specified by <paramref name="this" /> is a file.-or-The
        ///     network name is not known.
        /// </exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length string, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     .-or-<paramref name="this" /> is prefixed with, or contains only a colon character (:).
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, it is on
        ///     an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> contains a colon character (:) that
        ///     is not part of a drive label ("C:\").
        /// </exception>
        public static DirectoryInfo CreateDirectory(this FileInfo @this)
        {
            return Directory.CreateDirectory(@this.Directory.FullName);
        }

#if NetFX
        /// <summary>
        ///     Creates all the directories in the specified @this, applying the specified Windows security.
        /// </summary>
        /// <param name="this">The directory to create.</param>
        /// <param name="directorySecurity">The access control to apply to the directory.</param>
        /// <returns>An object that represents the directory for the specified @this.</returns>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     The directory specified by <paramref name="this" /> is a file.-or-The
        ///     network name is not known.
        /// </exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length string, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     . -or-<paramref name="this" /> is prefixed with, or contains only a colon character (:).
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, it is on
        ///     an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> contains a colon character (:) that
        ///     is not part of a drive label ("C:\").
        /// </exception>
        public static DirectoryInfo CreateDirectory(this FileInfo @this, DirectorySecurity directorySecurity)
        {
            return Directory.CreateDirectory(@this.Directory.FullName, directorySecurity);
        }
#endif
        
        /// <summary>
        ///     Creates all directories and subdirectories in the specified @this if the directory doesn't already exists.
        ///     This methods is the same as FileInfo.CreateDirectory however it's less ambigues about what happen if the
        ///     directory already exists.
        /// </summary>
        /// <param name="this">The directory @this to create.</param>
        /// <returns>An object that represents the directory for the specified @this.</returns>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     The directory specified by <paramref name="this" /> is a file.-or-The
        ///     network name is not known.
        /// </exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length string, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     .-or-<paramref name="this" /> is prefixed with, or contains only a colon character (:).
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, it is on
        ///     an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> contains a colon character (:) that
        ///     is not part of a drive label ("C:\").
        /// </exception>
        public static DirectoryInfo EnsureDirectoryExists(this FileInfo @this)
        {
            return Directory.CreateDirectory(@this.Directory.FullName);
        }

#if NetFX
        /// <summary>
        ///     Creates all directories and subdirectories in the specified @this if the directory doesn't already exists.
        ///     This methods is the same as FileInfo.CreateDirectory however it's less ambigues about what happen if the
        ///     directory already exists.
        /// </summary>
        /// <param name="this">The directory to create.</param>
        /// <param name="directorySecurity">The access control to apply to the directory.</param>
        /// <returns>An object that represents the directory for the specified @this.</returns>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     The directory specified by <paramref name="this" /> is a file.-or-The
        ///     network name is not known.
        /// </exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length string, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     . -or-<paramref name="this" /> is prefixed with, or contains only a colon character (:).
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, it is on
        ///     an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> contains a colon character (:) that
        ///     is not part of a drive label ("C:\").
        /// </exception>
        public static DirectoryInfo EnsureDirectoryExists(this FileInfo @this, DirectorySecurity directorySecurity)
        {
            return Directory.CreateDirectory(@this.Directory.FullName, directorySecurity);
        }
#endif

        /// <summary>
        ///     A FileInfo extension method that gets directory full name.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The directory full name.</returns>
        public static string GetDirectoryFullName(this FileInfo @this)
        {
            return @this.Directory.FullName;
        }

        /// <summary>
        ///     A FileInfo extension method that gets directory name.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The directory name.</returns>
        public static string GetDirectoryName(this FileInfo @this)
        {
            return @this.Directory.Name;
        }

        /// <summary>
        ///     Returns the file name of the specified @this string without the extension.
        /// </summary>
        /// <param name="this">The @this of the file.</param>
        /// <returns>
        ///     The string returned by <see cref="M:System.IO.Path.GetFileName(System.String)" />, minus the last period (.)
        ///     and all characters following it.
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> contains one or more of the invalid
        ///     characters defined in
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </exception>
        public static string GetFileNameWithoutExtension(this FileInfo @this)
        {
            return Path.GetFileNameWithoutExtension(@this.FullName);
        }

        /// <summary>
        ///     Gets the root directory information of the specified @this.
        /// </summary>
        /// <param name="this">The @this from which to obtain root directory information.</param>
        /// <returns>
        ///     The root directory of <paramref name="this" />, such as "C:\", or null if <paramref name="this" /> is null,
        ///     or an empty string if
        ///     <paramref
        ///         name="this" />
        ///     does not contain root directory information.
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> contains one or more of the invalid
        ///     characters defined in
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .-or- <see cref="F:System.String.Empty" /> was passed to
        ///     <paramref
        ///         name="this" />
        ///     .
        /// </exception>
        public static string GetPathRoot(this FileInfo @this)
        {
            return Path.GetPathRoot(@this.FullName);
        }

        /// <summary>
        ///     Determines whether a @this includes a file name extension.
        /// </summary>
        /// <param name="this">The @this to search for an extension.</param>
        /// <returns>
        ///     true if the characters that follow the last directory separator (\\ or /) or volume separator (:) in the
        ///     @this include a period (.) followed by one or more characters; otherwise, false.
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> contains one or more of the invalid
        ///     characters defined in
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </exception>
        public static bool HasExtension(this FileInfo @this)
        {
            return Path.HasExtension(@this.FullName);
        }

        /// <summary>
        ///     Gets a value indicating whether the specified @this string contains a root.
        /// </summary>
        /// <param name="this">The @this to test.</param>
        /// <returns>
        ///     true if <paramref name="this" /> contains a root; otherwise, false.
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> contains one or more of the invalid
        ///     characters defined in
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </exception>
        public static bool IsPathRooted(this FileInfo @this)
        {
            return Path.IsPathRooted(@this.FullName);
        }

        /// <summary>
        ///     Opens a binary file, reads the contents of the file into a byte array, and then closes the file.
        /// </summary>
        /// <param name="this">The file to open for reading.</param>
        /// <returns>A byte array containing the contents of the file.</returns>
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
        ///     This operation is not supported on the current
        ///     platform.-or- <paramref name="this" /> specified a directory.-or- The caller does not have the required
        ///     permission.
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
        public static byte[] ReadAllBytes(this FileInfo @this)
        {
            return File.ReadAllBytes(@this.FullName);
        }

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
        public static string[] ReadAllLines(this FileInfo @this)
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
        public static string[] ReadAllLines(this FileInfo @this, Encoding encoding)
        {
            return File.ReadAllLines(@this.FullName, encoding);
        }

        /// <summary>
        ///     Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        /// <param name="this">The file to open for reading.</param>
        /// <returns>A string containing all lines of the file.</returns>
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
        public static string ReadAllText(this FileInfo @this)
        {
            return File.ReadAllText(@this.FullName);
        }

        /// <summary>
        ///     Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
        /// </summary>
        /// <param name="this">The file to open for reading.</param>
        /// <param name="encoding">The encoding applied to the contents of the file.</param>
        /// <returns>A string containing all lines of the file.</returns>
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
        public static string ReadAllText(this FileInfo @this, Encoding encoding)
        {
            return File.ReadAllText(@this.FullName, encoding);
        }

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
        public static IEnumerable<string> ReadLines(this FileInfo @this)
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
        public static IEnumerable<string> ReadLines(this FileInfo @this, Encoding encoding)
        {
            return File.ReadLines(@this.FullName, encoding);
        }

        /// <summary>
        ///     A FileInfo extension method that reads the file to the end.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>
        ///     The rest of the stream as a string, from the current position to the end. If the current position is at the
        ///     end of the stream, returns an empty string ("").
        /// </returns>
        public static string ReadToEnd(this FileInfo @this)
        {
            using (var stream = File.Open(@this.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new StreamReader(stream, Encoding.Default))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that reads the file to the end.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="position">The position.</param>
        /// <returns>
        ///     The rest of the stream as a string, from the current position to the end. If the current position is at the
        ///     end of the stream, returns an empty string ("").
        /// </returns>
        public static string ReadToEnd(this FileInfo @this, long position)
        {
            using (var stream = File.Open(@this.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                stream.Position = position;

                using (var reader = new StreamReader(stream, Encoding.Default))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that reads the file to the end.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>
        ///     The rest of the stream as a string, from the current position to the end. If the current position is at the
        ///     end of the stream, returns an empty string ("").
        /// </returns>
        public static string ReadToEnd(this FileInfo @this, Encoding encoding)
        {
            using (var stream = File.Open(@this.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that reads the file to the end.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="position">The position.</param>
        /// <returns>
        ///     The rest of the stream as a string, from the current position to the end. If the current position is at the
        ///     end of the stream, returns an empty string ("").
        /// </returns>
        public static string ReadToEnd(this FileInfo @this, Encoding encoding, long position)
        {
            using (var stream = File.Open(@this.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                stream.Position = position;

                using (var reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that renames.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="newName">Name of the new.</param>
        /// ###
        /// <returns>.</returns>
        public static void Rename(this FileInfo @this, string newName)
        {
            var filePath = Path.Combine(@this.Directory.FullName, newName);
            @this.MoveTo(filePath);
        }

        /// <summary>
        ///     Changes the extension of a @this string.
        /// </summary>
        /// <param name="this">
        ///     The @this information to modify. The @this cannot contain any of the characters defined in
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </param>
        /// <param name="extension">
        ///     The new extension (with or without a leading period). Specify null to remove an existing
        ///     extension from
        ///     <paramref
        ///         name="this" />
        ///     .
        /// </param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> contains one or more of the invalid
        ///     characters defined in
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </exception>
        public static void RenameExtension(this FileInfo @this, string extension)
        {
            var filePath = Path.ChangeExtension(@this.FullName, extension);
            @this.MoveTo(filePath);
        }

        /// <summary>
        ///     A FileInfo extension method that rename file without extension.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="newName">Name of the new.</param>
        /// ###
        /// <returns>.</returns>
        public static void RenameFileWithoutExtension(this FileInfo @this, string newName)
        {
            var fileName = string.Concat(newName, @this.Extension);
            var filePath = Path.Combine(@this.Directory.FullName, fileName);
            @this.MoveTo(filePath);
        }

        /// <summary>
        ///     Creates a new file, writes the specified byte array to the file, and then closes the file. If the target file
        ///     already exists, it is overwritten.
        /// </summary>
        /// <param name="this">The file to write to.</param>
        /// <param name="bytes">The bytes to write to the file.</param>
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
        ///     <paramref name="this" /> is null or the byte array is empty.
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
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> is in an invalid format.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public static void WriteAllBytes(this FileInfo @this, byte[] bytes)
        {
            File.WriteAllBytes(@this.FullName, bytes);
        }

        /// <summary>
        ///     Creates a new file, write the specified string array to the file, and then closes the file.
        /// </summary>
        /// <param name="this">The file to write to.</param>
        /// <param name="contents">The string array to write to the file.</param>
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
        ///     Either <paramref name="this" /> or
        ///     <paramref name="contents" /> is null.
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
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> is in an invalid format.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public static void WriteAllLines(this FileInfo @this, string[] contents)
        {
            File.WriteAllLines(@this.FullName, contents);
        }

        /// <summary>
        ///     Creates a new file, writes the specified string array to the file by using the specified encoding, and then
        ///     closes the file.
        /// </summary>
        /// <param name="this">The file to write to.</param>
        /// <param name="contents">The string array to write to the file.</param>
        /// <param name="encoding">
        ///     An <see cref="T:System.Text.Encoding" /> object that represents the character encoding
        ///     applied to the string array.
        /// </param>
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
        ///     Either <paramref name="this" /> or
        ///     <paramref name="contents" /> is null.
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
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> is in an invalid format.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public static void WriteAllLines(this FileInfo @this, string[] contents, Encoding encoding)
        {
            File.WriteAllLines(@this.FullName, contents, encoding);
        }

        /// <summary>
        ///     Creates a new file, write the specified string array to the file, and then closes the file.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="contents">The string array to write to the file.</param>
        public static void WriteAllLines(this FileInfo @this, IEnumerable<string> contents)
        {
            File.WriteAllLines(@this.FullName, contents);
        }

        /// <summary>
        ///     Creates a new file, writes the specified string array to the file by using the specified encoding, and then
        ///     closes the file.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="contents">The string array to write to the file.</param>
        /// <param name="encoding">
        ///     An <see cref="T:System.Text.Encoding" /> object that represents the character encoding
        ///     applied to the string array.
        /// </param>
        public static void WriteAllLines(this FileInfo @this, IEnumerable<string> contents, Encoding encoding)
        {
            File.WriteAllLines(@this.FullName, contents, encoding);
        }

        /// <summary>
        ///     Creates a new file, writes the specified string to the file, and then closes the file. If the target file
        ///     already exists, it is overwritten.
        /// </summary>
        /// <param name="this">The file to write to.</param>
        /// <param name="contents">The string to write to the file.</param>
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
        ///     <paramref name="this" /> is null or
        ///     <paramref name="contents" /> is empty.
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
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> is in an invalid format.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public static void WriteAllText(this FileInfo @this, string contents)
        {
            File.WriteAllText(@this.FullName, contents);
        }

        /// <summary>
        ///     Creates a new file, writes the specified string to the file using the specified encoding, and then closes the
        ///     file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="this">The file to write to.</param>
        /// <param name="contents">The string to write to the file.</param>
        /// <param name="encoding">The encoding to apply to the string.</param>
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
        ///     <paramref name="this" /> is null or
        ///     <paramref name="contents" /> is empty.
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
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> is in an invalid format.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public static void WriteAllText(this FileInfo @this, string contents, Encoding encoding)
        {
            File.WriteAllText(@this.FullName, contents, encoding);
        }
    }
}