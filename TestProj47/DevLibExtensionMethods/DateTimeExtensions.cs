// Decompiled with JetBrains decompiler
// Type: TestProj47.DateTimeExtensions
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System;
using System.Globalization;

namespace HSNXT
{
    public static partial class Extensions

    {
        public const long TicksPerMillisecond = 10000;
        public const long TicksPerSecond = 10000000;
        public const long TicksPerMinute = 600000000;
        public const long TicksPerHour = 36000000000;
        public const long TicksPerDay = 864000000000;
        public const int DaysPerYear = 365;
        public const int DaysPer4Years = 1461;
        public const int DaysPer100Years = 36524;
        public const int DaysPer400Years = 146097;
        public const int DaysTo1970 = 719162;
        public const int DaysTo10000 = 3652059;
        public const long UnixEpochTicks = 621355968000000000;
        public const long UnixEpochSeconds = 62135596800;
        public const long UnixEpochMilliseconds = 62135596800000;
        public const long MinTicks = 0;
        public const long MaxTicks = 3155378975999999999;
        public const long MinSeconds = -62135596800;
        public const long MaxSeconds = 253402300799;
        public const long MinMilliseconds = -62135596800000;
        public const long MaxMilliseconds = 253402300799999;

        /// <summary>
        /// Gets the week number for a provided date time value based on the current culture settings.
        /// </summary>
        /// <param name="source">The DateTime.</param>
        /// <returns>The week number.</returns>
        public static int GetWeekOfYear(this DateTime source)
        {
            var currentUiCulture = CultureInfo.CurrentUICulture;
            var calendar = currentUiCulture.Calendar;
            var dateTimeFormat = currentUiCulture.DateTimeFormat;
            return calendar.GetWeekOfYear(source, dateTimeFormat.CalendarWeekRule, dateTimeFormat.FirstDayOfWeek);
        }

        /// <summary>Whether DateTime is a weekend.</summary>
        /// <param name="source">DateTime to check.</param>
        /// <returns>true if DateTime is weekend; otherwise, false.</returns>
        public static bool IsWeekend(this DateTime source)
        {
            if (source.DayOfWeek != DayOfWeek.Sunday)
                return source.DayOfWeek == DayOfWeek.Saturday;
            return true;
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified format and InvariantCulture information. The format of the string representation must match the specified format exactly.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="formats">A list of possible required format of <paramref name="source" />.</param>
        /// <returns>An object that is equivalent to the date and time contained in <paramref name="source" />, as specified by <paramref name="formats" />.</returns>
        public static DateTime ToDateTime(this string source, params string[] formats)
        {
            if (source.IsNullOrEmpty())
                return new DateTime();
            var result = new DateTime();
            if (!DateTime.TryParse(source, out result))
                DateTime.TryParseExact(source, formats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces,
                    out result);
            return result;
        }

        /// <summary>
        /// Converts a time to the time in another time zone based on the time zone offset.
        /// </summary>
        /// <param name="source">The date and time to convert.</param>
        /// <param name="destTimeZoneUtcOffset">The UTC offset of the destination time zone.</param>
        /// <returns>A System.DateTime value that represents the date and time in the destination time zone.</returns>
        public static DateTime ToTimeZone(this DateTime source, double destTimeZoneUtcOffset)
        {
            return new DateTimeOffset(source).ToOffset(TimeSpan.FromHours(destTimeZoneUtcOffset)).DateTime;
        }

        /// <summary>
        /// Converts a time to the time in another time zone based on the time zone's identifier.
        /// </summary>
        /// <param name="source">The date and time to convert.</param>
        /// <param name="destTimeZoneId">The identifier of the destination time zone.</param>
        /// <returns>A System.DateTime value that represents the date and time in the destination time zone.</returns>
        public static DateTime ToTimeZone(this DateTime source, string destTimeZoneId)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(source, destTimeZoneId);
        }

