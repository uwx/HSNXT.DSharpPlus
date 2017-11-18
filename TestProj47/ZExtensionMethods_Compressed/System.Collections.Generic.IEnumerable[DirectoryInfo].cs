using System;
using System.Collections.Generic;
using System.IO;

//using System.Data.SqlServerCe;

// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System.Collections.Generic;
//using System.IO;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     An IEnumerable&lt;DirectoryInfo&gt; extension method that deletes the given @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void Delete(this IEnumerable<DirectoryInfo> @this)
        {
            foreach (var t in @this)
            {
                t.Delete();
            }
        }

        /// <summary>
        ///     Enumerates for each in this collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="action">The action.</param>
        /// <returns>An enumerator that allows foreach to be used to process for each in this collection.</returns>
        public static IEnumerable<DirectoryInfo> ForEach(this IEnumerable<DirectoryInfo> @this,
            Action<DirectoryInfo> action)
        {
            foreach (var t in @this)
            {
                action(t);
            }
            return @this;
        }
    }
}