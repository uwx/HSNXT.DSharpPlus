// Decompiled with JetBrains decompiler
// Type: TestProj47.ByteExtensions
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace HSNXT
{
    public static partial class Extensions

    {
        /// <summary>Convert BitArray to byte array.</summary>
        /// <param name="source">The source BitArray.</param>
        /// <returns>Byte array.</returns>
        public static byte[] ToByteArray(this BitArray source)
        {
            var numArray = new byte[(source.Length - 1) / 8 + 1];
            source.CopyTo(numArray, 0);
            return numArray;
        }

        /// <summary>Convert BitArray to bool array.</summary>
        /// <param name="source">The source BitArray.</param>
        /// <returns>bool array.</returns>
        public static bool[] ToBoolArray(this BitArray source)
        {
            var flagArray = new bool[source.Length];
            source.CopyTo(flagArray, 0);
            return flagArray;
        }

        /// <summary>Convert BitArray to binary digit Int32 array.</summary>
        /// <param name="source">The source BitArray.</param>
        /// <returns>Binary digit Int32 array.</returns>
        public static int[] ToBitIntArray(this BitArray source)
        {
            var numArray = new int[source.Length];
            source.CopyTo(numArray, 0);
            return numArray;
        }

        /// <summary>Convert BitArray to binary digit string.</summary>
        /// <param name="source">The source BitArray.</param>
        /// <returns>Binary digit string.</returns>
        public static string ToBitString(this BitArray source)
        {
            var stringBuilder = new StringBuilder(source.Length);
            foreach (bool flag in source)
                stringBuilder.Append(flag ? "1" : "0");
            var str = stringBuilder.ToString();
            stringBuilder.Length = 0;
            return str;
        }

        /// <summary>Convert byte array to BitArray.</summary>
        /// <param name="source">The source byte array.</param>
        /// <returns>BitArray instance.</returns>
        public static BitArray ToBitArray(this byte[] source)
        {
            return new BitArray(source);
        }

        /// <summary>Convert byte to BitArray.</summary>
        /// <param name="source">The source byte.</param>
        /// <returns>BitArray instance.</returns>
        public static BitArray ToBitArray(this byte source)
        {
            return new BitArray(new byte[1] {source});
        }

        /// <summary>Convert Int32 to BitArray.</summary>
        /// <param name="source">The source Int32.</param>
        /// <returns>BitArray instance.</returns>
        public static BitArray ToBitArray(this int source)
        {
            return new BitArray(new int[1] {source});
        }

        /// <summary>Convert bit string to BitArray.</summary>
        /// <param name="source">The source bit string.</param>
        /// <returns>BitArray instance.</returns>
        public static BitArray BitStringToBitArray(this string source)
        {
            var values = new bool[source.Length];
            for (var index = 0; index < source.Length; ++index)
                values[index] = source[index] != 48;
            return new BitArray(values);
        }

        /// <summary>Convert bytes to Hex string.</summary>
        /// <param name="source">Byte array.</param>
        /// <param name="delimiter">Delimiter character.</param>
        /// <returns>Hex string.</returns>
        public static string ToHexString(this IList<byte> source, char? delimiter = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var chArray1 = new char[source.Count * 2];
            var index1 = 0;
            var index2 = 0;
            while (index1 < source.Count)
            {
                var num1 = (byte) ((uint) source[index1] >> 4);
                chArray1[index2] = (int) num1 > 9 ? (char) (num1 + 55 + 32) : (char) (num1 + 48);
                var num2 = (byte) (source[index1] & 15U);
                int num3;
                chArray1[num3 = index2 + 1] = (int) num2 > 9 ? (char) (num2 + 55 + 32) : (char) (num2 + 48);
                ++index1;
                index2 = num3 + 1;
            }
            var nullable = delimiter;
            if (!(nullable.HasValue ? nullable.GetValueOrDefault() : new int?()).HasValue)
                return new string(chArray1).ToUpperInvariant();
            var chArray2 = new char[source.Count * 3];
            for (var index3 = 0; index3 < source.Count; ++index3)
            {
                chArray2[index3 * 3] = chArray1[index3 * 2];
                chArray2[index3 * 3 + 1] = chArray1[index3 * 2 + 1];
                chArray2[index3 * 3 + 2] = delimiter.Value;
            }
            return new string(chArray2).TrimEnd(delimiter.Value).ToUpperInvariant();
        }

        /// <summary>Convert byte to Hex string.</summary>
        /// <param name="source">Source byte.</param>
        /// <returns>Hex string.</returns>
        public static string ToHexString(this byte source)
        {
            return Convert.ToString(source, 16).PadLeft(2, '0');
        }

        /// <summary>Convert Hex string to byte array.</summary>
        /// <param name="source">Hex string.</param>
        /// <returns>Byte array.</returns>
        public static byte[] HexToByteArray(this string source)
        {
            var str = source.RemoveAny(false, ' ', '-', '\n', '\r');
            if (str.Length % 2 == 1)
                str = "0" + str;
            var numArray = new byte[str.Length / 2];
            var index1 = 0;
            var index2 = 0;
            while (index1 < numArray.Length)
            {
                var ch1 = str[index2];
                numArray[index1] =
                    (byte) (((int) ch1 > 57 ? ((int) ch1 > 90 ? ch1 - 97 + 10 : ch1 - 65 + 10) : ch1 - 48) << 4);
                int num;
                var ch2 = str[num = index2 + 1];
                numArray[index1] |= (int) ch2 > 57
                    ? ((int) ch2 > 90 ? (byte) (ch2 - 97 + 10) : (byte) (ch2 - 65 + 10))
                    : (byte) (ch2 - 48);
                ++index1;
                index2 = num + 1;
            }
            return numArray;
        }

        /// <summary>Convert bytes to Encoding string.</summary>
        /// <param name="source">Byte array.</param>
        /// <param name="encoding">Instance of Encoding.</param>
        /// <returns>Encoding string.</returns>
        public static string ToEncodingString(this byte[] source, Encoding encoding = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            return (encoding ?? Encoding.UTF8).GetString(source);
        }
        
#if NetFX
        /// <summary>Convert Image to bytes.</summary>
        /// <param name="source">Image to convert.</param>
        /// <returns>Byte array.</returns>
        public static byte[] ImageToByteArray(this Image source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            return (byte[]) new ImageConverter().ConvertTo(source, typeof(byte[]));
        }
#endif
        /// <summary>Compresses byte array using CompressionType.</summary>
        /// <param name="source">Byte array to compress.</param>
        /// <param name="compressionType">Compression Type.</param>
        /// <returns>A compressed byte array.</returns>
        public static byte[] Compress(this byte[] source, CompressionType compressionType = CompressionType.GZip)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            using (var memoryStream = new MemoryStream())
            {
                using (var zipStream = GetZipStream(memoryStream, CompressionMode.Compress, compressionType))
                    zipStream.Write(source, 0, source.Length);
                return memoryStream.ToArray();
            }
        }

        /// <summary>Decompresses byte array using CompressionType.</summary>
        /// <param name="source">Byte array to decompress.</param>
        /// <param name="compressionType">Compression Type.</param>
        /// <returns>A decompressed byte array.</returns>
        public static byte[] Decompress(this byte[] source, CompressionType compressionType = CompressionType.GZip)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            using (var memoryStream1 = new MemoryStream())
            {
                using (var memoryStream2 = new MemoryStream(source))
                {
                    using (var zipStream = GetZipStream(memoryStream2, CompressionMode.Decompress, compressionType))
                    {
                        var buffer = new byte[81920];
                        int count;
                        while ((count = zipStream.Read(buffer, 0, buffer.Length)) != 0)
                            memoryStream1.Write(buffer, 0, count);
                    }
                }
                return memoryStream1.ToArray();
            }
        }

        /// <summary>Get Zip Stream by type.</summary>
        /// <param name="memoryStream">Instance of MemoryStream.</param>
        /// <param name="compressionMode">Compression mode.</param>
        /// <param name="compressionType">Compression type.</param>
        /// <returns>Instance of Stream.</returns>
        private static Stream GetZipStream(MemoryStream memoryStream, CompressionMode compressionMode,
            CompressionType compressionType)
        {
            if (compressionType == CompressionType.GZip)
                return new GZipStream(memoryStream, compressionMode);
            return new DeflateStream(memoryStream, compressionMode);
        }
    }
}