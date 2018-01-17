// Decompiled with JetBrains decompiler
// Type: TestProj47.SerializationExtensions
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
#if NetFX
using System.Runtime.Serialization.Formatters.Soap;
#endif
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using HSNXT.RegularExpressions;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>Field JsonTypeInfoRegex.</summary>
        private static readonly Regex JsonTypeInfoRegex = new JsonTypeInfoRegex();

        /// <summary>Serializes object to Json string.</summary>
        /// <param name="source">Object to serialize.</param>
        /// <param name="omitTypeInfo">Whether to omit type information.</param>
        /// <param name="encoding">The encoding to apply to the string.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>Json string.</returns>
        public static string SerializeDataContractJsonString(this object source, bool omitTypeInfo = false,
            Encoding encoding = null, Type[] knownTypes = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            using (var memoryStream = new MemoryStream())
            {
                (knownTypes == null || knownTypes.Length == 0
                    ? new DataContractJsonSerializer(source.GetType())
                    : new DataContractJsonSerializer(source.GetType(), knownTypes)).WriteObject(memoryStream, source);
                memoryStream.Position = 0L;
                var input = (encoding ?? Encoding.UTF8).GetString(memoryStream.ToArray());
                return omitTypeInfo ? JsonTypeInfoRegex.Replace(input, string.Empty) : input;
            }
        }

        /// <summary>Serializes object to Json string, write to file.</summary>
        /// <param name="source">Object to serialize.</param>
        /// <param name="filename">File name.</param>
        /// <param name="overwrite">Whether overwrite exists file.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>File full path.</returns>
        public static string WriteDataContractJson(this object source, string filename, bool overwrite = false,
            Type[] knownTypes = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));
            var fullPath = Path.GetFullPath(filename);
            var directoryName = Path.GetDirectoryName(fullPath);
            if (!overwrite && File.Exists(filename))
                throw new ArgumentException("The specified file already exists.", fullPath);
            if (!Directory.Exists(directoryName))
            {
                try
                {
                    Directory.CreateDirectory(directoryName);
                }
                catch
                {
                    throw;
                }
            }
            using (var fileStream = File.OpenWrite(fullPath))
            {
                (knownTypes == null || knownTypes.Length == 0
                    ? new DataContractJsonSerializer(source.GetType())
                    : new DataContractJsonSerializer(source.GetType(), knownTypes)).WriteObject(fileStream, source);
                return fullPath;
            }
        }

        /// <summary>Deserializes Json string to object.</summary>
        /// <param name="source">Json string object.</param>
        /// <param name="type">Type of object.</param>
        /// <param name="encoding">The encoding to apply to the string.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>Instance of object.</returns>
        public static object DeserializeDataContractJsonString(this string source, Type type, Encoding encoding = null,
            Type[] knownTypes = null)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            var contractJsonSerializer = knownTypes == null || knownTypes.Length == 0
                ? new DataContractJsonSerializer(type)
                : new DataContractJsonSerializer(type, knownTypes);
            using (var memoryStream = new MemoryStream((encoding ?? Encoding.UTF8).GetBytes(source)))
            {
                memoryStream.Position = 0L;
                return contractJsonSerializer.ReadObject(memoryStream);
            }
        }

        /// <summary>Deserializes Json string to object.</summary>
        /// <param name="source">Json string object.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array of object types to serialize.</param>
        /// <param name="encoding">The encoding to apply to the string.</param>
        /// <returns>Instance of object.</returns>
        public static object DeserializeDataContractJsonString(this string source, Type[] knownTypes,
            Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (knownTypes == null || knownTypes.Length < 1)
                throw new ArgumentException("knownTypes is null or empty.", nameof(knownTypes));
            Type type = null;
            using (var stringReader = new StringReader(source))
            {
                var rootNodeName = XElement.Load(stringReader).Name.LocalName;
                type = knownTypes.FirstOrDefault(p => p.Name == rootNodeName);
                if (type == null)
                    throw new InvalidOperationException();
            }
            var contractJsonSerializer = new DataContractJsonSerializer(type, knownTypes);
            using (var memoryStream = new MemoryStream((encoding ?? Encoding.UTF8).GetBytes(source)))
            {
                memoryStream.Position = 0L;
                return contractJsonSerializer.ReadObject(memoryStream);
            }
        }

        /// <summary>Deserializes Json string to object.</summary>
        /// <typeparam name="T">Type of the returns object.</typeparam>
        /// <param name="source">Json string object.</param>
        /// <param name="encoding">The encoding to apply to the string.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>Instance of object.</returns>
        public static T DeserializeDataContractJsonString<T>(this string source, Encoding encoding = null,
            Type[] knownTypes = null)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            var contractJsonSerializer = knownTypes == null || knownTypes.Length == 0
                ? new DataContractJsonSerializer(typeof(T))
                : new DataContractJsonSerializer(typeof(T), knownTypes);
            using (var memoryStream = new MemoryStream((encoding ?? Encoding.UTF8).GetBytes(source)))
            {
                memoryStream.Position = 0L;
                return (T) contractJsonSerializer.ReadObject(memoryStream);
            }
        }

        /// <summary>Deserializes Json string to object, read from file.</summary>
        /// <param name="source">File name.</param>
        /// <param name="type">Type of object.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>Instance of object.</returns>
        public static object ReadDataContractJson(this string source, Type type, Type[] knownTypes = null)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            var fullPath = Path.GetFullPath(source);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("The specified file does not exist.", fullPath);
            using (var fileStream = File.OpenRead(fullPath))
                return (knownTypes == null || knownTypes.Length == 0
                    ? new DataContractJsonSerializer(type)
                    : new DataContractJsonSerializer(type, knownTypes)).ReadObject(fileStream);
        }

        /// <summary>Deserializes Json string to object, read from file.</summary>
        /// <param name="source">File name.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array of object types to serialize.</param>
        /// <returns>Instance of object.</returns>
        public static object ReadDataContractJson(this string source, Type[] knownTypes = null)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (knownTypes == null || knownTypes.Length < 1)
                throw new ArgumentException("knownTypes is null or empty.", nameof(knownTypes));
            var fullPath = Path.GetFullPath(source);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("The specified file does not exist.", fullPath);
            var rootNodeName = XElement.Load(fullPath).Name.LocalName;
            var type = knownTypes.FirstOrDefault(p => p.Name == rootNodeName);
            if (type == null)
                throw new InvalidOperationException();
            using (var fileStream = File.OpenRead(fullPath))
                return new DataContractJsonSerializer(type, knownTypes).ReadObject(fileStream);
        }

        /// <summary>Deserializes Json string to object, read from file.</summary>
        /// <typeparam name="T">Type of the returns object.</typeparam>
        /// <param name="source">File name.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>Instance of object.</returns>
        public static T ReadDataContractJson<T>(this string source, Type[] knownTypes = null)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            var fullPath = Path.GetFullPath(source);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("The specified file does not exist.", fullPath);
            using (var fileStream = File.OpenRead(fullPath))
                return (T) (knownTypes == null || knownTypes.Length == 0
                    ? new DataContractJsonSerializer(typeof(T))
                    : new DataContractJsonSerializer(typeof(T), knownTypes)).ReadObject(fileStream);
        }

        /// <summary>Serializes object to Json bytes.</summary>
        /// <param name="source">Object to serialize.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>Json bytes.</returns>
        public static byte[] SerializeDataContractJsonBinary(this object source, Type[] knownTypes = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            using (var memoryStream = new MemoryStream())
            {
                (knownTypes == null || knownTypes.Length == 0
                    ? new DataContractJsonSerializer(source.GetType())
                    : new DataContractJsonSerializer(source.GetType(), knownTypes)).WriteObject(memoryStream, source);
                memoryStream.Position = 0L;
                return memoryStream.ToArray();
            }
        }

        /// <summary>Deserializes Json bytes to object.</summary>
        /// <param name="source">Json string object.</param>
        /// <param name="type">Type of object.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>Instance of object.</returns>
        public static object DeserializeDataContractJsonBinary(this byte[] source, Type type, Type[] knownTypes = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            var contractJsonSerializer = knownTypes == null || knownTypes.Length == 0
                ? new DataContractJsonSerializer(type)
                : new DataContractJsonSerializer(type, knownTypes);
            using (var memoryStream = new MemoryStream(source))
            {
                memoryStream.Position = 0L;
                return contractJsonSerializer.ReadObject(memoryStream);
            }
        }

        /// <summary>Deserializes Json bytes to object.</summary>
        /// <param name="source">Json string object.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array of object types to serialize.</param>
        /// <returns>Instance of object.</returns>
        public static object DeserializeDataContractJsonBinary(this byte[] source, Type[] knownTypes)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (knownTypes == null || knownTypes.Length < 1)
                throw new ArgumentException("knownTypes is null or empty.", nameof(knownTypes));
            using (var memoryStream = new MemoryStream(source))
            {
                using (var reader = XmlReader.Create(memoryStream))
                {
                    var rootNodeName = XElement.Load(reader).Name.LocalName;
                    var type = knownTypes.FirstOrDefault(p => p.Name == rootNodeName);
                    if (type == null)
                        throw new InvalidOperationException();
                    memoryStream.Position = 0L;
                    return new DataContractJsonSerializer(type, knownTypes).ReadObject(memoryStream);
                }
            }
        }

        /// <summary>Deserializes Json bytes to object.</summary>
        /// <typeparam name="T">Type of the returns object.</typeparam>
        /// <param name="source">Json string object.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>Instance of object.</returns>
        public static T DeserializeDataContractJsonBinary<T>(this byte[] source, Type[] knownTypes = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var contractJsonSerializer = knownTypes == null || knownTypes.Length == 0
                ? new DataContractJsonSerializer(typeof(T))
                : new DataContractJsonSerializer(typeof(T), knownTypes);
            using (var memoryStream = new MemoryStream(source))
            {
                memoryStream.Position = 0L;
                return (T) contractJsonSerializer.ReadObject(memoryStream);
            }
        }

