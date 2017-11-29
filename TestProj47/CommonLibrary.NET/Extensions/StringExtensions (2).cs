using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using HSNXT.ComLib;

namespace HSNXT
{
    public static partial class Extensions
    {
        #region Appending
        /// <summary>
        /// Multiply a string N number of times.
        /// </summary>
        /// <param name="str">String to multiply.</param>
        /// <param name="times">Number of times to multiply the string.</param>
        /// <returns>Original string multiplied N times.</returns>
        public static string Times(this string str, int times)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            if (times <= 1) return str;

            var strfinal = string.Empty;
            for (var ndx = 0; ndx < times; ndx++)
                strfinal += str;

            return strfinal;
        }


        /// <summary>
        /// Increases the string to the maximum length specified.
        /// If the string is already greater than maxlength, it is truncated if the flag truncate is true.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="maxLength">Length of the max.</param>
        /// <param name="truncate">if set to <c>true</c> [truncate].</param>
        /// <returns>Increased string.</returns>
        public static string IncreaseTo(this string str, int maxLength, bool truncate)
        {
            if (string.IsNullOrEmpty(str)) return str;
            if (str.Length == maxLength) return str;
            if (str.Length > maxLength && truncate) return str.Truncate(maxLength);

            var original = str;

            while (str.Length < maxLength)
            {
                // Still less after appending by original string.
                if (str.Length + original.Length < maxLength)
                {
                    str += original;
                }
                else // Append partial.
                {
                    str += str.Substring(0, maxLength - str.Length);
                }
            }
            return str;
        }


        /// <summary>
        /// Increases the string to the maximum length specified.
        /// If the string is already greater than maxlength, it is truncated if the flag truncate is true.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="minLength">String minimum length.</param>
        /// <param name="maxLength">Length of the max.</param>
        /// <param name="truncate">if set to <c>true</c> [truncate].</param>
        /// <returns>Randomly increased string.</returns>
        public static string IncreaseRandomly(this string str, int minLength, int maxLength, bool truncate)
        {
            var random = new Random(minLength);
            var randomMaxLength = random.Next(minLength, maxLength);
            return IncreaseTo(str, randomMaxLength, truncate);
        }
        #endregion


        #region Truncation


        /// <summary>
        /// Truncate the text supplied by number of characters specified by <paramref name="maxChars"/>
        /// and then appends the suffix.
        /// </summary>
        /// <param name="txt">String to truncate.</param>
        /// <param name="maxChars">Maximum string length.</param>
        /// <param name="suffix">Suffix to append to string.</param>
        /// <returns>Truncated string with suffix.</returns>
        public static string TruncateWithText(this string txt, int maxChars, string suffix)
        {
            if (string.IsNullOrEmpty(txt))
                return txt;

            if (txt.Length <= maxChars)
                return txt;

            // Now do the truncate and more.
            var partial = txt.Substring(0, maxChars);
            return partial + suffix;
        }
        #endregion


        #region Conversion
        /// <summary>
        /// Convert the text  to bytes.
        /// </summary>
        /// <param name="txt">Text to convert to bytes.</param>
        /// <returns>ASCII bytes representing the string.</returns>
        public static byte[] ToBytesAscii(this string txt)
        {
            return txt.ToBytesEncoding(new ASCIIEncoding());
        }

        /// <summary>
        /// Convert the text to bytes using a specified encoding.
        /// </summary>
        /// <param name="txt">Text to convert to bytes.</param>
        /// <param name="encoding">Encoding to use during the conversion.</param>
        /// <returns>Bytes representing the string.</returns>
        public static byte[] ToBytesEncoding(this string txt, Encoding encoding)
        {
            return string.IsNullOrEmpty(txt) ? new byte[] { } : encoding.GetBytes(txt);
        }


