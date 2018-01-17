// Decompiled with JetBrains decompiler
// Type: dff.Extensions.Gps.DffGpsTools
// Assembly: dff.Extensions, Version=1.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D6C927DF-93D7-4A34-9061-9B93EC850F98
// Assembly location: ...\bin\Debug\dff.Extensions.dll

using System;
using System.Globalization;

namespace HSNXT.dff.Extensions.Gps
{
    public class DffGpsTools
    {
        public static double GetDistanceDec(string p1XDec, string p1YDec, string p2XDec, string p2YDec)
        {
            var provider = (IFormatProvider) new CultureInfo("en-US", true);
            var num1 = double.Parse(p1XDec, provider) / 100000.0;
            var num2 = double.Parse(p1YDec, provider) / 100000.0;
            var num3 = double.Parse(p2XDec, provider) / 100000.0;
            var num4 = double.Parse(p2YDec, provider) / 100000.0;
            return Math.Round(6378.136 * Math.Acos(
                                  Math.Sin(num2 / (180.0 / Math.PI)) * Math.Sin(num4 / (180.0 / Math.PI)) +
                                  Math.Cos(num2 / (180.0 / Math.PI)) * Math.Cos(num4 / (180.0 / Math.PI)) *
                                  Math.Cos(num3 / (180.0 / Math.PI) - num1 / (180.0 / Math.PI))) * 1000.0);
        }

        public static double GetDistanceDme(string p1XDme, string p1YDme, string p2XDme, string p2YDme)
        {
            var provider = (IFormatProvider) new CultureInfo("en-US", true);
            var xGeodec1 = "";
            var yGeodec1 = "";
            var xGeodec2 = "";
            var yGeodec2 = "";
            Geodms2Geodec(p1XDme, p1YDme, ref xGeodec1, ref yGeodec1);
            Geodms2Geodec(p2XDme, p2YDme, ref xGeodec2, ref yGeodec2);
            var num1 = double.Parse(xGeodec1, provider);
            var num2 = double.Parse(yGeodec1, provider);
            var num3 = double.Parse(xGeodec2, provider);
            var num4 = double.Parse(yGeodec2, provider);
            return Math.Round(6.371 * Math.Sqrt(Math.Pow(num4 - num2, 2.0) +
                                                Math.Pow(
                                                    (num3 - num1) * Math.Cos((num4 + num2) * Math.PI / 360000000.0),
                                                    2.0)) * Math.PI / 18.0);
        }

        public static double Kmh2Knoten(double kmh)
        {
            return kmh * 0.539956803;
        }

        public static void Geodme2Geodms(string xGeodec, string yGeodec, bool north, bool east, ref string xGeodms,
            ref string yGeodms)
        {
            if (yGeodms == null)
                throw new ArgumentNullException(nameof(yGeodms));
            var provider = (IFormatProvider) new CultureInfo("en-US", true);
            var str1 = xGeodec.Substring(0, xGeodec.IndexOf(".", StringComparison.Ordinal) - 2);
            var str2 = yGeodec.Substring(0, yGeodec.IndexOf(".", StringComparison.Ordinal) - 2);
            if (str1.Length > 2)
                str1 = str1.Substring(str1.Length - 2);
            if (str2.Length > 2)
                str2 = str2.Substring(str2.Length - 2);
            var s1 = xGeodec.Substring(xGeodec.IndexOf(".", StringComparison.Ordinal) - 2);
            var s2 = yGeodec.Substring(yGeodec.IndexOf(".", StringComparison.Ordinal) - 2);
            var d1 = double.Parse(s1, provider);
            var d2 = double.Parse(s2, provider);
            var a1 = (d1 - Math.Floor(d1)) * 600.0;
            var a2 = (d2 - Math.Floor(d2)) * 600.0;
            var num1 = Math.Floor(d1);
            var num2 = Math.Floor(d2);
            var num3 = Math.Round(a1);
            var num4 = Math.Round(a2);
            var str3 = num1 >= 10.0 ? string.Concat(num1) : "0" + num1;
            var str4 = string.Concat(num3);
            if (num3 < 100.0)
                str4 = num3 >= 10.0 ? "0" + num3 : "00" + num3;
            var str5 = num2 >= 10.0 ? string.Concat(num2) : "0" + num2;
            var str6 = string.Concat(num4);
            if (num4 < 100.0)
                str6 = num4 >= 10.0 ? "0" + num4 : "00" + num4;
            xGeodms = str1 + str3 + str4;
            yGeodms = str2 + str5 + str6;
            if (!east)
                xGeodms = "-" + xGeodms;
            if (north)
                return;
            yGeodms = "-" + yGeodms;
        }

