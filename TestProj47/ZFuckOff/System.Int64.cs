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
        ///     An Int64 extension method that days the given this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A TimeSpan.</returns>
        public static TimeSpan Days(this Int64 @this)
        {
            return TimeSpan.FromDays(@this);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     An Int64 extension method that factor of.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="factorNumer">The factor numer.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool FactorOf(this Int64 @this, Int64 factorNumer)
        {
            return factorNumer % @this == 0;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     An Int64 extension method that hours the given this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A TimeSpan.</returns>
        public static TimeSpan Hours(this Int64 @this)
        {
            return TimeSpan.FromHours(@this);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     An Int64 extension method that query if '@this' is even.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if even, false if not.</returns>
        public static bool IsEven(this Int64 @this)
        {
            return @this % 2 == 0;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     An Int64 extension method that query if '@this' is multiple of.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="factor">The factor.</param>
        /// <returns>true if multiple of, false if not.</returns>
        public static bool IsMultipleOf(this Int64 @this, Int64 factor)
        {
            return @this % factor == 0;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     An Int64 extension method that query if '@this' is odd.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if odd, false if not.</returns>
        public static bool IsOdd(this Int64 @this)
        {
            return @this % 2 != 0;
        }
    }

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

            if (@this % 2 == 0)
            {
                return false;
            }

            var sqrt = (Int64) Math.Sqrt(@this);
            for (Int64 t = 3; t <= sqrt; t = t + 2)
            {
                if (@this % t == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     An Int64 extension method that milliseconds the given this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A TimeSpan.</returns>
        public static TimeSpan Milliseconds(this Int64 @this)
        {
            return TimeSpan.FromMilliseconds(@this);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     An Int64 extension method that minutes the given this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A TimeSpan.</returns>
        public static TimeSpan Minutes(this Int64 @this)
        {
            return TimeSpan.FromMinutes(@this);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     An Int64 extension method that seconds the given this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A TimeSpan.</returns>
        public static TimeSpan Seconds(this Int64 @this)
        {
            return TimeSpan.FromSeconds(@this);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     An Int64 extension method that weeks the given this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A TimeSpan.</returns>
        public static TimeSpan Weeks(this Int64 @this)
        {
            return TimeSpan.FromDays(@this * 7);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the specified 64-bit signed integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 8.</returns>
        public static Byte[] GetBytes(this Int64 value)
        {
            return BitConverter.GetBytes(value);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the specified 64-bit signed integer to a double-precision floating point number.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A double-precision floating point number whose value is equivalent to .</returns>
        public static Double Int64BitsToDouble(this Int64 value)
        {
            return BitConverter.Int64BitsToDouble(value);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Deserializes a 64-bit binary value and recreates an original serialized  object.
        /// </summary>
        /// <param name="dateData">
        ///     A 64-bit signed integer that encodes the  property in a 2-bit field and the  property in
        ///     a 62-bit field.
        /// </param>
        /// <returns>An object that is equivalent to the  object that was serialized by the  method.</returns>
        public static DateTime FromBinary(this Int64 dateData)
        {
            return DateTime.FromBinary(dateData);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the specified Windows file time to an equivalent local time.
        /// </summary>
        /// <param name="fileTime">A Windows file time expressed in ticks.</param>
        /// <returns>
        ///     An object that represents the local time equivalent of the date and time represented by the  parameter.
        /// </returns>
        public static DateTime FromFileTime(this Int64 fileTime)
        {
            return DateTime.FromFileTime(fileTime);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the specified Windows file time to an equivalent UTC time.
        /// </summary>
        /// <param name="fileTime">A Windows file time expressed in ticks.</param>
        /// <returns>
        ///     An object that represents the UTC time equivalent of the date and time represented by the  parameter.
        /// </returns>
        public static DateTime FromFileTimeUtc(this Int64 fileTime)
        {
            return DateTime.FromFileTimeUtc(fileTime);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the specified 64-bit signed integer, which contains an OLE Automation Currency value, to the
        ///     equivalent  value.
        /// </summary>
        /// <param name="cy">An OLE Automation Currency value.</param>
        /// <returns>A  that contains the equivalent of .</returns>
        public static Decimal FromOACurrency(this Int64 cy)
        {
            return Decimal.FromOACurrency(cy);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the absolute value of a 64-bit signed integer.
        /// </summary>
        /// <param name="value">A number that is greater than , but less than or equal to .</param>
        /// <returns>A 64-bit signed integer, x, such that 0 ? x ?.</returns>
        public static Int64 Abs(this Int64 value)
        {
            return Math.Abs(value);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     An Int64 extension method that div rem.
        /// </summary>
        /// <param name="a">a to act on.</param>
        /// <param name="b">The Int64 to process.</param>
        /// <param name="result">[out] The result.</param>
        /// <returns>An Int64.</returns>
        public static Int64 DivRem(this Int64 a, Int64 b, out Int64 result)
        {
            return Math.DivRem(a, b, out result);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the larger of two 64-bit signed integers.
        /// </summary>
        /// <param name="val1">The first of two 64-bit signed integers to compare.</param>
        /// <param name="val2">The second of two 64-bit signed integers to compare.</param>
        /// <returns>Parameter  or , whichever is larger.</returns>
        public static Int64 Max(this Int64 val1, Int64 val2)
        {
            return Math.Max(val1, val2);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the smaller of two 64-bit signed integers.
        /// </summary>
        /// <param name="val1">The first of two 64-bit signed integers to compare.</param>
        /// <param name="val2">The second of two 64-bit signed integers to compare.</param>
        /// <returns>Parameter  or , whichever is smaller.</returns>
        public static Int64 Min(this Int64 val1, Int64 val2)
        {
            return Math.Min(val1, val2);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Returns a value indicating the sign of a 64-bit signed integer.
        /// </summary>
        /// <param name="value">A signed number.</param>
        /// <returns>
        ///     A number that indicates the sign of , as shown in the following table.Return value Meaning -1  is less than
        ///     zero. 0  is equal to zero. 1  is greater than zero.
        /// </returns>
        public static Int32 Sign(this Int64 value)
        {
            return Math.Sign(value);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts a long value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order.</param>
        /// <returns>A long value, expressed in network byte order.</returns>
        public static Int64 HostToNetworkOrder(this Int64 host)
        {
            return IPAddress.HostToNetworkOrder(host);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts a long value from network byte order to host byte order.
        /// </summary>
        /// <param name="network">The number to convert, expressed in network byte order.</param>
        /// <returns>A long value, expressed in host byte order.</returns>
        public static Int64 NetworkToHostOrder(this Int64 network)
        {
            return IPAddress.NetworkToHostOrder(network);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Returns a  that represents a specified time, where the specification is in units of ticks.
        /// </summary>
        /// <param name="value">A number of ticks that represent a time.</param>
        /// <returns>An object that represents .</returns>
        public static TimeSpan FromTicks(this Int64 value)
        {
            return TimeSpan.FromTicks(value);
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
        public static bool Between(this Int64 @this, Int64 minValue, Int64 maxValue)
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
        public static bool InZ(this Int64 @this, params Int64[] values)
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
        public static bool InRange(this Int64 @this, Int64 minValue, Int64 maxValue)
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
        public static bool NotIn(this Int64 @this, params Int64[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
    }
}