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
        ///     An EventHandler extension method that raises the event event.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="sender">Source of the event.</param>
        public static void RaiseEvent(this EventHandler @this, object sender)
        {
            if (@this != null)
            {
                @this(sender, null);
            }
        }

        /// <summary>
        ///     An EventHandler extension method that raises.
        /// </summary>
        /// <param name="handler">The handler to act on.</param>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event information.</param>
        public static void Raise(this EventHandler handler, object sender, EventArgs e)
        {
            if (handler != null)
                handler(sender, e);
        }

        /// <summary>
        ///     An EventHandler&lt;TEventArgs&gt; extension method that raises the event event.
        /// </summary>
        /// <typeparam name="TEventArgs">Type of the event arguments.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="sender">Source of the event.</param>
        public static void RaiseEvent<TEventArgs>(this EventHandler<TEventArgs> @this, object sender)
            where TEventArgs : EventArgs
        {
            if (@this != null)
            {
                @this(sender, Activator.CreateInstance<TEventArgs>());
            }
        }

        /// <summary>
        ///     An EventHandler&lt;TEventArgs&gt; extension method that raises the event event.
        /// </summary>
        /// <typeparam name="TEventArgs">Type of the event arguments.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event information to send to registered event handlers.</param>
        public static void RaiseEvent<TEventArgs>(this EventHandler<TEventArgs> @this, object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (@this != null)
            {
                @this(sender, e);
            }
        }
    }
}