        public static void Geodms2Geodec(string xGeodms, string yGeodms, ref string xGeodec, ref string yGeodec)
        {
            if (xGeodec == null)
                throw new ArgumentNullException(nameof(xGeodec));
            var provider = (IFormatProvider) new CultureInfo("en-US", true);
            var num1 = double.Parse(xGeodms);
            var num2 = double.Parse(yGeodms);
            var flag1 = num1 < 0.0;
            var flag2 = num2 < 0.0;
            var a1 = double.Parse(xGeodms.Substring(xGeodms.Length - 3), provider) * (5.0 / 18.0) * 10.0;
            var a2 = double.Parse(yGeodms.Substring(yGeodms.Length - 3), provider) * (5.0 / 18.0) * 10.0;
            var a3 = double.Parse(xGeodms.Substring(xGeodms.Length - 5, 2), provider) * (5.0 / 3.0) * 1000.0;
            var a4 = double.Parse(yGeodms.Substring(yGeodms.Length - 5, 2), provider) * (5.0 / 3.0) * 1000.0;
            var a5 = double.Parse(xGeodms.Substring(xGeodms.Length - 7, 2), provider) * 100000.0;
            var a6 = double.Parse(yGeodms.Substring(yGeodms.Length - 7, 2), provider) * 100000.0;
            xGeodec = string.Concat(Math.Round(a5) + Math.Round(a3) + Math.Round(a1));
            yGeodec = string.Concat(Math.Round(a6) + Math.Round(a4) + Math.Round(a2));
            if (flag1)
                xGeodec = "-" + xGeodec;
            if (!flag2)
                return;
            yGeodec = "-" + yGeodec;
        }

        public static void Geodec2Geodms(string xGeodec, string yGeodec, ref string xGeodms, ref string yGeodms)
        {
            if (xGeodms == null)
                throw new ArgumentNullException(nameof(xGeodms));
            var num1 = double.Parse(xGeodec);
            var num2 = double.Parse(yGeodec);
            var flag1 = num1 < 0.0;
            var flag2 = num2 < 0.0;
            var num3 = Math.Floor(num1 / 100000.0);
            var num4 = Math.Floor(num2 / 100000.0);
            var d1 = (num1 / 100000.0 - num3) * 100.0 / (5.0 / 3.0);
            var d2 = (num2 / 100000.0 - num4) * 100.0 / (5.0 / 3.0);
            var a1 = Math.Abs(d1 - Math.Floor(d1)) * 600.0;
            var a2 = Math.Abs(d2 - Math.Floor(d2)) * 600.0;
            var num5 = Math.Abs(Math.Floor(d1));
            var num6 = Math.Abs(Math.Floor(d2));
            var num7 = Math.Abs(Math.Round(a1));
            var num8 = Math.Abs(Math.Round(a2));
            var str1 = string.Concat(num5);
            var str2 = string.Concat(num6);
            if (num5 < 10.0)
                str1 = "0" + num5;
            if (num6 < 10.0)
                str2 = "0" + num6;
            var str3 = string.Concat(num7);
            var str4 = string.Concat(num8);
            if (num7 < 100.0)
                str3 = num7 >= 10.0 ? "0" + num7 : "00" + num7;
            if (num8 < 100.0)
                str4 = num8 >= 10.0 ? "0" + num8 : "00" + num8;
            xGeodms = num3 + str1 + str3;
            yGeodms = num4 + str2 + str4;
            if (flag1)
                xGeodms = "-" + xGeodms;
            if (!flag2)
                return;
            yGeodms = "-" + yGeodms;
        }

        public static void GeodecimalToMercator(double fLongitude, double fLatitude, ref int mercatorX)
        {
            var mercatorY = 0;
            GeodecimalToMercator(fLongitude, fLatitude, ref mercatorX, ref mercatorY);
        }

