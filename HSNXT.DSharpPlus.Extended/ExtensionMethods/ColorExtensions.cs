using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.Extended.ExtensionMethods
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Converts a <see cref="DiscordColor"/> object into a hexadecimal string without a leading hash symbol.
        /// </summary>
        /// <param name="c">The color</param>
        /// <returns>The hexadecimal color string</returns>
        public static string ToHexString(this DiscordColor c) => $"{c.R:X2}{c.G:X2}{c.B:X2}";
    }
}
