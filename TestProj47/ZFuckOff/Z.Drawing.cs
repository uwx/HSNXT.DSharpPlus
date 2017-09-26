using System;
using System.Linq.Expressions;
using System.Runtime.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Collections.Specialized;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Data;
using System.Drawing;
using System.Web;
using System.Globalization;
using System.ComponentModel;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Data.Entity.Design.PluralizationServices;
using System.Security;
using System.Xml.Linq;
using System.Xml;
using System.Collections.ObjectModel;
using System.Data.Common;
//using System.Data.SqlServerCe;
using System.Drawing.Drawing2D;
using System.Security.AccessControl;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.Web.UI;
using System.Windows.Forms;
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System;
//using System.Drawing;

namespace TestProj47
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Translates the specified  structure to an HTML string color representation.
        /// </summary>
        /// <param name="c">The  structure to translate.</param>
        /// <returns>The string that represents the HTML color.</returns>
        public static String ToHtml(this Color c)
        {
            return ColorTranslator.ToHtml(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Translates the specified  structure to an OLE color.
        /// </summary>
        /// <param name="c">The  structure to translate.</param>
        /// <returns>The OLE color value.</returns>
        public static Int32 ToOle(this Color c)
        {
            return ColorTranslator.ToOle(c);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Translates the specified  structure to a Windows color.
        /// </summary>
        /// <param name="c">The  structure to translate.</param>
        /// <returns>The Windows color value.</returns>
        public static Int32 ToWin32(this Color c)
        {
            return ColorTranslator.ToWin32(c);
        }
    }

    public static partial class Extensions
    {
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

            using (Graphics g = Graphics.FromImage(r))
            {
                g.DrawImage(@this, destinationRectange, sourceRectangle, GraphicsUnit.Pixel);
            }

            return r;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     An Image extension method that scales an image to the specific ratio.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="ratio">The ratio.</param>
        /// <returns>The scaled image to the specific ratio.</returns>
        public static Image Scale(this Image @this, double ratio)
        {
            int width = Convert.ToInt32(@this.Width * ratio);
            int height = Convert.ToInt32(@this.Height * ratio);

            var r = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(r))
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

            using (Graphics g = Graphics.FromImage(r))
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