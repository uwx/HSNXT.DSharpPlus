// Decompiled with JetBrains decompiler
// Type: TestProj47.StringExtensions
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HSNXT
{
    public static partial class Extensions

    {
        /// <summary>
        /// Replaces the format item in a specified string with the string representation of a corresponding object in a specified array.
        /// </summary>
        /// <param name="source">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>A copy of format in which the format items have been replaced by the string representation of the corresponding objects in args.</returns>
        public static string FormatInvariantCultureWith(this string source, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, source, args);
        }

        /// <summary>
        /// Replaces the format item in a specified string with the string representation of a corresponding object in a specified array.
        /// </summary>
        /// <param name="source">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>A copy of format in which the format items have been replaced by the string representation of the corresponding objects in args.</returns>
        public static string FormatCurrentCultureWith(this string source, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, source, args);
        }

        /// <summary>
        /// Reports the indexes of all occurrence of the specified string in the current <see cref="T:System.String" /> object.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="ignoreCase">true to ignore case when comparing the string to seek; otherwise, false.</param>
        /// <returns>The list of index positions of the value parameter if that string is found, or empty list if it is not. If value is System.String.Empty or null, the return value is empty list.</returns>
        public static List<int> AllIndexOf(this string source, string value, bool ignoreCase)
        {
            var intList = new List<int>();
            if (source == null || string.IsNullOrEmpty(value))
                return intList;
            var length = value.Length;
            var startIndex = 0;
            if (ignoreCase)
            {
                int num;
                for (;
                    (num = source.IndexOf(value, startIndex, StringComparison.OrdinalIgnoreCase)) >= 0;
                    startIndex = num + length)
                    intList.Add(num);
                return intList;
            }
            int num1;
            for (; (num1 = source.IndexOf(value, startIndex)) >= 0; startIndex = num1 + length)
                intList.Add(num1);
            return intList;
        }

        /// <summary>
        /// Reports the indexes of all occurrence of the specified string in the current <see cref="T:System.String" /> object.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="value">A Unicode character to seek.</param>
        /// <param name="ignoreCase">true to ignore case when comparing the string to seek; otherwise, false.</param>
        /// <returns>The list of index positions of the value parameter if that string is found, or empty list if it is not. If value is System.String.Empty or null, the return value is empty list.</returns>
        public static List<int> AllIndexOf(this string source, char value, bool ignoreCase)
        {
            var intList = new List<int>();
            if (source == null)
                return intList;
            var startIndex = 0;
            if (ignoreCase)
            {
                int num;
                for (var str = value.ToString();
                    (num = source.IndexOf(str, startIndex, StringComparison.OrdinalIgnoreCase)) >= 0;
                    startIndex = num + 1)
                    intList.Add(num);
                return intList;
            }
            int num1;
            for (; (num1 = source.IndexOf(value, startIndex)) >= 0; startIndex = num1 + 1)
                intList.Add(num1);
            return intList;
        }

        /// <summary>
        /// Returns a value indicating whether the specified <see cref="T:System.String" /> object occurs within this string.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="ignoreCase">true to ignore case when comparing the string to seek; otherwise, false.</param>
        /// <returns>true if the <paramref name="value" /> parameter occurs within this string, or if <paramref name="value" /> is the empty string ("") or null; otherwise, false.</returns>
        public static bool Contains(this string source, string value, bool ignoreCase)
        {
            if (source == null)
                return false;
            if (string.IsNullOrEmpty(value))
                return true;
            if (ignoreCase)
                return source.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
            return source.Contains(value);
        }

        /// <summary>
        /// Returns a value indicating whether the specified <see cref="T:System.Char" /> object occurs within this string.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="value">A Unicode character to seek.</param>
        /// <param name="ignoreCase">true to ignore case when comparing the char to seek; otherwise, false.</param>
        /// <returns>true if the <paramref name="value" /> parameter occurs within this string, or if <paramref name="value" /> is null; otherwise, false.</returns>
        public static bool Contains(this string source, char value, bool ignoreCase)
        {
            if (source == null)
                return false;
            if (!ignoreCase)
                return source.Contains(value);
            if (string.IsNullOrEmpty(value.ToString()))
                return true;
            return source.IndexOf(value.ToString(), StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Contains any instance of the given string from the current string.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="ignoreCase">true to ignore case when comparing the string to seek; otherwise, false.</param>
        /// <param name="values">Strings to check.</param>
        /// <returns>true if the <paramref name="values" /> parameter occurs within this string, or if <paramref name="values" /> is null or empty; otherwise, false.</returns>
        public static bool ContainsAny(this string source, bool ignoreCase, params string[] values)
        {
            if (source == null)
                return false;
            if (values == null || values.Length < 1)
                return true;
            return values.Any(i => source.Contains(i, ignoreCase));
        }

        /// <summary>
        /// Contains any instance of the given string from the current string.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="ignoreCase">true to ignore case when comparing the string to seek; otherwise, false.</param>
        /// <param name="values">Chars to check.</param>
        /// <returns>true if the <paramref name="values" /> parameter occurs within this string, or if <paramref name="values" /> is null or empty; otherwise, false.</returns>
        public static bool ContainsAny(this string source, bool ignoreCase, params char[] values)
        {
            if (source == null)
                return false;
            if (values == null || values.Length < 1)
                return true;
            return values.Any(i => source.Contains(i, ignoreCase));
        }

        /// <summary>
        /// Returns a new string in which all occurrences of a specified string in this instance are replaced with another specified string.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="oldValue">A string to be replaced.</param>
        /// <param name="newValue">A string to replace all occurrences of oldValue.</param>
        /// <param name="ignoreCase">A System.Boolean indicating a case-sensitive or insensitive comparison. (true indicates a case-insensitive comparison.)</param>
        /// <returns>A System.String equivalent to this instance but with all instances of oldValue replaced with newValue.</returns>
        public static string Replace(this string source, string oldValue, string newValue, bool ignoreCase)
        {
            if (source == null || oldValue == null || newValue == null)
                return source;
            if (!ignoreCase)
                return source.Replace(oldValue, newValue);
            var str = source;
            var length1 = oldValue.Length;
            var length2 = newValue.Length;
            int startIndex1;
            for (var startIndex2 = 0;
                (startIndex1 = str.IndexOf(oldValue, startIndex2, StringComparison.OrdinalIgnoreCase)) >= 0;
                startIndex2 = startIndex1 + length2)
                str = str.Remove(startIndex1, length1).Insert(startIndex1, newValue);
            return str;
        }

        /// <summary>
        /// Returns a new string in which all occurrences of a specified char in this instance are replaced with another specified char.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="oldValue">A Unicode character to be replaced.</param>
        /// <param name="newValue">A Unicode character to replace all occurrences of oldChar.</param>
        /// <param name="ignoreCase">A System.Boolean indicating a case-sensitive or insensitive comparison. (true indicates a case-insensitive comparison.)</param>
        /// <returns>A System.String equivalent to this instance but with all instances of oldValue replaced with newValue.</returns>
        public static string Replace(this string source, char oldValue, char newValue, bool ignoreCase)
        {
            if (source == null)
                return source;
            if (!ignoreCase)
                return source.Replace(oldValue, newValue);
            var str1 = source;
            var str2 = oldValue.ToString();
            var length1 = str2.Length;
            var str3 = newValue.ToString();
            var length2 = str3.Length;
            int startIndex1;
            for (var startIndex2 = 0;
                (startIndex1 = str1.IndexOf(str2, startIndex2, StringComparison.OrdinalIgnoreCase)) >= 0;
                startIndex2 = startIndex1 + length2)
                str1 = str1.Remove(startIndex1, length1).Insert(startIndex1, str3);
            return str1;
        }

        /// <summary>
        /// Returns a new string in which all occurrences of a specified strings in this instance are replaced with another specified string.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="newValue">A string to replace all occurrences of oldValues.</param>
        /// <param name="ignoreCase">A System.Boolean indicating a case-sensitive or insensitive comparison. (true indicates a case-insensitive comparison.)</param>
        /// <param name="oldValues">A list of string to be replaced.</param>
        /// <returns>A System.String equivalent to this instance but with all instances of oldValues replaced with newValue.</returns>
        public static string ReplaceAny(this string source, string newValue, bool ignoreCase, params string[] oldValues)
        {
            if (source == null || newValue == null || oldValues == null || oldValues.Length < 1)
                return source;
            var source1 = source;
            foreach (var oldValue in oldValues)
                source1 = source1.Replace(oldValue, newValue, ignoreCase);
            return source1;
        }

        /// <summary>
        /// Returns a new string in which all occurrences of a specified chars in this instance are replaced with another specified char.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="newValue">A char to replace all occurrences of oldValues.</param>
        /// <param name="ignoreCase">A System.Boolean indicating a case-sensitive or insensitive comparison. (true indicates a case-insensitive comparison.)</param>
        /// <param name="oldValues">A list of char to be replaced.</param>
        /// <returns>A System.String equivalent to this instance but with all instances of oldValues replaced with newValue.</returns>
        public static string ReplaceAny(this string source, char newValue, bool ignoreCase, params char[] oldValues)
        {
            if (source == null || oldValues == null || oldValues.Length < 1)
                return source;
            var source1 = source;
            foreach (var oldValue in oldValues)
                source1 = source1.Replace(oldValue, newValue, ignoreCase);
            return source1;
        }

        /// <summary>
        /// Deletes all the string from this string beginning at a specified position and continuing through the last position.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="value">A string to be removed.</param>
        /// <param name="ignoreCase">A System.Boolean indicating a case-sensitive or insensitive comparison. (true indicates a case-insensitive comparison.)</param>
        /// <returns>A new System.String object that is equivalent to this string less the removed characters.</returns>
        public static string Remove(this string source, string value, bool ignoreCase)
        {
            if (source == null || value == null)
                return source;
            if (!ignoreCase)
                return source.Replace(value, string.Empty);
            var str = source;
            var length = value.Length;
            var startIndex = 0;
            while ((startIndex = str.IndexOf(value, startIndex, StringComparison.OrdinalIgnoreCase)) >= 0)
                str = str.Remove(startIndex, length);
            return str;
        }

        /// <summary>
        /// Deletes all the character from this string beginning at a specified position and continuing through the last position.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="value">A Unicode character to be removed.</param>
        /// <param name="ignoreCase">A System.Boolean indicating a case-sensitive or insensitive comparison. (true indicates a case-insensitive comparison.)</param>
        /// <returns>A new System.String object that is equivalent to this string less the removed characters.</returns>
        public static string Remove(this string source, char value, bool ignoreCase)
        {
            if (source == null)
                return source;
            if (!ignoreCase)
                return source.Replace(value.ToString(), string.Empty);
            var str1 = source;
            var str2 = value.ToString();
            var length = str2.Length;
            var startIndex = 0;
            while ((startIndex = str1.IndexOf(str2, startIndex, StringComparison.OrdinalIgnoreCase)) >= 0)
                str1 = str1.Remove(startIndex, length);
            return str1;
        }

        /// <summary>
        /// Deletes all the string from this string beginning at a specified position and continuing through the last position.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="ignoreCase">A System.Boolean indicating a case-sensitive or insensitive comparison. (true indicates a case-insensitive comparison.)</param>
        /// <param name="values">A list of string to be removed.</param>
        /// <returns>A new System.String object that is equivalent to this string less the removed characters.</returns>
        public static string RemoveAny(this string source, bool ignoreCase, params string[] values)
        {
            if (source == null || values == null || values.Length < 1)
                return source;
            var source1 = source;
            foreach (var str in values)
                source1 = source1.Remove(str, ignoreCase);
            return source1;
        }

        /// <summary>
        /// Deletes all the characters from this string beginning at a specified position and continuing through the last position.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="ignoreCase">A System.Boolean indicating a case-sensitive or insensitive comparison. (true indicates a case-insensitive comparison.)</param>
        /// <param name="values">A list of char to be removed.</param>
        /// <returns>A new System.String object that is equivalent to this string less the removed characters.</returns>
        public static string RemoveAny(this string source, bool ignoreCase, params char[] values)
        {
            if (source == null || values == null || values.Length < 1)
                return source;
            var source1 = source;
            foreach (var ch in values)
                source1 = source1.Remove(ch, ignoreCase);
            return source1;
        }

        /// <summary>
        /// Returns an empty string if source is null; otherwise, return source itself.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns>An empty string if source is null; otherwise, source itself.</returns>
        public static string EmptyIfNull(this string source)
        {
            return source ?? string.Empty;
        }

        /// <summary>
        /// Indicates whether the specified string is neither null nor an System.String.Empty string.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <returns>true if the value parameter is neither null nor an empty string (""); otherwise, false.</returns>
        public static bool IsNotNullNorEmpty(this string source)
        {
            return !string.IsNullOrEmpty(source);
        }

        /// <summary>
        /// Indicates whether a specified string is neither null, empty, nor consists only of white-space characters.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <returns>true if the <paramref name="source" /> parameter is neither null nor <see cref="F:System.String.Empty" />, or if <paramref name="source" /> consists exclusively of white-space characters. </returns>
        public static bool IsNotNullNorWhiteSpace(this string source)
        {
            return !source.IsNullOrWhiteSpace();
        }

        /// <summary>Extracts all digits from a string.</summary>
        /// <param name="source">String containing digits to extract.</param>
        /// <returns>All digits contained within the input string.</returns>
        public static string ExtractDigits(this string source)
        {
            return source.ExtractChars(char.IsDigit);
        }

        /// <summary>Extracts all letters from a string.</summary>
        /// <param name="source">String containing letters to extract.</param>
        /// <returns>All letters contained within the input string.</returns>
        public static string ExtractLetters(this string source)
        {
            return source.ExtractChars(char.IsLetter);
        }

        /// <summary>Extracts all symbols from a string.</summary>
        /// <param name="source">String containing symbols to extract.</param>
        /// <returns>All symbols contained within the input string.</returns>
        public static string ExtractSymbols(this string source)
        {
            return source.ExtractChars(char.IsSymbol);
        }

        /// <summary>Extracts all control chars from a string.</summary>
        /// <param name="source">String containing control chars to extract.</param>
        /// <returns>All control chars contained within the input string.</returns>
        public static string ExtractControlChars(this string source)
        {
            return source.ExtractChars(char.IsControl);
        }

        /// <summary>Extracts all letters and digits from a string.</summary>
        /// <param name="source">String containing letters and digits to extract.</param>
        /// <returns>All letters and digits contained within the input string.</returns>
        public static string ExtractLettersDigits(this string source)
        {
            return source.ExtractChars(char.IsLetterOrDigit);
        }

        /// <summary>
        /// Extracts all chars satisfy the condition from a string.
        /// </summary>
        /// <param name="source">String containing satisfied chars to extract.</param>
        /// <param name="predicate">A function to test char for a condition.</param>
        /// <returns>All satisfied chars contained within the input string.</returns>
        public static string ExtractChars(this string source, Func<char, bool> predicate)
        {
            if (source.IsNullOrEmpty())
                return string.Empty;
            return source.Where(predicate).Aggregate(new StringBuilder(source.Length),
                (stringBuilder, item) => stringBuilder.Append(item)).ToString();
        }

        /// <summary>Convert string to byte array.</summary>
        /// <param name="source">Source string.</param>
        /// <param name="encoding">Instance of Encoding.</param>
        /// <returns>Byte array.</returns>
        public static byte[] ToByteArray(this string source, Encoding encoding = null)
        {
            return (encoding ?? Encoding.UTF8).GetBytes(source);
        }

        /// <summary>
        /// Retrieves left part substring from this instance. The substring ends at the first occurrence of the specified string position. If the specified string is not found, the return value is <see cref="F:System.String.Empty" />.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="ignoreCase">true to ignore case when comparing the string to seek; otherwise, false.</param>
        /// <returns>A string equivalent to the substring that ends at the first occurrence of the specified string position.</returns>
        public static string LeftSubstringIndexOf(this string source, string value, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(value))
                return source;
            var length = ignoreCase ? source.IndexOf(value, StringComparison.OrdinalIgnoreCase) : source.IndexOf(value);
            switch (length)
            {
                case -1:
                case 0:
                    return string.Empty;
                default:
                    return source.Substring(0, length);
            }
        }

        /// <summary>
        /// Retrieves left part substring from this instance. The substring ends at the last occurrence of the specified string position. If the specified string is not found, the return value is <see cref="F:System.String.Empty" />.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="ignoreCase">true to ignore case when comparing the string to seek; otherwise, false.</param>
        /// <returns>A string equivalent to the substring that ends at the last occurrence of the specified string position.</returns>
        public static string LeftSubstringLastIndexOf(this string source, string value, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(value))
                return source;
            var length = ignoreCase
                ? source.LastIndexOf(value, StringComparison.OrdinalIgnoreCase)
                : source.LastIndexOf(value);
            switch (length)
            {
                case -1:
                case 0:
                    return string.Empty;
                default:
                    return source.Substring(0, length);
            }
        }

        /// <summary>
        /// Retrieves right part substring from this instance. The substring starts at the first occurrence of the specified string position. If the specified string is not found, the return value is <see cref="F:System.String.Empty" />.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="ignoreCase">true to ignore case when comparing the string to seek; otherwise, false.</param>
        /// <returns>A string equivalent to the substring that starts at the first occurrence of the specified string position.</returns>
        public static string RightSubstringIndexOf(this string source, string value, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(value))
                return source;
            var num = ignoreCase ? source.IndexOf(value, StringComparison.OrdinalIgnoreCase) : source.IndexOf(value);
            switch (num)
            {
                case -1:
                case 0:
                    return string.Empty;
                default:
                    return source.Substring(num + 1);
            }
        }

        /// <summary>
        /// Retrieves right part substring from this instance. The substring starts at the last occurrence of the specified string position. If the specified string is not found, the return value is <see cref="F:System.String.Empty" />.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="ignoreCase">true to ignore case when comparing the string to seek; otherwise, false.</param>
        /// <returns>A string equivalent to the substring that starts at the last occurrence of the specified string position.</returns>
        public static string RightSubstringLastIndexOf(this string source, string value, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(value))
                return source;
            var num = ignoreCase
                ? source.LastIndexOf(value, StringComparison.OrdinalIgnoreCase)
                : source.LastIndexOf(value);
            switch (num)
            {
                case -1:
                case 0:
                    return string.Empty;
                default:
                    return source.Substring(num + 1);
            }
        }

        /// <summary>
        /// Remove any invalid characters from Xml string and returns a clean Xml string.
        /// </summary>
        /// <param name="source">Xml string to check.</param>
        /// <returns>Clean Xml string.</returns>
        public static string ToCleanXmlString(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;
            return new string(source.Where(p =>
            {
                if ((p < 32 || p > 55295) && (p < 57344 || p > 65533) && p != 9 && p != 10)
                    return (int) p == 13;
                return true;
            }).ToArray());
        }

        /// <summary>
        /// Splits string by a specified delimiter and keep nested string with a specified qualifier.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="delimiter">Delimiter character.</param>
        /// <param name="qualifier">Qualifier character.</param>
        /// <returns>A list whose elements contain the substrings in this instance that are delimited by the delimiter.</returns>
        public static List<string> SplitNested(this string source, char delimiter = ' ', char qualifier = '"')
        {
            if (string.IsNullOrEmpty(source))
                return new List<string>();
            var stringBuilder = new StringBuilder();
            var stringList = new List<string>();
            var flag1 = false;
            var flag2 = false;
            for (var index = 0; index < source.Length; ++index)
            {
                var ch = source[index];
                if (!flag1)
                {
                    if (ch == delimiter)
                    {
                        stringList.Add(string.Empty);
                    }
                    else
                    {
                        if (ch == qualifier)
                            flag2 = true;
                        else
                            stringBuilder.Append(ch);
                        flag1 = true;
                    }
                }
                else
                {
                    if (flag2)
                    {
                        if (ch == qualifier && (source.Length > index + 1 && source[index + 1] == delimiter ||
                                                index + 1 == source.Length))
                        {
                            flag2 = false;
                            flag1 = false;
                            ++index;
                        }
                        else if (ch == qualifier && source.Length > index + 1 && source[index + 1] == qualifier)
                            ++index;
                    }
                    else if (ch == delimiter)
                        flag1 = false;
                    if (!flag1)
                    {
                        stringList.Add(stringBuilder.ToString());
                        stringBuilder.Length = 0;
                    }
                    else
                        stringBuilder.Append(ch);
                }
            }
            if (flag1)
                stringList.Add(stringBuilder.ToString());
            return stringList;
        }

        /// <summary>
        /// Splits string by a specified delimiter and keep nested string with a specified qualifier.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="delimiters">Delimiter characters.</param>
        /// <param name="qualifier">Qualifier character.</param>
        /// <returns>A list whose elements contain the substrings in this instance that are delimited by the delimiter.</returns>
        public static List<string> SplitNested(this string source, char[] delimiters, char qualifier = '"')
        {
            if (string.IsNullOrEmpty(source))
                return new List<string>();
            var stringBuilder = new StringBuilder();
            var stringList = new List<string>();
            var flag1 = false;
            var flag2 = false;
            for (var index = 0; index < source.Length; ++index)
            {
                var ch = source[index];
                if (!flag1)
                {
                    if (delimiters.Contains(ch))
                    {
                        stringList.Add(string.Empty);
                    }
                    else
                    {
                        if (ch == qualifier)
                            flag2 = true;
                        else
                            stringBuilder.Append(ch);
                        flag1 = true;
                    }
                }
                else
                {
                    if (flag2)
                    {
                        if (ch == qualifier && (source.Length > index + 1 && delimiters.Contains(source[index + 1]) ||
                                                index + 1 == source.Length))
                        {
                            flag2 = false;
                            flag1 = false;
                            ++index;
                        }
                        else if (ch == qualifier && source.Length > index + 1 && source[index + 1] == qualifier)
                            ++index;
                    }
                    else if (delimiters.Contains(ch))
                        flag1 = false;
                    if (!flag1)
                    {
                        stringList.Add(stringBuilder.ToString());
                        stringBuilder.Length = 0;
                    }
                    else
                        stringBuilder.Append(ch);
                }
            }
            if (flag1)
                stringList.Add(stringBuilder.ToString());
            return stringList;
        }

        /// <summary>Word wrap text for a specified maximum line length.</summary>
        /// <param name="source">Text to word wrap.</param>
        /// <param name="maxLineLength">Maximum length of a line.</param>
        /// <returns>A list of lines for the word wrapped text.</returns>
        public static List<string> WordWrap(this string source, int maxLineLength = 80)
        {
            var stringList = new List<string>();
            var empty = string.Empty;
            var str1 = source;
            var chArray = new char[1] {' '};
            foreach (var str2 in str1.Split(chArray))
            {
                if (empty.Length + str2.Length > maxLineLength)
                {
                    stringList.Add(empty);
                    empty = string.Empty;
                }
                empty += str2;
                if (empty.Length != maxLineLength)
                    empty += " ";
            }
            if (!string.IsNullOrEmpty(empty.Trim()))
                stringList.Add(empty);
            return stringList;
        }

        /// <summary>
        /// Concatenates the members of a collection of string, using the specified separator between each member.
        /// </summary>
        /// <param name="source">A collection that contains the strings to concatenate.</param>
        /// <param name="separator">The string to use as a separator. separator is included in the returned string only if values has more than one element.</param>
        /// <returns>A string that consists of the members of values delimited by the separator string. If values has no members, the method returns System.String.Empty.</returns>
        public static string JoinBy(this IEnumerable<string> source, string separator)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (separator == null)
                separator = string.Empty;
            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    return string.Empty;
                var stringBuilder = new StringBuilder();
                if (enumerator.Current != null)
                    stringBuilder.Append(enumerator.Current);
                while (enumerator.MoveNext())
                {
                    stringBuilder.Append(separator);
                    if (enumerator.Current != null)
                        stringBuilder.Append(enumerator.Current);
                }
                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// Escapes a minimal set of characters (\, *, +, ?, |, {, [, (,), ^, $,., #, and white space) by replacing them with their escape codes. This instructs the regular expression engine to interpret these characters literally rather than as metacharacters.
        /// </summary>
        /// <param name="source">The input string that contains the text to convert.</param>
        /// <returns>A string of characters with metacharacters converted to their escaped form.</returns>
        public static string Escape(this string source)
        {
            return Regex.Escape(source);
        }

        /// <summary>Converts any escaped characters in the source string.</summary>
        /// <param name="source">The input string containing the text to convert.</param>
        /// <returns>A string of characters with any escaped characters converted to their unescaped form.</returns>
        public static string Unescape(this string source)
        {
            return Regex.Unescape(source);
        }

        /// <summary>Gets empty string if string is null or empty.</summary>
        /// <param name="source">The source string.</param>
        /// <returns>Empty string if source string is null or empty; otherwise, source string.</returns>
        public static string EmptyIfNullOrEmpty(this string source)
        {
            return !string.IsNullOrEmpty(source) ? source : string.Empty;
        }

        /// <summary>Gets empty string if string is null or white space.</summary>
        /// <param name="source">The source string.</param>
        /// <returns>Empty string if source string is null or white space; otherwise, source string.</returns>
        public static string EmptyIfNullOrWhiteSpace(this string source)
        {
            return !source.IsNullOrWhiteSpace() ? source : string.Empty;
        }

        /// <summary>Gets null if string is null or empty.</summary>
        /// <param name="source">The source string.</param>
        /// <returns>Null if source string is null or empty; otherwise, source string.</returns>
        public static string NullIfNullOrEmpty(this string source)
        {
            return !string.IsNullOrEmpty(source) ? source : null;
        }

        /// <summary>Gets null if string is null or white space.</summary>
        /// <param name="source">The source string.</param>
        /// <returns>Null if source string is null or white space; otherwise, source string.</returns>
        public static string NullIfNullOrWhiteSpace(this string source)
        {
            return !source.IsNullOrWhiteSpace() ? source : null;
        }

        /// <summary>Retrieves a substring from the left.</summary>
        /// <param name="source">The source string.</param>
        /// <param name="length">The number of characters in the substring.</param>
        /// <returns>The sub string.</returns>
        public static string SubstringLeft(this string source, int length)
        {
            return source.Substring(0, length);
        }

        /// <summary>Retrieves a substring from the right.</summary>
        /// <param name="source">The source string.</param>
        /// <param name="length">The number of characters in the substring.</param>
        /// <returns>The sub string.</returns>
        public static string SubstringRight(this string source, int length)
        {
            return source.Substring(source.Length - length);
        }
    }
}