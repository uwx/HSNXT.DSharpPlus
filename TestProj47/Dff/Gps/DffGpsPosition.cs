// Decompiled with JetBrains decompiler
// Type: dff.Extensions.Gps.DffGpsPosition
// Assembly: dff.Extensions, Version=1.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D6C927DF-93D7-4A34-9061-9B93EC850F98
// Assembly location: ...\bin\Debug\dff.Extensions.dll

using System;
using System.Globalization;

namespace HSNXT.dff.Extensions.Gps
{
    public class DffGpsPosition
    {
        private double _decLatitude;
        private double _decLongitude;
        private int _mrcX;
        private int _mrcY;
        public Exception Ex { get; private set; }

        public DffGpsPosition(string dffPositionFormatString)
        {
            if (string.IsNullOrEmpty(dffPositionFormatString))
                return;
            if (dffPositionFormatString.Contains(";"))
                dffPositionFormatString = dffPositionFormatString.Split(";".ToCharArray())[0];
            else if (dffPositionFormatString.Contains(","))
                dffPositionFormatString = dffPositionFormatString.Split(",".ToCharArray())[0];
            if (string.IsNullOrEmpty(dffPositionFormatString))
                return;
            if (dffPositionFormatString.Contains("@"))
            {
                var str = dffPositionFormatString.Substring(
                    dffPositionFormatString.IndexOf("@", StringComparison.Ordinal) + 1);
                var s = dffPositionFormatString.Substring(0,
                    dffPositionFormatString.IndexOf("@", StringComparison.Ordinal));
                dffPositionFormatString = str;
                try
                {
                    this.FenceRadiusMeter = int.Parse(s);
                }
                catch
                {
                    // ignored
                }
            }
            DffGpsTools.DffPositionStringToMercator(dffPositionFormatString, ref this._mrcX, ref this._mrcY);
            DffGpsTools.MercatorToGeodecimal(this._mrcX, this._mrcY, ref this._decLongitude, ref this._decLatitude);
            if (!dffPositionFormatString.Contains("||"))
                return;
            try
            {
                var strArray = dffPositionFormatString
                    .Substring(dffPositionFormatString.IndexOf("||", StringComparison.Ordinal) + 2)
                    .Split("|".ToCharArray());
                if (strArray.Length > 0 && !string.IsNullOrEmpty(strArray[0]))
                    this.Speed = double.Parse(strArray[0]);
                if (strArray.Length > 1 && !string.IsNullOrEmpty(strArray[1]))
                    this.Heading = int.Parse(strArray[1]);
            }
            catch
            {
                // ignored
            }
        }

        public DffGpsPosition(int mercatorX, int mercatorY)
        {
            this.MercatorX = mercatorX;
            this.MercatorY = mercatorY;
            DffGpsTools.MercatorToGeodecimal(this._mrcX, this._mrcY, ref this._decLongitude, ref this._decLatitude);
        }

        public DffGpsPosition(double decx, double decy)
        {
            this.DecLatitude = decy;
            this.DecLongitude = decx;
        }

        public DffGpsPosition()
        {
        }

        public int FenceRadiusMeter { get; set; }

        public DateTime PositionTimestamp { get; set; }

        public double DecLongitude
        {
            get
            {
                if (Math.Abs(this._decLongitude) > double.Epsilon)
                    return this._decLongitude;
                if (Math.Abs(this.DmsLongitude) < double.Epsilon)
                    return 0.0;
                this.CalcualteDecValues();
                return this._decLongitude;
            }
            set => this._decLongitude = value;
        }

        public double DecLatitude
        {
            get
            {
                if (Math.Abs(this._decLatitude) > double.Epsilon || Math.Abs(this.DmsLatitude) < double.Epsilon)
                    return this._decLatitude;
                this.CalcualteDecValues();
                return this._decLatitude;
            }
            set => this._decLatitude = value;
        }

        public double DmsLongitude { get; set; }

        public double DmsLatitude { get; set; }

        public int MercatorX
        {
            get
            {
                if (this._mrcX != 0)
                    return this._mrcX;
                this.CalculateMercatorValues();
                return this._mrcX;
            }
            set => this._mrcX = value;
        }

        public int MercatorY
        {
            get
            {
                if (this._mrcY != 0)
                    return this._mrcY;
                this.CalculateMercatorValues();
                return this._mrcY;
            }
            set => this._mrcY = value;
        }

        public double Speed { get; set; }

        public double Heading { get; set; }

        public double Height { get; set; }

        public int Sats { get; set; }