        public static void GeodecimalToMercator(double fLongitude, double fLatitude, ref int mercatorX,
            ref int mercatorY)
        {
            mercatorX = (int) (fLongitude * 111194.926644559);
            mercatorY = (int) (Math.Log(Math.Tan(fLatitude * (Math.PI / 360.0) + Math.PI / 4.0)) * 6371000.0);
        }

        public static void MercatorToGeodecimal(int mercatorX, int mercatorY, ref double fLongitude,
            ref double fLatitude)
        {
            fLongitude = Math.Round(mercatorX * (180.0 / Math.PI) / 6371000.0, 5);
            fLatitude = Math.Round(360.0 / Math.PI * (Math.Atan(Math.Exp(mercatorY / 6371000.0)) - Math.PI / 4.0), 5);
        }

        public static int GetDistanceMercator(int p1X, int p1Y, int p2X, int p2Y)
        {
            var num1 = (double) (p1X - p2X);
            var num2 = (double) (p1Y - p2Y);
            return (int) Math.Round(
                Math.Cos(2.0 * (Math.Atan(Math.Exp((p1Y + p2Y) / 2 / 6371000)) - Math.PI / 4.0)) *
                Math.Sqrt(num1 * num1 + num2 * num2), 0);
        }

        public static int GetDistanceDffStrings(string dff1, string dff2)
        {
            var mercX1 = 0;
            var mercY1 = 0;
            var mercX2 = 0;
            var mercY2 = 0;
            DffPositionStringToMercator(dff1, ref mercX1, ref mercY1);
            DffPositionStringToMercator(dff2, ref mercX2, ref mercY2);
            return GetDistanceMercator(mercX1, mercY1, mercX2, mercY2);
        }

        public static void DffPositionStringToMercator(string dff, ref int mercX, ref int mercY)
        {
            if (string.IsNullOrEmpty(dff))
                return;
            var provider = (IFormatProvider) new CultureInfo("en-US", true);
            var strArray = dff.Split("|".ToCharArray());
            var s = strArray[0] + "." + strArray[1] + strArray[2];
            var num1 = double.Parse(strArray[3] + "." + strArray[4] + strArray[5], provider);
            var num2 = double.Parse(s, provider);
            var xGeodec = "";
            var yGeodec = "";
            var flag1 = false;
            var flag2 = false;
            var str1 = num1.ToString(provider);
            var str2 = num2.ToString(provider);
            if (str1.IndexOf(".", StringComparison.Ordinal) == -1)
                str1 += ".00000";
            if (str2.IndexOf(".", StringComparison.Ordinal) == -1)
                str2 += ".00000";
            if (str1.StartsWith("-"))
            {
                flag1 = true;
                str1 = str1.Substring(1);
            }
            if (str2.StartsWith("-"))
            {
                flag2 = true;
                str2 = str2.Substring(1);
            }
            while (str1.Substring(str1.IndexOf(".", StringComparison.Ordinal) + 1).Length < 5)
                str1 += "0";
            while (str2.Substring(str2.IndexOf(".", StringComparison.Ordinal) + 1).Length < 5)
                str2 += "0";
            while (str1.Length < 8)
                str1 = "0" + str1;
            while (str2.Length < 8)
                str2 = "0" + str2;
            if (flag1)
                str1 = "-" + str1;
            if (flag2)
                str2 = "-" + str2;
            Geodms2Geodec(str1.Replace(".", ""), str2.Replace(".", ""), ref xGeodec, ref yGeodec);
            var num3 = double.Parse(yGeodec, provider) / 100000.0;
            var num4 = double.Parse(xGeodec, provider) / 100000.0;
            mercX = (int) (num3 * 111194.926644559);
            mercY = (int) (Math.Log(Math.Tan(num4 * (Math.PI / 360.0) + Math.PI / 4.0)) * 6371000.0);
        }

