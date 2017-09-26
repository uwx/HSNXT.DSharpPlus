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

//using System.Net.Mail;

namespace TestProj47
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A MailMessage extension method that send this message.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void Send(this MailMessage @this)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Send(@this);
            }
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A MailMessage extension method that sends this message asynchronous.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="userToken">The user token.</param>
        public static void SendAsync(this MailMessage @this, object userToken)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.SendAsync(@this, userToken);
            }
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A WebRequest extension method that gets the WebRequest response or the WebException response.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The WebRequest response or WebException response.</returns>
        public static WebResponse GetResponseSafe(this WebRequest @this)
        {
            try
            {
                return @this.GetResponse();
            }
            catch (WebException e)
            {
                return e.Response;
            }
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A WebResponse extension method that reads the response stream to the end.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The response stream as a string, from the current position to the end.</returns>
        public static string ReadToEnd(this WebResponse @this)
        {
            using (Stream stream = @this.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     A WebRequest extension method that gets the WebRequest response and read the response stream.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The response stream as a string, from the current position to the end.</returns>
        public static string ReadToEndAndDispose(this WebResponse @this)
        {
            using (WebResponse response = @this)
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream, Encoding.Default))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}