using System;
using System.Drawing;
using System.Drawing.Drawing2D;

//using System.Data.SqlServerCe;

// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System;
//using System.Drawing;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Translates the specified  structure to an HTML string color representation.
        /// </summary>
        /// <param name="c">The  structure to translate.</param>
        /// <returns>The string that represents the HTML color.</returns>
        public static string ToHtml(this Color c)
        {
            return ColorTranslator.ToHtml(c);
        }

        /// <summary>
        ///     Translates the specified  structure to an OLE color.
        /// </summary>
        /// <param name="c">The  structure to translate.</param>
        /// <returns>The OLE color value.</returns>
        public static int ToOle(this Color c)
        {
            return ColorTranslator.ToOle(c);
        }

        /// <summary>
        ///     Translates the specified  structure to a Windows color.
        /// </summary>
        /// <param name="c">The  structure to translate.</param>
        /// <returns>The Windows color value.</returns>
        public static int ToWin32(this Color c)
        {
            return ColorTranslator.ToWin32(c);
        }

        /// <summary>
        ///     An Image extension method that cuts an image.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>The cutted image.</returns>
        public static Image Cut(this Image @this, int width, int height, int x, int y)
        {
            var r = new Bitmap(width, height);
            var destinationRectange = new Rectangle(0, 0, width, height);
            var sourceRectangle = new Rectangle(x, y, width, height);

            using (var g = Graphics.FromImage(r))
            {
                g.DrawImage(@this, destinationRectange, sourceRectangle, GraphicsUnit.Pixel);
            }

            return r;
        }

        /// <summary>
        ///     An Image extension method that scales an image to the specific ratio.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="ratio">The ratio.</param>
        /// <returns>The scaled image to the specific ratio.</returns>
        public static Image Scale(this Image @this, double ratio)
        {
            var width = Convert.ToInt32(@this.Width * ratio);
            var height = Convert.ToInt32(@this.Height * ratio);

            var r = new Bitmap(width, height);

            using (var g = Graphics.FromImage(r))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(@this, 0, 0, width, height);
            }

            return r;
        }

        /// <summary>
        ///     An Image extension method that scales an image to a specific with and height.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>The scaled image to the specific width and height.</returns>
        public static Image Scale(this Image @this, int width, int height)
        {
            var r = new Bitmap(width, height);

            using (var g = Graphics.FromImage(r))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(@this, 0, 0, width, height);
            }

            return r;
        }
    }
}