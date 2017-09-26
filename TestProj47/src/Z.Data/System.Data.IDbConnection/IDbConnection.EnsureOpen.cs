// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System.Data;

public static partial class Extensions
{
    /// <summary>
    ///     An IDbConnection extension method that ensures that open.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    public static void EnsureOpen(this IDbConnection @this)
    {
        if (@this.State == ConnectionState.Closed)
        {
            @this.Open();
        }
    }
}