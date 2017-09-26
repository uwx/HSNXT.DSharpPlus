// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

using System;
using System.IO;

namespace TestProj47
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A FileInfo extension method that gets directory name.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The directory name.</returns>
        public static String GetDirectoryName(this FileInfo @this)
        {
            return @this.Directory.Name;
        }
    }
}