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
        ///     Adds an offset to the value of an unsigned pointer.
        /// </summary>
        /// <param name="pointer">The unsigned pointer to add the offset to.</param>
        /// <param name="offset">The offset to add.</param>
        /// <returns>A new unsigned pointer that reflects the addition of  to .</returns>
        public static UIntPtr Add(this UIntPtr pointer, Int32 offset)
        {
            return UIntPtr.Add(pointer, offset);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Subtracts an offset from the value of an unsigned pointer.
        /// </summary>
        /// <param name="pointer">The unsigned pointer to subtract the offset from.</param>
        /// <param name="offset">The offset to subtract.</param>
        /// <returns>A new unsigned pointer that reflects the subtraction of  from .</returns>
        public static UIntPtr Subtract(this UIntPtr pointer, Int32 offset)
        {
            return UIntPtr.Subtract(pointer, offset);
        }
    }
}