// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

using System;

namespace TestProj47
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the larger of two 32-bit signed integers.
        /// </summary>
        /// <param name="val1">The first of two 32-bit signed integers to compare.</param>
        /// <param name="val2">The second of two 32-bit signed integers to compare.</param>
        /// <returns>Parameter  or , whichever is larger.</returns>
        public static Int32 Max(this Int32 val1, Int32 val2)
        {
            return Math.Max(val1, val2);
        }
    }
}