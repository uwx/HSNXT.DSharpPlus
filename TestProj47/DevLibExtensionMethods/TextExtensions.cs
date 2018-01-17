// Decompiled with JetBrains decompiler
// Type: TestProj47.TextExtensions
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
#if NetFX
using System.Web.Security;
#endif

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>The alphabet and numeric chars.</summary>
        private static readonly string[] AlphaNumericChars =
        {
            "a",
            "b",
            "c",
            "d",
            "e",
            "f",
            "g",
            "h",
            "i",
            "j",
            "k",
            "l",
            "m",
            "n",
            "o",
            "p",
            "q",
            "r",
            "s",
            "t",
            "u",
            "v",
            "w",
            "x",
            "y",
            "z",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z"
        };

        /// <summary>
        /// The Base 15 chars set.
        /// 123456789ABCDEF
        /// </summary>
        public const string Base15Chars = "123456789ABCDEF";

        /// <summary>
        /// The Base 26 chars set.
        /// 0123456789ABCDEF
        /// </summary>
        public const string Base16Chars = "0123456789ABCDEF";

        /// <summary>
        /// The Base 26 chars set.
        /// ABCDEFGHIJKLMNOPQRSTUVWXYZ
        /// </summary>
        public const string Base26Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// The Base 35 chars set.
        /// 123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ
        /// </summary>
        public const string Base35Chars = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// The Base 36 chars set.
        /// 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ
        /// </summary>
        public const string Base36Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// The Base 52 chars set.
        /// ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz
        /// </summary>
        public const string Base52Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// The Base 61 chars set.
        /// 123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz
        /// </summary>
        public const string Base61Chars = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// The Base 62 chars set.
        /// 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz
        /// </summary>
        public const string Base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        /// <summary>Field CP1252Encoding.</summary>
        private static volatile Encoding CP1252Encoding;

        /// <summary>
        /// Gets an encoding for the CP1252 (Windows-1252) character set.
        /// </summary>
        public static Encoding CP1252
        {
            get
            {
                if (CP1252Encoding == null)
                    CP1252Encoding = Encoding.GetEncoding(1252);
                return CP1252Encoding;
            }
        }

        /// <summary>
        /// Gets an encoding for the CP1252 (Windows-1252) character set.
        /// </summary>
        /// <param name="source">Any object.</param>
        /// <returns>Encoding instance for the CP1252 (Windows-1252) character set.</returns>
        public static Encoding GetCP1252Encoding(this object source)
        {
            return CP1252;
        }
        
#if NetFX
        /// <summary>Shortens the specified source string.</summary>
        /// <param name="source">The source.</param>
        /// <returns>4 shorten string candidates in string array.</returns>
        public static string[] Shorten(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return new[]
                {
                    source,
                    source,
                    source,
                    source
                };
#pragma warning disable 618
            var str = FormsAuthentication.HashPasswordForStoringInConfigFile(source, "md5");
#pragma warning restore 618
            var strArray = new string[4];
            for (var index1 = 0; index1 < 4; ++index1)
            {
                if (str == null) throw new ArgumentException(nameof(str));
                var num = 1073741823 & Convert.ToInt32("0x" + str.Substring(index1 * 8, 8), 16);
                var empty = string.Empty;
                for (var index2 = 0; index2 < 6; ++index2)
                {
                    var index3 = 61 & num;
                    empty += AlphaNumericChars[index3];
                    num >>= 5;
                }
                strArray[index1] = empty;
            }
            return strArray;
        }
