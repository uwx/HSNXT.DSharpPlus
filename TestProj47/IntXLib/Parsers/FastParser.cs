using System;
using System.Collections.Generic;

namespace HSNXT
{
    /// <summary>
    /// Fast parsing algorithm using divide-by-two (O[n*{log n}^2]).
    /// </summary>
    internal sealed class FastParser : ParserBase
    {
        #region Private fields

        private readonly IParser _classicParser; // classic parser

        #endregion Private fields

        #region Constructor

        /// <inheritdoc />
        /// <summary>
        /// Creates new <see cref="T:HSNXT.FastParser" /> instance.
        /// </summary>
        /// <param name="pow2Parser">Parser for pow2 case.</param>
        /// <param name="classicParser">Classic parser.</param>
        public FastParser(IParser pow2Parser, IParser classicParser) : base(pow2Parser)
        {
            _classicParser = classicParser;
        }

        #endregion Constructor

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
        public override unsafe uint Parse(string value, int startIndex, int endIndex, uint numberBase,
            IDictionary<char, uint> charToDigits, uint[] digitsRes)
        {
            var newLength = base.Parse(value, startIndex, endIndex, numberBase, charToDigits, digitsRes);

            // Maybe base method already parsed this number
            if (newLength != 0) return newLength;

            // Check length - maybe use classic parser instead
            var initialLength = (uint) digitsRes.LongLength;
            if (initialLength < Constants.FastParseLengthLowerBound ||
                initialLength > Constants.FastParseLengthUpperBound)
            {
                return _classicParser.Parse(value, startIndex, endIndex, numberBase, charToDigits, digitsRes);
            }

            var valueLength = (uint) (endIndex - startIndex + 1);
            var digitsLength = 1U << Bits.CeilLog2(valueLength);

            // Prepare array for digits in other base
            var valueDigits = ArrayPool<uint>.Instance.GetArray(digitsLength);

            // This second array will store integer lengths initially
            var valueDigits2 = ArrayPool<uint>.Instance.GetArray(digitsLength);

            fixed (uint* valueDigitsStartPtr = valueDigits, valueDigitsStartPtr2 = valueDigits2)
            {
                // In the string first digit means last in digits array
                var valueDigitsPtr = valueDigitsStartPtr + valueLength - 1;
                var valueDigitsPtr2 = valueDigitsStartPtr2 + valueLength - 1;

                // Reverse copy characters into digits
                fixed (char* valueStartPtr = value)
                {
                    var valuePtr = valueStartPtr + startIndex;
                    var valueEndPtr = valuePtr + valueLength;
                    for (; valuePtr < valueEndPtr; ++valuePtr, --valueDigitsPtr, --valueDigitsPtr2)
                    {
                        // Get digit itself - this call will throw an exception if char is invalid
                        *valueDigitsPtr = StrRepHelper.GetDigit(charToDigits, *valuePtr, numberBase);

                        // Set length of this digit (zero for zero)
                        *valueDigitsPtr2 = *valueDigitsPtr == 0U ? 0U : 1U;
                    }
                }

                // We have retrieved lengths array from pool - it needs to be cleared before using
                DigitHelper.SetBlockDigits(valueDigitsStartPtr2 + valueLength, digitsLength - valueLength, 0);

                // Now start from the digit arrays beginning
                valueDigitsPtr = valueDigitsStartPtr;
                valueDigitsPtr2 = valueDigitsStartPtr2;

                // Current multiplier (classic or fast) will be used
                var multiplier = MultiplyManager.GetCurrentMultiplier();

                // Here base in needed power will be stored
                IntX baseInt = default;

                // Temporary variables used on swapping

                // Variables used in cycle

                // Outer cycle instead of recursion
                for (uint innerStep = 1, outerStep = 2; innerStep < digitsLength; innerStep <<= 1, outerStep <<= 1)
                {
                    // Maybe baseInt must be multiplied by itself
                    baseInt = baseInt == default ? numberBase : baseInt * baseInt;

                    // Using unsafe here
                    fixed (uint* baseDigitsPtr = baseInt.Digits)
                    {
                        // Start from arrays beginning
                        var ptr1 = valueDigitsPtr;
                        var ptr2 = valueDigitsPtr2;

                        // vauleDigits array end
                        var valueDigitsPtrEnd = valueDigitsPtr + digitsLength;

                        // Cycle thru all digits and their lengths
                        for (; ptr1 < valueDigitsPtrEnd; ptr1 += outerStep, ptr2 += outerStep)
                        {
                            // Get lengths of "lower" and "higher" value parts
                            var loLength = *ptr2;
                            var hiLength = *(ptr2 + innerStep);

                            if (hiLength != 0)
                            {
                                // We always must clear an array before multiply
                                DigitHelper.SetBlockDigits(ptr2, outerStep, 0U);

                                // Multiply per baseInt
                                hiLength = multiplier.Multiply(
                                    baseDigitsPtr,
                                    baseInt.Length,
                                    ptr1 + innerStep,
                                    hiLength,
                                    ptr2);
                            }

                            // Sum results
                            if (hiLength != 0 || loLength != 0)
                            {
                                *ptr1 = DigitOpHelper.Add(
                                    ptr2,
                                    hiLength,
                                    ptr1,
                                    loLength,
                                    ptr2);
                            }
                            else
                            {
                                *ptr1 = 0U;
                            }
                        }
                    }

                    // After inner cycle valueDigits will contain lengths and valueDigits2 will contain actual values
                    // so we need to swap them here
                    var tempDigits = valueDigits;
                    valueDigits = valueDigits2;
                    valueDigits2 = tempDigits;

                    var tempPtr = valueDigitsPtr;
                    valueDigitsPtr = valueDigitsPtr2;
                    valueDigitsPtr2 = tempPtr;
                }
            }

            // Determine real length of converted number
            var realLength = valueDigits2[0];

            // Copy to result
            Array.Copy(valueDigits, digitsRes, realLength);

            // Return arrays to pool
            ArrayPool<uint>.Instance.AddArray(valueDigits);
            ArrayPool<uint>.Instance.AddArray(valueDigits2);

            return realLength;
        }
    }
}