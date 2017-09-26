﻿namespace TestProj47
{
    /// <summary>
    /// Integer Extensions
    /// </summary>
    public static partial class Extensions
    {
        #region PercentageOf calculations

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this int number, int percent)
        {
            return number * percent / 100;
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this int position, int total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = position / (decimal) total * 100;
            return result;
        }

        public static decimal PercentOf(this int? position, int total)
        {
            if (position == null) return 0;

            decimal result = 0;
            if (position > 0 && total > 0)
                result = (decimal) position / total * 100;
            return result;
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this int number, float percent)
        {
            return (decimal) (number * percent / 100);
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this int number, float percent)
        {
            decimal result = 0;
            if (number > 0 && percent > 0)
                result = number / (decimal) percent * 100;
            return result;
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this int number, double percent)
        {
            return (decimal) (number * percent / 100);
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this int position, double total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = position / (decimal) total * 100;
            return result;
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this int number, decimal percent)
        {
            return number * percent / 100;
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this int position, decimal total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = position / total * 100;
            return result;
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this int number, long percent)
        {
            return number * percent / 100;
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this int position, long total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = position / (decimal) total * 100;
            return result;
        }

        #endregion

        public static string ToString(this int? value, string defaultvalue)
        {
            if (value == null) return defaultvalue;
            return value.Value.ToString();
        }
    }
}