        /// <summary>
        /// Converts a time to the time in another time zone based on the time zone offset.
        /// </summary>
        /// <param name="source">The date and time to convert.</param>
        /// <param name="destTimeZoneUtcOffset">The UTC offset of the destination time zone.</param>
        /// <returns>A System.DateTime value that represents the date and time in the destination time zone.</returns>
        public static DateTimeOffset ToTimeZone(this DateTimeOffset source, double destTimeZoneUtcOffset)
        {
            return source.ToOffset(TimeSpan.FromHours(destTimeZoneUtcOffset));
        }

        /// <summary>
        /// Converts a time to the time in another time zone based on the time zone's identifier.
        /// </summary>
        /// <param name="source">The date and time to convert.</param>
        /// <param name="destTimeZoneId">The identifier of the destination time zone.</param>
        /// <returns>A System.DateTime value that represents the date and time in the destination time zone.</returns>
        public static DateTimeOffset ToTimeZone(this DateTimeOffset source, string destTimeZoneId)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(source, destTimeZoneId);
        }

        /// <summary>
        /// Returns the number of seconds that have elapsed since 1970-01-01T00:00:00Z.
        /// </summary>
        /// <param name="source">The source DateTimeOffset.</param>
        /// <returns>The number of seconds that have elapsed since 1970-01-01T00:00:00Z.</returns>
        public static long ToUnixTimeSeconds(this DateTimeOffset source)
        {
            return source.Ticks / 10000000L - 62135596800L;
        }

        /// <summary>
        /// Returns the number of milliseconds that have elapsed since 1970-01-01T00:00:00.000Z.
        /// </summary>
        /// <param name="source">The source DateTimeOffset.</param>
        /// <returns>The number of milliseconds that have elapsed since 1970-01-01T00:00:00.000Z.</returns>
        public static long ToUnixTimeMilliseconds(this DateTimeOffset source)
        {
            return source.Ticks / 10000L - 62135596800000L;
        }

        /// <summary>
        /// Converts a Unix time expressed as the number of seconds that have elapsed since 1970-01-01T00:00:00Z to a DateTimeOffset value.
        /// </summary>
        /// <param name="source">A Unix time, expressed as the number of seconds that have elapsed since 1970-01-01T00:00:00Z (January 1, 1970, at 12:00 AM UTC). For Unix times before this date, its value is negative.</param>
        /// <returns>A date and time value that represents the same moment in time as the Unix time.</returns>
        public static DateTimeOffset FromUnixTimeSeconds(this long source)
        {
            if (source < -62135596800L || source > 253402300799L)
                throw new ArgumentOutOfRangeException(nameof(source));
            return new DateTimeOffset(source * 10000000L + 621355968000000000L, TimeSpan.Zero);
        }

        /// <summary>
        /// Converts a Unix time expressed as the number of milliseconds that have elapsed since 1970-01-01T00:00:00Z to a DateTimeOffset value.
        /// </summary>
        /// <param name="source">A Unix time, expressed as the number of milliseconds that have elapsed since 1970-01-01T00:00:00Z (January 1, 1970, at 12:00 AM UTC). For Unix times before this date, its value is negative.</param>
        /// <returns>A date and time value that represents the same moment in time as the Unix time.</returns>
        public static DateTimeOffset FromUnixTimeMilliseconds(this long source)
        {
            if (source < -62135596800000L || source > 253402300799999L)
                throw new ArgumentOutOfRangeException(nameof(source));
            return new DateTimeOffset(source * 10000L + 621355968000000000L, TimeSpan.Zero);
        }

        /// <summary>
        /// Returns the number of seconds that have elapsed since 1970-01-01T00:00:00Z.
        /// </summary>
        /// <param name="source">The source DateTime.</param>
        /// <returns>The number of seconds that have elapsed since 1970-01-01T00:00:00Z.</returns>
        public static long ToUnixTimeSeconds(this DateTime source)
        {
            return source.Ticks / 10000000L - 62135596800L;
        }

        /// <summary>
        /// Returns the number of milliseconds that have elapsed since 1970-01-01T00:00:00.000Z.
        /// </summary>
        /// <param name="source">The source DateTime.</param>
        /// <returns>The number of milliseconds that have elapsed since 1970-01-01T00:00:00.000Z.</returns>
        public static long ToUnixTimeMilliseconds(this DateTime source)
        {
            return source.Ticks / 10000L - 62135596800000L;
        }
    }
}