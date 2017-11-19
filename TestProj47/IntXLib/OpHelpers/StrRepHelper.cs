using System;
using System.Collections.Generic;
using TestProj47;

namespace HSNXT
{
    /// <summary>
    /// Helps to work with <see cref="IntX" /> string representations.
    /// </summary>
    internal static class StrRepHelper
    {
        /// <summary>
        /// Returns digit for given char.
        /// </summary>
        /// <param name="charToDigits">Char->digit dictionary.</param>
        /// <param name="ch">Char which represents big integer digit.</param>
        /// <param name="numberBase">String representation number base.</param>
        /// <returns>Digit.</returns>
        /// <exception cref="FormatException"><paramref name="ch" /> is not in valid format.</exception>
        public static uint GetDigit(IDictionary<char, uint> charToDigits, char ch, uint numberBase)
        {
            if (charToDigits == null)
            {
                throw new ArgumentNullException(nameof(charToDigits));
            }

            // Try to identify this digit
            uint digit;
            if (!charToDigits.TryGetValue(ch, out digit))
            {
                throw new FormatException(Strings.ParseInvalidChar);
            }
            if (digit >= numberBase)
            {
                throw new FormatException(Strings.ParseTooBigDigit);
            }
            return digit;
        }

        /// <summary>
        /// Verfies string alphabet provider by user for validity.
        /// </summary>
        /// <param name="alphabet">Alphabet.</param>
        /// <param name="numberBase">String representation number base.</param>
        public static void AssertAlphabet(string alphabet, uint numberBase)
        {
            if (alphabet == null)
            {
                throw new ArgumentNullException(nameof(alphabet));
            }

            // Ensure that alphabet has enough characters to represent numbers in given base
            if (alphabet.Length < numberBase)
            {
                throw new ArgumentException(string.Format(Strings.AlphabetTooSmall, numberBase), nameof(alphabet));
            }

            // Ensure that all the characters in alphabet are unique
            var sortedChars = alphabet.ToCharArray();
            Array.Sort(sortedChars);
            for (var i = 0; i < sortedChars.Length; i++)
            {
                if (i > 0 && sortedChars[i] == sortedChars[i - 1])
                {
                    throw new ArgumentException(Strings.AlphabetRepeatingChars, nameof(alphabet));
                }
            }
        }

        /// <summary>
        /// Generates char->digit dictionary from alphabet.
        /// </summary>
        /// <param name="alphabet">Alphabet.</param>
        /// <param name="numberBase">String representation number base.</param>
        /// <returns>Char->digit dictionary.</returns>
        public static IDictionary<char, uint> CharDictionaryFromAlphabet(string alphabet, uint numberBase)
        {
            AssertAlphabet(alphabet, numberBase);
            var charToDigits = new Dictionary<char, uint>((int) numberBase);
            for (var i = 0; i < numberBase; i++)
            {
                charToDigits.Add(alphabet[i], (uint) i);
            }
            return charToDigits;
        }
    }
}