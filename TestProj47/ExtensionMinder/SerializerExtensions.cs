// Decompiled with JetBrains decompiler
// Type: ExtensionMinder.SerializerExtensions
// Assembly: ExtensionMinder, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9895DAEA-F52E-4F0C-B5FD-FB311AEFF61C
// Assembly location: ...\bin\Debug\ExtensionMinder.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static string SerializeToBinary<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
                throw new ArgumentException("The type must be serializable.", nameof(source));
            using (var memoryStream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(memoryStream, source);
                memoryStream.Flush();
                memoryStream.Position = 0L;
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        public static T DeserializeFromBinary<T>(this string str)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(str)))
                return (T) new BinaryFormatter().Deserialize(memoryStream);
        }

        public static string Serialize(this object obj)
        {
            var xmlSerializer = new XmlSerializer(obj.GetType());
            var stringWriter = new StringWriter();
            xmlSerializer.Serialize(new XmlTextWriter(stringWriter)
            {
                Formatting = Formatting.Indented
            }, obj);
            return stringWriter.ToString();
        }

        public static string SerializeWithDataContractSerializer(this object obj)
        {
            var output = new StringBuilder();
            using (var writer = XmlWriter.Create(output))
            {
                new DataContractSerializer(obj.GetType()).WriteObject(writer, obj);
                writer.Flush();
                return output.ToString();
            }
        }

        public static string SerializeWithDataContractSerializer(this object obj, IList<Type> knownTypes)
        {
            var output = new StringBuilder();
            using (var writer = XmlWriter.Create(output))
            {
                new DataContractSerializer(obj.GetType(), knownTypes).WriteObject(writer, obj);
                writer.Flush();
                return output.ToString();
            }
        }

        public static T Deserialize<T>(this XmlDocument xmlDocument)
        {
            var contractSerializer = new DataContractSerializer(typeof(T));
            using (var stringWriter = new StringWriter())
            {
                using (var xmlTextWriter = new XmlTextWriter(stringWriter))
                {
                    xmlDocument.WriteTo(xmlTextWriter);
                    var memoryStream =
                        new MemoryStream(Encoding.GetEncoding("UTF-16").GetBytes(stringWriter.ToString()));
                    memoryStream.Position = 0L;
                    var textReader =
                        XmlDictionaryReader.CreateTextReader(memoryStream, new XmlDictionaryReaderQuotas());
                    return (T) contractSerializer.ReadObject(textReader, true);
                }
            }
        }

        public static T Deserialize<T>(this byte[] buffer)
        {
            return (T) new BinaryFormatter().Deserialize(new MemoryStream(buffer));
        }

        public static void SerializeTo<T>(this T o, Stream stream)
        {
            new BinaryFormatter().Serialize(stream, o);
        }

        public static T Deserialize<T>(this Stream stream)
        {
            return (T) new BinaryFormatter().Deserialize(stream);
        }

        public static T Deserialize<T>(this XDocument xmlDocument)
        {
            using (var reader = xmlDocument.CreateReader())
                return (T) new XmlSerializer(typeof(T)).Deserialize(reader);
        }
    }
}