namespace TestProj47
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
			return number * percent / 100;
		}

		/// <summary>
		/// The numbers percentage
		/// </summary>
		/// <param name="number">The number.</param>
		/// <param name="percent">The percent.</param>
		/// <returns>The result</returns>
		public static decimal PercentageOf(this long number, float percent)
		{
			return (decimal)(number * percent / 100);
		}

		/// <summary>
		/// The numbers percentage
		/// </summary>
		/// <param name="number">The number.</param>
		/// <param name="percent">The percent.</param>
		/// <returns>The result</returns>
		public static decimal PercentageOf(this long number, double percent)
		{
			return (decimal)(number * percent / 100);
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
			return number * percent / 100;
		}

		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="percent">The percent</param>
		/// <param name="number">The Number</param>
		/// <returns>The result</returns>
		public static decimal PercentOf(this long position, int total)
		{
            decimal result = 0;
            if (position > 0 && total > 0)
                result = position / (decimal)total * 100;
            return result;
		}

		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="percent">The percent</param>
		/// <param name="number">The Number</param>
		/// <returns>The result</returns>
		public static decimal PercentOf(this long position, float total)
		{
            decimal result = 0;
            if (position > 0 && total > 0)
                result = position / (decimal)total * 100;
            return result;
		}

		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="percent">The percent</param>
		/// <param name="number">The Number</param>
		/// <returns>The result</returns>
		public static decimal PercentOf(this long position, double total)
		{
            decimal result = 0;
            if (position > 0 && total > 0)
                result = position / (decimal)total * 100;
            return result;
		}

		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="percent">The percent</param>
		/// <param name="number">The Number</param>
		/// <returns>The result</returns>
		public static decimal PercentOf(this long position, decimal total)
		{
            decimal result = 0;
            if (position > 0 && total > 0)
                result = position / total * 100;
            return result;
		}

		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="percent">The percent</param>
		/// <param name="number">The Number</param>
		/// <returns>The result</returns>
		public static decimal PercentOf(this long position, long total)
		{
            decimal result = 0;
            if (position > 0 && total > 0)
                result = position / (decimal)total * 100;
            return result;
		}
	}
}
