namespace TestProj47
{
    /// <summary>
    /// Contains big integer uint[] digits utilitary methods.
    /// </summary>
    internal static class DigitHelper
    {
        #region Working with digits length methods

        /// <summary>
        /// Returns real length of digits array (excluding leading zeroes).
        /// </summary>
        /// <param name="digits">Big ingeter digits.</param>
        /// <param name="length">Initial big integers length.</param>
        /// <returns>Real length.</returns>
        public static uint GetRealDigitsLength(uint[] digits, uint length)
        {
            for (; length > 0 && digits[length - 1] == 0; --length)
            {
            }
            return length;
        }

        /// <summary>
        /// Returns real length of digits array (excluding leading zeroes).
        /// </summary>
        /// <param name="digits">Big ingeter digits.</param>
        /// <param name="length">Initial big integers length.</param>
        /// <returns>Real length.</returns>
        public static unsafe uint GetRealDigitsLength(uint* digits, uint length)
        {
            for (; length > 0 && digits[length - 1] == 0; --length)
            {
            }
            return length;
        }

        /// <summary>
        /// Determines <see cref="IntX" /> object with lower length.
        /// </summary>
        /// <param name="int1">First big integer.</param>
        /// <param name="int2">Second big integer.</param>
        /// <param name="smallerInt">Resulting smaller big integer (by length only).</param>
        /// <param name="biggerInt">Resulting bigger big integer (by length only).</param>
        public static void GetMinMaxLengthObjects(IntX int1, IntX int2, out IntX smallerInt, out IntX biggerInt)
        {
            if (int1.Length < int2.Length)
            {
                smallerInt = int1;
                biggerInt = int2;
            }
            else
            {
                smallerInt = int2;
                biggerInt = int1;
            }
        }

        #endregion Working with digits length methods

        #region Signed to unsigned+sign conversion methods

        /// <summary>
        /// Converts int value to uint digit and value sign.
        /// </summary>
        /// <param name="value">Initial value.</param>
        /// <param name="resultValue">Resulting unsigned part.</param>
        /// <param name="negative">Resulting sign.</param>
        public static void ToUInt32WithSign(int value, out uint resultValue, out bool negative)
        {
            negative = value < 0;
            resultValue = !negative
                ? (uint) value
                : value != int.MinValue
                    ? (uint) -value
                    : int.MaxValue + 1U;
        }

        /// <summary>
        /// Converts long value to ulong digit and value sign.
        /// </summary>
        /// <param name="value">Initial value.</param>
        /// <param name="resultValue">Resulting unsigned part.</param>
        /// <param name="negative">Resulting sign.</param>
        public static void ToUInt64WithSign(long value, out ulong resultValue, out bool negative)
        {
            negative = value < 0;
            resultValue = !negative
                ? (ulong) value
                : value != long.MinValue
                    ? (ulong) -value
                    : long.MaxValue + 1UL;
        }

        #endregion Signed to unsigned+sign conversion methods

        #region Working with digits directly methods

        /// <summary>
        /// Sets digits in given block to given value.
        /// </summary>
        /// <param name="block">Block start pointer.</param>
        /// <param name="blockLength">Block length.</param>
        /// <param name="value">Value to set.</param>
        public static unsafe void SetBlockDigits(uint* block, uint blockLength, uint value)
        {
            for (var blockEnd = block + blockLength; block < blockEnd; *block++ = value)
            {
            }
        }

        /// <summary>
        /// Sets digits in given block to given value.
        /// </summary>
        /// <param name="block">Block start pointer.</param>
        /// <param name="blockLength">Block length.</param>
        /// <param name="value">Value to set.</param>
        public static unsafe void SetBlockDigits(double* block, uint blockLength, double value)
        {
            for (var blockEnd = block + blockLength; block < blockEnd; *block++ = value)
            {
            }
        }

        /// <summary>
        /// Copies digits from one block to another.
        /// </summary>
        /// <param name="blockFrom">From block start pointer.</param>
        /// <param name="blockTo">To block start pointer.</param>
        /// <param name="count">Count of dwords to copy.</param>
        public static unsafe void DigitsBlockCopy(uint* blockFrom, uint* blockTo, uint count)
        {
            for (var blockFromEnd = blockFrom + count; blockFrom < blockFromEnd; *blockTo++ = *blockFrom++)
            {
            }
        }

        #endregion Working with digits directly methods
    }
}