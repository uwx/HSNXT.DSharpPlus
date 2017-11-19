using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        /// Determines whether [is midnight exactly] [the specified t].
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>
        /// 	<c>true</c> if [is midnight exactly] [the specified t]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMidnightExactly(this TimeSpan t)
        {
            return t.Hours == 0 && t.Minutes == 0 && t.Seconds == 0;
        }


        /// <summary>
        /// Get simple time format
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ToMilitaryString(this TimeSpan t)
        {
            var time = t.Hours + ":" + t.Minutes + ":" + t.Seconds;

            if (t.Days > 0)
                time = t.Days + "dys " + time;

            return time;
        }


        /// <summary>
        /// Returns an integer representing mii
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int ToMilitaryInt(this TimeSpan t)
        {
            var time = t.Hours * 100;
            time += t.Minutes;
            return time;
        }
    }
}