#endif

        /// <summary>Base64 decodes a string.</summary>
        /// <param name="source">A base64 encoded string.</param>
        /// <returns>Decoded string.</returns>
        public static string Base64Decode(this string source)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(source));
        }

        /// <summary>Base64 encodes a string.</summary>
        /// <param name="source">String to encode.</param>
        /// <returns>A base64 encoded string.</returns>
        public static string Base64Encode(this string source)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(source));
        }

        /// <summary>Encodes to 123456789ABCDEF chars set.</summary>
        /// <param name="source">The source number.</param>
        /// <returns>A Based encoded string.</returns>
        public static string Base15Encode(this int source)
        {
            return ((long) source).Base15Encode();
        }

        /// <summary>Encodes to 123456789ABCDEF chars set.</summary>
        /// <param name="source">The source number.</param>
        /// <returns>A Based encoded string.</returns>
        public static string Base15Encode(this long source)
        {
            return source.BaseEncode("123456789ABCDEF");
        }

        /// <summary>Decodes 123456789ABCDEF Based string to number.</summary>
        /// <param name="source">The source Based string.</param>
        /// <returns>The decoded number.</returns>
        public static long Base15Decode(this string source)
        {
            return source.BaseDecode("123456789ABCDEF");
        }

        /// <summary>Encodes to 0123456789ABCDEF chars set.</summary>
        /// <param name="source">The source number.</param>
        /// <returns>A Based encoded string.</returns>
        public static string Base16Encode(this int source)
        {
            return ((long) source).Base16Encode();
        }

        /// <summary>Encodes to 0123456789ABCDEF chars set.</summary>
        /// <param name="source">The source number.</param>
        /// <returns>A Based encoded string.</returns>
        public static string Base16Encode(this long source)
        {
            return source.BaseEncode("0123456789ABCDEF");
        }

        /// <summary>Decodes 0123456789ABCDEF Based string to number.</summary>
        /// <param name="source">The source Based string.</param>
        /// <returns>The decoded number.</returns>
        public static long Base16Decode(this string source)
        {
            return source.BaseDecode("0123456789ABCDEF");
        }

        /// <summary>Encodes to ABCDEFGHIJKLMNOPQRSTUVWXYZ chars set.</summary>
        /// <param name="source">The source number.</param>
        /// <returns>A Based encoded string.</returns>
        public static string Base26Encode(this int source)
        {
            return ((long) source).Base26Encode();
        }

        /// <summary>Encodes to ABCDEFGHIJKLMNOPQRSTUVWXYZ chars set.</summary>
        /// <param name="source">The source number.</param>
        /// <returns>A Based encoded string.</returns>
        public static string Base26Encode(this long source)
        {
            return source.BaseEncode("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }

        /// <summary>
        /// Decodes ABCDEFGHIJKLMNOPQRSTUVWXYZ Based string to number.
        /// </summary>
        /// <param name="source">The source Based string.</param>
        /// <returns>The decoded number.</returns>
        public static long Base26Decode(this string source)
        {
            return source.BaseDecode("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }

        /// <summary>
        /// Encodes to 123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ chars set.
        /// </summary>
        /// <param name="source">The source number.</param>
        /// <returns>A Based encoded string.</returns>
        public static string Base35Encode(this int source)
        {
            return ((long) source).Base35Encode();
        }

        /// <summary>
        /// Encodes to 123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ chars set.
        /// </summary>
        /// <param name="source">The source number.</param>
        /// <returns>A Based encoded string.</returns>
        public static string Base35Encode(this long source)
        {
            return source.BaseEncode("123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }

        /// <summary>
        /// Decodes 123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ Based string to number.
        /// </summary>
        /// <param name="source">The source Based string.</param>
        /// <returns>The decoded number.</returns>
        public static long Base35Decode(this string source)
        {
            return source.BaseDecode("123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }

        /// <summary>
        /// Encodes to 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ chars set.
        /// </summary>
        /// <param name="source">The source number.</param>
        /// <returns>A Based encoded string.</returns>
        public static string Base36Encode(this int source)
        {
            return ((long) source).Base36Encode();
        }

        /// <summary>
        /// Encodes to 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ chars set.
        /// </summary>
        /// <param name="source">The source number.</param>
        /// <returns>A Based encoded string.</returns>
        public static string Base36Encode(this long source)
        {
            return source.BaseEncode("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }

        /// <summary>
        /// Decodes 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ Based string to number.
        /// </summary>
        /// <param name="source">The source Based string.</param>
        /// <returns>The decoded number.</returns>
        public static long Base36Decode(this string source)
        {
            return source.BaseDecode("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }

        /// <summary>
        /// Encodes to ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz chars set.
        /// </summary>
        /// <param name="source">The source number.</param>
        /// <returns>A Based encoded string.</returns>
        public static string Base52Encode(this int source)
        {
            return ((long) source).Base52Encode();
        }

        /// <summary>
        /// Encodes to ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz chars set.
        /// </summary>
        /// <param name="source">The source number.</param>
        /// <returns>A Based encoded string.</returns>
        public static string Base52Encode(this long source)
        {
            return source.BaseEncode("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
        }

        /// <summary>
        /// Decodes ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz Based string to number.
        /// </summary>
        /// <param name="source">The source Based string.</param>
        /// <returns>The decoded number.</returns>
        public static long Base52Decode(this string source)
        {
            return source.BaseDecode("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
        }

        /// <summary>
        /// Encodes to 123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz chars set.
        /// </summary>
        /// <param name="source">The source number.</param>
        /// <returns>A Based encoded string.</returns>
        public static string Base61Encode(this int source)
        {
            return ((long) source).Base61Encode();
        }

        /// <summary>
        /// Encodes to 123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz chars set.
        /// </summary>
        /// <param name="source">The source number.</param>
        /// <returns>A Based encoded string.</returns>
        public static string Base61Encode(this long source)
        {
            return source.BaseEncode("123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
        }

        /// <summary>
        /// Decodes 123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz Based string to number.
        /// </summary>
        /// <param name="source">The source Based string.</param>
        /// <returns>The decoded number.</returns>
        public static long Base61Decode(this string source)
        {
            return source.BaseDecode("123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
        }

        /// <summary>
        /// Encodes to 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz chars set.
        /// </summary>
        /// <param name="source">The source number.</param>
        /// <returns>A Based encoded string.</returns>
        public static string Base62Encode(this int source)
        {
            return ((long) source).Base62Encode();
        }

        /// <summary>
        /// Encodes to 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz chars set.
        /// </summary>
        /// <param name="source">The source number.</param>
        /// <returns>A Based encoded string.</returns>
        public static string Base62Encode(this long source)
        {
            return source.BaseEncode("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
        }

        /// <summary>
        /// Decodes 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz Based string to number.
        /// </summary>
        /// <param name="source">The source Based string.</param>
        /// <returns>The decoded number.</returns>
        public static long Base62Decode(this string source)
        {
            return source.BaseDecode("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
        }

        /// <summary>
        /// Encodes to 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz chars set.
        /// </summary>
        /// <param name="source">The source bytes.</param>
        /// <returns>A Based encoded string.</returns>
        public static string Base62EncodeBytes(this byte[] source)
        {
            var stringBuilder = new StringBuilder();
            var bitStream = new BitStream(source);
            var buffer = new byte[1];
            int num;
            while (true)
            {
                buffer[0] = 0;
                num = bitStream.Read(buffer, 0, 6);
                switch (num)
                {
                    case 6:
                        switch (buffer[0] >> 3)
                        {
                            case 31:
                                stringBuilder.Append(
                                    "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"[61]);
                                bitStream.Seek(-1L, SeekOrigin.Current);
                                continue;
                            case 30:
                                stringBuilder.Append(
                                    "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"[60]);
                                bitStream.Seek(-1L, SeekOrigin.Current);
                                continue;
                        }
                        stringBuilder.Append(
                            "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"[buffer[0] >> 2]);
                        continue;
                    case 0:
                        goto label_8;
                    default:
                        goto label_7;
                }
            }
            label_7:
            stringBuilder.Append(
                "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"[buffer[0] >> 8 - num]);
            label_8:
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Decodes 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz Based string to number.
        /// </summary>
        /// <param name="source">The source Based string.</param>
        /// <returns>The decoded bytes.</returns>
        public static byte[] Base62DecodeBytes(this string source)
        {
            var num1 = 0;
            var bitStream = new BitStream(source.Length * 6 / 8);
            foreach (var ch in source)
            {
                var num2 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".IndexOf(ch);
                if (num1 == source.Length - 1)
                {
                    var num3 = (int) (bitStream.Position % 8L);
                    if (num3 == 0)
                        throw new InvalidDataException("An extra character was found");
                    if (num2 >> 8 - num3 > 0)
                        throw new InvalidDataException("Invalid ending character was found");
                    bitStream.Write(new[]
                    {
                        (byte) (num2 << num3)
                    }, 0, 8 - num3);
                }
                else
                    switch (num2)
                    {
                        case 60:
                            bitStream.Write(new byte[] {240}, 0, 5);
                            break;
                        case 61:
                            bitStream.Write(new byte[] {248}, 0, 5);
                            break;
                        default:
                            bitStream.Write(new[] {(byte) num2}, 2, 6);
                            break;
                    }
                ++num1;
            }
            var buffer = new byte[bitStream.Position / 8L];
            bitStream.Seek(0L, SeekOrigin.Begin);
            bitStream.Read(buffer, 0, buffer.Length * 8);
            return buffer;
        }

        /// <summary>Encodes to target Base chars set.</summary>
        /// <param name="source">The source number.</param>
        /// <param name="baseChars">The Base chars set.</param>
        /// <returns>A Based encoded string.</returns>
        public static string BaseEncode(this int source, string baseChars)
        {
            return ((long) source).BaseEncode(baseChars);
        }

        /// <summary>Encodes to target Base chars set.</summary>
        /// <param name="source">The source number.</param>
        /// <param name="baseChars">The Base chars set.</param>
        /// <returns>A Based encoded string.</returns>
        public static string BaseEncode(this long source, string baseChars)
        {
            Decimal num = source;
            var flag = num < new Decimal(0);
            if (flag)
                num = new Decimal(-1, -1, 0, false, 0) + num;
            long length = baseChars.Length;
            var charStack = new Stack<char>();
            do
            {
                charStack.Push(baseChars[(int) (num % length)]);
                num = (long) (num / length);
            } while (num != new Decimal(0));
            var str = new string(charStack.ToArray());
            if (!flag)
                return str;
            return "-" + str;
        }

        /// <summary>Decodes Based string to number.</summary>
        /// <param name="source">The source Based string.</param>
        /// <param name="baseChars">The Base chars set.</param>
        /// <returns>The decoded number.</returns>
        public static long BaseDecode(this string source, string baseChars)
        {
            var flag = source.StartsWith("-") && source.Length > 1;
            if (flag)
                source = source.Substring(1);
            var length = baseChars.Length;
            var num = new Decimal(0);
            foreach (var ch in source)
                num = num * length + baseChars.IndexOf(ch);
            return (long) (flag ? num - new Decimal(-1, -1, 0, false, 0) : num);
        }
    }
}