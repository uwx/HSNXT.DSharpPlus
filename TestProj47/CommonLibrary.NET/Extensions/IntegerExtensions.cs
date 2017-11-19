using System;
using HSNXT.ComLib;

namespace HSNXT
{
    public static partial class Extensions
    {
        #region Loops
        /// <summary>
        /// Iterates the action using a for loop from 0 to ndx.
        /// </summary>
        /// <param name="ndx">The number of times to iterate ( 0 based )</param>
        /// <param name="action">The action to call</param>
        public static void Times(this int ndx, Action<int> action)
        {
            for (var i = 0; i < ndx; i++)
            {
                action(i);
            }
        }


        /// <summary>
        /// Iterates over the action from the start to the end non-inclusive.
        /// </summary>
        /// <param name="start">The starting number.</param>
        /// <param name="end">The ending number ( non-inclusive )</param>
        /// <param name="action">Action to call.</param>
        public static void Upto(this int start, int end, Action<int> action)
        {
            for (var i = start; i <= end; i++)
            {
                action(i);
            }
        }


        /// <summary>
        /// Iterates over the action from start to end including the end number.
        /// </summary>
        /// <param name="start">The starting number.</param>
        /// <param name="end">The ending number ( inclusive )</param>
        /// <param name="action">Action to call.</param>
        public static void UptoIncluding(this int start, int end, Action<int> action)
        {
            for (var i = start; i <= end; i++)
            {
                action(i);
            }
        }


        /// <summary>
        /// Iterates over the action from the end to the start non-inclusive.
        /// </summary>
        /// <param name="start">The starting number  ( non-inclusive ).</param>
        /// <param name="end">The ending number</param>
        /// <param name="action">Action to call.</param>
        public static void Downto(this int end, int start, Action<int> action)
        {
            for (var i = end; i > start; i--)
            {
                action(i);
            }            
        }


        /// <summary>
        /// Iterates over the action from the end to the start inclusive.
        /// </summary>
        /// <param name="start">The starting number  ( inclusive ).</param>
        /// <param name="end">The ending number</param>
        /// <param name="action">Action to call.</param>
        public static void DowntoIncluding(this int end, int start, Action<int> action)
        {
            for (var i = end; i >= start; i--)
            {
                action(i);
            }
        }
        #endregion

        #region Bytes
        /// <summary>
        /// Converts the number provided into kilobytes by dividing it by 1000;
        /// </summary>
        /// <param name="numberInBytes">Number in bytes</param>
        /// <returns></returns>
        public static int MegaBytes(this int numberInBytes)
        {
            return numberInBytes / 1000000;
        }


        /// <summary>
        /// Converts the number provided into kilobytes by dividing it by 1000;
        /// </summary>
        /// <param name="numberInBytes">Number in bytes</param>
        /// <returns></returns>
        public static int KiloBytes(this int numberInBytes)
        {
            return numberInBytes / 1000;
        }


        /// <summary>
        /// Converts the number provided into kilobytes by dividing it by 1000;
        /// </summary>
        /// <param name="numberInBytes">Number in bytes</param>
        /// <returns></returns>
        public static int TeraBytes(this int numberInBytes)
        {
            return numberInBytes / 1000000000;
        }
        #endregion


        #region Dates

        /// <summary>
        /// Gets the datetime from today - days supplied.
        /// </summary>
        /// <param name="days">Number of days ago</param>
        /// <returns></returns>
        public static DateTime DaysAgo(this int days)
        {
            return DateTime.Now.AddDays(-days);
        }


        /// <summary>
        /// Gets the datetime from today - months supplied.
        /// </summary>
        /// <param name="months">Number of months ago</param>
        /// <returns></returns>
        public static DateTime MonthsAgo(this int months)
        {
            return DateTime.Now.AddMonths(-months);
        }


        /// <summary>
        /// Gets the datetime from today - years supplied.
        /// </summary>
        /// <param name="years">Number of years ago</param>
        /// <returns></returns>
        public static DateTime YearsAgo(this int years)
        {
            return DateTime.Now.AddYears(-years);
        }


        /// <summary>
        /// Gets the datetime from now - hours supplied
        /// </summary>
        /// <param name="hours">Number of hours ago</param>
        /// <returns></returns>
        public static DateTime HoursAgo(this int hours)
        {
            return DateTime.Now.AddHours(-hours);
        }


        /// <summary>
        /// Gets the datetime from now - minutes supplied
        /// </summary>
        /// <param name="minutes">Number of minutes ago</param>
        /// <returns></returns>
        public static DateTime MinutesAgo(this int minutes)
        {
            return DateTime.Now.AddMinutes(-minutes);
        }


        /// <summary>
        /// Gets the datetime from now + days supplied
        /// </summary>
        /// <param name="days">Number of days from now</param>
        /// <returns></returns>
        public static DateTime DaysFromNow(this int days)
        {
            return DateTime.Now.AddDays(days);
        }


        /// <summary>
        /// Gets the datetime from now + months supplied
        /// </summary>
        /// <param name="months">Number of months from now</param>
        /// <returns></returns>
        public static DateTime MonthsFromNow(this int months)
        {
            return DateTime.Now.AddMonths(months);
        }


        /// <summary>
        /// Gets the datetime from now + years supplied
        /// </summary>
        /// <param name="years">Number of years from now</param>
        /// <returns></returns>
        public static DateTime YearsFromNow(this int years)
        {
            return DateTime.Now.AddYears(years);
        }


        /// <summary>
        /// Gets the datetime from now + hours supplied
        /// </summary>
        /// <param name="hours">Number of hourse from now</param>
        /// <returns></returns>
        public static DateTime HoursFromNow(this int hours)
        {
            return DateTime.Now.AddHours(hours);
        }


        /// <summary>
        /// Gets the datetime from now + minutes supplied
        /// </summary>
        /// <param name="minutes">Number of minutes from now</param>
        /// <returns></returns>
        public static DateTime MinutesFromNow(this int minutes)
        {
            return DateTime.Now.AddMinutes(minutes);
        }
        #endregion


        #region Time

        /// <summary>
        /// Converts the number to a TimeSpan.
        /// </summary>
        /// <param name="num">Number reprsenting a timespan</param>
        /// <returns></returns>
        public static TimeSpan Time(this int num)
        {
            return Time(num, false);
        }



        /// <summary>
        /// Converts the military time to a timespan.
        /// </summary>
        /// <param name="num"></param>
        /// <param name="convertSingleDigitsToHours">Indicates whether to treat "9" as 9 hours instead of minutes.</param>
        /// <returns></returns>
        public static TimeSpan Time(this int num, bool convertSingleDigitsToHours)
        {
            var time = TimeSpan.MinValue;
            if (convertSingleDigitsToHours)
            {
                if (num <= 24)
                    num *= 100;
            }
            var hours = num / 100;
            var hour = hours;
            var minutes = num % 100;

            time = new TimeSpan(hours, minutes, 0);
            return time;
        }


        /// <summary>
        /// Returns military time formatted as standard time w/ suffix am/pm.
        /// e.g. 1am 9:30pm
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string TimeWithSuffix(this int num)
        {
            var time = Time(num, true);
            return TimeHelper.Format(time);
        }
        #endregion


        #region Hexadecimal and binary 
        /// <summary>
        /// Returns a hexadecimal string representation of the number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToHex(this int number)
        {
            return Convert.ToString(number, 16);
        }


        /// <summary>
        /// Returns a binary string representation of the number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToBinary(this int number)
        {
            return Convert.ToString(number, 2);
        }
        #endregion
    }
}
