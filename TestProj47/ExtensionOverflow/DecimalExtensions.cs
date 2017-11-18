namespace HSNXT
{
    /// <summary>
    /// Decimal Extensions
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
        public static decimal PercentageOf(this decimal number, int percent)
        {
            return number * percent / 100;
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this decimal position, int percent)
        {
            decimal result = 0;
            if (position > 0 && percent > 0)
                result = position / percent * 100;
            return result;
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this decimal number, decimal percent)
        {
            return number * percent / 100;
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this decimal position, decimal percent)
        {
            decimal result = 0;
            if (position > 0 && percent > 0)
                result = position / percent * 100;
            return result;
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this decimal number, long percent)
        {
            return number * percent / 100;
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this decimal position, long percent)
        {
            decimal result = 0;
            if (position > 0 && percent > 0)
                result = position / percent * 100;
            return result;
        }

        #endregion
    }
}