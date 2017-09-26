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
        ///     Returns a value indicating whether the specified number evaluates to positive infinity.
        /// </summary>
        /// <param name="f">A single-precision floating-point number.</param>
        /// <returns>true if  evaluates to ; otherwise, false.</returns>
        public static Boolean IsPositiveInfinity(this Single f)
        {
            return Single.IsPositiveInfinity(f);
        }
    }
}