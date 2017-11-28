using System;
using HSNXT;

namespace HSNXT
{
    /// <summary>
    /// Contains helping methods for operations over <see cref="IntX" />.
    /// </summary>
    internal static class OpHelper
    {
        #region Add operation

        /// <summary>
        /// Adds two big integers.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>Resulting big integer.</returns>
        /// <exception cref="ArgumentException"><paramref name="int1" /> or <paramref name="int2" /> is too big for add operation.</exception>
        public static IntX Add(IntX int1, IntX int2)
        {
            // Process zero values in special way
            if (int2.Length == 0) return new IntX(int1);
            if (int1.Length == 0)
            {
                var x = new IntX(int2);
                x.Negative = int1.Negative; // always get sign of the first big integer
                return x;
            }

            // Determine big int with lower length
            IntX smallerInt;
            IntX biggerInt;
            DigitHelper.GetMinMaxLengthObjects(int1, int2, out smallerInt, out biggerInt);

            // Check for add operation possibility
            if (biggerInt.Length == uint.MaxValue)
            {
                throw new ArgumentException(Strings.IntegerTooBig);
            }

            // Create new big int object of needed length
            var newInt = new IntX(biggerInt.Length + 1, int1.Negative);

            // Do actual addition
            newInt.Length = DigitOpHelper.Add(
                biggerInt.Digits,
                biggerInt.Length,
                smallerInt.Digits,
                smallerInt.Length,
                newInt.Digits);

            // Normalization may be needed
            newInt.TryNormalize();

            return newInt;
        }

        #endregion Add operation

        #region Subtract operation

        /// <summary>
        /// Subtracts two big integers.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>Resulting big integer.</returns>
        public static IntX Sub(IntX int1, IntX int2)
        {
            // Process zero values in special way
            if (int1.Length == 0) return new IntX(int2.Digits, true);
            if (int2.Length == 0) return new IntX(int1);

            // Determine lower big int (without sign)
            IntX smallerInt;
            IntX biggerInt;
            var compareResult = DigitOpHelper.Cmp(int1.Digits, int1.Length, int2.Digits, int2.Length);
            if (compareResult == 0) return new IntX(); // integers are equal
            if (compareResult < 0)
            {
                smallerInt = int1;
                biggerInt = int2;
            }
            else
            {
                smallerInt = int2;
                biggerInt = int1;
            }

            // Create new big int object
            var newInt = new IntX(biggerInt.Length, ReferenceEquals(int1, smallerInt) ^ int1.Negative);

            // Do actual subtraction
            newInt.Length = DigitOpHelper.Sub(
                biggerInt.Digits,
                biggerInt.Length,
                smallerInt.Digits,
                smallerInt.Length,
                newInt.Digits);

            // Normalization may be needed
            newInt.TryNormalize();

            return newInt;
        }

        #endregion Subtract operation

        #region Add/Subtract operation - common methods

        /// <summary>
        /// Adds/subtracts one <see cref="IntX" /> to/from another.
        /// Determines which operation to use basing on operands signs.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <param name="subtract">Was subtraction initially.</param>
        /// <returns>Add/subtract operation result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="int1" /> or <paramref name="int2" /> is a null reference.</exception>
        public static IntX AddSub(IntX int1, IntX int2, bool subtract)
        {
            // Exceptions
            if (ReferenceEquals(int1, null))
            {
                throw new ArgumentNullException(nameof(int1), Strings.CantBeNull);
            }
            if (ReferenceEquals(int2, null))
            {
                throw new ArgumentNullException(nameof(int2), Strings.CantBeNull);
            }

            // Determine real operation type and result sign
            return subtract ^ int1.Negative == int2.Negative ? Add(int1, int2) : Sub(int1, int2);
        }

        #endregion Add/Subtract operation - common methods

        #region Power operation

