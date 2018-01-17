// Decompiled with JetBrains decompiler
// Type: ExtensionMethods.Extensions
// Assembly: ExtensionMethods, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A78E21D-808B-4A21-BFAA-D781C27D2CD5
// Assembly location: ...\bin\Debug\ExtensionMethods.dll

using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using HSNXT.RegularExpressions;

namespace HSNXT
{
    public static partial class Extensions
    {
        private static readonly Regex RegexCapitalize = new RegexCapitalize();

        private static readonly Regex ToLinkRegex =
            new Regex(
                "(((?<scheme>http(s)?):\\/\\/)?([\\w-]+?\\.\\w+)+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\,]*)?)",
                RegexOptions.Multiline | RegexOptions.Compiled);

        /// <summary>
        /// Replaces the format item in a specified string with the string representation of a corresponding object in a specified array.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>s if args is null or empty, or a copy of format in which the format items have been replaced by the string representation of the corresponding objects in args.</returns>
        /// <exception cref="T:System.ArgumentNullException">format or args is null.</exception>
        /// <exception cref="T:System.FormatException">format is not valid.</exception>
        public static string ToFormat(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        /// <summary>Capitalizes the first letter of each word.</summary>
        /// <param name="value">A string to capitalize.</param>
        /// <returns>A capitalized string</returns>
        public static string Capitalize(this string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? value
                : RegexCapitalize.Replace(value, m => m.Groups[1].Value.ToUpperInvariant());
        }

        /// <summary>
        /// Determines whether this string has the same value as any other string in the values array ignoring Case.
        /// </summary>
        /// <param name="s">Original string.</param>
        /// <param name="values">A string array that contains zero or more strings to compare ignoring case.</param>
        /// <returns>true if the original string value is the same as any string in the compareTo array ignoring case.</returns>
        public static bool IsLikeAny(this string s, params string[] values)
        {
            if (s != null && values != null)
                return values.Any(str => str.Equals(s, StringComparison.OrdinalIgnoreCase));
            if (s == null)
                return values == null;
            return false;
        }

        /// <summary>
        /// Determines whether this string has the same value as all the other strings in the values array ignoring Case.
        /// </summary>
        /// <param name="s">Original string.</param>
        /// <param name="values">A string array that contains zero or more strings to compare ignoring case.</param>
        /// <returns>true if the original string value is the same as all the strings in the values array ignoring case.</returns>
        public static bool IsLikeAll(this string s, params string[] values)
        {
            if (s != null && values != null)
                return values.All(str => str.Equals(s, StringComparison.OrdinalIgnoreCase));
            if (s == null)
                return values == null;
            return false;
        }

        /// <summary>
        /// Replaces the named format item in a specified string with the string representation of a corresponding object in a specified array.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>A copy of format in which the format items have been replaced by the string representation of the corresponding objects in args.</returns>
        /// <exception cref="T:System.ArgumentNullException">format or args is null.</exception>
        /// <exception cref="T:System.FormatException">format is not valid.</exception>
        [Obsolete("Use C# 6.0 interpolated strings instead.")]
        public static string ToFormatNamed(this string format, params object[] args)
        {
            return format.FormatNamed(null, args).ToString();
        }

        /// <summary>
        /// Removes a trimString from the end of a string, optionally ignoring case.
        /// </summary>
        /// <param name="s">Original string to trim.</param>
        /// <param name="trimString">string to remove.</param>
        /// <param name="caseSensitive">true if comparison should be case sensitive </param>
        /// <returns></returns>
        public static string TrimEnd(this string s, string trimString, bool caseSensitive = true)
        {
            if (s == null || trimString == null || s.Length < trimString.Length)
                return s;
            int num;
            for (num = 1; num <= trimString.Length; ++num)
            {
                if ((caseSensitive
                        ? ((int) trimString[trimString.Length - num] != (int) s[s.Length - num] ? 1 : 0)
                        : ((int) char.ToLowerInvariant(trimString[trimString.Length - num]) !=
                           (int) char.ToLowerInvariant(s[s.Length - num])
                            ? 1
                            : 0)) != 0)
                    return s;
            }
            return s.Substring(0, s.Length - num + 1);
        }

        /// <summary>
        /// Scans a string for valid http Urls and converts them to Html links.
        /// </summary>
        /// <param name="text">String to scan</param>
        /// <param name="value">Anchor value. If null, the link wil be used.</param>
        /// <returns></returns>
        public static string ToLink(this string text, string value = null)
        {
            return ToLinkRegex.Replace(text, match =>
            {
                var uri = match.ToString();
                var str = match.Groups["scheme"].Value == "https" ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;
                return
                    $@"<a href=""{
                            (object) new UriBuilder(uri) {Scheme = str}.Uri.ToString()
                        }"">{(object) (value ?? uri)}</a>";
            });
        }

