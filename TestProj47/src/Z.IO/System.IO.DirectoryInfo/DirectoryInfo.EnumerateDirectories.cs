// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static partial class Extensions
{
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
    public static IEnumerable<DirectoryInfo> EnumerateDirectories(this DirectoryInfo @this, String searchPattern, SearchOption searchOption)
    {
        return Directory.EnumerateDirectories(@this.FullName, searchPattern, searchOption).Select(x => new DirectoryInfo(x));
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
    public static IEnumerable<DirectoryInfo> EnumerateDirectories(this DirectoryInfo @this, String[] searchPatterns, SearchOption searchOption)
    {
        return searchPatterns.SelectMany(x => @this.GetDirectories(x, searchOption)).Distinct();
    }
}