        /// <summary>
        /// Converts an ASCII byte array to a string.
        /// </summary>
        /// <param name="bytes">ASCII bytes.</param>
        /// <returns>String representation of ASCII bytes.</returns>
        public static string StringFromBytesAscii(this byte[] bytes)
        {
            return bytes.StringFromBytesEncoding(new ASCIIEncoding());
        }


        /// <summary>
        /// Converts a byte array to a string using the system default code page.
        /// </summary>
        /// <param name="bytes">Byte array.</param>
        /// <returns>String representation of bytes.</returns>
        public static string StringFromBytes(this byte[] bytes)
        {
            return bytes.StringFromBytesEncoding(Encoding.Default);
        }

        
        /// <summary>
        /// Converts a byte array to a string using a specified encoding.
        /// </summary>
        /// <param name="bytes">Byte array.</param>
        /// <param name="encoding">Encoding to use during the conversion.</param>
        /// <returns>String representation of bytes.</returns>
        public static string StringFromBytesEncoding(this byte[] bytes, Encoding encoding)
        {
            return 0 == bytes.GetLength(0) ? null : encoding.GetString(bytes);
        }


        /// <summary>
        /// Converts "yes/no/true/false/0/1"
        /// </summary>
        /// <param name="txt">String to convert to boolean.</param>
        /// <returns>Boolean converted from string.</returns>
        public static object ToBoolObject(this string txt)
        {
            return ToBool(txt);
        }


        /// <summary>
        /// Converts "yes/no/true/false/0/1"
        /// </summary>
        /// <param name="txt">String to convert to boolean.</param>
        /// <returns>Boolean converted from string.</returns>
        public static bool ToBool(this string txt)
        {            
            if (string.IsNullOrEmpty(txt))
                return false;

            var trimed = txt.Trim().ToLower();
            return trimed == "yes" || trimed == "true" || trimed == "1";
        }


        /// <summary>
        /// Converts a string to an integer and returns it as an object.
        /// </summary>
        /// <param name="txt">String to convert to integer.</param>
        /// <returns>Integer converted from string.</returns>
        /// <remarks>The method takes into starting monetary symbols like $.</remarks>
        public static object ToIntObject(this string txt)
        {
            return ToInt(txt);
        }

        /// <summary>
        /// Converts a string to a long and returns it as an object.
        /// </summary>
        /// <param name="txt">String to convert to long.</param>
        /// <returns>Long converted from string.</returns>
        /// <remarks>The method takes into starting monetary symbols like $.</remarks>
        public static object ToLongObject(this string txt)
        {
            return ToLong(txt);
        }


        /// <summary>
        /// Converts a string to a long.
        /// </summary>
        /// <param name="txt">String to convert to long.</param>
        /// <returns>Long converted from string.</returns>
        /// <remarks>The method takes into starting monetary symbols like $.</remarks>
        public static long ToLong(this string txt)
        {
            return ToNumber(txt, Convert.ToInt64, 0);
        }


        /// <summary>
        /// Converts a string to a double and returns it as an object.
        /// </summary>
        /// <param name="txt">String to convert to double.</param>
        /// <returns>Double converted from string.</returns>
        /// <remarks>The method takes into starting monetary symbols like $.</remarks>
        public static object ToDoubleObject(this string txt)
        {
            return ToDouble(txt);
        }

        /// <summary>
        /// Converts a string to a float and returns it as an object.
        /// </summary>
        /// <param name="txt">String to convert to float.</param>
        /// <returns>Float converted from string.</returns>
        /// <remarks>The method takes into starting monetary symbols like $.</remarks>
        public static object ToFloatObject(this string txt)
        {
            return ToFloat(txt);
        }


        /// <summary>
        /// Converts a string as a float and returns it.
        /// </summary>
        /// <param name="txt">String to convert to float.</param>
        /// <returns>Float converted from string.</returns>
        /// <remarks>The method takes into starting monetary symbols like $.</remarks>
        public static float ToFloat(this string txt)
        {
            return ToNumber(txt, Convert.ToSingle, 0);
        }


