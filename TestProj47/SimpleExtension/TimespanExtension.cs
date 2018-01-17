// Decompiled with JetBrains decompiler
// Type: SimpleExtension.TimespanExtension
// Assembly: SimpleExtension, Version=0.0.23.0, Culture=neutral, PublicKeyToken=null
// MVID: B43B1EC6-29EF-47CC-B98E-E2FE4FC2095C
// Assembly location: ...\bin\Debug\SimpleExtension.dll

using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static string ToHumanTimeString(this TimeSpan span, int significantDigits = 3)
        {
            var format = "G" + significantDigits;
            if (span.TotalMilliseconds < 1000.0)
                return span.TotalMilliseconds.ToString(format) + " Miliseconds";
            if (span.TotalSeconds < 60.0)
                return span.TotalSeconds.ToString(format) + " Seconds";
            if (span.TotalMinutes < 60.0)
                return span.TotalMinutes.ToString(format) + " Minutes";
            if (span.TotalHours >= 24.0)
                return span.TotalDays.ToString(format) + " Days";
            return span.TotalHours.ToString(format) + " Hours";
        }

        public static TimeSpan RoundToNearest(this TimeSpan a, TimeSpan roundTo)
        {
            return new TimeSpan((long) (Math.Round(a.Ticks / (double) roundTo.Ticks) * roundTo.Ticks));
        }

        public static TimeSpan ToTimespan(this string pTime)
        {
            TimeSpan result;
            if (!TimeSpan.TryParse(pTime, out result))
                return new TimeSpan(0, 0, 0);
            return result;
        }

        public static TimeSpan ToTimespan(this int pSeconds)
        {
            return new TimeSpan(0, 0, pSeconds);
        }
    }
}