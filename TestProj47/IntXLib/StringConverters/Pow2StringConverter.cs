namespace TestProj47
{
    /// <summary>
    /// Provides special fast (with linear time) ToString converting if base is pow of 2.
    /// </summary>
    internal sealed class Pow2StringConverter : IStringConverter
    {
        // Not needed in this implementation
        public string ToString(IntX intX, uint numberBase, char[] alphabet)
        {
            return null;
        }

        /// <summary>
        /// Converts digits from internal representaion into given base.
        /// </summary>
        /// <param name="digits">Big integer digits.</param>
        /// <param name="length">Big integer length.</param>
        /// <param name="numberBase">Base to use for output.</param>
        /// <param name="outputLength">Calculated output length (will be corrected inside).</param>
        /// <returns>Conversion result (later will be transformed to string).</returns>
        public uint[] ToString(uint[] digits, uint length, uint numberBase, ref uint outputLength)
        {
            // Calculate real output length
            var bitsInChar = Bits.Msb(numberBase);
            var digitsBitLength = (ulong) (length - 1) * Constants.DigitBitCount +
                                  (ulong) Bits.Msb(digits[length - 1]) + 1UL;
            var realOutputLength = (uint) (digitsBitLength / (ulong) bitsInChar);
            if (digitsBitLength % (ulong) bitsInChar != 0)
            {
                ++realOutputLength;
            }

            // Prepare shift variables
            var nextDigitShift = Constants.DigitBitCount - bitsInChar; // after this shift next digit must be used
            var initialShift = 0;

            // We will also need bitmask for copying digits
            var digitBitMask = numberBase - 1;

            // Create an output array for storing of number in other base
            var outputArray = new uint[realOutputLength];

            // Walk thru original digits and fill output
            uint outputDigit;
            for (uint outputIndex = 0, digitIndex = 0; outputIndex < realOutputLength; ++outputIndex)
            {
                // Get part of current digit
                outputDigit = digits[digitIndex] >> initialShift;

                // Maybe we need to go to the next digit
                if (initialShift >= nextDigitShift)
                {
                    // Go to the next digit
                    ++digitIndex;

                    // Maybe we also need a part of the next digit
                    if (initialShift != nextDigitShift && digitIndex < length)
                    {
                        outputDigit |= digits[digitIndex] << (Constants.DigitBitCount - initialShift);
                    }

                    // Modify shift so that it will be valid for the next digit
                    initialShift = (initialShift + bitsInChar) % Constants.DigitBitCount;
                }
                else
                {
                    // Modify shift as usual
                    initialShift += bitsInChar;
                }

                // Write masked result to the output
                outputArray[outputIndex] = outputDigit & digitBitMask;
            }

            outputLength = realOutputLength;
            return outputArray;
        }
    }
}