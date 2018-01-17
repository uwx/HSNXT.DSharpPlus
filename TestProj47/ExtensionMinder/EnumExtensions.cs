// Decompiled with JetBrains decompiler
// Type: ExtensionMinder.EnumExtensions
// Assembly: ExtensionMinder, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9895DAEA-F52E-4F0C-B5FD-FB311AEFF61C
// Assembly location: ...\bin\Debug\ExtensionMinder.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static T ParseAsEnumByDescriptionAttribute<T>(this string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException(description, "Cannot parse an empty description");
            if (!typeof(T).IsEnum)
                throw new InvalidOperationException($"Invalid Enum type{typeof(T)}");
            foreach (T obj in Enum.GetValues(typeof(T)))
            {
                var customAttributes = (DescriptionAttribute[]) obj.GetType().GetField(obj.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (customAttributes.Length > 0 && customAttributes[0].Description.ToUpper() == description.ToUpper())
                    return obj;
            }
            throw new InvalidOperationException(
                $"Couldn't find enum of type {typeof(T)} with attribute of '{description}'");
        }

        public static string GetXmlEnumAttribute(this Enum enumerationValue)
        {
            var customAttributes = (XmlEnumAttribute[]) enumerationValue.GetType().GetField(enumerationValue.ToString())
                .GetCustomAttributes(typeof(XmlEnumAttribute), false);
            if (customAttributes.Length <= 0)
                return enumerationValue.ToString();
            return customAttributes[0].Name;
        }

        public static T GetEnumAttribute<T>(this Enum enumerationValue) where T : Attribute
        {
            return ((IEnumerable<T>) enumerationValue.GetType().GetField(enumerationValue.ToString())
                .GetCustomAttributes(typeof(T), false)).FirstOrDefault();
        }

        public static IEnumerable<T> GetAllItems<T>(this Enum value)
        {
            return Enum.GetValues(typeof(T)).Cast<object>().Select(item => (T) item);
        }

        public static IEnumerable<T> GetAllItems<T>() where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static IEnumerable<T> GetAllSelectedItems<T>(this Enum value)
        {
            var valueAsInt = Convert.ToInt32(value, CultureInfo.InvariantCulture);
            return Enum.GetValues(typeof(T)).Cast<object>().Select(item => new
                {
                    item,
                    itemAsInt = Convert.ToInt32(item, CultureInfo.InvariantCulture)
                }).Where(_param1 => _param1.itemAsInt == (valueAsInt & _param1.itemAsInt))
                .Select(_param0 => (T) _param0.item);
        }

        public static bool Contains<T>(this Enum value, T request)
        {
            var int32_1 = Convert.ToInt32(value, CultureInfo.InvariantCulture);
            var int32_2 = Convert.ToInt32(request, CultureInfo.InvariantCulture);
            return int32_2 == (int32_1 & int32_2);
        }

        public static T? TryParseEnum<T>(this string enumerationString) where T : struct
        {
            if (GetAllItems<T>().Any(e => e.ToString() == enumerationString))
                return enumerationString.ToEnum<T>();
            return new T?();
        }

        public static string GetDescription(this Enum enumerationValue)
        {
            if (enumerationValue == null)
                return null;
            var customAttributes = (DescriptionAttribute[]) enumerationValue.GetType()
                .GetField(enumerationValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (customAttributes.Length <= 0)
                return enumerationValue.ToString();
            return customAttributes[0].Description;
        }
    }
}