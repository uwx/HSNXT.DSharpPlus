// Decompiled with JetBrains decompiler
// Type: dff.Extensions.DateTimeExtensions
// Assembly: dff.Extensions, Version=1.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D6C927DF-93D7-4A34-9061-9B93EC850F98
// Assembly location: ...\bin\Debug\dff.Extensions.dll

using System;
using System.Globalization;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static string GetDateTimeString(this DateTime date)
        {
            try
            {
                var provider = (IFormatProvider) new CultureInfo("de-DE", true);
                return date.ToString("G", provider);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetDateTimeStringIso(this DateTime date)
        {
            try
            {
                return date.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch
            {
                return string.Empty;
            }
        }

        public static TimeSpan GetTimeSpanTillNow(this DateTime date)
        {
            return new TimeSpan(DateTime.Now.Ticks - date.Ticks);
        }

        public static TimeSpan GetTimeSpanTillNow(this DateTime? date)
        {
            var dateTime = DateTime.Now;
            var ticks1 = dateTime.Ticks;
            dateTime = Convert.ToDateTime(date);
            var ticks2 = dateTime.Ticks;
            return new TimeSpan(ticks1 - ticks2);
        }
    }
}