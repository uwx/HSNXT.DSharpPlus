// Decompiled with JetBrains decompiler
// Type: ExtensionMethods.ByteExtensions
// Assembly: ExtensionMethods, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A78E21D-808B-4A21-BFAA-D781C27D2CD5
// Assembly location: ...\bin\Debug\ExtensionMethods.dll

using System;
using System.Collections.Generic;
using System.Text;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        /// Converts a collection of bytes into hexadecimal string respresentation in lower case.
        /// </summary>
        /// <param name="bytes">Bytes to be converted.</param>
        /// <returns>Hexadecimal representation in lowercase.</returns>
        public static string ToHexString(this IEnumerable<byte> bytes)
        {
            return bytes.ToHexString(string.Empty, false);
        }

        /// <summary>
        /// Converts a collection of bytes into hexadecimal string respresentation.
        /// </summary>
        /// <param name="bytes">Bytes to be converted</param>
        /// <param name="separator">String to separate the hexadecimal digits.
        /// Use string.Empty if you do not need a spearator.</param>
        /// <param name="uppercase">True to return hexadecimal digits in uppercase;
        /// false returns hexadecimal digits in lowercase.</param>
        /// <returns>Hexadecimal representation</returns>
        /// <exception cref="T:System.ArgumentNullException">When <paramref name="separator" /> is null.</exception>
        public static string ToHexString(this IEnumerable<byte> bytes, string separator, bool uppercase)
        {
            if (separator == null)
                throw new ArgumentNullException(nameof(separator));
            var stringBuilder = new StringBuilder();
            var format = uppercase ? "{0:X2}{1}" : "{0:x2}{1}";
            foreach (var num in bytes)
                stringBuilder.AppendFormat(format, num, separator);
            return stringBuilder.ToString(0, stringBuilder.Length - separator.Length);
        }
    }
}