        /// <summary>
        /// Returns a specified big integer raised to the specified power.
        /// </summary>
        /// <param name="value">Number to raise.</param>
        /// <param name="power">Power.</param>
        /// <param name="multiplyMode">Multiply mode set explicitly.</param>
        /// <returns>Number in given power.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value" /> is a null reference.</exception>
        public static IntX Pow(IntX value, uint power, MultiplyMode multiplyMode)
        {
            // Exception
            if (ReferenceEquals(value, null))
            {
                throw new ArgumentNullException(nameof(value));
            }

            // Return one for zero pow
            if (power == 0) return 1;

            // Return the number itself from a power of one
            if (power == 1) return new IntX(value);

            // Return zero for a zero
            if (value.Length == 0) return new IntX();

            // Get first one bit
            var msb = Bits.Msb(power);

            // Get multiplier
            var multiplier = MultiplyManager.GetMultiplier(multiplyMode);

            // Do actual raising
            var res = value;
            for (var powerMask = 1U << (msb - 1); powerMask != 0; powerMask >>= 1)
            {
                // Always square
                res = multiplier.Multiply(res, res);

                // Maybe mul
                if ((power & powerMask) != 0)
                {
                    res = multiplier.Multiply(res, value);
                }
            }
            return res;
        }

        #endregion Power operation

        #region Compare operation

        /// <summary>
        /// Compares 2 <see cref="IntX" /> objects.
        /// Returns "-2" if any argument is null, "-1" if <paramref name="int1" /> &lt; <paramref name="int2" />,
        /// "0" if equal and "1" if &gt;.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <param name="throwNullException">Raises or not <see cref="NullReferenceException" />.</param>
        /// <returns>Comparsion result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="int1" /> or <paramref name="int2" /> is a null reference and <paramref name="throwNullException" /> is set to true.</exception>
        public static int Cmp(IntX int1, IntX int2, bool throwNullException)
        {
            // If one of the operands is null, throw exception or return -2
            var isNull1 = ReferenceEquals(int1, null);
            var isNull2 = ReferenceEquals(int2, null);
            if (isNull1 || isNull2)
            {
                if (throwNullException)
                {
                    throw new ArgumentNullException(isNull1 ? "int1" : "int2", Strings.CantBeNullCmp);
                }
                return isNull1 && isNull2 ? 0 : -2;
            }

            // Compare sign
            if (int1.Negative && !int2.Negative) return -1;
            if (!int1.Negative && int2.Negative) return 1;

            // Compare presentation
            return DigitOpHelper.Cmp(int1.Digits, int1.Length, int2.Digits, int2.Length) * (int1.Negative ? -1 : 1);
        }

        /// <summary>
        /// Compares <see cref="IntX" /> object to int.
        /// Returns "-1" if <paramref name="int1" /> &lt; <paramref name="int2" />, "0" if equal and "1" if &gt;.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second integer.</param>
        /// <returns>Comparsion result.</returns>
        public static int Cmp(IntX int1, int int2)
        {
            // Special processing for zero
            if (int2 == 0) return int1.Length == 0 ? 0 : (int1.Negative ? -1 : 1);
            if (int1.Length == 0) return int2 > 0 ? -1 : 1;

            // Compare presentation
            if (int1.Length > 1) return int1.Negative ? -1 : 1;
            uint digit2;
            bool negative2;
            DigitHelper.ToUInt32WithSign(int2, out digit2, out negative2);

            // Compare sign
            if (int1.Negative && !negative2) return -1;
            if (!int1.Negative && negative2) return 1;

            return int1.Digits[0] == digit2 ? 0 : (int1.Digits[0] < digit2 ^ negative2 ? -1 : 1);
        }

        /// <summary>
        /// Compares <see cref="IntX" /> object to unsigned int.
        /// Returns "-1" if <paramref name="int1" /> &lt; <paramref name="int2" />, "0" if equal and "1" if &gt;.
        /// For internal use.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second unsigned integer.</param>
        /// <returns>Comparsion result.</returns>
        public static int Cmp(IntX int1, uint int2)
        {
            // Special processing for zero
            if (int2 == 0) return int1.Length == 0 ? 0 : (int1.Negative ? -1 : 1);
            if (int1.Length == 0) return -1;

            // Compare presentation
            if (int1.Negative) return -1;
            if (int1.Length > 1) return 1;
            return int1.Digits[0] == int2 ? 0 : (int1.Digits[0] < int2 ? -1 : 1);
        }

