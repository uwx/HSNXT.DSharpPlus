// Decompiled with JetBrains decompiler
// Type: ExtensionMinder.StringExtension
// Assembly: ExtensionMinder, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9895DAEA-F52E-4F0C-B5FD-FB311AEFF61C
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\ExtensionMinder.dll

using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace ExtensionMinder
{
    public static class StringExtension
    {
        public static string Escape(this string s)
        {
            return HttpUtility.HtmlEncode(s);
        }

        public static string Unescape(this string s)
        {
            return HttpUtility.HtmlDecode(s);
        }

        public static bool In(this string value, params string[] stringValues)
        {
            return stringValues.Any(stringValue => string.CompareOrdinal(value, stringValue) == 0);
        }

        public static string Right(this string value, int length)
        {
            if (value == null || value.Length <= length)
                return value;
            return value.Substring(value.Length - length);
        }

        public static string Left(this string value, int length)
        {
            if (value == null || value.Length <= length)
                return value;
            return value.Substring(0, length);
        }

        public static string Format(this string value, object arg0)
        {
            return string.Format(value, arg0);
        }

        public static string Format(this string value, params object[] args)
        {
            return string.Format(value, args);
        }

        public static string Base64Encode(this string plainText)
        {
            if (string.IsNullOrWhiteSpace(plainText))
                return null;
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            if (string.IsNullOrWhiteSpace(base64EncodedData))
                return null;
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));
        }

        public static bool IsValidRegex(this string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
                return false;
            try
            {
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                Regex.Match("", pattern);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }

        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            if (str.Length < 2)
                return str.ToLowerInvariant();
            return str.Substring(0, 1).ToLowerInvariant() + str.Substring(1);
        }

        public static bool EndsWithWhitespace(string q)
        {
            return Regex.IsMatch(q, @"^.*\s$");
        }

        public static string ToTitleCase(this string str)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static string ToTitleCase(this string str, string cultureInfoName)
        {
            return new CultureInfo(cultureInfoName).TextInfo.ToTitleCase(str.ToLower());
        }

        public static string ToTitleCase(this string str, CultureInfo cultureInfo)
        {
            return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
        }
    }
}