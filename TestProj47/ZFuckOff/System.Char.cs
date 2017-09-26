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

namespace TestProj47
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A char extension method that repeats a character the specified number of times.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="repeatCount">Number of repeats.</param>
        /// <returns>The repeated char.</returns>
        public static string Repeat(this char @this, int repeatCount)
        {
            return new string(@this, repeatCount);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Enumerates from @this to toCharacter.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="toCharacter">to character.</param>
        /// <returns>An enumerator that allows foreach to be used to process @this to toCharacter.</returns>
        public static IEnumerable<char> To(this char @this, char toCharacter)
        {
            bool reverseRequired = (@this > toCharacter);

            char first = reverseRequired ? toCharacter : @this;
            char last = reverseRequired ? @this : toCharacter;

            IEnumerable<char> result = Enumerable.Range(first, last - first + 1).Select(charCode => (char) charCode);

            if (reverseRequired)
            {
                result = result.Reverse();
            }


            return result;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the value of a UTF-16 encoded surrogate pair into a Unicode code point.
        /// </summary>
        /// <param name="highSurrogate">A high surrogate code unit (that is, a code unit ranging from U+D800 through U+DBFF).</param>
        /// <param name="lowSurrogate">A low surrogate code unit (that is, a code unit ranging from U+DC00 through U+DFFF).</param>
        /// <returns>The 21-bit Unicode code point represented by the  and  parameters.</returns>
        public static Int32 ConvertToUtf32(this Char highSurrogate, Char lowSurrogate)
        {
            return Char.ConvertToUtf32(highSurrogate, lowSurrogate);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the specified numeric Unicode character to a double-precision floating point number.
        /// </summary>
        /// <param name="c">The Unicode character to convert.</param>
        /// <returns>The numeric value of  if that character represents a number; otherwise, -1.0.</returns>
        public static Double GetNumericValue(this Char c)
        {
            return Char.GetNumericValue(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Categorizes a specified Unicode character into a group identified by one of the  values.
        /// </summary>
        /// <param name="c">The Unicode character to categorize.</param>
        /// <returns>A  value that identifies the group that contains .</returns>
        public static UnicodeCategory GetUnicodeCategory(this Char c)
        {
            return Char.GetUnicodeCategory(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a control character.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a control character; otherwise, false.</returns>
        public static Boolean IsControl(this Char c)
        {
            return Char.IsControl(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a decimal digit.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a decimal digit; otherwise, false.</returns>
        public static Boolean IsDigit(this Char c)
        {
            return Char.IsDigit(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the specified  object is a high surrogate.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>
        ///     true if the numeric value of the  parameter ranges from U+D800 through U+DBFF; otherwise, false.
        /// </returns>
        public static Boolean IsHighSurrogate(this Char c)
        {
            return Char.IsHighSurrogate(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a Unicode letter.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a letter; otherwise, false.</returns>
        public static Boolean IsLetter(this Char c)
        {
            return Char.IsLetter(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a letter or a decimal digit.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a letter or a decimal digit; otherwise, false.</returns>
        public static Boolean IsLetterOrDigit(this Char c)
        {
            return Char.IsLetterOrDigit(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a lowercase letter.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a lowercase letter; otherwise, false.</returns>
        public static Boolean IsLower(this Char c)
        {
            return Char.IsLower(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the specified  object is a low surrogate.
        /// </summary>
        /// <param name="c">The character to evaluate.</param>
        /// <returns>
        ///     true if the numeric value of the  parameter ranges from U+DC00 through U+DFFF; otherwise, false.
        /// </returns>
        public static Boolean IsLowSurrogate(this Char c)
        {
            return Char.IsLowSurrogate(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a number.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a number; otherwise, false.</returns>
        public static Boolean IsNumber(this Char c)
        {
            return Char.IsNumber(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a punctuation mark.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a punctuation mark; otherwise, false.</returns>
        public static Boolean IsPunctuation(this Char c)
        {
            return Char.IsPunctuation(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a separator character.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a separator character; otherwise, false.</returns>
        public static Boolean IsSeparator(this Char c)
        {
            return Char.IsSeparator(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the specified character has a surrogate code unit.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is either a high surrogate or a low surrogate; otherwise, false.</returns>
        public static Boolean IsSurrogate(this Char c)
        {
            return Char.IsSurrogate(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the two specified  objects form a surrogate pair.
        /// </summary>
        /// <param name="highSurrogate">The character to evaluate as the high surrogate of a surrogate pair.</param>
        /// <param name="lowSurrogate">The character to evaluate as the low surrogate of a surrogate pair.</param>
        /// <returns>
        ///     true if the numeric value of the  parameter ranges from U+D800 through U+DBFF, and the numeric value of the
        ///     parameter ranges from U+DC00 through U+DFFF; otherwise, false.
        /// </returns>
        public static Boolean IsSurrogatePair(this Char highSurrogate, Char lowSurrogate)
        {
            return Char.IsSurrogatePair(highSurrogate, lowSurrogate);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as a symbol character.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is a symbol character; otherwise, false.</returns>
        public static Boolean IsSymbol(this Char c)
        {
            return Char.IsSymbol(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as an uppercase letter.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is an uppercase letter; otherwise, false.</returns>
        public static Boolean IsUpper(this Char c)
        {
            return Char.IsUpper(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the specified Unicode character is categorized as white space.
        /// </summary>
        /// <param name="c">The Unicode character to evaluate.</param>
        /// <returns>true if  is white space; otherwise, false.</returns>
        public static Boolean IsWhiteSpace(this Char c)
        {
            return Char.IsWhiteSpace(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the value of a specified Unicode character to its lowercase equivalent using specified culture-
        ///     specific formatting information.
        /// </summary>
        /// <param name="c">The Unicode character to convert.</param>
        /// <param name="culture">An object that supplies culture-specific casing rules.</param>
        /// <returns>
        ///     The lowercase equivalent of , modified according to , or the unchanged value of , if  is already lowercase or
        ///     not alphabetic.
        /// </returns>
        public static Char ToLower(this Char c, CultureInfo culture)
        {
            return Char.ToLower(c, culture);
        }

        /// <summary>
        ///     Converts the value of a Unicode character to its lowercase equivalent.
        /// </summary>
        /// <param name="c">The Unicode character to convert.</param>
        /// <returns>
        ///     The lowercase equivalent of , or the unchanged value of , if  is already lowercase or not alphabetic.
        /// </returns>
        public static Char ToLower(this Char c)
        {
            return Char.ToLower(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the value of a Unicode character to its lowercase equivalent using the casing rules of the invariant
        ///     culture.
        /// </summary>
        /// <param name="c">The Unicode character to convert.</param>
        /// <returns>
        ///     The lowercase equivalent of the  parameter, or the unchanged value of , if  is already lowercase or not
        ///     alphabetic.
        /// </returns>
        public static Char ToLowerInvariant(this Char c)
        {
            return Char.ToLowerInvariant(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the specified Unicode character to its equivalent string representation.
        /// </summary>
        /// <param name="c">The Unicode character to convert.</param>
        /// <returns>The string representation of the value of .</returns>
        public static String ToString(this Char c)
        {
            return Char.ToString(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the value of a specified Unicode character to its uppercase equivalent using specified culture-
        ///     specific formatting information.
        /// </summary>
        /// <param name="c">The Unicode character to convert.</param>
        /// <param name="culture">An object that supplies culture-specific casing rules.</param>
        /// <returns>
        ///     The uppercase equivalent of , modified according to , or the unchanged value of  if  is already uppercase,
        ///     has no uppercase equivalent, or is not alphabetic.
        /// </returns>
        public static Char ToUpper(this Char c, CultureInfo culture)
        {
            return Char.ToUpper(c, culture);
        }

        /// <summary>
        ///     Converts the value of a Unicode character to its uppercase equivalent.
        /// </summary>
        /// <param name="c">The Unicode character to convert.</param>
        /// <returns>
        ///     The uppercase equivalent of , or the unchanged value of  if  is already uppercase, has no uppercase
        ///     equivalent, or is not alphabetic.
        /// </returns>
        public static Char ToUpper(this Char c)
        {
            return Char.ToUpper(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the value of a Unicode character to its uppercase equivalent using the casing rules of the invariant
        ///     culture.
        /// </summary>
        /// <param name="c">The Unicode character to convert.</param>
        /// <returns>
        ///     The uppercase equivalent of the  parameter, or the unchanged value of , if  is already uppercase or not
        ///     alphabetic.
        /// </returns>
        public static Char ToUpperInvariant(this Char c)
        {
            return Char.ToUpperInvariant(c);
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
        public static bool InZ(this Char @this, params Char[] values)
        {
            return Array.IndexOf(values, @this) != -1;
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
        public static bool NotIn(this Char @this, params Char[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
    }
}