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
//using System.Globalization;
//using System.Reflection;

namespace TestProj47
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A Type extension method that creates an instance.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <param name="binder">The binder.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The new instance.</returns>
        public static T CreateInstance<T>(this Type @this, BindingFlags bindingAttr, Binder binder, Object[] args,
            CultureInfo culture)
        {
            return (T) Activator.CreateInstance(@this, bindingAttr, binder, args, culture);
        }

        /// <summary>
        ///     A Type extension method that creates an instance.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <param name="binder">The binder.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="activationAttributes">The activation attributes.</param>
        /// <returns>The new instance.</returns>
        public static T CreateInstance<T>(this Type @this, BindingFlags bindingAttr, Binder binder, Object[] args,
            CultureInfo culture, Object[] activationAttributes)
        {
            return (T) Activator.CreateInstance(@this, bindingAttr, binder, args, culture, activationAttributes);
        }

        /// <summary>
        ///     A Type extension method that creates an instance.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The new instance.</returns>
        public static T CreateInstance<T>(this Type @this, Object[] args)
        {
            return (T) Activator.CreateInstance(@this, args);
        }

        /// <summary>
        ///     A Type extension method that creates an instance.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="activationAttributes">The activation attributes.</param>
        /// <returns>The new instance.</returns>
        public static T CreateInstance<T>(this Type @this, Object[] args, Object[] activationAttributes)
        {
            return (T) Activator.CreateInstance(@this, args, activationAttributes);
        }

        /// <summary>
        ///     A Type extension method that creates an instance.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The new instance.</returns>
        public static T CreateInstance<T>(this Type @this)
        {
            return (T) Activator.CreateInstance(@this);
        }

        /// <summary>
        ///     A Type extension method that creates an instance.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="nonPublic">true to non public.</param>
        /// <returns>The new instance.</returns>
        public static T CreateInstance<T>(this Type @this, Boolean nonPublic)
        {
            return (T) Activator.CreateInstance(@this, nonPublic);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Creates an instance of the specified type using the constructor that best matches the specified parameters.
        /// </summary>
        /// <param name="type">The type of object to create.</param>
        /// <param name="bindingAttr">
        ///     A combination of zero or more bit flags that affect the search for the  constructor. If
        ///     is zero, a case-sensitive search for public constructors is conducted.
        /// </param>
        /// <param name="binder">
        ///     An object that uses  and  to seek and identify the  constructor. If  is null, the default
        ///     binder is used.
        /// </param>
        /// <param name="args">
        ///     An array of arguments that match in number, order, and type the parameters of the constructor
        ///     to invoke. If  is an empty array or null, the constructor that takes no parameters (the default constructor) is
        ///     invoked.
        /// </param>
        /// <param name="culture">
        ///     Culture-specific information that governs the coercion of  to the formal types declared for
        ///     the  constructor. If  is null, the  for the current thread is used.
        /// </param>
        /// <returns>A reference to the newly created object.</returns>
        public static Object CreateInstance(this Type type, BindingFlags bindingAttr, Binder binder, Object[] args,
            CultureInfo culture)
        {
            return Activator.CreateInstance(type, bindingAttr, binder, args, culture);
        }

        /// <summary>
        ///     Creates an instance of the specified type using the constructor that best matches the specified parameters.
        /// </summary>
        /// <param name="type">The type of object to create.</param>
        /// <param name="bindingAttr">
        ///     A combination of zero or more bit flags that affect the search for the  constructor. If
        ///     is zero, a case-sensitive search for public constructors is conducted.
        /// </param>
        /// <param name="binder">
        ///     An object that uses  and  to seek and identify the  constructor. If  is null, the default
        ///     binder is used.
        /// </param>
        /// <param name="args">
        ///     An array of arguments that match in number, order, and type the parameters of the constructor
        ///     to invoke. If  is an empty array or null, the constructor that takes no parameters (the default constructor) is
        ///     invoked.
        /// </param>
        /// <param name="culture">
        ///     Culture-specific information that governs the coercion of  to the formal types declared for
        ///     the  constructor. If  is null, the  for the current thread is used.
        /// </param>
        /// <param name="activationAttributes">
        ///     An array of one or more attributes that can participate in activation. This
        ///     is typically an array that contains a single  object. The  specifies the URL that is required to activate a
        ///     remote object.
        /// </param>
        /// <returns>A reference to the newly created object.</returns>
        public static Object CreateInstance(this Type type, BindingFlags bindingAttr, Binder binder, Object[] args,
            CultureInfo culture, Object[] activationAttributes)
        {
            return Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
        }

        /// <summary>
        ///     Creates an instance of the specified type using the constructor that best matches the specified parameters.
        /// </summary>
        /// <param name="type">The type of object to create.</param>
        /// <param name="args">
        ///     An array of arguments that match in number, order, and type the parameters of the constructor
        ///     to invoke. If  is an empty array or null, the constructor that takes no parameters (the default constructor) is
        ///     invoked.
        /// </param>
        /// <returns>A reference to the newly created object.</returns>
        public static Object CreateInstance(this Type type, Object[] args)
        {
            return Activator.CreateInstance(type, args);
        }

        /// <summary>
        ///     Creates an instance of the specified type using the constructor that best matches the specified parameters.
        /// </summary>
        /// <param name="type">The type of object to create.</param>
        /// <param name="args">
        ///     An array of arguments that match in number, order, and type the parameters of the constructor
        ///     to invoke. If  is an empty array or null, the constructor that takes no parameters (the default constructor) is
        ///     invoked.
        /// </param>
        /// <param name="activationAttributes">
        ///     An array of one or more attributes that can participate in activation. This
        ///     is typically an array that contains a single  object. The  specifies the URL that is required to activate a
        ///     remote object.
        /// </param>
        /// <returns>A reference to the newly created object.</returns>
        public static Object CreateInstance(this Type type, Object[] args, Object[] activationAttributes)
        {
            return Activator.CreateInstance(type, args, activationAttributes);
        }

        /// <summary>
        ///     Creates an instance of the specified type using that type&#39;s default constructor.
        /// </summary>
        /// <param name="type">The type of object to create.</param>
        /// <returns>A reference to the newly created object.</returns>
        public static Object CreateInstance(this Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        ///     Creates an instance of the specified type using that type&#39;s default constructor.
        /// </summary>
        /// <param name="type">The type of object to create.</param>
        /// <param name="nonPublic">
        ///     true if a public or nonpublic default constructor can match; false if only a public
        ///     default constructor can match.
        /// </param>
        /// <returns>A reference to the newly created object.</returns>
        public static Object CreateInstance(this Type type, Boolean nonPublic)
        {
            return Activator.CreateInstance(type, nonPublic);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Creates a proxy for the well-known object indicated by the specified type and URL.
        /// </summary>
        /// <param name="type">The type of the well-known object to which you want to connect.</param>
        /// <param name="url">The URL of the well-known object.</param>
        /// <returns>A proxy that points to an endpoint served by the requested well-known object.</returns>
        public static Object GetObject(this Type type, String url)
        {
            return Activator.GetObject(type, url);
        }

        /// <summary>
        ///     Creates a proxy for the well-known object indicated by the specified type, URL, and channel data.
        /// </summary>
        /// <param name="type">The type of the well-known object to which you want to connect.</param>
        /// <param name="url">The URL of the well-known object.</param>
        /// <param name="state">Channel-specific data or null.</param>
        /// <returns>A proxy that points to an endpoint served by the requested well-known object.</returns>
        public static Object GetObject(this Type type, String url, Object state)
        {
            return Activator.GetObject(type, url, state);
        }
    }
}