        public string DffPositionString
        {
            get
            {
                var str1 = "";
                try
                {
                    if (Math.Abs(this.DmsLatitude) < double.Epsilon & Math.Abs(this.DmsLongitude) < double.Epsilon &
                        this.MercatorX != 0 & this.MercatorY != 0)
                    {
                        var fLatitude = 0.0;
                        var fLongitude = 0.0;
                        DffGpsTools.MercatorToGeodecimal(this.MercatorX, this.MercatorY, ref fLongitude, ref fLatitude);
                        this.DecLatitude = fLatitude;
                        this.DecLongitude = fLongitude;
                    }
                    if (Math.Abs(this.DmsLatitude) < double.Epsilon & Math.Abs(this.DmsLongitude) < double.Epsilon &
                        Math.Abs(this.DecLatitude) > double.Epsilon & Math.Abs(this.DecLongitude) > double.Epsilon)
                    {
                        var xGeodms = "";
                        var yGeodms = "";
                        var str2 = this.DecLatitude.ToString(CultureInfo.InvariantCulture).Replace(".", ",");
                        var str3 = this.DecLongitude.ToString(CultureInfo.InvariantCulture).Replace(".", ",");
                        while (str2.Substring(str2.IndexOf(",", StringComparison.Ordinal) + 1).Length < 5)
                            str2 += "0";
                        while (str3.Substring(str3.IndexOf(",", StringComparison.Ordinal) + 1).Length < 5)
                            str3 += "0";
                        DffGpsTools.Geodec2Geodms(
                            (str2.Substring(0, str2.IndexOf(",", StringComparison.Ordinal)) + "," +
                             str2.Substring(str2.IndexOf(",", StringComparison.Ordinal) + 1, 5)).Replace(",", ""),
                            (str3.Substring(0, str3.IndexOf(",", StringComparison.Ordinal)) + "," +
                             str3.Substring(str3.IndexOf(",", StringComparison.Ordinal) + 1, 5)).Replace(",", ""),
                            ref xGeodms, ref yGeodms);
                        this.DmsLatitude = double.Parse(xGeodms) / 100000.0;
                        this.DmsLongitude = double.Parse(yGeodms) / 100000.0;
                    }
                    if (Math.Abs(this.DmsLatitude) > double.Epsilon & Math.Abs(this.DmsLongitude) > double.Epsilon)
                    {
                        var provider = (IFormatProvider) new CultureInfo("en-US", true);
                        var flag1 = false;
                        var flag2 = false;
                        var num = this.DmsLatitude;
                        var str2 = num.ToString(provider);
                        num = this.DmsLongitude;
                        var str3 = num.ToString(provider);
                        if (str2.IndexOf(".", StringComparison.Ordinal) == -1)
                            str2 += ".00000";
                        if (str3.IndexOf(".", StringComparison.Ordinal) == -1)
                            str3 += ".00000";
                        if (str2.StartsWith("-"))
                        {
                            flag1 = true;
                            str2 = str2.Substring(1);
                        }
                        if (str3.StartsWith("-"))
                        {
                            flag2 = true;
                            str3 = str3.Substring(1);
                        }
                        while (str2.Substring(str2.IndexOf(".", StringComparison.Ordinal) + 1).Length < 5)
                            str2 += "0";
                        while (str3.Substring(str3.IndexOf(".", StringComparison.Ordinal) + 1).Length < 5)
                            str3 += "0";
                        while (str2.Substring(0, str2.IndexOf(".", StringComparison.Ordinal)).Length < 2)
                            str2 = "0" + str2;
                        while (str3.Substring(0, str3.IndexOf(".", StringComparison.Ordinal)).Length < 2)
                            str3 = "0" + str3;
                        if (flag2)
                            str3 = "-" + str3;
                        if (flag1)
                            str2 = "-" + str2;
                        var str4 = str2.Replace(".", "");
                        var str5 = str3.Replace(".", "");
                        if (!str5.StartsWith("-"))
                            str1 = str5.Substring(0, 2) + "|" + str5.Substring(2, 2) + "|" + str5.Substring(4, 3) + "|";
                        else
                            str1 = str5.Substring(0, 3) + "|" + str5.Substring(3, 2) + "|" + str5.Substring(5, 3) + "|";
                        if (!str4.StartsWith("-"))
                            str1 = str1 + str4.Substring(0, 2) + "|" + str4.Substring(2, 2) + "|" +
                                   str4.Substring(4, 3) + "||" + this.Speed + "|" + this.Heading;
                        else
                            str1 = str1 + str4.Substring(0, 3) + "|" + str4.Substring(3, 2) + "|" +
                                   str4.Substring(5, 3) + "||" + this.Speed + "|" + this.Heading;
                    }
                }
                catch (Exception ex)
                {
                    this.Ex = ex;
                }
                return str1.Trim();
            }
        }

