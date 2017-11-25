using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using HSNXT.RegularExpressions;

namespace HSNXT
{
    public static partial class Extensions
    {
        private const string IsEmailBigRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                               @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                               @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        private static readonly Regex ObjNotWholePattern = new ObjNotWholePattern();
        private static readonly Regex ObjAlphaNumericPattern = new ObjAlphaNumericPattern();
        private static readonly Regex ObjAlphaNumericPatternWhite = new ObjAlphaNumericPatternWhite();
        private static readonly Regex ObjAlphaPatternWhite = new ObjAlphaPatternWhite();
        private static readonly Regex ObjAlphaDashPattern = new ObjAlphaDashPattern();
        private static readonly Regex ObjAlphaPattern = new ObjAlphaPattern();
        private static readonly Regex IsEmailBigRe = new IsEmailBigRe();

        public static bool EqualsAnyInvariant(this string a, params string[] b)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var s in b)
            {
                if (string.Equals(a, s, StringComparison.InvariantCulture))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool EqualsAllInvariant(this string a, params string[] b)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var s in b)
            {
                if (!string.Equals(a, s, StringComparison.InvariantCulture))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool EqualsAnyOrdinal(this string a, params string[] b)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var s in b)
            {
                if (string.Equals(a, s, StringComparison.Ordinal))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool EqualsAllOrdinal(this string a, params string[] b)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var s in b)
            {
                if (!string.Equals(a, s, StringComparison.Ordinal))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool EqualsNoneIgnoreCase(this string a, params string[] b)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var s in b)
            {
                if (!string.Equals(a, s, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool EqualsNoneInvariant(this string a, params string[] b)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var s in b)
            {
                if (!string.Equals(a, s, StringComparison.InvariantCulture))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool EqualsNoneOrdinal(this string a, params string[] b)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var s in b)
            {
                if (!string.Equals(a, s, StringComparison.Ordinal))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool EqualsIgnoreCase(this string a, string b) => a.Equals(b, StringComparison.OrdinalIgnoreCase);

        public static bool EqualsAnyIgnoreCase(this string a, params string[] b)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var s in b)
            {
                if (string.Equals(a, s, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool EqualsAllIgnoreCase(this string a, params string[] b)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var s in b)
            {
                if (!string.Equals(a, s, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }

        public static string OnlyDigits(this string value)
        {
            return new string(value?.Where(char.IsDigit).ToArray());
        }

        private static readonly ReadOnlyDictionary<char, int> RomanMap = new ReadOnlyDictionaryBuilder<char, int>
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
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
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
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
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
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
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
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
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

        public static string UppercaseFirstLetter(this string value)
        {
            // Uppercase the first letter in the string.
            if (value.IsNullOrEmpty()) return value;
            return char.ToUpper(value[0]) + value.Substring(1);
        }

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
        [SuppressMessage("ReSharper", "StringCompareIsCultureSpecific.1")]
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