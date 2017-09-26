// Decompiled with JetBrains decompiler
// Type: CSharpBasicExtensionsKit.StringDateTimeExtensions
// Assembly: CSharpBasicExtensionsKit, Version=1.0.6.0, Culture=neutral, PublicKeyToken=null
// MVID: EC83AE8C-D918-490B-8983-23C0E093D8F5
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\CSharpBasicExtensionsKit.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TestProj47
{
    public static partial class Extensions
    {
    private static readonly string[] dateFormats = new string[5]
    {
      "ddd, d MMM yyyy HH:mm:ss zzz",
      "ddd, dd MMM yyyy HH:mm:ss zzz",
      "dd MMM yyyy HH:mm:ss zzz",
      "d MMM yyyy HH:mm:ss zzz",
      "ddd, dd MMM yyyy hh:mm:ss zzz"
    };

    public static DateTime ToDateTimeFromStr(this string obj)
    {
      DateTime result;
      if (!DateTime.TryParse(obj, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) && !DateTime.TryParseExact(obj, StringDateTimeExtensions.dateFormats, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite, out result) && !DateTime.TryParseExact(StringDateTimeExtensions.ReplaceRfcDate(obj), StringDateTimeExtensions.dateFormats, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite, out result))
        DateTime.TryParse(obj, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out result);
      DateTime dateTime1;
      if (result == DateTime.MinValue)
      {
        string lower = obj.ToLower();
        dateTime1 = DateTime.Now;
        int year = dateTime1.Year;
        int month = StringDateTimeExtensions.GuessMonth(lower);
        int day = StringDateTimeExtensions.GuessDay(lower);
        dateTime1 = DateTime.Now;
        int hour = dateTime1.Hour;
        dateTime1 = DateTime.Now;
        int minute = dateTime1.Minute;
        int second = 0;
        dateTime1 = new DateTime(year, month, day, hour, minute, second);
        result = dateTime1.ToUniversalTime();
        DateTime dateTime2 = result;
        dateTime1 = DateTime.Now;
        DateTime universalTime = dateTime1.ToUniversalTime();
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
      int num = ((IEnumerable<string>) dt.Split(' ')).Select<string, int>((Func<string, int>) (x =>
      {
        int result;
        int.TryParse(x, out result);
        return result;
      })).FirstOrDefault<int>((Func<int, bool>) (x =>
      {
        if (x > 0)
          return x <= 27;
        return false;
      }));
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