        /// <summary>
        /// Converts to a number using the callback.
        /// </summary>
        /// <typeparam name="T">Type to convert to.</typeparam>
        /// <param name="txt">String to convert.</param>
        /// <param name="callback">Conversion callback method.</param>
        /// <param name="defaultValue">Default conversion value.</param>
        /// <returns>Instance of type converted from string.</returns>
        public static T ToNumber<T>(string txt, Func<string, T> callback, T defaultValue)
        {            
            if (string.IsNullOrEmpty(txt))
                return defaultValue;

            var trimed = txt.Trim().ToLower();
            // Parse $ or the system currency symbol.
            if (trimed.StartsWith("$") || trimed.StartsWith(Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol))
            {
                trimed = trimed.Substring(1);
            }
            return callback(trimed);
        }


        /// <summary>
        /// Converts a string to a timespan instance and returns it as an object.
        /// </summary>
        /// <param name="txt">String to convert to a timespan instance.</param>
        /// <returns>Timespan instance converted from string.</returns>
        public static object ToTimeObject(this string txt)
        {
            return ToTime(txt);
        }


        /// <summary>
        /// Converts a string to a timespan instance.
        /// </summary>
        /// <param name="txt">String to convert to timespan instance.</param>
        /// <returns>Timespan instance converted from string.</returns>
        public static TimeSpan ToTime(this string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return TimeSpan.MinValue;

            var trimmed = txt.Trim().ToLower();
            return TimeHelper.Parse(trimmed).Item;
        }


        /// <summary>
        /// Converts a string to a datetime instance.
        /// </summary>
        /// <param name="txt">String to conevert to datetime instance.</param>
        /// <returns>Datetime instance converted from string.</returns>
        public static object ToDateTimeObject(this string txt)
        {
            return ToDateTime(txt);
        }

        #endregion


        #region Hex and Binary
        /// <summary>
        /// Determines whether the string contains valid hexadecimal characters only.
        /// </summary>
        /// <param name="txt">String to check.</param>
        /// <returns>True if the string contains valid hexadecimal characters.</returns>
        /// <remarks>An empty or null string is considered to <b>not</b> contain
        /// valid hexadecimal characters.</remarks>
        public static bool IsHex(this string txt)
        {
            return (!txt.IsNullOrEmpty()) && (txt.ReplaceChars("0123456789ABCDEFabcdef", "                      ").Trim().IsNullOrEmpty());
        }


        /// <summary>
        /// Determines whether the string contains valid binary characters only.
        /// </summary>
        /// <param name="txt">String to check.</param>
        /// <returns>True if the string contains valid binary characters.</returns>
        /// <remarks>An empty or null string is considered to <b>not</b> contain
        /// valid binary characters.</remarks>
        public static bool IsBinary(this string txt)
        {
            return (!txt.IsNullOrEmpty()) && (txt.ReplaceChars("01", "  ").Trim().IsNullOrEmpty());
        }

        /// <summary>
        /// Returns the hexadecimal representation of a decimal number.
        /// </summary>
        /// <param name="txt">Hexadecimal string to convert to decimal.</param>
        /// <returns>Decimal representation of string.</returns>
        public static string DecimalToHex(this string txt)
        {
            return Convert.ToInt32(txt).ToHex();
        }


        /// <summary>
        /// Returns the binary representation of a binary number.
        /// </summary>
        /// <param name="txt">Decimal string to convert to binary.</param>
        /// <returns>Binary representation of string.</returns>
        public static string DecimalToBinary(this string txt)
        {
            return Convert.ToInt32(txt).ToBinary();
        }


        /// <summary>
        /// Returns the decimal representation of a hexadecimal number.
        /// </summary>
        /// <param name="txt">Hexadecimal string to convert to decimal.</param>
        /// <returns>Decimal representation of string.</returns>
        public static string HexToDecimal(this string txt)
        {
            return Convert.ToString(Convert.ToInt32(txt, 16));
        }


