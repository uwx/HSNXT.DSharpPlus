// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;

public static partial class Extensions
{
    /// <summary>
    ///     An Int64 extension method that query if '@this' is prime.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if prime, false if not.</returns>
    public static bool IsPrime(this Int64 @this)
    {
        if (@this == 1 || @this == 2)
        {
            return true;
        }

        if (@this%2 == 0)
        {
            return false;
        }

        var sqrt = (Int64) Math.Sqrt(@this);
        for (Int64 t = 3; t <= sqrt; t = t + 2)
        {
            if (@this%t == 0)
            {
                return false;
            }
        }

        return true;
    }
}