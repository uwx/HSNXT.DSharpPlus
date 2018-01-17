using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;

//using System.Data.SqlServerCe;

// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A byte[] extension method that resizes the byte[].
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="newSize">Size of the new.</param>
        /// <returns>A byte[].</returns>
        public static byte[] Resize(this byte[] @this, int newSize)
        {
            Array.Resize(ref @this, newSize);
            return @this;
        }

        /// <summary>
        ///     A byte[] extension method that converts the @this to an image.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an Image.</returns>
        public static Image ToImage(this byte[] @this)
        {
            using (var ms = new MemoryStream(@this))
            {
                return Image.FromStream(ms);
            }
        }

        /// <summary>
        ///     A byte[] extension method that converts the @this to a memory stream.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a MemoryStream.</returns>
        public static MemoryStream ToMemoryStream(this byte[] @this)
        {
            return new MemoryStream(@this);
        }

        /// <summary>
        ///     Converts a subset of an 8-bit unsigned integer array to an equivalent subset of a Unicode character array
        ///     encoded with base-64 digits. Parameters specify the subsets as offsets in the input and output arrays, and
        ///     the number of elements in the input array to convert.
        /// </summary>
        /// <param name="inArray">An input array of 8-bit unsigned integers.</param>
        /// <param name="offsetIn">A position within .</param>
        /// <param name="length">The number of elements of  to convert.</param>
        /// <param name="outArray">An output array of Unicode characters.</param>
        /// <param name="offsetOut">A position within .</param>
        /// <returns>A 32-bit signed integer containing the number of bytes in .</returns>
        public static int ToBase64CharArray(this byte[] inArray, int offsetIn, int length, char[] outArray,
            int offsetOut)
        {
            return Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut);
        }

        /// <summary>
        ///     Converts a subset of an 8-bit unsigned integer array to an equivalent subset of a Unicode character array
        ///     encoded with base-64 digits. Parameters specify the subsets as offsets in the input and output arrays, the
        ///     number of elements in the input array to convert, and whether line breaks are inserted in the output array.
        /// </summary>
        /// <param name="inArray">An input array of 8-bit unsigned integers.</param>
        /// <param name="offsetIn">A position within .</param>
        /// <param name="length">The number of elements of  to convert.</param>
        /// <param name="outArray">An output array of Unicode characters.</param>
        /// <param name="offsetOut">A position within .</param>
        /// <param name="options">to insert a line break every 76 characters, or  to not insert line breaks.</param>
        /// <returns>A 32-bit signed integer containing the number of bytes in .</returns>
        public static int ToBase64CharArray(this byte[] inArray, int offsetIn, int length, char[] outArray,
            int offsetOut, Base64FormattingOptions options)
        {
            return Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut, options);
        }

        /// <summary>
        ///     Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with
        ///     base-64 digits.
        /// </summary>
        /// <param name="inArray">An array of 8-bit unsigned integers.</param>
        /// <returns>The string representation, in base 64, of the contents of .</returns>
        public static string ToBase64String(this byte[] inArray)
        {
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        ///     Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with
        ///     base-64 digits. A parameter specifies whether to insert line breaks in the return value.
        /// </summary>
        /// <param name="inArray">An array of 8-bit unsigned integers.</param>
        /// <param name="options">to insert a line break every 76 characters, or  to not insert line breaks.</param>
        /// <returns>The string representation in base 64 of the elements in .</returns>
        public static string ToBase64String(this byte[] inArray, Base64FormattingOptions options)
        {
            return Convert.ToBase64String(inArray, options);
        }

        /// <summary>
        ///     Converts a subset of an array of 8-bit unsigned integers to its equivalent string representation that is
        ///     encoded with base-64 digits. Parameters specify the subset as an offset in the input array, and the number of
        ///     elements in the array to convert.
        /// </summary>
        /// <param name="inArray">An array of 8-bit unsigned integers.</param>
        /// <param name="offset">An offset in .</param>
        /// <param name="length">The number of elements of  to convert.</param>
        /// <returns>The string representation in base 64 of  elements of , starting at position .</returns>
        public static string ToBase64String(this byte[] inArray, int offset, int length)
        {
            return Convert.ToBase64String(inArray, offset, length);
        }

        /// <summary>
        ///     Converts a subset of an array of 8-bit unsigned integers to its equivalent string representation that is
        ///     encoded with base-64 digits. Parameters specify the subset as an offset in the input array, the number of
        ///     elements in the array to convert, and whether to insert line breaks in the return value.
        /// </summary>
        /// <param name="inArray">An array of 8-bit unsigned integers.</param>
        /// <param name="offset">An offset in .</param>
        /// <param name="length">The number of elements of  to convert.</param>
        /// <param name="options">to insert a line break every 76 characters, or  to not insert line breaks.</param>
        /// <returns>The string representation in base 64 of  elements of , starting at position .</returns>
        public static string ToBase64String(this byte[] inArray, int offset, int length,
            Base64FormattingOptions options)
        {
            return Convert.ToBase64String(inArray, offset, length, options);
        }

#if NetFX
        /// <summary>
        ///     Encodes a byte array into its equivalent string representation using base 64 digits, which is usable for
        ///     transmission on the URL.
        /// </summary>
        /// <param name="input">The byte array to encode.</param>
        /// <returns>
        ///     The string containing the encoded token if the byte array length is greater than one; otherwise, an empty
        ///     string (&quot;&quot;).
        /// </returns>
        public static string UrlTokenEncode(this byte[] input)
        {
            return HttpServerUtility.UrlTokenEncode(input);
        }
#endif
        
        /// <summary>
        ///     Converts a URL-encoded byte array into a decoded string using the specified decoding object.
        /// </summary>
        /// <param name="bytes">The array of bytes to decode.</param>
        /// <param name="e">The  that specifies the decoding scheme.</param>
        /// <returns>A decoded string.</returns>
        public static string UrlDecodeZ(this byte[] bytes, Encoding e)
        {
            return HttpUtility.UrlDecode(bytes, e);
        }

        /// <summary>
        ///     Converts a URL-encoded byte array into a decoded string using the specified encoding object, starting at the
        ///     specified position in the array, and continuing for the specified number of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes to decode.</param>
        /// <param name="offset">The position in the byte to begin decoding.</param>
        /// <param name="count">The number of bytes to decode.</param>
        /// <param name="e">The  object that specifies the decoding scheme.</param>
        /// <returns>A decoded string.</returns>
        public static string UrlDecodeZ(this byte[] bytes, int offset, int count, Encoding e)
        {
            return HttpUtility.UrlDecode(bytes, offset, count, e);
        }

        /// <summary>
        ///     Converts a URL-encoded array of bytes into a decoded array of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes to decode.</param>
        /// <returns>A decoded array of bytes.</returns>
        public static byte[] UrlDecodeToBytes(this byte[] bytes)
        {
            return HttpUtility.UrlDecodeToBytes(bytes);
        }

        /// <summary>
        ///     Converts a URL-encoded array of bytes into a decoded array of bytes, starting at the specified position in
        ///     the array and continuing for the specified number of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes to decode.</param>
        /// <param name="offset">The position in the byte array at which to begin decoding.</param>
        /// <param name="count">The number of bytes to decode.</param>
        /// <returns>A decoded array of bytes.</returns>
        public static byte[] UrlDecodeToBytes(this byte[] bytes, int offset, int count)
        {
            return HttpUtility.UrlDecodeToBytes(bytes, offset, count);
        }

        /// <summary>
        ///     Converts a byte array into an encoded URL string.
        /// </summary>
        /// <param name="bytes">The array of bytes to encode.</param>
        /// <returns>An encoded string.</returns>
        public static string UrlEncodeZ(this byte[] bytes)
        {
            return HttpUtility.UrlEncode(bytes);
        }

        /// <summary>
        ///     Converts a byte array into a URL-encoded string, starting at the specified position in the array and
        ///     continuing for the specified number of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes to encode.</param>
        /// <param name="offset">The position in the byte array at which to begin encoding.</param>
        /// <param name="count">The number of bytes to encode.</param>
        /// <returns>An encoded string.</returns>
        public static string UrlEncodeZ(this byte[] bytes, int offset, int count)
        {
            return HttpUtility.UrlEncode(bytes, offset, count);
        }

        /// <summary>
        ///     Converts an array of bytes into a URL-encoded array of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes to encode.</param>
        /// <returns>An encoded array of bytes.</returns>
        public static byte[] UrlEncodeToBytes(this byte[] bytes)
        {
            return HttpUtility.UrlEncodeToBytes(bytes);
        }

        /// <summary>
        ///     Converts an array of bytes into a URL-encoded array of bytes, starting at the specified position in the array
        ///     and continuing for the specified number of bytes.
        /// </summary>
        /// <param name="bytes">The array of bytes to encode.</param>
        /// <param name="offset">The position in the byte array at which to begin encoding.</param>
        /// <param name="count">The number of bytes to encode.</param>
        /// <returns>An encoded array of bytes.</returns>
        public static byte[] UrlEncodeToBytes(this byte[] bytes, int offset, int count)
        {
            return HttpUtility.UrlEncodeToBytes(bytes, offset, count);
        }
    }
}