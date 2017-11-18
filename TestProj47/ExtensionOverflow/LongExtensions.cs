namespace HSNXT
{
    /// <summary>
    /// Long Extensions
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this long number, int percent)
        {
            return number * percent / 100m;
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this long number, float percent)
        {
            return (decimal) (number * percent / 100);
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this long number, double percent)
        {
            return (decimal) (number * percent / 100);
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this long number, decimal percent)
        {
            return number * percent / 100;
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this long number, long percent)
        {
            return number * percent / 100m;
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this long number, int percent)
        {
            decimal result = 0;
            if (number > 0 && percent > 0)
                result = number / (decimal) percent * 100;
            return result;
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this long number, float percent)
        {
            decimal result = 0;
            if (number > 0 && percent > 0)
                result = number / (decimal) percent * 100;
            return result;
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this long number, double percent)
        {
            decimal result = 0;
            if (number > 0 && percent > 0)
                result = number / (decimal) percent * 100;
            return result;
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this long number, decimal percent)
        {
            decimal result = 0;
            if (number > 0 && percent > 0)
                result = number / percent * 100;
            return result;
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this long number, long percent)
        {
            decimal result = 0;
            if (number > 0 && percent > 0)
                result = number / (decimal) percent * 100;
            return result;
        }
    }
}