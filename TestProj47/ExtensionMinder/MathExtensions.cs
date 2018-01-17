// Decompiled with JetBrains decompiler
// Type: ExtensionMinder.MathExtensions
// Assembly: ExtensionMinder, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9895DAEA-F52E-4F0C-B5FD-FB311AEFF61C
// Assembly location: ...\bin\Debug\ExtensionMinder.dll

using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static decimal MultiplyBy(this decimal dec, int multiple)
        {
            return dec * multiple;
        }

        public static decimal DivideBy(this decimal dec, int divider)
        {
            return dec / divider;
        }

        public static int MultiplyBy(this int i, int multiple)
        {
            return i * multiple;
        }

        public static int DivideBy(this int i, int divider)
        {
            return i / divider;
        }

        public static decimal ToCeiling(this decimal d)
        {
            return Math.Ceiling(d);
        }

        public static decimal RoundUp(this decimal d, int dp)
        {
            var num = Convert.ToDecimal(Math.Pow(10.0, dp));
            return Math.Ceiling(d * num) / num;
        }

        public static double RoundUp(this double d, int dp)
        {
            return (double) Convert.ToDecimal(d).RoundUp(dp);
        }

        public static decimal TruncateDecimal(this decimal d, int dp)
        {
            var num = Convert.ToDecimal(Math.Pow(10.0, dp));
            return Math.Truncate(d * num) / num;
        }

        public static bool IsBetween(this int x, int lower, int upper, bool includeBoundaries = true)
        {
            if (upper < lower)
                throw new ArgumentException("IsBetween: Upper limit must be greater then lower limit");
            if (includeBoundaries)
            {
                if (x >= lower)
                    return x <= upper;
                return false;
            }
            if (x > lower)
                return x < upper;
            return false;
        }

        public static bool IsBetween(this decimal x, decimal lower, decimal upper, bool includeBoundaries = true)
        {
            if (upper < lower)
                throw new ArgumentException("IsBetween: Upper limit must be greater then lower limit");
            if (includeBoundaries)
            {
                if (x >= lower)
                    return x <= upper;
                return false;
            }
            if (x > lower)
                return x < upper;
            return false;
        }

        public static bool IsInIncrementOf(this decimal x, decimal increment)
        {
            return Math.Floor(x / increment) == x / increment;
        }

        public static int GetCents(this decimal x)
        {
            return x.GetCents(2);
        }

        public static int GetCents(this decimal x, int places)
        {
            return (int) (Convert.ToDouble(x - decimal.Truncate(x)) * Math.Pow(10.0, places));
        }

        public static int RoundDownToNearest(this decimal x, int number)
        {
            return (int) Math.Floor(x / number) * number;
        }

        public static int RoundUpToNearest(this decimal x, int number)
        {
            return (int) Math.Ceiling(x / number) * number;
        }

        public static int ButNotLessThen(this int x, int number)
        {
            return Math.Max(x, number);
        }

        public static decimal Add(this decimal x, decimal number)
        {
            return x + number;
        }

        public static decimal Minus(this decimal x, decimal number)
        {
            return x - number;
        }

        public static int Minus(this int x, int number)
        {
            return x - number;
        }

        public static decimal Add(this decimal x, Func<decimal, decimal> func)
        {
            return x + func(x);
        }

        public static decimal Minus(this decimal x, Func<decimal, decimal> func)
        {
            return x - func(x);
        }
    }
}