#if NetFX
        /// <summary>Serializes object to Soap string.</summary>
        /// <param name="source">The object to serialize.</param>
        /// <returns>Soap string.</returns>
        public static string SerializeSoapString(this object source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            using (var memoryStream = new MemoryStream())
            {
                new SoapFormatter().Serialize(memoryStream, source);
                memoryStream.Position = 0L;
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(memoryStream);
                return xmlDocument.InnerXml;
            }
        }

        /// <summary>Serializes object to Soap string, write to file.</summary>
        /// <param name="source">The object to serialize.</param>
        /// <param name="filename">File name.</param>
        /// <param name="overwrite">Whether overwrite exists file.</param>
        /// <returns>File full path.</returns>
        public static string WriteSoap(this object source, string filename, bool overwrite = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var fullPath = Path.GetFullPath(filename);
            var directoryName = Path.GetDirectoryName(fullPath);
            if (!overwrite && File.Exists(filename))
                throw new ArgumentException("The specified file already exists.", fullPath);
            if (!Directory.Exists(directoryName))
            {
                try
                {
                    Directory.CreateDirectory(directoryName);
                }
                catch
                {
                    throw;
                }
            }
            using (var fileStream = File.OpenWrite(fullPath))
            {
                new SoapFormatter().Serialize(fileStream, source);
                return fullPath;
            }
        }

        /// <summary>Deserializes Soap string to object.</summary>
        /// <param name="source">The Soap string to deserialize.</param>
        /// <returns>Instance of object.</returns>
        public static object DeserializeSoapString(this string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(source);
            var soapFormatter = new SoapFormatter();
            using (var memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);
                memoryStream.Position = 0L;
                return soapFormatter.Deserialize(memoryStream);
            }
        }

        /// <summary>Deserializes Soap string to object.</summary>
        /// <typeparam name="T">Type of the returns object.</typeparam>
        /// <param name="source">The Soap string to deserialize.</param>
        /// <returns>Instance of T.</returns>
        public static T DeserializeSoapString<T>(this string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(source);
            var soapFormatter = new SoapFormatter();
            using (var memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);
                memoryStream.Position = 0L;
                return (T) soapFormatter.Deserialize(memoryStream);
            }
        }

        /// <summary>Deserializes Soap string to object, read from file.</summary>
        /// <param name="source">File name.</param>
        /// <returns>Instance of object.</returns>
        public static object ReadSoap(this string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            var fullPath = Path.GetFullPath(source);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("The specified file does not exist.", fullPath);
            using (var fileStream = File.OpenRead(fullPath))
                return new SoapFormatter().Deserialize(fileStream);
        }

        /// <summary>Deserializes Soap string to object, read from file.</summary>
        /// <typeparam name="T">Type of the returns object.</typeparam>
        /// <param name="source">File name.</param>
        /// <returns>Instance of T.</returns>
        public static T ReadSoap<T>(this string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            var fullPath = Path.GetFullPath(source);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("The specified file does not exist.", fullPath);
            using (var fileStream = File.OpenRead(fullPath))
                return (T) new SoapFormatter().Deserialize(fileStream);
        }

        /// <summary>Serializes object to Soap bytes.</summary>
        /// <param name="source">The object to serialize.</param>
        /// <returns>Bytes representation of the source object.</returns>
        public static byte[] SerializeSoapBinary(this object source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            using (var memoryStream = new MemoryStream())
            {
                new SoapFormatter().Serialize(memoryStream, source);
                memoryStream.Position = 0L;
                return memoryStream.ToArray();
            }
        }

        /// <summary>Deserializes Soap bytes to object.</summary>
        /// <param name="source">The bytes to deserialize.</param>
        /// <returns>Instance of Soap object.</returns>
        public static object DeserializeSoapBinary(this byte[] source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var soapFormatter = new SoapFormatter();
            using (var memoryStream = new MemoryStream(source))
            {
                memoryStream.Position = 0L;
                return soapFormatter.Deserialize(memoryStream);
            }
        }

        /// <summary>Deserializes Soap bytes to object.</summary>
        /// <typeparam name="T">Type of the returns object.</typeparam>
        /// <param name="source">The bytes to deserialize.</param>
        /// <returns>Instance of Soap object.</returns>
        public static T DeserializeSoapBinary<T>(this byte[] source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var soapFormatter = new SoapFormatter();
            using (var memoryStream = new MemoryStream(source))
            {
                memoryStream.Position = 0L;
                return (T) soapFormatter.Deserialize(memoryStream);
            }
        }