        public static int CheckIfDeviceIsInGeoFenceDff(string location, string fence, int toleranz)
        {
            var flag = false;
            try
            {
                if (fence.EndsWith(";"))
                    fence = fence.Substring(0, fence.Length - 1);
                var str1 = location.Replace("|", "");
                var xGeodms1 = str1.Substring(0, 7);
                var yGeodms1 = str1.Substring(7, 7);
                var xGeodec1 = "";
                var yGeodec1 = "";
                Geodms2Geodec(xGeodms1, yGeodms1, ref xGeodec1, ref yGeodec1);
                var strArray = fence.Split(';');
                for (var index = 0; index < strArray.Length; ++index)
                {
                    if (strArray[index].Length != 0)
                    {
                        var num = int.Parse(strArray[index]
                            .Substring(0, strArray[index].IndexOf("@", StringComparison.Ordinal)));
                        var str2 = strArray[index].Substring(strArray[index].IndexOf("@", StringComparison.Ordinal) + 1)
                            .Replace("|", "");
                        var xGeodms2 = str2.Substring(0, 7);
                        var yGeodms2 = str2.Substring(7, 7);
                        var xGeodec2 = "";
                        var yGeodec2 = "";
                        Geodms2Geodec(xGeodms2, yGeodms2, ref xGeodec2, ref yGeodec2);
                        if (GetDistanceDec(xGeodec1, yGeodec1, xGeodec2, yGeodec2) <= num + toleranz + 10)
                            flag = true;
                    }
                }
            }
            catch
            {
                return -1;
            }
            return flag ? 1 : 0;
        }

        public static string GetMiddleOfPositionsDff(string pos1Dff, string pos2Dff)
        {
            pos1Dff = pos1Dff.Replace("|", "");
            var xGeodms1 = pos1Dff.Substring(0, 7);
            var yGeodms1 = pos1Dff.Substring(7, 7);
            var xGeodec1 = "";
            var yGeodec1 = "";
            Geodms2Geodec(xGeodms1, yGeodms1, ref xGeodec1, ref yGeodec1);
            pos2Dff = pos2Dff.Replace("|", "");
            var xGeodms2 = pos2Dff.Substring(0, 7);
            var yGeodms2 = pos2Dff.Substring(7, 7);
            var xGeodec2 = "";
            var yGeodec2 = "";
            Geodms2Geodec(xGeodms2, yGeodms2, ref xGeodec2, ref yGeodec2);
            var num1 = (double.Parse(xGeodec1) + double.Parse(xGeodec2)) / 2.0;
            var num2 = (double.Parse(yGeodec1) + double.Parse(yGeodec2)) / 2.0;
            var xGeodms3 = "";
            var yGeodms3 = "";
            Geodec2Geodms(num1.ToString(), num2.ToString(), ref xGeodms3, ref yGeodms3);
            if (!xGeodms3.StartsWith("-"))
            {
                while (xGeodms3.Length < 7)
                    xGeodms3 = "0" + xGeodms3;
            }
            else
            {
                var str = xGeodms3.Substring(1);
                while (str.Length < 7)
                    str = "0" + str;
                xGeodms3 = "-" + str;
            }
            if (!yGeodms3.StartsWith("-"))
            {
                while (yGeodms3.Length < 7)
                    yGeodms3 = "0" + yGeodms3;
            }
            else
            {
                var str = yGeodms3.Substring(1);
                while (str.Length < 7)
                    str = "0" + str;
                yGeodms3 = "-" + str;
            }
            var str1 = "";
            try
            {
                str1 = xGeodms3.Substring(0, 2) + "|" + xGeodms3.Substring(2, 2) + "|" + xGeodms3.Substring(4, 3) + "|";
                str1 = str1 + yGeodms3.Substring(0, 2) + "|" + yGeodms3.Substring(2, 2) + "|" +
                       yGeodms3.Substring(4, 3);
            }
            catch
            {
            }
            return str1.Trim();
        }

        public static int CheckIfDeviceIsInGeoFenceDec(string location, string fence, int toleranz)
        {
            var flag = false;
            try
            {
                if (fence.EndsWith(";"))
                    fence = fence.Substring(0, fence.Length - 1);
                var str1 = location.Replace("|", "");
                var xGeodms = str1.Substring(0, 7);
                var yGeodms = str1.Substring(7, 7);
                var xGeodec = "";
                var yGeodec = "";
                Geodms2Geodec(xGeodms, yGeodms, ref xGeodec, ref yGeodec);
                var strArray = fence.Split(';');
                for (var index = 0; index < strArray.Length; ++index)
                {
                    if (strArray[index].Length != 0)
                    {
                        var num = int.Parse(strArray[index]
                            .Substring(0, strArray[index].IndexOf("@", StringComparison.Ordinal)));
                        var str2 = strArray[index]
                            .Substring(strArray[index].IndexOf("@", StringComparison.Ordinal) + 1);
                        var p2XDec = str2.Substring(0, str2.IndexOf("|", StringComparison.Ordinal));
                        var p2YDec = str2.Substring(str2.IndexOf("|", StringComparison.Ordinal) + 1);
                        if (GetDistanceDec(xGeodec, yGeodec, p2XDec, p2YDec) <= num + toleranz + 10)
                            flag = true;
                    }
                }
            }
            catch
            {
                return -1;
            }
            return flag ? 1 : 0;
        }

