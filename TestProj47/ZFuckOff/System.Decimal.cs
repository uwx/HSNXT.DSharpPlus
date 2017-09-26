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
        ///     A Decimal extension method that converts the @this to a money.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a Decimal.</returns>
        public static Decimal ToMoney(this Decimal @this)
        {
            return Math.Round(@this, 2);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Divides two specified  values.
        /// </summary>
        /// <param name="d1">The dividend.</param>
        /// <param name="d2">The divisor.</param>
        /// <returns>The result of dividing  by .</returns>
        public static Decimal Divide(this Decimal d1, Decimal d2)
        {
            return Decimal.Divide(d1, d2);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the value of a specified instance of  to its equivalent binary representation.
        /// </summary>
        /// <param name="d">The value to convert.</param>
        /// <returns>A 32-bit signed integer array with four elements that contain the binary representation of .</returns>
        public static Int32[] GetBits(this Decimal d)
        {
            return Decimal.GetBits(d);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Multiplies two specified  values.
        /// </summary>
        /// <param name="d1">The multiplicand.</param>
        /// <param name="d2">The multiplier.</param>
        /// <returns>The result of multiplying  and .</returns>
        public static Decimal Multiply(this Decimal d1, Decimal d2)
        {
            return Decimal.Multiply(d1, d2);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the result of multiplying the specified  value by negative one.
        /// </summary>
        /// <param name="d">The value to negate.</param>
        /// <returns>A decimal number with the value of , but the opposite sign.-or- Zero, if  is zero.</returns>
        public static Decimal Negate(this Decimal d)
        {
            return Decimal.Negate(d);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Computes the remainder after dividing two  values.
        /// </summary>
        /// <param name="d1">The dividend.</param>
        /// <param name="d2">The divisor.</param>
        /// <returns>The remainder after dividing  by .</returns>
        public static Decimal Remainder(this Decimal d1, Decimal d2)
        {
            return Decimal.Remainder(d1, d2);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Subtracts one specified  value from another.
        /// </summary>
        /// <param name="d1">The minuend.</param>
        /// <param name="d2">The subtrahend.</param>
        /// <returns>The result of subtracting  from .</returns>
        public static Decimal Subtract(this Decimal d1, Decimal d2)
        {
            return Decimal.Subtract(d1, d2);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the value of the specified  to the equivalent 8-bit unsigned integer.
        /// </summary>
        /// <param name="value">The decimal number to convert.</param>
        /// <returns>An 8-bit unsigned integer equivalent to .</returns>
        public static Byte ToByte(this Decimal value)
        {
            return Decimal.ToByte(value);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the value of the specified  to the equivalent double-precision floating-point number.
        /// </summary>
        /// <param name="d">The decimal number to convert.</param>
        /// <returns>A double-precision floating-point number equivalent to .</returns>
        public static Double ToDoubleZ(this Decimal d)
        {
            return Decimal.ToDouble(d);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the value of the specified  to the equivalent 16-bit signed integer.
        /// </summary>
        /// <param name="value">The decimal number to convert.</param>
        /// <returns>A 16-bit signed integer equivalent to .</returns>
        public static Int16 ToInt16(this Decimal value)
        {
            return Decimal.ToInt16(value);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the value of the specified  to the equivalent 32-bit signed integer.
        /// </summary>
        /// <param name="d">The decimal number to convert.</param>
        /// <returns>A 32-bit signed integer equivalent to the value of .</returns>
        public static Int32 ToInt32(this Decimal d)
        {
            return Decimal.ToInt32(d);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the value of the specified  to the equivalent 64-bit signed integer.
        /// </summary>
        /// <param name="d">The decimal number to convert.</param>
        /// <returns>A 64-bit signed integer equivalent to the value of .</returns>
        public static Int64 ToInt64(this Decimal d)
        {
            return Decimal.ToInt64(d);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the specified  value to the equivalent OLE Automation Currency value, which is contained in a 64-bit
        ///     signed integer.
        /// </summary>
        /// <param name="value">The decimal number to convert.</param>
        /// <returns>A 64-bit signed integer that contains the OLE Automation equivalent of .</returns>
        public static Int64 ToOACurrency(this Decimal value)
        {
            return Decimal.ToOACurrency(value);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the value of the specified  to the equivalent 8-bit signed integer.
        /// </summary>
        /// <param name="value">The decimal number to convert.</param>
        /// <returns>An 8-bit signed integer equivalent to .</returns>
        public static SByte ToSByte(this Decimal value)
        {
            return Decimal.ToSByte(value);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the value of the specified  to the equivalent single-precision floating-point number.
        /// </summary>
        /// <param name="d">The decimal number to convert.</param>
        /// <returns>A single-precision floating-point number equivalent to the value of .</returns>
        public static Single ToSingle(this Decimal d)
        {
            return Decimal.ToSingle(d);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the value of the specified  to the equivalent 16-bit unsigned integer.
        /// </summary>
        /// <param name="value">The decimal number to convert.</param>
        /// <returns>A 16-bit unsigned integer equivalent to the value of .</returns>
        public static UInt16 ToUInt16(this Decimal value)
        {
            return Decimal.ToUInt16(value);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the value of the specified  to the equivalent 32-bit unsigned integer.
        /// </summary>
        /// <param name="d">The decimal number to convert.</param>
        /// <returns>A 32-bit unsigned integer equivalent to the value of .</returns>
        public static UInt32 ToUInt32(this Decimal d)
        {
            return Decimal.ToUInt32(d);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the value of the specified  to the equivalent 64-bit unsigned integer.
        /// </summary>
        /// <param name="d">The decimal number to convert.</param>
        /// <returns>A 64-bit unsigned integer equivalent to the value of .</returns>
        public static UInt64 ToUInt64(this Decimal d)
        {
            return Decimal.ToUInt64(d);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the absolute value of a  number.
        /// </summary>
        /// <param name="value">A number that is greater than or equal to , but less than or equal to .</param>
        /// <returns>A decimal number, x, such that 0 ? x ?.</returns>
        public static Decimal Abs(this Decimal value)
        {
            return Math.Abs(value);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the smallest integral value that is greater than or equal to the specified decimal number.
        /// </summary>
        /// <param name="d">A decimal number.</param>
        /// <returns>
        ///     The smallest integral value that is greater than or equal to . Note that this method returns a  instead of an
        ///     integral type.
        /// </returns>
        public static Decimal Ceiling(this Decimal d)
        {
            return Math.Ceiling(d);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the largest integer less than or equal to the specified decimal number.
        /// </summary>
        /// <param name="d">A decimal number.</param>
        /// <returns>
        ///     The largest integer less than or equal to .  Note that the method returns an integral value of type .
        /// </returns>
        public static Decimal Floor(this Decimal d)
        {
            return Math.Floor(d);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the larger of two decimal numbers.
        /// </summary>
        /// <param name="val1">The first of two decimal numbers to compare.</param>
        /// <param name="val2">The second of two decimal numbers to compare.</param>
        /// <returns>Parameter  or , whichever is larger.</returns>
        public static Decimal Max(this Decimal val1, Decimal val2)
        {
            return Math.Max(val1, val2);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the smaller of two decimal numbers.
        /// </summary>
        /// <param name="val1">The first of two decimal numbers to compare.</param>
        /// <param name="val2">The second of two decimal numbers to compare.</param>
        /// <returns>Parameter  or , whichever is smaller.</returns>
        public static Decimal Min(this Decimal val1, Decimal val2)
        {
            return Math.Min(val1, val2);
        }
    }

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

    public static partial class Extensions
    {
        /// <summary>
        ///     Returns a value indicating the sign of a decimal number.
        /// </summary>
        /// <param name="value">A signed decimal number.</param>
        /// <returns>
        ///     A number that indicates the sign of , as shown in the following table.Return value Meaning -1  is less than
        ///     zero. 0  is equal to zero. 1  is greater than zero.
        /// </returns>
        public static Int32 Sign(this Decimal value)
        {
            return Math.Sign(value);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Calculates the integral part of a specified decimal number.
        /// </summary>
        /// <param name="d">A number to truncate.</param>
        /// <returns>
        ///     The integral part of ; that is, the number that remains after any fractional digits have been discarded.
        /// </returns>
        public static Decimal TruncateZ(this Decimal d)
        {
            return Math.Truncate(d);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
        /// ###
        /// <typeparam name="T">Generic type parameter.</typeparam>
        public static bool Between(this Decimal @this, Decimal minValue, Decimal maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A T extension method to determines whether the object is equal to any of the provided values.
        /// </summary>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list contains the object, else false.</returns>
        /// ###
        /// <typeparam name="T">Generic type parameter.</typeparam>
        public static bool InZ(this Decimal @this, params Decimal[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A T extension method that check if the value is between inclusively the minValue and maxValue.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>true if the value is between inclusively the minValue and maxValue, otherwise false.</returns>
        /// ###
        /// <typeparam name="T">Generic type parameter.</typeparam>
        public static bool InRange(this Decimal @this, Decimal minValue, Decimal maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A T extension method to determines whether the object is not equal to any of the provided values.
        /// </summary>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list doesn't contains the object, else false.</returns>
        /// ###
        /// <typeparam name="T">Generic type parameter.</typeparam>
        public static bool NotIn(this Decimal @this, params Decimal[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
    }
}