        /// <summary>
        /// Removes trimString string from the beginning of a string.
        /// </summary>
        /// <param name="s">Original string.</param>
        /// <param name="trimString">A string to be trimmed</param>
        /// <param name="caseSensitive">true if comparsion should be case sensitive.</param>
        /// <returns></returns>
        public static string TrimStart(this string s, string trimString, bool caseSensitive = true)
        {
            if (s == null || trimString == null || s.Length < trimString.Length)
                return s;
            int startIndex;
            for (startIndex = 0; startIndex < trimString.Length; ++startIndex)
            {
                if ((caseSensitive
                        ? ((int) trimString[startIndex] != (int) s[startIndex] ? 1 : 0)
                        : ((int) char.ToLowerInvariant(trimString[startIndex]) !=
                           (int) char.ToLowerInvariant(s[startIndex])
                            ? 1
                            : 0)) != 0)
                    return s;
            }
            return s.Substring(startIndex);
        }

        /// <summary>
        /// Determines whether the beginning of this string instance matches the specified string ignoring case.
        /// </summary>
        /// <param name="s">String to test.</param>
        /// <param name="value"> The string to compare.</param>
        /// <returns> true if the value parameter matches the beginning of this string ignoring case; otherwise, false.</returns>
        public static bool StartsLike(this string s, string value)
        {
            if (s == null)
                return null == value;
            return s.StartsWith(value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines whether the end of this string instance matches the specified string ignoring case.
        /// </summary>
        /// <param name="s">String to test.</param>
        /// <param name="value"> The string to compare.</param>
        /// <returns> true if the value parameter matches the beginning of this string ignoring case; otherwise, false.</returns>
        public static bool EndsLike(this string s, string value)
        {
            if (s == null)
                return null == value;
            return s.EndsWith(value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>Computes MD5 hash of a string.</summary>
        /// <param name="value">String to hash.</param>
        /// <param name="encoding">An encoding for conversion. If null, UTF8 will be used .</param>
        /// <returns>MD5 hash in hexadecimal format.</returns>
        public static string ToMd5(this string value, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            if (encoding == null)
                encoding = Encoding.UTF8;
            var stringBuilder = new StringBuilder();
            using (var md5 = MD5.Create())
            {
                foreach (var num in md5.ComputeHash(encoding.GetBytes(value)))
                    stringBuilder.Append(num.ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Parses an integer from a string if possible, or returns a default integer.
        /// </summary>
        /// <param name="value">String to parse into an integer.</param>
        /// <param name="defaultValue">An value to return if parsing fails. </param>
        /// <returns>An integer.</returns>
        public static int ToIntOrDefault(this string value, int defaultValue = 0)
        {
            return int.TryParse(value, out var result) ? result : defaultValue;
        }

        /// <summary>
        /// Parses a double from a string if possible, or returns a default value.
        /// </summary>
        /// <param name="value">String to parse into a double.</param>
        /// <param name="defaultValue">A value to return if parsing fails. </param>
        /// <returns>A double.</returns>
        public static double ToDoubleOrDefault(this string value, double defaultValue = 0.0)
        {
            return double.TryParse(value, out var result) ? result : defaultValue;
        }

        /// <summary>Converts a string into Base64 encoding.</summary>
        /// <param name="value">A string</param>
        /// <param name="encoding">An encoding for conversion. If null, UTF8 will be used .</param>
        /// <returns>Base64 encoded string</returns>
        public static string ToBase64(this string value, Encoding encoding = null)
        {
            if (value == null)
                return null;
            if (encoding == null)
                encoding = Encoding.UTF8;
            return Convert.ToBase64String(encoding.GetBytes(value));
        }

        /// <summary>Decodes a Base64-encoded string.</summary>
        /// <param name="value">A Base64-encoded string</param>
        /// <param name="encoding">An encoding for conversion. If null, UTF8 will be used .</param>
        /// <returns>Plain string</returns>
        public static string FromBase64(this string value, Encoding encoding = null)
        {
            if (value == null)
                return null;
            if (encoding == null)
                encoding = Encoding.UTF8;
            var bytes = Convert.FromBase64String(value);
            return encoding.GetString(bytes);
        }

        /// <summary>Converts a string into an array of bytes.</summary>
        /// <param name="value">A string to convert.</param>
        /// <param name="encoding">An encoding for conversion. If null, UTF8 will be used .</param>
        /// <returns>A byte array or null.</returns>
        public static byte[] ToBytes(this string value, Encoding encoding = null)
        {
            if (value == null)
                return null;
            if (encoding == null)
                encoding = Encoding.UTF8;
            return encoding.GetBytes(value);
        }

        /// <summary>
        /// Parses a hexadecimal string into an array of bytes, opposite of <code>ToHexString</code>"/&gt;
        /// </summary>
        /// <param name="value">A hexadecimal string to parse. Digits are not separated.</param>
        /// <returns>A byte array or null.</returns>
        /// <exception cref="T:System.FormatException"><paramref name="value" /> contains invalid hexadecimal digit(s).</exception>
        public static byte[] ToBytesFromHex(this string value)
        {
            return value.ToBytesFromHex(string.Empty);
        }

        /// <summary>
        /// Parses a hexadecimal string into an array of bytes, opposite of <code>ToHexString</code>"/&gt;
        /// </summary>
        /// <param name="value">A hexadecimal string to parse.</param>
        /// <param name="separator">String that separates hexadecimal digits.
        /// Use <code>string.Empty</code> if there is no separator between digits.</param>
        /// <returns>A byte array or null.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="separator" /> is null.</exception>
        /// <exception cref="T:System.FormatException"><paramref name="value" /> contains invalid hexadecimal digit(s).</exception>
        public static byte[] ToBytesFromHex(this string value, string separator)
        {
            if (separator == null)
                throw new ArgumentNullException(nameof(separator));
            if (value == null)
                return null;
            var str = separator == string.Empty ? value : value.Replace(separator, string.Empty);
            if ((str.Length & 1) == 1)
                throw new ArgumentException($"The length of {(object) value} is not valid.");
            var numArray = new byte[str.Length / 2];
            var startIndex = 0;
            while (startIndex < str.Length)
            {
                numArray[startIndex / 2] = byte.Parse(str.Substring(startIndex, 2), NumberStyles.HexNumber);
                startIndex += 2;
            }
            return numArray;
        }

        private static StringBuilder FormatNamed(this string format, IFormatProvider provider, params object[] args)
        {
            if (format == null || args == null)
                throw new ArgumentNullException(format == null ? nameof(format) : nameof(args));
            var stringBuilder1 = new StringBuilder(format.Length + args.Length * 10);
            var index1 = -1;
            var index2 = 0;
            var length = format.Length;
            var customFormatter = (ICustomFormatter) null;
            if (provider != null)
                customFormatter = (ICustomFormatter) provider.GetFormat(typeof(ICustomFormatter));
            while (true)
            {
                char ch1;
                bool flag;
                int repeatCount;
                do
                {
                    if (index2 < length)
                    {
                        ch1 = format[index2];
                        ++index2;
                        if (ch1 == 125)
                        {
                            if (index2 < length && format[index2] == 125)
                                ++index2;
                            else
                                ThrowInvalidFormatException();
                        }
                        if (ch1 == 123)
                        {
                            if (index2 >= length || format[index2] != 123)
                                --index2;
                            else
                                goto label_10;
                        }
                        else
                            goto label_12;
                    }
                    if (index2 == length)
                        return stringBuilder1;
                    var index3 = index2 + 1;
                    if (index3 == length)
                        ThrowInvalidFormatException();
                    ++index1;
                    char ch2;
                    do
                    {
                        ++index3;
                        if (index3 == length)
                            ThrowInvalidFormatException();
                        ch2 = format[index3];
                    } while (ch2 >= 48 && ch2 <= 57 || ch2 >= 65 && ch2 <= 122 && index1 < 1000000);
                    if (index1 >= args.Length)
                        ThrowOutOfRangeException();
                    while (index3 < length && (ch2 = format[index3]) == 32)
                        ++index3;
                    flag = false;
                    var num = 0;
                    if (ch2 == 44)
                    {
                        ++index3;
                        while (index3 < length && format[index3] == 32)
                            ++index3;
                        if (index3 == length)
                            ThrowInvalidFormatException();
                        ch2 = format[index3];
                        if (ch2 == 45)
                        {
                            flag = true;
                            ++index3;
                            if (index3 == length)
                                ThrowInvalidFormatException();
                            ch2 = format[index3];
                        }
                        if (ch2 < 48 || ch2 > 57)
                            ThrowInvalidFormatException();
                        do
                        {
                            num = num * 10 + ch2 - 48;
                            ++index3;
                            if (index3 == length)
                                ThrowInvalidFormatException();
                            ch2 = format[index3];
                        } while (ch2 >= 48 && ch2 <= 57 && num < 1000000);
                    }
                    while (index3 < length && (ch2 = format[index3]) == 32)
                        ++index3;
                    var obj = args[index1];
                    var stringBuilder2 = (StringBuilder) null;
                    if (ch2 == 58)
                    {
                        var index4 = index3 + 1;
                        while (true)
                        {
                            if (index4 == length)
                                ThrowInvalidFormatException();
                            ch2 = format[index4];
                            ++index4;
                            if (ch2 != 123)
                            {
                                if (ch2 == 125)
                                {
                                    if (index4 < length && format[index4] == 125)
                                        ++index4;
                                    else
                                        break;
                                }
                            }
                            else if (index4 < length && format[index4] == 123)
                                ++index4;
                            else
                                ThrowInvalidFormatException();
                            if (stringBuilder2 == null)
                                stringBuilder2 = new StringBuilder();
                            stringBuilder2.Append(ch2);
                        }
                        index3 = index4 - 1;
                    }
                    if (ch2 != 125)
                        ThrowInvalidFormatException();
                    index2 = index3 + 1;
                    var format1 = (string) null;
                    var str = (string) null;
                    if (customFormatter != null)
                    {
                        if (stringBuilder2 != null)
                            format1 = stringBuilder2.ToString();
                        str = customFormatter.Format(format1, obj, provider);
                    }
                    if (str == null)
                    {
                        if (obj is IFormattable formattable)
                        {
                            if (format1 == null && stringBuilder2 != null)
                                format1 = stringBuilder2.ToString();
                            str = formattable.ToString(format1, provider);
                        }
                        else if (obj != null)
                            str = obj.ToString();
                    }
                    if (str == null)
                        str = string.Empty;
                    repeatCount = num - str.Length;
                    if (!flag && repeatCount > 0)
                        stringBuilder1.Append(' ', repeatCount);
                    stringBuilder1.Append(str);
                } while (!flag || repeatCount <= 0);
                goto label_76;
                label_10:
                ++index2;
                label_12:
                stringBuilder1.Append(ch1);
                continue;
                label_76:
                stringBuilder1.Append(' ', repeatCount);
            }
        }

        private static void ThrowOutOfRangeException()
        {
            throw new FormatException(
                "The number of named parameters {..} must be less than or equal to the arguments.");
        }

        private static void ThrowInvalidFormatException()
        {
            throw new FormatException("Input string was not in a correct format.");
        }
    }
}