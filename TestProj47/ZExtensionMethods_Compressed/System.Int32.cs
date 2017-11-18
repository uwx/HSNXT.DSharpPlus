using System;
using System.Data;
using System.Drawing;
using System.Net;

//using System.Data.SqlServerCe;

// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System;
//using System.Data;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static SqlDbType SqlSystemTypeToSqlDbType(this int @this)
        {
            switch (@this)
            {
                case 34: // 34 | "image" | SqlDbType.Image
                    return SqlDbType.Image;

                case 35: // 35 | "text" | SqlDbType.Text
                    return SqlDbType.Text;

                case 36: // 36 | "uniqueidentifier" | SqlDbType.UniqueIdentifier
                    return SqlDbType.UniqueIdentifier;

                case 40: // 40 | "date" | SqlDbType.Date
                    return SqlDbType.Date;

                case 41: // 41 | "time" | SqlDbType.Time
                    return SqlDbType.Time;

                case 42: // 42 | "datetime2" | SqlDbType.DateTime2
                    return SqlDbType.DateTime2;

                case 43: // 43 | "datetimeoffset" | SqlDbType.DateTimeOffset
                    return SqlDbType.DateTimeOffset;

                case 48: // 48 | "tinyint" | SqlDbType.TinyInt
                    return SqlDbType.TinyInt;

                case 52: // 52 | "smallint" | SqlDbType.SmallInt
                    return SqlDbType.SmallInt;

                case 56: // 56 | "int" | SqlDbType.Int
                    return SqlDbType.Int;

                case 58: // 58 | "smalldatetime" | SqlDbType.SmallDateTime
                    return SqlDbType.SmallDateTime;

                case 59: // 59 | "real" | SqlDbType.Real
                    return SqlDbType.Real;

                case 60: // 60 | "money" | SqlDbType.Money
                    return SqlDbType.Money;

                case 61: // 61 | "datetime" | SqlDbType.DateTime
                    return SqlDbType.DateTime;

                case 62: // 62 | "float" | SqlDbType.Float
                    return SqlDbType.Float;

                case 98: // 98 | "sql_variant" | SqlDbType.Variant
                    return SqlDbType.Variant;

                case 99: // 99 | "ntext" | SqlDbType.NText
                    return SqlDbType.NText;

                case 104: // 104 | "bit" | SqlDbType.Bit
                    return SqlDbType.Bit;

                case 106: // 106 | "decimal" | SqlDbType.Decimal
                    return SqlDbType.Decimal;

                case 108: // 108 | "numeric" | SqlDbType.Decimal
                    return SqlDbType.Decimal;

                case 122: // 122 | "smallmoney" | SqlDbType.SmallMoney
                    return SqlDbType.SmallMoney;

                case 127: // 127 | "bigint" | SqlDbType.BigInt
                    return SqlDbType.BigInt;

                case 165: // 165 | "varbinary" | SqlDbType.VarBinary
                    return SqlDbType.VarBinary;

                case 167: // 167 | "varchar" | SqlDbType.VarChar
                    return SqlDbType.VarChar;

                case 173: // 173 | "binary" | SqlDbType.Binary
                    return SqlDbType.Binary;

                case 175: // 175 | "char" | SqlDbType.Char
                    return SqlDbType.Char;

                case 189: // 189 | "timestamp" | SqlDbType.Timestamp
                    return SqlDbType.Timestamp;

                case 231: // 231 | "nvarchar", "sysname" | SqlDbType.NVarChar
                    return SqlDbType.NVarChar;

                case 239: // 239 | "nchar" | SqlDbType.NChar
                    return SqlDbType.NChar;

                case 240: // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                    return SqlDbType.Udt;

                case 241: // 241 | "xml" | SqlDbType.Xml
                    return SqlDbType.Xml;

                default:
                    throw new Exception(
                        $"Unsupported Type: {@this}. Please let us know about this type and we will support it: sales@zzzprojects.com");
            }
        }

        /// <summary>
        ///     An Int32 extension method that days the given this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A TimeSpan.</returns>
        public static TimeSpan Days(this int @this)
        {
            return TimeSpan.FromDays(@this);
        }

        /// <summary>
        ///     An Int32 extension method that factor of.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="factorNumer">The factor numer.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool FactorOf(this int @this, int factorNumer)
        {
            return factorNumer % @this == 0;
        }

        /// <summary>
        ///     An Int32 extension method that hours the given this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A TimeSpan.</returns>
        public static TimeSpan Hours(this int @this)
        {
            return TimeSpan.FromHours(@this);
        }

        /// <summary>
        ///     An Int32 extension method that query if '@this' is even.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if even, false if not.</returns>
        public static bool IsEven(this int @this)
        {
            return @this % 2 == 0;
        }

        /// <summary>
        ///     An Int32 extension method that query if '@this' is multiple of.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="factor">The factor.</param>
        /// <returns>true if multiple of, false if not.</returns>
        public static bool IsMultipleOf(this int @this, int factor)
        {
            return @this % factor == 0;
        }

        /// <summary>
        ///     An Int32 extension method that query if '@this' is odd.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if odd, false if not.</returns>
        public static bool IsOdd(this int @this)
        {
            return @this % 2 != 0;
        }

        /// <summary>
        ///     An Int32 extension method that query if '@this' is prime.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if prime, false if not.</returns>
        public static bool IsPrime(this int @this)
        {
            if (@this == 1 || @this == 2)
            {
                return true;
            }

            if (@this % 2 == 0)
            {
                return false;
            }

            var sqrt = (int) Math.Sqrt(@this);
            for (long t = 3; t <= sqrt; t = t + 2)
            {
                if (@this % t == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     An Int32 extension method that milliseconds the given this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A TimeSpan.</returns>
        public static TimeSpan Milliseconds(this int @this)
        {
            return TimeSpan.FromMilliseconds(@this);
        }

        /// <summary>
        ///     An Int32 extension method that minutes the given this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A TimeSpan.</returns>
        public static TimeSpan Minutes(this int @this)
        {
            return TimeSpan.FromMinutes(@this);
        }

        /// <summary>
        ///     An Int32 extension method that seconds the given this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A TimeSpan.</returns>
        public static TimeSpan Seconds(this int @this)
        {
            return TimeSpan.FromSeconds(@this);
        }

        /// <summary>
        ///     An Int32 extension method that weeks the given this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A TimeSpan.</returns>
        public static TimeSpan Weeks(this int @this)
        {
            return TimeSpan.FromDays(@this * 7);
        }

        /// <summary>
        ///     Returns the specified 32-bit signed integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 4.</returns>
        public static byte[] GetBytes(this int value)
        {
            return BitConverter.GetBytes(value);
        }

        /// <summary>
        ///     Converts the specified Unicode code point into a UTF-16 encoded string.
        /// </summary>
        /// <param name="utf32">A 21-bit Unicode code point.</param>
        /// <returns>
        ///     A string consisting of one  object or a surrogate pair of  objects equivalent to the code point specified by
        ///     the  parameter.
        /// </returns>
        public static string ConvertFromUtf32(this int utf32)
        {
            return char.ConvertFromUtf32(utf32);
        }

        /// <summary>
        ///     Returns the number of days in the specified month and year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month (a number ranging from 1 to 12).</param>
        /// <returns>
        ///     The number of days in  for the specified .For example, if  equals 2 for February, the return value is 28 or
        ///     29 depending upon whether  is a leap year.
        /// </returns>
        public static int DaysInMonth(this int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        /// <summary>
        ///     Returns an indication whether the specified year is a leap year.
        /// </summary>
        /// <param name="year">A 4-digit year.</param>
        /// <returns>true if  is a leap year; otherwise, false.</returns>
        public static bool IsLeapYear(this int year)
        {
            return DateTime.IsLeapYear(year);
        }

        /// <summary>
        ///     Creates a  structure from a 32-bit ARGB value.
        /// </summary>
        /// <param name="argb">A value specifying the 32-bit ARGB value.</param>
        /// <returns>The  structure that this method creates.</returns>
        public static Color FromArgb(this int argb)
        {
            return Color.FromArgb(argb);
        }

        /// <summary>
        ///     Creates a  structure from the four ARGB component (alpha, red, green, and blue) values. Although this method
        ///     allows a 32-bit value to be passed for each component, the value of each component is limited to 8 bits.
        /// </summary>
        /// <param name="argb">A value specifying the 32-bit ARGB value.</param>
        /// <param name="red">The red component. Valid values are 0 through 255.</param>
        /// <param name="green">The green component. Valid values are 0 through 255.</param>
        /// <param name="blue">The blue component. Valid values are 0 through 255.</param>
        /// <returns>The  that this method creates.</returns>
        public static Color FromArgb(this int argb, int red, int green, int blue)
        {
            return Color.FromArgb(argb, red, green, blue);
        }

        /// <summary>
        ///     Creates a  structure from the specified  structure, but with the new specified alpha value. Although this
        ///     method allows a 32-bit value to be passed for the alpha value, the value is limited to 8 bits.
        /// </summary>
        /// <param name="argb">A value specifying the 32-bit ARGB value.</param>
        /// <param name="baseColor">The  from which to create the new .</param>
        /// <returns>The  that this method creates.</returns>
        public static Color FromArgb(this int argb, Color baseColor)
        {
            return Color.FromArgb(argb, baseColor);
        }

        /// <summary>
        ///     Creates a  structure from the specified 8-bit color values (red, green, and blue). The alpha value is
        ///     implicitly 255 (fully opaque). Although this method allows a 32-bit value to be passed for each color
        ///     component, the value of each component is limited to 8 bits.
        /// </summary>
        /// <param name="argb">A value specifying the 32-bit ARGB value.</param>
        /// <param name="green">The green component value for the new . Valid values are 0 through 255.</param>
        /// <param name="blue">The blue component value for the new . Valid values are 0 through 255.</param>
        /// <returns>The  that this method creates.</returns>
        public static Color FromArgb(this int argb, int green, int blue)
        {
            return Color.FromArgb(argb, green, blue);
        }

        /// <summary>
        ///     Translates an OLE color value to a GDI+  structure.
        /// </summary>
        /// <param name="oleColor">The OLE color to translate.</param>
        /// <returns>The  structure that represents the translated OLE color.</returns>
        public static Color FromOle(this int oleColor)
        {
            return ColorTranslator.FromOle(oleColor);
        }

        /// <summary>
        ///     Translates a Windows color value to a GDI+  structure.
        /// </summary>
        /// <param name="win32Color">The Windows color to translate.</param>
        /// <returns>The  structure that represents the translated Windows color.</returns>
        public static Color FromWin32(this int win32Color)
        {
            return ColorTranslator.FromWin32(win32Color);
        }

        /// <summary>
        ///     Returns the absolute value of a 32-bit signed integer.
        /// </summary>
        /// <param name="value">A number that is greater than , but less than or equal to .</param>
        /// <returns>A 32-bit signed integer, x, such that 0 ? x ?.</returns>
        public static int Abs(this int value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        ///     Produces the full product of two 32-bit numbers.
        /// </summary>
        /// <param name="a">The first number to multiply.</param>
        /// <param name="b">The second number to multiply.</param>
        /// <returns>The number containing the product of the specified numbers.</returns>
        public static long BigMul(this int a, int b)
        {
            return Math.BigMul(a, b);
        }

        /// <summary>
        ///     An Int32 extension method that div rem.
        /// </summary>
        /// <param name="a">a to act on.</param>
        /// <param name="b">The Int32 to process.</param>
        /// <param name="result">[out] The result.</param>
        /// <returns>An Int32.</returns>
        public static int DivRem(this int a, int b, out int result)
        {
            return Math.DivRem(a, b, out result);
        }

        /// <summary>
        ///     Returns the larger of two 32-bit signed integers.
        /// </summary>
        /// <param name="val1">The first of two 32-bit signed integers to compare.</param>
        /// <param name="val2">The second of two 32-bit signed integers to compare.</param>
        /// <returns>Parameter  or , whichever is larger.</returns>
        public static int Max(this int val1, int val2)
        {
            return Math.Max(val1, val2);
        }

        /// <summary>
        ///     Returns the smaller of two 32-bit signed integers.
        /// </summary>
        /// <param name="val1">The first of two 32-bit signed integers to compare.</param>
        /// <param name="val2">The second of two 32-bit signed integers to compare.</param>
        /// <returns>Parameter  or , whichever is smaller.</returns>
        public static int Min(this int val1, int val2)
        {
            return Math.Min(val1, val2);
        }

        /// <summary>
        ///     Returns a value indicating the sign of a 32-bit signed integer.
        /// </summary>
        /// <param name="value">A signed number.</param>
        /// <returns>
        ///     A number that indicates the sign of , as shown in the following table.Return value Meaning -1  is less than
        ///     zero. 0  is equal to zero. 1  is greater than zero.
        /// </returns>
        public static int Sign(this int value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        ///     Converts an integer value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order.</param>
        /// <returns>An integer value, expressed in network byte order.</returns>
        public static int HostToNetworkOrder(this int host)
        {
            return IPAddress.HostToNetworkOrder(host);
        }

        /// <summary>
        ///     Converts an integer value from network byte order to host byte order.
        /// </summary>
        /// <param name="network">The number to convert, expressed in network byte order.</param>
        /// <returns>An integer value, expressed in host byte order.</returns>
        public static int NetworkToHostOrder(this int network)
        {
            return IPAddress.NetworkToHostOrder(network);
        }

        /// <summary>
        ///     A T extension method that check if the value is between (exclusif) the minValue and maxValue.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>true if the value is between the minValue and maxValue, otherwise false.</returns>
        public static bool Between(this int @this, int minValue, int maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        /// <summary>
        ///     A T extension method to determines whether the object is equal to any of the provided values.
        /// </summary>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list contains the object, else false.</returns>
        public static bool InZ(this int @this, params int[] values)
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
        public static bool InRange(this int @this, int minValue, int maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        /// <summary>
        ///     A T extension method to determines whether the object is not equal to any of the provided values.
        /// </summary>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list doesn't contains the object, else false.</returns>
        public static bool NotIn(this int @this, params int[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
    }
}