namespace HSNXT
{
    /// <summary>
    /// Double Extensions
    /// </summary>
    public static partial class Extensions
    {
        #region PercentageOf calculations

        public static decimal PercentageOf(this double number, int percent)
        {
            return (decimal) (number * percent / 100);
        }

        public static decimal PercentageOf(this double number, float percent)
        {
            return (decimal) (number * percent / 100);
        }

        public static decimal PercentageOf(this double number, double percent)
        {
            return (decimal) (number * percent / 100);
        }

        public static decimal PercentageOf(this double number, long percent)
        {
            return (decimal) (number * percent / 100);
        }

        public static decimal PercentOf(this double position, int percent)
        {
            decimal result = 0;
            if (position > 0 && percent > 0)
                result = (decimal) position / percent * 100;
            return result;
        }

        public static decimal PercentOf(this double position, float percent)
        {
            decimal result = 0;
            if (position > 0 && percent > 0)
                result = (decimal) position / (decimal) percent * 100;
            return result;
        }

        public static decimal PercentOf(this double position, double percent)
        {
            decimal result = 0;
            if (position > 0 && percent > 0)
                result = (decimal) position / (decimal) percent * 100;
            return result;
        }

        public static decimal PercentOf(this double position, long percent)
        {
            decimal result = 0;
            if (position > 0 && percent > 0)
                result = (decimal) position / percent * 100;
            return result;
        }

        #endregion
    }
}