﻿using System;
using System.Globalization;

namespace TestProj47
{
    /// <summary>
    /// DateTime Extensions
    /// </summary>
    public static partial class Extensions
    {
        #region Elapsed extension

        /// <summary>
        /// Elapseds the time.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns>TimeSpan</returns>
        public static TimeSpan Elapsed(this DateTime datetime)
        {
            return DateTime.Now - datetime;
        }

        #endregion

        #region Week of year

        /// <summary>
        /// Weeks the of year.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="weekrule">The weekrule.</param>
        /// <param name="firstDayOfWeek">The first day of week.</param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime datetime, CalendarWeekRule weekrule, DayOfWeek firstDayOfWeek)
        {
            var ciCurr = CultureInfo.CurrentCulture;
            return ciCurr.Calendar.GetWeekOfYear(datetime, weekrule, firstDayOfWeek);
        }

        /// <summary>
        /// Weeks the of year.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="firstDayOfWeek">The first day of week.</param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime datetime, DayOfWeek firstDayOfWeek)
        {
            var dateinf = new DateTimeFormatInfo();
            var weekrule = dateinf.CalendarWeekRule;
            return WeekOfYear(datetime, weekrule, firstDayOfWeek);
        }

        /// <summary>
        /// Weeks the of year.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="weekrule">The weekrule.</param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime datetime, CalendarWeekRule weekrule)
        {
            var dateinf = new DateTimeFormatInfo();
            var firstDayOfWeek = dateinf.FirstDayOfWeek;
            return WeekOfYear(datetime, weekrule, firstDayOfWeek);
        }

        /// <summary>
        /// Weeks the of year.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime datetime)
        {
            var dateinf = new DateTimeFormatInfo();
            var weekrule = dateinf.CalendarWeekRule;
            var firstDayOfWeek = dateinf.FirstDayOfWeek;
            return WeekOfYear(datetime, weekrule, firstDayOfWeek);
        }

        #endregion

        #region Get Datetime for Day of Week

        /// <summary>
        /// Gets the date time for day of week.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="day">The day.</param>
        /// <param name="firstDayOfWeek">The first day of week.</param>
        /// <returns></returns>
        public static DateTime GetDateTimeForDayOfWeek(this DateTime datetime, DayOfWeek day, DayOfWeek firstDayOfWeek)
        {
            var current = DaysFromFirstDayOfWeek(datetime.DayOfWeek, firstDayOfWeek);
            var resultday = DaysFromFirstDayOfWeek(day, firstDayOfWeek);
            return datetime.AddDays(resultday - current);
        }

        public static DateTime GetDateTimeForDayOfWeek(this DateTime datetime, DayOfWeek day)
        {
            var dateinf = new DateTimeFormatInfo();
            var firstDayOfWeek = dateinf.FirstDayOfWeek;
            return GetDateTimeForDayOfWeek(datetime, day, firstDayOfWeek);
        }

        /// <summary>
        /// Firsts the date time of week.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns></returns>
        public static DateTime FirstDateTimeOfWeek(this DateTime datetime)
        {
            var dateinf = new DateTimeFormatInfo();
            var firstDayOfWeek = dateinf.FirstDayOfWeek;
            return FirstDateTimeOfWeek(datetime, firstDayOfWeek);
        }

        /// <summary>
        /// Firsts the date time of week.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="firstDayOfWeek">The first day of week.</param>
        /// <returns></returns>
        public static DateTime FirstDateTimeOfWeek(this DateTime datetime, DayOfWeek firstDayOfWeek)
        {
            return datetime.AddDays(-DaysFromFirstDayOfWeek(datetime.DayOfWeek, firstDayOfWeek));
        }

        /// <summary>
        /// Dayses from first day of week.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="firstDayOfWeek">The first day of week.</param>
        /// <returns></returns>
        private static int DaysFromFirstDayOfWeek(DayOfWeek current, DayOfWeek firstDayOfWeek)
        {
            //Sunday = 0,Monday = 1,...,Saturday = 6
            var daysbetween = current - firstDayOfWeek;
            if (daysbetween < 0) daysbetween = 7 + daysbetween;
            return daysbetween;
        }

        #endregion

        public static string GetValueOrDefaultToString(this DateTime? datetime, string defaultvalue)
        {
            return datetime == null ? defaultvalue : datetime.Value.ToStringCurrent();
        }

        public static string GetValueOrDefaultToString(this DateTime? datetime, string format, string defaultvalue)
        {
            return datetime?.ToString(format) ?? defaultvalue;
        }
    }
}