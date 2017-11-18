using System;

//using System.Data.SqlServerCe;

// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A Decimal extension method that converts the @this to a money.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a Decimal.</returns>
        public static decimal ToMoney(this decimal @this)
        {
            return Math.Round(@this, 2);
        }

        /// <summary>
        ///     Divides two specified  values.
        /// </summary>
        /// <param name="d1">The dividend.</param>
        /// <param name="d2">The divisor.</param>
        /// <returns>The result of dividing  by .</returns>
        public static decimal Divide(this decimal d1, decimal d2)
        {
            return decimal.Divide(d1, d2);
        }

        /// <summary>
        ///     Converts the value of a specified instance of  to its equivalent binary representation.
        /// </summary>
        /// <param name="d">The value to convert.</param>
        /// <returns>A 32-bit signed integer array with four elements that contain the binary representation of .</returns>
        public static int[] GetBits(this decimal d)
        {
            return decimal.GetBits(d);
        }

        /// <summary>
        ///     Multiplies two specified  values.
        /// </summary>
        /// <param name="d1">The multiplicand.</param>
        /// <param name="d2">The multiplier.</param>
        /// <returns>The result of multiplying  and .</returns>
        public static decimal Multiply(this decimal d1, decimal d2)
        {
            return decimal.Multiply(d1, d2);
        }

        /// <summary>
        ///     Returns the result of multiplying the specified  value by negative one.
        /// </summary>
        /// <param name="d">The value to negate.</param>
        /// <returns>A decimal number with the value of , but the opposite sign.-or- Zero, if  is zero.</returns>
        public static decimal Negate(this decimal d)
        {
            return decimal.Negate(d);
        }

        /// <summary>
        ///     Computes the remainder after dividing two  values.
        /// </summary>
        /// <param name="d1">The dividend.</param>
        /// <param name="d2">The divisor.</param>
        /// <returns>The remainder after dividing  by .</returns>
        public static decimal Remainder(this decimal d1, decimal d2)
        {
            return decimal.Remainder(d1, d2);
        }

        /// <summary>
        ///     Subtracts one specified  value from another.
        /// </summary>
        /// <param name="d1">The minuend.</param>
        /// <param name="d2">The subtrahend.</param>
        /// <returns>The result of subtracting  from .</returns>
        public static decimal Subtract(this decimal d1, decimal d2)
        {
            return decimal.Subtract(d1, d2);
        }

        /// <summary>
        ///     Converts the value of the specified  to the equivalent 8-bit unsigned integer.
        /// </summary>
        /// <param name="value">The decimal number to convert.</param>
        /// <returns>An 8-bit unsigned integer equivalent to .</returns>
        public static byte ToByte(this decimal value)
        {
            return decimal.ToByte(value);
        }

        /// <summary>
        ///     Converts the value of the specified  to the equivalent double-precision floating-point number.
        /// </summary>
        /// <param name="d">The decimal number to convert.</param>
        /// <returns>A double-precision floating-point number equivalent to .</returns>
        public static double ToDoubleZ(this decimal d)
        {
            return decimal.ToDouble(d);
        }

        /// <summary>
        ///     Converts the value of the specified  to the equivalent 16-bit signed integer.
        /// </summary>
        /// <param name="value">The decimal number to convert.</param>
        /// <returns>A 16-bit signed integer equivalent to .</returns>
        public static short ToInt16(this decimal value)
        {
            return decimal.ToInt16(value);
        }

        /// <summary>
        ///     Converts the value of the specified  to the equivalent 32-bit signed integer.
        /// </summary>
        /// <param name="d">The decimal number to convert.</param>
        /// <returns>A 32-bit signed integer equivalent to the value of .</returns>
        public static int ToInt32(this decimal d)
        {
            return decimal.ToInt32(d);
        }

        /// <summary>
        ///     Converts the value of the specified  to the equivalent 64-bit signed integer.
        /// </summary>
        /// <param name="d">The decimal number to convert.</param>
        /// <returns>A 64-bit signed integer equivalent to the value of .</returns>
        public static long ToInt64(this decimal d)
        {
            return decimal.ToInt64(d);
        }

        /// <summary>
        ///     Converts the specified  value to the equivalent OLE Automation Currency value, which is contained in a 64-bit
        ///     signed integer.
        /// </summary>
        /// <param name="value">The decimal number to convert.</param>
        /// <returns>A 64-bit signed integer that contains the OLE Automation equivalent of .</returns>
        public static long ToOaCurrency(this decimal value)
        {
            return decimal.ToOACurrency(value);
        }

        /// <summary>
        ///     Converts the value of the specified  to the equivalent 8-bit signed integer.
        /// </summary>
        /// <param name="value">The decimal number to convert.</param>
        /// <returns>An 8-bit signed integer equivalent to .</returns>
        public static sbyte ToSByte(this decimal value)
        {
            return decimal.ToSByte(value);
        }

        /// <summary>
        ///     Converts the value of the specified  to the equivalent single-precision floating-point number.
        /// </summary>
        /// <param name="d">The decimal number to convert.</param>
        /// <returns>A single-precision floating-point number equivalent to the value of .</returns>
        public static float ToSingle(this decimal d)
        {
            return decimal.ToSingle(d);
        }

        /// <summary>
        ///     Converts the value of the specified  to the equivalent 16-bit unsigned integer.
        /// </summary>
        /// <param name="value">The decimal number to convert.</param>
        /// <returns>A 16-bit unsigned integer equivalent to the value of .</returns>
        public static ushort ToUInt16(this decimal value)
        {
            return decimal.ToUInt16(value);
        }

        /// <summary>
        ///     Converts the value of the specified  to the equivalent 32-bit unsigned integer.
        /// </summary>
        /// <param name="d">The decimal number to convert.</param>
        /// <returns>A 32-bit unsigned integer equivalent to the value of .</returns>
        public static uint ToUInt32(this decimal d)
        {
            return decimal.ToUInt32(d);
        }

        /// <summary>
        ///     Converts the value of the specified  to the equivalent 64-bit unsigned integer.
        /// </summary>
        /// <param name="d">The decimal number to convert.</param>
        /// <returns>A 64-bit unsigned integer equivalent to the value of .</returns>
        public static ulong ToUInt64(this decimal d)
        {
            return decimal.ToUInt64(d);
        }

        /// <summary>
        ///     Returns the absolute value of a  number.
        /// </summary>
        /// <param name="value">A number that is greater than or equal to , but less than or equal to .</param>
        /// <returns>A decimal number, x, such that 0 ? x ?.</returns>
        public static decimal Abs(this decimal value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        ///     Returns the smallest integral value that is greater than or equal to the specified decimal number.
        /// </summary>
        /// <param name="d">A decimal number.</param>
        /// <returns>
        ///     The smallest integral value that is greater than or equal to . Note that this method returns a  instead of an
        ///     integral type.
        /// </returns>
        public static decimal Ceiling(this decimal d)
        {
            return Math.Ceiling(d);
        }

        /// <summary>
        ///     Returns the largest integer less than or equal to the specified decimal number.
        /// </summary>
        /// <param name="d">A decimal number.</param>
        /// <returns>
        ///     The largest integer less than or equal to .  Note that the method returns an integral value of type .
        /// </returns>
        public static decimal Floor(this decimal d)
        {
            return Math.Floor(d);
        }

        /// <summary>
        ///     Returns the larger of two decimal numbers.
        /// </summary>
        /// <param name="val1">The first of two decimal numbers to compare.</param>
        /// <param name="val2">The second of two decimal numbers to compare.</param>
        /// <returns>Parameter  or , whichever is larger.</returns>
        public static decimal Max(this decimal val1, decimal val2)
        {
            return Math.Max(val1, val2);
        }

        /// <summary>
        ///     Returns the smaller of two decimal numbers.
        /// </summary>
        /// <param name="val1">The first of two decimal numbers to compare.</param>
        /// <param name="val2">The second of two decimal numbers to compare.</param>
        /// <returns>Parameter  or , whichever is smaller.</returns>
        public static decimal Min(this decimal val1, decimal val2)
        {
            return Math.Min(val1, val2);
        }

        /// <summary>
        ///     Rounds a decimal value to the nearest integral value.
        /// </summary>
        /// <param name="d">A decimal number to be rounded.</param>
        /// <returns>
        ///     The integer nearest parameter . If the fractional component of  is halfway between two integers, one of which
        ///     is even and the other odd, the even number is returned. Note that this method returns a  instead of an
        ///     integral type.
        /// </returns>
        public static decimal Round(this decimal d)
        {
            return Math.Round(d);
        }

        /// <summary>
        ///     Rounds a decimal value to a specified number of fractional digits.
        /// </summary>
        /// <param name="d">A decimal number to be rounded.</param>
        /// <param name="decimals">The number of decimal places in the return value.</param>
        /// <returns>The number nearest to  that contains a number of fractional digits equal to .</returns>
        public static decimal Round(this decimal d, int decimals)
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
        public static decimal Round(this decimal d, MidpointRounding mode)
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
        public static decimal Round(this decimal d, int decimals, MidpointRounding mode)
        {
            return Math.Round(d, decimals, mode);
        }

        /// <summary>
        ///     Returns a value indicating the sign of a decimal number.
        /// </summary>
        /// <param name="value">A signed decimal number.</param>
        /// <returns>
        ///     A number that indicates the sign of , as shown in the following table.Return value Meaning -1  is less than
        ///     zero. 0  is equal to zero. 1  is greater than zero.
        /// </returns>
        public static int Sign(this decimal value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        ///     Calculates the integral part of a specified decimal number.
        /// </summary>
        /// <param name="d">A number to truncate.</param>
        /// <returns>
        ///     The integral part of ; that is, the number that remains after any fractional digits have been discarded.
        /// </returns>
        public static decimal TruncateZ(this decimal d)
        {
            return Math.Truncate(d);
        }

        /// <summary>
        ///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
        /// ###
        /// <typeparam name="T">Generic type parameter.</typeparam>
        public static bool Between(this decimal @this, decimal minValue, decimal maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        /// <summary>
        ///     A T extension method to determines whether the object is equal to any of the provided values.
        /// </summary>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list contains the object, else false.</returns>
        /// ###
        /// <typeparam name="T">Generic type parameter.</typeparam>
        public static bool InZ(this decimal @this, params decimal[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        /// <summary>
        ///     A T extension method that check if the value is between inclusively the minValue and maxValue.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>true if the value is between inclusively the minValue and maxValue, otherwise false.</returns>
        /// ###
        /// <typeparam name="T">Generic type parameter.</typeparam>
        public static bool InRange(this decimal @this, decimal minValue, decimal maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        /// <summary>
        ///     A T extension method to determines whether the object is not equal to any of the provided values.
        /// </summary>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list doesn't contains the object, else false.</returns>
        /// ###
        /// <typeparam name="T">Generic type parameter.</typeparam>
        public static bool NotIn(this decimal @this, params decimal[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
    }
}