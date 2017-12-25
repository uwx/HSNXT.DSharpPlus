using System;

namespace HSNXT
{
    public partial class Extensions
    {
        /// <summary>
        /// Ensures the order where first is less than or equal to the second.
        /// </summary>
        /// <param name="first">The first number.</param>
        /// <param name="second">The second number.</param>
        /// <exception cref="System.InvalidOperationException">The second cannot be less than the first.</exception>
        public static void EnsureOrder(int first, int second)
        {
            if (second < first) throw new InvalidOperationException("The first value {0} cannot be larger than the second value {1}.".FormatWith(first, second));
        }

        /// <summary>
        /// Creates an <see cref="ExclusiveInteger"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ExclusiveInteger Exclusive(this int value)
        {
            return new ExclusiveInteger(value);
        }

        /// <summary>
        /// Creates an <see cref="InclusiveInteger"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static InclusiveInteger Inclusive(this int value)
        {
            return new InclusiveInteger(value);
        }

        /// <summary>
        /// Determines whether the value is between the specified limits.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="lowerLimit">The lower limit.</param>
        /// <param name="upperLimit">The upper limit.</param>
        /// <returns>true is value is between lower and upper limit, otherwise false.</returns>
        /// <exception cref="InvalidOperationException">When lowerLimit is greater than upperLimit.</exception>
        public static bool IsBetween(this int value, ExclusiveInteger lowerLimit, ExclusiveInteger upperLimit)
        {
            EnsureOrder(lowerLimit.Value, upperLimit.Value);
            return lowerLimit.Value < value && value < upperLimit.Value;
        }

        /// <summary>
        /// Determines whether the value is between the specified limits.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="lowerLimit">The lower limit.</param>
        /// <param name="upperLimit">The upper limit.</param>
        /// <returns>true is value is between lower (inclusive) and upper limit, otherwise false.</returns>
        /// <exception cref="InvalidOperationException">When lowerLimit is greater than upperLimit.</exception>
        public static bool IsBetween(this int value, InclusiveInteger lowerLimit, ExclusiveInteger upperLimit)
        {
            EnsureOrder(lowerLimit.Value, upperLimit.Value);
            return lowerLimit.Value <= value && value < upperLimit.Value;
        }

        /// <summary>
        /// Determines whether the value is between the specified limits.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="lowerLimit">The lower limit.</param>
        /// <param name="upperLimit">The upper limit.</param>
        /// <returns>true is value is between lower and upper limit (inclusive), otherwise false.</returns>
        /// <exception cref="InvalidOperationException">When lowerLimit is greater than upperLimit.</exception>
        public static bool IsBetween(this int value, ExclusiveInteger lowerLimit, InclusiveInteger upperLimit)
        {
            EnsureOrder(lowerLimit.Value, upperLimit.Value);
            return lowerLimit.Value < value && value <= upperLimit.Value;
        }

        /// <summary>
        /// Determines whether the value is between the specified limits.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="lowerLimit">The lower limit.</param>
        /// <param name="upperLimit">The upper limit.</param>
        /// <returns>true is value is between lower (inclusive) and upper limit (inclusive), otherwise false.</returns>
        /// <exception cref="InvalidOperationException">When lowerLimit is greater than upperLimit.</exception>
        public static bool IsBetween(this int value, InclusiveInteger lowerLimit, InclusiveInteger upperLimit)
        {
            EnsureOrder(lowerLimit.Value, upperLimit.Value);
            return lowerLimit.Value <= value && value <= upperLimit.Value;
        }

        /// <summary>
        /// Determines whether the value is between the lower and upper limit (exclusive).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="lowerLimit">The lower limit.</param>
        /// <param name="upperLimit">The upper limit.</param>
        /// <returns>true is value is between lower and upper limit, otherwise false.</returns>
        /// <exception cref="InvalidOperationException">When lowerLimit is greater than upperLimit.</exception>
        public static bool IsBetweenExclusive(this int value, int lowerLimit, int upperLimit)
        {
            EnsureOrder(lowerLimit, upperLimit);
            return lowerLimit < value && value < upperLimit;
        }

    }
}
