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
        ///     A string extension method that line break 2 newline.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string Br2Nl(this string @this)
        {
            return @this.Replace("<br />", "\r\n").Replace("<br>", "\r\n");
        }
    }

    public static partial class Extensions
    {
        /// <summary>An IEnumerable&lt;string&gt; extension method that concatenates the given this.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string Concatenate(this IEnumerable<string> @this)
        {
            var sb = new StringBuilder();

            foreach (var s in @this)
            {
                sb.Append(s);
            }

            return sb.ToString();
        }

        /// <summary>An IEnumerable&lt;T&gt; extension method that concatenates.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="source">The source to act on.</param>
        /// <param name="func">The function.</param>
        /// <returns>A string.</returns>
        public static string Concatenate<T>(this IEnumerable<T> source, Func<T, string> func)
        {
            var sb = new StringBuilder();
            foreach (var item in source)
            {
                sb.Append(func(item));
            }

            return sb.ToString();
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that concatenate with.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>A string.</returns>
        public static string ConcatWith(this string @this, params string[] values)
        {
            return string.Concat(@this, string.Concat(values));
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that query if this object contains the given value.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="value">The value.</param>
        /// <returns>true if the value is in the string, false if not.</returns>
        public static bool Contains(this string @this, string value)
        {
            return @this.IndexOf(value) != -1;
        }

        /// <summary>
        ///     A string extension method that query if this object contains the given value.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="value">The value.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <returns>true if the value is in the string, false if not.</returns>
        public static bool Contains(this string @this, string value, StringComparison comparisonType)
        {
            return @this.IndexOf(value, comparisonType) != -1;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that query if '@this' contains all values.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>true if it contains all values, otherwise false.</returns>
        public static bool ContainsAll(this string @this, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        ///     A string extension method that query if this object contains the given @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>true if it contains all values, otherwise false.</returns>
        public static bool ContainsAll(this string @this, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value, comparisonType) == -1)
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
        ///     A string extension method that query if '@this' contains any values.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>true if it contains any values, otherwise false.</returns>
        public static bool ContainsAny(this string @this, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     A string extension method that query if '@this' contains any values.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>true if it contains any values, otherwise false.</returns>
        public static bool ContainsAny(this string @this, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value, comparisonType) != -1)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that decode a Base64 String.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The Base64 String decoded.</returns>
        public static string DecodeBase64(this string @this)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(@this));
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that decrypt a string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="key">The key.</param>
        /// <returns>The decrypted string.</returns>
        public static string DecryptRSA(this string @this, string key)
        {
            var cspp = new CspParameters {KeyContainerName = key};
            var rsa = new RSACryptoServiceProvider(cspp) {PersistKeyInCsp = true};
            string[] decryptArray = @this.Split(new[] {"-"}, StringSplitOptions.None);
            byte[] decryptByteArray =
                Array.ConvertAll(decryptArray, (s => Convert.ToByte(byte.Parse(s, NumberStyles.HexNumber))));
            byte[] bytes = rsa.Decrypt(decryptByteArray, true);

            return Encoding.UTF8.GetString(bytes);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that encode the string to Base64.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The encoded string to Base64.</returns>
        public static string EncodeBase64(this string @this)
        {
            return Convert.ToBase64String(Activator.CreateInstance<ASCIIEncoding>().GetBytes(@this));
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that encrypts the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="key">The key.</param>
        /// <returns>The encrypted string.</returns>
        public static string EncryptRSA(this string @this, string key)
        {
            var cspp = new CspParameters {KeyContainerName = key};
            var rsa = new RSACryptoServiceProvider(cspp) {PersistKeyInCsp = true};
            byte[] bytes = rsa.Encrypt(Encoding.UTF8.GetBytes(@this), true);

            return BitConverter.ToString(bytes);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that escape XML.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string EscapeXml(this string @this)
        {
            return @this.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;")
                .Replace("'", "&apos;");
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts this object.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A string.</returns>
        public static string Extract(this string @this, Func<char, bool> predicate)
        {
            return new string(@this.ToCharArray().Where(predicate).ToArray());
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts the letter described by @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted letter.</returns>
        public static string ExtractLetter(this string @this)
        {
            return new string(@this.ToCharArray().Where(x => Char.IsLetter(x)).ToArray());
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts the number described by @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted number.</returns>
        public static string ExtractNumber(this string @this)
        {
            return new string(@this.ToCharArray().Where(x => Char.IsNumber(x)).ToArray());
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Replaces the format item in a specified String with the text equivalent of the value of a corresponding
        ///     Object instance in a specified array.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="arg0">The argument 0.</param>
        /// <returns>
        ///     A copy of format in which the format items have been replaced by the String equivalent of the corresponding
        ///     instances of Object in args.
        /// </returns>
        public static String FormatWithZ(this String @this, Object arg0)
        {
            return String.Format(@this, arg0);
        }

        /// <summary>
        ///     Replaces the format item in a specified String with the text equivalent of the value of a corresponding
        ///     Object instance in a specified array.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="arg0">The argument 0.</param>
        /// <param name="arg1">The first argument.</param>
        /// <returns>
        ///     A copy of format in which the format items have been replaced by the String equivalent of the corresponding
        ///     instances of Object in args.
        /// </returns>
        public static String FormatWithZ(this String @this, Object arg0, Object arg1)
        {
            return String.Format(@this, arg0, arg1);
        }

        /// <summary>
        ///     Replaces the format item in a specified String with the text equivalent of the value of a corresponding
        ///     Object instance in a specified array.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="arg0">The argument 0.</param>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <returns>
        ///     A copy of format in which the format items have been replaced by the String equivalent of the corresponding
        ///     instances of Object in args.
        /// </returns>
        public static String FormatWithZ(this String @this, Object arg0, Object arg1, Object arg2)
        {
            return String.Format(@this, arg0, arg1, arg2);
        }

        /// <summary>
        ///     Replaces the format item in a specified String with the text equivalent of the value of a corresponding
        ///     Object instance in a specified array.
        /// </summary>
        /// <param name="this">A String containing zero or more format items.</param>
        /// <param name="values">An Object array containing zero or more objects to format.</param>
        /// <returns>
        ///     A copy of format in which the format items have been replaced by the String equivalent of the corresponding
        ///     instances of Object in args.
        /// </returns>
        public static string FormatWithZ(this string @this, params object[] values)
        {
            return String.Format(@this, values);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that get the string after the specified string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="value">The value to search.</param>
        /// <returns>The string after the specified value.</returns>
        public static string GetAfter(this string @this, string value)
        {
            if (@this.IndexOf(value) == -1)
            {
                return "";
            }
            return @this.Substring(@this.IndexOf(value) + value.Length);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that get the string before the specified string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="value">The value to search.</param>
        /// <returns>The string before the specified value.</returns>
        public static string GetBefore(this string @this, string value)
        {
            if (@this.IndexOf(value) == -1)
            {
                return "";
            }
            return @this.Substring(0, @this.IndexOf(value));
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that get the string between the two specified string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="before">The string before to search.</param>
        /// <param name="after">The string after to search.</param>
        /// <returns>The string between the two specified string.</returns>
        public static string GetBetween(this string @this, string before, string after)
        {
            int beforeStartIndex = @this.IndexOf(before);
            int startIndex = beforeStartIndex + before.Length;
            int afterStartIndex = @this.IndexOf(after, startIndex);

            if (beforeStartIndex == -1 || afterStartIndex == -1)
            {
                return "";
            }

            return @this.Substring(startIndex, afterStartIndex - startIndex);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that if empty.
        /// </summary>
        /// <param name="value">The value to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>A string.</returns>
        public static string IfEmpty(this string value, string defaultValue)
        {
            return (IsNotEmpty(value) ? value : defaultValue);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that query if '@this' is Alpha.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if Alpha, false if not.</returns>
        public static bool IsAlphaZ(this string @this)
        {
            return !Regex.IsMatch(@this, "[^a-zA-Z]");
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that query if '@this' is Alphanumeric.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if Alphanumeric, false if not.</returns>
        public static bool IsAlphaNumericZ(this string @this)
        {
            return !Regex.IsMatch(@this, "[^a-zA-Z0-9]");
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that query if '@this' is anagram of other String.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="otherString">The other string</param>
        /// <returns>true if the @this is anagram of the otherString, false if not.</returns>
        public static bool IsAnagram(this string @this, string otherString)
        {
            return @this
                .OrderBy(c => c)
                .SequenceEqual(otherString.OrderBy(c => c));
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that query if '@this' is empty.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if empty, false if not.</returns>
        public static bool IsEmpty(this string @this)
        {
            return @this == "";
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that query if '@this' satisfy the specified pattern.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="pattern">The pattern to use. Use '*' as wildcard string.</param>
        /// <returns>true if '@this' satisfy the specified pattern, false if not.</returns>
        public static bool IsLike(this string @this, string pattern)
        {
            // Turn the pattern into regex pattern, and match the whole string with ^$
            string regexPattern = "^" + Regex.Escape(pattern) + "$";

            // Escape special character ?, #, *, [], and [!]
            regexPattern = regexPattern.Replace(@"\[!", "[^")
                .Replace(@"\[", "[")
                .Replace(@"\]", "]")
                .Replace(@"\?", ".")
                .Replace(@"\*", ".*")
                .Replace(@"\#", @"\d");

            return Regex.IsMatch(@this, regexPattern);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that queries if a not is empty.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if a not is empty, false if not.</returns>
        public static bool IsNotEmpty(this string @this)
        {
            return @this != "";
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that queries if '@this' is not (null or empty).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if '@this' is not (null or empty), false if not.</returns>
        public static bool IsNotNullOrEmptyZ(this string @this)
        {
            return !string.IsNullOrEmpty(@this);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether a specified string is not null, not empty, or not consists only of white-space characters.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the  parameter is null or , or if  consists exclusively of white-space characters.</returns>
        public static Boolean IsNotNullOrWhiteSpace(this string value)
        {
            return !String.IsNullOrWhiteSpace(value);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that queries if '@this' is null or is empty.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if '@this' is null or is empty, false if not.</returns>
        public static bool IsNullOrEmptyZ(this string @this)
        {
            return string.IsNullOrEmpty(@this);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that query if '@this' is numeric.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if numeric, false if not.</returns>
        public static bool IsNumeric(this string @this)
        {
            return !Regex.IsMatch(@this, "[^0-9]");
        }
    }

    public static partial class Extensions
    {
        /// <summary>A string extension method that query if '@this' is palindrome.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if palindrome, false if not.</returns>
        public static bool IsPalindrome(this string @this)
        {
            // Keep only alphanumeric characters

            var rgx = new Regex("[^a-zA-Z0-9]");
            @this = rgx.Replace(@this, "");
            return @this.SequenceEqual(Reverse(@this));
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that return the left part of the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="length">The length.</param>
        /// <returns>The left part.</returns>
        public static string Left(this string @this, int length)
        {
            return @this.Substring(0, length);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that left safe.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="length">The length.</param>
        /// <returns>A string.</returns>
        public static string LeftSafe(this string @this, int length)
        {
            return @this.Substring(0, Math.Min(length, @this.Length));
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that newline 2 line break.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string Nl2Br(this string @this)
        {
            return @this.Replace("\r\n", "<br />").Replace("\n", "<br />");
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that return null if the value is empty else the value.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>null if the value is empty, otherwise the value.</returns>
        public static string NullIfEmpty(this string @this)
        {
            return @this == "" ? null : @this;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Combines multiples string into a path.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="paths">A variable-length parameters list containing paths.</param>
        /// <returns>
        ///     The combined paths. If one of the specified paths is a zero-length string, this method returns the other path.
        /// </returns>
        public static string PathCombine(this string @this, params string[] paths)
        {
            List<string> list = paths.ToList();
            list.Insert(0, @this);
            return Path.Combine(list.ToArray());
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that removes the diacritics character from the strings.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The string without diacritics character.</returns>
        public static string RemoveDiacritics(this string @this)
        {
            string normalizedString = @this.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char t in normalizedString)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(t);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(t);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that removes the letter described by @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string RemoveLetter(this string @this)
        {
            return new string(@this.ToCharArray().Where(x => !Char.IsLetter(x)).ToArray());
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that removes the number described by @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string RemoveNumber(this string @this)
        {
            return new string(@this.ToCharArray().Where(x => !Char.IsNumber(x)).ToArray());
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that removes the letter.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A string.</returns>
        public static string RemoveWhere(this string @this, Func<char, bool> predicate)
        {
            return new string(@this.ToCharArray().Where(x => !predicate(x)).ToArray());
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that repeats the string a specified number of times.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="repeatCount">Number of repeats.</param>
        /// <returns>The repeated string.</returns>
        public static string Repeat(this string @this, int repeatCount)
        {
            if (@this.Length == 1)
            {
                return new string(@this[0], repeatCount);
            }

            var sb = new StringBuilder(repeatCount * @this.Length);
            while (repeatCount-- > 0)
            {
                sb.Append(@this);
            }

            return sb.ToString();
        }
    }

    public static partial class Extensions
    {
        /// <summary>A string extension method that replaces.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="length">The length.</param>
        /// <param name="value">The value.</param>
        /// <returns>A string.</returns>
        public static string Replace(this string @this, int startIndex, int length, string value)
        {
            @this = @this.Remove(startIndex, length).Insert(startIndex, value);

            return @this;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that replace all values specified by an empty string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>A string with all specified values replaced by an empty string.</returns>
        public static string ReplaceByEmpty(this string @this, params string[] values)
        {
            foreach (string value in values)
            {
                @this = @this.Replace(value, "");
            }

            return @this;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that replace first occurence.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The string with the first occurence of old value replace by new value.</returns>
        public static string ReplaceFirst(this string @this, string oldValue, string newValue)
        {
            int startindex = @this.IndexOf(oldValue);

            if (startindex == -1)
            {
                return @this;
            }

            return @this.Remove(startindex, oldValue.Length).Insert(startindex, newValue);
        }

        /// <summary>
        ///     A string extension method that replace first number of occurences.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="number">Number of.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The string with the numbers of occurences of old value replace by new value.</returns>
        public static string ReplaceFirst(this string @this, int number, string oldValue, string newValue)
        {
            List<string> list = @this.Split(oldValue).ToList();
            int old = number + 1;
            IEnumerable<string> listStart = list.Take(old);
            IEnumerable<string> listEnd = list.Skip(old);

            return string.Join(newValue, listStart) +
                   (listEnd.Any() ? oldValue : "") +
                   string.Join(oldValue, listEnd);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that replace last occurence.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The string with the last occurence of old value replace by new value.</returns>
        public static string ReplaceLast(this string @this, string oldValue, string newValue)
        {
            int startindex = @this.LastIndexOf(oldValue);

            if (startindex == -1)
            {
                return @this;
            }

            return @this.Remove(startindex, oldValue.Length).Insert(startindex, newValue);
        }

        /// <summary>
        ///     A string extension method that replace last numbers occurences.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="number">Number of.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The string with the last numbers occurences of old value replace by new value.</returns>
        public static string ReplaceLast(this string @this, int number, string oldValue, string newValue)
        {
            List<string> list = @this.Split(oldValue).ToList();
            int old = Math.Max(0, list.Count - number - 1);
            IEnumerable<string> listStart = list.Take(old);
            IEnumerable<string> listEnd = list.Skip(old);

            return string.Join(oldValue, listStart) +
                   (old > 0 ? oldValue : "") +
                   string.Join(newValue, listEnd);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that replace when equals.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The new value if the string equal old value; Otherwise old value.</returns>
        public static string ReplaceWhenEquals(this string @this, string oldValue, string newValue)
        {
            return @this == oldValue ? newValue : @this;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that reverses the given string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The string reversed.</returns>
        public static string Reverse(this string @this)
        {
            if (@this.Length <= 1)
            {
                return @this;
            }

            char[] chars = @this.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that return the right part of the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="length">The length.</param>
        /// <returns>The right part.</returns>
        public static string Right(this string @this, int length)
        {
            return @this.Substring(@this.Length - length);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that right safe.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="length">The length.</param>
        /// <returns>A string.</returns>
        public static string RightSafe(this string @this, int length)
        {
            return @this.Substring(Math.Max(0, @this.Length - length));
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that save the string into a file.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="fileName">Filename of the file.</param>
        /// <param name="append">(Optional) if the text should be appended to file file if it's exists.</param>
        public static void SaveAs(this string @this, string fileName, bool append = false)
        {
            using (TextWriter tw = new StreamWriter(fileName, append))
            {
                tw.Write(@this);
            }
        }

        /// <summary>
        ///     A string extension method that save the string into a file.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="file">The FileInfo.</param>
        /// <param name="append">(Optional) if the text should be appended to file file if it's exists.</param>
        public static void SaveAs(this string @this, FileInfo file, bool append = false)
        {
            using (TextWriter tw = new StreamWriter(file.FullName, append))
            {
                tw.Write(@this);
            }
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Returns a String array containing the substrings in this string that are delimited by elements of a specified
        ///     String array. A parameter specifies whether to return empty array elements.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="separator">A string that delimit the substrings in this string.</param>
        /// <param name="option">
        ///     (Optional) Specify RemoveEmptyEntries to omit empty array elements from the array returned,
        ///     or None to include empty array elements in the array returned.
        /// </param>
        /// <returns>
        ///     An array whose elements contain the substrings in this string that are delimited by the separator.
        /// </returns>
        public static string[] Split(this string @this, string separator,
            StringSplitOptions option = StringSplitOptions.None)
        {
            return @this.Split(new[] {separator}, option);
        }
    }

    public static partial class Extensions
    {
        public static SqlDbType SqlTypeNameToSqlDbType(this string @this)
        {
            switch (@this.ToLower())
            {
                case "image": // 34 | "image" | SqlDbType.Image
                    return SqlDbType.Image;

                case "text": // 35 | "text" | SqlDbType.Text
                    return SqlDbType.Text;

                case "uniqueidentifier": // 36 | "uniqueidentifier" | SqlDbType.UniqueIdentifier
                    return SqlDbType.UniqueIdentifier;

                case "date": // 40 | "date" | SqlDbType.Date
                    return SqlDbType.Date;

                case "time": // 41 | "time" | SqlDbType.Time
                    return SqlDbType.Time;

                case "datetime2": // 42 | "datetime2" | SqlDbType.DateTime2
                    return SqlDbType.DateTime2;

                case "datetimeoffset": // 43 | "datetimeoffset" | SqlDbType.DateTimeOffset
                    return SqlDbType.DateTimeOffset;

                case "tinyint": // 48 | "tinyint" | SqlDbType.TinyInt
                    return SqlDbType.TinyInt;

                case "smallint": // 52 | "smallint" | SqlDbType.SmallInt
                    return SqlDbType.SmallInt;

                case "int": // 56 | "int" | SqlDbType.Int
                    return SqlDbType.Int;

                case "smalldatetime": // 58 | "smalldatetime" | SqlDbType.SmallDateTime
                    return SqlDbType.SmallDateTime;

                case "real": // 59 | "real" | SqlDbType.Real
                    return SqlDbType.Real;

                case "money": // 60 | "money" | SqlDbType.Money
                    return SqlDbType.Money;

                case "datetime": // 61 | "datetime" | SqlDbType.DateTime
                    return SqlDbType.DateTime;

                case "float": // 62 | "float" | SqlDbType.Float
                    return SqlDbType.Float;

                case "sql_variant": // 98 | "sql_variant" | SqlDbType.Variant
                    return SqlDbType.Variant;

                case "ntext": // 99 | "ntext" | SqlDbType.NText
                    return SqlDbType.NText;

                case "bit": // 104 | "bit" | SqlDbType.Bit
                    return SqlDbType.Bit;

                case "decimal": // 106 | "decimal" | SqlDbType.Decimal
                    return SqlDbType.Decimal;

                case "numeric": // 108 | "numeric" | SqlDbType.Decimal
                    return SqlDbType.Decimal;

                case "smallmoney": // 122 | "smallmoney" | SqlDbType.SmallMoney
                    return SqlDbType.SmallMoney;

                case "bigint": // 127 | "bigint" | SqlDbType.BigInt
                    return SqlDbType.BigInt;

                case "varbinary": // 165 | "varbinary" | SqlDbType.VarBinary
                    return SqlDbType.VarBinary;

                case "varchar": // 167 | "varchar" | SqlDbType.VarChar
                    return SqlDbType.VarChar;

                case "binary": // 173 | "binary" | SqlDbType.Binary
                    return SqlDbType.Binary;

                case "char": // 175 | "char" | SqlDbType.Char
                    return SqlDbType.Char;

                case "timestamp": // 189 | "timestamp" | SqlDbType.Timestamp
                    return SqlDbType.Timestamp;

                case "nvarchar": // 231 | "nvarchar", "sysname" | SqlDbType.NVarChar
                    return SqlDbType.NVarChar;
                case "sysname": // 231 | "nvarchar", "sysname" | SqlDbType.NVarChar
                    return SqlDbType.NVarChar;

                case "nchar": // 239 | "nchar" | SqlDbType.NChar
                    return SqlDbType.NChar;

                case "hierarchyid": // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                    return SqlDbType.Udt;
                case "geometry": // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                    return SqlDbType.Udt;
                case "geography": // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                    return SqlDbType.Udt;

                case "xml": // 241 | "xml" | SqlDbType.Xml
                    return SqlDbType.Xml;

                default:
                    throw new Exception(string.Format(
                        "Unsupported Type: {0}. Please let us know about this type and we will support it: sales@zzzprojects.com",
                        @this));
            }
        }
    }

    public static partial class Extensions
    {
        /// <summary>A string extension method that strip HTML.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string StripHtml(this string @this)
        {
            var path = new StringBuilder(@this);
            var sb = new StringBuilder();

            int pos = 0;

            while (pos < path.Length)
            {
                char ch = path[pos];
                pos++;

                if (ch == '<')
                {
                    // LOOP until we close the html tag
                    while (pos < path.Length)
                    {
                        ch = path[pos];
                        pos++;

                        if (ch == '>')
                        {
                            break;
                        }

                        if (ch == '\'')
                        {
                            // SKIP attribute starting with single quote
                            pos = GetIndexAfterNextSingleQuote(path, pos, true);
                        }
                        else if (ch == '"')
                        {
                            // SKIP attribute starting with double quote
                            pos = GetIndexAfterNextDoubleQuote(path, pos, true);
                        }
                    }
                }
                else
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString();
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that converts the @this to a byte array.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a byte[].</returns>
        public static byte[] ToByteArray(this string @this)
        {
            Encoding encoding = Activator.CreateInstance<ASCIIEncoding>();
            return encoding.GetBytes(@this);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that converts the @this to a directory information.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a DirectoryInfo.</returns>
        public static DirectoryInfo ToDirectoryInfo(this string @this)
        {
            return new DirectoryInfo(@this);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that converts the @this to an enum.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a T.</returns>
        public static T ToEnumZ<T>(this string @this)
        {
            Type enumType = typeof(T);
            return (T) Enum.Parse(enumType, @this);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that converts the @this to a file information.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a FileInfo.</returns>
        public static FileInfo ToFileInfo(this string @this)
        {
            return new FileInfo(@this);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that converts the @this to a MemoryStream.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a MemoryStream.</returns>
        public static Stream ToMemoryStream(this string @this)
        {
            Encoding encoding = Activator.CreateInstance<ASCIIEncoding>();
            return new MemoryStream(encoding.GetBytes(@this));
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that converts the @this to a plural.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a string.</returns>
        public static string ToPlural(this string @this)
        {
            return PluralizationService.CreateService(new CultureInfo("en-US")).Pluralize(@this);
        }

        /// <summary>
        ///     A string extension method that converts the @this to a plural.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cultureInfo">Information describing the culture.</param>
        /// <returns>@this as a string.</returns>
        public static string ToPlural(this string @this, CultureInfo cultureInfo)
        {
            return PluralizationService.CreateService(cultureInfo).Pluralize(@this);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A String extension method that converts the @this to a secure string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a SecureString.</returns>
        public static SecureString ToSecureString(this string @this)
        {
            var secureString = new SecureString();
            foreach (Char c in @this)
                secureString.AppendChar(c);

            return secureString;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that converts the @this to a title case.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a string.</returns>
        public static string ToTitleCase(this string @this)
        {
            return new CultureInfo("en-US").TextInfo.ToTitleCase(@this);
        }

        /// <summary>
        ///     A string extension method that converts the @this to a title case.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cultureInfo">Information describing the culture.</param>
        /// <returns>@this as a string.</returns>
        public static string ToTitleCase(this string @this, CultureInfo cultureInfo)
        {
            return cultureInfo.TextInfo.ToTitleCase(@this);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        /// A string extension method that converts the @this to a valid date time or null.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a DateTime?</returns>
        public static DateTime? ToValidDateTimeOrNull(this string @this)
        {
            DateTime date;

            if (DateTime.TryParse(@this, out date))
            {
                return date;
            }

            return null;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that converts the @this to a XDocument.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an XDocument.</returns>
        public static XDocument ToXDocument(this string @this)
        {
            Encoding encoding = Activator.CreateInstance<ASCIIEncoding>();
            using (var ms = new MemoryStream(encoding.GetBytes(@this)))
            {
                return XDocument.Load(ms);
            }
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that converts the @this to an XmlDocument.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an XmlDocument.</returns>
        public static XmlDocument ToXmlDocument(this string @this)
        {
            var doc = new XmlDocument();
            doc.LoadXml(@this);
            return doc;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that truncates.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns>A string.</returns>
        public static string TruncateZ(this string @this, int maxLength)
        {
            const string suffix = "...";

            if (@this == null || @this.Length <= maxLength)
            {
                return @this;
            }

            int strLength = maxLength - suffix.Length;
            return @this.Substring(0, strLength) + suffix;
        }

        /// <summary>
        ///     A string extension method that truncates.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="suffix">The suffix.</param>
        /// <returns>A string.</returns>
        public static string TruncateZ(this string @this, int maxLength, string suffix)
        {
            if (@this == null || @this.Length <= maxLength)
            {
                return @this;
            }

            int strLength = maxLength - suffix.Length;
            return @this.Substring(0, strLength) + suffix;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the value of a UTF-16 encoded character or surrogate pair at a specified position in a string into a
        ///     Unicode code point.
        /// </summary>
        /// <param name="s">A string that contains a character or surrogate pair.</param>
        /// <param name="index">The index position of the character or surrogate pair in .</param>
        /// <returns>
        ///     The 21-bit Unicode code point represented by the character or surrogate pair at the position in the parameter
        ///     specified by the  parameter.
        /// </returns>
        public static Int32 ConvertToUtf32(this String s, Int32 index)
        {
            return Char.ConvertToUtf32(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the numeric Unicode character at the specified position in a specified string to a double-precision
        ///     floating point number.
        /// </summary>
        /// <param name="s">A .</param>
        /// <param name="index">The character position in .</param>
        /// <returns>
        ///     The numeric value of the character at position  in  if that character represents a number; otherwise, -1.
        /// </returns>
        public static Double GetNumericValue(this String s, Int32 index)
        {
            return Char.GetNumericValue(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Categorizes the character at the specified position in a specified string into a group identified by one of
        ///     the  values.
        /// </summary>
        /// <param name="s">A .</param>
        /// <param name="index">The character position in .</param>
        /// <returns>A  enumerated constant that identifies the group that contains the character at position  in .</returns>
        public static UnicodeCategory GetUnicodeCategory(this String s, Int32 index)
        {
            return Char.GetUnicodeCategory(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a control
        ///     character.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a control character; otherwise, false.</returns>
        public static Boolean IsControl(this String s, Int32 index)
        {
            return Char.IsControl(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a decimal
        ///     digit.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a decimal digit; otherwise, false.</returns>
        public static Boolean IsDigit(this String s, Int32 index)
        {
            return Char.IsDigit(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the  object at the specified position in a string is a high surrogate.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>
        ///     true if the numeric value of the specified character in the  parameter ranges from U+D800 through U+DBFF;
        ///     otherwise, false.
        /// </returns>
        public static Boolean IsHighSurrogate(this String s, Int32 index)
        {
            return Char.IsHighSurrogate(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a Unicode
        ///     letter.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a letter; otherwise, false.</returns>
        public static Boolean IsLetter(this String s, Int32 index)
        {
            return Char.IsLetter(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a letter or
        ///     a decimal digit.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a letter or a decimal digit; otherwise, false.</returns>
        public static Boolean IsLetterOrDigit(this String s, Int32 index)
        {
            return Char.IsLetterOrDigit(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a lowercase
        ///     letter.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a lowercase letter; otherwise, false.</returns>
        public static Boolean IsLower(this String s, Int32 index)
        {
            return Char.IsLower(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the  object at the specified position in a string is a low surrogate.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>
        ///     true if the numeric value of the specified character in the  parameter ranges from U+DC00 through U+DFFF;
        ///     otherwise, false.
        /// </returns>
        public static Boolean IsLowSurrogate(this String s, Int32 index)
        {
            return Char.IsLowSurrogate(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a number.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a number; otherwise, false.</returns>
        public static Boolean IsNumber(this String s, Int32 index)
        {
            return Char.IsNumber(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a
        ///     punctuation mark.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a punctuation mark; otherwise, false.</returns>
        public static Boolean IsPunctuation(this String s, Int32 index)
        {
            return Char.IsPunctuation(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a separator
        ///     character.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a separator character; otherwise, false.</returns>
        public static Boolean IsSeparator(this String s, Int32 index)
        {
            return Char.IsSeparator(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string has a surrogate code unit.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>
        ///     true if the character at position  in  is a either a high surrogate or a low surrogate; otherwise, false.
        /// </returns>
        public static Boolean IsSurrogate(this String s, Int32 index)
        {
            return Char.IsSurrogate(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether two adjacent  objects at a specified position in a string form a surrogate pair.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The starting position of the pair of characters to evaluate within .</param>
        /// <returns>
        ///     true if the  parameter includes adjacent characters at positions  and  + 1, and the numeric value of the
        ///     character at position  ranges from U+D800 through U+DBFF, and the numeric value of the character at position
        ///     +1 ranges from U+DC00 through U+DFFF; otherwise, false.
        /// </returns>
        public static Boolean IsSurrogatePair(this String s, Int32 index)
        {
            return Char.IsSurrogatePair(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as a symbol
        ///     character.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is a symbol character; otherwise, false.</returns>
        public static Boolean IsSymbol(this String s, Int32 index)
        {
            return Char.IsSymbol(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as an
        ///     uppercase letter.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is an uppercase letter; otherwise, false.</returns>
        public static Boolean IsUpper(this String s, Int32 index)
        {
            return Char.IsUpper(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the character at the specified position in a specified string is categorized as white space.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <param name="index">The position of the character to evaluate in .</param>
        /// <returns>true if the character at position  in  is white space; otherwise, false.</returns>
        public static Boolean IsWhiteSpace(this String s, Int32 index)
        {
            return Char.IsWhiteSpace(s, index);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Compares two specified  objects by evaluating the numeric values of the corresponding  objects in each string.
        /// </summary>
        /// <param name="strA">The first string to compare.</param>
        /// <param name="strB">The second string to compare.</param>
        /// <returns>
        ///     An integer that indicates the lexical relationship between the two comparands.ValueCondition Less than zero
        ///     is less than . Zero  and  are equal. Greater than zero  is greater than .
        /// </returns>
        public static Int32 CompareOrdinalZ(this String strA, String strB)
        {
            return String.CompareOrdinal(strA, strB);
        }

        /// <summary>
        ///     Compares substrings of two specified  objects by evaluating the numeric values of the corresponding  objects
        ///     in each substring.
        /// </summary>
        /// <param name="strA">The first string to use in the comparison.</param>
        /// <param name="indexA">The starting index of the substring in .</param>
        /// <param name="strB">The second string to use in the comparison.</param>
        /// <param name="indexB">The starting index of the substring in .</param>
        /// <param name="length">The maximum number of characters in the substrings to compare.</param>
        /// <returns>
        ///     A 32-bit signed integer that indicates the lexical relationship between the two comparands.ValueCondition
        ///     Less than zero The substring in  is less than the substring in . Zero The substrings are equal, or  is zero.
        ///     Greater than zero The substring in  is greater than the substring in .
        /// </returns>
        public static Int32 CompareOrdinalZ(this String strA, Int32 indexA, String strB, Int32 indexB, Int32 length)
        {
            return String.CompareOrdinal(strA, indexA, strB, indexB, length);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Concatenates two specified instances of .
        /// </summary>
        /// <param name="str0">The first string to concatenate.</param>
        /// <param name="str1">The second string to concatenate.</param>
        /// <returns>The concatenation of  and .</returns>
        public static String Concat(this String str0, String str1)
        {
            return String.Concat(str0, str1);
        }

        /// <summary>
        ///     Concatenates three specified instances of .
        /// </summary>
        /// <param name="str0">The first string to concatenate.</param>
        /// <param name="str1">The second string to concatenate.</param>
        /// <param name="str2">The third string to concatenate.</param>
        /// <returns>The concatenation of , , and .</returns>
        public static String Concat(this String str0, String str1, String str2)
        {
            return String.Concat(str0, str1, str2);
        }

        /// <summary>
        ///     Concatenates four specified instances of .
        /// </summary>
        /// <param name="str0">The first string to concatenate.</param>
        /// <param name="str1">The second string to concatenate.</param>
        /// <param name="str2">The third string to concatenate.</param>
        /// <param name="str3">The fourth string to concatenate.</param>
        /// <returns>The concatenation of , , , and .</returns>
        public static String Concat(this String str0, String str1, String str2, String str3)
        {
            return String.Concat(str0, str1, str2, str3);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Creates a new instance of  with the same value as a specified .
        /// </summary>
        /// <param name="str">The string to copy.</param>
        /// <returns>A new string with the same value as .</returns>
        public static String Copy(this String str)
        {
            return String.Copy(str);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Replaces one or more format items in a specified string with the string representation of a specified object.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The object to format.</param>
        /// <returns>A copy of  in which any format items are replaced by the string representation of .</returns>
        public static String Format(this String format, Object arg0)
        {
            return String.Format(format, arg0);
        }

        /// <summary>
        ///     Replaces the format items in a specified string with the string representation of two specified objects.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <returns>A copy of  in which format items are replaced by the string representations of  and .</returns>
        public static String Format(this String format, Object arg0, Object arg1)
        {
            return String.Format(format, arg0, arg1);
        }

        /// <summary>
        ///     Replaces the format items in a specified string with the string representation of three specified objects.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <param name="arg2">The third object to format.</param>
        /// <returns>
        ///     A copy of  in which the format items have been replaced by the string representations of , , and .
        /// </returns>
        public static String Format(this String format, Object arg0, Object arg1, Object arg2)
        {
            return String.Format(format, arg0, arg1, arg2);
        }

        /// <summary>
        ///     Replaces the format item in a specified string with the string representation of a corresponding object in a
        ///     specified array.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>
        ///     A copy of  in which the format items have been replaced by the string representation of the corresponding
        ///     objects in .
        /// </returns>
        public static String Format(this String format, Object[] args)
        {
            return String.Format(format, args);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Retrieves the system&#39;s reference to the specified .
        /// </summary>
        /// <param name="str">A string to search for in the intern pool.</param>
        /// <returns>
        ///     The system&#39;s reference to , if it is interned; otherwise, a new reference to a string with the value of .
        /// </returns>
        public static String InternZ(this String str)
        {
            return String.Intern(str);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Retrieves a reference to a specified .
        /// </summary>
        /// <param name="str">The string to search for in the intern pool.</param>
        /// <returns>A reference to  if it is in the common language runtime intern pool; otherwise, null.</returns>
        public static String IsInternedZ(this String str)
        {
            return String.IsInterned(str);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the  parameter is null or , or if  consists exclusively of white-space characters.</returns>
        public static Boolean IsNullOrWhiteSpaceZ(this String value)
        {
            return String.IsNullOrWhiteSpace(value);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Concatenates all the elements of a string array, using the specified separator between each element.
        /// </summary>
        /// <param name="separator">
        ///     The string to use as a separator.  is included in the returned string only if  has more
        ///     than one element.
        /// </param>
        /// <param name="value">An array that contains the elements to concatenate.</param>
        /// <returns>
        ///     A string that consists of the elements in  delimited by the  string. If  is an empty array, the method
        ///     returns .
        /// </returns>
        public static String Join(this String separator, String[] value)
        {
            return String.Join(separator, value);
        }

        /// <summary>
        ///     Concatenates the elements of an object array, using the specified separator between each element.
        /// </summary>
        /// <param name="separator">
        ///     The string to use as a separator.  is included in the returned string only if  has more
        ///     than one element.
        /// </param>
        /// <param name="values">An array that contains the elements to concatenate.</param>
        /// <returns>
        ///     A string that consists of the elements of  delimited by the  string. If  is an empty array, the method
        ///     returns .
        /// </returns>
        public static String Join(this String separator, Object[] values)
        {
            return String.Join(separator, values);
        }

        /// <summary>
        ///     A String extension method that joins.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="separator">
        ///     The string to use as a separator.  is included in the returned string only if  has more
        ///     than one element.
        /// </param>
        /// <param name="values">An array that contains the elements to concatenate.</param>
        /// <returns>A String.</returns>
        public static String Join<T>(this String separator, IEnumerable<T> values)
        {
            return String.Join(separator, values);
        }

        /// <summary>
        ///     Concatenates all the elements of a string array, using the specified separator between each element.
        /// </summary>
        /// <param name="separator">
        ///     The string to use as a separator.  is included in the returned string only if  has more
        ///     than one element.
        /// </param>
        /// <param name="values">An array that contains the elements to concatenate.</param>
        /// <returns>
        ///     A string that consists of the elements in  delimited by the  string. If  is an empty array, the method
        ///     returns .
        /// </returns>
        public static String Join(this String separator, IEnumerable<String> values)
        {
            return String.Join(separator, values);
        }

        /// <summary>
        ///     Concatenates the specified elements of a string array, using the specified separator between each element.
        /// </summary>
        /// <param name="separator">
        ///     The string to use as a separator.  is included in the returned string only if  has more
        ///     than one element.
        /// </param>
        /// <param name="value">An array that contains the elements to concatenate.</param>
        /// <param name="startIndex">The first element in  to use.</param>
        /// <param name="count">The number of elements of  to use.</param>
        /// <returns>
        ///     A string that consists of the strings in  delimited by the  string. -or- if  is zero,  has no elements, or
        ///     and all the elements of  are .
        /// </returns>
        public static String Join(this String separator, String[] value, Int32 startIndex, Int32 count)
        {
            return String.Join(separator, value, startIndex, count);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the specified regular expression finds a match in the specified input string.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <returns>true if the regular expression finds a match; otherwise, false.</returns>
        public static Boolean IsMatch(this String input, String pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        ///     Indicates whether the specified regular expression finds a match in the specified input string, using the
        ///     specified matching options.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
        /// <returns>true if the regular expression finds a match; otherwise, false.</returns>
        public static Boolean IsMatch(this String input, String pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Searches the specified input string for the first occurrence of the specified regular expression.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <returns>An object that contains information about the match.</returns>
        public static Match Match(this String input, String pattern)
        {
            return Regex.Match(input, pattern);
        }

        /// <summary>
        ///     Searches the input string for the first occurrence of the specified regular expression, using the specified
        ///     matching options.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
        /// <returns>An object that contains information about the match.</returns>
        public static Match Match(this String input, String pattern, RegexOptions options)
        {
            return Regex.Match(input, pattern, options);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Searches the specified input string for all occurrences of a specified regular expression.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <returns>
        ///     A collection of the  objects found by the search. If no matches are found, the method returns an empty
        ///     collection object.
        /// </returns>
        public static MatchCollection Matches(this String input, String pattern)
        {
            return Regex.Matches(input, pattern);
        }

        /// <summary>
        ///     Searches the specified input string for all occurrences of a specified regular expression, using the
        ///     specified matching options.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that specify options for matching.</param>
        /// <returns>
        ///     A collection of the  objects found by the search. If no matches are found, the method returns an empty
        ///     collection object.
        /// </returns>
        public static MatchCollection Matches(this String input, String pattern, RegexOptions options)
        {
            return Regex.Matches(input, pattern, options);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Minimally converts a string to an HTML-encoded string.
        /// </summary>
        /// <param name="s">The string to encode.</param>
        /// <returns>An encoded string.</returns>
        public static String HtmlAttributeEncode(this String s)
        {
            return HttpUtility.HtmlAttributeEncode(s);
        }

        /// <summary>
        ///     Minimally converts a string into an HTML-encoded string and sends the encoded string to a  output stream.
        /// </summary>
        /// <param name="s">The string to encode.</param>
        /// <param name="output">A  output stream.</param>
        public static void HtmlAttributeEncode(this String s, TextWriter output)
        {
            HttpUtility.HtmlAttributeEncode(s, output);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts a string that has been HTML-encoded for HTTP transmission into a decoded string.
        /// </summary>
        /// <param name="s">The string to decode.</param>
        /// <returns>A decoded string.</returns>
        public static String HtmlDecodeZ(this String s)
        {
            return HttpUtility.HtmlDecode(s);
        }

        /// <summary>
        ///     Converts a string that has been HTML-encoded into a decoded string, and sends the decoded string to a  output
        ///     stream.
        /// </summary>
        /// <param name="s">The string to decode.</param>
        /// <param name="output">A  stream of output.</param>
        public static void HtmlDecodeZ(this String s, TextWriter output)
        {
            HttpUtility.HtmlDecode(s, output);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts a string to an HTML-encoded string.
        /// </summary>
        /// <param name="s">The string to encode.</param>
        /// <returns>An encoded string.</returns>
        public static String HtmlEncodeZ(this String s)
        {
            return HttpUtility.HtmlEncode(s);
        }

        /// <summary>
        ///     Converts a string into an HTML-encoded string, and returns the output as a  stream of output.
        /// </summary>
        /// <param name="s">The string to encode.</param>
        /// <param name="output">A  output stream.</param>
        public static void HtmlEncodeZ(this String s, TextWriter output)
        {
            HttpUtility.HtmlEncode(s, output);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Encodes a string.
        /// </summary>
        /// <param name="value">A string to encode.</param>
        /// <returns>An encoded string.</returns>
        public static String JavaScriptStringEncode(this String value)
        {
            return HttpUtility.JavaScriptStringEncode(value);
        }

        /// <summary>
        ///     Encodes a string.
        /// </summary>
        /// <param name="value">A string to encode.</param>
        /// <param name="addDoubleQuotes">
        ///     A value that indicates whether double quotation marks will be included around the
        ///     encoded string.
        /// </param>
        /// <returns>An encoded string.</returns>
        public static String JavaScriptStringEncode(this String value, Boolean addDoubleQuotes)
        {
            return HttpUtility.JavaScriptStringEncode(value, addDoubleQuotes);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Parses a query string into a  using  encoding.
        /// </summary>
        /// <param name="query">The query string to parse.</param>
        /// <returns>A  of query parameters and values.</returns>
        public static NameValueCollection ParseQueryStringZ(this String query)
        {
            return HttpUtility.ParseQueryString(query);
        }

        /// <summary>
        ///     Parses a query string into a  using the specified .
        /// </summary>
        /// <param name="query">The query string to parse.</param>
        /// <param name="encoding">The  to use.</param>
        /// <returns>A  of query parameters and values.</returns>
        public static NameValueCollection ParseQueryStringZ(this String query, Encoding encoding)
        {
            return HttpUtility.ParseQueryString(query, encoding);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts a string that has been encoded for transmission in a URL into a decoded string.
        /// </summary>
        /// <param name="str">The string to decode.</param>
        /// <returns>A decoded string.</returns>
        public static String UrlDecodeZ(this String str)
        {
            return HttpUtility.UrlDecode(str);
        }

        /// <summary>
        ///     Converts a URL-encoded string into a decoded string, using the specified encoding object.
        /// </summary>
        /// <param name="str">The string to decode.</param>
        /// <param name="e">The  that specifies the decoding scheme.</param>
        /// <returns>A decoded string.</returns>
        public static String UrlDecodeZ(this String str, Encoding e)
        {
            return HttpUtility.UrlDecode(str, e);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts a URL-encoded string into a decoded array of bytes.
        /// </summary>
        /// <param name="str">The string to decode.</param>
        /// <returns>A decoded array of bytes.</returns>
        public static Byte[] UrlDecodeToBytes(this String str)
        {
            return HttpUtility.UrlDecodeToBytes(str);
        }

        /// <summary>
        ///     Converts a URL-encoded string into a decoded array of bytes using the specified decoding object.
        /// </summary>
        /// <param name="str">The string to decode.</param>
        /// <param name="e">The  object that specifies the decoding scheme.</param>
        /// <returns>A decoded array of bytes.</returns>
        public static Byte[] UrlDecodeToBytes(this String str, Encoding e)
        {
            return HttpUtility.UrlDecodeToBytes(str, e);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Encodes a URL string.
        /// </summary>
        /// <param name="str">The text to encode.</param>
        /// <returns>An encoded string.</returns>
        public static String UrlEncodeZ(this String str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        ///     Encodes a URL string using the specified encoding object.
        /// </summary>
        /// <param name="str">The text to encode.</param>
        /// <param name="e">The  object that specifies the encoding scheme.</param>
        /// <returns>An encoded string.</returns>
        public static String UrlEncodeZ(this String str, Encoding e)
        {
            return HttpUtility.UrlEncode(str, e);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Converts a string into a URL-encoded array of bytes.
        /// </summary>
        /// <param name="str">The string to encode.</param>
        /// <returns>An encoded array of bytes.</returns>
        public static Byte[] UrlEncodeToBytes(this String str)
        {
            return HttpUtility.UrlEncodeToBytes(str);
        }

        /// <summary>
        ///     Converts a string into a URL-encoded array of bytes using the specified encoding object.
        /// </summary>
        /// <param name="str">The string to encode.</param>
        /// <param name="e">The  that specifies the encoding scheme.</param>
        /// <returns>An encoded array of bytes.</returns>
        public static Byte[] UrlEncodeToBytes(this String str, Encoding e)
        {
            return HttpUtility.UrlEncodeToBytes(str, e);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Encodes the path portion of a URL string for reliable HTTP transmission from the Web server to a client.
        /// </summary>
        /// <param name="str">The text to encode.</param>
        /// <returns>The encoded text.</returns>
        public static String UrlPathEncodeZ(this String str)
        {
            return HttpUtility.UrlPathEncode(str);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that query if 'obj' is valid email.
        /// </summary>
        /// <param name="obj">The obj to act on.</param>
        /// <returns>true if valid email, false if not.</returns>
        public static bool IsValidEmail(this string obj)
        {
            return Regex.IsMatch(obj,
                @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z0-9]{1,30})(\]?)$");
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that query if 'obj' is valid IP.
        /// </summary>
        /// <param name="obj">The obj to act on.</param>
        /// <returns>true if valid ip, false if not.</returns>
        public static bool IsValidIP(this string obj)
        {
            return Regex.IsMatch(obj,
                @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
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
        public static bool InZ(this String @this, params String[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A T extension method that query if '@this' is not null.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if not null, false if not.</returns>
        public static bool IsNotNull(this String @this)
        {
            return @this != null;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A T extension method that query if '@this' is null.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if null, false if not.</returns>
        public static bool IsNull(this String @this)
        {
            return @this == null;
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
        public static bool NotIn(this String @this, params String[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts the Decimal from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted Decimal.</returns>
        public static decimal ExtractDecimal(this string @this)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < @this.Length; i++)
            {
                if (Char.IsDigit(@this[i]) || @this[i] == '.')
                {
                    if (sb.Length == 0 && i > 0 && @this[i - 1] == '-')
                    {
                        sb.Append('-');
                    }
                    sb.Append(@this[i]);
                }
            }

            return Convert.ToDecimal(sb.ToString());
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts the Double from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted Double.</returns>
        public static double ExtractDouble(this string @this)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < @this.Length; i++)
            {
                if (Char.IsDigit(@this[i]) || @this[i] == '.')
                {
                    if (sb.Length == 0 && i > 0 && @this[i - 1] == '-')
                    {
                        sb.Append('-');
                    }
                    sb.Append(@this[i]);
                }
            }

            return Convert.ToDouble(sb.ToString());
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts the Int16 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted Int16.</returns>
        public static short ExtractInt16(this string @this)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < @this.Length; i++)
            {
                if (Char.IsDigit(@this[i]))
                {
                    if (sb.Length == 0 && i > 0 && @this[i - 1] == '-')
                    {
                        sb.Append('-');
                    }
                    sb.Append(@this[i]);
                }
            }

            return Convert.ToInt16(sb.ToString());
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts the Int32 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted Int32.</returns>
        public static int ExtractInt32(this string @this)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < @this.Length; i++)
            {
                if (Char.IsDigit(@this[i]))
                {
                    if (sb.Length == 0 && i > 0 && @this[i - 1] == '-')
                    {
                        sb.Append('-');
                    }
                    sb.Append(@this[i]);
                }
            }

            return Convert.ToInt32(sb.ToString());
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts the Int64 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted Int64.</returns>
        public static long ExtractInt64(this string @this)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < @this.Length; i++)
            {
                if (Char.IsDigit(@this[i]))
                {
                    if (sb.Length == 0 && i > 0 && @this[i - 1] == '-')
                    {
                        sb.Append('-');
                    }
                    sb.Append(@this[i]);
                }
            }

            return Convert.ToInt64(sb.ToString());
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts all Decimal from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>All extracted Decimal.</returns>
        public static decimal[] ExtractManyDecimal(this string @this)
        {
            return Regex.Matches(@this, @"[-]?\d+(\.\d+)?")
                .Cast<Match>()
                .Select(x => Convert.ToDecimal(x.Value))
                .ToArray();
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts all Double from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>All extracted Double.</returns>
        public static double[] ExtractManyDouble(this string @this)
        {
            return Regex.Matches(@this, @"[-]?\d+(\.\d+)?")
                .Cast<Match>()
                .Select(x => Convert.ToDouble(x.Value))
                .ToArray();
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts all Int16 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>All extracted Int16.</returns>
        public static short[] ExtractManyInt16(this string @this)
        {
            return Regex.Matches(@this, @"[-]?\d+")
                .Cast<Match>()
                .Select(x => Convert.ToInt16(x.Value))
                .ToArray();
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts all Int32 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>All extracted Int32.</returns>
        public static int[] ExtractManyInt32(this string @this)
        {
            return Regex.Matches(@this, @"[-]?\d+")
                .Cast<Match>()
                .Select(x => Convert.ToInt32(x.Value))
                .ToArray();
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts all Int64 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>All extracted Int64.</returns>
        public static long[] ExtractManyInt64(this string @this)
        {
            return Regex.Matches(@this, @"[-]?\d+")
                .Cast<Match>()
                .Select(x => Convert.ToInt64(x.Value))
                .ToArray();
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts all UInt16 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>All extracted UInt16.</returns>
        public static ushort[] ExtractManyUInt16(this string @this)
        {
            return Regex.Matches(@this, @"\d+")
                .Cast<Match>()
                .Select(x => Convert.ToUInt16(x.Value))
                .ToArray();
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts all UInt32 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>All extracted UInt32.</returns>
        public static uint[] ExtractManyUInt32(this string @this)
        {
            return Regex.Matches(@this, @"\d+")
                .Cast<Match>()
                .Select(x => Convert.ToUInt32(x.Value))
                .ToArray();
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts all UInt64 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>All extracted UInt64.</returns>
        public static ulong[] ExtractManyUInt64(this string @this)
        {
            return Regex.Matches(@this, @"\d+")
                .Cast<Match>()
                .Select(x => Convert.ToUInt64(x.Value))
                .ToArray();
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts the UInt16 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted UInt16.</returns>
        public static ushort ExtractUInt16(this string @this)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < @this.Length; i++)
            {
                if (Char.IsDigit(@this[i]))
                {
                    sb.Append(@this[i]);
                }
            }

            return Convert.ToUInt16(sb.ToString());
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts the UInt32 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted UInt32.</returns>
        public static uint ExtractUInt32(this string @this)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < @this.Length; i++)
            {
                if (Char.IsDigit(@this[i]))
                {
                    sb.Append(@this[i]);
                }
            }

            return Convert.ToUInt32(sb.ToString());
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that extracts the UInt64 from the string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The extracted UInt64.</returns>
        public static ulong ExtractUInt64(this string @this)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < @this.Length; i++)
            {
                if (Char.IsDigit(@this[i]))
                {
                    sb.Append(@this[i]);
                }
            }

            return Convert.ToUInt64(sb.ToString());
        }
    }
}