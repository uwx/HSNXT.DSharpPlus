using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
// ReSharper disable StringCompareIsCultureSpecific.1
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace TestProj47
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello World!");
        }
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

        public IEnumerator<string> GetEnumerator()
        {
            return new LazyEnumerator(_self, _c);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
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

        public IEnumerator<(string, int)> GetEnumerator()
        {
            return new LazyEnumeratorIndex(_self, _c);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
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

        public void Reset()
        {
            _position = 0;
        }

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

        public void Reset()
        {
            _position = 0;
        }

        public (string, int) Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }

    public static partial class Extensions
    {
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

        /// <summary>
        /// Check for Positive Integers with zero inclusive  
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsWholeNumber(this string strNumber)
        {
            if (strNumber == "")
                return false;

            var objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strNumber);
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
            catch (Exception)
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
            if (strToCheck == "")
                return false;

            var objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");

            var valid = !objAlphaNumericPattern.IsMatch(strToCheck);
            return valid;
        }

        /// <summary>
        ///Function to Check for valid alphanumeric input with space chars also
        /// </summary>
        /// <param name="strToCheck"> str to check for alphanumeric</param>
        /// <returns>True if it is Alphanumeric</returns>
        public static bool IsValidAlphaNumericWithSpace(this string strToCheck)
        {
            if (strToCheck == "")
                return false;

            var objAlphaNumericPattern = new Regex("[^a-zA-Z0-9\\s]");

            var valid = !objAlphaNumericPattern.IsMatch(strToCheck);
            return valid;
        }

        /// <summary>
        /// Check for valid alphabet input with space chars also
        /// </summary>
        /// <param name="strToCheck"> str to check for alphanumeric</param>
        /// <returns>True if it is Alphanumeric</returns>
        public static bool IsValidAlphabetWithSpace(this string strToCheck)
        {
            if (strToCheck == "")
                return false;

            var objAlphaNumericPattern = new Regex("[^a-zA-Z\\s]");

            var valid = !objAlphaNumericPattern.IsMatch(strToCheck);
            return valid;
        }

        /// <summary>
        /// Check for valid alphabet input with space chars also
        /// </summary>
        /// <param name="strToCheck"> str to check for alphanumeric</param>
        /// <returns>True if it is Alphanumeric</returns>
        public static bool IsValidAlphabetWithHyphen(this string strToCheck)
        {
            if (strToCheck == "")
                return false;

            var objAlphaNumericPattern = new Regex("[^a-zA-Z\\-]");

            var valid = !objAlphaNumericPattern.IsMatch(strToCheck);
            return valid;
        }

        /// <summary>
        ///  Check for Alphabets.
        /// </summary>
        /// <param name="strToCheck">Input string to check for validity</param>
        /// <returns>True if valid alphabetic string, False otherwise</returns>
        public static bool IsAlpha(this string strToCheck)
        {
            if (strToCheck == "")
                return false;

            var objAlphaPattern = new Regex("[^a-zA-Z]");

            var valid = !objAlphaPattern.IsMatch(strToCheck);
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
            return (!Regex.IsMatch(HttpUtility.HtmlDecode(strBuff) ?? throw new ArgumentException(), "<(.|\n)+?>"));
        }

        /// <summary>
        /// Checks whether a valid Email address was input
        /// </summary>
        /// <param name="inputEmail">Email address to validate</param>
        /// <returns>True if valid, False otherwise</returns>
        public static bool IsEmail(this string inputEmail)
        {
            if (inputEmail.IsNullOrEmpty()) return (false);
            const string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                    @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                    @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var re = new Regex(strRegex);
            return re.IsMatch(inputEmail);
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
            catch
            {
                throw new Exception("Object cannot be converted to Integer");
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
            catch
            {
                throw new Exception("Object cannot be converted to Long");
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
            catch
            {
                throw new Exception("Object cannot be converted to double");
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
            catch
            {
                throw new Exception("Object cannot be converted to decimal");
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
            catch
            {
                throw new Exception("str cannot be converted to decimal");
            }
        }

        /// <summary>
        /// Converts a string to a Sentence case
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToSentence(this string str)
        {
            if (str.Length > 0)
                return str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1);

            return "";
        }

        public static bool ToBool(this object Object)
        {
            try
            {
                return Convert.ToBoolean(Object.ToString());
            }
            catch
            {
                throw new Exception("Object cannot be converted to Boolean");
            }
        }

        public static DateTime ToDateTime(this object Object)
        {
            try
            {
                return Convert.ToDateTime(Convert.ToString(Object));
            }
            catch (Exception)
            {
                throw new Exception("Object cannot be converted to DateTime. Object: " + Object);
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
            catch (Exception)
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
            if (dayDiff < 31)
            {
                return $"{Math.Ceiling((double) dayDiff / 7)} weeks ago";
            }
            return null;
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
            if (value.Length <= 0) return value;
            return char.ToUpper(value[0]) + value.Substring(1);
        }

        public static void AddAll<T>(this List<T> self, params T[] items) => self.AddRange(items);

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

        public static int Compare(this string strA, int indexA, string strB, int indexB, int length, CultureInfo culture,
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