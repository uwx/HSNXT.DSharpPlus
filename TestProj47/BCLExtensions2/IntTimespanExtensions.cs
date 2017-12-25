using System;

namespace HSNXT
{
    public partial class Extensions
    {
        /// <summary>
        /// Determines whether the number of days is valid for time span.
        /// </summary>
        /// <param name="numberOfDays">The number of days.</param>
        /// <returns>true if number is valid, otherwise false.</returns>
        public static bool IsAValidNumberOfDaysForTimeSpan(this int numberOfDays)
        {
            return TimeSpan.MinValue.TotalDays <= numberOfDays && numberOfDays <= TimeSpan.MaxValue.TotalDays;
        }

        /// <summary>
        /// Determines whether the number of hours is valid for time span.
        /// </summary>
        /// <param name="numberOfHours">The number of hours.</param>
        /// <returns>true if number is valid, otherwise false.</returns>
        public static bool IsAValidNumberOfHoursForTimeSpan(this int numberOfHours)
        {
            return TimeSpan.MinValue.TotalHours <= numberOfHours && numberOfHours <= TimeSpan.MaxValue.TotalHours;
        }

        /// <summary>
        /// The specified number in weeks as a <see cref="TimeSpan" />.
        /// </summary>
        /// <param name="numberOfWeeks">The number of weeks.</param>
        /// <returns>The <see cref="TimeSpan" /> representation.</returns>
        public static TimeSpan Week(this int numberOfWeeks)
        {
            return new TimeSpan(numberOfWeeks*7, 0, 0, 0);
        }
    }
}