        /// <summary>
        /// Returns the binary representation of a hexadecimal number.
        /// </summary>
        /// <param name="txt">Binary string to convert to hexadecimal.</param>
        /// <returns>Hexadecimal representation of string.</returns>
        public static string HexToBinary(this string txt)
        {
            return Convert.ToString(Convert.ToInt32(txt, 16), 2);
        }

        /// <summary>
        /// Converts a byte array to a hexadecimal string representation.
        /// </summary>
        /// <param name="b">Byte array to convert to hexadecimal string.</param>
        /// <returns>String representation of byte array.</returns>
        public static string ByteArrayToHex(this byte[] b)
        {
            return BitConverter.ToString(b).Replace("-", "");
        }


        /// <summary>
        /// Returns the hexadecimal representation of a binary number.
        /// </summary>
        /// <param name="txt">Binary string to convert to hexadecimal.</param>
        /// <returns>Hexadecimal representation of string.</returns>
        public static string BinaryToHex(this string txt)
        {
            return Convert.ToString(Convert.ToInt32(txt, 2), 16);
        }


        /// <summary>
        /// Returns the decimal representation of a binary number.
        /// </summary>
        /// <param name="txt">Binary string to convert to decimal.</param>
        /// <returns>Decimal representation of string.</returns>
        public static string BinaryToDecimal(this string txt)
        {
            return Convert.ToString(Convert.ToInt32(txt, 2));
        }
        #endregion


        #region Replacement
        /// <summary>
        /// Replaces the characters in the originalChars string with the
        /// corresponding characters of the newChars string.
        /// </summary>
        /// <param name="txt">String to operate on.</param>
        /// <param name="originalChars">String with original characters.</param>
        /// <param name="newChars">String with replacement characters.</param>
        /// <example>For an original string equal to "123456654321" and originalChars="35" and
        /// newChars "AB", the result will be "12A4B66B4A21".</example>
        /// <returns>String with replaced characters.</returns>
        public static string ReplaceChars(this string txt, string originalChars, string newChars)
        {
            var returned = "";

            for (var i = 0; i < txt.Length; i++)
            {
                var pos = originalChars.IndexOf(txt.Substring(i, 1), StringComparison.Ordinal);
                
                if (-1 != pos)
                    returned += newChars.Substring(pos, 1);
                else
                    returned += txt.Substring(i, 1);
            }
            return returned;
        }
        #endregion


        #region Lists
        /// <summary>
        /// Prefixes all items in the list w/ the prefix value.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns>List with prefixes.</returns>
        public static List<string> PreFixWith(this List<string> items, string prefix)
        {
            for(var ndx = 0; ndx < items.Count; ndx++)
            {
                items[ndx] = prefix + items[ndx];
            }
            return items;
        }
        #endregion


        #region Matching
        /// <summary>
        /// Determines whether or not the string value supplied represents a "not applicable" string value by matching on na, n.a., n/a etc.
        /// </summary>
        /// <param name="val">String to check.</param>
        /// <param name="useNullOrEmptyStringAsNotApplicable">True to use null or empty string check.</param>
        /// <returns>True if the string value represents a "not applicable" string.</returns>
        public static bool IsNotApplicableValue(this string val, bool useNullOrEmptyStringAsNotApplicable = false)
        {
            var isEmpty = string.IsNullOrEmpty(val);
            if(isEmpty && useNullOrEmptyStringAsNotApplicable) return true;
            if(isEmpty && !useNullOrEmptyStringAsNotApplicable) return false;
            val = val.Trim().ToLower();

            if (val == "na" || val == "n.a." || val == "n/a" || val == "n\\a" || val == "n.a" || val == "not applicable")
                return true;
            return false;
        }


