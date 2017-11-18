using System;
using System.Globalization;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static string Invariant<T>(this T obj, Func<CultureInfo, string> action)
        {
            return action(CultureInfo.InvariantCulture);
        }

        public static string Invariant<T, T0>(this T obj, Func<T0, CultureInfo, string> action, T0 arg0)
        {
            return action(arg0, CultureInfo.InvariantCulture);
        }

        public static TX Invariant<T, TX>(this T obj, Func<CultureInfo, TX> action)
        {
            return action(CultureInfo.InvariantCulture);
        }

        public static TX Invariant<T, TX, T0>(this T obj, Func<T0, CultureInfo, TX> action, T0 arg0)
        {
            return action(arg0, CultureInfo.InvariantCulture);
        }
    }
}