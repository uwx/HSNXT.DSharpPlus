// Decompiled with JetBrains decompiler
// Type: dff.Extensions.TimeSpanExtensions
// Assembly: dff.Extensions, Version=1.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D6C927DF-93D7-4A34-9061-9B93EC850F98
// Assembly location: ...\bin\Debug\dff.Extensions.dll

using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static string GetDescription(this TimeSpan ts)
        {
            var str1 = ts.Ticks > 0L ? "vor " : "in ";
            var minutes = ts.Minutes;
            var hours = ts.Hours;
            var days = ts.Days;
            string str2;
            switch (days)
            {
                case 0:
                    if (hours > 1)
                        str1 = str1 + hours + " Stunden ";
                    else if (hours == 1)
                        str1 = str1 + hours + " Stunde ";
                    str2 = minutes != 0 ? str1 + minutes + " Minuten" : str1 + minutes + " Minute";
                    goto label_9;
                case 1:
                    str2 = str1 + days + " Tag";
                    break;
                default:
                    str2 = str1 + days + " Tagen";
                    break;
            }
            label_9:
            return str2;
        }
    }
}