        private string DecToDffPositionString(double decLatitude, double decLongitude)
        {
            var str1 = "";
            var xGeodms = "";
            var yGeodms = "";
            var str2 = decLatitude.ToString().Replace(".", ",");
            var str3 = decLongitude.ToString().Replace(".", ",");
            while (str2.Substring(str2.IndexOf(",", StringComparison.Ordinal) + 1).Length < 5)
                str2 += "0";
            while (str3.Substring(str3.IndexOf(",", StringComparison.Ordinal) + 1).Length < 5)
                str3 += "0";
            Geodec2Geodms(
                (str2.Substring(0, str2.IndexOf(",", StringComparison.Ordinal)) + "," +
                 str2.Substring(str2.IndexOf(",", StringComparison.Ordinal) + 1, 5)).Replace(",", ""),
                (str3.Substring(0, str3.IndexOf(",", StringComparison.Ordinal)) + "," +
                 str3.Substring(str3.IndexOf(",", StringComparison.Ordinal) + 1, 5)).Replace(",", ""), ref xGeodms,
                ref yGeodms);
            var num1 = double.Parse(xGeodms) / 100000.0;
            var num2 = double.Parse(yGeodms) / 100000.0;
            if (num1 != 0.0 & num2 != 0.0)
            {
                var provider = (IFormatProvider) new CultureInfo("en-US", true);
                var flag1 = false;
                var flag2 = false;
                var str4 = num1.ToString(provider);
                var str5 = num2.ToString(provider);
                if (str4.IndexOf(".", StringComparison.Ordinal) == -1)
                    str4 += ".00000";
                if (str5.IndexOf(".", StringComparison.Ordinal) == -1)
                    str5 += ".00000";
                if (str4.StartsWith("-"))
                {
                    flag1 = true;
                    str4 = str4.Substring(1);
                }
                if (str5.StartsWith("-"))
                {
                    flag2 = true;
                    str5 = str5.Substring(1);
                }
                while (str4.Substring(str4.IndexOf(".", StringComparison.Ordinal) + 1).Length < 5)
                    str4 += "0";
                while (str5.Substring(str5.IndexOf(".", StringComparison.Ordinal) + 1).Length < 5)
                    str5 += "0";
                while (str4.Substring(0, str4.IndexOf(".", StringComparison.Ordinal)).Length < 2)
                    str4 = "0" + str4;
                while (str5.Substring(0, str5.IndexOf(".", StringComparison.Ordinal)).Length < 2)
                    str5 = "0" + str5;
                if (flag2)
                    str5 = "-" + str5;
                if (flag1)
                    str4 = "-" + str4;
                var str6 = str4.Replace(".", "");
                var str7 = str5.Replace(".", "");
                string str8;
                if (!str7.StartsWith("-"))
                    str8 = str7.Substring(0, 2) + "|" + str7.Substring(2, 2) + "|" + str7.Substring(4, 3) + "|";
                else
                    str8 = str7.Substring(0, 3) + "|" + str7.Substring(3, 2) + "|" + str7.Substring(5, 3) + "|";
                if (!str6.StartsWith("-"))
                    str1 = str8 + str6.Substring(0, 2) + "|" + str6.Substring(2, 2) + "|" + str6.Substring(4, 3) +
                           "||" + 0 + "|" + 0;
                else
                    str1 = str8 + str6.Substring(0, 3) + "|" + str6.Substring(3, 2) + "|" + str6.Substring(5, 3) +
                           "||" + 0 + "|" + 0;
            }
            return str1;
        }
    }
}