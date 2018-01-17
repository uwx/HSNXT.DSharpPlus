// Decompiled with JetBrains decompiler
// Type: SimpleExtension.ObjectExtension
// Assembly: SimpleExtension, Version=0.0.23.0, Culture=neutral, PublicKeyToken=null
// MVID: B43B1EC6-29EF-47CC-B98E-E2FE4FC2095C
// Assembly location: ...\bin\Debug\SimpleExtension.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static string ObjectToString(this object pObject, string pSeparator, ObjectToStringTypes pType)
        {
            var fields = pObject.GetType().GetFields();
            var str = string.Empty;
            if (pType == ObjectToStringTypes.Properties || pType == ObjectToStringTypes.PropertiesAndFields)
            {
                foreach (var property in pObject.GetType().GetProperties())
                {
                    try
                    {
                        str += $"{(object) property.Name}:{property.GetValue(pObject, null)}{(object) pSeparator}";
                    }
                    catch
                    {
                        str += $"{(object) property.Name}: n/a{(object) pSeparator}";
                    }
                }
            }
            if (pType == ObjectToStringTypes.Fields || pType == ObjectToStringTypes.PropertiesAndFields)
            {
                foreach (var fieldInfo in fields)
                {
                    try
                    {
                        str =
                            $"{(object) str}{(object) fieldInfo.Name}: {fieldInfo.GetValue(pObject)}{(object) pSeparator}";
                    }
                    catch
                    {
                        str = $"{(object) str}{(object) fieldInfo.Name}: n/a{(object) pSeparator}";
                    }
                }
            }
            return str;
        }

        public static T DeserializeXMLToObject<T>(string pXmlData) where T : new()
        {
            if (string.IsNullOrEmpty(pXmlData))
                return default;
            var obj =
                (T) new XmlSerializer(Activator.CreateInstance<T>().GetType()).Deserialize(new StringReader(pXmlData));
            if (obj != null)
                return obj;
            return default;
        }

        public static string SeralizeObjectToXML<T>(T pObject)
        {
            if (pObject == null)
                return string.Empty;
            var output = new StringBuilder();
            var xmlSerializer = new XmlSerializer(pObject.GetType());
            var settings = new XmlWriterSettings();
            var xmlWriter = XmlWriter.Create(output, settings);
            xmlSerializer.Serialize(xmlWriter, pObject);
            return output.ToString();
        }

        public static string ToString<T>(this T instance) where T : class
        {
            if (instance == null)
                return string.Empty;
            var type1 = typeof(List<string>);
            var type2 = typeof(string[]);
            var arrayTypes = new Type[2] {type1, type2};
            var handledTypes = new Type[8]
            {
                typeof(int),
                typeof(string),
                typeof(bool),
                typeof(DateTime),
                typeof(double),
                typeof(decimal),
                type1,
                type2
            };
            var list = instance.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(prop => handledTypes.Contains(prop.PropertyType))
                .Where(prop => prop.GetValue(instance, null) != null).ToList();
            var format =
                $"{{0,-{(object) list.Max(prp => prp.Name.Length)}}} : {{1}}";
            return string.Join(Environment.NewLine,
                list.Select(prop => string.Format(format, prop.Name,
                    arrayTypes.Contains(prop.PropertyType)
                        ? string.Join(", ", prop.GetValue(instance, null) as string[])
                        : prop.GetValue(instance, null))) as string[]);
        }
    }
}