using System.Collections.Generic;

namespace HSNXT
{
    /// <summary>
    /// Provides special fast (with linear time) parsing if base is pow of 2.
    /// </summary>
    internal sealed class Pow2Parser : IParser
    {
        // Not needed in this implementation
        public IntX Parse(string value, uint numberBase, IDictionary<char, uint> charToDigits, bool checkFormat)
        {
            return default;
        }

        /// <summary>
        /// Parses provided string representation of <see cref="IntX" /> object.
        /// </summary>
        /// <param name="value">Number as string.</param>
        /// <param name="startIndex">Index inside string from which to start.</param>
        /// <param name="endIndex">Index inside string on which to end.</param>
        /// <param name="numberBase">Number base.</param>
        /// <param name="charToDigits">Char->digit dictionary.</param>
        /// <param name="digitsRes">Resulting digits.</param>
        /// <returns>Parsed integer length.</returns>
        public uint Parse(string value, int startIndex, int endIndex, uint numberBase,
            IDictionary<char, uint> charToDigits, uint[] digitsRes)
        {
            // Calculate length of input string
            var bitsInChar = Bits.Msb(numberBase);
            var valueLength = (uint) (endIndex - startIndex + 1);
            var valueBitLength = valueLength * (ulong) bitsInChar;

            // Calculate needed digits length and first shift
            var digitsLength = (uint) (valueBitLength / Constants.DigitBitCount) + 1;
            var digitIndex = digitsLength - 1;
            var initialShift = (int) (valueBitLength % Constants.DigitBitCount);

            // Probably correct digits length
            if (initialShift == 0)
            {
                --digitsLength;
            }

            // Do parsing in big cycle
            uint digit;
            for (var i = startIndex; i <= endIndex; ++i)
            {
                digit = StrRepHelper.GetDigit(charToDigits, value[i], numberBase);

                // Correct initial digit shift
                if (initialShift == 0)
                {
                    // If shift is equals to zero then char is not on digit elemtns bounds,
                    // so just go to the previous digit
                    initialShift = Constants.DigitBitCount - bitsInChar;
                    --digitIndex;
                }
                else
                {
                    // Here shift might be negative, but it's okay
                    initialShift -= bitsInChar;
                }

                // Insert new digit in correct place
                digitsRes[digitIndex] |= initialShift < 0 ? digit >> -initialShift : digit << initialShift;

                // In case if shift was negative we also must modify previous digit
                if (initialShift < 0)
                {
                    initialShift += Constants.DigitBitCount;
                    digitsRes[--digitIndex] |= digit << initialShift;
                }
            }

            if (digitsRes[digitsLength - 1] == 0)
            {
                --digitsLength;
            }
            return digitsLength;
        }
    }
}