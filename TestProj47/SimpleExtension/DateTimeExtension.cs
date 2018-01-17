// Decompiled with JetBrains decompiler
// Type: SimpleExtension.DateTimeExtension
// Assembly: SimpleExtension, Version=0.0.23.0, Culture=neutral, PublicKeyToken=null
// MVID: B43B1EC6-29EF-47CC-B98E-E2FE4FC2095C
// Assembly location: ...\bin\Debug\SimpleExtension.dll

using System;
using System.Globalization;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static DateTime MinDateValue = new DateTime(1900, 1, 1, 0, 0, 0, 0,
            CultureInfo.InvariantCulture.Calendar, DateTimeKind.Utc);

        public static DateTime BeginningOfDay(this DateTime date)
        {
            return date.Date;
        }

        public static string LengthOfTime(this DateTime date)
        {
            var timeSpan = DateTime.Now.Subtract(date);
            if (timeSpan.Minutes == 0)
                return timeSpan.Seconds + "s";
            if (timeSpan.Hours == 0)
                return timeSpan.Minutes + "m";
            if (timeSpan.Days == 0)
                return timeSpan.Hours + "h";
            return timeSpan.Days + "d";
        }

        public static DateTime BeginningOfMonth(this DateTime obj)
        {
            return new DateTime(obj.Year, obj.Month, 1, 0, 0, 0, 0);
        }

        public static DateTime DateTimeFromDateAndTime(this string date, string time)
        {
            return DateTime.Parse($"{(object) date} {(object) time}");
        }

        public static DateTime DateTimeFromDateAndTime(this DateTime date, string time)
        {
            return DateTime.Parse($"{(object) date.ToShortDateString()} {(object) time}");
        }

        public static string FractionalHoursToString(this decimal hours, string format)
        {
            if (string.IsNullOrEmpty(format))
                format = "{0}:{1}";
            var timeSpan = TimeSpan.FromHours((double) hours);
            var minutes = timeSpan.Minutes;
            if (timeSpan.Seconds > 29)
                ++minutes;
            return string.Format(format, timeSpan.Hours + timeSpan.Days * 24, minutes);
        }

        public static string FractionalHoursToString(this decimal hours)
        {
            return hours.FractionalHoursToString(null);
        }

        public static string FriendlyDateString(this DateTime date, bool showTime)
        {
            if (date < MinDateValue)
                return string.Empty;
            var str = !(date.Date == DateTime.Today)
                ? (!(date.Date == DateTime.Today.AddDays(-1.0))
                    ? (!(date.Date > DateTime.Today.AddDays(-6.0))
                        ? date.ToString("MMMM dd, yyyy")
                        : date.ToString("dddd"))
                    : " Yesterday")
                : " Today";
            if (showTime)
                str += $" @ {(object) date.ToString("t").ToLower().Replace(" ", "")}";
            return str;
        }

        public static string FriendlyElapsedTimeString(this int milliSeconds)
        {
            if (milliSeconds < 0)
                return string.Empty;
            if (milliSeconds < 60000)
                return "just now";
            return milliSeconds >= 3600000
                ? $"{(object) (milliSeconds / 3600000)}h ago"
                : $"{(object) (milliSeconds / 60000)}m ago";
        }

        public static string FriendlyElapsedTimeString(this TimeSpan elapsed)
        {
            return ((int) elapsed.TotalMilliseconds).FriendlyElapsedTimeString();
        }

        public static string MimeDateTime(this DateTime time)
        {
#if NetFX
            var utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(time);
#else
            var utcOffset = TimeZoneInfo.Local.GetUtcOffset(time);
#endif
            var str = (utcOffset.Hours < 0
                          ? $"-{(object) (utcOffset.Hours * -1).ToString().PadLeft(2, '0')}"
                          : $"+{(object) utcOffset.Hours.ToString().PadLeft(2, '0')}"
                      ) + utcOffset.Minutes.ToString().PadLeft(2, '0');
            return
                $"Date: {(object) time.ToString("ddd, dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture)} {(object) str}";
        }

        public static string ShortDateString(this DateTime date, bool showTime = false)
        {
            if (date < MinDateValue)
                return string.Empty;
            var str = date.ToString("MMM dd, yyyy");
            return !showTime ? str : $"{(object) str} - {(object) date.ToString("h:mmtt").ToLower()}";
        }

        public static string ShortDateString(this DateTime? date, bool showTime)
        {
            return !date.HasValue ? string.Empty : date.Value.ShortDateString(showTime);
        }
    }
}