using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

//using System.Data.SqlServerCe;

// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System;
//using System.Reflection;
//using System.Linq;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>An object extension method that gets the first custom attribute.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns>The custom attribute.</returns>
        public static object GetCustomAttribute(this object @this, Type attribute)
        {
            var type = @this.GetType();

            return type.IsEnum
                ? Attribute.GetCustomAttribute(type.GetField(@this.ToString()), attribute)
                : Attribute.GetCustomAttribute(type, attribute);
        }

        /// <summary>
        ///     An object extension method that gets the first custom attribute.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>The custom attribute.</returns>
        public static object GetCustomAttribute(this object @this, Type attribute, bool inherit)
        {
            var type = @this.GetType();

            return type.IsEnum
                ? Attribute.GetCustomAttribute(type.GetField(@this.ToString()), attribute, inherit)
                : Attribute.GetCustomAttribute(type, attribute, inherit);
        }

        /// <summary>An object extension method that gets custom attribute.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The custom attribute.</returns>
        public static T GetCustomAttribute<T>(this object @this) where T : Attribute
        {
            var type = @this.GetType();

            return (T) (type.IsEnum
                ? Attribute.GetCustomAttribute(type.GetField(@this.ToString()), typeof(T))
                : Attribute.GetCustomAttribute(type, typeof(T)));
        }

        /// <summary>
        ///     An object extension method that gets custom attribute.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>The custom attribute.</returns>
        public static T GetCustomAttribute<T>(this object @this, bool inherit) where T : Attribute
        {
            var type = @this.GetType();

            return (T) (type.IsEnum
                ? Attribute.GetCustomAttribute(type.GetField(@this.ToString()), typeof(T), inherit)
                : Attribute.GetCustomAttribute(type, typeof(T), inherit));
        }

        /// <summary>An object extension method that gets custom attribute.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The custom attribute.</returns>
        public static T GetCustomAttribute<T>(this MemberInfo @this) where T : Attribute
        {
            return (T) Attribute.GetCustomAttribute(@this, typeof(T));
        }

        /// <summary>An object extension method that gets custom attribute.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>The custom attribute.</returns>
        public static T GetCustomAttribute<T>(this MemberInfo @this, bool inherit) where T : Attribute
        {
            return (T) Attribute.GetCustomAttribute(@this, typeof(T), inherit);
        }

        /// <summary>A MemberInfo extension method that gets custom attribute by full name.</summary>
        /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
        /// <param name="this">The @this to act on.</param>
        /// <param name="fullName">Name of the full.</param>
        /// <returns>The custom attribute by full name.</returns>
        public static object GetCustomAttributeByFullName(this object @this, string fullName)
        {
            var type = @this.GetType();

            var attributes = type.IsEnum
                ? type.GetField(@this.ToString()).GetCustomAttributes().Where(x => x.GetType().FullName == fullName)
                    .ToArray()
                : type.GetCustomAttributes().Where(x => x.GetType().FullName == fullName).ToArray();

            if (attributes.Length == 0)
            {
                return null;
            }

            if (attributes.Length == 1)
            {
                return attributes[0];
            }

            throw new Exception(
                $"Ambiguous attribute. Multiple custom attributes of the same type found: {attributes.Length} attributes found.");
        }

        /// <summary>A MemberInfo extension method that gets custom attribute by full name.</summary>
        /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
        /// <param name="this">The @this to act on.</param>
        /// <param name="fullName">Name of the full.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>The custom attribute by full name.</returns>
        public static object GetCustomAttributeByFullName(this object @this, string fullName, bool inherit)
        {
            var type = @this.GetType();

            var attributes = type.IsEnum
                ? type.GetField(@this.ToString()).GetCustomAttributes(inherit)
                    .Where(x => x.GetType().FullName == fullName).ToArray()
                : type.GetCustomAttributes(inherit).Where(x => x.GetType().FullName == fullName).ToArray();

            if (attributes.Length == 0)
            {
                return null;
            }

            if (attributes.Length == 1)
            {
                return attributes[0];
            }

            throw new Exception(
                $"Ambiguous attribute. Multiple custom attributes of the same type found: {attributes.Length} attributes found.");
        }

        /// <summary>A MemberInfo extension method that gets custom attribute by full name.</summary>
        /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
        /// <param name="this">The @this to act on.</param>
        /// <param name="fullName">Name of the full.</param>
        /// <returns>The custom attribute by full name.</returns>
        public static object GetCustomAttributeByFullName(this MemberInfo @this, string fullName)
        {
            var attributes = @this.GetCustomAttributes().Where(x => x.GetType().FullName == fullName).ToArray();

            if (attributes.Length == 0)
            {
                return null;
            }

            if (attributes.Length == 1)
            {
                return attributes[0];
            }

            throw new Exception(
                $"Ambiguous attribute. Multiple custom attributes of the same type found: {attributes.Length} attributes found.");
        }

        /// <summary>A MemberInfo extension method that gets custom attribute by full name.</summary>
        /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
        /// <param name="this">The @this to act on.</param>
        /// <param name="fullName">Name of the full.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>The custom attribute by full name.</returns>
        public static object GetCustomAttributeByFullName(this MemberInfo @this, string fullName, bool inherit)
        {
            var attributes = @this.GetCustomAttributes(inherit).Where(x => x.GetType().FullName == fullName).ToArray();

            if (attributes.Length == 0)
            {
                return null;
            }

            if (attributes.Length == 1)
            {
                return attributes[0];
            }

            throw new Exception(
                $"Ambiguous attribute. Multiple custom attributes of the same type found: {attributes.Length} attributes found.");
        }

        /// <summary>An object extension method that gets custom attribute by name.</summary>
        /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
        /// <param name="this">The @this to act on.</param>
        /// <param name="name">The name.</param>
        /// <returns>The custom attribute by name.</returns>
        public static object GetCustomAttributeByName(this object @this, string name)
        {
            var type = @this.GetType();

            var attributes = type.IsEnum
                ? type.GetField(@this.ToString()).GetCustomAttributes().Where(x => x.GetType().Name == name).ToArray()
                : type.GetCustomAttributes().Where(x => x.GetType().Name == name).ToArray();

            if (attributes.Length == 0)
            {
                return null;
            }

            if (attributes.Length == 1)
            {
                return attributes[0];
            }

            throw new Exception(
                $"Ambiguous attribute. Multiple custom attributes of the same type found: {attributes.Length} attributes found.");
        }

        /// <summary>An object extension method that gets custom attribute by name.</summary>
        /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
        /// <param name="this">The @this to act on.</param>
        /// <param name="name">The name.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>The custom attribute by name.</returns>
        public static object GetCustomAttributeByName(this object @this, string name, bool inherit)
        {
            var type = @this.GetType();

            var attributes = type.IsEnum
                ? type.GetField(@this.ToString()).GetCustomAttributes(inherit).Where(x => x.GetType().Name == name)
                    .ToArray()
                : type.GetCustomAttributes(inherit).Where(x => x.GetType().Name == name).ToArray();

            if (attributes.Length == 0)
            {
                return null;
            }

            if (attributes.Length == 1)
            {
                return attributes[0];
            }

            throw new Exception(
                $"Ambiguous attribute. Multiple custom attributes of the same type found: {attributes.Length} attributes found.");
        }

        /// <summary>An object extension method that gets custom attribute by name.</summary>
        /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
        /// <param name="this">The @this to act on.</param>
        /// <param name="name">The name.</param>
        /// <returns>The custom attribute by name.</returns>
        public static object GetCustomAttributeByName(this MemberInfo @this, string name)
        {
            var attributes = @this.GetCustomAttributes().Where(x => x.GetType().Name == name).ToArray();

            if (attributes.Length == 0)
            {
                return null;
            }

            if (attributes.Length == 1)
            {
                return attributes[0];
            }

            throw new Exception(
                $"Ambiguous attribute. Multiple custom attributes of the same type found: {attributes.Length} attributes found.");
        }

        /// <summary>An object extension method that gets custom attribute by name.</summary>
        /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
        /// <param name="this">The @this to act on.</param>
        /// <param name="name">The name.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>The custom attribute by name.</returns>
        public static object GetCustomAttributeByName(this MemberInfo @this, string name, bool inherit)
        {
            var attributes = @this.GetCustomAttributes(inherit).Where(x => x.GetType().Name == name).ToArray();

            if (attributes.Length == 0)
            {
                return null;
            }

            if (attributes.Length == 1)
            {
                return attributes[0];
            }

            throw new Exception(
                $"Ambiguous attribute. Multiple custom attributes of the same type found: {attributes.Length} attributes found.");
        }

        /// <summary>
        ///     An object extension method that gets description attribute.
        /// </summary>
        /// <param name="value">The value to act on.</param>
        /// <returns>The description attribute.</returns>
        public static string GetCustomAttributeDescription(this object value)
        {
            var type = value.GetType();

            var attributes = type.IsEnum
                ? type.GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute))
                : type.GetCustomAttributes(typeof(DescriptionAttribute));

            if (attributes.Length == 0)
            {
                return null;
            }
            if (attributes.Length == 1)
            {
                return ((DescriptionAttribute) attributes[0]).Description;
            }

            throw new Exception(
                $"Ambiguous attribute. Multiple custom attributes of the same type found: {attributes.Length} attributes found.");
        }

        /// <summary>An object extension method that gets description attribute.</summary>
        /// <param name="value">The value to act on.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>The description attribute.</returns>
        public static string GetCustomAttributeDescription(this object value, bool inherit)
        {
            var type = value.GetType();

            var attributes = type.IsEnum
                ? type.GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), inherit)
                : type.GetCustomAttributes(typeof(DescriptionAttribute));

            if (attributes.Length == 0)
            {
                return null;
            }
            if (attributes.Length == 1)
            {
                return ((DescriptionAttribute) attributes[0]).Description;
            }

            throw new Exception(
                $"Ambiguous attribute. Multiple custom attributes of the same type found: {attributes.Length} attributes found.");
        }

        /// <summary>An object extension method that gets description attribute.</summary>
        /// <param name="value">The value to act on.</param>
        /// <returns>The description attribute.</returns>
        public static string GetCustomAttributeDescription(this MemberInfo value)
        {
            var attributes = value.GetCustomAttributes(typeof(DescriptionAttribute));

            if (attributes.Length == 0)
            {
                return null;
            }
            if (attributes.Length == 1)
            {
                return ((DescriptionAttribute) attributes[0]).Description;
            }

            throw new Exception(
                $"Ambiguous attribute. Multiple custom attributes of the same type found: {attributes.Length} attributes found.");
        }

        /// <summary>An object extension method that gets description attribute.</summary>
        /// <param name="value">The value to act on.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>The description attribute.</returns>
        public static string GetCustomAttributeDescription(this MemberInfo value, bool inherit)
        {
            var attributes = value.GetCustomAttributes(typeof(DescriptionAttribute), inherit);

            if (attributes.Length == 0)
            {
                return null;
            }
            if (attributes.Length == 1)
            {
                return ((DescriptionAttribute) attributes[0]).Description;
            }

            throw new Exception(
                $"Ambiguous attribute. Multiple custom attributes of the same type found: {attributes.Length} attributes found.");
        }

        /// <summary>An object extension method that gets custom attributes.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>An array of object.</returns>
        public static Attribute[] GetCustomAttributes(this object @this)
        {
            var type = @this.GetType();

            return type.IsEnum ? type.GetField(@this.ToString()).GetCustomAttributes() : type.GetCustomAttributes();
        }

        /// <summary>
        ///     An object extension method that gets custom attributes.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>An array of object.</returns>
        public static object[] GetCustomAttributes(this object @this, bool inherit)
        {
            var type = @this.GetType();

            return type.IsEnum
                ? type.GetField(@this.ToString()).GetCustomAttributes(inherit)
                : type.GetCustomAttributes(inherit);
        }

        /// <summary>An object extension method that gets custom attributes.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>An array of object.</returns>
        public static T[] GetCustomAttributes<T>(this object @this) where T : Attribute
        {
            var type = @this.GetType();

            return (T[]) (type.IsEnum
                ? type.GetField(@this.ToString()).GetCustomAttributes(typeof(T))
                : type.GetCustomAttributes(typeof(T)));
        }

        /// <summary>
        ///     An object extension method that gets custom attributes.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>An array of object.</returns>
        public static T[] GetCustomAttributes<T>(this object @this, bool inherit) where T : Attribute
        {
            var type = @this.GetType();

            return (T[]) (type.IsEnum
                ? type.GetField(@this.ToString()).GetCustomAttributes(typeof(T), inherit)
                : type.GetCustomAttributes(typeof(T), inherit));
        }

        /// <summary>An object extension method that gets custom attributes.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>An array of object.</returns>
        public static T[] GetCustomAttributes<T>(this MemberInfo @this) where T : Attribute
        {
            return (T[]) @this.GetCustomAttributes(typeof(T));
        }

        /// <summary>An object extension method that gets custom attributes.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>An array of object.</returns>
        public static T[] GetCustomAttributes<T>(this MemberInfo @this, bool inherit) where T : Attribute
        {
            return (T[]) @this.GetCustomAttributes(typeof(T), inherit);
        }

        /// <summary>An object extension method that gets custom attributes by full name.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="fullName">Name of the full.</param>
        /// <returns>An array of attribute.</returns>
        public static Attribute[] GetCustomAttributesByFullName(this object @this, string fullName)
        {
            var type = @this.GetType();

            return type.IsEnum
                ? type.GetField(@this.ToString()).GetCustomAttributes().Where(x => x.GetType().FullName == fullName)
                    .ToArray()
                : type.GetCustomAttributes().Where(x => x.GetType().FullName == fullName).ToArray();
        }

        /// <summary>An object extension method that gets custom attributes by full name.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="fullName">Name of the full.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>An array of attribute.</returns>
        public static object[] GetCustomAttributesByFullName(this object @this, string fullName, bool inherit)
        {
            var type = @this.GetType();

            return type.IsEnum
                ? type.GetField(@this.ToString()).GetCustomAttributes(inherit)
                    .Where(x => x.GetType().FullName == fullName).ToArray()
                : type.GetCustomAttributes(inherit).Where(x => x.GetType().FullName == fullName).ToArray();
        }

        /// <summary>An object extension method that gets custom attributes by full name.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="fullName">Name of the full.</param>
        /// <returns>An array of attribute.</returns>
        public static Attribute[] GetCustomAttributesByFullName(this MemberInfo @this, string fullName)
        {
            return @this.GetCustomAttributes().Where(x => x.GetType().FullName == fullName).ToArray();
        }

        /// <summary>An object extension method that gets custom attributes by full name.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="fullName">Name of the full.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>An array of attribute.</returns>
        public static object[] GetCustomAttributesByFullName(this MemberInfo @this, string fullName, bool inherit)
        {
            return @this.GetCustomAttributes(inherit).Where(x => x.GetType().FullName == fullName).ToArray();
        }

        /// <summary>An object extension method that gets custom attributes by name.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="name">The name.</param>
        /// <returns>An array of attribute.</returns>
        public static Attribute[] GetCustomAttributesByName(this object @this, string name)
        {
            var type = @this.GetType();

            return type.IsEnum
                ? type.GetField(@this.ToString()).GetCustomAttributes().Where(x => x.GetType().Name == name).ToArray()
                : type.GetCustomAttributes().Where(x => x.GetType().Name == name).ToArray();
        }

        /// <summary>An object extension method that gets custom attributes by name.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="name">The name.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>An array of attribute.</returns>
        public static object[] GetCustomAttributesByName(this object @this, string name, bool inherit)
        {
            var type = @this.GetType();

            return type.IsEnum
                ? type.GetField(@this.ToString()).GetCustomAttributes(inherit).Where(x => x.GetType().Name == name)
                    .ToArray()
                : type.GetCustomAttributes(inherit).Where(x => x.GetType().Name == name).ToArray();
        }

        /// <summary>An object extension method that gets custom attributes by name.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="name">The name.</param>
        /// <returns>An array of attribute.</returns>
        public static Attribute[] GetCustomAttributesByName(this MemberInfo @this, string name)
        {
            return @this.GetCustomAttributes().Where(x => x.GetType().Name == name).ToArray();
        }

        /// <summary>An object extension method that gets custom attributes by name.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="name">The name.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>An array of attribute.</returns>
        public static object[] GetCustomAttributesByName(this MemberInfo @this, string name, bool inherit)
        {
            return @this.GetCustomAttributes(inherit).Where(x => x.GetType().Name == name).ToArray();
        }

        /// <summary>A T extension method that searches for the public field with the specified name.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="name">The string containing the name of the data field to get.</param>
        /// <returns>
        ///     An object representing the field that matches the specified requirements, if found;
        ///     otherwise, null.
        /// </returns>
        public static FieldInfo GetField<T>(this T @this, string name)
        {
            return @this.GetType().GetField(name);
        }

        /// <summary>
        ///     A T extension method that searches for the specified field, using the specified
        ///     binding constraints.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="name">The string containing the name of the data field to get.</param>
        /// <param name="bindingAttr">
        ///     A bitmask comprised of one or more BindingFlags that specify how the
        ///     search is conducted.
        /// </param>
        /// <returns>
        ///     An object representing the field that matches the specified requirements, if found;
        ///     otherwise, null.
        /// </returns>
        public static FieldInfo GetField<T>(this T @this, string name, BindingFlags bindingAttr)
        {
            return @this.GetType().GetField(name, bindingAttr);
        }

        /// <summary>An object extension method that gets the fields.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>An array of field information.</returns>
        public static FieldInfo[] GetFields(this object @this)
        {
            return @this.GetType().GetFields();
        }

        /// <summary>An object extension method that gets the fields.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <returns>An array of field information.</returns>
        public static FieldInfo[] GetFields(this object @this, BindingFlags bindingAttr)
        {
            return @this.GetType().GetFields(bindingAttr);
        }

        /// <summary>
        ///     A T extension method that gets a field value (Public | NonPublic | Instance | Static)
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>The field value.</returns>
        public static object GetFieldValue<T>(this T @this, string fieldName)
        {
            var type = @this.GetType();
            var field = type.GetField(fieldName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            return field.GetValue(@this);
        }

        /// <summary>A T extension method that gets member paths.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="path">Full pathname of the file.</param>
        /// <returns>An array of member information.</returns>
        public static MemberInfo[] GetMemberPaths<T>(this T @this, string path)
        {
            var lastType = @this.GetType();
            var paths = path.Split('.');

            var memberPaths = new List<MemberInfo>();

            foreach (var item in paths)
            {
                var propertyInfo = lastType.GetProperty(item);
                var fieldInfo = lastType.GetField(item);

                if (propertyInfo != null)
                {
                    memberPaths.Add(propertyInfo);
                    lastType = propertyInfo.PropertyType;
                }
                if (fieldInfo != null)
                {
                    memberPaths.Add(fieldInfo);
                    lastType = fieldInfo.FieldType;
                }
            }

            return memberPaths.ToArray();
        }

        /// <summary>
        ///     A T extension method that searches for the public method with the specified name.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="name">The string containing the name of the public method to get.</param>
        /// <returns>
        ///     An object that represents the public method with the specified name, if found; otherwise, null.
        /// </returns>
        public static MethodInfo GetMethod<T>(this T @this, string name)
        {
            return @this.GetType().GetMethod(name);
        }

        /// <summary>
        ///     A T extension method that searches for the specified method whose parameters match the specified argument
        ///     types and modifiers, using the specified binding constraints.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="name">The string containing the name of the public method to get.</param>
        /// <param name="bindingAttr">A bitmask comprised of one or more BindingFlags that specify how the search is conducted.</param>
        /// <returns>
        ///     An object that represents the public method with the specified name, if found; otherwise, null.
        /// </returns>
        public static MethodInfo GetMethod<T>(this T @this, string name, BindingFlags bindingAttr)
        {
            return @this.GetType().GetMethod(name, bindingAttr);
        }

        /// <summary>
        ///     A T extension method that returns all the public methods of the current Type.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>
        ///     An array of MethodInfo objects representing all the public methods defined for the current Type. or An empty
        ///     array of type MethodInfo, if no public methods are defined for the current Type.
        /// </returns>
        public static MethodInfo[] GetMethods<T>(this T @this)
        {
            return @this.GetType().GetMethods();
        }

        /// <summary>
        ///     A T extension method that searches for the methods defined for the current Type, using the specified binding
        ///     constraints.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="bindingAttr">A bitmask comprised of one or more BindingFlags that specify how the search is conducted.</param>
        /// <returns>
        ///     An array of MethodInfo objects representing all methods defined for the current Type that match the specified
        ///     binding constraints. or An empty array of type MethodInfo, if no methods are defined for the current Type, or
        ///     if none of the defined methods match the binding constraints.
        /// </returns>
        public static MethodInfo[] GetMethods<T>(this T @this, BindingFlags bindingAttr)
        {
            return @this.GetType().GetMethods(bindingAttr);
        }

        /// <summary>An object extension method that gets the properties.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>An array of property information.</returns>
        public static PropertyInfo[] GetProperties(this object @this)
        {
            return @this.GetType().GetProperties();
        }

        /// <summary>An object extension method that gets the properties.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <returns>An array of property information.</returns>
        public static PropertyInfo[] GetProperties(this object @this, BindingFlags bindingAttr)
        {
            return @this.GetType().GetProperties(bindingAttr);
        }

        /// <summary>
        ///     A T extension method that gets a property.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="name">The name.</param>
        /// <returns>The property.</returns>
        public static PropertyInfo GetProperty<T>(this T @this, string name)
        {
            return @this.GetType().GetProperty(name);
        }

        /// <summary>
        ///     A T extension method that gets a property.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="name">The name.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <returns>The property.</returns>
        public static PropertyInfo GetProperty<T>(this T @this, string name, BindingFlags bindingAttr)
        {
            return @this.GetType().GetProperty(name, bindingAttr);
        }

        /// <summary>A T extension method that gets property or field.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="name">The name.</param>
        /// <returns>The property or field.</returns>
        public static MemberInfo GetPropertyOrField<T>(this T @this, string name)
        {
            var property = @this.GetProperty(name);
            if (property != null)
            {
                return property;
            }

            var field = @this.GetField(name);
            if (field != null)
            {
                return field;
            }

            return null;
        }

        /// <summary>
        ///     A T extension method that gets property value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The property value.</returns>
        public static object GetPropertyValue<T>(this T @this, string propertyName)
        {
            var type = @this.GetType();
            var property = type.GetProperty(propertyName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            return property.GetValue(@this, null);
        }

        /// <summary>
        ///     An object extension method that executes the method on a different thread, and waits for the result.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="obj">The obj to act on.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <returns>An object.</returns>
        public static object InvokeMethod<T>(this T obj, string methodName, params object[] parameters)
        {
            var type = obj.GetType();
            var method = type.GetMethod(methodName, parameters.Select(o => o.GetType()).ToArray());

            return method.Invoke(obj, parameters);
        }

        /// <summary>
        ///     An object extension method that executes the method on a different thread, and waits for the result.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="obj">The obj to act on.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <returns>A T.</returns>
        public static T InvokeMethod<T>(this object obj, string methodName, params object[] parameters)
        {
            var type = obj.GetType();
            var method = type.GetMethod(methodName, parameters.Select(o => o.GetType()).ToArray());

            var value = method.Invoke(obj, parameters);
            return value is T ? (T) value : default;
        }

        /// <summary>
        ///     An object extension method that query if '@this' is attribute defined.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="attributeType">Type of the attribute.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>true if attribute defined, false if not.</returns>
        public static bool IsAttributeDefined(this object @this, Type attributeType, bool inherit)
        {
            return @this.GetType().IsDefined(attributeType, inherit);
        }

        /// <summary>
        ///     An object extension method that query if '@this' is attribute defined.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="inherit">true to inherit.</param>
        /// <returns>true if attribute defined, false if not.</returns>
        public static bool IsAttributeDefined<T>(this object @this, bool inherit) where T : Attribute
        {
            return @this.GetType().IsDefined(typeof(T), inherit);
        }

        /// <summary>
        ///     A T extension method that sets field value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="value">The value.</param>
        public static void SetFieldValue<T>(this T @this, string fieldName, object value)
        {
            var type = @this.GetType();
            var field = type.GetField(fieldName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            field.SetValue(@this, value);
        }

        /// <summary>
        ///     A T extension method that sets property value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        public static void SetPropertyValue<T>(this T @this, string propertyName, object value)
        {
            var type = @this.GetType();
            var property = type.GetProperty(propertyName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            property.SetValue(@this, value, null);
        }

        /// <summary>
        ///     A T extension method that query if '@this' is array.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if array, false if not.</returns>
        public static bool IsArray<T>(this T @this)
        {
            return @this.GetType().IsArray;
        }

        /// <summary>
        ///     A T extension method that query if '@this' is class.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if class, false if not.</returns>
        public static bool IsClass<T>(this T @this)
        {
            return @this.GetType().IsClass;
        }

        /// <summary>
        ///     A T extension method that query if '@this' is enum.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if enum, false if not.</returns>
        public static bool IsEnum<T>(this T @this)
        {
            return @this.GetType().IsEnum;
        }

        /// <summary>
        ///     A T extension method that query if '@this' is subclass of.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="type">The Type to process.</param>
        /// <returns>true if subclass of, false if not.</returns>
        public static bool IsSubclassOf<T>(this T @this, Type type)
        {
            return @this.GetType().IsSubclassOf(type);
        }

        /// <summary>
        ///     A T extension method that query if '@this' is type of.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="type">The type.</param>
        /// <returns>true if type of, false if not.</returns>
        public static bool IsTypeOf<T>(this T @this, Type type)
        {
            return @this.GetType() == type;
        }

        /// <summary>
        ///     A T extension method that query if '@this' is type or inherits of.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="type">The type.</param>
        /// <returns>true if type or inherits of, false if not.</returns>
        public static bool IsTypeOrInheritsOf<T>(this T @this, Type type)
        {
            var objectType = @this.GetType();

            while (true)
            {
                if (objectType.Equals(type))
                {
                    return true;
                }

                if (objectType == objectType.BaseType || objectType.BaseType == null)
                {
                    return false;
                }

                objectType = objectType.BaseType;
            }
        }
    }
}