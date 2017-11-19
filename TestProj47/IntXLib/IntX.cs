/*

Copyright (c) 2005-2015, Andriy Kozachuk
All rights reserved.

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this
  list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice, this
  list of conditions and the following disclaimer in the documentation and/or
  other materials provided with the distribution.

* Neither the name of Andriy Kozachuk nor the names of its contributors may be
  used to endorse or promote products derived from this software without
  specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/


using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TestProj47;

namespace HSNXT
{
    /// <summary>
    /// Numeric class which represents arbitrary-precision integers.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct IntX :
        IEquatable<IntX>, IEquatable<int>, IEquatable<uint>, IEquatable<long>, IEquatable<ulong>,
        IComparable, IComparable<IntX>, IComparable<int>, IComparable<uint>, IComparable<long>, IComparable<ulong>
    {
#if DEBUG

        /// <summary>
        /// Lock for maximal error during FHT rounding (debug-mode only).
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal static readonly object MaxFhtRoundErrorLock = new object();

        /// <summary>
        /// Maximal error during FHT rounding (debug-mode only).
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public static double MaxFhtRoundError;
#endif

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] internal uint[] Digits; // big integer digits

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] internal uint Length; // big integer digits length

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] internal bool Negative; // big integer sign ("-" if true)

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay => ToString();

        #region Constructors

        // ReSharper disable once UnusedParameter.Local
        private IntX(bool _) : this()
        {
            Settings = new IntXSettings(GlobalSettings);
        }

        /// <summary>
        /// Creates new big integer from integer value.
        /// </summary>
        /// <param name="value">Integer value to create big integer from.</param>
        public IntX(int value) : this(false)
        {
            if (value == 0)
            {
                InitializeZero();
            }
            else
            {
                // Prepare internal fields
                Digits = new uint[Length = 1];

                // Fill the only big integer digit
                DigitHelper.ToUInt32WithSign(value, out Digits[0], out Negative);
            }
        }

        /// <summary>
        /// Creates new big integer from unsigned integer value.
        /// </summary>
        /// <param name="value">Unsigned integer value to create big integer from.</param>
        public IntX(uint value) : this(false)
        {
            Settings = new IntXSettings(GlobalSettings);
            if (value == 0)
            {
                InitializeZero();
            }
            else
            {
                // Prepare internal fields
                Digits = new[] {value};
                Length = 1;
            }
        }

        /// <summary>
        /// Creates new big integer from long value.
        /// </summary>
        /// <param name="value">Long value to create big integer from.</param>
        public IntX(long value) : this(false)
        {
            Settings = new IntXSettings(GlobalSettings);
            if (value == 0)
            {
                InitializeZero();
            }
            else
            {
                // Fill the only big integer digit
                DigitHelper.ToUInt64WithSign(value, out var newValue, out Negative);
                InitFromUlong(newValue);
            }
        }

        /// <summary>
        /// Creates new big integer from unsigned long value.
        /// </summary>
        /// <param name="value">Unsigned long value to create big integer from.</param>
        public IntX(ulong value) : this(false)
        {
            Settings = new IntXSettings(GlobalSettings);
            if (value == 0)
            {
                InitializeZero();
            }
            else
            {
                InitFromUlong(value);
            }
        }

        /// <summary>
        /// Creates new big integer from array of it's "digits".
        /// Digit with lower index has less weight.
        /// </summary>
        /// <param name="digits">Array of <see cref="IntX" /> digits.</param>
        /// <param name="negative">True if this number is negative.</param>
        /// <exception cref="ArgumentNullException"><paramref name="digits" /> is a null reference.</exception>
        public IntX(uint[] digits, bool negative) : this(false)
        {
            // Exceptions
            if (digits == null)
            {
                throw new ArgumentNullException(nameof(digits));
            }

            InitFromDigits(digits, negative, DigitHelper.GetRealDigitsLength(digits, (uint) digits.LongLength));
        }


        /// <summary>
        /// Creates new <see cref="IntX" /> from string.
        /// </summary>
        /// <param name="value">Number as string.</param>
        public IntX(string value) : this(false)
        {
            var intX = Parse(value);
            InitFromIntX(intX);
        }

        /// <summary>
        /// Creates new <see cref="IntX" /> from string.
        /// </summary>
        /// <param name="value">Number as string.</param>
        /// <param name="numberBase">Number base.</param>
        public IntX(string value, uint numberBase) : this(false)
        {
            var intX = Parse(value, numberBase);
            InitFromIntX(intX);
        }


        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="value">Value to copy from.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value" /> is a null reference.</exception>
        public IntX(IntX value) : this(false)
        {
            // Exceptions
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            InitFromIntX(value);
        }


        /// <summary>
        /// Creates new empty big integer with desired sign and length.
        /// 
        /// For internal use.
        /// </summary>
        /// <param name="length">Desired digits length.</param>
        /// <param name="negative">Desired integer sign.</param>
        internal IntX(uint length, bool negative) : this(false)
        {
            Digits = new uint[Length = length];
            Negative = negative;
        }

        /// <summary>
        /// Creates new big integer from array of it's "digits" but with given length.
        /// Digit with lower index has less weight.
        /// 
        /// For internal use.
        /// </summary>
        /// <param name="digits">Array of <see cref="IntX" /> digits.</param>
        /// <param name="negative">True if this number is negative.</param>
        /// <param name="length">Length to use for internal digits array.</param>
        /// <exception cref="ArgumentNullException"><paramref name="digits" /> is a null reference.</exception>
        internal IntX(uint[] digits, bool negative, uint length) : this(false)
        {
            // Exceptions
            if (digits == null)
            {
                throw new ArgumentNullException(nameof(digits));
            }

            InitFromDigits(digits, negative, length);
        }

        #endregion Constructors

        #region Static public properties

        /// <summary>
        /// <see cref="IntX" /> global settings.
        /// </summary>
        public static IntXGlobalSettings GlobalSettings { get; } = new IntXGlobalSettings();

        #endregion Static public properties

        #region Public properties

        /// <summary>
        /// <see cref="IntX" /> instance settings.
        /// </summary>
        public IntXSettings Settings;

        /// <summary>
        /// Gets flag indicating if big integer is odd.
        /// </summary>
        public bool IsOdd => Length > 0 && (Digits[0] & 1) == 1;

        #endregion Public properties

        #region Operators

        #region operator ==

        /// <summary>
        /// Compares two <see cref="IntX" /> objects and returns true if their internal state is equal.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if equals.</returns>
        public static bool operator ==(IntX int1, IntX int2)
        {
            return OpHelper.Cmp(int1, int2, false) == 0;
        }

        /// <summary>
        /// Compares <see cref="IntX" /> object with integer and returns true if their internal state is equal.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second integer.</param>
        /// <returns>True if equals.</returns>
        public static bool operator ==(IntX int1, int int2)
        {
            return OpHelper.Cmp(int1, int2) == 0;
        }

        /// <summary>
        /// Compares integer with <see cref="IntX" /> object and returns true if their internal state is equal.
        /// </summary>
        /// <param name="int1">First integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if equals.</returns>
        public static bool operator ==(int int1, IntX int2)
        {
            return OpHelper.Cmp(int2, int1) == 0;
        }

        /// <summary>
        /// Compares <see cref="IntX" /> object with unsinged integer and returns true if their internal state is equal.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second unsinged integer.</param>
        /// <returns>True if equals.</returns>
        public static bool operator ==(IntX int1, uint int2)
        {
            return OpHelper.Cmp(int1, int2) == 0;
        }

        /// <summary>
        /// Compares unsinged integer with <see cref="IntX" /> object and returns true if their internal state is equal.
        /// </summary>
        /// <param name="int1">First unsinged integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if equals.</returns>
        public static bool operator ==(uint int1, IntX int2)
        {
            return OpHelper.Cmp(int2, int1) == 0;
        }

        #endregion operator ==

        #region operator !=

        /// <summary>
        /// Compares two <see cref="IntX" /> objects and returns true if their internal state is not equal.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if not equals.</returns>
        public static bool operator !=(IntX int1, IntX int2)
        {
            return OpHelper.Cmp(int1, int2, false) != 0;
        }

        /// <summary>
        /// Compares <see cref="IntX" /> object with integer and returns true if their internal state is not equal.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second integer.</param>
        /// <returns>True if not equals.</returns>
        public static bool operator !=(IntX int1, int int2)
        {
            return OpHelper.Cmp(int1, int2) != 0;
        }

        /// <summary>
        /// Compares integer with <see cref="IntX" /> object and returns true if their internal state is not equal.
        /// </summary>
        /// <param name="int1">First integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if not equals.</returns>
        public static bool operator !=(int int1, IntX int2)
        {
            return OpHelper.Cmp(int2, int1) != 0;
        }

        /// <summary>
        /// Compares <see cref="IntX" /> object with unsigned integer and returns true if their internal state is not equal.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second unsigned integer.</param>
        /// <returns>True if not equals.</returns>
        ////[CLSCompliant(false)]
        public static bool operator !=(IntX int1, uint int2)
        {
            return OpHelper.Cmp(int1, int2) != 0;
        }

        /// <summary>
        /// Compares unsigned integer with <see cref="IntX" /> object and returns true if their internal state is not equal.
        /// </summary>
        /// <param name="int1">First unsigned integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if not equals.</returns>
        ////[CLSCompliant(false)]
        public static bool operator !=(uint int1, IntX int2)
        {
            return OpHelper.Cmp(int2, int1) != 0;
        }

        #endregion operator !=

        #region operator >

        /// <summary>
        /// Compares two <see cref="IntX" /> objects and returns true if first is greater.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if first is greater.</returns>
        public static bool operator >(IntX int1, IntX int2)
        {
            return OpHelper.Cmp(int1, int2, true) > 0;
        }

        /// <summary>
        /// Compares <see cref="IntX" /> object with integer and returns true if first is greater.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second integer.</param>
        /// <returns>True if first is greater.</returns>
        public static bool operator >(IntX int1, int int2)
        {
            return OpHelper.Cmp(int1, int2) > 0;
        }

        /// <summary>
        /// Compares integer with <see cref="IntX" /> object and returns true if first is greater.
        /// </summary>
        /// <param name="int1">First integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if first is greater.</returns>
        public static bool operator >(int int1, IntX int2)
        {
            return OpHelper.Cmp(int2, int1) < 0;
        }

        /// <summary>
        /// Compares <see cref="IntX" /> object with unsigned integer and returns true if first is greater.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second unsigned integer.</param>
        /// <returns>True if first is greater.</returns>
        //[CLSCompliant(false)]
        public static bool operator >(IntX int1, uint int2)
        {
            return OpHelper.Cmp(int1, int2) > 0;
        }

        /// <summary>
        /// Compares unsigned integer with <see cref="IntX" /> object and returns true if first is greater.
        /// </summary>
        /// <param name="int1">First unsigned integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if first is greater.</returns>
        //[CLSCompliant(false)]
        public static bool operator >(uint int1, IntX int2)
        {
            return OpHelper.Cmp(int2, int1) < 0;
        }

        #endregion operator >

        #region operator >=

        /// <summary>
        /// Compares two <see cref="IntX" /> objects and returns true if first is greater or equal.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if first is greater or equal.</returns>
        public static bool operator >=(IntX int1, IntX int2)
        {
            return OpHelper.Cmp(int1, int2, true) >= 0;
        }

        /// <summary>
        /// Compares <see cref="IntX" /> object with integer and returns true if first is greater or equal.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second integer.</param>
        /// <returns>True if first is greater or equal.</returns>
        public static bool operator >=(IntX int1, int int2)
        {
            return OpHelper.Cmp(int1, int2) >= 0;
        }

        /// <summary>
        /// Compares integer with <see cref="IntX" /> object and returns true if first is greater or equal.
        /// </summary>
        /// <param name="int1">First integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if first is greater or equal.</returns>
        public static bool operator >=(int int1, IntX int2)
        {
            return OpHelper.Cmp(int2, int1) <= 0;
        }

        /// <summary>
        /// Compares <see cref="IntX" /> object with unsinged integer and returns true if first is greater or equal.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second unsinged integer.</param>
        /// <returns>True if first is greater or equal.</returns>
        //[CLSCompliant(false)]
        public static bool operator >=(IntX int1, uint int2)
        {
            return OpHelper.Cmp(int1, int2) >= 0;
        }

        /// <summary>
        /// Compares unsinged integer with <see cref="IntX" /> object and returns true if first is greater or equal.
        /// </summary>
        /// <param name="int1">First unsinged integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if first is greater or equal.</returns>
        //[CLSCompliant(false)]
        public static bool operator >=(uint int1, IntX int2)
        {
            return OpHelper.Cmp(int2, int1) <= 0;
        }

        #endregion operator >=

        #region operator <

        /// <summary>
        /// Compares two <see cref="IntX" /> objects and returns true if first is lighter.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if first is lighter.</returns>
        public static bool operator <(IntX int1, IntX int2)
        {
            return OpHelper.Cmp(int1, int2, true) < 0;
        }

        /// <summary>
        /// Compares <see cref="IntX" /> object with integer and returns true if first is lighter.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second integer.</param>
        /// <returns>True if first is lighter.</returns>
        public static bool operator <(IntX int1, int int2)
        {
            return OpHelper.Cmp(int1, int2) < 0;
        }

        /// <summary>
        /// Compares integer with <see cref="IntX" /> object and returns true if first is lighter.
        /// </summary>
        /// <param name="int1">First integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if first is lighter.</returns>
        public static bool operator <(int int1, IntX int2)
        {
            return OpHelper.Cmp(int2, int1) > 0;
        }

        /// <summary>
        /// Compares <see cref="IntX" /> object with unsinged integer and returns true if first is lighter.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second unsinged integer.</param>
        /// <returns>True if first is lighter.</returns>
        //[CLSCompliant(false)]
        public static bool operator <(IntX int1, uint int2)
        {
            return OpHelper.Cmp(int1, int2) < 0;
        }

        /// <summary>
        /// Compares unsinged integer with <see cref="IntX" /> object and returns true if first is lighter.
        /// </summary>
        /// <param name="int1">First unsinged integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if first is lighter.</returns>
        //[CLSCompliant(false)]
        public static bool operator <(uint int1, IntX int2)
        {
            return OpHelper.Cmp(int2, int1) > 0;
        }

        #endregion operator<

        #region operator <=

        /// <summary>
        /// Compares two <see cref="IntX" /> objects and returns true if first is lighter or equal.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if first is lighter or equal.</returns>
        public static bool operator <=(IntX int1, IntX int2)
        {
            return OpHelper.Cmp(int1, int2, true) <= 0;
        }

        /// <summary>
        /// Compares <see cref="IntX" /> object with integer and returns true if first is lighter or equal.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second integer.</param>
        /// <returns>True if first is lighter or equal.</returns>
        public static bool operator <=(IntX int1, int int2)
        {
            return OpHelper.Cmp(int1, int2) <= 0;
        }

        /// <summary>
        /// Compares integer with <see cref="IntX" /> object and returns true if first is lighter or equal.
        /// </summary>
        /// <param name="int1">First integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if first is lighter or equal.</returns>
        public static bool operator <=(int int1, IntX int2)
        {
            return OpHelper.Cmp(int2, int1) >= 0;
        }

        /// <summary>
        /// Compares <see cref="IntX" /> object with unsinged integer and returns true if first is lighter or equal.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second unsinged integer.</param>
        /// <returns>True if first is lighter or equal.</returns>
        //[CLSCompliant(false)]
        public static bool operator <=(IntX int1, uint int2)
        {
            return OpHelper.Cmp(int1, int2) <= 0;
        }

        /// <summary>
        /// Compares unsinged integer with <see cref="IntX" /> object and returns true if first is lighter or equal.
        /// </summary>
        /// <param name="int1">First unsinged integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>True if first is lighter or equal.</returns>
        //[CLSCompliant(false)]
        public static bool operator <=(uint int1, IntX int2)
        {
            return OpHelper.Cmp(int2, int1) >= 0;
        }

        #endregion operator<=

        #region operator+ and operator-

        /// <summary>
        /// Adds one <see cref="IntX" /> object to another.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>Addition result.</returns>
        public static IntX operator +(IntX int1, IntX int2)
        {
            return OpHelper.AddSub(int1, int2, false);
        }

        /// <summary>
        /// Subtracts one <see cref="IntX" /> object from another.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>Subtraction result.</returns>
        public static IntX operator -(IntX int1, IntX int2)
        {
            return OpHelper.AddSub(int1, int2, true);
        }

        #endregion operator+ and operator-

        #region operator*

        /// <summary>
        /// Multiplies one <see cref="IntX" /> object on another.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>Multiply result.</returns>
        public static IntX operator *(IntX int1, IntX int2)
        {
            return MultiplyManager.GetCurrentMultiplier().Multiply(int1, int2);
        }

        #endregion operator*

        #region operator/ and operator%

        /// <summary>
        /// Divides one <see cref="IntX" /> object by another.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>Division result.</returns>
        public static IntX operator /(IntX int1, IntX int2)
        {
            return DivideManager.GetCurrentDivider().DivMod(int1, int2, out _, DivModResultFlags.Div);
        }

        /// <summary>
        /// Divides one <see cref="IntX" /> object by another and returns division modulo.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>Modulo result.</returns>
        public static IntX operator %(IntX int1, IntX int2)
        {
            DivideManager.GetCurrentDivider().DivMod(int1, int2, out var modRes, DivModResultFlags.Mod);
            return modRes;
        }

        #endregion operator/ and operator%

        #region operator<< and operator>>

        /// <summary>
        /// Shifts <see cref="IntX" /> object on selected bits count to the left.
        /// </summary>
        /// <param name="intX">Big integer.</param>
        /// <param name="shift">Bits count.</param>
        /// <returns>Shifting result.</returns>
        public static IntX operator <<(IntX intX, int shift)
        {
            return OpHelper.Sh(intX, shift, true);
        }

        /// <summary>
        /// Shifts <see cref="IntX" /> object on selected bits count to the right.
        /// </summary>
        /// <param name="intX">Big integer.</param>
        /// <param name="shift">Bits count.</param>
        /// <returns>Shifting result.</returns>
        public static IntX operator >>(IntX intX, int shift)
        {
            return OpHelper.Sh(intX, shift, false);
        }

        #endregion operator<< and operator>>

        #region +, -, ++, -- unary operators

        /// <summary>
        /// Returns the same <see cref="IntX" /> value.
        /// </summary>
        /// <param name="value">Initial value.</param>
        /// <returns>The same value, but new object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value" /> is a null reference.</exception>
        public static IntX operator +(IntX value)
        {
            // Exception
            if (default == value)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new IntX(value);
        }

        /// <summary>
        /// Returns the same <see cref="IntX" /> value, but with other sign.
        /// </summary>
        /// <param name="value">Initial value.</param>
        /// <returns>The same value, but with other sign.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value" /> is a null reference.</exception>
        public static IntX operator -(IntX value)
        {
            // Exception
            if (default == value)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var newValue = new IntX(value);
            if (newValue.Length != 0)
            {
                newValue.Negative = !newValue.Negative;
            }
            return newValue;
        }

        /// <summary>
        /// Returns increased <see cref="IntX" /> value.
        /// </summary>
        /// <param name="value">Initial value.</param>
        /// <returns>Increased value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value" /> is a null reference.</exception>
        public static IntX operator ++(IntX value)
        {
            // Exception
            if (default == value)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value + 1U;
        }

        /// <summary>
        /// Returns decreased <see cref="IntX" /> value.
        /// </summary>
        /// <param name="value">Initial value.</param>
        /// <returns>Decreased value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value" /> is a null reference.</exception>
        public static IntX operator --(IntX value)
        {
            // Exception
            if (default == value)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value - 1U;
        }

        #endregion +, -, ++, -- unary operators

        #region Bitwise operations

        /// <summary>
        /// Performs bitwise OR for two big integers.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>Resulting big integer.</returns>
        public static IntX operator |(IntX int1, IntX int2)
        {
            return OpHelper.BitwiseOr(int1, int2);
        }

        /// <summary>
        /// Performs bitwise AND for two big integers.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>Resulting big integer.</returns>
        public static IntX operator &(IntX int1, IntX int2)
        {
            return OpHelper.BitwiseAnd(int1, int2);
        }

        /// <summary>
        /// Performs bitwise XOR for two big integers.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>Resulting big integer.</returns>
        public static IntX operator ^(IntX int1, IntX int2)
        {
            return OpHelper.ExclusiveOr(int1, int2);
        }

        /// <summary>
        /// Performs bitwise NOT for big integer.
        /// </summary>
        /// <param name="value">Big integer.</param>
        /// <returns>Resulting big integer.</returns>
        public static IntX operator ~(IntX value)
        {
            return OpHelper.OnesComplement(value);
        }

        #endregion Bitwise operations

        #region Conversion operators

        #region To IntX (Implicit)

        /// <summary>
        /// Implicitly converts <see cref="Int32" /> to <see cref="IntX" />.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Conversion result.</returns>
        public static implicit operator IntX(int value)
        {
            return new IntX(value);
        }

        /// <summary>
        /// Implicitly converts <see cref="UInt32" /> to <see cref="IntX" />.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Conversion result.</returns>
        //[CLSCompliant(false)]
        public static implicit operator IntX(uint value)
        {
            return new IntX(value);
        }

        /// <summary>
        /// Implicitly converts <see cref="UInt16" /> to <see cref="IntX" />.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Conversion result.</returns>
        //[CLSCompliant(false)]
        public static implicit operator IntX(ushort value)
        {
            return new IntX(value);
        }

        /// <summary>
        /// Implicitly converts <see cref="Int64" /> to <see cref="IntX" />.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Conversion result.</returns>
        public static implicit operator IntX(long value)
        {
            return new IntX(value);
        }

        /// <summary>
        /// Implicitly converts <see cref="UInt64" /> to <see cref="IntX" />.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Conversion result.</returns>
        //[CLSCompliant(false)]
        public static implicit operator IntX(ulong value)
        {
            return new IntX(value);
        }

        #endregion To IntX (Implicit)

        #region From IntX (Explicit)

        /// <summary>
        /// Explicitly converts <see cref="IntX" /> to <see cref="int" />.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Conversion result.</returns>
        public static explicit operator int(IntX value)
        {
            var res = (int) (uint) value;
            return value.Negative ? -res : res;
        }

        /// <summary>
        /// Explicitly converts <see cref="IntX" /> to <see cref="uint" />.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Conversion result.</returns>
        //[CLSCompliant(false)]
        public static explicit operator uint(IntX value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.Length == 0 ? 0 : value.Digits[0];
        }

        /// <summary>
        /// Explicitly converts <see cref="IntX" /> to <see cref="long" />.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Conversion result.</returns>
        public static explicit operator long(IntX value)
        {
            var res = (long) (ulong) value;
            return value.Negative ? -res : res;
        }

        /// <summary>
        /// Explicitly converts <see cref="IntX" /> to <see cref="ulong" />.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Conversion result.</returns>
        //[CLSCompliant(false)]
        public static explicit operator ulong(IntX value)
        {
            ulong res = (uint) value;
            if (value.Length > 1)
            {
                res |= (ulong) value.Digits[1] << Constants.DigitBitCount;
            }
            return res;
        }

        /// <summary>
        /// Explicitly converts <see cref="IntX" /> to <see cref="ushort" />.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Conversion result.</returns>
        //[CLSCompliant(false)]
        public static explicit operator ushort(IntX value)
        {
            return (ushort) (uint) value;
        }

        #endregion From IntX (Explicit)

        #endregion Conversion operators

        #endregion Operators

        #region Math static methods

        #region Multiply

        /// <summary>
        /// Multiplies one <see cref="IntX" /> object on another.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <param name="mode">Multiply mode set explicitly.</param>
        /// <returns>Multiply result.</returns>
        public static IntX Multiply(IntX int1, IntX int2, MultiplyMode mode)
        {
            return MultiplyManager.GetMultiplier(mode).Multiply(int1, int2);
        }

        #endregion Multiply

        #region Divide/modulo

        /// <summary>
        /// Divides one <see cref="IntX" /> object by another.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <param name="mode">Divide mode.</param>
        /// <returns>Division result.</returns>
        public static IntX Divide(IntX int1, IntX int2, DivideMode mode)
        {
            return DivideManager.GetDivider(mode).DivMod(int1, int2, out _, DivModResultFlags.Div);
        }

        /// <summary>
        /// Divides one <see cref="IntX" /> object by another and returns division modulo.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <param name="mode">Divide mode.</param>
        /// <returns>Modulo result.</returns>
        public static IntX Modulo(IntX int1, IntX int2, DivideMode mode)
        {
            DivideManager.GetDivider(mode).DivMod(int1, int2, out var modRes, DivModResultFlags.Mod);
            return modRes;
        }

        /// <summary>
        /// Divides one <see cref="IntX" /> object on another.
        /// Returns both divident and remainder
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <param name="modRes">Remainder big integer.</param>
        /// <returns>Division result.</returns>
        public static IntX DivideModulo(IntX int1, IntX int2, out IntX modRes)
        {
            return DivideManager.GetCurrentDivider()
                .DivMod(int1, int2, out modRes, DivModResultFlags.Div | DivModResultFlags.Mod);
        }

        /// <summary>
        /// Divides one <see cref="IntX" /> object on another.
        /// Returns both divident and remainder
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <param name="modRes">Remainder big integer.</param>
        /// <param name="mode">Divide mode.</param>
        /// <returns>Division result.</returns>
        public static IntX DivideModulo(IntX int1, IntX int2, out IntX modRes, DivideMode mode)
        {
            return DivideManager.GetDivider(mode)
                .DivMod(int1, int2, out modRes, DivModResultFlags.Div | DivModResultFlags.Mod);
        }

        #endregion Divide/modulo

        #region Pow

        /// <summary>
        /// Returns a specified big integer raised to the specified power.
        /// </summary>
        /// <param name="value">Number to raise.</param>
        /// <param name="power">Power.</param>
        /// <returns>Number in given power.</returns>
        //[CLSCompliant(false)]
        public static IntX Pow(IntX value, uint power)
        {
            return OpHelper.Pow(value, power, GlobalSettings.MultiplyMode);
        }

        /// <summary>
        /// Returns a specified big integer raised to the specified power.
        /// </summary>
        /// <param name="value">Number to raise.</param>
        /// <param name="power">Power.</param>
        /// <param name="multiplyMode">Multiply mode set explicitly.</param>
        /// <returns>Number in given power.</returns>
        //[CLSCompliant(false)]
        public static IntX Pow(IntX value, uint power, MultiplyMode multiplyMode)
        {
            return OpHelper.Pow(value, power, multiplyMode);
        }

        #endregion Pow

        #endregion Math static methods

        #region ToString override

        /// <summary>
        /// Returns decimal string representation of this <see cref="IntX" /> object.
        /// </summary>
        /// <returns>Decimal number in string.</returns>
        public override string ToString()
        {
            return ToString(10U, true);
        }

        /// <summary>
        /// Returns string representation of this <see cref="IntX" /> object in given base.
        /// </summary>
        /// <param name="numberBase">Base of system in which to do output.</param>
        /// <returns>Object string representation.</returns>
        public string ToString(uint numberBase)
        {
            return ToString(numberBase, true);
        }

        /// <summary>
        /// Returns string representation of this <see cref="IntX" /> object in given base.
        /// </summary>
        /// <param name="numberBase">Base of system in which to do output.</param>
        /// <param name="upperCase">Use uppercase for bases from 11 to 16 (which use letters A-F).</param>
        /// <returns>Object string representation.</returns>
        public string ToString(uint numberBase, bool upperCase)
        {
            return StringConvertManager.GetStringConverter(Settings.ToStringMode)
                .ToString(this, numberBase, upperCase ? Constants.BaseUpperChars : Constants.BaseLowerChars);
        }

        /// <summary>
        /// Returns string representation of this <see cref="IntX" /> object in given base using custom alphabet.
        /// </summary>
        /// <param name="numberBase">Base of system in which to do output.</param>
        /// <param name="alphabet">Alphabet which contains chars used to represent big integer, char position is coresponding digit value.</param>
        /// <returns>Object string representation.</returns>
        public string ToString(uint numberBase, string alphabet)
        {
            StrRepHelper.AssertAlphabet(alphabet, numberBase);
            return StringConvertManager.GetStringConverter(Settings.ToStringMode)
                .ToString(this, numberBase, alphabet.ToCharArray());
        }

        #endregion ToString override

        #region Parsing methods

        /// <summary>
        /// Parses provided string representation of <see cref="IntX" /> object in decimal base.
        /// If number starts from "0" then it's treated as octal; if number starts fropm "0x"
        /// then it's treated as hexadecimal.
        /// </summary>
        /// <param name="value">Number as string.</param>
        /// <returns>Parsed object.</returns>
        public static IntX Parse(string value)
        {
            return ParseManager.GetCurrentParser().Parse(value, 10U, Constants.BaseCharToDigits, true);
        }

        /// <summary>
        /// Parses provided string representation of <see cref="IntX" /> object.
        /// </summary>
        /// <param name="value">Number as string.</param>
        /// <param name="numberBase">Number base.</param>
        /// <returns>Parsed object.</returns>
        public static IntX Parse(string value, uint numberBase)
        {
            return ParseManager.GetCurrentParser().Parse(value, numberBase, Constants.BaseCharToDigits, false);
        }

        /// <summary>
        /// Parses provided string representation of <see cref="IntX" /> object using custom alphabet.
        /// </summary>
        /// <param name="value">Number as string.</param>
        /// <param name="numberBase">Number base.</param>
        /// <param name="alphabet">Alphabet which contains chars used to represent big integer, char position is coresponding digit value.</param>
        /// <returns>Parsed object.</returns>
        public static IntX Parse(string value, uint numberBase, string alphabet)
        {
            return ParseManager.GetCurrentParser()
                .Parse(value, numberBase, StrRepHelper.CharDictionaryFromAlphabet(alphabet, numberBase), false);
        }

        /// <summary>
        /// Parses provided string representation of <see cref="IntX" /> object in decimal base.
        /// If number starts from "0" then it's treated as octal; if number starts fropm "0x"
        /// then it's treated as hexadecimal.
        /// </summary>
        /// <param name="value">Number as string.</param>
        /// <param name="mode">Parse mode.</param>
        /// <returns>Parsed object.</returns>
        public static IntX Parse(string value, ParseMode mode)
        {
            return ParseManager.GetParser(mode).Parse(value, 10U, Constants.BaseCharToDigits, true);
        }

        /// <summary>
        /// Parses provided string representation of <see cref="IntX" /> object.
        /// </summary>
        /// <param name="value">Number as string.</param>
        /// <param name="numberBase">Number base.</param>
        /// <param name="mode">Parse mode.</param>
        /// <returns>Parsed object.</returns>
        public static IntX Parse(string value, uint numberBase, ParseMode mode)
        {
            return ParseManager.GetParser(mode).Parse(value, numberBase, Constants.BaseCharToDigits, false);
        }

        /// <summary>
        /// Parses provided string representation of <see cref="IntX" /> object using custom alphabet.
        /// </summary>
        /// <param name="value">Number as string.</param>
        /// <param name="numberBase">Number base.</param>
        /// <param name="alphabet">Alphabet which contains chars used to represent big integer, char position is coresponding digit value.</param>
        /// <param name="mode">Parse mode.</param>
        /// <returns>Parsed object.</returns>
        public static IntX Parse(string value, uint numberBase, string alphabet, ParseMode mode)
        {
            return ParseManager.GetParser(mode)
                .Parse(value, numberBase, StrRepHelper.CharDictionaryFromAlphabet(alphabet, numberBase), false);
        }

        #endregion Parsing methods

        #region IEquatable/Equals/GetHashCode implementation/overrides

        /// <inheritdoc />
        /// <summary>
        /// Returns equality of this <see cref="T:HSNXT.IntX" /> with another big integer.
        /// </summary>
        /// <param name="n">Big integer to compare with.</param>
        /// <returns>True if equals.</returns>
        public bool Equals(IntX n) => Equals(this, n) || this == n;

        /// <inheritdoc />
        /// <summary>
        /// Returns equality of this <see cref="T:HSNXT.IntX" /> with another integer.
        /// </summary>
        /// <param name="n">Integer to compare with.</param>
        /// <returns>True if equals.</returns>
        public bool Equals(int n) => this == n;

        /// <inheritdoc />
        /// <summary>
        /// Returns equality of this <see cref="T:HSNXT.IntX" /> with another unsigned integer.
        /// </summary>
        /// <param name="n">Unsigned integer to compare with.</param>
        /// <returns>True if equals.</returns>
        public bool Equals(uint n) => this == n;

        /// <inheritdoc />
        /// <summary>
        /// Returns equality of this <see cref="T:HSNXT.IntX" /> with another long integer.
        /// </summary>
        /// <param name="n">Long integer to compare with.</param>
        /// <returns>True if equals.</returns>
        public bool Equals(long n) => this == n;

        /// <inheritdoc />
        /// <summary>
        /// Returns equality of this <see cref="T:HSNXT.IntX" /> with another unsigned long integer.
        /// </summary>
        /// <param name="n">Unsigned long integer to compare with.</param>
        /// <returns>True if equals.</returns>
        public bool Equals(ulong n) => this == n;


        /// <summary>
        /// Returns equality of this <see cref="IntX" /> with another object.
        /// </summary>
        /// <param name="obj">Object to compare with.</param>
        /// <returns>True if equals.</returns>
        public override bool Equals(object obj) => obj is IntX x && Equals(x);

        /// <summary>
        /// Returns hash code for this <see cref="IntX" /> object.
        /// </summary>
        /// <returns>Object hash code.</returns>
        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            switch (Length)
            {
                case 0:
                    return 0;
                case 1:
                    return (int) (Digits[0] ^ Length ^ (Negative ? 1 : 0));
                default:
                    return (int) (Digits[0] ^ Digits[Length - 1] ^ Length ^ (Negative ? 1 : 0));
            }
        }

        #endregion Equals/GetHashCode implementation/overrides

        #region IComparable implementation

        /// <inheritdoc />
        /// <summary>
        /// Compares current object with another big integer.
        /// </summary>
        /// <param name="n">Big integer to compare with.</param>
        /// <returns>1 if object is bigger than <paramref name="n" />, -1 if object is smaller than <paramref name="n" />, 0 if they are equal.</returns>
        public int CompareTo(IntX n) => OpHelper.Cmp(this, n, true);

        /// <inheritdoc />
        /// <summary>
        /// Compares current object with another integer.
        /// </summary>
        /// <param name="n">Integer to compare with.</param>
        /// <returns>1 if object is bigger than <paramref name="n" />, -1 if object is smaller than <paramref name="n" />, 0 if they are equal.</returns>
        public int CompareTo(int n) => OpHelper.Cmp(this, n);

        /// <inheritdoc />
        /// <summary>
        /// Compares current object with another unsigned integer.
        /// </summary>
        /// <param name="n">Unsigned integer to compare with.</param>
        /// <returns>1 if object is bigger than <paramref name="n" />, -1 if object is smaller than <paramref name="n" />, 0 if they are equal.</returns>
        public int CompareTo(uint n) => OpHelper.Cmp(this, n);

        /// <inheritdoc />
        /// <summary>
        /// Compares current object with another long integer.
        /// </summary>
        /// <param name="n">Long integer to compare with.</param>
        /// <returns>1 if object is bigger than <paramref name="n" />, -1 if object is smaller than <paramref name="n" />, 0 if they are equal.</returns>
        public int CompareTo(long n) => OpHelper.Cmp(this, n, true);

        /// <inheritdoc />
        /// <summary>
        /// Compares current object with another unsigned long integer.
        /// </summary>
        /// <param name="n">Unsigned long integer to compare with.</param>
        /// <returns>1 if object is bigger than <paramref name="n" />, -1 if object is smaller than <paramref name="n" />, 0 if they are equal.</returns>
        public int CompareTo(ulong n) => OpHelper.Cmp(this, n, true);

        /// <inheritdoc />
        /// <summary>
        /// Compares current object with another object.
        /// </summary>
        /// <param name="obj">Object to compare with.</param>
        /// <returns>1 if object is bigger than <paramref name="obj" />, -1 if object is smaller than <paramref name="obj" />, 0 if they are equal.</returns>
        public int CompareTo(object obj)
        {
            switch (obj)
            {
                case IntX x:
                    return CompareTo(x);
                case int i:
                    return CompareTo(i);
                case uint u:
                    return CompareTo(u);
                case long l:
                    return CompareTo(l);
                case ulong @ulong:
                    return CompareTo(@ulong);
            }

            throw new ArgumentException(Strings.CantCmp, nameof(obj));
        }

        #endregion IComparable implementation

        #region Other public methods

        /// <summary>
        /// Frees extra space not used by digits.
        /// </summary>
        public void Normalize()
        {
            if (Digits.LongLength > Length)
            {
                var newDigits = new uint[Length];
                Array.Copy(Digits, newDigits, Length);
                Digits = newDigits;
            }

            if (Length == 0)
            {
                Negative = false;
            }
        }

        /// <summary>
        /// Retrieves this <see cref="IntX" /> internal state as digits array and sign.
        /// Can be used for serialization and other purposes.
        /// Note: please use constructor instead to clone <see cref="IntX" /> object.
        /// </summary>
        /// <param name="digits">Digits array.</param>
        /// <param name="negative">Is negative integer.</param>
        public void GetInternalState(out uint[] digits, out bool negative)
        {
            digits = new uint[Length];
            Array.Copy(Digits, digits, Length);

            negative = Negative;
        }

        #endregion Other public methods

        #region Utilitary methods

        /// <summary>
        /// Initializes class instance from <see cref="UInt64" /> value.
        /// Doesn't initialize sign.
        /// For internal use.
        /// </summary>
        /// <param name="value">Unsigned long value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InitFromUlong(ulong value)
        {
            // Divide ulong into 2 uint values
            var low = (uint) value;
            var high = (uint) (value >> Constants.DigitBitCount);

            // Prepare internal fields
            Digits = high == 0 ? new[] {low} : new[] {low, high};
            Length = (uint) Digits.Length;
        }

        /// <summary>
        /// Initializes class instance from another <see cref="IntX" /> value.
        /// For internal use.
        /// </summary>
        /// <param name="value">Big integer value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InitFromIntX(IntX value)
        {
            Digits = value.Digits;
            Length = value.Length;
            Negative = value.Negative;
        }

        /// <summary>
        /// Initializes class instance from digits array.
        /// For internal use.
        /// </summary>
        /// <param name="digits">Big integer digits.</param>
        /// <param name="negative">Big integer sign.</param>
        /// <param name="length">Big integer length.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InitFromDigits(uint[] digits, bool negative, uint length)
        {
            Digits = new uint[Length = length];
            Array.Copy(digits, Digits, Math.Min((uint) digits.LongLength, length));
            if (length != 0)
            {
                Negative = negative;
            }
        }

        /// <summary>
        /// Frees extra space not used by digits only if auto-normalize is set for the instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void TryNormalize()
        {
            if (Settings.AutoNormalize)
            {
                Normalize();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InitializeZero()
        {
            // Very specific fast processing for zero values
            //Length = 0;
            Digits = new uint[0];
        }

        #endregion Utility methods
    }
}