// Decompiled with JetBrains decompiler
// Type: ExtensionMinder.StringExtension
// Assembly: ExtensionMinder, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9895DAEA-F52E-4F0C-B5FD-FB311AEFF61C
// Assembly location: ...\bin\Debug\ExtensionMinder.dll

using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static bool In(this string value, params string[] stringValues)
        {
            return stringValues.Any(stringValue => string.CompareOrdinal(value, stringValue) == 0);
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

        public static bool EndsWithWhitespace(string q)
        {
            return Regex.IsMatch(q, @"^.*\s$");
        }

        public static string ToTitleCase(this string str, string cultureInfoName)
        {
            return new CultureInfo(cultureInfoName).TextInfo.ToTitleCase(str.ToLower());
        }
    }
}