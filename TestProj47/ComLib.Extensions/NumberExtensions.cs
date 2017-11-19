using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        private static readonly string[] Sizes = { "B", "KB", "MB", "GB", "TB", "PB" };

        #region ToFileSizeString
        /// <summary>
        /// Template method for ToFileSizeString
        /// </summary>
        private static Tuple<int, double> FileSizeCalculation(double length)
        {
            var sizeIndex = 0;
            while (length >= 1024 && sizeIndex + 1 < Sizes.Length)
            {
                sizeIndex++;
                length = length / 1024;
            }

            return new Tuple<int, double>(sizeIndex, length);
        }

        /// <summary>
        /// Turns number into a file size string.
        /// <para/> Example: "9.54 MB"
        /// </summary>
        public static string ToFileSizeString(this long size)
        {
            var result = FileSizeCalculation(size);
            return $"{result.Item2:0.##} {Sizes[result.Item1]}";
        }

        /// <summary>
        /// Turns number into a file size string.
        /// <para/> Example: "9.54 MB"
        /// </summary>
        public static string ToFileSizeString(this int size)
        {
            var result = FileSizeCalculation(size);
            return $"{result.Item1:0.##} {result.Item2}";
        }

        /// <summary>
        /// Turns number into a file size string.
        /// <para/> Example: "9.54 MB"
        /// </summary>
        public static string ToFileSizeString(this double size)
        {
            var result = FileSizeCalculation(size);
            return $"{result.Item1:0.##} {result.Item2}";
        }
        #endregion
    }
}
