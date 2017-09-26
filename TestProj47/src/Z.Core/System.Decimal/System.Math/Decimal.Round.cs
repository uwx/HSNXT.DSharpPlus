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
    ///     Rounds a decimal value to the nearest integral value.
    /// </summary>
    /// <param name="d">A decimal number to be rounded.</param>
    /// <returns>
    ///     The integer nearest parameter . If the fractional component of  is halfway between two integers, one of which
    ///     is even and the other odd, the even number is returned. Note that this method returns a  instead of an
    ///     integral type.
    /// </returns>
    public static Decimal Round(this Decimal d)
    {
        return Math.Round(d);
    }

    /// <summary>
    ///     Rounds a decimal value to a specified number of fractional digits.
    /// </summary>
    /// <param name="d">A decimal number to be rounded.</param>
    /// <param name="decimals">The number of decimal places in the return value.</param>
    /// <returns>The number nearest to  that contains a number of fractional digits equal to .</returns>
    public static Decimal Round(this Decimal d, Int32 decimals)
    {
        return Math.Round(d, decimals);
    }

    /// <summary>
    ///     Rounds a decimal value to the nearest integer. A parameter specifies how to round the value if it is midway
    ///     between two numbers.
    /// </summary>
    /// <param name="d">A decimal number to be rounded.</param>
    /// <param name="mode">Specification for how to round  if it is midway between two other numbers.</param>
    /// <returns>
    ///     The integer nearest . If  is halfway between two numbers, one of which is even and the other odd, then
    ///     determines which of the two is returned.
    /// </returns>
    public static Decimal Round(this Decimal d, MidpointRounding mode)
    {
        return Math.Round(d, mode);
    }

    /// <summary>
    ///     Rounds a decimal value to a specified number of fractional digits. A parameter specifies how to round the
    ///     value if it is midway between two numbers.
    /// </summary>
    /// <param name="d">A decimal number to be rounded.</param>
    /// <param name="decimals">The number of decimal places in the return value.</param>
    /// <param name="mode">Specification for how to round  if it is midway between two other numbers.</param>
    /// <returns>
    ///     The number nearest to  that contains a number of fractional digits equal to . If  has fewer fractional digits
    ///     than ,  is returned unchanged.
    /// </returns>
    public static Decimal Round(this Decimal d, Int32 decimals, MidpointRounding mode)
    {
        return Math.Round(d, decimals, mode);
    }
}