        public string CosPositionString
        {
            get
            {
                var provider1 = (IFormatProvider) new CultureInfo("en-US", true);
                var provider2 = (IFormatProvider) new CultureInfo("de-DE", true);
                double num1;
                if (Math.Abs(this.DmsLatitude) < double.Epsilon & Math.Abs(this.DmsLongitude) < double.Epsilon &
                    Math.Abs(this.DecLatitude) > double.Epsilon & Math.Abs(this.DecLongitude) > double.Epsilon)
                {
                    var xGeodms = "";
                    var yGeodms = "";
                    num1 = this.DecLatitude;
                    var str1 = num1.ToString(provider2);
                    num1 = this.DecLongitude;
                    var str2 = num1.ToString(provider2);
                    while (str1.Substring(str1.IndexOf(",", StringComparison.Ordinal) + 1).Length < 5)
                        str1 += "0";
                    while (str2.Substring(str2.IndexOf(",", StringComparison.Ordinal) + 1).Length < 5)
                        str2 += "0";
                    DffGpsTools.Geodec2Geodms(
                        (str1.Substring(0, str1.IndexOf(",", StringComparison.Ordinal)) + "," +
                         str1.Substring(str1.IndexOf(",", StringComparison.Ordinal) + 1, 5)).Replace(
                            ",", ""),
                        (str2.Substring(0, str2.IndexOf(",", StringComparison.Ordinal)) + "," +
                         str2.Substring(str2.IndexOf(",", StringComparison.Ordinal) + 1, 5)).Replace(
                            ",", ""), ref xGeodms, ref yGeodms);
                    this.DmsLatitude = double.Parse(xGeodms) / 100000.0;
                    this.DmsLongitude = double.Parse(yGeodms) / 100000.0;
                }
                var str3 = string.Format(provider2, "{0:0.00}", new object[]
                {
                    this.DmsLatitude * 100.0
                });
                var str4 = string.Format(provider2, "{0:0.00}", new object[]
                {
                    this.DmsLongitude * 100.0
                });
                var strArray1 = str3.Split(',');
                var strArray2 = str4.Split(',');
                var str5 = strArray1[0].Length == 4 ? "0" + strArray1[0] : "00" + strArray1[0];
                var str6 = strArray2[0].Length == 4 ? "0" + strArray2[0] : "00" + strArray2[0];
                var num2 = decimal.Parse(strArray1[1]) / new decimal(60) * new decimal(100);
                var str7 = num2.ToString(provider2).Replace(",", "");
                var str8 = str7.Length >= 10 ? str7.Substring(0, 9) : str7;
                for (var length = str8.Length; length < 10; ++length)
                    str8 += "0";
                num2 = decimal.Parse(strArray2[1]) / new decimal(60) * new decimal(100);
                var str9 = num2.ToString(provider2).Replace(",", "");
                var str10 = str9.Length >= 10 ? str9.Substring(0, 9) : str9;
                for (var length = str10.Length; length < 10; ++length)
                    str10 += "0";
                var str11 = str5 + "." + str8;
                var str12 = str6 + "." + str10;
                var str13 = this.DecLatitude > 0.0 ? "N" : "S";
                var str14 = this.DecLongitude > 0.0 ? "E" : "W";
                num1 = DffGpsTools.Kmh2Knoten(this.Speed);
                var str15 = num1.ToString(provider1);
                return "A#" + str11 + "#" + str13 + "#" + str12 + "#" + str14 + "#" + str15 + "#" + this.Heading;
            }
        }

        private void CalcualteDecValues()
        {
            var provider = (IFormatProvider) new CultureInfo("en-US", true);
            var xGeodec = "";
            var yGeodec = "";
            var flag1 = false;
            var flag2 = false;
            var str1 = this.DmsLatitude.ToString(provider);
            var str2 = this.DmsLongitude.ToString(provider);
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
            DffGpsTools.Geodms2Geodec(str1.Replace(".", ""), str2.Replace(".", ""), ref xGeodec, ref yGeodec);
            this._decLongitude = double.Parse(yGeodec) / 100000.0;
            this._decLatitude = double.Parse(xGeodec) / 100000.0;
        }

        private void CalculateMercatorValues()
        {
            if (Math.Abs(this._decLongitude) < double.Epsilon & Math.Abs(this._decLatitude) < double.Epsilon &
                Math.Abs(this.DmsLatitude) > double.Epsilon & Math.Abs(this.DmsLongitude) > double.Epsilon)
                this.CalcualteDecValues();
            var mercatorX = 0;
            var mercatorY = 0;
            DffGpsTools.GeodecimalToMercator(this._decLongitude, this._decLatitude, ref mercatorX, ref mercatorY);
            this._mrcX = mercatorX;
            this._mrcY = mercatorY;
        }

        public TimeSpan GetAgeOfPosition()
        {
            var dateTime = DateTime.Now;
            var ticks1 = dateTime.Ticks;
            dateTime = this.PositionTimestamp;
            var ticks2 = dateTime.Ticks;
            return new TimeSpan(ticks1 - ticks2);
        }
    }
}