        /// <summary>
        /// Use the Levenshtein algorithm to determine the similarity between
        /// two strings. The higher the number, the more different the two
        /// strings are.
        /// TODO: This method needs to be rewritten to handle very large strings
        /// See <a href="http://www.merriampark.com/ld.htm"></a>.
        /// See <a href="http://en.wikipedia.org/wiki/Levenshtein_distance"></a>.
        /// </summary>
        /// <param name="source">Source string to compare</param>
        /// <param name="comparison">Comparison string</param>
        /// <returns>0 if both strings are identical, otherwise a number indicating the level of difference</returns>
        public static int Levenshtein(this string source, string comparison)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "Can't parse null string");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison", "Can't parse null string");
            }

            var s = source.ToCharArray();
            var t = comparison.ToCharArray();
            var n = source.Length;
            var m = comparison.Length;
            var d = new int[n + 1, m + 1];

            // shortcut calculation for zero-length strings
            if (n == 0) { return m; }
            if (m == 0) { return n; }

            for (var i = 0; i <= n; d[i, 0] = i++) {}
            for (var j = 0; j <= m; d[0, j] = j++) {}

            for (var i = 1; i <= n; i++)
            {
                for (var j = 1; j <= m; j++)
                {
                    var cost = t[j - 1].Equals(s[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(Math.Min(
                        d[i - 1, j] + 1,
                        d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }


        /// <summary>
        /// Calculate the simplified soundex value for the specified string.
        /// See <a href="http://en.wikipedia.org/wiki/Soundex"></a>.
        /// See <a href="http://west-penwith.org.uk/misc/soundex.htm"></a>.
        /// </summary>
        /// <param name="source">String to calculate</param>
        /// <returns>Soundex value of string</returns>
        public static string SimplifiedSoundex(this string source)
        {
            return source.SimplifiedSoundex(4);
        }

        /// <summary>
        /// Calculate the simplified soundex value for the specified string.
        /// See <a href="http://en.wikipedia.org/wiki/Soundex"></a>.
        /// See <a href="http://west-penwith.org.uk/misc/soundex.htm"></a>.
        /// </summary>
        /// <param name="source">String to calculate</param>
        /// <param name="length">Length of soundex value (typically 4)</param>
        /// <returns>Soundex value of string</returns>
        public static string SimplifiedSoundex(this string source, int length)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (source.Length < 3)
            {
                throw new ArgumentException(
                    "Source string must be at least two characters", "source");
            }

            var t = source.ToUpper().ToCharArray();
            var buffer = new StringBuilder();

            short prev = -1;

            foreach (var c in t)
            {
                short curr = 0;
                switch (c)
                {
                    case 'A':
                    case 'E':
                    case 'I':
                    case 'O':
                    case 'U':
                    case 'H':
                    case 'W':
                    case 'Y':
                        curr = 0;
                        break;
                    case 'B':
                    case 'F':
                    case 'P':
                    case 'V':
                        curr = 1;
                        break;
                    case 'C':
                    case 'G':
                    case 'J':
                    case 'K':
                    case 'Q':
                    case 'S':
                    case 'X':
                    case 'Z':
                        curr = 2;
                        break;
                    case 'D':
                    case 'T':
                        curr = 3;
                        break;
                    case 'L':
                        curr = 4;
                        break;
                    case 'M':
                    case 'N':
                        curr = 5;
                        break;
                    case 'R':
                        curr = 6;
                        break;
                    default:
                        throw new ApplicationException(
                            "Invalid state in switch statement");
                }

                /* Change all consecutive duplicate digits to a single digit
                 * by not processing duplicate values. 
                 * Ignore vowels (i.e. zeros). */
                if (curr != prev)
                {
                    buffer.Append(curr);
                }

                prev = curr;
            }

            // Prefix value with first character
            buffer.Remove(0, 1).Insert(0, t.First());
            
            // Remove all vowels (i.e. zeros) from value
            buffer.Replace("0", "");
            
            // Pad soundex value with zeros until output string equals length))))
            while (buffer.Length < length) { buffer.Append('0'); }
            
            // Truncate values that are longer than the supplied length
            return buffer.ToString().Substring(0, length);
        }

        #endregion
    }
}