        #endregion Compare operation

        #region Shift operation

        /// <summary>
        /// Shifts <see cref="IntX" /> object.
        /// Determines which operation to use basing on shift sign.
        /// </summary>
        /// <param name="intX">Big integer.</param>
        /// <param name="shift">Bits count to shift.</param>
        /// <param name="toLeft">If true the shifting to the left.</param>
        /// <returns>Bitwise shift operation result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="intX" /> is a null reference.</exception>
        public static IntX Sh(IntX intX, long shift, bool toLeft)
        {
            // Exceptions
            if (ReferenceEquals(intX, null))
            {
                throw new ArgumentNullException(nameof(intX), Strings.CantBeNullOne);
            }

            // Zero can't be shifted
            if (intX.Length == 0) return new IntX();

            // Can't shift on zero value
            if (shift == 0) return new IntX(intX);

            // Determine real bits count and direction
            ulong bitCount;
            bool negativeShift;
            DigitHelper.ToUInt64WithSign(shift, out bitCount, out negativeShift);
            toLeft ^= negativeShift;

            // Get position of the most significant bit in intX and amount of bits in intX
            var msb = Bits.Msb(intX.Digits[intX.Length - 1]);
            var intXBitCount = (ulong) (intX.Length - 1) * Constants.DigitBitCount + (ulong) msb + 1UL;

            // If shifting to the right and shift is too big then return zero
            if (!toLeft && bitCount >= intXBitCount) return new IntX();

            // Calculate new bit count
            var newBitCount = toLeft ? intXBitCount + bitCount : intXBitCount - bitCount;

            // If shifting to the left and shift is too big to fit in big integer, throw an exception
            if (toLeft && newBitCount > Constants.MaxBitCount)
            {
                throw new ArgumentException(Strings.IntegerTooBig, nameof(intX));
            }

            // Get exact length of new big integer (no normalize is ever needed here).
            // Create new big integer with given length
            var newLength = (uint) (newBitCount / Constants.DigitBitCount +
                                    (newBitCount % Constants.DigitBitCount == 0 ? 0UL : 1UL));
            var newInt = new IntX(newLength, intX.Negative);

            // Get full and small shift values
            var fullDigits = (uint) (bitCount / Constants.DigitBitCount);
            var smallShift = (int) (bitCount % Constants.DigitBitCount);

            // We can just copy (no shift) if small shift is zero
            if (smallShift == 0)
            {
                if (toLeft)
                {
                    Array.Copy(intX.Digits, 0, newInt.Digits, fullDigits, intX.Length);
                }
                else
                {
                    Array.Copy(intX.Digits, fullDigits, newInt.Digits, 0, newLength);
                }
            }
            else
            {
                // Do copy with real shift in the needed direction
                if (toLeft)
                {
                    DigitOpHelper.Shr(intX.Digits, 0, intX.Length, newInt.Digits, fullDigits + 1,
                        Constants.DigitBitCount - smallShift);
                }
                else
                {
                    // If new result length is smaller then original length we shouldn't lose any digits
                    if (newLength < intX.Length - fullDigits)
                    {
                        newLength++;
                    }

                    DigitOpHelper.Shr(intX.Digits, fullDigits, newLength, newInt.Digits, 0, smallShift);
                }
            }

            return newInt;
        }

        #endregion Shift operation

        #region Bitwise operations

