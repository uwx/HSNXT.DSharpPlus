using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        /// Returns a DateTime with its value set to Now minus the provided TimeSpan value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AgoUtc(this TimeSpan value)
        {
            return DateTime.UtcNow.Subtract(value);
        }

        /// <summary>
        /// Returns a DateTime with its value set to Now plus the provided TimeSpan value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime FromNowUtc(this TimeSpan value)
        {
            return DateTime.UtcNow.Add(value);
        }
    }
}