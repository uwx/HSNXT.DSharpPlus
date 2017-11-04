using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

// ReSharper disable StringCompareIsCultureSpecific.1
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace TestProj47
{
    internal static class Program
    {
        private static void Main() => Console.WriteLine("Hello World!");
    }

    public class LazySplitter : IEnumerable<string>
    {
        private readonly string _self;
        private readonly char _c;

        public LazySplitter(string self, char c)
        {
            _self = self;
            _c = c;
        }

        public IEnumerator<string> GetEnumerator() => new LazyEnumerator(_self, _c);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class LazySplitterIndex : IEnumerable<(string, int)>
    {
        private readonly string _self;
        private readonly char _c;

        public LazySplitterIndex(string self, char c)
        {
            _self = self;
            _c = c;
        }

        public IEnumerator<(string, int)> GetEnumerator() => new LazyEnumeratorIndex(_self, _c);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class LazyEnumerator : IEnumerator<string>
    {
        private readonly string _self;
        private readonly char _c;
        private int _position;

        public LazyEnumerator(string self, char c)
        {
            _self = self;
            _c = c;
        }

        public bool MoveNext()
        {
            var idx = _self.IndexOf(_c, _position);
            if (idx == -1) return false;

            Current = _self.Substring(_position, idx);

            _position = idx + 1; // + c.Length
            return true;
        }

        public void Reset() => _position = 0;

        public string Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }

    public class LazyEnumeratorIndex : IEnumerator<(string, int)>
    {
        private readonly string _self;
        private readonly char _c;
        private int _position;

        public LazyEnumeratorIndex(string self, char c)
        {
            _self = self;
            _c = c;
        }

        public bool MoveNext()
        {
            var idx = _self.IndexOf(_c, _position);
            if (idx == -1) return false;

            Current = (_self.Substring(_position, idx), _position);

            _position = idx + 1; // + c.Length
            return true;
        }

        public void Reset() => _position = 0;

        public (string, int) Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }

    public static partial class Extensions
    {
        public static IEnumerable<T> ReturnEnumerable<T>(this T item)
        {
            return new[] { item };
        }

        public static string EnumerableToString(this IEnumerable<char> enumerable) => new string(enumerable.ToArray());

        public static string EnumerableToString(this IEnumerable<string> enumerable) => enumerable.StringJoin("");

        public static string EnumerableToString(this IEnumerable<IEnumerable<char>> enumerable) => new string(enumerable.SelectMany(e => e).ToArray());

        /// <summary>
        /// Converts the target float in to a string with the specified number of decimal places.
        /// </summary>
        /// <param name="this">The extended float.</param>
        /// <param name="numDecimalPlaces">The number of decimal places to display.</param>
        /// <returns>A string that represents the target float.</returns>
        public static string ToString(this float @this, uint numDecimalPlaces) {
            var formatString = "{0:n" + numDecimalPlaces + "}";
            return string.Format(CultureInfo.InvariantCulture, formatString, @this);
        }
        
        public static IEnumerable<T[]> TakeWindow<T>(this IEnumerable<T> e, int count)
        {
            var window = new LinkedList<T>();
            foreach (var elem in e)
            {
                if (window.Count == count)
                {
                    yield return window.ToArray();
                    window.RemoveFirst();
                }
                window.AddLast(elem);
            }
            yield return window.ToArray();
        }
        
        public static string ToString<T>(this IEnumerable<T> list, Func<T, string> itemOutput, string seperator = ",")
        {
            list = list ?? new List<T>();
            seperator = seperator ?? "";
            itemOutput = itemOutput ?? (x => x.ToString());
            var builder = new StringBuilder();
            var tempSeperator = "";
            foreach (var item in list)
            {
                builder.Append(tempSeperator).Append(itemOutput(item));
                tempSeperator = seperator;
            }
            return builder.ToString();
        }
        
        public static string ToStringOrDefault<T>(this T? nullable, string defaultValue) where T : struct
        {
            return nullable?.ToString() ?? defaultValue;
        }

        public static string ToStringOrDefault<T>(this T? nullable, string format, string defaultValue) where T : struct, IFormattable
        {
            return nullable?.ToString(format, CultureInfo.CurrentCulture) ?? defaultValue;
        }
        
        /// <summary>
        /// this is really missing from C# - returns the key of the highest value in a dictionary.
        /// </summary>
        /// <typeparam name="TKey">The key type (determined from the dictionary)</typeparam>
        /// <typeparam name="TValue">Value type (determined from the dictionary), must implement IComparable&lt;Value&gt;</typeparam>
        /// <param name="dictionary">The dictionary</param>
        /// <returns>The key of the highest value in the dic.</returns>
        public static TKey Max<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) where TValue : IComparable<TValue>
        {
            if (dictionary == null || dictionary.Count == 0) return default;
            var dicList = dictionary.ToList();
            var maxKvp = dicList.First();
            foreach (var kvp in dicList.Skip(1))
            {
                if (kvp.Value.CompareTo(maxKvp.Value) > 0)
                {
                    maxKvp = kvp;
                }
            }
            return maxKvp.Key;
        }
        
        public static IEnumerable<TDerived> WhereIs<TBase, TDerived>(this IEnumerable<TBase> source)
            where TBase : class
            where TDerived : class, TBase
        {
            return source.OfType<TDerived>();
        }
        private const string IsEmailBigRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                               @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                               @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        private static readonly Regex ObjNotWholePattern = new Regex("[^0-9]", RegexOptions.Compiled);
        private static readonly Regex ObjAlphaNumericPattern = new Regex("[^a-zA-Z0-9]", RegexOptions.Compiled);
        private static readonly Regex ObjAlphaNumericPatternWhite = new Regex("[^a-zA-Z0-9\\s]", RegexOptions.Compiled);
        private static readonly Regex ObjAlphaPatternWhite = new Regex("[^a-zA-Z\\s]", RegexOptions.Compiled);
        private static readonly Regex ObjAlphaDashPattern = new Regex("[^a-zA-Z\\-]", RegexOptions.Compiled);
        private static readonly Regex ObjAlphaPattern = new Regex("[^a-zA-Z]", RegexOptions.Compiled);
        private static readonly Regex IsEmailBigRe = new Regex(IsEmailBigRegex, RegexOptions.Compiled);
        
        public static string OnlyDigits(this string value)
        {
            return new string(value?.Where(char.IsDigit).ToArray());
        }
        
        private static readonly Dictionary<char, int> RomanMap = new Dictionary<char, int>()
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        public static int RomanToInteger(this string roman)
        {
            var number = 0;
            for (var i = 0; i < roman.Length; i++)
            {
                if (i + 1 < roman.Length && RomanMap[roman[i]] < RomanMap[roman[i + 1]])
                {
                    number -= RomanMap[roman[i]];
                }
                else
                {
                    number += RomanMap[roman[i]];
                }
            }
            return number;
        }
        
        public static string ToStringInvariant(this DateTime o)
        {
            return o.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToStringCurrent(this DateTime o)
        {
            return o.ToString(CultureInfo.CurrentCulture);
        }

        public static bool In<T>(this T source, params T[] list)
        {
            if (null == source) throw new ArgumentNullException(nameof(source));
            return list.Contains(source);
        }

        /// <summary>Converts the value of this instance to all the string representations supported by the standard date and time format specifiers and the specified culture-specific formatting information.</summary>
        /// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.</returns>
        public static string[] GetDateTimeFormatsInvariant(this DateTime o)
        {
            return o.GetDateTimeFormats(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to all the string representations supported by the specified standard date and time format specifier.</summary>
        /// <param name="o">This object</param>
        /// <param name="format">A standard date and time format string (see Remarks). </param>
        /// <returns>A string array where each element is the representation of the value of this instance formatted with the <paramref name="format" /> standard date and time format specifier.</returns>
        /// <exception cref="T:System.FormatException">
        /// <paramref name="format" /> is not a valid standard date and time format specifier character.</exception>
        public static string[] GetDateTimeFormatsInvariant(this DateTime o, char format)
        {
            return o.GetDateTimeFormats(format, CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting information.</summary>
        /// <returns>A Boolean value equivalent to the value of this instance.</returns>
        public static bool ToBooleanInvariant(this IConvertible o)
        {
            return o.ToBoolean(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent Unicode character using the specified culture-specific formatting information.</summary>
        /// <returns>A Unicode character equivalent to the value of this instance.</returns>
        public static char ToCharInvariant(this IConvertible o)
        {
            return o.ToChar(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 8-bit signed integer equivalent to the value of this instance.</returns>
        public static sbyte ToSByteInvariant(this IConvertible o)
        {
            return o.ToSByte(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 8-bit unsigned integer equivalent to the value of this instance.</returns>
        public static byte ToByteInvariant(this IConvertible o)
        {
            return o.ToByte(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 16-bit signed integer equivalent to the value of this instance.</returns>
        public static short ToInt16Invariant(this IConvertible o)
        {
            return o.ToInt16(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 16-bit unsigned integer equivalent to the value of this instance.</returns>
        public static ushort ToUInt16Invariant(this IConvertible o)
        {
            return o.ToUInt16(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 32-bit signed integer equivalent to the value of this instance.</returns>
        public static int ToInt32Invariant(this IConvertible o)
        {
            return o.ToInt32(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 32-bit unsigned integer equivalent to the value of this instance.</returns>
        public static uint ToUInt32Invariant(this IConvertible o)
        {
            return o.ToUInt32(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 64-bit signed integer equivalent to the value of this instance.</returns>
        public static long ToInt64Invariant(this IConvertible o)
        {
            return o.ToInt64(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 64-bit unsigned integer equivalent to the value of this instance.</returns>
        public static ulong ToUInt64Invariant(this IConvertible o)
        {
            return o.ToUInt64(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent single-precision floating-point number using the specified culture-specific formatting information.</summary>
        /// <returns>A single-precision floating-point number equivalent to the value of this instance.</returns>
        public static float ToSingleInvariant(this IConvertible o)
        {
            return o.ToSingle(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent double-precision floating-point number using the specified culture-specific formatting information.</summary>
        /// <returns>A double-precision floating-point number equivalent to the value of this instance.</returns>
        public static double ToDoubleInvariant(this IConvertible o)
        {
            return o.ToDouble(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent <see cref="T:System.Decimal" /> number using the specified culture-specific formatting information.</summary>
        /// <returns>A <see cref="T:System.Decimal" /> number equivalent to the value of this instance.</returns>
        public static decimal ToDecimalInvariant(this IConvertible o)
        {
            return o.ToDecimal(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent <see cref="T:System.DateTime" /> using the specified culture-specific formatting information.</summary>
        /// <returns>A <see cref="T:System.DateTime" /> instance equivalent to the value of this instance.</returns>
        public static DateTime ToDateTimeInvariant(this IConvertible o)
        {
            return o.ToDateTime(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent <see cref="T:System.String" /> using the specified culture-specific formatting information.</summary>
        /// <returns>A <see cref="T:System.String" /> instance equivalent to the value of this instance.</returns>
        public static string ToStringInvariant(this IConvertible o)
        {
            return o.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an <see cref="T:System.Object" /> of the specified <see cref="T:System.Type" /> that has an equivalent value, using the specified culture-specific formatting information.</summary>
        /// <param name="o">This object</param>
        /// <param name="conversionType">The <see cref="T:System.Type" /> to which the value of this instance is converted. </param>
        /// <returns>An <see cref="T:System.Object" /> instance of type <paramref name="conversionType" /> whose value is equivalent to the value of this instance.</returns>
        public static object ToTypeInvariant(this IConvertible o, Type conversionType)
        {
            return o.ToType(conversionType, CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting information.</summary>
        /// <returns>A Boolean value equivalent to the value of this instance.</returns>
        public static bool ToBoolean(this IConvertible o)
        {
            return o.ToBoolean(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent Unicode character using the specified culture-specific formatting information.</summary>
        /// <returns>A Unicode character equivalent to the value of this instance.</returns>
        public static char ToChar(this IConvertible o)
        {
            return o.ToChar(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 8-bit signed integer equivalent to the value of this instance.</returns>
        public static sbyte ToSByte(this IConvertible o)
        {
            return o.ToSByte(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 8-bit unsigned integer equivalent to the value of this instance.</returns>
        public static byte ToByte(this IConvertible o)
        {
            return o.ToByte(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 16-bit signed integer equivalent to the value of this instance.</returns>
        public static short ToInt16(this IConvertible o)
        {
            return o.ToInt16(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 16-bit unsigned integer equivalent to the value of this instance.</returns>
        public static ushort ToUInt16(this IConvertible o)
        {
            return o.ToUInt16(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 32-bit signed integer equivalent to the value of this instance.</returns>
        public static int ToInt32(this IConvertible o)
        {
            return o.ToInt32(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 32-bit unsigned integer equivalent to the value of this instance.</returns>
        public static uint ToUInt32(this IConvertible o)
        {
            return o.ToUInt32(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 64-bit signed integer equivalent to the value of this instance.</returns>
        public static long ToInt64(this IConvertible o)
        {
            return o.ToInt64(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 64-bit unsigned integer equivalent to the value of this instance.</returns>
        public static ulong ToUInt64(this IConvertible o)
        {
            return o.ToUInt64(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent single-precision floating-point number using the specified culture-specific formatting information.</summary>
        /// <returns>A single-precision floating-point number equivalent to the value of this instance.</returns>
        public static float ToSingle(this IConvertible o)
        {
            return o.ToSingle(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent double-precision floating-point number using the specified culture-specific formatting information.</summary>
        /// <returns>A double-precision floating-point number equivalent to the value of this instance.</returns>
        public static double ToDouble(this IConvertible o)
        {
            return o.ToDouble(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent <see cref="T:System.Decimal" /> number using the specified culture-specific formatting information.</summary>
        /// <returns>A <see cref="T:System.Decimal" /> number equivalent to the value of this instance.</returns>
        public static decimal ToDecimal(this IConvertible o)
        {
            return o.ToDecimal(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent <see cref="T:System.DateTime" /> using the specified culture-specific formatting information.</summary>
        /// <returns>A <see cref="T:System.DateTime" /> instance equivalent to the value of this instance.</returns>
        public static DateTime ToDateTime(this IConvertible o)
        {
            return o.ToDateTime(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent <see cref="T:System.String" /> using the specified culture-specific formatting information.</summary>
        /// <returns>A <see cref="T:System.String" /> instance equivalent to the value of this instance.</returns>
        public static string ToString(this IConvertible o)
        {
            return o.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an <see cref="T:System.Object" /> of the specified <see cref="T:System.Type" /> that has an equivalent value, using the specified culture-specific formatting information.</summary>
        /// <param name="o">This object</param>
        /// <param name="conversionType">The <see cref="T:System.Type" /> to which the value of this instance is converted. </param>
        /// <returns>An <see cref="T:System.Object" /> instance of type <paramref name="conversionType" /> whose value is equivalent to the value of this instance.</returns>
        public static object ToType(this IConvertible o, Type conversionType)
        {
            return o.ToType(conversionType, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Check for Positive Integers with zero inclusive  
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsWholeNumber(this string strNumber)
        {
            if (string.IsNullOrEmpty(strNumber))
                return false;

            return !ObjNotWholePattern.IsMatch(strNumber);
        }

        /// <summary>
        /// Check if the string is Double  
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsDouble(this string strNumber)
        {
            if (strNumber == "")
                return false;

            try
            {
                Convert.ToDouble(strNumber);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///Function to Check for AlphaNumeric. 
        /// </summary>
        /// <param name="strToCheck"> str to check for alphanumeric</param>
        /// <returns>True if it is Alphanumeric</returns>
        public static bool IsAlphaNumeric(this string strToCheck)
        {
            if (string.IsNullOrEmpty(strToCheck))
                return false;

            var valid = !ObjAlphaNumericPattern.IsMatch(strToCheck);
            return valid;
        }

        /// <summary>
        ///Function to Check for valid alphanumeric input with space chars also
        /// </summary>
        /// <param name="strToCheck"> str to check for alphanumeric</param>
        /// <returns>True if it is Alphanumeric</returns>
        public static bool IsValidAlphaNumericWithSpace(this string strToCheck)
        {
            if (string.IsNullOrEmpty(strToCheck))
                return false;

            var valid = !ObjAlphaNumericPatternWhite.IsMatch(strToCheck);
            return valid;
        }

        /// <summary>
        /// Check for valid alphabet input with space chars also
        /// </summary>
        /// <param name="strToCheck"> str to check for alphanumeric</param>
        /// <returns>True if it is Alphanumeric</returns>
        public static bool IsValidAlphabetWithSpace(this string strToCheck)
        {
            if (string.IsNullOrEmpty(strToCheck))
                return false;

            var valid = !ObjAlphaPatternWhite.IsMatch(strToCheck);
            return valid;
        }

        /// <summary>
        /// Check for valid alphabet input with space chars also
        /// </summary>
        /// <param name="strToCheck"> str to check for alphanumeric</param>
        /// <returns>True if it is Alphanumeric</returns>
        public static bool IsValidAlphabetWithHyphen(this string strToCheck)
        {
            if (string.IsNullOrEmpty(strToCheck))
                return false;

            var valid = !ObjAlphaDashPattern.IsMatch(strToCheck);
            return valid;
        }

        /// <summary>
        ///  Check for Alphabets.
        /// </summary>
        /// <param name="strToCheck">Input string to check for validity</param>
        /// <returns>True if valid alphabetic string, False otherwise</returns>
        public static bool IsAlpha(this string strToCheck)
        {
            if (string.IsNullOrEmpty(strToCheck))
                return false;

            var valid = !ObjAlphaPattern.IsMatch(strToCheck);
            return valid;
        }

        /// <summary>
        /// Check whether the string is valid number or not
        /// </summary>
        /// <param name="strNumber">Number to check for </param>
        /// <returns>True if valid number, False otherwise</returns>
        public static bool IsNumber(this string strNumber)
        {
            try
            {
                Convert.ToDouble(strNumber);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strInteger"></param>
        /// <returns></returns>
        public static bool IsInteger(this string strInteger)
        {
            try
            {
                if (string.IsNullOrEmpty(strInteger))
                    return false;

                Convert.ToInt32(strInteger);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strDateTime"></param>
        /// <returns></returns>
        public static bool IsDateTime(this string strDateTime)
        {
            try
            {
                Convert.ToDateTime(strDateTime);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Function to validate given string for HTML Injection
        /// </summary>
        /// <param name="strBuff">str to be validated</param>
        /// <returns>Boolean value indicating if given input string passes HTML Injection validation</returns>
        public static bool IsValidHtmlInjection(this string strBuff)
        {
            return !Regex.IsMatch(HttpUtility.HtmlDecode(strBuff) ?? throw new ArgumentException(), "<(.|\n)+?>");
        }

        /// <summary>
        /// Checks whether a valid Email address was input
        /// </summary>
        /// <param name="inputEmail">Email address to validate</param>
        /// <returns>True if valid, False otherwise</returns>
        public static bool IsEmail(this string inputEmail)
        {
            return !inputEmail.IsNullOrEmpty() && IsEmailBigRe.IsMatch(inputEmail);
        }

        /// <summary>
        /// Converts an Object to it's integer value
        /// </summary>
        /// <param name="objectToConvert"></param>
        /// <returns></returns>
        public static int ToInteger(this object objectToConvert)
        {
            try
            {
                return Convert.ToInt32(objectToConvert.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Object cannot be converted to Integer", e);
            }
        }

        /// <summary>
        /// Converts an Object to it's integer value
        /// </summary>
        /// <param name="objectToConvert"></param>
        /// <returns></returns>
        public static long ToLong(this object objectToConvert)
        {
            try
            {
                return Convert.ToInt64(objectToConvert.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Object cannot be converted to Long", e);
            }
        }

        /// <summary>
        /// Converts an Object to it's double value
        /// </summary>
        /// <param name="objectToConvert"></param>
        /// <returns></returns>
        public static double ToDouble(this object objectToConvert)
        {
            try
            {
                return Convert.ToDouble(objectToConvert.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Object cannot be converted to double", e);
            }
        }

        /// <summary>
        /// Converts an Object to it's decimal value
        /// </summary>
        /// <param name="objectToConvert"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object objectToConvert)
        {
            try
            {
                return Convert.ToDecimal(objectToConvert.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Object cannot be converted to decimal", e);
            }
        }

        /// <summary>
        /// Converts an str to it's decimal value
        /// </summary>
        /// <returns></returns>
        public static decimal ToDecimal(this string strToConvert)
        {
            try
            {
                return Convert.ToDecimal(strToConvert);
            }
            catch (Exception e)
            {
                throw new Exception("str cannot be converted to decimal", e);
            }
        }

        /// <summary>
        /// Converts a string to a Sentence case
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToSentence(this string str)
        {
            if (str?.Length > 0)
                return str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1);

            return "";
        }

        public static bool ToBool(this object obj)
        {
            try
            {
                return Convert.ToBoolean(obj.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Object cannot be converted to Boolean", e);
            }
        }

        public static DateTime ToDateTime(this object obj)
        {
            try
            {
                return Convert.ToDateTime(Convert.ToString(obj));
            }
            catch (Exception e)
            {
                throw new Exception("Object cannot be converted to DateTime. Object: " + obj, e);
            }
        }

        /// <summary>
        /// Selects specific number of rows from a datatable
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static DataTable SelectRows(this DataTable dataTable, int rowCount)
        {
            try
            {
                var myTable = dataTable.Clone();
                var myRows = dataTable.Select();
                for (var i = 0; i < rowCount; i++)
                {
                    if (i >= myRows.Length) continue;
                    myTable.ImportRow(myRows[i]);
                    myTable.AcceptChanges();
                }

                return myTable;
            }
            catch
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// Accepts a date time value, calculates number of days, minutes or seconds and shows 'pretty dates'
        /// like '2 days ago', '1 week ago' or '10 minutes ago'
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string GetPrettyDate(this DateTime d)
        {
            // 1.
            // Get time span elapsed since the date.
            var s = DateTime.Now.Subtract(d);

            // 2.
            // Get total number of days elapsed.
            var dayDiff = (int) s.TotalDays;

            // 3.
            // Get total number of seconds elapsed.
            var secDiff = (int) s.TotalSeconds;

            // 4.
            // Don't allow out of range values.
            if (dayDiff < 0 || dayDiff >= 31)
            {
                return d.ToStringInvariant();
            }

            // 5.
            // Handle same-day times.
            switch (dayDiff)
            {
                case 0:
                    // A.
                    // Less than one minute ago.
                    if (secDiff < 60)
                    {
                        return "just now";
                    }
                    // B.
                    // Less than 2 minutes ago.
                    if (secDiff < 120)
                    {
                        return "1 minute ago";
                    }
                    // C.
                    // Less than one hour ago.
                    if (secDiff < 3600)
                    {
                        return $"{Math.Floor((double) secDiff / 60)} minutes ago";
                    }
                    // D.
                    // Less than 2 hours ago.
                    if (secDiff < 7200)
                    {
                        return "1 hour ago";
                    }
                    // E.
                    // Less than one day ago.
                    if (secDiff < 86400)
                    {
                        return $"{Math.Floor((double) secDiff / 3600)} hours ago";
                    }
                    break;
                case 1:
                    return "yesterday";
            }
            // 6.
            // Handle previous days.
            if (dayDiff < 7)
            {
                return $"{dayDiff} days ago";
            }
            return dayDiff < 31 ? $"{Math.Ceiling((double) dayDiff / 7)} weeks ago" : null;
        }

        public static IEnumerable<string> SplitLazy(this string self, char c)
        {
            return new LazySplitter(self, c);
        }

        public static IEnumerable<(string substring, int index)> SplitLazyWithIndex(this string self, char c)
        {
            return new LazySplitterIndex(self, c);
        }

        public static string UppercaseFirstLetter(this string value)
        {
            // Uppercase the first letter in the string.
            if (value.IsNullOrEmpty()) return value;
            return char.ToUpper(value[0]) + value.Substring(1);
        }

        public static void AddAll<T>(this ICollection<T> self, params T[] items) => self.AddRange(items);

        public static int IndexOfInvariant(this string self, string s) => self.IndexOf(s, StringComparison.Ordinal);

        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);

        public static string IsInterned(this string value) => string.IsInterned(value);

        public static string Intern(this string value) => string.Intern(value);

        /// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects and returns an integer that indicates their relative position in the sort order.</summary>
        /// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition Less than zero The substring in <paramref name="strA" /> precedes the substring in <paramref name="strB" /> in the sort order. Zero The substrings occur in the same position in the sort order, or <paramref name="length" /> is zero. Greater than zero The substring in <paramref name="strA" /> follows the substring in <paramref name="strB" /> in the sort order. </returns>
        /// <param name="strA">The first string to use in the comparison. </param>
        /// <param name="indexA">The position of the substring within <paramref name="strA" />. </param>
        /// <param name="strB">The second string to use in the comparison. </param>
        /// <param name="indexB">The position of the substring within <paramref name="strB" />. </param>
        /// <param name="length">The maximum number of characters in the substrings to compare. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="indexA" /> is greater than <paramref name="strA" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexB" /> is greater than <paramref name="strB" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. -or-Either <paramref name="indexA" /> or <paramref name="indexB" /> is null, and <paramref name="length" /> is greater than zero.</exception>
        /// <filterpriority>1</filterpriority>
        public static int Compare(this string strA, int indexA, string strB, int indexB, int length)
            => string.Compare(strA, indexA, strB, indexB, length);

        public static int Compare(this string strA, int indexA, string strB, int indexB, int length, bool ignoreCase)
            => string.Compare(strA, indexA, strB, indexB, length, ignoreCase);

        /// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects using the specified rules, and returns an integer that indicates their relative position in the sort order. </summary>
        /// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero The substring in <paramref name="strA" /> precedes the substring in <paramref name="strB" /> in the sort order.Zero The substrings occur in the same position in the sort order, or the <paramref name="length" /> parameter is zero. Greater than zero The substring in <paramref name="strA" /> follllows the substring in <paramref name="strB" /> in the sort order. </returns>
        /// <param name="strA">The first string to use in the comparison. </param>
        /// <param name="indexA">The position of the substring within <paramref name="strA" />. </param>
        /// <param name="strB">The second string to use in the comparison.</param>
        /// <param name="indexB">The position of the substring within <paramref name="strB" />. </param>
        /// <param name="length">The maximum number of characters in the substrings to compare. </param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules to use in the comparison. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="indexA" /> is greater than <paramref name="strA" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexB" /> is greater than <paramref name="strB" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. -or-Either <paramref name="indexA" /> or <paramref name="indexB" /> is null, and <paramref name="length" /> is greater than zero.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value. </exception>
        /// <filterpriority>1</filterpriority>
        public static int Compare(this string strA, int indexA, string strB, int indexB, int length,
            StringComparison comparisonType)
            => string.Compare(strA, indexA, strB, indexB, length, comparisonType);

        /// <summary>Compares two specified <see cref="T:System.String" /> objects and returns an integer that indicates their relative position in the sort order.</summary>
        /// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA" /> precedes <paramref name="strB" /> in the sort order. Zero <paramref name="strA" /> occurs in the same position as <paramref name="strB" /> in the sort order. Greater than zero <paramref name="strA" /> follows <paramref name="strB" /> in the sort order. </returns>
        /// <param name="strA">The first string to compare. </param>
        /// <param name="strB">The second string to compare. </param>
        /// <filterpriority>1</filterpriority>
        public static int Compare(this string strA, string strB)
            => string.Compare(strA, strB);

        /// <summary>Compares two specified <see cref="T:System.String" /> objects, ignoring or honoring their case, and returns an integer that indicates their relative position in the sort order.</summary>
        /// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA" /> precedes <paramref name="strB" /> in the sort order. Zero <paramref name="strA" /> occurs in the same position as <paramref name="strB" /> in the sort order. Greater than zero <paramref name="strA" /> follows <paramref name="strB" /> in the sort order. </returns>
        /// <param name="strA">The first string to compare. </param>
        /// <param name="strB">The second string to compare. </param>
        /// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param>
        /// <filterpriority>1</filterpriority>
        public static int Compare(this string strA, string strB, bool ignoreCase)
            => string.Compare(strA, strB, ignoreCase);

        public static int Compare(this string strA, string strB, bool ignoreCase, CultureInfo culture)
            => string.Compare(strA, strB, ignoreCase, culture);

        public static int Compare(this string strA, string strB, CultureInfo culture, CompareOptions options)
            => string.Compare(strA, strB, culture, options);

        public static int Compare(this string strA, int indexA, string strB, int indexB, int length, bool ignoreCase,
            CultureInfo culture)
            => string.Compare(strA, indexA, strB, indexB, length, ignoreCase, culture);

        public static int Compare(this string strA, int indexA, string strB, int indexB, int length,
            CultureInfo culture,
            CompareOptions options)
            => string.Compare(strA, indexA, strB, indexB, length, culture, options);

        /// <summary>Compares two specified <see cref="T:System.String" /> objects using the specified rules, and returns an integer that indicates their relative position in the sort order.</summary>
        /// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA" /> precedes <paramref name="strB" /> in the sort order. Zero <paramref name="strA" /> is in the same position as <paramref name="strB" /> in the sort order. Greater than zero <paramref name="strA" /> follows <paramref name="strB" /> in the sort order. </returns>
        /// <param name="strA">The first string to compare.</param>
        /// <param name="strB">The second string to compare. </param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules to use in the comparison. </param>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value. </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// <see cref="T:System.StringComparison" /> is not supported.</exception>
        /// <filterpriority>1</filterpriority>
        public static int Compare(this string strA, string strB, StringComparison comparisonType)
            => string.Compare(strA, strB, comparisonType);

        /// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects by evaluating the numeric values of the corresponding <see cref="T:System.Char" /> objects in each substring. </summary>
        /// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.ValueCondition Less than zero The substring in <paramref name="strA" /> is less than the substring in <paramref name="strB" />. Zero The substrings are equal, or <paramref name="length" /> is zero. Greater than zero The substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />. </returns>
        /// <param name="strA">The first string to use in the comparison. </param>
        /// <param name="indexA">The starting index of the substring in <paramref name="strA" />. </param>
        /// <param name="strB">The second string to use in the comparison. </param>
        /// <param name="indexB">The starting index of the substring in <paramref name="strB" />. </param>
        /// <param name="length">The maximum number of characters in the substrings to compare. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="strA" /> is not null and <paramref name="indexA" /> is greater than <paramref name="strA" />.<see cref="P:System.String.Length" />.-or- <paramref name="strB" /> is not null and<paramref name="indexB" /> is greater than <paramref name="strB" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. </exception>
        /// <filterpriority>2</filterpriority>
        public static int CompareOrdinal(this string strA, int indexA, string strB, int indexB, int length)
            => string.CompareOrdinal(strA, indexA, strB, indexB, length);

        /// <summary>Compares two specified <see cref="T:System.String" /> objects by evaluating the numeric values of the corresponding <see cref="T:System.Char" /> objects in each string.</summary>
        /// <returns>An integer that indicates the lexical relationship between the two comparands.ValueCondition Less than zero <paramref name="strA" /> is less than <paramref name="strB" />. Zero <paramref name="strA" /> and <paramref name="strB" /> are equal. Greater than zero <paramref name="strA" /> is greater than <paramref name="strB" />. </returns>
        /// <param name="strA">The first string to compare. </param>
        /// <param name="strB">The second string to compare. </param>
        /// <filterpriority>2</filterpriority>
        public static int CompareOrdinal(this string strA, string strB)
            => string.CompareOrdinal(strA, strB);
    }
}