// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BigInteger.cs">
//   Copyright (c) 2014 Alexander Logger. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region [The Bouncy Castle License] Base license of partial code used in this file.

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2000-2011 The Legion Of The Bouncy Castle (http://www.bouncycastle.org)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
// and associated documentation files (the "Software"), to deal in the Software without restriction, 
// including without limitation the rights to use, copy, modify, merge, publish, distribute, 
// sub license, and/or sell copies of the Software, and to permit persons to whom the Software 
// is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using HSNXT.Utils;

// ReSharper disable UnusedMethodReturnValue.Local
// ReSharper disable SuggestBaseTypeForParameter

namespace HSNXT
{
    /// <summary>
    ///     Big integer.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class BigInteger : IComparable<BigInteger>, IComparable, IEquatable<BigInteger>, IFormattable
    {
        // The primes b/w 2 and ~2^10
        /*
                3   5   7   11  13  17  19  23  29
            31  37  41  43  47  53  59  61  67  71
            73  79  83  89  97  101 103 107 109 113
            127 131 137 139 149 151 157 163 167 173
            179 181 191 193 197 199 211 223 227 229
            233 239 241 251 257 263 269 271 277 281
            283 293 307 311 313 317 331 337 347 349
            353 359 367 373 379 383 389 397 401 409
            419 421 431 433 439 443 449 457 461 463
            467 479 487 491 499 503 509 521 523 541
            547 557 563 569 571 577 587 593 599 601
            607 613 617 619 631 641 643 647 653 659
            661 673 677 683 691 701 709 719 727 733
            739 743 751 757 761 769 773 787 797 809
            811 821 823 827 829 839 853 857 859 863
            877 881 883 887 907 911 919 929 937 941
            947 953 967 971 977 983 991 997
            1009 1013 1019 1021 1031
        */

        // Each list has a product < 2^31
        private const long Mask = 0xffffffffL;

        private const ulong Umask = Mask;
        private const int BitsPerByte = 8;
        private const int BitsPerInt = 32;
        private const int BytesPerInt = 4;
        private const int Chunk2 = 1; // TODO Parse 64 bits at a time
        private const int Chunk10 = 19;
        private const int Chunk16 = 16;

        private static readonly int[][] PrimeLists =
        {
            new[] {3, 5, 7, 11, 13, 17, 19, 23}, new[] {29, 31, 37, 41, 43}, new[] {47, 53, 59, 61, 67},
            new[] {71, 73, 79, 83},
            new[] {89, 97, 101, 103}, new[] {107, 109, 113, 127}, new[] {131, 137, 139, 149},
            new[] {151, 157, 163, 167}, new[] {173, 179, 181, 191},
            new[] {193, 197, 199, 211}, new[] {223, 227, 229}, new[] {233, 239, 241}, new[] {251, 257, 263},
            new[] {269, 271, 277}, new[] {281, 283, 293},
            new[] {307, 311, 313}, new[] {317, 331, 337}, new[] {347, 349, 353}, new[] {359, 367, 373},
            new[] {379, 383, 389}, new[] {397, 401, 409},
            new[] {419, 421, 431}, new[] {433, 439, 443}, new[] {449, 457, 461}, new[] {463, 467, 479},
            new[] {487, 491, 499}, new[] {503, 509, 521},
            new[] {523, 541, 547}, new[] {557, 563, 569}, new[] {571, 577, 587}, new[] {593, 599, 601},
            new[] {607, 613, 617}, new[] {619, 631, 641},
            new[] {643, 647, 653}, new[] {659, 661, 673}, new[] {677, 683, 691}, new[] {701, 709, 719},
            new[] {727, 733, 739}, new[] {743, 751, 757},
            new[] {761, 769, 773}, new[] {787, 797, 809}, new[] {811, 821, 823}, new[] {827, 829, 839},
            new[] {853, 857, 859}, new[] {863, 877, 881},
            new[] {883, 887, 907}, new[] {911, 919, 929}, new[] {937, 941, 947}, new[] {953, 967, 971},
            new[] {977, 983, 991}, new[] {997, 1009, 1013},
            new[] {1019, 1021, 1031}
        };

        private static readonly int[] PrimeProducts;

        private static readonly int[] ZeroMagnitude = new int[0];
        private static readonly byte[] ZeroEncoding = new byte[0];

        public static readonly BigInteger Zero = new BigInteger(0, ZeroMagnitude, false);
        public static readonly BigInteger One = CreateUValueOf(1);
        public static readonly BigInteger Two = CreateUValueOf(2);
        public static readonly BigInteger Three = CreateUValueOf(3);
        public static readonly BigInteger Ten = CreateUValueOf(10);

        private static readonly BigInteger Radix2 = ValueOf(2);
        private static readonly BigInteger Radix2E = Radix2.Pow(Chunk2);

        private static readonly BigInteger Radix10 = ValueOf(10);
        private static readonly BigInteger Radix10E = Radix10.Pow(Chunk10);

        private static readonly BigInteger Radix16 = ValueOf(16);
        private static readonly BigInteger Radix16E = Radix16.Pow(Chunk16);

        private static readonly Random RandomSource = new Random();
        private static readonly byte[] RndMask = {255, 127, 63, 31, 15, 7, 3, 1};

        private static readonly byte[] BitCounts =
        {
            0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2, 3, 2, 3, 3, 4, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 1, 2, 2, 3,
            2, 3, 3, 4,
            2, 3, 3, 4, 3, 4, 4, 5, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4,
            3, 4, 4, 5, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4,
            5, 5, 6, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7, 1,
            2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 2, 3,
            3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 3, 4, 4, 5, 4, 5,
            5, 6, 4, 5, 5, 6, 5, 6, 6, 7, 2, 3, 3, 4, 3, 4, 4,
            5, 3, 4, 4, 5, 4, 5, 5, 6, 3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7, 3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5,
            6, 5, 6, 6, 7, 4, 5, 5, 6, 5, 6, 6, 7, 5, 6, 6, 7,
            6, 7, 7, 8
        };

        #region Fields

        private long _mQuote = -1L; // -m^(-1) mod b, b = 2^32 (see Montgomery mult.)
        private int[] _magnitude; // Array of ints with [0] being the most significant (Big-endian).
        private int _nBitLength = -1; // cache calcBitLength() value
        private int _nBits = -1; // cache BitCount() value
        private int _sign; // -1 means -ve; +1 means +ve; 0 means 0;

        #endregion

        #region Constructors

        static BigInteger()
        {
            PrimeProducts = new int[PrimeLists.Length];

            for (var i = 0; i < PrimeLists.Length; ++i)
            {
                var primeList = PrimeLists[i];
                var product = primeList.Aggregate(1, (current, t) => current * t);
                PrimeProducts[i] = product;
            }
        }

        private BigInteger()
        {
        }

        private BigInteger(int signum, int[] mag, bool checkMag)
        {
            if (checkMag)
            {
                var i = 0;
                while (i < mag.Length && mag[i] == 0)
                {
                    ++i;
                }

                if (i == mag.Length)
                {
                    _magnitude = ZeroMagnitude;
                }
                else
                {
                    _sign = signum;

                    if (i == 0)
                    {
                        _magnitude = mag;
                    }
                    else
                    {
                        // strip leading 0 words
                        _magnitude = new int[mag.Length - i];
                        Array.Copy(mag, i, _magnitude, 0, _magnitude.Length);
                    }
                }
            }
            else
            {
                _sign = signum;
                _magnitude = mag;
            }
        }

        public BigInteger(string value) : this(value, 10)
        {
        }

        public BigInteger(string str, int radix, IFormatProvider formatProvider = null)
        {
            if (str.Length == 0)
            {
                throw new FormatException("Zero length BigInteger");
            }

            if (formatProvider == null)
            {
                formatProvider = CultureInfo.InvariantCulture;
            }
            var nfi = (NumberFormatInfo) formatProvider.GetFormat(typeof(NumberFormatInfo));

            NumberStyles style;
            int chunk;
            BigInteger r;
            BigInteger rE;

            switch (radix)
            {
                case 2:
                    // Is there anyway to restrict to binary digits?
                    style = NumberStyles.Integer;
                    chunk = Chunk2;
                    r = Radix2;
                    rE = Radix2E;
                    break;
                case 10:
                    // This style seems to handle spaces and minus sign already (our processing redundant?)
                    style = NumberStyles.Integer;
                    chunk = Chunk10;
                    r = Radix10;
                    rE = Radix10E;
                    break;
                case 16:
                    // TODO Should this be HexNumber?
                    style = NumberStyles.AllowHexSpecifier;
                    chunk = Chunk16;
                    r = Radix16;
                    rE = Radix16E;
                    break;
                default:
                    throw new FormatException("Only bases 2, 10, or 16 are allowed.");
            }


            var index = 0;
            _sign = 1;

            if (nfi != null && str.StartsWith(nfi.NegativeSign))
            {
                if (str.Length == nfi.NegativeSign.Length)
                {
                    throw new FormatException("Zero length BigInteger.");
                }

                _sign = -1;
                index = nfi.NegativeSign.Length;
            }

            // strip leading zeros from the string str
            while (index < str.Length && Int32.Parse(str[index].ToString(), style) == 0)
            {
                index++;
            }

            if (index >= str.Length)
            {
                // zero value - we're done
                _sign = 0;
                _magnitude = ZeroMagnitude;
                return;
            }

            //////
            // could we work out the max number of ints required to store
            // str.Length digits in the given base, then allocate that
            // storage in one hit?, then Generate the magnitude in one hit too?
            //////

            var b = Zero;


            var next = index + chunk;

            while (next <= str.Length)
            {
                var s = str.Substring(index, chunk);
                var i = ulong.Parse(s, style);
                var bi = CreateUValueOf(i);

                switch (radix)
                {
                    case 2:
                        // TODO Need this because we are parsing in radix 10 above
                        if (i > 1)
                        {
                            throw new FormatException("Bad character in radix 2 string: " + s);
                        }

                        // TODO Parse 64 bits at a time
                        b = b.ShiftLeft(1);
                        break;
                    case 16:
                        b = b.ShiftLeft(64);
                        break;
                    default:
                        b = b.Multiply(rE);
                        break;
                }

                b = b.Add(bi);

                index = next;
                next += chunk;
            }

            if (index < str.Length)
            {
                var s = str.Substring(index);
                var i = ulong.Parse(s, style);
                var bi = CreateUValueOf(i);

                if (b._sign > 0)
                {
                    if (radix == 2)
                    {
                        // NB: Can't reach here since we are parsing one char at a time
                        Debug.Assert(false);

                        // TODO Parse all bits at once
                        //						b = b.ShiftLeft(s.Length);
                    }
                    else if (radix == 16)
                    {
                        b = b.ShiftLeft(s.Length << 2);
                    }
                    else
                    {
                        b = b.Multiply(r.Pow(s.Length));
                    }

                    b = b.Add(bi);
                }
                else
                {
                    b = bi;
                }
            }

            // Note: This is the previous (slower) algorithm
            //			while (index < value.Length)
            //            {
            //				char c = value[index];
            //				string s = c.ToString();
            //				int i = Int32.Parse(s, style);
            //
            //                b = b.Multiply(r).Add(ValueOf(i));
            //                index++;
            //            }

            _magnitude = b._magnitude;
        }

        public BigInteger(byte[] bytes) : this(bytes, 0, bytes.Length)
        {
        }

        public BigInteger(byte[] bytes, int offset, int length)
        {
            if (length == 0)
            {
                throw new FormatException("Zero length BigInteger");
            }

            // TODO Move this processing into MakeMagnitude (provide sign argument)
            if ((sbyte) bytes[offset] < 0)
            {
                _sign = -1;

                var end = offset + length;

                int iBval;
                // strip leading sign bytes
                for (iBval = offset; iBval < end && (sbyte) bytes[iBval] == -1; iBval++)
                {
                }

                if (iBval >= end)
                {
                    _magnitude = One._magnitude;
                }
                else
                {
                    var numBytes = end - iBval;
                    var inverse = new byte[numBytes];

                    var index = 0;
                    while (index < numBytes)
                    {
                        inverse[index++] = (byte) ~bytes[iBval++];
                    }

                    Debug.Assert(iBval == end);

                    while (inverse[--index] == byte.MaxValue)
                    {
                        inverse[index] = byte.MinValue;
                    }

                    inverse[index]++;

                    _magnitude = MakeMagnitude(inverse, 0, inverse.Length);
                }
            }
            else
            {
                // strip leading zero bytes and return magnitude bytes
                _magnitude = MakeMagnitude(bytes, offset, length);
                _sign = _magnitude.Length > 0 ? 1 : 0;
            }
        }

        public BigInteger(int sign, byte[] bytes) : this(sign, bytes, 0, bytes.Length)
        {
        }

        public BigInteger(int sign, byte[] bytes, int offset, int length)
        {
            if (sign < -1 || sign > 1)
            {
                throw new FormatException("Invalid sign value");
            }

            if (sign == 0)
            {
                //this.sign = 0;
                _magnitude = ZeroMagnitude;
            }
            else
            {
                // copy bytes
                _magnitude = MakeMagnitude(bytes, offset, length);
                _sign = _magnitude.Length < 1 ? 0 : sign;
            }
        }

        public BigInteger(int sizeInBits, Random random)
        {
            if (sizeInBits < 0)
            {
                throw new ArgumentException("sizeInBits must be non-negative");
            }

            _nBits = -1;
            _nBitLength = -1;

            if (sizeInBits == 0)
            {
                //				this.sign = 0;
                _magnitude = ZeroMagnitude;
                return;
            }

            var nBytes = GetByteLength(sizeInBits);
            var b = new byte[nBytes];
            random.NextBytes(b);

            // strip off any excess bits in the MSB
            b[0] &= RndMask[BitsPerByte * nBytes - sizeInBits];

            _magnitude = MakeMagnitude(b, 0, b.Length);
            _sign = _magnitude.Length < 1 ? 0 : 1;
        }

        public BigInteger(int bitLength, int certainty, Random random)
        {
            if (bitLength < 2)
            {
                throw new ArithmeticException("bitLength < 2");
            }

            _sign = 1;
            _nBitLength = bitLength;

            if (bitLength == 2)
            {
                _magnitude = random.Next(2) == 0 ? Two._magnitude : Three._magnitude;
                return;
            }

            var nBytes = GetByteLength(bitLength);
            var b = new byte[nBytes];

            var xBits = BitsPerByte * nBytes - bitLength;
            var mask = RndMask[xBits];

            for (;;)
            {
                random.NextBytes(b);

                // strip off any excess bits in the MSB
                b[0] &= mask;

                // ensure the leading bit is 1 (to meet the strength requirement)
                b[0] |= (byte) (1 << (7 - xBits));

                // ensure the trailing bit is 1 (i.e. must be odd)
                b[nBytes - 1] |= 1;

                _magnitude = MakeMagnitude(b, 0, b.Length);
                _nBits = -1;
                _mQuote = -1L;

                if (certainty < 1)
                {
                    break;
                }

                if (CheckProbablePrime(certainty, random))
                {
                    break;
                }

                if (bitLength > 32)
                {
                    for (var rep = 0; rep < 10000; ++rep)
                    {
                        var n = 33 + random.Next(bitLength - 2);
                        _magnitude[_magnitude.Length - (n >> 5)] ^= 1 << (n & 31);
                        _magnitude[_magnitude.Length - 1] ^= (random.Next() + 1) << 1;
                        _mQuote = -1L;

                        if (CheckProbablePrime(certainty, random))
                        {
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BigInteger" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public BigInteger(byte value) : this((ulong) value)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BigInteger" /> struct.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public BigInteger(bool value) : this((ulong) (value ? 1 : 0))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BigInteger" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public BigInteger(char value) : this((ulong) value)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BigInteger" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public BigInteger(decimal value)
        {
            var isNegative = value < 0;
            var bits = decimal.GetBits(value).ConvertAll(i => (uint) i);
            var scale = (bits[3] >> 16) & 0x1F;
            if (scale > 0)
            {
                MathUtils.DivModUnsigned(bits, new[] {10U * scale}, out var quotient, out _);

                bits = quotient;
            }

            _magnitude = bits.ConvertAll(u => (int) u);
            _sign = isNegative ? -1 : 1;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BigInteger" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public BigInteger(double value) : this((decimal) value)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BigInteger" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public BigInteger(float value) : this((decimal) value)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BigInteger" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public BigInteger(short value) : this((ulong) value)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BigInteger" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public BigInteger(int value) : this((ulong) value)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BigInteger" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public BigInteger(long value) : this((ulong) value)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BigInteger" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public BigInteger(sbyte value) : this((ulong) value)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BigInteger" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public BigInteger(ushort value) : this((ulong) value)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BigInteger" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public BigInteger(uint value) : this((ulong) value)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BigInteger" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public BigInteger(ulong value) : this(value.ToBytes(false))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BigInteger" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public BigInteger(Guid value) : this(value.ToByteArray())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BigInteger" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public BigInteger(Int128 value) : this(value.ToBytes(false))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BigInteger" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public BigInteger(Int256 value) : this(value.ToBytes(false))
        {
        }

        #endregion

        public int BitCount
        {
            get
            {
                if (_nBits == -1)
                {
                    if (_sign < 0)
                    {
                        // TODO Optimise this case
                        _nBits = Not().BitCount;
                    }
                    else
                    {
                        var sum = 0;
                        foreach (int t in _magnitude)
                        {
                            sum += BitCounts[(byte) t];
                            sum += BitCounts[(byte) (t >> 8)];
                            sum += BitCounts[(byte) (t >> 16)];
                            sum += BitCounts[(byte) (t >> 24)];
                        }
                        _nBits = sum;
                    }
                }

                return _nBits;
            }
        }

        public int BitLength
        {
            get
            {
                if (_nBitLength == -1)
                {
                    _nBitLength = _sign == 0 ? 0 : CalcBitLength(0, _magnitude);
                }

                return _nBitLength;
            }
        }

        public int IntValue
        {
            get
            {
                return _sign == 0 ? 0 :
                    _sign > 0 ? _magnitude[_magnitude.Length - 1] : -_magnitude[_magnitude.Length - 1];
            }
        }

        public long LongValue
        {
            get
            {
                if (_sign == 0)
                {
                    return 0;
                }

                long v;
                if (_magnitude.Length > 1)
                {
                    v = ((long) _magnitude[_magnitude.Length - 2] << 32) | (_magnitude[_magnitude.Length - 1] & Mask);
                }
                else
                {
                    v = _magnitude[_magnitude.Length - 1] & Mask;
                }

                return _sign < 0 ? -v : v;
            }
        }

        public int Sign
        {
            get { return _sign; }
        }

        /// <summary>
        ///     Compares the current instance with another object of the same type and returns an integer that indicates whether
        ///     the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has these meanings: Value
        ///     Meaning Less than zero This instance is less than <paramref name="obj" />. Zero This instance is equal to
        ///     <paramref name="obj" />. Greater than zero This instance is greater than <paramref name="obj" />.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="obj" /> is not the same type as this instance.
        /// </exception>
        int IComparable.CompareTo(object obj)
        {
            return Compare(this, obj);
        }

        /// <summary>
        ///     Compares this instance to a specified big integer and returns an indication of their relative values.
        /// </summary>
        /// <param name="value">An integer to compare.</param>
        /// <returns>A signed number indicating the relative values of this instance and value.</returns>
        public int CompareTo(BigInteger value)
        {
            return Compare(this, value);
        }

        public bool Equals(BigInteger other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other._sign != _sign || other._magnitude.Length != _magnitude.Length)
            {
                return false;
            }

            for (var i = 0; i < _magnitude.Length; i++)
            {
                if (other._magnitude[i] != _magnitude[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="format">
        ///     The format. Only x, X, g, G, d, D, b, B are supported.
        /// </param>
        /// <param name="formatProvider">Format provider.</param>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (formatProvider == null)
            {
                formatProvider = CultureInfo.InvariantCulture;
            }

            if (string.IsNullOrEmpty(format))
            {
                format = "G";
            }
            var ch = format[0];
            var caps = char.IsUpper(ch);
            if (!int.TryParse(format.Substring(1).Trim(), out var min))
            {
                min = 1;
            }
            switch (char.ToUpperInvariant(ch))
            {
                case 'X':
                {
                    return ToString(16, formatProvider, caps, min);
                }
                case 'G':
                case 'D':
                {
                    return ToString(10, formatProvider, caps, min);
                }
                case 'B':
                {
                    return ToString(2, formatProvider, caps, min);
                }
                default:
                    throw new NotSupportedException("Not supported format: " + format);
            }
        }

        #region Operators

        /// <summary>
        ///     Performs an implicit conversion from <see cref="System.Boolean" /> to <see cref="BigInteger" />.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator BigInteger(bool value)
        {
            return new BigInteger(value);
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="System.Byte" /> to <see cref="BigInteger" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator BigInteger(byte value)
        {
            return new BigInteger(value);
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="System.Char" /> to <see cref="BigInteger" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator BigInteger(char value)
        {
            return new BigInteger(value);
        }

        /// <summary>
        ///     Performs an explicit conversion from <see cref="System.Decimal" /> to <see cref="BigInteger" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator BigInteger(decimal value)
        {
            return new BigInteger(value);
        }

        /// <summary>
        ///     Performs an explicit conversion from <see cref="System.Double" /> to <see cref="BigInteger" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator BigInteger(double value)
        {
            return new BigInteger(value);
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="System.Int16" /> to <see cref="BigInteger" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator BigInteger(short value)
        {
            return new BigInteger(value);
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="System.Int32" /> to <see cref="BigInteger" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator BigInteger(int value)
        {
            return new BigInteger(value);
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="System.Int64" /> to <see cref="BigInteger" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator BigInteger(long value)
        {
            return new BigInteger(value);
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="System.SByte" /> to <see cref="BigInteger" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator BigInteger(sbyte value)
        {
            return new BigInteger(value);
        }

        /// <summary>
        ///     Performs an explicit conversion from <see cref="System.Single" /> to <see cref="BigInteger" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator BigInteger(float value)
        {
            return new BigInteger(value);
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="System.UInt16" /> to <see cref="BigInteger" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator BigInteger(ushort value)
        {
            return new BigInteger(value);
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="System.UInt32" /> to <see cref="BigInteger" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator BigInteger(uint value)
        {
            return new BigInteger(value);
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="System.UInt64" /> to <see cref="BigInteger" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator BigInteger(ulong value)
        {
            return new BigInteger(value);
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="Int128" /> to <see cref="BigInteger" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator BigInteger(Int128 value)
        {
            return new BigInteger(value);
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="Int256" /> to <see cref="BigInteger" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator BigInteger(Int256 value)
        {
            return new BigInteger(value);
        }

        /// <summary>
        ///     Performs an explicit conversion from <see cref="BigInteger" /> to <see cref="System.Boolean" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator bool(BigInteger value)
        {
            return value._sign != 0;
        }

        /// <summary>
        ///     Performs an explicit conversion from <see cref="BigInteger" /> to <see cref="System.Byte" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator byte(BigInteger value)
        {
            if (value._sign == 0)
            {
                return 0;
            }

            if (value < byte.MinValue || value > byte.MaxValue)
            {
                throw new OverflowException();
            }

            return (byte) value._magnitude[0];
        }

        /// <summary>
        ///     Performs an explicit conversion from <see cref="BigInteger" /> to <see cref="System.Char" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator char(BigInteger value)
        {
            if (value.Sign == 0)
            {
                return (char) 0;
            }

            if (value < char.MinValue || value > char.MaxValue)
            {
                throw new OverflowException();
            }

            return (char) (ushort) value._magnitude[0];
        }

        /// <summary>
        ///     Performs an explicit conversion from <see cref="BigInteger" /> to <see cref="System.Decimal" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator decimal(BigInteger value)
        {
            throw new NotImplementedException();

            /*if (value.Sign == 0)
            {
                return 0;
            }

            if ((value < (BigInteger)decimal.MinValue) || (value > (BigInteger)decimal.MaxValue))
            {
                throw new OverflowException();
            }

            return new decimal((int)(value._magnitude & 0xFFFFFFFF), (int)(value._d >> 32), (int)(value._c & 0xFFFFFFFF), value.Sign < 0, 0);*/
        }

        /// <summary>
        ///     Performs an explicit conversion from <see cref="BigInteger" /> to <see cref="System.Double" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator double(BigInteger value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            var culture = CultureInfo.InvariantCulture;
            if (!double.TryParse(value.ToString(null, culture), NumberStyles.Number, culture, out var d))
            {
                throw new OverflowException();
            }

            return d;
        }

        /// <summary>
        ///     Performs an explicit conversion from <see cref="BigInteger" /> to <see cref="System.Single" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator float(BigInteger value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            var culture = CultureInfo.InvariantCulture;
            if (!float.TryParse(value.ToString(null, culture), NumberStyles.Number, culture, out var f))
            {
                throw new OverflowException();
            }

            return f;
        }

        /// <summary>
        ///     Performs an explicit conversion from <see cref="BigInteger" /> to <see cref="System.Int16" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator short(BigInteger value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if (value < short.MinValue || value > short.MaxValue)
            {
                throw new OverflowException();
            }

            return (short) value.IntValue;
        }

        /// <summary>
        ///     Performs an explicit conversion from <see cref="BigInteger" /> to <see cref="System.UInt16" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator ushort(BigInteger value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if (value < ushort.MinValue || value > ushort.MaxValue)
            {
                throw new OverflowException();
            }

            return (ushort) value.IntValue;
        }

        /// <summary>
        ///     Performs an explicit conversion from <see cref="BigInteger" /> to <see cref="System.Int32" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator int(BigInteger value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if (value < int.MinValue || value > int.MaxValue)
            {
                throw new OverflowException();
            }

            return value.IntValue;
        }

        /// <summary>
        ///     Performs an explicit conversion from <see cref="BigInteger" /> to <see cref="System.UInt32" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator uint(BigInteger value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if (value < uint.MinValue || value > uint.MaxValue)
            {
                throw new OverflowException();
            }

            return (uint) value.IntValue;
        }

        /// <summary>
        ///     Performs an explicit conversion from <see cref="BigInteger" /> to <see cref="System.Int64" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator long(BigInteger value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if (value < long.MinValue || value > long.MaxValue)
            {
                throw new OverflowException();
            }

            return value.LongValue;
        }

        /// <summary>
        ///     Performs an explicit conversion from <see cref="BigInteger" /> to <see cref="System.UInt64" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator ulong(BigInteger value)
        {
            if (value < ushort.MinValue || value > ushort.MaxValue)
            {
                throw new OverflowException();
            }

            return (ulong) value.LongValue;
        }

        /// <summary>
        ///     Performs an explicit conversion from <see cref="BigInteger" /> to <see cref="Int128" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator Int128(BigInteger value)
        {
            if (value < Int128.MinValue || value > Int128.MaxValue)
            {
                throw new OverflowException();
            }

            return value.ToByteArray().ToInt128(asLittleEndian: false);
        }

        /// <summary>
        ///     Performs an explicit conversion from <see cref="BigInteger" /> to <see cref="Int256" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static explicit operator Int256(BigInteger value)
        {
            if (value < Int256.MinValue || value > Int256.MaxValue)
            {
                throw new OverflowException();
            }

            return value.ToByteArray().ToInt256(asLittleEndian: false);
        }

        /// <summary>
        ///     Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The x.</param>
        /// <param name="right">The y.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator >(BigInteger left, BigInteger right)
        {
            return Compare(left, right) > 0;
        }

        /// <summary>
        ///     Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The x.</param>
        /// <param name="right">The y.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator <(BigInteger left, BigInteger right)
        {
            return Compare(left, right) < 0;
        }

        /// <summary>
        ///     Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The x.</param>
        /// <param name="right">The y.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator >=(BigInteger left, BigInteger right)
        {
            return Compare(left, right) >= 0;
        }

        /// <summary>
        ///     Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The x.</param>
        /// <param name="right">The y.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator <=(BigInteger left, BigInteger right)
        {
            return Compare(left, right) <= 0;
        }

        /// <summary>
        ///     Implements the operator !=.
        /// </summary>
        /// <param name="left">The x.</param>
        /// <param name="right">The y.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator !=(BigInteger left, BigInteger right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///     Implements the operator ==.
        /// </summary>
        /// <param name="left">The x.</param>
        /// <param name="right">The y.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator ==(BigInteger left, BigInteger right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///     Implements the operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static BigInteger operator +(BigInteger value)
        {
            return value;
        }

        /// <summary>
        ///     Implements the operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static BigInteger operator -(BigInteger value)
        {
            return value.Negate();
        }

        /// <summary>
        ///     Implements the operator +.
        /// </summary>
        /// <param name="left">The x.</param>
        /// <param name="right">The y.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static BigInteger operator +(BigInteger left, BigInteger right)
        {
            return left.Add(right);
        }

        /// <summary>
        ///     Implements the operator -.
        /// </summary>
        /// <param name="left">The x.</param>
        /// <param name="right">The y.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static BigInteger operator -(BigInteger left, BigInteger right)
        {
            return left + -right;
        }

        /// <summary>
        ///     Implements the operator %.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static BigInteger operator %(BigInteger dividend, BigInteger divisor)
        {
            return dividend.Remainder(divisor);
        }

        /// <summary>
        ///     Implements the operator /.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static BigInteger operator /(BigInteger dividend, BigInteger divisor)
        {
            return dividend.Divide(divisor);
        }

        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="left">The x.</param>
        /// <param name="right">The y.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static BigInteger operator *(BigInteger left, BigInteger right)
        {
            return left.Multiply(right);
        }

        /// <summary>
        ///     Implements the operator &gt;&gt;.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="shift">The shift.</param>
        /// <returns>The result of the operator.</returns>
        public static BigInteger operator >>(BigInteger value, int shift)
        {
            return shift == 0 ? value : value.ShiftRight(shift);
        }

        /// <summary>
        ///     Implements the operator &lt;&lt;.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="shift">The shift.</param>
        /// <returns>The result of the operator.</returns>
        public static BigInteger operator <<(BigInteger value, int shift)
        {
            return shift == 0 ? value : value.ShiftLeft(shift);
        }

        /// <summary>
        ///     Implements the operator |.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static BigInteger operator |(BigInteger left, BigInteger right)
        {
            if (left == 0)
            {
                return right;
            }

            return right == 0 ? left : left.Or(right);
        }

        /// <summary>
        ///     Implements the operator &amp;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static BigInteger operator &(BigInteger left, BigInteger right)
        {
            if (left == 0 || right == 0)
            {
                return Zero;
            }

            return left.And(right);
        }

        /// <summary>
        ///     Implements the operator ~.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static BigInteger operator ~(BigInteger value)
        {
            return value.Not();
        }

        /// <summary>
        ///     Implements the operator ++.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static BigInteger operator ++(BigInteger value)
        {
            return value + 1;
        }

        /// <summary>
        ///     Implements the operator --.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static BigInteger operator --(BigInteger value)
        {
            return value - 1;
        }

        #endregion

        private static int GetByteLength(int nBits)
        {
            return (nBits + BitsPerByte - 1) / BitsPerByte;
        }

        private static int[] MakeMagnitude(byte[] bytes, int offset, int length)
        {
            var end = offset + length;

            // strip leading zeros
            int firstSignificant;
            for (firstSignificant = offset; firstSignificant < end && bytes[firstSignificant] == 0; firstSignificant++)
            {
            }

            if (firstSignificant >= end)
            {
                return ZeroMagnitude;
            }

            var nInts = (end - firstSignificant + 3) / BytesPerInt;
            var bCount = (end - firstSignificant) % BytesPerInt;
            if (bCount == 0)
            {
                bCount = BytesPerInt;
            }

            if (nInts < 1)
            {
                return ZeroMagnitude;
            }

            var mag = new int[nInts];

            var v = 0;
            var magnitudeIndex = 0;
            for (var i = firstSignificant; i < end; ++i)
            {
                v <<= 8;
                v |= bytes[i] & 0xff;
                bCount--;
                if (bCount <= 0)
                {
                    mag[magnitudeIndex] = v;
                    magnitudeIndex++;
                    bCount = BytesPerInt;
                    v = 0;
                }
            }

            if (magnitudeIndex < mag.Length)
            {
                mag[magnitudeIndex] = v;
            }

            return mag;
        }

        public BigInteger Abs()
        {
            return _sign >= 0 ? this : Negate();
        }

        /**
         * return a = a + b - b preserved.
         */

        private static int[] AddMagnitudes(int[] a, int[] b)
        {
            var tI = a.Length - 1;
            var vI = b.Length - 1;
            long m = 0;

            while (vI >= 0)
            {
                m += (uint) a[tI] + (long) (uint) b[vI--];
                a[tI--] = (int) m;
                m = (long) ((ulong) m >> 32);
            }

            if (m != 0)
            {
                while (tI >= 0 && ++a[tI--] == 0)
                {
                }
            }

            return a;
        }

        public BigInteger Add(BigInteger value)
        {
            if (_sign == 0)
            {
                return value;
            }

            if (_sign != value._sign)
            {
                if (value._sign == 0)
                {
                    return this;
                }

                return value._sign < 0 ? Subtract(value.Negate()) : value.Subtract(Negate());
            }

            return AddToMagnitude(value._magnitude);
        }

        private BigInteger AddToMagnitude(int[] magToAdd)
        {
            int[] big, small;
            if (_magnitude.Length < magToAdd.Length)
            {
                big = magToAdd;
                small = _magnitude;
            }
            else
            {
                big = _magnitude;
                small = magToAdd;
            }

            // Conservatively avoid over-allocation when no overflow possible
            var limit = uint.MaxValue;
            if (big.Length == small.Length)
            {
                limit -= (uint) small[0];
            }

            var possibleOverflow = (uint) big[0] >= limit;

            int[] bigCopy;
            if (possibleOverflow)
            {
                bigCopy = new int[big.Length + 1];
                big.CopyTo(bigCopy, 1);
            }
            else
            {
                bigCopy = (int[]) big.Clone();
            }

            bigCopy = AddMagnitudes(bigCopy, small);

            return new BigInteger(_sign, bigCopy, possibleOverflow);
        }

        public BigInteger And(BigInteger value)
        {
            if (_sign == 0 || value._sign == 0)
            {
                return Zero;
            }

            var aMag = _sign > 0 ? _magnitude : Add(One)._magnitude;

            var bMag = value._sign > 0 ? value._magnitude : value.Add(One)._magnitude;

            var resultNeg = _sign < 0 && value._sign < 0;
            var resultLength = Math.Max(aMag.Length, bMag.Length);
            var resultMag = new int[resultLength];

            var aStart = resultMag.Length - aMag.Length;
            var bStart = resultMag.Length - bMag.Length;

            for (var i = 0; i < resultMag.Length; ++i)
            {
                var aWord = i >= aStart ? aMag[i - aStart] : 0;
                var bWord = i >= bStart ? bMag[i - bStart] : 0;

                if (_sign < 0)
                {
                    aWord = ~aWord;
                }

                if (value._sign < 0)
                {
                    bWord = ~bWord;
                }

                resultMag[i] = aWord & bWord;

                if (resultNeg)
                {
                    resultMag[i] = ~resultMag[i];
                }
            }

            var result = new BigInteger(1, resultMag, true);

            // TODO Optimise this case
            if (resultNeg)
            {
                result = result.Not();
            }

            return result;
        }

        public BigInteger AndNot(BigInteger val)
        {
            return And(val.Not());
        }

        private int CalcBitLength(int indx, int[] mag)
        {
            for (;;)
            {
                if (indx >= mag.Length)
                {
                    return 0;
                }

                if (mag[indx] != 0)
                {
                    break;
                }

                ++indx;
            }

            // bit length for everything after the first int
            var bitLength = 32 * (mag.Length - indx - 1);

            // and determine bitlength of first int
            var firstMag = mag[indx];
            bitLength += BitLen(firstMag);

            // Check for negative powers of two
            if (_sign < 0 && (firstMag & -firstMag) == firstMag)
            {
                do
                {
                    if (++indx >= mag.Length)
                    {
                        --bitLength;
                        break;
                    }
                } while (mag[indx] == 0);
            }

            return bitLength;
        }

        //
        // BitLen(value) is the number of bits in value.
        //
        private static int BitLen(int w)
        {
            // Binary search - decision tree (5 tests, rarely 6)
            return w < 1 << 15
                ? (w < 1 << 7
                    ? (w < 1 << 3
                        ? (w < 1 << 1 ? (w < 1 << 0 ? (w < 0 ? 32 : 0) : 1) : (w < 1 << 2 ? 2 : 3))
                        : (w < 1 << 5 ? (w < 1 << 4 ? 4 : 5) : (w < 1 << 6 ? 6 : 7)))
                    : (w < 1 << 11
                        ? (w < 1 << 9 ? (w < 1 << 8 ? 8 : 9) : (w < 1 << 10 ? 10 : 11))
                        : (w < 1 << 13 ? (w < 1 << 12 ? 12 : 13) : (w < 1 << 14 ? 14 : 15))))
                : (w < 1 << 23
                    ? (w < 1 << 19
                        ? (w < 1 << 17 ? (w < 1 << 16 ? 16 : 17) : (w < 1 << 18 ? 18 : 19))
                        : (w < 1 << 21 ? (w < 1 << 20 ? 20 : 21) : (w < 1 << 22 ? 22 : 23)))
                    : (w < 1 << 27
                        ? (w < 1 << 25 ? (w < 1 << 24 ? 24 : 25) : (w < 1 << 26 ? 26 : 27))
                        : (w < 1 << 29 ? (w < 1 << 28 ? 28 : 29) : (w < 1 << 30 ? 30 : 31))));
        }

        //		private readonly static byte[] bitLengths =
        //		{
        //			0, 1, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4,
        //			5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
        //			6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
        //			7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
        //			7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 8, 8, 8, 8,
        //			8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
        //			8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
        //			8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
        //			8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
        //			8, 8, 8, 8, 8, 8, 8, 8
        //		};

        private bool QuickPow2Check()
        {
            return _sign > 0 && _nBits == 1;
        }

        /**
         * unsigned comparison on two arrays - note the arrays may
         * start with leading zeros.
         */

        private static int CompareTo(int xIndx, int[] x, int yIndx, int[] y)
        {
            while (xIndx != x.Length && x[xIndx] == 0)
            {
                xIndx++;
            }

            while (yIndx != y.Length && y[yIndx] == 0)
            {
                yIndx++;
            }

            return CompareNoLeadingZeroes(xIndx, x, yIndx, y);
        }

        private static int CompareNoLeadingZeroes(int xIndx, int[] x, int yIndx, int[] y)
        {
            var diff = x.Length - y.Length - (xIndx - yIndx);

            if (diff != 0)
            {
                return diff < 0 ? -1 : 1;
            }

            // lengths of magnitudes the same, test the magnitude values

            while (xIndx < x.Length)
            {
                var v1 = (uint) x[xIndx++];
                var v2 = (uint) y[yIndx++];

                if (v1 != v2)
                {
                    return v1 < v2 ? -1 : 1;
                }
            }

            return 0;
        }

        /**
         * return z = x / y - done in place (z value preserved, x contains the
         * remainder)
         */

        private int[] Divide(int[] x, int[] y)
        {
            var xStart = 0;
            while (xStart < x.Length && x[xStart] == 0)
            {
                ++xStart;
            }

            var yStart = 0;
            while (yStart < y.Length && y[yStart] == 0)
            {
                ++yStart;
            }

            Debug.Assert(yStart < y.Length);

            var xyCmp = CompareNoLeadingZeroes(xStart, x, yStart, y);
            int[] count;

            if (xyCmp > 0)
            {
                var yBitLength = CalcBitLength(yStart, y);
                var xBitLength = CalcBitLength(xStart, x);
                var shift = xBitLength - yBitLength;

                int[] iCount;
                var iCountStart = 0;

                int[] c;
                var cStart = 0;
                var cBitLength = yBitLength;
                if (shift > 0)
                {
                    //					iCount = ShiftLeft(One.magnitude, shift);
                    iCount = new int[(shift >> 5) + 1];
                    iCount[0] = 1 << (shift % 32);

                    c = ShiftLeft(y, shift);
                    cBitLength += shift;
                }
                else
                {
                    iCount = new[] {1};

                    var len = y.Length - yStart;
                    c = new int[len];
                    Array.Copy(y, yStart, c, 0, len);
                }

                count = new int[iCount.Length];

                for (;;)
                {
                    if (cBitLength < xBitLength || CompareNoLeadingZeroes(xStart, x, cStart, c) >= 0)
                    {
                        Subtract(xStart, x, cStart, c);
                        AddMagnitudes(count, iCount);

                        while (x[xStart] == 0)
                        {
                            if (++xStart == x.Length)
                            {
                                return count;
                            }
                        }

                        //xBitLength = calcBitLength(xStart, x);
                        xBitLength = 32 * (x.Length - xStart - 1) + BitLen(x[xStart]);

                        if (xBitLength <= yBitLength)
                        {
                            if (xBitLength < yBitLength)
                            {
                                return count;
                            }

                            xyCmp = CompareNoLeadingZeroes(xStart, x, yStart, y);

                            if (xyCmp <= 0)
                            {
                                break;
                            }
                        }
                    }

                    shift = cBitLength - xBitLength;

                    // NB: The case where c[cStart] is 1-bit is harmless
                    if (shift == 1)
                    {
                        var firstC = (uint) c[cStart] >> 1;
                        var firstX = (uint) x[xStart];
                        if (firstC > firstX)
                        {
                            ++shift;
                        }
                    }

                    if (shift < 2)
                    {
                        ShiftRightOneInPlace(cStart, c);
                        --cBitLength;
                        ShiftRightOneInPlace(iCountStart, iCount);
                    }
                    else
                    {
                        ShiftRightInPlace(cStart, c, shift);
                        cBitLength -= shift;
                        ShiftRightInPlace(iCountStart, iCount, shift);
                    }

                    //cStart = c.Length - ((cBitLength + 31) / 32);
                    while (c[cStart] == 0)
                    {
                        ++cStart;
                    }

                    while (iCount[iCountStart] == 0)
                    {
                        ++iCountStart;
                    }
                }
            }
            else
            {
                count = new int[1];
            }

            if (xyCmp == 0)
            {
                AddMagnitudes(count, One._magnitude);
                Array.Clear(x, xStart, x.Length - xStart);
            }

            return count;
        }

        public BigInteger Divide(BigInteger val)
        {
            if (val._sign == 0)
            {
                throw new ArithmeticException("Division by zero error");
            }

            if (_sign == 0)
            {
                return Zero;
            }

            if (val.QuickPow2Check()) // val is power of two
            {
                var result = Abs().ShiftRight(val.Abs().BitLength - 1);
                return val._sign == _sign ? result : result.Negate();
            }

            var mag = (int[]) _magnitude.Clone();

            return new BigInteger(_sign * val._sign, Divide(mag, val._magnitude), true);
        }

        public BigInteger[] DivideAndRemainder(BigInteger val)
        {
            if (val._sign == 0)
            {
                throw new ArithmeticException("Division by zero error");
            }

            var biggies = new BigInteger[2];

            if (_sign == 0)
            {
                biggies[0] = Zero;
                biggies[1] = Zero;
            }
            else if (val.QuickPow2Check()) // val is power of two
            {
                var e = val.Abs().BitLength - 1;
                var quotient = Abs().ShiftRight(e);
                var remainder = LastNBits(e);

                biggies[0] = val._sign == _sign ? quotient : quotient.Negate();
                biggies[1] = new BigInteger(_sign, remainder, true);
            }
            else
            {
                var remainder = (int[]) _magnitude.Clone();
                var quotient = Divide(remainder, val._magnitude);

                biggies[0] = new BigInteger(_sign * val._sign, quotient, true);
                biggies[1] = new BigInteger(_sign, remainder, true);
            }

            return biggies;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.GetType() == GetType() && Equals((BigInteger) obj);
        }

        public BigInteger Gcd(BigInteger value)
        {
            if (value._sign == 0)
            {
                return Abs();
            }

            if (_sign == 0)
            {
                return value.Abs();
            }

            var u = this;
            var v = value;

            while (v._sign != 0)
            {
                var r = u.Mod(v);
                u = v;
                v = r;
            }

            return u;
        }

        public override int GetHashCode()
        {
            // ReSharper disable NonReadonlyMemberInGetHashCode
            var hc = _magnitude.Length;
            if (_magnitude.Length > 0)
            {
                hc ^= _magnitude[0];

                if (_magnitude.Length > 1)
                {
                    hc ^= _magnitude[_magnitude.Length - 1];
                }
            }

            return _sign < 0 ? ~hc : hc;
        }

        // TODO Make public?
        private BigInteger Inc()
        {
            if (_sign == 0)
            {
                return One;
            }

            return _sign < 0
                ? new BigInteger(-1, DoSubBigLil(_magnitude, One._magnitude), true)
                : AddToMagnitude(One._magnitude);
        }

        /**
         * return whether or not a BigInteger is probably prime with a
         * probability of 1 - (1/2)**certainty.
         * <p>From Knuth Vol 2, pg 395.</p>
         */

        public bool IsProbablePrime(int certainty)
        {
            if (certainty <= 0)
            {
                return true;
            }

            var n = Abs();

            if (!n.TestBit(0))
            {
                return n.Equals(Two);
            }

            return !n.Equals(One) && n.CheckProbablePrime(certainty, RandomSource);
        }

        private bool CheckProbablePrime(int certainty, Random random)
        {
            Debug.Assert(certainty > 0);
            Debug.Assert(CompareTo(Two) > 0);
            Debug.Assert(TestBit(0));


            // Try to reduce the penalty for really small numbers
            var numLists = Math.Min(BitLength - 1, PrimeLists.Length);

            for (var i = 0; i < numLists; ++i)
            {
                var test = Remainder(PrimeProducts[i]);

                var primeList = PrimeLists[i];
                foreach (var prime in primeList)
                {
                    var qRem = test % prime;
                    if (qRem == 0)
                    {
                        // We may find small numbers in the list
                        return BitLength < 16 && IntValue == prime;
                    }
                }
            }


            // TODO Special case for < 10^16 (RabinMiller fixed list)
            //			if (BitLength < 30)
            //			{
            //				RabinMiller against 2, 3, 5, 7, 11, 13, 23 is sufficient
            //			}


            // TODO Is it worth trying to create a hybrid of these two?
            return RabinMillerTest(certainty, random);
            //			return SolovayStrassenTest(certainty, random);

            //			bool rbTest = RabinMillerTest(certainty, random);
            //			bool ssTest = SolovayStrassenTest(certainty, random);
            //
            //			Debug.Assert(rbTest == ssTest);
            //
            //			return rbTest;
        }

        public bool RabinMillerTest(int certainty, Random random)
        {
            Debug.Assert(certainty > 0);
            Debug.Assert(BitLength > 2);
            Debug.Assert(TestBit(0));

            // let n = 1 + d . 2^s
            var n = this;
            var nMinusOne = n.Subtract(One);
            var s = nMinusOne.GetLowestSetBit();
            var r = nMinusOne.ShiftRight(s);

            Debug.Assert(s >= 1);

            do
            {
                // TODO Make a method for random BigIntegers in range 0 < x < n)
                // - Method can be optimized by only replacing examined bits at each trial
                BigInteger a;
                do
                {
                    a = new BigInteger(n.BitLength, random);
                } while (a.CompareTo(One) <= 0 || a.CompareTo(nMinusOne) >= 0);

                var y = a.ModPow(r, n);

                if (!y.Equals(One))
                {
                    var j = 0;
                    while (!y.Equals(nMinusOne))
                    {
                        if (++j == s)
                        {
                            return false;
                        }

                        y = y.ModPow(Two, n);

                        if (y.Equals(One))
                        {
                            return false;
                        }
                    }
                }

                certainty -= 2; // composites pass for only 1/4 possible 'a'
            } while (certainty > 0);

            return true;
        }

        //		private bool SolovayStrassenTest(
        //			int		certainty,
        //			Random	random)
        //		{
        //			Debug.Assert(certainty > 0);
        //			Debug.Assert(CompareTo(Two) > 0);
        //			Debug.Assert(TestBit(0));
        //
        //			BigInteger n = this;
        //			BigInteger nMinusOne = n.Subtract(One);
        //			BigInteger e = nMinusOne.ShiftRight(1);
        //
        //			do
        //			{
        //				BigInteger a;
        //				do
        //				{
        //					a = new BigInteger(nBitLength, random);
        //				}
        //				// NB: Spec says 0 < x < n, but 1 is trivial
        //				while (a.CompareTo(One) <= 0 || a.CompareTo(n) >= 0);
        //
        //
        //				// TODO Check this is redundant given the way Jacobi() works?
        ////				if (!a.Gcd(n).Equals(One))
        ////					return false;
        //
        //				int x = Jacobi(a, n);
        //
        //				if (x == 0)
        //					return false;
        //
        //				BigInteger check = a.ModPow(e, n);
        //
        //				if (x == 1 && !check.Equals(One))
        //					return false;
        //
        //				if (x == -1 && !check.Equals(nMinusOne))
        //					return false;
        //
        //				--certainty;
        //			}
        //			while (certainty > 0);
        //
        //			return true;
        //		}
        //
        //		private static int Jacobi(
        //			BigInteger	a,
        //			BigInteger	b)
        //		{
        //			Debug.Assert(a.sign >= 0);
        //			Debug.Assert(b.sign > 0);
        //			Debug.Assert(b.TestBit(0));
        //			Debug.Assert(a.CompareTo(b) < 0);
        //
        //			int totalS = 1;
        //			for (;;)
        //			{
        //				if (a.sign == 0)
        //					return 0;
        //
        //				if (a.Equals(One))
        //					break;
        //
        //				int e = a.GetLowestSetBit();
        //
        //				int bLsw = b.magnitude[b.magnitude.Length - 1];
        //				if ((e & 1) != 0 && ((bLsw & 7) == 3 || (bLsw & 7) == 5))
        //					totalS = -totalS;
        //
        //				// TODO Confirm this is faster than later a1.Equals(One) test
        //				if (a.BitLength == e + 1)
        //					break;
        //				BigInteger a1 = a.ShiftRight(e);
        ////				if (a1.Equals(One))
        ////					break;
        //
        //				int a1Lsw = a1.magnitude[a1.magnitude.Length - 1];
        //				if ((bLsw & 3) == 3 && (a1Lsw & 3) == 3)
        //					totalS = -totalS;
        //
        ////				a = b.Mod(a1);
        //				a = b.Remainder(a1);
        //				b = a1;
        //			}
        //			return totalS;
        //		}

        public BigInteger Max(BigInteger value)
        {
            return CompareTo(value) > 0 ? this : value;
        }

        public BigInteger Min(BigInteger value)
        {
            return CompareTo(value) < 0 ? this : value;
        }

        public BigInteger Mod(BigInteger m)
        {
            if (m._sign < 1)
            {
                throw new ArithmeticException("Modulus must be positive");
            }

            var biggie = Remainder(m);

            return biggie._sign >= 0 ? biggie : biggie.Add(m);
        }

        public BigInteger ModInverse(BigInteger m)
        {
            if (m._sign < 1)
            {
                throw new ArithmeticException("Modulus must be positive");
            }

            // TODO Too slow at the moment
            //			// "Fast Key Exchange with Elliptic Curve Systems" R.Schoeppel
            //			if (m.TestBit(0))
            //			{
            //				//The Almost Inverse Algorithm
            //				int k = 0;
            //				BigInteger B = One, C = Zero, F = this, G = m, tmp;
            //
            //				for (;;)
            //				{
            //					// While F is even, do F=F/u, C=C*u, k=k+1.
            //					int zeroes = F.GetLowestSetBit();
            //					if (zeroes > 0)
            //					{
            //						F = F.ShiftRight(zeroes);
            //						C = C.ShiftLeft(zeroes);
            //						k += zeroes;
            //					}
            //
            //					// If F = 1, then return B,k.
            //					if (F.Equals(One))
            //					{
            //						BigInteger half = m.Add(One).ShiftRight(1);
            //						BigInteger halfK = half.ModPow(BigInteger.ValueOf(k), m);
            //						return B.Multiply(halfK).Mod(m);
            //					}
            //
            //					if (F.CompareTo(G) < 0)
            //					{
            //						tmp = G; G = F; F = tmp;
            //						tmp = B; B = C; C = tmp;
            //					}
            //
            //					F = F.Add(G);
            //					B = B.Add(C);
            //				}
            //			}

            var x = new BigInteger();
            var gcd = ExtEuclid(Mod(m), m, x, null);

            if (!gcd.Equals(One))
            {
                throw new ArithmeticException("Numbers not relatively prime.");
            }

            if (x._sign < 0)
            {
                x._sign = 1;
                //x = m.Subtract(x);
                x._magnitude = DoSubBigLil(m._magnitude, x._magnitude);
            }

            return x;
        }

        /**
         * Calculate the numbers u1, u2, and u3 such that:
         *
         * u1 * a + u2 * b = u3
         *
         * where u3 is the greatest common divider of a and b.
         * a and b using the extended Euclid algorithm (refer p. 323
         * of The Art of Computer Programming vol 2, 2nd ed).
         * This also seems to have the side effect of calculating
         * some form of multiplicative inverse.
         *
         * @param a    First number to calculate gcd for
         * @param b    Second number to calculate gcd for
         * @param u1Out      the return object for the u1 value
         * @param u2Out      the return object for the u2 value
         * @return     The greatest common divisor of a and b
         */

        private static BigInteger ExtEuclid(BigInteger a, BigInteger b, BigInteger u1Out, BigInteger u2Out)
        {
            var u1 = One;
            var u3 = a;
            var v1 = Zero;
            var v3 = b;

            while (v3._sign > 0)
            {
                var q = u3.DivideAndRemainder(v3);

                var tmp = v1.Multiply(q[0]);
                var tn = u1.Subtract(tmp);
                u1 = v1;
                v1 = tn;

                u3 = v3;
                v3 = q[1];
            }

            if (u1Out != null)
            {
                u1Out._sign = u1._sign;
                u1Out._magnitude = u1._magnitude;
            }

            if (u2Out != null)
            {
                var tmp = u1.Multiply(a);
                tmp = u3.Subtract(tmp);
                var res = tmp.Divide(b);
                u2Out._sign = res._sign;
                u2Out._magnitude = res._magnitude;
            }

            return u3;
        }

        private static void ZeroOut(int[] x)
        {
            Array.Clear(x, 0, x.Length);
        }

        public BigInteger ModPow(BigInteger exponent, BigInteger m)
        {
            if (m._sign < 1)
            {
                throw new ArithmeticException("Modulus must be positive");
            }

            if (m.Equals(One))
            {
                return Zero;
            }

            if (exponent._sign == 0)
            {
                return One;
            }

            if (_sign == 0)
            {
                return Zero;
            }

            int[] zVal = null;
            int[] yAccum = null;

            // Montgomery exponentiation is only possible if the modulus is odd,
            // but AFAIK, this is always the case for crypto algo's
            var useMonty = (m._magnitude[m._magnitude.Length - 1] & 1) == 1;
            long mQ = 0;
            if (useMonty)
            {
                mQ = m.GetMQuote();

                // tmp = this * R mod m
                var tmp = ShiftLeft(32 * m._magnitude.Length).Mod(m);
                zVal = tmp._magnitude;

                useMonty = zVal.Length <= m._magnitude.Length;

                if (useMonty)
                {
                    yAccum = new int[m._magnitude.Length + 1];
                    if (zVal.Length < m._magnitude.Length)
                    {
                        var longZ = new int[m._magnitude.Length];
                        zVal.CopyTo(longZ, longZ.Length - zVal.Length);
                        zVal = longZ;
                    }
                }
            }

            if (!useMonty)
            {
                if (_magnitude.Length <= m._magnitude.Length)
                {
                    //zAccum = new int[m.magnitude.Length * 2];
                    zVal = new int[m._magnitude.Length];
                    _magnitude.CopyTo(zVal, zVal.Length - _magnitude.Length);
                }
                else
                {
                    //
                    // in normal practice we'll never see this...
                    //
                    var tmp = Remainder(m);

                    //zAccum = new int[m.magnitude.Length * 2];
                    zVal = new int[m._magnitude.Length];
                    tmp._magnitude.CopyTo(zVal, zVal.Length - tmp._magnitude.Length);
                }

                yAccum = new int[m._magnitude.Length * 2];
            }

            var yVal = new int[m._magnitude.Length];

            //
            // from LSW to MSW
            //
            for (var i = 0; i < exponent._magnitude.Length; i++)
            {
                var v = exponent._magnitude[i];
                var bits = 0;

                if (i == 0)
                {
                    while (v > 0)
                    {
                        v <<= 1;
                        bits++;
                    }

                    //
                    // first time in initialise y
                    //
                    zVal.CopyTo(yVal, 0);

                    v <<= 1;
                    bits++;
                }

                while (v != 0)
                {
                    if (useMonty)
                    {
                        // Montgomery square algo doesn't exist, and a normal
                        // square followed by a Montgomery reduction proved to
                        // be almost as heavy as a Montgomery mulitply.
                        MultiplyMonty(yAccum, yVal, yVal, m._magnitude, mQ);
                    }
                    else
                    {
                        Square(yAccum, yVal);
                        Remainder(yAccum, m._magnitude);
                        Array.Copy(yAccum, yAccum.Length - yVal.Length, yVal, 0, yVal.Length);
                        ZeroOut(yAccum);
                    }
                    bits++;

                    if (v < 0)
                    {
                        if (useMonty)
                        {
                            MultiplyMonty(yAccum, yVal, zVal, m._magnitude, mQ);
                        }
                        else
                        {
                            Multiply(yAccum, yVal, zVal);
                            Remainder(yAccum, m._magnitude);
                            Array.Copy(yAccum, yAccum.Length - yVal.Length, yVal, 0, yVal.Length);
                            ZeroOut(yAccum);
                        }
                    }

                    v <<= 1;
                }

                while (bits < 32)
                {
                    if (useMonty)
                    {
                        MultiplyMonty(yAccum, yVal, yVal, m._magnitude, mQ);
                    }
                    else
                    {
                        Square(yAccum, yVal);
                        Remainder(yAccum, m._magnitude);
                        Array.Copy(yAccum, yAccum.Length - yVal.Length, yVal, 0, yVal.Length);
                        ZeroOut(yAccum);
                    }
                    bits++;
                }
            }

            if (useMonty)
            {
                // Return y * R^(-1) mod m by doing y * 1 * R^(-1) mod m
                ZeroOut(zVal);
                zVal[zVal.Length - 1] = 1;
                MultiplyMonty(yAccum, yVal, zVal, m._magnitude, mQ);
            }

            var result = new BigInteger(1, yVal, true);

            return exponent._sign > 0 ? result : result.ModInverse(m);
        }

        /**
         * return w with w = x * x - w is assumed to have enough space.
         */

        private static int[] Square(int[] w, int[] x)
        {
            // Note: this method allows w to be only (2 * x.Length - 1) words if result will fit
            //			if (w.Length != 2 * x.Length)
            //				throw new ArgumentException("no I don't think so...");

            ulong u1, u2;

            var wBase = w.Length - 1;

            for (var i = x.Length - 1; i != 0; i--)
            {
                ulong v = (uint) x[i];

                u1 = v * v;
                u2 = u1 >> 32;
                u1 = (uint) u1;

                u1 += (uint) w[wBase];

                w[wBase] = (int) (uint) u1;
                var c = u2 + (u1 >> 32);

                for (var j = i - 1; j >= 0; j--)
                {
                    --wBase;
                    u1 = v * (uint) x[j];
                    u2 = u1 >> 31; // multiply by 2!
                    u1 = (uint) (u1 << 1); // multiply by 2!
                    u1 += c + (uint) w[wBase];

                    w[wBase] = (int) (uint) u1;
                    c = u2 + (u1 >> 32);
                }

                c += (uint) w[--wBase];
                w[wBase] = (int) (uint) c;

                if (--wBase >= 0)
                {
                    w[wBase] = (int) (uint) (c >> 32);
                }
                else
                {
                    Debug.Assert((uint) (c >> 32) == 0);
                }
                wBase += i;
            }

            u1 = (uint) x[0];
            u1 = u1 * u1;
            u2 = u1 >> 32;
            u1 = u1 & Mask;

            u1 += (uint) w[wBase];

            w[wBase] = (int) (uint) u1;
            if (--wBase >= 0)
            {
                w[wBase] = (int) (uint) (u2 + (u1 >> 32) + (uint) w[wBase]);
            }
            else
            {
                Debug.Assert((uint) (u2 + (u1 >> 32)) == 0);
            }

            return w;
        }

        /**
         * return x with x = y * z - x is assumed to have enough space.
         */

        private static int[] Multiply(int[] x, int[] y, int[] z)
        {
            var i = z.Length;

            if (i < 1)
            {
                return x;
            }

            var xBase = x.Length - y.Length;

            for (;;)
            {
                var a = z[--i] & Mask;
                long val = 0;

                for (var j = y.Length - 1; j >= 0; j--)
                {
                    val += a * (y[j] & Mask) + (x[xBase + j] & Mask);

                    x[xBase + j] = (int) val;

                    val = (long) ((ulong) val >> 32);
                }

                --xBase;

                if (i < 1)
                {
                    if (xBase >= 0)
                    {
                        x[xBase] = (int) val;
                    }
                    else
                    {
                        Debug.Assert(val == 0);
                    }
                    break;
                }

                x[xBase] = (int) val;
            }

            return x;
        }

        private static long FastExtEuclid(long a, long b, long[] uOut)
        {
            long u1 = 1;
            var u3 = a;
            long v1 = 0;
            var v3 = b;

            while (v3 > 0)
            {
                var q = u3 / v3;

                var tn = u1 - v1 * q;
                u1 = v1;
                v1 = tn;

                tn = u3 - v3 * q;
                u3 = v3;
                v3 = tn;
            }

            uOut[0] = u1;
            uOut[1] = (u3 - u1 * a) / b;

            return u3;
        }

        private static long FastModInverse(long v, long m)
        {
            if (m < 1)
            {
                throw new ArithmeticException("Modulus must be positive");
            }

            var x = new long[2];
            var gcd = FastExtEuclid(v, m, x);

            if (gcd != 1)
            {
                throw new ArithmeticException("Numbers not relatively prime.");
            }

            if (x[0] < 0)
            {
                x[0] += m;
            }

            return x[0];
        }

        //		private static BigInteger MQuoteB = One.ShiftLeft(32);
        //		private static BigInteger MQuoteBSub1 = MQuoteB.Subtract(One);

        /**
         * Calculate mQuote = -m^(-1) mod b with b = 2^32 (32 = word size)
         */

        private long GetMQuote()
        {
            Debug.Assert(_sign > 0);

            if (_mQuote != -1)
            {
                return _mQuote; // already calculated
            }

            if (_magnitude.Length == 0 || (_magnitude[_magnitude.Length - 1] & 1) == 0)
            {
                return -1; // not for even numbers
            }

            var v = (~_magnitude[_magnitude.Length - 1] | 1) & 0xffffffffL;
            _mQuote = FastModInverse(v, 0x100000000L);

            return _mQuote;
        }

        /**
         * Montgomery multiplication: a = x * y * R^(-1) mod m
         * <br/>
         * Based algorithm 14.36 of Handbook of Applied Cryptography.
         * <br/>
         * <li> m, x, y should have length n </li>
         * <li> a should have length (n + 1) </li>
         * <li> b = 2^32, R = b^n </li>
         * <br/>
         * The result is put in x
         * <br/>
         * NOTE: the indices of x, y, m, a different in HAC and in Java
         */

        private static void MultiplyMonty(int[] a, int[] x, int[] y, int[] m, long mQuote) // mQuote = -m^(-1) mod b
        {
            if (m.Length == 1)
            {
                x[0] = (int) MultiplyMontyNIsOne((uint) x[0], (uint) y[0], (uint) m[0], (ulong) mQuote);
                return;
            }

            var n = m.Length;
            var nMinus1 = n - 1;
            var y0 = y[nMinus1] & Mask;

            // 1. a = 0 (Notation: a = (a_{n} a_{n-1} ... a_{0})_{b} )
            Array.Clear(a, 0, n + 1);

            // 2. for i from 0 to (n - 1) do the following:
            for (var i = n; i > 0; i--)
            {
                var xI = x[i - 1] & Mask;

                // 2.1 u = ((a[0] + (x[i] * y[0]) * mQuote) mod b
                var u = ((((a[n] & Mask) + ((xI * y0) & Mask)) & Mask) * mQuote) & Mask;

                // 2.2 a = (a + x_i * y + u * m) / b
                var prod1 = xI * y0;
                var prod2 = u * (m[nMinus1] & Mask);
                var tmp = (a[n] & Mask) + (prod1 & Mask) + (prod2 & Mask);
                var carry = (long) ((ulong) prod1 >> 32) + (long) ((ulong) prod2 >> 32) + (long) ((ulong) tmp >> 32);
                for (var j = nMinus1; j > 0; j--)
                {
                    prod1 = xI * (y[j - 1] & Mask);
                    prod2 = u * (m[j - 1] & Mask);
                    tmp = (a[j] & Mask) + (prod1 & Mask) + (prod2 & Mask) + (carry & Mask);
                    carry = (long) ((ulong) carry >> 32) + (long) ((ulong) prod1 >> 32) + (long) ((ulong) prod2 >> 32) +
                            (long) ((ulong) tmp >> 32);
                    a[j + 1] = (int) tmp; // division by b
                }
                carry += a[0] & Mask;
                a[1] = (int) carry;
                a[0] = (int) ((ulong) carry >> 32); // OJO!!!!!
            }

            // 3. if x >= m the x = x - m
            if (CompareTo(0, a, 0, m) >= 0)
            {
                Subtract(0, a, 0, m);
            }

            // put the result in x
            Array.Copy(a, 1, x, 0, n);
        }

        private static uint MultiplyMontyNIsOne(uint x, uint y, uint m, ulong mQuote)
        {
            ulong um = m;
            var prod1 = x * (ulong) y;
            var u = (prod1 * mQuote) & Umask;
            var prod2 = u * um;
            var tmp = (prod1 & Umask) + (prod2 & Umask);
            var carry = (prod1 >> 32) + (prod2 >> 32) + (tmp >> 32);

            if (carry > um)
            {
                carry -= um;
            }

            return (uint) (carry & Umask);
        }

        public BigInteger Multiply(BigInteger val)
        {
            if (_sign == 0 || val._sign == 0)
            {
                return Zero;
            }

            if (val.QuickPow2Check()) // val is power of two
            {
                var result = ShiftLeft(val.Abs().BitLength - 1);
                return val._sign > 0 ? result : result.Negate();
            }

            if (QuickPow2Check()) // this is power of two
            {
                var result = val.ShiftLeft(Abs().BitLength - 1);
                return _sign > 0 ? result : result.Negate();
            }

            var resLength = (BitLength + val.BitLength) / BitsPerInt + 1;
            var res = new int[resLength];

            if (val == this)
            {
                Square(res, _magnitude);
            }
            else
            {
                Multiply(res, _magnitude, val._magnitude);
            }

            return new BigInteger(_sign * val._sign, res, true);
        }

        public BigInteger Negate()
        {
            return _sign == 0 ? this : new BigInteger(-_sign, _magnitude, false);
        }

        public BigInteger NextProbablePrime()
        {
            if (_sign < 0)
            {
                throw new ArithmeticException("Cannot be called on value < 0");
            }

            if (CompareTo(Two) < 0)
            {
                return Two;
            }

            var n = Inc().SetBit(0);

            while (!n.CheckProbablePrime(100, RandomSource))
            {
                n = n.Add(Two);
            }

            return n;
        }

        public BigInteger Not()
        {
            return Inc().Negate();
        }

        public BigInteger Pow(int exp)
        {
            if (exp < 0)
            {
                throw new ArithmeticException("Negative exponent");
            }

            if (exp == 0)
            {
                return One;
            }

            if (_sign == 0 || Equals(One))
            {
                return this;
            }

            var y = One;
            var z = this;

            for (;;)
            {
                if ((exp & 0x1) == 1)
                {
                    y = y.Multiply(z);
                }
                exp >>= 1;
                if (exp == 0)
                {
                    break;
                }
                z = z.Multiply(z);
            }

            return y;
        }

        public static BigInteger ProbablePrime(int bitLength, Random random)
        {
            return new BigInteger(bitLength, 100, random);
        }

        private int Remainder(int m)
        {
            Debug.Assert(m > 0);

            var acc = _magnitude.Select(t => (long) (uint) t)
                .Aggregate<long, long>(0, (current, posVal) => (current << 32 | posVal) % m);

            return (int) acc;
        }

        /**
         * return x = x % y - done in place (y value preserved)
         */

        private int[] Remainder(int[] x, int[] y)
        {
            var xStart = 0;
            while (xStart < x.Length && x[xStart] == 0)
            {
                ++xStart;
            }

            var yStart = 0;
            while (yStart < y.Length && y[yStart] == 0)
            {
                ++yStart;
            }

            Debug.Assert(yStart < y.Length);

            var xyCmp = CompareNoLeadingZeroes(xStart, x, yStart, y);

            if (xyCmp > 0)
            {
                var yBitLength = CalcBitLength(yStart, y);
                var xBitLength = CalcBitLength(xStart, x);
                var shift = xBitLength - yBitLength;

                int[] c;
                var cStart = 0;
                var cBitLength = yBitLength;
                if (shift > 0)
                {
                    c = ShiftLeft(y, shift);
                    cBitLength += shift;
                    Debug.Assert(c[0] != 0);
                }
                else
                {
                    var len = y.Length - yStart;
                    c = new int[len];
                    Array.Copy(y, yStart, c, 0, len);
                }

                for (;;)
                {
                    if (cBitLength < xBitLength || CompareNoLeadingZeroes(xStart, x, cStart, c) >= 0)
                    {
                        Subtract(xStart, x, cStart, c);

                        while (x[xStart] == 0)
                        {
                            if (++xStart == x.Length)
                            {
                                return x;
                            }
                        }

                        //xBitLength = calcBitLength(xStart, x);
                        xBitLength = 32 * (x.Length - xStart - 1) + BitLen(x[xStart]);

                        if (xBitLength <= yBitLength)
                        {
                            if (xBitLength < yBitLength)
                            {
                                return x;
                            }

                            xyCmp = CompareNoLeadingZeroes(xStart, x, yStart, y);

                            if (xyCmp <= 0)
                            {
                                break;
                            }
                        }
                    }

                    shift = cBitLength - xBitLength;

                    // NB: The case where c[cStart] is 1-bit is harmless
                    if (shift == 1)
                    {
                        var firstC = (uint) c[cStart] >> 1;
                        var firstX = (uint) x[xStart];
                        if (firstC > firstX)
                        {
                            ++shift;
                        }
                    }

                    if (shift < 2)
                    {
                        ShiftRightOneInPlace(cStart, c);
                        --cBitLength;
                    }
                    else
                    {
                        ShiftRightInPlace(cStart, c, shift);
                        cBitLength -= shift;
                    }

                    //cStart = c.Length - ((cBitLength + 31) / 32);
                    while (c[cStart] == 0)
                    {
                        ++cStart;
                    }
                }
            }

            if (xyCmp == 0)
            {
                Array.Clear(x, xStart, x.Length - xStart);
            }

            return x;
        }

        public BigInteger Remainder(BigInteger n)
        {
            if (n._sign == 0)
            {
                throw new ArithmeticException("Division by zero error");
            }

            if (_sign == 0)
            {
                return Zero;
            }

            // For small values, use fast remainder method
            if (n._magnitude.Length == 1)
            {
                var val = n._magnitude[0];

                if (val > 0)
                {
                    if (val == 1)
                    {
                        return Zero;
                    }

                    // TODO Make this func work on uint, and handle val == 1?
                    var rem = Remainder(val);

                    return rem == 0 ? Zero : new BigInteger(_sign, new[] {rem}, false);
                }
            }

            if (CompareNoLeadingZeroes(0, _magnitude, 0, n._magnitude) < 0)
            {
                return this;
            }

            int[] result;
            if (n.QuickPow2Check()) // n is power of two
            {
                // TODO Move before small values branch above?
                result = LastNBits(n.Abs().BitLength - 1);
            }
            else
            {
                result = (int[]) _magnitude.Clone();
                result = Remainder(result, n._magnitude);
            }

            return new BigInteger(_sign, result, true);
        }

        private int[] LastNBits(int n)
        {
            if (n < 1)
            {
                return ZeroMagnitude;
            }

            var numWords = (n + BitsPerInt - 1) / BitsPerInt;
            numWords = Math.Min(numWords, _magnitude.Length);
            var result = new int[numWords];

            Array.Copy(_magnitude, _magnitude.Length - numWords, result, 0, numWords);

            var hiBits = n % 32;
            if (hiBits != 0)
            {
                result[0] &= ~(-1 << hiBits);
            }

            return result;
        }

        /**
         * do a left shift - this returns a new array.
         */

        private static int[] ShiftLeft(int[] mag, int n)
        {
            var nInts = (int) ((uint) n >> 5);
            var nBits = n & 0x1f;
            var magLen = mag.Length;
            int[] newMag;

            if (nBits == 0)
            {
                newMag = new int[magLen + nInts];
                mag.CopyTo(newMag, 0);
            }
            else
            {
                var i = 0;
                var nBits2 = 32 - nBits;
                var highBits = (int) ((uint) mag[0] >> nBits2);

                if (highBits != 0)
                {
                    newMag = new int[magLen + nInts + 1];
                    newMag[i++] = highBits;
                }
                else
                {
                    newMag = new int[magLen + nInts];
                }

                var m = mag[0];
                for (var j = 0; j < magLen - 1; j++)
                {
                    var next = mag[j + 1];

                    newMag[i++] = (m << nBits) | (int) ((uint) next >> nBits2);
                    m = next;
                }

                newMag[i] = mag[magLen - 1] << nBits;
            }

            return newMag;
        }

        public BigInteger ShiftLeft(int n)
        {
            if (_sign == 0 || _magnitude.Length == 0)
            {
                return Zero;
            }

            if (n == 0)
            {
                return this;
            }

            if (n < 0)
            {
                return ShiftRight(-n);
            }

            var result = new BigInteger(_sign, ShiftLeft(_magnitude, n), true);

            if (_nBits != -1)
            {
                result._nBits = _sign > 0 ? _nBits : _nBits + n;
            }

            if (_nBitLength != -1)
            {
                result._nBitLength = _nBitLength + n;
            }

            return result;
        }

        /**
         * do a right shift - this does it in place.
         */

        private static void ShiftRightInPlace(int start, int[] mag, int n)
        {
            var nInts = (int) ((uint) n >> 5) + start;
            var nBits = n & 0x1f;
            var magEnd = mag.Length - 1;

            if (nInts != start)
            {
                var delta = nInts - start;

                for (var i = magEnd; i >= nInts; i--)
                {
                    mag[i] = mag[i - delta];
                }
                for (var i = nInts - 1; i >= start; i--)
                {
                    mag[i] = 0;
                }
            }

            if (nBits != 0)
            {
                var nBits2 = 32 - nBits;
                var m = mag[magEnd];

                for (var i = magEnd; i > nInts; --i)
                {
                    var next = mag[i - 1];

                    mag[i] = (int) ((uint) m >> nBits) | (next << nBits2);
                    m = next;
                }

                mag[nInts] = (int) ((uint) mag[nInts] >> nBits);
            }
        }

        /**
         * do a right shift by one - this does it in place.
         */

        private static void ShiftRightOneInPlace(int start, int[] mag)
        {
            var i = mag.Length;
            var m = mag[i - 1];

            while (--i > start)
            {
                var next = mag[i - 1];
                mag[i] = (int) ((uint) m >> 1) | (next << 31);
                m = next;
            }

            mag[start] = (int) ((uint) mag[start] >> 1);
        }

        public BigInteger ShiftRight(int n)
        {
            if (n == 0)
            {
                return this;
            }

            if (n < 0)
            {
                return ShiftLeft(-n);
            }

            if (n >= BitLength)
            {
                return _sign < 0 ? One.Negate() : Zero;
            }

            //			int[] res = (int[]) this.magnitude.Clone();
            //
            //			ShiftRightInPlace(0, res, n);
            //
            //			return new BigInteger(this.sign, res, true);

            var resultLength = (BitLength - n + 31) >> 5;
            var res = new int[resultLength];

            var numInts = n >> 5;
            var numBits = n & 31;

            if (numBits == 0)
            {
                Array.Copy(_magnitude, 0, res, 0, res.Length);
            }
            else
            {
                var numBits2 = 32 - numBits;

                var magPos = _magnitude.Length - 1 - numInts;
                for (var i = resultLength - 1; i >= 0; --i)
                {
                    res[i] = (int) ((uint) _magnitude[magPos--] >> numBits);

                    if (magPos >= 0)
                    {
                        res[i] |= _magnitude[magPos] << numBits2;
                    }
                }
            }

            Debug.Assert(res[0] != 0);

            return new BigInteger(_sign, res, false);
        }

        /**
         * returns x = x - y - we assume x is >= y
         */

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static int[] Subtract(int xStart, int[] x, int yStart, int[] y)
        {
            Debug.Assert(yStart < y.Length);
            Debug.Assert(x.Length - xStart >= y.Length - yStart);

            var iT = x.Length;
            var iV = y.Length;
            var borrow = 0;

            do
            {
                var m = (x[--iT] & Mask) - (y[--iV] & Mask) + borrow;
                x[iT] = (int) m;

                //				borrow = (m < 0) ? -1 : 0;
                borrow = (int) (m >> 63);
            } while (iV > yStart);

            if (borrow == 0) return x;
            while (--x[--iT] == -1)
            {
            }

            return x;
        }

        public BigInteger Subtract(BigInteger n)
        {
            if (n._sign == 0)
            {
                return this;
            }

            if (_sign == 0)
            {
                return n.Negate();
            }

            if (_sign != n._sign)
            {
                return Add(n.Negate());
            }

            var compare = CompareNoLeadingZeroes(0, _magnitude, 0, n._magnitude);
            if (compare == 0)
            {
                return Zero;
            }

            BigInteger bigun, lilun;
            if (compare < 0)
            {
                bigun = n;
                lilun = this;
            }
            else
            {
                bigun = this;
                lilun = n;
            }

            return new BigInteger(_sign * compare, DoSubBigLil(bigun._magnitude, lilun._magnitude), true);
        }

        private static int[] DoSubBigLil(int[] bigMag, int[] lilMag)
        {
            var res = (int[]) bigMag.Clone();

            return Subtract(0, res, 0, lilMag);
        }

        public byte[] ToByteArray()
        {
            return ToByteArray(false);
        }

        public byte[] ToByteArrayUnsigned()
        {
            return ToByteArray(true);
        }

        private byte[] ToByteArray(bool unsigned)
        {
            if (_sign == 0)
            {
                return unsigned ? ZeroEncoding : new byte[1];
            }

            var nBits = unsigned && _sign > 0 ? BitLength : BitLength + 1;

            var nBytes = GetByteLength(nBits);
            var bytes = new byte[nBytes];

            var magIndex = _magnitude.Length;
            var bytesIndex = bytes.Length;

            if (_sign > 0)
            {
                while (magIndex > 1)
                {
                    var mag = (uint) _magnitude[--magIndex];
                    bytes[--bytesIndex] = (byte) mag;
                    bytes[--bytesIndex] = (byte) (mag >> 8);
                    bytes[--bytesIndex] = (byte) (mag >> 16);
                    bytes[--bytesIndex] = (byte) (mag >> 24);
                }

                var lastMag = (uint) _magnitude[0];
                while (lastMag > byte.MaxValue)
                {
                    bytes[--bytesIndex] = (byte) lastMag;
                    lastMag >>= 8;
                }

                bytes[--bytesIndex] = (byte) lastMag;
            }
            else // sign < 0
            {
                var carry = true;

                while (magIndex > 1)
                {
                    var mag = ~(uint) _magnitude[--magIndex];

                    if (carry)
                    {
                        carry = ++mag == uint.MinValue;
                    }

                    bytes[--bytesIndex] = (byte) mag;
                    bytes[--bytesIndex] = (byte) (mag >> 8);
                    bytes[--bytesIndex] = (byte) (mag >> 16);
                    bytes[--bytesIndex] = (byte) (mag >> 24);
                }

                var lastMag = (uint) _magnitude[0];

                if (carry)
                {
                    // Never wraps because magnitude[0] != 0
                    --lastMag;
                }

                while (lastMag > byte.MaxValue)
                {
                    bytes[--bytesIndex] = (byte) ~lastMag;
                    lastMag >>= 8;
                }

                bytes[--bytesIndex] = (byte) ~lastMag;

                if (bytesIndex > 0)
                {
                    bytes[--bytesIndex] = byte.MaxValue;
                }
            }

            return bytes;
        }

        public override string ToString()
        {
            return ToString("G", CultureInfo.InvariantCulture);
        }

        public string ToString(int radix, IFormatProvider formatProvider = null, bool caps = true, int min = 1)
        {
            // TODO Make this method work for other radices (ideally 2 <= radix <= 16)

            switch (radix)
            {
                case 2:
                case 10:
                case 16:
                    break;
                default:
                    throw new FormatException("Only bases 2, 10, 16 are allowed");
            }

            if (formatProvider == null)
            {
                formatProvider = CultureInfo.InvariantCulture;
            }

            var nfi = (NumberFormatInfo) formatProvider.GetFormat(typeof(NumberFormatInfo));

            // NB: Can only happen to internally managed instances
            if (_magnitude == null)
            {
                return "null";
            }

            if (_sign == 0)
            {
                return GetZeroChars(min);
            }

            Debug.Assert(_magnitude.Length > 0);

            var sb = new StringBuilder();

            if (radix == 16)
            {
                sb.Append(_magnitude[0].ToString(caps ? "X" : "x"));

                for (var i = 1; i < _magnitude.Length; i++)
                {
                    sb.Append(_magnitude[i].ToString(caps ? "X8" : "x8"));
                }
            }
            else if (radix == 2)
            {
                sb.Append('1');

                for (var i = BitLength - 2; i >= 0; --i)
                {
                    sb.Append(TestBit(i) ? '1' : '0');
                }
            }
            else
            {
                // This is algorithm 1a from chapter 4.4 in Seminumerical Algorithms, slow but it works.
                var strings = new List<string>();
                var bs = ValueOf(radix);

                // The sign is handled separatly.
                var u = Abs();

                while (u._sign != 0)
                {
                    var b = u.Mod(bs);
                    strings.Add(b._sign == 0 ? "0" : b._magnitude[0].ToString(caps ? "D" : "d"));
                    u = u.Divide(bs);
                }

                // Then pop the stack
                for (var i = strings.Count - 1; i >= 0; --i)
                {
                    sb.Append(strings[i]);
                }
            }

            var str = sb.ToString();
            var strLength = str.Length;

            Debug.Assert(strLength > 0);

            if (strLength < min)
            {
                // Prepend with zeros to ensure minimal length.
                var gap = min - strLength;
                str = GetZeroChars(gap) + str;
            }
            else if (strLength > min && str[0] == '0')
            {
                // Strip leading zeros.
                var nonZeroPos = 0;
                while (str[++nonZeroPos] == '0')
                {
                }

                str = str.Substring(nonZeroPos);
            }

            if (_sign == -1)
            {
                if (nfi != null) str = nfi.NegativeSign + str;
            }

            return str;
        }

        private static BigInteger CreateUValueOf(ulong value)
        {
            var msw = (int) (value >> 32);
            var lsw = (int) value;

            if (msw != 0)
            {
                return new BigInteger(1, new[] {msw, lsw}, false);
            }

            if (lsw != 0)
            {
                var n = new BigInteger(1, new[] {lsw}, false);
                // Check for a power of two
                if ((lsw & -lsw) == lsw)
                {
                    n._nBits = 1;
                }
                return n;
            }

            return Zero;
        }

        private static BigInteger CreateValueOf(long value)
        {
            if (value < 0)
            {
                return value == long.MinValue ? CreateValueOf(~value).Not() : CreateValueOf(-value).Negate();
            }

            return CreateUValueOf((ulong) value);

            //			// store value into a byte array
            //			byte[] b = new byte[8];
            //			for (int i = 0; i < 8; i++)
            //			{
            //				b[7 - i] = (byte)value;
            //				value >>= 8;
            //			}
            //
            //			return new BigInteger(b);
        }

        public static BigInteger ValueOf(long value)
        {
            switch (value)
            {
                case 0:
                    return Zero;
                case 1:
                    return One;
                case 2:
                    return Two;
                case 3:
                    return Three;
                case 10:
                    return Ten;
            }

            return CreateValueOf(value);
        }

        public int GetLowestSetBit()
        {
            if (_sign == 0)
            {
                return -1;
            }

            var w = _magnitude.Length;

            while (--w > 0)
            {
                if (_magnitude[w] != 0)
                {
                    break;
                }
            }

            var word = _magnitude[w];
            Debug.Assert(word != 0);

            var b = (word & 0x0000FFFF) == 0 ? (word & 0x00FF0000) == 0 ? 7 : 15 :
                (word & 0x000000FF) == 0 ? 23 : 31;

            while (b > 0)
            {
                if (word << b == int.MinValue)
                {
                    break;
                }

                b--;
            }

            return (_magnitude.Length - w) * 32 - (b + 1);
        }

        public bool TestBit(int n)
        {
            if (n < 0)
            {
                throw new ArithmeticException("Bit position must not be negative");
            }

            if (_sign < 0)
            {
                return !Not().TestBit(n);
            }

            var wordNum = n / 32;
            if (wordNum >= _magnitude.Length)
            {
                return false;
            }

            var word = _magnitude[_magnitude.Length - 1 - wordNum];
            return ((word >> (n % 32)) & 1) > 0;
        }

        public BigInteger Or(BigInteger value)
        {
            if (_sign == 0)
            {
                return value;
            }

            if (value._sign == 0)
            {
                return this;
            }

            var aMag = _sign > 0 ? _magnitude : Add(One)._magnitude;

            var bMag = value._sign > 0 ? value._magnitude : value.Add(One)._magnitude;

            var resultNeg = _sign < 0 || value._sign < 0;
            var resultLength = Math.Max(aMag.Length, bMag.Length);
            var resultMag = new int[resultLength];

            var aStart = resultMag.Length - aMag.Length;
            var bStart = resultMag.Length - bMag.Length;

            for (var i = 0; i < resultMag.Length; ++i)
            {
                var aWord = i >= aStart ? aMag[i - aStart] : 0;
                var bWord = i >= bStart ? bMag[i - bStart] : 0;

                if (_sign < 0)
                {
                    aWord = ~aWord;
                }

                if (value._sign < 0)
                {
                    bWord = ~bWord;
                }

                resultMag[i] = aWord | bWord;

                if (resultNeg)
                {
                    resultMag[i] = ~resultMag[i];
                }
            }

            var result = new BigInteger(1, resultMag, true);

            // TODO Optimise this case
            if (resultNeg)
            {
                result = result.Not();
            }

            return result;
        }

        public BigInteger Xor(BigInteger value)
        {
            if (_sign == 0)
            {
                return value;
            }

            if (value._sign == 0)
            {
                return this;
            }

            var aMag = _sign > 0 ? _magnitude : Add(One)._magnitude;

            var bMag = value._sign > 0 ? value._magnitude : value.Add(One)._magnitude;

            // TODO Can just replace with sign != value.sign?
            var resultNeg = _sign < 0 && value._sign >= 0 || _sign >= 0 && value._sign < 0;
            var resultLength = Math.Max(aMag.Length, bMag.Length);
            var resultMag = new int[resultLength];

            var aStart = resultMag.Length - aMag.Length;
            var bStart = resultMag.Length - bMag.Length;

            for (var i = 0; i < resultMag.Length; ++i)
            {
                var aWord = i >= aStart ? aMag[i - aStart] : 0;
                var bWord = i >= bStart ? bMag[i - bStart] : 0;

                if (_sign < 0)
                {
                    aWord = ~aWord;
                }

                if (value._sign < 0)
                {
                    bWord = ~bWord;
                }

                resultMag[i] = aWord ^ bWord;

                if (resultNeg)
                {
                    resultMag[i] = ~resultMag[i];
                }
            }

            var result = new BigInteger(1, resultMag, true);

            // TODO Optimise this case
            if (resultNeg)
            {
                result = result.Not();
            }

            return result;
        }

        public BigInteger SetBit(int n)
        {
            if (n < 0)
            {
                throw new ArithmeticException("Bit address less than zero");
            }

            if (TestBit(n))
            {
                return this;
            }

            // TODO Handle negative values and zero
            if (_sign > 0 && n < BitLength - 1)
            {
                return FlipExistingBit(n);
            }

            return Or(One.ShiftLeft(n));
        }

        public BigInteger ClearBit(int n)
        {
            if (n < 0)
            {
                throw new ArithmeticException("Bit address less than zero");
            }

            if (!TestBit(n))
            {
                return this;
            }

            // TODO Handle negative values
            if (_sign > 0 && n < BitLength - 1)
            {
                return FlipExistingBit(n);
            }

            return AndNot(One.ShiftLeft(n));
        }

        public BigInteger FlipBit(int n)
        {
            if (n < 0)
            {
                throw new ArithmeticException("Bit address less than zero");
            }

            // TODO Handle negative values and zero
            if (_sign > 0 && n < BitLength - 1)
            {
                return FlipExistingBit(n);
            }

            return Xor(One.ShiftLeft(n));
        }

        private BigInteger FlipExistingBit(int n)
        {
            Debug.Assert(_sign > 0);
            Debug.Assert(n >= 0);
            Debug.Assert(n < BitLength - 1);

            var mag = (int[]) _magnitude.Clone();
            mag[mag.Length - 1 - (n >> 5)] ^= 1 << (n & 31); // Flip bit
            //mag[mag.Length - 1 - (n / 32)] ^= (1 << (n % 32));
            return new BigInteger(_sign, mag, false);
        }

        /// <summary>
        ///     Compares two BigInteger values and returns an integer that indicates whether the first value is less than, equal
        ///     to, or
        ///     greater than the second value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>A signed integer that indicates the relative values of left and right, as shown in the following table.</returns>
        public static int Compare(BigInteger left, object right)
        {
            if (right is BigInteger integer)
            {
                return Compare(left, integer);
            }

            // NOTE: this could be optimized type per type
            if (right is bool b)
            {
                return Compare(left, new BigInteger(b));
            }

            if (right is byte b1)
            {
                return Compare(left, new BigInteger(b1));
            }

            if (right is char c)
            {
                return Compare(left, new BigInteger(c));
            }

            if (right is decimal @decimal)
            {
                return Compare(left, new BigInteger(@decimal));
            }

            if (right is double d)
            {
                return Compare(left, new BigInteger(d));
            }

            if (right is short s)
            {
                return Compare(left, new BigInteger(s));
            }

            if (right is int i)
            {
                return Compare(left, new BigInteger(i));
            }

            if (right is long l)
            {
                return Compare(left, new BigInteger(l));
            }

            if (right is sbyte @sbyte)
            {
                return Compare(left, new BigInteger(@sbyte));
            }

            if (right is float f)
            {
                return Compare(left, new BigInteger(f));
            }

            if (right is ushort @ushort)
            {
                return Compare(left, new BigInteger(@ushort));
            }

            if (right is uint u)
            {
                return Compare(left, new BigInteger(u));
            }

            if (right is ulong @ulong)
            {
                return Compare(left, new BigInteger(@ulong));
            }

            if (right is byte[] bytes && bytes.Length == 32)
            {
                // TODO: ensure endian.
                return Compare(left, new BigInteger(bytes));
            }

            if (right is Guid guid)
            {
                return Compare(left, new BigInteger(guid));
            }

            throw new ArgumentException();
        }

        /// <summary>
        ///     Compares two 256-bit signed integer values and returns an integer that indicates whether the first value is less
        ///     than, equal to, or greater than the second value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        ///     A signed number indicating the relative values of this instance and value.
        /// </returns>
        public static int Compare(BigInteger left, BigInteger right)
        {
            var leftSign = left.Sign;
            var rightSign = right.Sign;

            if (leftSign == 0 && rightSign == 0)
            {
                return 0;
            }

            if (leftSign > rightSign)
            {
                return 1;
            }

            if (leftSign < rightSign)
            {
                return -1;
            }

            return leftSign * CompareNoLeadingZeroes(0, left._magnitude, 0, right._magnitude);
        }

        #region Zero chars buffer

        private static volatile char[] _zeroCharsBuffer;
        private static readonly object ZeroCharsBufferSyncRoot = new object();

        private static char[] GetZeroCharsBuffer(int minLength)
        {
            lock (ZeroCharsBufferSyncRoot)
            {
                if (_zeroCharsBuffer == null || _zeroCharsBuffer.Length < minLength)
                {
                    _zeroCharsBuffer = new char[minLength];
                    for (var i = 0; i < minLength; i++)
                    {
                        _zeroCharsBuffer[i] = '0';
                    }
                }
                return _zeroCharsBuffer;
            }
        }

        private static string GetZeroChars(int length)
        {
            return new string(GetZeroCharsBuffer(length), 0, length);
        }

        #endregion
    }
}