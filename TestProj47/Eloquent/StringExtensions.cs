using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace HSNXT
{
    public static partial class Extensions
    {
        #region Convert

        public static byte ToByte(this string value)
        {
            return Convert.ToByte(value, CultureInfo.InvariantCulture);
        }

        public static DateTime ToDateTime(this string value)
        {
            return Convert.ToDateTime(value, CultureInfo.InvariantCulture);
        }

        public static int ToInt16(this string value)
        {
            return Convert.ToInt16(value, CultureInfo.InvariantCulture);
        }

        public static sbyte ToSByte(this string value)
        {
            return Convert.ToSByte(value, CultureInfo.InvariantCulture);
        }

        public static float ToSingle(this string value)
        {
            return Convert.ToSingle(value, CultureInfo.InvariantCulture);
        }

        public static ushort ToUInt16(this string value)
        {
            return Convert.ToUInt16(value, CultureInfo.InvariantCulture);
        }

        public static uint ToUInt32(this string value)
        {
            return Convert.ToUInt32(value, CultureInfo.InvariantCulture);
        }

        public static ulong ToUInt64(this string value)
        {
            return Convert.ToUInt64(value, CultureInfo.InvariantCulture);
        }

        #endregion

        #region Parsing

        public static DateTime ParseDateTime(this string value)
        {
            return DateTime.Parse(value, CultureInfo.InvariantCulture);
        }

        public static DateTime ParseDateTime(this string value, IFormatProvider provider)
        {
            return DateTime.Parse(value, provider);
        }

        public static DateTime ParseDateTime(this string value, IFormatProvider provider, DateTimeStyles styles)
        {
            return DateTime.Parse(value, provider, styles);
        }

        public static bool TryParseDateTime(this string value, out DateTime result)
        {
            return DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out result);
        }

        public static bool TryParseDateTime(this string value, out DateTime result, IFormatProvider provider)
        {
            return DateTime.TryParse(value, provider, DateTimeStyles.AssumeLocal, out result);
        }

        public static bool TryParseDateTime(this string value, out DateTime result, IFormatProvider provider,
            DateTimeStyles styles)
        {
            return DateTime.TryParse(value, provider, styles, out result);
        }

        public static short ParseInt16(this string value)
        {
            return short.Parse(value, CultureInfo.InvariantCulture);
        }

        public static bool TryParseInt16(this string value, out short result)
        {
            return short.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
        }

        public static int ParseInt32(this string value)
        {
            return int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
        }

        public static bool TryParseInt32(this string value, out int result)
        {
            return int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
        }

        public static long ParseInt64(this string value)
        {
            return long.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
        }

        public static bool TryParseInt64(this string value, out long result)
        {
            return long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
        }

        #endregion

        #region Queries

        /// <summary>
        /// Returns a measure of the information content of a string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Entropy(this string value)
        {
            var dict = CountDistinctCharacters(value);

            var logTwo = Math.Log(2);

            var infoCount = dict.Select(kv => kv.Value / value.Length).Select(freq => freq * Math.Log(freq) / logTwo)
                .Sum();
            infoCount *= -1;
            return infoCount;
        }

        private static Dictionary<char, double> CountDistinctCharacters(string value)
        {
            var dict = new Dictionary<char, double>();

            foreach (var c in value)
            {
                if (!dict.ContainsKey(c))
                    dict[c] = 1;
                else
                    dict[c] = dict[c] + 1;
            }
            return dict;
        }

        #endregion

        #region Functions

        public static MatchCollection RegexMatches(this string value, string pattern)
        {
            return Regex.Matches(value, pattern);
        }

        public static string RegexReplace(this string value, string pattern, string replacement)
        {
            var regex = new Regex(pattern);
            return regex.Replace(value, replacement);
        }

        /// <summary>
        /// Returns an escaped string where whitespace characters are replaced with their non-whitespace symbol.
        ///     HorizontalTab:  \t
        ///     VerticalTab:    \v
        ///     NewLine:        \n
        ///     CarriageReturn: \r
        ///     FormFeed:       \f
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToLiteral(this string value)
        {
            using (var writer = new StringWriter(CultureInfo.InvariantCulture))
            {
                using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    var opts = new CodeGeneratorOptions {VerbatimOrder = true};
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(value), writer, opts);
                    return writer.ToString();
                }
            }
        }

        #endregion
    }
}