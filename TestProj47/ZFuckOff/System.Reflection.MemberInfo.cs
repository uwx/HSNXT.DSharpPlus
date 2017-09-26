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
//using System.Reflection;

namespace TestProj47
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Retrieves a custom attribute applied to a member of a type. Parameters specify the member, and the type of
        ///     the custom attribute to search for.
        /// </summary>
        /// <param name="element">
        ///     An object derived from the  class that describes a constructor, event, field, method, or
        ///     property member of a class.
        /// </param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <returns>
        ///     A reference to the single custom attribute of type  that is applied to , or null if there is no such
        ///     attribute.
        /// </returns>
        public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType)
        {
            return Attribute.GetCustomAttribute(element, attributeType);
        }

        /// <summary>
        ///     Retrieves a custom attribute applied to a member of a type. Parameters specify the member, the type of the
        ///     custom attribute to search for, and whether to search ancestors of the member.
        /// </summary>
        /// <param name="element">
        ///     An object derived from the  class that describes a constructor, event, field, method, or
        ///     property member of a class.
        /// </param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <param name="inherit">If true, specifies to also search the ancestors of  for custom attributes.</param>
        /// <returns>
        ///     A reference to the single custom attribute of type  that is applied to , or null if there is no such
        ///     attribute.
        /// </returns>
        public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType, bool inherit)
        {
            return Attribute.GetCustomAttribute(element, attributeType, inherit);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Retrieves an array of the custom attributes applied to a member of a type. Parameters specify the member, and
        ///     the type of the custom attribute to search for.
        /// </summary>
        /// <param name="element">
        ///     An object derived from the  class that describes a constructor, event, field, method, or
        ///     property member of a class.
        /// </param>
        /// <param name="type">The type, or a base type, of the custom attribute to search for.</param>
        /// <returns>
        ///     An  array that contains the custom attributes of type  applied to , or an empty array if no such custom
        ///     attributes exist.
        /// </returns>
        public static Attribute[] GetCustomAttributes(this MemberInfo element, Type type)
        {
            return Attribute.GetCustomAttributes(element, type);
        }

        /// <summary>
        ///     Retrieves an array of the custom attributes applied to a member of a type. Parameters specify the member, the
        ///     type of the custom attribute to search for, and whether to search ancestors of the member.
        /// </summary>
        /// <param name="element">
        ///     An object derived from the  class that describes a constructor, event, field, method, or
        ///     property member of a class.
        /// </param>
        /// <param name="type">The type, or a base type, of the custom attribute to search for.</param>
        /// <param name="inherit">If true, specifies to also search the ancestors of  for custom attributes.</param>
        /// <returns>
        ///     An  array that contains the custom attributes of type  applied to , or an empty array if no such custom
        ///     attributes exist.
        /// </returns>
        public static Attribute[] GetCustomAttributes(this MemberInfo element, Type type, bool inherit)
        {
            return Attribute.GetCustomAttributes(element, type, inherit);
        }

        /// <summary>
        ///     Retrieves an array of the custom attributes applied to a member of a type. A parameter specifies the member.
        /// </summary>
        /// <param name="element">
        ///     An object derived from the  class that describes a constructor, event, field, method, or
        ///     property member of a class.
        /// </param>
        /// <returns>
        ///     An  array that contains the custom attributes applied to , or an empty array if no such custom attributes
        ///     exist.
        /// </returns>
        public static Attribute[] GetCustomAttributes(this MemberInfo element)
        {
            return Attribute.GetCustomAttributes(element);
        }

        /// <summary>
        ///     Retrieves an array of the custom attributes applied to a member of a type. Parameters specify the member, the
        ///     type of the custom attribute to search for, and whether to search ancestors of the member.
        /// </summary>
        /// <param name="element">
        ///     An object derived from the  class that describes a constructor, event, field, method, or
        ///     property member of a class.
        /// </param>
        /// <param name="inherit">If true, specifies to also search the ancestors of  for custom attributes.</param>
        /// <returns>
        ///     An  array that contains the custom attributes applied to , or an empty array if no such custom attributes
        ///     exist.
        /// </returns>
        public static Attribute[] GetCustomAttributes(this MemberInfo element, bool inherit)
        {
            return Attribute.GetCustomAttributes(element, inherit);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Determines whether any custom attributes are applied to a member of a type. Parameters specify the member,
        ///     and the type of the custom attribute to search for.
        /// </summary>
        /// <param name="element">
        ///     An object derived from the  class that describes a constructor, event, field, method, type,
        ///     or property member of a class.
        /// </param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <returns>true if a custom attribute of type  is applied to ; otherwise, false.</returns>
        public static bool IsDefined(this MemberInfo element, Type attributeType)
        {
            return Attribute.IsDefined(element, attributeType);
        }

        /// <summary>
        ///     Determines whether any custom attributes are applied to a member of a type. Parameters specify the member,
        ///     the type of the custom attribute to search for, and whether to search ancestors of the member.
        /// </summary>
        /// <param name="element">
        ///     An object derived from the  class that describes a constructor, event, field, method, type,
        ///     or property member of a class.
        /// </param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <param name="inherit">If true, specifies to also search the ancestors of  for custom attributes.</param>
        /// <returns>true if a custom attribute of type  is applied to ; otherwise, false.</returns>
        public static bool IsDefined(this MemberInfo element, Type attributeType, bool inherit)
        {
            return Attribute.IsDefined(element, attributeType, inherit);
        }
    }
}