        /// <summary>
        /// Performs bitwise OR for two big integers.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>Resulting big integer.</returns>
        public static IntX BitwiseOr(IntX int1, IntX int2)
        {
            // Exceptions
            if (ReferenceEquals(int1, null))
            {
                throw new ArgumentNullException(nameof(int1), Strings.CantBeNull);
            }
            if (ReferenceEquals(int2, null))
            {
                throw new ArgumentNullException(nameof(int2), Strings.CantBeNull);
            }

            // Process zero values in special way
            if (int1.Length == 0)
            {
                return new IntX(int2);
            }
            if (int2.Length == 0)
            {
                return new IntX(int1);
            }

            // Determine big int with lower length
            IntX smallerInt;
            IntX biggerInt;
            DigitHelper.GetMinMaxLengthObjects(int1, int2, out smallerInt, out biggerInt);

            // Create new big int object of needed length
            var newInt = new IntX(biggerInt.Length, int1.Negative | int2.Negative);

            // Do actual operation
            DigitOpHelper.BitwiseOr(
                biggerInt.Digits,
                biggerInt.Length,
                smallerInt.Digits,
                smallerInt.Length,
                newInt.Digits);

            return newInt;
        }

        /// <summary>
        /// Performs bitwise AND for two big integers.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>Resulting big integer.</returns>
        public static IntX BitwiseAnd(IntX int1, IntX int2)
        {
            // Exceptions
            if (ReferenceEquals(int1, null))
            {
                throw new ArgumentNullException(nameof(int1), Strings.CantBeNull);
            }
            if (ReferenceEquals(int2, null))
            {
                throw new ArgumentNullException(nameof(int2), Strings.CantBeNull);
            }

            // Process zero values in special way
            if (int1.Length == 0 || int2.Length == 0)
            {
                return new IntX();
            }

            // Determine big int with lower length
            IntX smallerInt;
            IntX biggerInt;
            DigitHelper.GetMinMaxLengthObjects(int1, int2, out smallerInt, out biggerInt);

            // Create new big int object of needed length
            var newInt = new IntX(smallerInt.Length, int1.Negative & int2.Negative);

            // Do actual operation
            newInt.Length = DigitOpHelper.BitwiseAnd(
                biggerInt.Digits,
                smallerInt.Digits,
                smallerInt.Length,
                newInt.Digits);

            // Normalization may be needed
            newInt.TryNormalize();

            return newInt;
        }

        /// <summary>
        /// Performs bitwise XOR for two big integers.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <returns>Resulting big integer.</returns>
        public static IntX ExclusiveOr(IntX int1, IntX int2)
        {
            // Exceptions
            if (ReferenceEquals(int1, null))
            {
                throw new ArgumentNullException(nameof(int1), Strings.CantBeNull);
            }
            if (ReferenceEquals(int2, null))
            {
                throw new ArgumentNullException(nameof(int2), Strings.CantBeNull);
            }

            // Process zero values in special way
            if (int1.Length == 0)
            {
                return new IntX(int2);
            }
            if (int2.Length == 0)
            {
                return new IntX(int1);
            }

            // Determine big int with lower length
            IntX smallerInt;
            IntX biggerInt;
            DigitHelper.GetMinMaxLengthObjects(int1, int2, out smallerInt, out biggerInt);

            // Create new big int object of needed length
            var newInt = new IntX(biggerInt.Length, int1.Negative ^ int2.Negative);

            // Do actual operation
            newInt.Length = DigitOpHelper.ExclusiveOr(
                biggerInt.Digits,
                biggerInt.Length,
                smallerInt.Digits,
                smallerInt.Length,
                newInt.Digits);

            // Normalization may be needed
            newInt.TryNormalize();

            return newInt;
        }

        /// <summary>
        /// Performs bitwise NOT for big integer.
        /// </summary>
        /// <param name="value">Big integer.</param>
        /// <returns>Resulting big integer.</returns>
        public static IntX OnesComplement(IntX value)
        {
            // Exceptions
            if (ReferenceEquals(value, null))
            {
                throw new ArgumentNullException(nameof(value), Strings.CantBeNull);
            }

            // Process zero values in special way
            if (value.Length == 0)
            {
                return new IntX();
            }

            // Create new big int object of needed length
            var newInt = new IntX(value.Length, !value.Negative);

            // Do actual operation
            newInt.Length = DigitOpHelper.OnesComplement(
                value.Digits,
                value.Length,
                newInt.Digits);

            // Normalization may be needed
            newInt.TryNormalize();

            return newInt;
        }

        #endregion Bitwise operations
    }
}