#endif

        /// <summary>Serializes object to Xml string.</summary>
        /// <remarks>
        /// The object to be serialized should be decorated with the <see cref="T:System.SerializableAttribute" />, or implement the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface.
        /// </remarks>
        /// <param name="source">The object to serialize.</param>
        /// <param name="indent">Whether to write individual elements on new lines and indent.</param>
        /// <param name="omitXmlDeclaration">Whether to write an Xml declaration.</param>
        /// <param name="removeDefaultNamespace">Whether to write default namespace.</param>
        /// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
        /// <returns>An Xml encoded string representation of the source object.</returns>
        public static string SerializeXmlString(this object source, bool indent = false, bool omitXmlDeclaration = true,
            bool removeDefaultNamespace = true, Type[] extraTypes = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var xmlSerializer = extraTypes == null || extraTypes.Length == 0
                ? new XmlSerializer(source.GetType())
                : new XmlSerializer(source.GetType(), extraTypes);
            using (var memoryStream1 = new MemoryStream())
            {
                using (var streamReader = new StreamReader(memoryStream1))
                {
                    var memoryStream2 = memoryStream1;
                    var settings = new XmlWriterSettings
                    {
                        OmitXmlDeclaration = omitXmlDeclaration,
                        Indent = indent,
                        Encoding = new UTF8Encoding(false),
                        CloseOutput = true
                    };
                    using (var xmlWriter = XmlWriter.Create(memoryStream2, settings))
                    {
                        if (removeDefaultNamespace)
                        {
                            var namespaces = new XmlSerializerNamespaces();
                            namespaces.Add(string.Empty, string.Empty);
                            xmlSerializer.Serialize(xmlWriter, source, namespaces);
                        }
                        else
                            xmlSerializer.Serialize(xmlWriter, source);
                        xmlWriter.Flush();
                        memoryStream1.Position = 0L;
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }

        /// <summary>Serializes object to Xml string, write to file.</summary>
        /// <remarks>
        /// The object to be serialized should be decorated with the <see cref="T:System.SerializableAttribute" />, or implement the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface.
        /// </remarks>
        /// <param name="source">The object to serialize.</param>
        /// <param name="filename">File name.</param>
        /// <param name="overwrite">Whether overwrite exists file.</param>
        /// <param name="indent">Whether to write individual elements on new lines and indent.</param>
        /// <param name="omitXmlDeclaration">Whether to write an Xml declaration.</param>
        /// <param name="removeDefaultNamespace">Whether to write default namespace.</param>
        /// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
        /// <returns>File full path.</returns>
        public static string WriteXml(this object source, string filename, bool overwrite = false, bool indent = true,
            bool omitXmlDeclaration = true, bool removeDefaultNamespace = true, Type[] extraTypes = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));
            var fullPath = Path.GetFullPath(filename);
            var directoryName = Path.GetDirectoryName(fullPath);
            if (!overwrite && File.Exists(filename))
                throw new ArgumentException("The specified file already exists.", fullPath);
            if (!Directory.Exists(directoryName))
            {
                try
                {
                    Directory.CreateDirectory(directoryName);
                }
                catch
                {
                    throw;
                }
            }
            var xmlSerializer = extraTypes == null || extraTypes.Length == 0
                ? new XmlSerializer(source.GetType())
                : new XmlSerializer(source.GetType(), extraTypes);
            var outputFileName = fullPath;
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = omitXmlDeclaration,
                Indent = indent,
                Encoding = new UTF8Encoding(false),
                CloseOutput = true
            };
            using (var xmlWriter = XmlWriter.Create(outputFileName, settings))
            {
                if (removeDefaultNamespace)
                {
                    var namespaces = new XmlSerializerNamespaces();
                    namespaces.Add(string.Empty, string.Empty);
                    xmlSerializer.Serialize(xmlWriter, source, namespaces);
                }
                else
                    xmlSerializer.Serialize(xmlWriter, source);
                xmlWriter.Flush();
                return fullPath;
            }
        }

        /// <summary>Deserializes Xml string to object.</summary>
        /// <param name="source">The Xml string to deserialize.</param>
        /// <param name="type">Type of object.</param>
        /// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
        /// <returns>Instance of object.</returns>
        public static object DeserializeXmlString(this string source, Type type, Type[] extraTypes = null)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            using (var stringReader = new StringReader(source))
                return (extraTypes == null || extraTypes.Length == 0
                    ? new XmlSerializer(type)
                    : new XmlSerializer(type, extraTypes)).Deserialize(stringReader);
        }

        /// <summary>Deserializes Xml string to object.</summary>
        /// <param name="source">The Xml string to deserialize.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array of object types to serialize.</param>
        /// <returns>Instance of object.</returns>
        public static object DeserializeXmlString(this string source, Type[] knownTypes)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (knownTypes == null || knownTypes.Length < 1)
                throw new ArgumentException("knownTypes is null or empty.", nameof(knownTypes));
            Type type = null;
            using (var stringReader = new StringReader(source))
            {
                var rootNodeName = XElement.Load(stringReader).Name.LocalName;
                type = knownTypes.FirstOrDefault(p => p.Name.Equals(rootNodeName, StringComparison.OrdinalIgnoreCase));
                if (type == null)
                    throw new InvalidOperationException();
            }
            using (var stringReader = new StringReader(source))
                return new XmlSerializer(type, knownTypes).Deserialize(stringReader);
        }

        /// <summary>Deserializes Xml string to object.</summary>
        /// <typeparam name="T">Type of the returns object.</typeparam>
        /// <param name="source">The Xml string to deserialize.</param>
        /// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
        /// <returns>Instance of T.</returns>
        public static T DeserializeXmlString<T>(this string source, Type[] extraTypes = null)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            using (var stringReader = new StringReader(source))
                return (T) (extraTypes == null || extraTypes.Length == 0
                    ? new XmlSerializer(typeof(T))
                    : new XmlSerializer(typeof(T), extraTypes)).Deserialize(stringReader);
        }

        /// <summary>Deserializes Xml string to object, read from file.</summary>
        /// <param name="source">File name.</param>
        /// <param name="type">Type of object.</param>
        /// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
        /// <returns>Instance of object.</returns>
        public static object ReadXml(this string source, Type type, Type[] extraTypes = null)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            var fullPath = Path.GetFullPath(source);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("The specified file does not exist.", fullPath);
            using (var fileStream = File.OpenRead(fullPath))
                return (extraTypes == null || extraTypes.Length == 0
                    ? new XmlSerializer(type)
                    : new XmlSerializer(type, extraTypes)).Deserialize(fileStream);
        }

        /// <summary>Deserializes Xml string to object, read from file.</summary>
        /// <param name="source">File name.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array of object types to serialize.</param>
        /// <returns>Instance of object.</returns>
        public static object ReadXml(this string source, Type[] knownTypes)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (knownTypes == null || knownTypes.Length < 1)
                throw new ArgumentException("knownTypes is null or empty.", nameof(knownTypes));
            var fullPath = Path.GetFullPath(source);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("The specified file does not exist.", fullPath);
            var rootNodeName = XElement.Load(fullPath).Name.LocalName;
            var type = knownTypes.FirstOrDefault(p => p.Name.Equals(rootNodeName, StringComparison.OrdinalIgnoreCase));
            if (type == null)
                throw new InvalidOperationException();
            using (var fileStream = File.OpenRead(fullPath))
                return new XmlSerializer(type, knownTypes).Deserialize(fileStream);
        }

        /// <summary>Deserializes Xml string to object, read from file.</summary>
        /// <typeparam name="T">Type of the returns object.</typeparam>
        /// <param name="source">File name.</param>
        /// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
        /// <returns>Instance of T.</returns>
        public static T ReadXml<T>(this string source, Type[] extraTypes = null)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            var fullPath = Path.GetFullPath(source);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("The specified file does not exist.", fullPath);
            using (var fileStream = File.OpenRead(fullPath))
                return (T) (extraTypes == null || extraTypes.Length == 0
                    ? new XmlSerializer(typeof(T))
                    : new XmlSerializer(typeof(T), extraTypes)).Deserialize(fileStream);
        }

        /// <summary>Serializes object to Xml bytes.</summary>
        /// <param name="source">The object to serialize.</param>
        /// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
        /// <returns>Bytes representation of the source object.</returns>
        public static byte[] SerializeXmlBinary(this object source, Type[] extraTypes = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            using (var memoryStream = new MemoryStream())
            {
                (extraTypes == null || extraTypes.Length == 0
                    ? new XmlSerializer(source.GetType())
                    : new XmlSerializer(source.GetType(), extraTypes)).Serialize(memoryStream, source);
                memoryStream.Position = 0L;
                return memoryStream.ToArray();
            }
        }

        /// <summary>Deserializes Xml bytes to object.</summary>
        /// <param name="source">The bytes to deserialize.</param>
        /// <param name="type">Type of Xml object.</param>
        /// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
        /// <returns>Instance of Xml object.</returns>
        public static object DeserializeXmlBinary(this byte[] source, Type type, Type[] extraTypes = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            using (var memoryStream = new MemoryStream(source))
                return (extraTypes == null || extraTypes.Length == 0
                    ? new XmlSerializer(type)
                    : new XmlSerializer(type, extraTypes)).Deserialize(memoryStream);
        }

        /// <summary>Deserializes Xml bytes to object.</summary>
        /// <param name="source">The bytes to deserialize.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array of object types to serialize.</param>
        /// <returns>Instance of Xml object.</returns>
        public static object DeserializeXmlBinary(this byte[] source, Type[] knownTypes = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (knownTypes == null || knownTypes.Length < 1)
                throw new ArgumentException("knownTypes is null or empty.", nameof(knownTypes));
            using (var memoryStream = new MemoryStream(source))
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(memoryStream);
                var rootNodeName = xmlDocument.LocalName;
                var type = knownTypes.FirstOrDefault(p => p.Name == rootNodeName);
                if (type == null)
                    throw new InvalidOperationException();
                memoryStream.Position = 0L;
                return new XmlSerializer(type, knownTypes).Deserialize(memoryStream);
            }
        }

        /// <summary>Deserializes Xml bytes to object.</summary>
        /// <typeparam name="T">Type of the returns object.</typeparam>
        /// <param name="source">The bytes to deserialize.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>Instance of Xml object.</returns>
        public static T DeserializeXmlBinary<T>(this byte[] source, Type[] knownTypes = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            using (var memoryStream = new MemoryStream(source))
                return (T) (knownTypes == null || knownTypes.Length == 0
                    ? new XmlSerializer(typeof(T))
                    : new XmlSerializer(typeof(T), knownTypes)).Deserialize(memoryStream);
        }

        /// <summary>Serializes DataContract object to Xml string.</summary>
        /// <param name="source">The DataContract object to serialize.</param>
        /// <param name="indent">Whether to write individual elements on new lines and indent.</param>
        /// <param name="omitXmlDeclaration">Whether to write an Xml declaration.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>An Xml encoded string representation of the source DataContract object.</returns>
        public static string SerializeDataContractXmlString(this object source, bool indent = false,
            bool omitXmlDeclaration = true, Type[] knownTypes = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var contractSerializer = knownTypes == null || knownTypes.Length == 0
                ? new DataContractSerializer(source.GetType())
                : new DataContractSerializer(source.GetType(), knownTypes);
            using (var memoryStream1 = new MemoryStream())
            {
                using (var streamReader = new StreamReader(memoryStream1))
                {
                    var memoryStream2 = memoryStream1;
                    var settings = new XmlWriterSettings
                    {
                        OmitXmlDeclaration = omitXmlDeclaration,
                        Indent = indent,
                        Encoding = new UTF8Encoding(false),
                        CloseOutput = true
                    };
                    using (var writer = XmlWriter.Create(memoryStream2, settings))
                    {
                        contractSerializer.WriteObject(writer, source);
                        writer.Flush();
                        memoryStream1.Position = 0L;
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }

        /// <summary>
        /// Serializes DataContract object to Xml string, write to file.
        /// </summary>
        /// <param name="source">The DataContract object to serialize.</param>
        /// <param name="filename">File name.</param>
        /// <param name="overwrite">Whether overwrite exists file.</param>
        /// <param name="indent">Whether to write individual elements on new lines and indent.</param>
        /// <param name="omitXmlDeclaration">Whether to write an Xml declaration.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>File full path.</returns>
        public static string WriteDataContractXml(this object source, string filename, bool overwrite = false,
            bool indent = true, bool omitXmlDeclaration = true, Type[] knownTypes = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));
            var fullPath = Path.GetFullPath(filename);
            var directoryName = Path.GetDirectoryName(fullPath);
            if (!overwrite && File.Exists(filename))
                throw new ArgumentException("The specified file already exists.", fullPath);
            if (!Directory.Exists(directoryName))
            {
                try
                {
                    Directory.CreateDirectory(directoryName);
                }
                catch
                {
                    throw;
                }
            }
            var contractSerializer = knownTypes == null || knownTypes.Length == 0
                ? new DataContractSerializer(source.GetType())
                : new DataContractSerializer(source.GetType(), knownTypes);
            var outputFileName = fullPath;
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = omitXmlDeclaration,
                Indent = indent,
                Encoding = new UTF8Encoding(false),
                CloseOutput = true
            };
            using (var writer = XmlWriter.Create(outputFileName, settings))
            {
                contractSerializer.WriteObject(writer, source);
                writer.Flush();
                return fullPath;
            }
        }

        /// <summary>Deserializes DataContract Xml string to object.</summary>
        /// <param name="source">The DataContract Xml string to deserialize.</param>
        /// <param name="type">Type of DataContract object.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>Instance of DataContract object.</returns>
        public static object DeserializeDataContractXmlString(this string source, Type type, Type[] knownTypes = null)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            var contractSerializer = knownTypes == null || knownTypes.Length == 0
                ? new DataContractSerializer(type)
                : new DataContractSerializer(type, knownTypes);
            var stringReader = new StringReader(source);
            var settings = new XmlReaderSettings
            {
                CheckCharacters = false,
                IgnoreComments = true,
                IgnoreWhitespace = true,
                IgnoreProcessingInstructions = true,
                CloseInput = true
            };
            using (var reader = XmlReader.Create(stringReader, settings))
                return contractSerializer.ReadObject(reader);
        }

        /// <summary>Deserializes DataContract Xml string to object.</summary>
        /// <param name="source">The DataContract Xml string to deserialize.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array of object types to serialize.</param>
        /// <returns>Instance of DataContract object.</returns>
        public static object DeserializeDataContractXmlString(this string source, Type[] knownTypes)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (knownTypes == null || knownTypes.Length < 1)
                throw new ArgumentException("knownTypes is null or empty.", nameof(knownTypes));
            Type type = null;
            using (var stringReader = new StringReader(source))
            {
                var rootNodeName = XElement.Load(stringReader).Name.LocalName;
                type = knownTypes.FirstOrDefault(p => p.Name == rootNodeName);
                if (type == null)
                    throw new InvalidOperationException();
            }
            var contractSerializer = new DataContractSerializer(type, knownTypes);
            var stringReader1 = new StringReader(source);
            var settings = new XmlReaderSettings
            {
                CheckCharacters = false,
                IgnoreComments = true,
                IgnoreWhitespace = true,
                IgnoreProcessingInstructions = true,
                CloseInput = true
            };
            using (var reader = XmlReader.Create(stringReader1, settings))
                return contractSerializer.ReadObject(reader);
        }

        /// <summary>Deserializes DataContract Xml string to object.</summary>
        /// <typeparam name="T">Type of the returns object.</typeparam>
        /// <param name="source">The DataContract Xml string to deserialize.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>Instance of T.</returns>
        public static T DeserializeDataContractXmlString<T>(this string source, Type[] knownTypes = null)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            var contractSerializer = knownTypes == null || knownTypes.Length == 0
                ? new DataContractSerializer(typeof(T))
                : new DataContractSerializer(typeof(T), knownTypes);
            var stringReader = new StringReader(source);
            var settings = new XmlReaderSettings
            {
                CheckCharacters = false,
                IgnoreComments = true,
                IgnoreWhitespace = true,
                IgnoreProcessingInstructions = true,
                CloseInput = true
            };
            using (var reader = XmlReader.Create(stringReader, settings))
                return (T) contractSerializer.ReadObject(reader);
        }

        /// <summary>
        /// Deserializes DataContract Xml string to object, read from file.
        /// </summary>
        /// <param name="source">File name.</param>
        /// <param name="type">Type of DataContract object.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>Instance of DataContract object.</returns>
        public static object ReadDataContractXml(this string source, Type type, Type[] knownTypes = null)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            var fullPath = Path.GetFullPath(source);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("The specified file does not exist.", fullPath);
            using (var fileStream = File.OpenRead(fullPath))
                return (knownTypes == null || knownTypes.Length == 0
                    ? new DataContractSerializer(type)
                    : new DataContractSerializer(type, knownTypes)).ReadObject(fileStream);
        }

        /// <summary>
        /// Deserializes DataContract Xml string to object, read from file.
        /// </summary>
        /// <param name="source">File name.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array of object types to serialize.</param>
        /// <returns>Instance of DataContract object.</returns>
        public static object ReadDataContractXml(this string source, Type[] knownTypes)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (knownTypes == null || knownTypes.Length < 1)
                throw new ArgumentException("knownTypes is null or empty.", nameof(knownTypes));
            var fullPath = Path.GetFullPath(source);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("The specified file does not exist.", fullPath);
            var rootNodeName = XElement.Load(fullPath).Name.LocalName;
            var type = knownTypes.FirstOrDefault(p => p.Name == rootNodeName);
            if (type == null)
                throw new InvalidOperationException();
            using (var fileStream = File.OpenRead(fullPath))
                return new DataContractSerializer(type, knownTypes).ReadObject(fileStream);
        }

        /// <summary>
        /// Deserializes DataContract Xml string to object, read from file.
        /// </summary>
        /// <typeparam name="T">Type of the returns object.</typeparam>
        /// <param name="source">File name.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>Instance of T.</returns>
        public static T ReadDataContractXml<T>(this string source, Type[] knownTypes = null)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            var fullPath = Path.GetFullPath(source);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("The specified file does not exist.", fullPath);
            using (var fileStream = File.OpenRead(fullPath))
                return (T) (knownTypes == null || knownTypes.Length == 0
                    ? new DataContractSerializer(typeof(T))
                    : new DataContractSerializer(typeof(T), knownTypes)).ReadObject(fileStream);
        }

        /// <summary>Serializes DataContract object to bytes.</summary>
        /// <param name="source">The DataContract object to serialize.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>Bytes representation of the source DataContract object.</returns>
        public static byte[] SerializeDataContractXmlBinary(this object source, Type[] knownTypes = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            using (var memoryStream = new MemoryStream())
            {
                (knownTypes == null || knownTypes.Length == 0
                    ? new DataContractSerializer(source.GetType())
                    : new DataContractSerializer(source.GetType(), knownTypes)).WriteObject(memoryStream, source);
                memoryStream.Position = 0L;
                return memoryStream.ToArray();
            }
        }

        /// <summary>Deserializes DataContract bytes to object.</summary>
        /// <param name="source">The bytes to deserialize.</param>
        /// <param name="type">Type of DataContract object.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>Instance of DataContract object.</returns>
        public static object DeserializeDataContractXmlBinary(this byte[] source, Type type, Type[] knownTypes = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            var contractSerializer = knownTypes == null || knownTypes.Length == 0
                ? new DataContractSerializer(type)
                : new DataContractSerializer(type, knownTypes);
            using (var memoryStream = new MemoryStream(source))
            {
                memoryStream.Position = 0L;
                return contractSerializer.ReadObject(memoryStream);
            }
        }

        /// <summary>Deserializes DataContract bytes to object.</summary>
        /// <param name="source">The bytes to deserialize.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array of object types to serialize.</param>
        /// <returns>Instance of DataContract object.</returns>
        public static object DeserializeDataContractXmlBinary(this byte[] source, Type[] knownTypes = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (knownTypes == null || knownTypes.Length < 1)
                throw new ArgumentException("knownTypes is null or empty.", nameof(knownTypes));
            using (var memoryStream = new MemoryStream(source))
            {
                using (var reader = XmlReader.Create(memoryStream))
                {
                    var rootNodeName = XElement.Load(reader).Name.LocalName;
                    var type = knownTypes.FirstOrDefault(p => p.Name == rootNodeName);
                    if (type == null)
                        throw new InvalidOperationException();
                    memoryStream.Position = 0L;
                    return new DataContractSerializer(type, knownTypes).ReadObject(memoryStream);
                }
            }
        }

        /// <summary>Deserializes DataContract bytes to object.</summary>
        /// <typeparam name="T">Type of the returns object.</typeparam>
        /// <param name="source">The bytes to deserialize.</param>
        /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
        /// <returns>Instance of DataContract object.</returns>
        public static T DeserializeDataContractXmlBinary<T>(this byte[] source, Type[] knownTypes = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var contractSerializer = knownTypes == null || knownTypes.Length == 0
                ? new DataContractSerializer(typeof(T))
                : new DataContractSerializer(typeof(T), knownTypes);
            using (var memoryStream = new MemoryStream(source))
            {
                memoryStream.Position = 0L;
                return (T) contractSerializer.ReadObject(memoryStream);
            }
        }

        /// <summary>Serializes object to Json string.</summary>
        /// <param name="source">Object to serialize.</param>
        /// <param name="omitTypeInfo">Whether to omit type information.</param>
        /// <returns>Json string.</returns>
        public static string SerializeJsonString(this object source, bool omitTypeInfo = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var input = JsonSerializer.Serialize(source);
            if (omitTypeInfo)
                return JsonTypeInfoRegex.Replace(input, string.Empty);
            return input;
        }

        /// <summary>Serializes object to Json string, write to file.</summary>
        /// <param name="source">Object to serialize.</param>
        /// <param name="filename">File name.</param>
        /// <param name="overwrite">Whether overwrite exists file.</param>
        /// <param name="omitTypeInfo">Whether to omit type information.</param>
        /// <returns>File full path.</returns>
        public static string WriteJson(this object source, string filename, bool overwrite = false,
            bool omitTypeInfo = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));
            var fullPath = Path.GetFullPath(filename);
            var directoryName = Path.GetDirectoryName(fullPath);
            if (!overwrite && File.Exists(filename))
                throw new ArgumentException("The specified file already exists.", fullPath);
            if (!Directory.Exists(directoryName))
            {
                try
                {
                    Directory.CreateDirectory(directoryName);
                }
                catch
                {
                    throw;
                }
            }
            var str = JsonSerializer.Serialize(source);
            if (omitTypeInfo)
                str = JsonTypeInfoRegex.Replace(str, string.Empty);
            File.WriteAllText(fullPath, str);
            return fullPath;
        }

        /// <summary>Deserializes Json string to object.</summary>
        /// <param name="source">Json string object.</param>
        /// <param name="type">Type of object.</param>
        /// <returns>Instance of object.</returns>
        public static object DeserializeJsonString(this string source, Type type = null)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            return JsonSerializer.Deserialize(source, type);
        }

        /// <summary>Deserializes Json string to object.</summary>
        /// <typeparam name="T">Type of the returns object.</typeparam>
        /// <param name="source">Json string object.</param>
        /// <returns>Instance of object.</returns>
        public static T DeserializeJsonString<T>(this string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            return JsonSerializer.Deserialize<T>(source);
        }

        /// <summary>Deserializes Json string to object, read from file.</summary>
        /// <param name="source">File name.</param>
        /// <param name="type">Type of object.</param>
        /// <returns>Instance of object.</returns>
        public static object ReadJson(this string source, Type type = null)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            var fullPath = Path.GetFullPath(source);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("The specified file does not exist.", fullPath);
            return JsonSerializer.Deserialize(File.ReadAllText(fullPath), type);
        }

        /// <summary>Deserializes Json string to object, read from file.</summary>
        /// <typeparam name="T">Type of the returns object.</typeparam>
        /// <param name="source">File name.</param>
        /// <returns>Instance of object.</returns>
        public static T ReadJson<T>(this string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            var fullPath = Path.GetFullPath(source);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("The specified file does not exist.", fullPath);
            return JsonSerializer.Deserialize<T>(File.ReadAllText(fullPath));
        }

        /// <summary>Serializes object to Json bytes.</summary>
        /// <param name="source">Object to serialize.</param>
        /// <returns>Json bytes.</returns>
        public static byte[] SerializeJsonBinary(this object source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(source));
        }

        /// <summary>Deserializes Json bytes to object.</summary>
        /// <param name="source">Json string object.</param>
        /// <param name="type">Type of object.</param>
        /// <returns>Instance of object.</returns>
        public static object DeserializeJsonBinary(this byte[] source, Type type = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            return JsonSerializer.Deserialize(Encoding.UTF8.GetString(source), type);
        }

        /// <summary>Deserializes Json bytes to object.</summary>
        /// <typeparam name="T">Type of the returns object.</typeparam>
        /// <param name="source">Json string object.</param>
        /// <returns>Instance of object.</returns>
        public static T DeserializeJsonBinary<T>(this byte[] source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(source));
        }

        /// <summary>Serializes object to bytes.</summary>
        /// <param name="source">Source object.</param>
        /// <returns>Byte array.</returns>
        public static byte[] SerializeBinary(this object source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            using (var memoryStream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(memoryStream, source);
                memoryStream.Position = 0L;
                return memoryStream.ToArray();
            }
        }

        /// <summary>Serializes object to bytes, write to file.</summary>
        /// <param name="source">Source object.</param>
        /// <param name="filename">File name.</param>
        /// <param name="overwrite">Whether overwrite exists file.</param>
        /// <returns>File full path.</returns>
        public static string WriteBinary(this object source, string filename, bool overwrite = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));
            var fullPath = Path.GetFullPath(filename);
            var directoryName = Path.GetDirectoryName(fullPath);
            if (!overwrite && File.Exists(filename))
                throw new ArgumentException("The specified file already exists.", fullPath);
            if (!Directory.Exists(directoryName))
            {
                try
                {
                    Directory.CreateDirectory(directoryName);
                }
                catch
                {
                    throw;
                }
            }
            using (var fileStream = File.OpenWrite(fullPath))
            {
                new BinaryFormatter().Serialize(fileStream, source);
                return fullPath;
            }
        }

        /// <summary>Deserializes bytes to object.</summary>
        /// <param name="source">Byte array.</param>
        /// <returns>Instance object.</returns>
        public static object DeserializeBinary(this byte[] source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (source.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(source));
            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream(source))
            {
                memoryStream.Position = 0L;
                return binaryFormatter.Deserialize(memoryStream);
            }
        }

        /// <summary>Deserializes bytes to object, read from file.</summary>
        /// <param name="source">File name.</param>
        /// <returns>Instance object.</returns>
        public static object ReadBinary(this string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            var fullPath = Path.GetFullPath(source);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("The specified file does not exist.", fullPath);
            using (var fileStream = File.OpenRead(fullPath))
                return new BinaryFormatter().Deserialize(fileStream);
        }

        /// <summary>Deserializes bytes to object.</summary>
        /// <typeparam name="T">The type of returns object.</typeparam>
        /// <param name="source">Byte array.</param>
        /// <returns>Instance of T.</returns>
        public static T DeserializeBinary<T>(this byte[] source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (source.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(source));
            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream(source))
            {
                memoryStream.Position = 0L;
                return (T) binaryFormatter.Deserialize(memoryStream);
            }
        }

        /// <summary>Deserializes bytes to object, read from file.</summary>
        /// <typeparam name="T">The type of returns object.</typeparam>
        /// <param name="source">File name.</param>
        /// <returns>Instance of T.</returns>
        public static T ReadBinary<T>(this string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            var fullPath = Path.GetFullPath(source);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("The specified file does not exist.", fullPath);
            using (var fileStream = File.OpenRead(fullPath))
                return (T) new BinaryFormatter().Deserialize(fileStream);
        }
    }
}