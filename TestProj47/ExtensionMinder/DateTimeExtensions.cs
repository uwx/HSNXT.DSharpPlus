// Decompiled with JetBrains decompiler
// Type: ExtensionMinder.DateTimeExtensions
// Assembly: ExtensionMinder, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9895DAEA-F52E-4F0C-B5FD-FB311AEFF61C
// Assembly location: ...\bin\Debug\ExtensionMinder.dll

using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime FromUnixTimeToDateTimeUtc(this long secondsSinceEpoch)
        {
            return Epoch.AddSeconds(secondsSinceEpoch);
        }

        public static long DateTimeUtcToUnixTime(this DateTime dateTime)
        {
            return (long) (dateTime - Epoch).TotalSeconds;
        }

        public static bool IsBetween(this DateTime input, DateTime start, DateTime end)
        {
            return input.IsBetween(start, end, true);
        }

        public static bool IsBetween(this DateTime input, DateTime start, DateTime end, bool includeBoundaries)
        {
            if (!includeBoundaries)
            {
                if (input > start)
                    return input < end;
                return false;
            }
            if (input >= start)
                return input <= end;
            return false;
        }

        public static string FormatFinancialYear(this DateTime date)
        {
            return
                $"{(object) (date.Year - (date.Month <= 6 ? 1 : 0))}/{(object) (date.Year + (date.Month <= 6 ? 0 : 1))}";
        }

        public static DateTime GetFinancialYearStartDate(this DateTime date)
        {
            return new DateTime(date.Year - (date.Month <= 6 ? 1 : 0), 7, 1);
        }

        public static DateTime GetFinancialYearEndDate(this DateTime date)
        {
            return new DateTime(date.Year + (date.Month <= 6 ? 0 : 1), 6, 30);
        }

        public static DateTime Min(DateTime t1, DateTime t2)
        {
            if (DateTime.Compare(t1, t2) > 0)
                return t2;
            return t1;
        }

        public static DateTime Max(DateTime t1, DateTime t2)
        {
            if (DateTime.Compare(t1, t2) < 0)
                return t2;
            return t1;
        }
    }
}