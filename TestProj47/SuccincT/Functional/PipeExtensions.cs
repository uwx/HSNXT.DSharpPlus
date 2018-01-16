using System;

namespace HSNXT
{
    public partial class Extensions
    {
        public static TR Into<T, TR>(this T value, Func<T, TR > func) => func(value);

        public static void Into<T>(this T value, Action<T> action) => action(value);
    }
}
