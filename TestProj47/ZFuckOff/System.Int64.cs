using System;
using System.Linq.Expressions;
using System.Runtime.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Collections.Specialized;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Data;
using System.Drawing;
using System.Web;
using System.Globalization;
using System.ComponentModel;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Data.Entity.Design.PluralizationServices;
using System.Security;
using System.Xml.Linq;
using System.Xml;
using System.Collections.ObjectModel;
using System.Data.Common;
//using System.Data.SqlServerCe;
using System.Drawing.Drawing2D;
using System.Security.AccessControl;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.Web.UI;
using System.Windows.Forms;
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System;

namespace TestProj47
{
    public static partial class Extensions
    {
        /// <summary>
        ///     An Int64 extension method that days the given this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A TimeSpan.</returns>
        public static TimeSpan Days(this long @this)
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
        public static bool FactorOf(this long @this, long factorNumer)
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
        public static TimeSpan Hours(this long @this)
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
        public static bool IsEven(this long @this)
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
        public static bool IsMultipleOf(this long @this, long factor)
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
        public static bool IsOdd(this long @this)
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
        public static bool IsPrime(this long @this)
        {
            if (@this == 1 || @this == 2)
            {
                return true;
            }

            if (@this % 2 == 0)
            {
                return false;
            }

            var sqrt = (long) Math.Sqrt(@this);
            for (long t = 3; t <= sqrt; t = t + 2)
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
        public static TimeSpan Milliseconds(this long @this)
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
        public static TimeSpan Minutes(this long @this)
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
        public static TimeSpan Seconds(this long @this)
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
        public static TimeSpan Weeks(this long @this)
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
        public static byte[] GetBytes(this long value)
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
        public static double Int64BitsToDouble(this long value)
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
        public static DateTime FromBinary(this long dateData)
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
        public static DateTime FromFileTime(this long fileTime)
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
        public static DateTime FromFileTimeUtc(this long fileTime)
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
        public static decimal FromOACurrency(this long cy)
        {
            return decimal.FromOACurrency(cy);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the absolute value of a 64-bit signed integer.
        /// </summary>
        /// <param name="value">A number that is greater than , but less than or equal to .</param>
        /// <returns>A 64-bit signed integer, x, such that 0 ? x ?.</returns>
        public static long Abs(this long value)
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
        public static long DivRem(this long a, long b, out long result)
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
        public static long Max(this long val1, long val2)
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
        public static long Min(this long val1, long val2)
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
        public static int Sign(this long value)
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
        public static long HostToNetworkOrder(this long host)
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
        public static long NetworkToHostOrder(this long network)
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
        public static TimeSpan FromTicks(this long value)
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
        public static bool Between(this long @this, long minValue, long maxValue)
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
        public static bool InZ(this long @this, params long[] values)
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
        public static bool InRange(this long @this, long minValue, long maxValue)
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
        public static bool NotIn(this long @this, params long[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
    }
}