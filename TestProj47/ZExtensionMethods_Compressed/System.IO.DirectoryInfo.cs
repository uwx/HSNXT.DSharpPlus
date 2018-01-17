using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
//using System.IO;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A DirectoryInfo extension method that clears all files and directories in this directory.
        /// </summary>
        /// <param name="obj">The obj to act on.</param>
        public static void Clear(this DirectoryInfo obj)
        {
            Array.ForEach(obj.GetFiles(), x => x.Delete());
            Array.ForEach(obj.GetDirectories(), x => x.Delete(true));
        }

        /// <summary>A DirectoryInfo extension method that copies to.</summary>
        /// <param name="obj">The obj to act on.</param>
        /// <param name="destDirName">Pathname of the destination directory.</param>
        public static void CopyTo(this DirectoryInfo obj, string destDirName)
        {
            obj.CopyTo(destDirName, "*.*", SearchOption.TopDirectoryOnly);
        }

        /// <summary>A DirectoryInfo extension method that copies to.</summary>
        /// <param name="obj">The obj to act on.</param>
        /// <param name="destDirName">Pathname of the destination directory.</param>
        /// <param name="searchPattern">A pattern specifying the search.</param>
        public static void CopyTo(this DirectoryInfo obj, string destDirName, string searchPattern)
        {
            obj.CopyTo(destDirName, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>A DirectoryInfo extension method that copies to.</summary>
        /// <param name="obj">The obj to act on.</param>
        /// <param name="destDirName">Pathname of the destination directory.</param>
        /// <param name="searchOption">The search option.</param>
        public static void CopyTo(this DirectoryInfo obj, string destDirName, SearchOption searchOption)
        {
            obj.CopyTo(destDirName, "*.*", searchOption);
        }

        /// <summary>A DirectoryInfo extension method that copies to.</summary>
        /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
        /// <param name="obj">The obj to act on.</param>
        /// <param name="destDirName">Pathname of the destination directory.</param>
        /// <param name="searchPattern">A pattern specifying the search.</param>
        /// <param name="searchOption">The search option.</param>
        public static void CopyTo(this DirectoryInfo obj, string destDirName, string searchPattern,
            SearchOption searchOption)
        {
            var files = obj.GetFiles(searchPattern, searchOption);
            foreach (var file in files)
            {
                var outputFile = destDirName + file.FullName.Substring(obj.FullName.Length);
                var directory = new FileInfo(outputFile).Directory;

                if (directory == null)
                {
                    throw new Exception("The directory cannot be null.");
                }

                if (!directory.Exists)
                {
                    directory.Create();
                }

                file.CopyTo(outputFile);
            }

            // Ensure empty dir are also copied
            var directories = obj.GetDirectories(searchPattern, searchOption);
            foreach (var directory in directories)
            {
                var outputDirectory = destDirName + directory.FullName.Substring(obj.FullName.Length);
                var directoryInfo = new DirectoryInfo(outputDirectory);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }
            }
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
        public static DirectoryInfo CreateAllDirectories(this DirectoryInfo @this)
        {
            return Directory.CreateDirectory(@this.FullName);
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
        public static DirectoryInfo CreateAllDirectories(this DirectoryInfo @this, DirectorySecurity directorySecurity)
        {
            return Directory.CreateDirectory(@this.FullName, directorySecurity);
        }
#endif

        /// <summary>
        ///     A DirectoryInfo extension method that deletes the directories where.
        /// </summary>
        /// <param name="obj">The obj to act on.</param>
        /// <param name="predicate">The predicate.</param>
        public static void DeleteDirectoriesWhere(this DirectoryInfo obj, Func<DirectoryInfo, bool> predicate)
        {
            obj.GetDirectories().Where(predicate).ForEach(x => x.Delete());
        }

        /// <summary>
        ///     A DirectoryInfo extension method that deletes the directories where.
        /// </summary>
        /// <param name="obj">The obj to act on.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="predicate">The predicate.</param>
        public static void DeleteDirectoriesWhere(this DirectoryInfo obj, SearchOption searchOption,
            Func<DirectoryInfo, bool> predicate)
        {
            obj.GetDirectories("*.*", searchOption).Where(predicate).ForEach(x => x.Delete());
        }

        /// <summary>
        ///     A DirectoryInfo extension method that deletes the files where.
        /// </summary>
        /// <param name="obj">The obj to act on.</param>
        /// <param name="predicate">The predicate.</param>
        public static void DeleteFilesWhere(this DirectoryInfo obj, Func<FileInfo, bool> predicate)
        {
            obj.GetFiles().Where(predicate).ForEach(x => x.Delete());
        }

        /// <summary>
        ///     A DirectoryInfo extension method that deletes the files where.
        /// </summary>
        /// <param name="obj">The obj to act on.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="predicate">The predicate.</param>
        public static void DeleteFilesWhere(this DirectoryInfo obj, SearchOption searchOption,
            Func<FileInfo, bool> predicate)
        {
            obj.GetFiles("*.*", searchOption).Where(predicate).ForEach(x => x.Delete());
        }

        /// <summary>
        ///     A DirectoryInfo extension method that deletes the older than.
        /// </summary>
        /// <param name="obj">The obj to act on.</param>
        /// <param name="timeSpan">The time span.</param>
        public static void DeleteOlderThan(this DirectoryInfo obj, TimeSpan timeSpan)
        {
            var minDate = DateTime.Now.Subtract(timeSpan);
            obj.GetFiles().Where(x => x.LastWriteTime < minDate).ToList().ForEach(x => x.Delete());
            obj.GetDirectories().Where(x => x.LastWriteTime < minDate).ToList().ForEach(x => x.Delete());
        }

        /// <summary>
        ///     A DirectoryInfo extension method that deletes the older than.
        /// </summary>
        /// <param name="obj">The obj to act on.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="timeSpan">The time span.</param>
        public static void DeleteOlderThan(this DirectoryInfo obj, SearchOption searchOption, TimeSpan timeSpan)
        {
            var minDate = DateTime.Now.Subtract(timeSpan);
            obj.GetFiles("*.*", searchOption).Where(x => x.LastWriteTime < minDate).ToList().ForEach(x => x.Delete());
            obj.GetDirectories("*.*", searchOption).Where(x => x.LastWriteTime < minDate).ToList()
                .ForEach(x => x.Delete());
        }

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
        public static DirectoryInfo EnsureDirectoryExists(this DirectoryInfo @this)
        {
            return Directory.CreateDirectory(@this.FullName);
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
        public static DirectoryInfo EnsureDirectoryExists(this DirectoryInfo @this, DirectorySecurity directorySecurity)
        {
            return Directory.CreateDirectory(@this.FullName, directorySecurity);
        }
#endif
        
        /// <summary>
        ///     Returns an enumerable collection of directory names in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the directories in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     .
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<DirectoryInfo> EnumerateDirectories(this DirectoryInfo @this)
        {
            return Directory.EnumerateDirectories(@this.FullName).Select(x => new DirectoryInfo(x));
        }

        /// <summary>
        ///     Returns an enumerable collection of directory names that match a search pattern in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the directories in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateDirectories
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateDirectories()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateDirectories&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;Directory1&quot;);
        ///                   root.CreateSubdirectory(&quot;Directory2&quot;);
        /// 
        ///                   // Exemples
        ///                   List&lt;DirectoryInfo&gt; result = root.EnumerateDirectories().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateDirectories
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateDirectories()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateDirectories&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;Directory1&quot;);
        ///                   root.CreateSubdirectory(&quot;Directory2&quot;);
        /// 
        ///                   // Exemples
        ///                   List&lt;DirectoryInfo&gt; result = root.EnumerateDirectories().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<DirectoryInfo> EnumerateDirectories(this DirectoryInfo @this, String searchPattern)
        {
            return Directory.EnumerateDirectories(@this.FullName, searchPattern).Select(x => new DirectoryInfo(x));
        }

        /// <summary>
        ///     Returns an enumerable collection of directory names that match a search pattern in a specified @this, and
        ///     optionally searches subdirectories.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="searchOption">
        ///     One of the enumeration values that specifies whether the search operation should
        ///     include only the current directory or should include all subdirectories.The default value is
        ///     <see
        ///         cref="F:System.IO.SearchOption.TopDirectoryOnly" />
        ///     .
        /// </param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the directories in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern and option.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateDirectories
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateDirectories()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateDirectories&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;Directory1&quot;);
        ///                   root.CreateSubdirectory(&quot;Directory2&quot;);
        /// 
        ///                   // Exemples
        ///                   List&lt;DirectoryInfo&gt; result = root.EnumerateDirectories().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateDirectories
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateDirectories()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateDirectories&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;Directory1&quot;);
        ///                   root.CreateSubdirectory(&quot;Directory2&quot;);
        /// 
        ///                   // Exemples
        ///                   List&lt;DirectoryInfo&gt; result = root.EnumerateDirectories().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="searchOption" /> is not a valid
        ///     <see cref="T:System.IO.SearchOption" /> value.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<DirectoryInfo> EnumerateDirectories(this DirectoryInfo @this, String searchPattern,
            SearchOption searchOption)
        {
            return Directory.EnumerateDirectories(@this.FullName, searchPattern, searchOption)
                .Select(x => new DirectoryInfo(x));
        }

        /// <summary>
        ///     Returns an enumerable collection of directory names that match a search pattern in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">The search string to match against the names of directories in.</param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the directories in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateDirectories
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateDirectories()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateDirectories&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;Directory1&quot;);
        ///                   root.CreateSubdirectory(&quot;Directory2&quot;);
        /// 
        ///                   // Exemples
        ///                   List&lt;DirectoryInfo&gt; result = root.EnumerateDirectories().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateDirectories
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateDirectories()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateDirectories&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;Directory1&quot;);
        ///                   root.CreateSubdirectory(&quot;Directory2&quot;);
        /// 
        ///                   // Exemples
        ///                   List&lt;DirectoryInfo&gt; result = root.EnumerateDirectories().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<DirectoryInfo> EnumerateDirectories(this DirectoryInfo @this, String[] searchPatterns)
        {
            return searchPatterns.SelectMany(x => @this.GetDirectories(x)).Distinct();
        }

        /// <summary>
        ///     Returns an enumerable collection of directory names that match a search pattern in a specified @this, and
        ///     optionally searches subdirectories.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="searchOption">
        ///     One of the enumeration values that specifies whether the search operation should
        ///     include only the current directory or should include all subdirectories.The default value is
        ///     <see
        ///         cref="F:System.IO.SearchOption.TopDirectoryOnly" />
        ///     .
        /// </param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the directories in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern and option.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateDirectories
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateDirectories()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateDirectories&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;Directory1&quot;);
        ///                   root.CreateSubdirectory(&quot;Directory2&quot;);
        /// 
        ///                   // Exemples
        ///                   List&lt;DirectoryInfo&gt; result = root.EnumerateDirectories().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateDirectories
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateDirectories()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateDirectories&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;Directory1&quot;);
        ///                   root.CreateSubdirectory(&quot;Directory2&quot;);
        /// 
        ///                   // Exemples
        ///                   List&lt;DirectoryInfo&gt; result = root.EnumerateDirectories().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="searchOption" /> is not a valid
        ///     <see cref="T:System.IO.SearchOption" /> value.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<DirectoryInfo> EnumerateDirectories(this DirectoryInfo @this, String[] searchPatterns,
            SearchOption searchOption)
        {
            return searchPatterns.SelectMany(x => @this.GetDirectories(x, searchOption)).Distinct();
        }

        /// <summary>
        ///     Returns an enumerable collection of file names in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the files in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     .
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo @this)
        {
            return Directory.EnumerateFiles(@this.FullName).Select(x => new FileInfo(x));
        }

        /// <summary>
        ///     Returns an enumerable collection of file names that match a search pattern in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the files in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateFiles
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateFiles()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateFiles&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   var file2 = new FileInfo(Path.Combine(root.FullName, &quot;test2.txt&quot;));
        ///                   file1.Create();
        ///                   file2.Create();
        /// 
        ///                   // Exemples
        ///                   List&lt;FileInfo&gt; result = root.EnumerateFiles().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateFiles
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateFiles()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateFiles&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   var file2 = new FileInfo(Path.Combine(root.FullName, &quot;test2.txt&quot;));
        ///                   file1.Create();
        ///                   file2.Create();
        /// 
        ///                   // Exemples
        ///                   List&lt;FileInfo&gt; result = root.EnumerateFiles().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo @this, String searchPattern)
        {
            return Directory.EnumerateFiles(@this.FullName, searchPattern).Select(x => new FileInfo(x));
        }

        /// <summary>
        ///     Returns an enumerable collection of file names that match a search pattern in a specified @this, and
        ///     optionally searches subdirectories.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="searchOption">
        ///     One of the enumeration values that specifies whether the search operation should
        ///     include only the current directory or should include all subdirectories.The default value is
        ///     <see
        ///         cref="F:System.IO.SearchOption.TopDirectoryOnly" />
        ///     .
        /// </param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the files in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern and option.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateFiles
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateFiles()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateFiles&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   var file2 = new FileInfo(Path.Combine(root.FullName, &quot;test2.txt&quot;));
        ///                   file1.Create();
        ///                   file2.Create();
        /// 
        ///                   // Exemples
        ///                   List&lt;FileInfo&gt; result = root.EnumerateFiles().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateFiles
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateFiles()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateFiles&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   var file2 = new FileInfo(Path.Combine(root.FullName, &quot;test2.txt&quot;));
        ///                   file1.Create();
        ///                   file2.Create();
        /// 
        ///                   // Exemples
        ///                   List&lt;FileInfo&gt; result = root.EnumerateFiles().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="searchOption" /> is not a valid
        ///     <see cref="T:System.IO.SearchOption" /> value.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo @this, String searchPattern,
            SearchOption searchOption)
        {
            return Directory.EnumerateFiles(@this.FullName, searchPattern, searchOption).Select(x => new FileInfo(x));
        }

        /// <summary>
        ///     Returns an enumerable collection of file names that match a search pattern in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">The search string to match against the names of directories in.</param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the files in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateFiles
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateFiles()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateFiles&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   var file2 = new FileInfo(Path.Combine(root.FullName, &quot;test2.txt&quot;));
        ///                   file1.Create();
        ///                   file2.Create();
        /// 
        ///                   // Exemples
        ///                   List&lt;FileInfo&gt; result = root.EnumerateFiles().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateFiles
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateFiles()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateFiles&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   var file2 = new FileInfo(Path.Combine(root.FullName, &quot;test2.txt&quot;));
        ///                   file1.Create();
        ///                   file2.Create();
        /// 
        ///                   // Exemples
        ///                   List&lt;FileInfo&gt; result = root.EnumerateFiles().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo @this, String[] searchPatterns)
        {
            return searchPatterns.SelectMany(x => @this.GetFiles(x)).Distinct();
        }

        /// <summary>
        ///     Returns an enumerable collection of file names that match a search pattern in a specified @this, and
        ///     optionally searches subdirectories.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="searchOption">
        ///     One of the enumeration values that specifies whether the search operation should
        ///     include only the current directory or should include all subdirectories.The default value is
        ///     <see
        ///         cref="F:System.IO.SearchOption.TopDirectoryOnly" />
        ///     .
        /// </param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the files in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern and option.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateFiles
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateFiles()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateFiles&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   var file2 = new FileInfo(Path.Combine(root.FullName, &quot;test2.txt&quot;));
        ///                   file1.Create();
        ///                   file2.Create();
        /// 
        ///                   // Exemples
        ///                   List&lt;FileInfo&gt; result = root.EnumerateFiles().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateFiles
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateFiles()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateFiles&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   var file2 = new FileInfo(Path.Combine(root.FullName, &quot;test2.txt&quot;));
        ///                   file1.Create();
        ///                   file2.Create();
        /// 
        ///                   // Exemples
        ///                   List&lt;FileInfo&gt; result = root.EnumerateFiles().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="searchOption" /> is not a valid
        ///     <see cref="T:System.IO.SearchOption" /> value.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo @this, String[] searchPatterns,
            SearchOption searchOption)
        {
            return searchPatterns.SelectMany(x => @this.GetFiles(x, searchOption)).Distinct();
        }

        /// <summary>
        ///     Returns an enumerable collection of file-system entries in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <returns>
        ///     An enumerable collection of file-system entries in the directory specified by <paramref name="this" />.
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<string> EnumerateFileSystemEntries(this DirectoryInfo @this)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName);
        }

        /// <summary>
        ///     Returns an enumerable collection of file-system entries that match a search pattern in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <returns>
        ///     An enumerable collection of file-system entries in the directory specified by <paramref name="this" /> and
        ///     that match the specified search pattern.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateFileSystemEntries
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateFileSystemEntries()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateFileSystemEntries&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        ///                   root.CreateSubdirectory(&quot;test2&quot;);
        /// 
        ///                   // Exemples
        ///                   List&lt;string&gt; result = root.EnumerateFileSystemEntries().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateFileSystemEntries
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateFileSystemEntries()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateFileSystemEntries&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        ///                   root.CreateSubdirectory(&quot;test2&quot;);
        /// 
        ///                   // Exemples
        ///                   List&lt;string&gt; result = root.EnumerateFileSystemEntries().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<string> EnumerateFileSystemEntries(this DirectoryInfo @this, String searchPattern)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName, searchPattern);
        }

        /// <summary>
        ///     Returns an enumerable collection of file names and directory names that match a search pattern in a specified
        ///     @this, and optionally searches subdirectories.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="searchOption">
        ///     One of the enumeration values  that specifies whether the search operation should
        ///     include only the current directory or should include all subdirectories.The default value is
        ///     <see
        ///         cref="F:System.IO.SearchOption.TopDirectoryOnly" />
        ///     .
        /// </param>
        /// <returns>
        ///     An enumerable collection of file-system entries in the directory specified by <paramref name="this" /> and
        ///     that match the specified search pattern and option.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateFileSystemEntries
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateFileSystemEntries()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateFileSystemEntries&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        ///                   root.CreateSubdirectory(&quot;test2&quot;);
        /// 
        ///                   // Exemples
        ///                   List&lt;string&gt; result = root.EnumerateFileSystemEntries().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateFileSystemEntries
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateFileSystemEntries()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateFileSystemEntries&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        ///                   root.CreateSubdirectory(&quot;test2&quot;);
        /// 
        ///                   // Exemples
        ///                   List&lt;string&gt; result = root.EnumerateFileSystemEntries().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="searchOption" /> is not a valid
        ///     <see cref="T:System.IO.SearchOption" /> value.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<string> EnumerateFileSystemEntries(this DirectoryInfo @this, String searchPattern,
            SearchOption searchOption)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName, searchPattern, searchOption);
        }

        /// <summary>
        ///     Returns an enumerable collection of file-system entries that match a search pattern in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">The search string to match against the names of directories in.</param>
        /// <returns>
        ///     An enumerable collection of file-system entries in the directory specified by <paramref name="this" /> and
        ///     that match the specified search pattern.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateFileSystemEntries
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateFileSystemEntries()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateFileSystemEntries&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        ///                   root.CreateSubdirectory(&quot;test2&quot;);
        /// 
        ///                   // Exemples
        ///                   List&lt;string&gt; result = root.EnumerateFileSystemEntries().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateFileSystemEntries
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateFileSystemEntries()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateFileSystemEntries&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        ///                   root.CreateSubdirectory(&quot;test2&quot;);
        /// 
        ///                   // Exemples
        ///                   List&lt;string&gt; result = root.EnumerateFileSystemEntries().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<string> EnumerateFileSystemEntries(this DirectoryInfo @this, String[] searchPatterns)
        {
            return searchPatterns.SelectMany(x => Directory.EnumerateFileSystemEntries(@this.FullName, x)).Distinct();
        }

        /// <summary>
        ///     Returns an enumerable collection of file names and directory names that match a search pattern in a specified
        ///     @this, and optionally searches subdirectories.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="searchOption">
        ///     One of the enumeration values  that specifies whether the search operation should
        ///     include only the current directory or should include all subdirectories.The default value is
        ///     <see
        ///         cref="F:System.IO.SearchOption.TopDirectoryOnly" />
        ///     .
        /// </param>
        /// <returns>
        ///     An enumerable collection of file-system entries in the directory specified by <paramref name="this" /> and
        ///     that match the specified search pattern and option.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateFileSystemEntries
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateFileSystemEntries()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateFileSystemEntries&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        ///                   root.CreateSubdirectory(&quot;test2&quot;);
        /// 
        ///                   // Exemples
        ///                   List&lt;string&gt; result = root.EnumerateFileSystemEntries().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.Collections.Generic;
        ///       using System.IO;
        ///       using System.Linq;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_EnumerateFileSystemEntries
        ///           {
        ///               [TestMethod]
        ///               public void EnumerateFileSystemEntries()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_EnumerateFileSystemEntries&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        ///                   root.CreateSubdirectory(&quot;test2&quot;);
        /// 
        ///                   // Exemples
        ///                   List&lt;string&gt; result = root.EnumerateFileSystemEntries().ToList();
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Count);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="searchOption" /> is not a valid
        ///     <see cref="T:System.IO.SearchOption" /> value.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static IEnumerable<string> EnumerateFileSystemEntries(this DirectoryInfo @this, String[] searchPatterns,
            SearchOption searchOption)
        {
            return searchPatterns.SelectMany(x => Directory.EnumerateFileSystemEntries(@this.FullName, x, searchOption))
                .Distinct();
        }

        /// <summary>
        ///     Returns an enumerable collection of directory names that match a search pattern in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">The search string to match against the names of directories in.</param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the directories in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern.
        /// </returns>
        /// ###
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static DirectoryInfo[] GetDirectories(this DirectoryInfo @this, String[] searchPatterns)
        {
            return searchPatterns.SelectMany(x => @this.GetDirectories(x)).Distinct().ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of directory names that match a search pattern in a specified @this, and
        ///     optionally searches subdirectories.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="searchOption">
        ///     One of the enumeration values that specifies whether the search operation should
        ///     include only the current directory or should include all subdirectories.The default value is
        ///     <see
        ///         cref="F:System.IO.SearchOption.TopDirectoryOnly" />
        ///     .
        /// </param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the directories in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern and option.
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="searchOption" /> is not a valid
        ///     <see cref="T:System.IO.SearchOption" /> value.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static DirectoryInfo[] GetDirectories(this DirectoryInfo @this, String[] searchPatterns,
            SearchOption searchOption)
        {
            return searchPatterns.SelectMany(x => @this.GetDirectories(x, searchOption)).Distinct().ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of directory names in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the directories in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     .
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static DirectoryInfo[] GetDirectoriesWhere(this DirectoryInfo @this, Func<DirectoryInfo, bool> predicate)
        {
            return Directory.EnumerateDirectories(@this.FullName).Select(x => new DirectoryInfo(x))
                .Where(x => predicate(x)).ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of directory names that match a search pattern in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the directories in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetDirectoriesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetDirectoriesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetDirectories&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirNotFound123&quot;);
        /// 
        ///                   // Exemples
        ///                   DirectoryInfo[] result = root.GetDirectoriesWhere(x =&gt; x.Name.StartsWith(&quot;DirFizz&quot;) || x.Name.StartsWith(&quot;DirBuzz&quot;));
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetDirectoriesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetDirectoriesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetDirectories&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirNotFound123&quot;);
        /// 
        ///                   // Exemples
        ///                   DirectoryInfo[] result = root.GetDirectoriesWhere(x =&gt; x.Name.StartsWith(&quot;DirFizz&quot;) || x.Name.StartsWith(&quot;DirBuzz&quot;));
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static DirectoryInfo[] GetDirectoriesWhere(this DirectoryInfo @this, String searchPattern,
            Func<DirectoryInfo, bool> predicate)
        {
            return Directory.EnumerateDirectories(@this.FullName, searchPattern).Select(x => new DirectoryInfo(x))
                .Where(x => predicate(x)).ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of directory names that match a search pattern in a specified @this, and
        ///     optionally searches subdirectories.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="searchOption">
        ///     One of the enumeration values that specifies whether the search operation should
        ///     include only the current directory or should include all subdirectories.The default value is
        ///     <see
        ///         cref="F:System.IO.SearchOption.TopDirectoryOnly" />
        ///     .
        /// </param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the directories in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern and option.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetDirectoriesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetDirectoriesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetDirectories&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirNotFound123&quot;);
        /// 
        ///                   // Exemples
        ///                   DirectoryInfo[] result = root.GetDirectoriesWhere(x =&gt; x.Name.StartsWith(&quot;DirFizz&quot;) || x.Name.StartsWith(&quot;DirBuzz&quot;));
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetDirectoriesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetDirectoriesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetDirectories&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirNotFound123&quot;);
        /// 
        ///                   // Exemples
        ///                   DirectoryInfo[] result = root.GetDirectoriesWhere(x =&gt; x.Name.StartsWith(&quot;DirFizz&quot;) || x.Name.StartsWith(&quot;DirBuzz&quot;));
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="searchOption" /> is not a valid
        ///     <see cref="T:System.IO.SearchOption" /> value.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static DirectoryInfo[] GetDirectoriesWhere(this DirectoryInfo @this, String searchPattern,
            SearchOption searchOption, Func<DirectoryInfo, bool> predicate)
        {
            return Directory.EnumerateDirectories(@this.FullName, searchPattern, searchOption)
                .Select(x => new DirectoryInfo(x)).Where(x => predicate(x)).ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of directory names that match a search pattern in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">The search string to match against the names of directories in.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the directories in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetDirectoriesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetDirectoriesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetDirectories&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirNotFound123&quot;);
        /// 
        ///                   // Exemples
        ///                   DirectoryInfo[] result = root.GetDirectoriesWhere(x =&gt; x.Name.StartsWith(&quot;DirFizz&quot;) || x.Name.StartsWith(&quot;DirBuzz&quot;));
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetDirectoriesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetDirectoriesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetDirectories&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirNotFound123&quot;);
        /// 
        ///                   // Exemples
        ///                   DirectoryInfo[] result = root.GetDirectoriesWhere(x =&gt; x.Name.StartsWith(&quot;DirFizz&quot;) || x.Name.StartsWith(&quot;DirBuzz&quot;));
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static DirectoryInfo[] GetDirectoriesWhere(this DirectoryInfo @this, String[] searchPatterns,
            Func<DirectoryInfo, bool> predicate)
        {
            return searchPatterns.SelectMany(x => @this.GetDirectories(x)).Distinct().Where(x => predicate(x))
                .ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of directory names that match a search pattern in a specified @this, and
        ///     optionally searches subdirectories.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="searchOption">
        ///     One of the enumeration values that specifies whether the search operation should
        ///     include only the current directory or should include all subdirectories.The default value is
        ///     <see
        ///         cref="F:System.IO.SearchOption.TopDirectoryOnly" />
        ///     .
        /// </param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the directories in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern and option.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetDirectoriesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetDirectoriesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetDirectories&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirNotFound123&quot;);
        /// 
        ///                   // Exemples
        ///                   DirectoryInfo[] result = root.GetDirectoriesWhere(x =&gt; x.Name.StartsWith(&quot;DirFizz&quot;) || x.Name.StartsWith(&quot;DirBuzz&quot;));
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetDirectoriesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetDirectoriesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetDirectories&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirNotFound123&quot;);
        /// 
        ///                   // Exemples
        ///                   DirectoryInfo[] result = root.GetDirectoriesWhere(x =&gt; x.Name.StartsWith(&quot;DirFizz&quot;) || x.Name.StartsWith(&quot;DirBuzz&quot;));
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="searchOption" /> is not a valid
        ///     <see cref="T:System.IO.SearchOption" /> value.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static DirectoryInfo[] GetDirectoriesWhere(this DirectoryInfo @this, String[] searchPatterns,
            SearchOption searchOption, Func<DirectoryInfo, bool> predicate)
        {
            return searchPatterns.SelectMany(x => @this.GetDirectories(x, searchOption)).Distinct()
                .Where(x => predicate(x)).ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of file names that match a search pattern in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">The search string to match against the names of directories in.</param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the files in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern.
        /// </returns>
        /// ###
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static FileInfo[] GetFiles(this DirectoryInfo @this, String[] searchPatterns)
        {
            return searchPatterns.SelectMany(x => @this.GetFiles(x)).Distinct().ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of file names that match a search pattern in a specified @this, and
        ///     optionally searches subdirectories.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="searchOption">
        ///     One of the enumeration values that specifies whether the search operation should
        ///     include only the current directory or should include all subdirectories.The default value is
        ///     <see
        ///         cref="F:System.IO.SearchOption.TopDirectoryOnly" />
        ///     .
        /// </param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the files in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern and option.
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="searchOption" /> is not a valid
        ///     <see cref="T:System.IO.SearchOption" /> value.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static FileInfo[] GetFiles(this DirectoryInfo @this, String[] searchPatterns, SearchOption searchOption)
        {
            return searchPatterns.SelectMany(x => @this.GetFiles(x, searchOption)).Distinct().ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of file names in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the files in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     .
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static FileInfo[] GetFilesWhere(this DirectoryInfo @this, Func<FileInfo, bool> predicate)
        {
            return Directory.EnumerateFiles(@this.FullName).Select(x => new FileInfo(x)).Where(x => predicate(x))
                .ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of file names that match a search pattern in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the files in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFilesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetFilesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFilesWhere&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test.txt&quot;));
        ///                   var file2 = new FileInfo(Path.Combine(root.FullName, &quot;test.cs&quot;));
        ///                   var file3 = new FileInfo(Path.Combine(root.FullName, &quot;test.asp&quot;));
        ///                   file1.Create();
        ///                   file2.Create();
        ///                   file3.Create();
        /// 
        ///                   // Exemples
        ///                   FileInfo[] result = root.GetFilesWhere(x =&gt; x.Extension == &quot;.txt&quot; || x.Extension == &quot;.cs&quot;);
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFilesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetFilesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFilesWhere&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test.txt&quot;));
        ///                   var file2 = new FileInfo(Path.Combine(root.FullName, &quot;test.cs&quot;));
        ///                   var file3 = new FileInfo(Path.Combine(root.FullName, &quot;test.asp&quot;));
        ///                   file1.Create();
        ///                   file2.Create();
        ///                   file3.Create();
        /// 
        ///                   // Exemples
        ///                   FileInfo[] result = root.GetFilesWhere(x =&gt; x.Extension == &quot;.txt&quot; || x.Extension == &quot;.cs&quot;);
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static FileInfo[] GetFilesWhere(this DirectoryInfo @this, String searchPattern,
            Func<FileInfo, bool> predicate)
        {
            return Directory.EnumerateFiles(@this.FullName, searchPattern).Select(x => new FileInfo(x))
                .Where(x => predicate(x)).ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of file names that match a search pattern in a specified @this, and
        ///     optionally searches subdirectories.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="searchOption">
        ///     One of the enumeration values that specifies whether the search operation should
        ///     include only the current directory or should include all subdirectories.The default value is
        ///     <see
        ///         cref="F:System.IO.SearchOption.TopDirectoryOnly" />
        ///     .
        /// </param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the files in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern and option.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFilesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetFilesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFilesWhere&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test.txt&quot;));
        ///                   var file2 = new FileInfo(Path.Combine(root.FullName, &quot;test.cs&quot;));
        ///                   var file3 = new FileInfo(Path.Combine(root.FullName, &quot;test.asp&quot;));
        ///                   file1.Create();
        ///                   file2.Create();
        ///                   file3.Create();
        /// 
        ///                   // Exemples
        ///                   FileInfo[] result = root.GetFilesWhere(x =&gt; x.Extension == &quot;.txt&quot; || x.Extension == &quot;.cs&quot;);
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFilesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetFilesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFilesWhere&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test.txt&quot;));
        ///                   var file2 = new FileInfo(Path.Combine(root.FullName, &quot;test.cs&quot;));
        ///                   var file3 = new FileInfo(Path.Combine(root.FullName, &quot;test.asp&quot;));
        ///                   file1.Create();
        ///                   file2.Create();
        ///                   file3.Create();
        /// 
        ///                   // Exemples
        ///                   FileInfo[] result = root.GetFilesWhere(x =&gt; x.Extension == &quot;.txt&quot; || x.Extension == &quot;.cs&quot;);
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="searchOption" /> is not a valid
        ///     <see cref="T:System.IO.SearchOption" /> value.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static FileInfo[] GetFilesWhere(this DirectoryInfo @this, String searchPattern,
            SearchOption searchOption, Func<FileInfo, bool> predicate)
        {
            return Directory.EnumerateFiles(@this.FullName, searchPattern, searchOption).Select(x => new FileInfo(x))
                .Where(x => predicate(x)).ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of file names that match a search pattern in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">The search string to match against the names of directories in.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the files in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFilesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetFilesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFilesWhere&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test.txt&quot;));
        ///                   var file2 = new FileInfo(Path.Combine(root.FullName, &quot;test.cs&quot;));
        ///                   var file3 = new FileInfo(Path.Combine(root.FullName, &quot;test.asp&quot;));
        ///                   file1.Create();
        ///                   file2.Create();
        ///                   file3.Create();
        /// 
        ///                   // Exemples
        ///                   FileInfo[] result = root.GetFilesWhere(x =&gt; x.Extension == &quot;.txt&quot; || x.Extension == &quot;.cs&quot;);
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFilesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetFilesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFilesWhere&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test.txt&quot;));
        ///                   var file2 = new FileInfo(Path.Combine(root.FullName, &quot;test.cs&quot;));
        ///                   var file3 = new FileInfo(Path.Combine(root.FullName, &quot;test.asp&quot;));
        ///                   file1.Create();
        ///                   file2.Create();
        ///                   file3.Create();
        /// 
        ///                   // Exemples
        ///                   FileInfo[] result = root.GetFilesWhere(x =&gt; x.Extension == &quot;.txt&quot; || x.Extension == &quot;.cs&quot;);
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static FileInfo[] GetFilesWhere(this DirectoryInfo @this, String[] searchPatterns,
            Func<FileInfo, bool> predicate)
        {
            return searchPatterns.SelectMany(x => @this.GetFiles(x)).Distinct().Where(x => predicate(x)).ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of file names that match a search pattern in a specified @this, and
        ///     optionally searches subdirectories.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="searchOption">
        ///     One of the enumeration values that specifies whether the search operation should
        ///     include only the current directory or should include all subdirectories.The default value is
        ///     <see
        ///         cref="F:System.IO.SearchOption.TopDirectoryOnly" />
        ///     .
        /// </param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///     An enumerable collection of the full names (including paths) for the files in the directory specified by
        ///     <paramref
        ///         name="this" />
        ///     and that match the specified search pattern and option.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFilesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetFilesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFilesWhere&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test.txt&quot;));
        ///                   var file2 = new FileInfo(Path.Combine(root.FullName, &quot;test.cs&quot;));
        ///                   var file3 = new FileInfo(Path.Combine(root.FullName, &quot;test.asp&quot;));
        ///                   file1.Create();
        ///                   file2.Create();
        ///                   file3.Create();
        /// 
        ///                   // Exemples
        ///                   FileInfo[] result = root.GetFilesWhere(x =&gt; x.Extension == &quot;.txt&quot; || x.Extension == &quot;.cs&quot;);
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFilesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetFilesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFilesWhere&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        /// 
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test.txt&quot;));
        ///                   var file2 = new FileInfo(Path.Combine(root.FullName, &quot;test.cs&quot;));
        ///                   var file3 = new FileInfo(Path.Combine(root.FullName, &quot;test.asp&quot;));
        ///                   file1.Create();
        ///                   file2.Create();
        ///                   file3.Create();
        /// 
        ///                   // Exemples
        ///                   FileInfo[] result = root.GetFilesWhere(x =&gt; x.Extension == &quot;.txt&quot; || x.Extension == &quot;.cs&quot;);
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="searchOption" /> is not a valid
        ///     <see cref="T:System.IO.SearchOption" /> value.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static FileInfo[] GetFilesWhere(this DirectoryInfo @this, String[] searchPatterns,
            SearchOption searchOption, Func<FileInfo, bool> predicate)
        {
            return searchPatterns.SelectMany(x => @this.GetFiles(x, searchOption)).Distinct().Where(x => predicate(x))
                .ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of file-system entries in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <returns>
        ///     An enumerable collection of file-system entries in the directory specified by <paramref name="this" />.
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static string[] GetFileSystemEntries(this DirectoryInfo @this)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName).ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of file-system entries that match a search pattern in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <returns>
        ///     An enumerable collection of file-system entries in the directory specified by <paramref name="this" /> and
        ///     that match the specified search pattern.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFileSystemEntries
        ///           {
        ///               [TestMethod]
        ///               public void GetFileSystemEntries()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFileSystemEntries&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        /// 
        ///                   // Exemples
        ///                   string[] result = root.GetFileSystemEntries(new[] {&quot;DirFizz*&quot;, &quot;*.txt&quot;});
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFileSystemEntries
        ///           {
        ///               [TestMethod]
        ///               public void GetFileSystemEntries()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFileSystemEntries&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        /// 
        ///                   // Exemples
        ///                   string[] result = root.GetFileSystemEntries(new[] {&quot;DirFizz*&quot;, &quot;*.txt&quot;});
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static string[] GetFileSystemEntries(this DirectoryInfo @this, String searchPattern)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName, searchPattern).ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of file names and directory names that match a search pattern in a specified
        ///     @this, and optionally searches subdirectories.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="searchOption">
        ///     One of the enumeration values  that specifies whether the search operation should
        ///     include only the current directory or should include all subdirectories.The default value is
        ///     <see
        ///         cref="F:System.IO.SearchOption.TopDirectoryOnly" />
        ///     .
        /// </param>
        /// <returns>
        ///     An enumerable collection of file-system entries in the directory specified by <paramref name="this" /> and
        ///     that match the specified search pattern and option.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFileSystemEntries
        ///           {
        ///               [TestMethod]
        ///               public void GetFileSystemEntries()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFileSystemEntries&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        /// 
        ///                   // Exemples
        ///                   string[] result = root.GetFileSystemEntries(new[] {&quot;DirFizz*&quot;, &quot;*.txt&quot;});
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFileSystemEntries
        ///           {
        ///               [TestMethod]
        ///               public void GetFileSystemEntries()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFileSystemEntries&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        /// 
        ///                   // Exemples
        ///                   string[] result = root.GetFileSystemEntries(new[] {&quot;DirFizz*&quot;, &quot;*.txt&quot;});
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="searchOption" /> is not a valid
        ///     <see cref="T:System.IO.SearchOption" /> value.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static string[] GetFileSystemEntries(this DirectoryInfo @this, String searchPattern,
            SearchOption searchOption)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName, searchPattern, searchOption).ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of file-system entries that match a search pattern in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">The search string to match against the names of directories in.</param>
        /// <returns>
        ///     An enumerable collection of file-system entries in the directory specified by <paramref name="this" /> and
        ///     that match the specified search pattern.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFileSystemEntries
        ///           {
        ///               [TestMethod]
        ///               public void GetFileSystemEntries()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFileSystemEntries&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        /// 
        ///                   // Exemples
        ///                   string[] result = root.GetFileSystemEntries(new[] {&quot;DirFizz*&quot;, &quot;*.txt&quot;});
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFileSystemEntries
        ///           {
        ///               [TestMethod]
        ///               public void GetFileSystemEntries()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFileSystemEntries&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        /// 
        ///                   // Exemples
        ///                   string[] result = root.GetFileSystemEntries(new[] {&quot;DirFizz*&quot;, &quot;*.txt&quot;});
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static string[] GetFileSystemEntries(this DirectoryInfo @this, String[] searchPatterns)
        {
            return searchPatterns.SelectMany(x => Directory.EnumerateFileSystemEntries(@this.FullName, x)).Distinct()
                .ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of file names and directory names that match a search pattern in a specified
        ///     @this, and optionally searches subdirectories.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="searchOption">
        ///     One of the enumeration values  that specifies whether the search operation should
        ///     include only the current directory or should include all subdirectories.The default value is
        ///     <see
        ///         cref="F:System.IO.SearchOption.TopDirectoryOnly" />
        ///     .
        /// </param>
        /// <returns>
        ///     An enumerable collection of file-system entries in the directory specified by <paramref name="this" /> and
        ///     that match the specified search pattern and option.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFileSystemEntries
        ///           {
        ///               [TestMethod]
        ///               public void GetFileSystemEntries()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFileSystemEntries&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        /// 
        ///                   // Exemples
        ///                   string[] result = root.GetFileSystemEntries(new[] {&quot;DirFizz*&quot;, &quot;*.txt&quot;});
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFileSystemEntries
        ///           {
        ///               [TestMethod]
        ///               public void GetFileSystemEntries()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFileSystemEntries&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        /// 
        ///                   // Exemples
        ///                   string[] result = root.GetFileSystemEntries(new[] {&quot;DirFizz*&quot;, &quot;*.txt&quot;});
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="searchOption" /> is not a valid
        ///     <see cref="T:System.IO.SearchOption" /> value.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static string[] GetFileSystemEntries(this DirectoryInfo @this, String[] searchPatterns,
            SearchOption searchOption)
        {
            return searchPatterns.SelectMany(x => Directory.EnumerateFileSystemEntries(@this.FullName, x, searchOption))
                .Distinct().ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of file-system entries in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///     An enumerable collection of file-system entries in the directory specified by <paramref name="this" />.
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static string[] GetFileSystemEntriesWhere(this DirectoryInfo @this, Func<string, bool> predicate)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName).Where(x => predicate(x)).ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of file-system entries that match a search pattern in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///     An enumerable collection of file-system entries in the directory specified by <paramref name="this" /> and
        ///     that match the specified search pattern.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFileSystemEntriesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetFileSystemEntriesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFileSystemEntriesWhere&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        /// 
        ///                   // Exemples
        ///                   string[] result = root.GetFileSystemEntriesWhere(x =&gt; x.Contains(&quot;DirFizz&quot;) || x.EndsWith(&quot;.txt&quot;));
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFileSystemEntriesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetFileSystemEntriesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFileSystemEntriesWhere&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        /// 
        ///                   // Exemples
        ///                   string[] result = root.GetFileSystemEntriesWhere(x =&gt; x.Contains(&quot;DirFizz&quot;) || x.EndsWith(&quot;.txt&quot;));
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static string[] GetFileSystemEntriesWhere(this DirectoryInfo @this, String searchPattern,
            Func<string, bool> predicate)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName, searchPattern).Where(x => predicate(x))
                .ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of file names and directory names that match a search pattern in a specified
        ///     @this, and optionally searches subdirectories.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="searchOption">
        ///     One of the enumeration values  that specifies whether the search operation should
        ///     include only the current directory or should include all subdirectories.The default value is
        ///     <see
        ///         cref="F:System.IO.SearchOption.TopDirectoryOnly" />
        ///     .
        /// </param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///     An enumerable collection of file-system entries in the directory specified by <paramref name="this" /> and
        ///     that match the specified search pattern and option.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFileSystemEntriesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetFileSystemEntriesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFileSystemEntriesWhere&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        /// 
        ///                   // Exemples
        ///                   string[] result = root.GetFileSystemEntriesWhere(x =&gt; x.Contains(&quot;DirFizz&quot;) || x.EndsWith(&quot;.txt&quot;));
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFileSystemEntriesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetFileSystemEntriesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFileSystemEntriesWhere&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        /// 
        ///                   // Exemples
        ///                   string[] result = root.GetFileSystemEntriesWhere(x =&gt; x.Contains(&quot;DirFizz&quot;) || x.EndsWith(&quot;.txt&quot;));
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="searchOption" /> is not a valid
        ///     <see cref="T:System.IO.SearchOption" /> value.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static string[] GetFileSystemEntriesWhere(this DirectoryInfo @this, String searchPattern,
            SearchOption searchOption, Func<string, bool> predicate)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName, searchPattern, searchOption)
                .Where(x => predicate(x)).ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of file-system entries that match a search pattern in a specified @this.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">The search string to match against the names of directories in.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///     An enumerable collection of file-system entries in the directory specified by <paramref name="this" /> and
        ///     that match the specified search pattern.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFileSystemEntriesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetFileSystemEntriesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFileSystemEntriesWhere&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        /// 
        ///                   // Exemples
        ///                   string[] result = root.GetFileSystemEntriesWhere(x =&gt; x.Contains(&quot;DirFizz&quot;) || x.EndsWith(&quot;.txt&quot;));
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFileSystemEntriesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetFileSystemEntriesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFileSystemEntriesWhere&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        /// 
        ///                   // Exemples
        ///                   string[] result = root.GetFileSystemEntriesWhere(x =&gt; x.Contains(&quot;DirFizz&quot;) || x.EndsWith(&quot;.txt&quot;));
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <param name="searchPattern">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static string[] GetFileSystemEntriesWhere(this DirectoryInfo @this, String[] searchPatterns,
            Func<string, bool> predicate)
        {
            return searchPatterns.SelectMany(x => Directory.EnumerateFileSystemEntries(@this.FullName, x)).Distinct()
                .Where(x => predicate(x)).ToArray();
        }

        /// <summary>
        ///     Returns an enumerable collection of file names and directory names that match a search pattern in a specified
        ///     @this, and optionally searches subdirectories.
        /// </summary>
        /// <param name="this">The directory to search.</param>
        /// <param name="searchPatterns">
        ///     The search string to match against the names of directories in
        ///     <paramref name="this" />.
        /// </param>
        /// <param name="searchOption">
        ///     One of the enumeration values  that specifies whether the search operation should
        ///     include only the current directory or should include all subdirectories.The default value is
        ///     <see
        ///         cref="F:System.IO.SearchOption.TopDirectoryOnly" />
        ///     .
        /// </param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///     An enumerable collection of file-system entries in the directory specified by <paramref name="this" /> and
        ///     that match the specified search pattern and option.
        /// </returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFileSystemEntriesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetFileSystemEntriesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFileSystemEntriesWhere&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        /// 
        ///                   // Exemples
        ///                   string[] result = root.GetFileSystemEntriesWhere(x =&gt; x.Contains(&quot;DirFizz&quot;) || x.EndsWith(&quot;.txt&quot;));
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using System.IO;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_IO_DirectoryInfo_GetFileSystemEntriesWhere
        ///           {
        ///               [TestMethod]
        ///               public void GetFileSystemEntriesWhere()
        ///               {
        ///                   // Type
        ///                   var root = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, &quot;System_IO_DirectoryInfo_GetFileSystemEntriesWhere&quot;));
        ///                   Directory.CreateDirectory(root.FullName);
        ///                   root.CreateSubdirectory(&quot;DirFizz123&quot;);
        ///                   root.CreateSubdirectory(&quot;DirBuzz123&quot;);
        ///                   var file1 = new FileInfo(Path.Combine(root.FullName, &quot;test1.txt&quot;));
        ///                   file1.Create();
        /// 
        ///                   // Exemples
        ///                   string[] result = root.GetFileSystemEntriesWhere(x =&gt; x.Contains(&quot;DirFizz&quot;) || x.EndsWith(&quot;.txt&quot;));
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(2, result.Length);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this " />is a zero-length string, contains only
        ///     white space, or contains invalid characters as defined by
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .- or -<paramref name="searchPattern" /> does not contain a valid pattern.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.-or-
        ///     <paramref name="searchPattern" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="searchOption" /> is not a valid
        ///     <see cref="T:System.IO.SearchOption" /> value.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid, such as
        ///     referring to an unmapped drive.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     <paramref name="this" /> is a file name.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or combined exceed the
        ///     system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters
        ///     and file names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        public static string[] GetFileSystemEntriesWhere(this DirectoryInfo @this, String[] searchPatterns,
            SearchOption searchOption, Func<string, bool> predicate)
        {
            return searchPatterns.SelectMany(x => Directory.EnumerateFileSystemEntries(@this.FullName, x, searchOption))
                .Distinct().Where(x => predicate(x)).ToArray();
        }

        /// <summary>
        ///     A DirectoryInfo extension method that gets a size.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The size.</returns>
        public static long GetSize(this DirectoryInfo @this)
        {
            return @this.GetFiles("*.*", SearchOption.AllDirectories).Sum(x => x.Length);
        }

        /// <summary>
        ///     Combines multiples string into a path.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="paths">A variable-length parameters list containing paths.</param>
        /// <returns>
        ///     The combined paths. If one of the specified paths is a zero-length string, this method returns the other path.
        /// </returns>
        public static string PathCombine(this DirectoryInfo @this, params string[] paths)
        {
            var list = paths.ToList();
            list.Insert(0, @this.FullName);
            return Path.Combine(list.ToArray());
        }

        /// <summary>
        ///     Combines multiples string into a path.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="paths">A variable-length parameters list containing paths.</param>
        /// <returns>
        ///     The combined paths as a DirectoryInfo. If one of the specified paths is a zero-length string, this method
        ///     returns the other path.
        /// </returns>
        public static DirectoryInfo PathCombineDirectory(this DirectoryInfo @this, params string[] paths)
        {
            var list = paths.ToList();
            list.Insert(0, @this.FullName);
            return new DirectoryInfo(Path.Combine(list.ToArray()));
        }

        /// <summary>
        ///     Combines multiples string into a path.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="paths">A variable-length parameters list containing paths.</param>
        /// <returns>
        ///     The combined paths as a FileInfo. If one of the specified paths is a zero-length string, this method returns
        ///     the other path.
        /// </returns>
        public static FileInfo PathCombineFile(this DirectoryInfo @this, params string[] paths)
        {
            var list = paths.ToList();
            list.Insert(0, @this.FullName);
            return new FileInfo(Path.Combine(list.ToArray()));
        }
    }
}