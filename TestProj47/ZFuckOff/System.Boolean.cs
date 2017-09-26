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

namespace TestProj47
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A bool extension method that execute an Action if the value is false.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="action">The action to execute.</param>
        public static void IfFalse(this bool @this, Action action)
        {
            if (!@this)
            {
                action();
            }
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A bool extension method that execute an Action if the value is true.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="action">The action to execute.</param>
        public static void IfTrue(this bool @this, Action action)
        {
            if (@this)
            {
                action();
            }
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A bool extension method that convert this object into a binary representation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A binary represenation of this object.</returns>
        public static byte ToBinary(this bool @this)
        {
            return Convert.ToByte(@this);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A bool extension method that show the trueValue when the @this value is true; otherwise show the falseValue.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="trueValue">The true value to be returned if the @this value is true.</param>
        /// <param name="falseValue">The false value to be returned if the @this value is false.</param>
        /// <returns>A string that represents of the current boolean value.</returns>
        public static string ToString(this bool @this, string trueValue, string falseValue)
        {
            return @this ? trueValue : falseValue;
        }
    }
}