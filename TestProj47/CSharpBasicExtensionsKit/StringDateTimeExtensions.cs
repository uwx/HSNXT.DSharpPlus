// Decompiled with JetBrains decompiler
// Type: CSharpBasicExtensionsKit.StringDateTimeExtensions
// Assembly: CSharpBasicExtensionsKit, Version=1.0.6.0, Culture=neutral, PublicKeyToken=null
// MVID: EC83AE8C-D918-490B-8983-23C0E093D8F5
// Assembly location: ...\bin\Debug\CSharpBasicExtensionsKit.dll

using System;
using System.Globalization;
using System.Linq;

// ReSharper disable StringIndexOfIsCultureSpecific.1

namespace HSNXT
{
    public static partial class Extensions
    {
        private static readonly string[] DateFormats =
        {
            "ddd, d MMM yyyy HH:mm:ss zzz",
            "ddd, dd MMM yyyy HH:mm:ss zzz",
            "dd MMM yyyy HH:mm:ss zzz",
            "d MMM yyyy HH:mm:ss zzz",
            "ddd, dd MMM yyyy hh:mm:ss zzz"
        };

        public static DateTime ToDateTimeFromStr(this string obj)
        {
            if (
                !DateTime.TryParse(obj, CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out var result) &&
                !DateTime.TryParseExact(obj, DateFormats, CultureInfo.InvariantCulture,
                    DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite, out result) &&
                !DateTime.TryParseExact(ReplaceRfcDate(obj), DateFormats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite, out result))
                DateTime.TryParse(obj, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal,
                    out result);
            DateTime dateTime1;
            if (result == DateTime.MinValue)
            {
                var lower = obj.ToLower();
                dateTime1 = DateTime.Now;
                var year = dateTime1.Year;
                var month = GuessMonth(lower);
                var day = GuessDay(lower);
                dateTime1 = DateTime.Now;
                var hour = dateTime1.Hour;
                dateTime1 = DateTime.Now;
                var minute = dateTime1.Minute;
                var second = 0;
                dateTime1 = new DateTime(year, month, day, hour, minute, second);
                result = dateTime1.ToUniversalTime();
                var dateTime2 = result;
                dateTime1 = DateTime.Now;
                var universalTime = dateTime1.ToUniversalTime();
                if (dateTime2 > universalTime)
                    result = result.AddYears(-1);
            }
            if (result == DateTime.MinValue)
            {
                dateTime1 = DateTime.Now;
                dateTime1 = dateTime1.AddDays(-1.0);
                result = dateTime1.ToUniversalTime();
            }
            return result;
        }

        private static int GuessMonth(string dt)
        {
            if (-1 != dt.IndexOf("jan"))
                return 1;
            if (-1 != dt.IndexOf("feb"))
                return 2;
            if (-1 != dt.IndexOf("mar"))
                return 3;
            if (-1 != dt.IndexOf("apr"))
                return 4;
            if (-1 != dt.IndexOf("may"))
                return 5;
            if (-1 != dt.IndexOf("jun"))
                return 6;
            if (-1 != dt.IndexOf("jul"))
                return 7;
            if (-1 != dt.IndexOf("aug"))
                return 8;
            if (-1 != dt.IndexOf("sep"))
                return 9;
            if (-1 != dt.IndexOf("oct"))
                return 10;
            if (-1 != dt.IndexOf("nov"))
                return 11;
            if (-1 != dt.IndexOf("dec"))
                return 12;
            return DateTime.Now.Month;
        }

        private static int GuessDay(string dt)
        {
            var num = dt.Split(' ').Select(x =>
            {
                int.TryParse(x, out var result);
                return result;
            }).FirstOrDefault(x =>
            {
                if (x > 0)
                    return x <= 27;
                return false;
            });
            if (num != 0)
                return num;
            return DateTime.Now.Day;
        }

        private static string ReplaceRfcDate(string dateStr)
        {
            if (dateStr.IndexOf(" EDT") != -1)
                return dateStr.Replace(" EDT", " -04:00");
            if (dateStr.IndexOf(" EST") != -1)
                return dateStr.Replace(" EST", " -05:00");
            if (dateStr.IndexOf(" GMT") != -1)
                return dateStr.Replace(" GMT", " -00:00");
            if (dateStr.IndexOf(" CST") != -1)
                return dateStr.Replace(" CST", " -06:00");
            if (dateStr.IndexOf(" CDT") != -1)
                return dateStr.Replace(" CDT", " -05:00");
            if (dateStr.IndexOf(" MST") != -1)
                return dateStr.Replace(" MST", " -07:00");
            if (dateStr.IndexOf(" MDT") != -1)
                return dateStr.Replace(" MDT", " -06:00");
            if (dateStr.IndexOf(" PST") != -1)
                return dateStr.Replace(" PST", " -08:00");
            if (dateStr.IndexOf(" PDT") != -1)
                return dateStr.Replace(" PDT", " -07:00");
            return dateStr;
        }
    }
}