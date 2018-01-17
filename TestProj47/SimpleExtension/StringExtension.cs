// Decompiled with JetBrains decompiler
// Type: SimpleExtension.StringExtension
// Assembly: SimpleExtension, Version=0.0.23.0, Culture=neutral, PublicKeyToken=null
// MVID: B43B1EC6-29EF-47CC-B98E-E2FE4FC2095C
// Assembly location: ...\bin\Debug\SimpleExtension.dll

using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static double GetDouble(this string value, double defaultValue)
        {
            if (!double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out var result) &&
                !double.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                !double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                result = defaultValue;
            return result;
        }

        public static string Substring(this string @this, string from = null, string until = null,
            StringComparison comparison = StringComparison.InvariantCulture)
        {
            var length = (from ?? string.Empty).Length;
            var startIndex = !string.IsNullOrEmpty(from) ? @this.IndexOf(from, comparison) + length : 0;
            if (startIndex < length)
                throw new ArgumentException("from: Failed to find an instance of the first anchor");
            var num = !string.IsNullOrEmpty(until) ? @this.IndexOf(until, startIndex, comparison) : @this.Length;
            if (num < 0)
                throw new ArgumentException("until: Failed to find an instance of the last anchor");
            return @this.Substring(startIndex, num - startIndex);
        }

        public static Stream ToStream(this string pValue)
        {
            return pValue != null ? new MemoryStream(Encoding.UTF8.GetBytes(pValue)) : null;
        }

        public static byte[] BinHexToString(this string hex)
        {
            var index1 = hex.StartsWith("0x") ? 2 : 0;
            if (hex.Length % 2 != 0)
                throw new ArgumentException($"Invalid Hex String : {(object) hex.Length}");
            var numArray = new byte[(hex.Length - index1) / 2];
            for (var index2 = 0; index2 < numArray.Length; ++index2)
            {
                numArray[index2] = (byte) (hex[index1].ParseHexChar() << 4 | hex[index1 + 1].ParseHexChar());
                index1 += 2;
            }
            return numArray;
        }

        public static string ExtractString(this string source, string beginDelim, string endDelim,
            bool caseSensitive = false, bool allowMissingEndDelimiter = false, bool returnDelimiters = false)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            int startIndex;
            int num;
            if (caseSensitive)
            {
                startIndex = source.IndexOf(beginDelim, StringComparison.Ordinal);
                if (startIndex == -1)
                    return string.Empty;
                num = !returnDelimiters
                    ? source.IndexOf(endDelim, startIndex + beginDelim.Length, StringComparison.Ordinal)
                    : source.IndexOf(endDelim, startIndex, StringComparison.Ordinal);
            }
            else
            {
                startIndex = source.IndexOf(beginDelim, 0, source.Length, StringComparison.OrdinalIgnoreCase);
                if (startIndex == -1)
                    return string.Empty;
                num = !returnDelimiters
                    ? source.IndexOf(endDelim, startIndex + beginDelim.Length, StringComparison.OrdinalIgnoreCase)
                    : source.IndexOf(endDelim, startIndex, StringComparison.OrdinalIgnoreCase);
            }
            if (allowMissingEndDelimiter && num == -1)
                return source.Substring(startIndex + beginDelim.Length);
            if (startIndex <= -1 || num <= 1)
                return string.Empty;
            return !returnDelimiters
                ? source.Substring(startIndex + beginDelim.Length, num - startIndex - beginDelim.Length)
                : source.Substring(startIndex, num - startIndex + endDelim.Length);
        }

        public static string FixHtmlForDisplay(this string html)
        {
            html = html.Replace("<", "&lt;");
            html = html.Replace(">", "&gt;");
            html = html.Replace("\"", "&quot;");
            return html;
        }

        public static decimal ParseDecimal(this string input, IFormatProvider numberFormat)
        {
            decimal.TryParse(input, NumberStyles.Any, numberFormat, out var result);
            return result;
        }

        public static int ParseInt(this string input, IFormatProvider numberFormat)
        {
            int.TryParse(input, NumberStyles.Any, numberFormat, out var result);
            return result;
        }

        public static int ParseInt(this string input)
        {
            return input.ParseInt(CultureInfo.CurrentCulture.NumberFormat);
        }

        public static string ProperCase(string input)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(input);
        }

        public static string ReplaceString(this string origString, string findString, string replaceString,
            bool caseInsensitive)
        {
            var startIndex = 0;
            while (true)
            {
                var length = !caseInsensitive
                    ? origString.IndexOf(findString, startIndex, StringComparison.Ordinal)
                    : origString.IndexOf(findString, startIndex, origString.Length - startIndex,
                        StringComparison.OrdinalIgnoreCase);
                if (length != -1)
                {
                    origString =
                        $"{(object) origString.Substring(0, length)}{(object) replaceString}{(object) origString.Substring(length + findString.Length)}";
                    startIndex = length + replaceString.Length;
                }
                else
                    break;
            }
            return origString;
        }

        public static string ReplaceStringInstance(this string origString, string findString, string replaceWith,
            int instance, bool caseInsensitive)
        {
            if (instance == -1)
                return origString.ReplaceString(findString, replaceWith, caseInsensitive);
            var num = 0;
            for (var index = 0; index < instance; ++index)
            {
                num = !caseInsensitive
                    ? origString.IndexOf(findString, num, StringComparison.Ordinal)
                    : origString.IndexOf(findString, num, origString.Length - num, StringComparison.OrdinalIgnoreCase);
                if (num == -1)
                    return origString;
                if (index < instance - 1)
                    num += findString.Length;
            }
            return
                $"{(object) origString.Substring(0, num)}{(object) replaceWith}{(object) origString.Substring(num + findString.Length)}";
        }

        public static string Replicate(this string input, int charCount)
        {
            return new StringBuilder().Insert(0, input, charCount).ToString();
        }

        public static string Replicate(this char character, int charCount)
        {
            return new string(character, charCount);
        }

        public static string SetProperty(this string propertyString, string key, string value)
        {
            var oldValue = propertyString.ExtractString($"<{(object) key}>", $"</{(object) key}>");
            if (string.IsNullOrEmpty(value) && oldValue != string.Empty)
                return propertyString.Replace(oldValue, "");
            var newValue = $"<{(object) key}>{(object) value}</{(object) key}>";
            return oldValue != string.Empty
                ? propertyString.Replace(oldValue, newValue)
                : $"{(object) propertyString}{(object) newValue}\r\n";
        }

        public static Stream StringToStream(this string text, Encoding encoding)
        {
            var memoryStream = new MemoryStream(text.Length * 2);
            var bytes = encoding.GetBytes(text);
            var buffer = bytes;
            var offset = 0;
            var length = bytes.Length;
            memoryStream.Write(buffer, offset, length);
            long num = 0;
            memoryStream.Position = num;
            return memoryStream;
        }

        public static Stream StringToStream(this string text)
        {
            return text.StringToStream(Encoding.Default);
        }

        public static string StripNonNumber(this string input)
        {
            var charArray = input.ToCharArray();
            var stringBuilder = new StringBuilder();
            foreach (var c in charArray)
            {
                if (char.IsNumber(c) || char.IsSeparator(c))
                    stringBuilder.Append(c);
            }
            return stringBuilder.ToString();
        }

        public static string TerminateString(this string value, string terminator)
        {
            if (string.IsNullOrEmpty(value) || value.EndsWith(terminator))
                return value;
            return $"{(object) value}{(object) terminator}";
        }

        public static string TextAbstract(this string text, int length)
        {
            if (text == null)
                return string.Empty;
            if (text.Length <= length)
                return text;
            text = text.Substring(0, length);
            text = text.Substring(0, text.LastIndexOf(" ", StringComparison.Ordinal));
            return $"{(object) text}...";
        }

        public static string ToCamelCase(this string phrase)
        {
            if (phrase == null)
                return string.Empty;
            var stringBuilder = new StringBuilder(phrase.Length);
            var flag = true;
            foreach (var c in phrase)
            {
                if (char.IsWhiteSpace(c) || char.IsPunctuation(c) || char.IsSeparator(c))
                {
                    flag = true;
                }
                else
                {
                    stringBuilder.Append(flag ? char.ToUpper(c) : char.ToLower(c));
                    flag = false;
                }
            }
            return stringBuilder.ToString();
        }

        public static string TrimTo(this string value, int charCount)
        {
            return value == null ? string.Empty : (value.Length > charCount ? value.Substring(0, charCount) : value);
        }

        private static int ParseHexChar(this char c)
        {
            if (c >= 48 && c <= 57)
                return c - 48;
            if (c >= 65 && c <= 70)
                return c - 65 + 10;
            if (c >= 97 && c <= 102)
                return c - 97 + 10;
            throw new ArgumentException($"Invalid Hex String{(object) c}");
        }
    }
}