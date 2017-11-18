using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        private const long Kilobyte = 1024;
        private const long Megabyte = Kilobyte * 1024;
        private const long Gigabyte = Megabyte * 1024;
        private const long Terabyte = Gigabyte * 1024;
        private const long Petabyte = Terabyte * 1024;
        private const long Exabyte = Petabyte * 1024;

        public static long Bytes(this int number)
        {
            return number;
        }

        public static long Kilobytes(this int number)
        {
            return number * Kilobyte;
        }

        public static long Megabytes(this int number)
        {
            return number * Megabyte;
        }

        public static long Gigabytes(this int number)
        {
            return number * Gigabyte;
        }

        public static long Terabytes(this int number)
        {
            return number * Terabyte;
        }

        public static long Petabytes(this int number)
        {
            return number * Petabyte;
        }

        public static long Exabytes(this int number)
        {
            return number * Exabyte;
        }

        public static void Times(this int value, Action action)
        {
            if (action == null) return;

            for (var i = 0; i < value; i++)
            {
                action();
            }
        }
    }
}