// Decompiled with JetBrains decompiler
// Type: dff.Extensions.StringExtensions
// Assembly: dff.Extensions, Version=1.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D6C927DF-93D7-4A34-9061-9B93EC850F98
// Assembly location: ...\bin\Debug\dff.Extensions.dll

using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using HSNXT.dff.Extensions.Gps;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static string ShortenString(this string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            if (maxLength <= 3 | text.Length <= maxLength)
                return text;
            try
            {
                var num1 = Math.Floor(text.Length * 0.75);
                var num2 = text.Length - maxLength + 3;
                text = text.Substring(0, (int) (num1 - num2 * 0.75)) + "..." +
                       text.Substring((int) (num1 + num2 * 0.25));
            }
            catch (Exception)
            {
                return text.Substring(0, maxLength - 3) + "...";
            }
            return text;
        }

        public static string RemoveLastSeperator(this string text)
        {
            if (text == null)
                return null;
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            text = text.TrimEnd(' ');
            while (text.EndsWith(",") | text.EndsWith(";") | text.EndsWith("|") | text.EndsWith("@") |
                   text.EndsWith("#") | text.EndsWith("+") | text.EndsWith("*") | text.EndsWith("-") |
                   text.EndsWith("_"))
                text = text.Substring(0, text.Length - 1);
            if (text.EndsWith(Environment.NewLine))
                text = text.Substring(0, text.LastIndexOf(Environment.NewLine, StringComparison.Ordinal));
            return text;
        }

        public static int TryToInt(this string value)
        {
            try
            {
                return int.Parse(value);
            }
            catch
            {
                return 0;
            }
        }

        public static DateTime GetDateTime(this string date)
        {
            try
            {
                var provider = (IFormatProvider) new CultureInfo("de-DE", true);
                return DateTime.Parse(date, provider, DateTimeStyles.NoCurrentDateDefault);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static string GetLast(this string source, int tailLength)
        {
            return tailLength >= source.Length ? source : source.Substring(source.Length - tailLength);
        }

        public static string RemoveLast(this string source, int removeCharacters)
        {
            return source.Length >= removeCharacters ? source.Substring(0, source.Length - removeCharacters) : source;
        }

        public static string RemoveFirst(this string source, int removeCharacters)
        {
            return source.Length >= removeCharacters ? source.Substring(removeCharacters) : source;
        }

        public static Bitmap BitmapFromBase64(string base64)
        {
            try
            {
                Bitmap bitmap;
                using (var memoryStream = new MemoryStream(Convert.FromBase64String(base64)))
                    bitmap = new Bitmap(memoryStream);
                return bitmap;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public static string GetMd5Hash(this string source)
        {
            try
            {
                if (source == null)
                    return string.Empty;
                var hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(source));
                var stringBuilder = new StringBuilder();
                foreach (var b in hash)
                    stringBuilder.Append(b.ToString("X2"));
                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return string.Empty;
            }
        }

        public static string RemoveTextBetween(this string source, string startDelimiter, string endDelimiter)
        {
            try
            {
                var num1 = source.IndexOf(startDelimiter, StringComparison.CurrentCulture);
                var str = source.Substring(0, num1);
                var num2 = source.IndexOf(endDelimiter, num1, StringComparison.CurrentCulture);
                return str + source.Substring(num2 + endDelimiter.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return string.Empty;
            }
        }

        public static DffGpsPosition GetDffGpsPosition(this string position)
        {
            try
            {
                return new DffGpsPosition(position);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}