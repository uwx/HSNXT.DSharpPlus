// Decompiled with JetBrains decompiler
// Type: DevLib.ExtensionMethods.SerializationExtensions
// Assembly: DevLib.ExtensionMethods, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\DevLib.ExtensionMethods.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DevLib.ExtensionMethods
{
  /// <summary>Serialization Extensions.</summary>
  /// <summary>Serialization Extensions.</summary>
  /// <summary>Serialization Extensions.</summary>
  /// <summary>Serialization Extensions.</summary>
  /// <summary>Serialization Extensions.</summary>
  /// <summary>Serialization Extensions.</summary>
  public static class SerializationExtensions
  {
    /// <summary>Field JsonTypeInfoRegex.</summary>
    private static readonly Regex JsonTypeInfoRegex = new Regex("\\s*\"__type\"\\s*:\\s*\"[^\"]*\"\\s*,\\s*", RegexOptions.Compiled);

    /// <summary>Serializes object to Json string.</summary>
    /// <param name="source">Object to serialize.</param>
    /// <param name="omitTypeInfo">Whether to omit type information.</param>
    /// <param name="encoding">The encoding to apply to the string.</param>
    /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
    /// <returns>Json string.</returns>
    public static string SerializeDataContractJsonString(this object source, bool omitTypeInfo = false, Encoding encoding = null, Type[] knownTypes = null)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      using (MemoryStream memoryStream = new MemoryStream())
      {
        (knownTypes == null || knownTypes.Length == 0 ? new DataContractJsonSerializer(source.GetType()) : new DataContractJsonSerializer(source.GetType(), (IEnumerable<Type>) knownTypes)).WriteObject((Stream) memoryStream, source);
        memoryStream.Position = 0L;
        string input = (encoding ?? Encoding.UTF8).GetString(memoryStream.ToArray());
        if (omitTypeInfo)
          return SerializationExtensions.JsonTypeInfoRegex.Replace(input, string.Empty);
        return input;
      }
    }

    /// <summary>Serializes object to Json string, write to file.</summary>
    /// <param name="source">Object to serialize.</param>
    /// <param name="filename">File name.</param>
    /// <param name="overwrite">Whether overwrite exists file.</param>
    /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
    /// <returns>File full path.</returns>
    public static string WriteDataContractJson(this object source, string filename, bool overwrite = false, Type[] knownTypes = null)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (string.IsNullOrEmpty(filename))
        throw new ArgumentNullException(nameof (filename));
      string fullPath = Path.GetFullPath(filename);
      string directoryName = Path.GetDirectoryName(fullPath);
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
      using (FileStream fileStream = File.OpenWrite(fullPath))
      {
        (knownTypes == null || knownTypes.Length == 0 ? new DataContractJsonSerializer(source.GetType()) : new DataContractJsonSerializer(source.GetType(), (IEnumerable<Type>) knownTypes)).WriteObject((Stream) fileStream, source);
        return fullPath;
      }
    }

    /// <summary>Deserializes Json string to object.</summary>
    /// <param name="source">Json string object.</param>
    /// <param name="type">Type of object.</param>
    /// <param name="encoding">The encoding to apply to the string.</param>
    /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
    /// <returns>Instance of object.</returns>
    public static object DeserializeDataContractJsonString(this string source, Type type, Encoding encoding = null, Type[] knownTypes = null)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      DataContractJsonSerializer contractJsonSerializer = knownTypes == null || knownTypes.Length == 0 ? new DataContractJsonSerializer(type) : new DataContractJsonSerializer(type, (IEnumerable<Type>) knownTypes);
      using (MemoryStream memoryStream = new MemoryStream((encoding ?? Encoding.UTF8).GetBytes(source)))
      {
        memoryStream.Position = 0L;
        return contractJsonSerializer.ReadObject((Stream) memoryStream);
      }
    }

    /// <summary>Deserializes Json string to object.</summary>
    /// <param name="source">Json string object.</param>
    /// <param name="knownTypes">A <see cref="T:System.Type" /> array of object types to serialize.</param>
    /// <param name="encoding">The encoding to apply to the string.</param>
    /// <returns>Instance of object.</returns>
    public static object DeserializeDataContractJsonString(this string source, Type[] knownTypes, Encoding encoding = null)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      if (knownTypes == null || knownTypes.Length < 1)
        throw new ArgumentException("knownTypes is null or empty.", nameof (knownTypes));
      Type type = (Type) null;
      using (StringReader stringReader = new StringReader(source))
      {
        string rootNodeName = XElement.Load((TextReader) stringReader).Name.LocalName;
        type = ((IEnumerable<Type>) knownTypes).FirstOrDefault<Type>((Func<Type, bool>) (p => p.Name == rootNodeName));
        if (type == null)
          throw new InvalidOperationException();
      }
      DataContractJsonSerializer contractJsonSerializer = new DataContractJsonSerializer(type, (IEnumerable<Type>) knownTypes);
      using (MemoryStream memoryStream = new MemoryStream((encoding ?? Encoding.UTF8).GetBytes(source)))
      {
        memoryStream.Position = 0L;
        return contractJsonSerializer.ReadObject((Stream) memoryStream);
      }
    }

    /// <summary>Deserializes Json string to object.</summary>
    /// <typeparam name="T">Type of the returns object.</typeparam>
    /// <param name="source">Json string object.</param>
    /// <param name="encoding">The encoding to apply to the string.</param>
    /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
    /// <returns>Instance of object.</returns>
    public static T DeserializeDataContractJsonString<T>(this string source, Encoding encoding = null, Type[] knownTypes = null)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      DataContractJsonSerializer contractJsonSerializer = knownTypes == null || knownTypes.Length == 0 ? new DataContractJsonSerializer(typeof (T)) : new DataContractJsonSerializer(typeof (T), (IEnumerable<Type>) knownTypes);
      using (MemoryStream memoryStream = new MemoryStream((encoding ?? Encoding.UTF8).GetBytes(source)))
      {
        memoryStream.Position = 0L;
        return (T) contractJsonSerializer.ReadObject((Stream) memoryStream);
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
        throw new ArgumentNullException(nameof (source));
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      string fullPath = Path.GetFullPath(source);
      if (!File.Exists(fullPath))
        throw new FileNotFoundException("The specified file does not exist.", fullPath);
      using (FileStream fileStream = File.OpenRead(fullPath))
        return (knownTypes == null || knownTypes.Length == 0 ? new DataContractJsonSerializer(type) : new DataContractJsonSerializer(type, (IEnumerable<Type>) knownTypes)).ReadObject((Stream) fileStream);
    }

    /// <summary>Deserializes Json string to object, read from file.</summary>
    /// <param name="source">File name.</param>
    /// <param name="knownTypes">A <see cref="T:System.Type" /> array of object types to serialize.</param>
    /// <returns>Instance of object.</returns>
    public static object ReadDataContractJson(this string source, Type[] knownTypes = null)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      if (knownTypes == null || knownTypes.Length < 1)
        throw new ArgumentException("knownTypes is null or empty.", nameof (knownTypes));
      string fullPath = Path.GetFullPath(source);
      if (!File.Exists(fullPath))
        throw new FileNotFoundException("The specified file does not exist.", fullPath);
      string rootNodeName = XElement.Load(fullPath).Name.LocalName;
      Type type = ((IEnumerable<Type>) knownTypes).FirstOrDefault<Type>((Func<Type, bool>) (p => p.Name == rootNodeName));
      if (type == null)
        throw new InvalidOperationException();
      using (FileStream fileStream = File.OpenRead(fullPath))
        return new DataContractJsonSerializer(type, (IEnumerable<Type>) knownTypes).ReadObject((Stream) fileStream);
    }

    /// <summary>Deserializes Json string to object, read from file.</summary>
    /// <typeparam name="T">Type of the returns object.</typeparam>
    /// <param name="source">File name.</param>
    /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
    /// <returns>Instance of object.</returns>
    public static T ReadDataContractJson<T>(this string source, Type[] knownTypes = null)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      string fullPath = Path.GetFullPath(source);
      if (!File.Exists(fullPath))
        throw new FileNotFoundException("The specified file does not exist.", fullPath);
      using (FileStream fileStream = File.OpenRead(fullPath))
        return (T) (knownTypes == null || knownTypes.Length == 0 ? new DataContractJsonSerializer(typeof (T)) : new DataContractJsonSerializer(typeof (T), (IEnumerable<Type>) knownTypes)).ReadObject((Stream) fileStream);
    }

    /// <summary>Serializes object to Json bytes.</summary>
    /// <param name="source">Object to serialize.</param>
    /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
    /// <returns>Json bytes.</returns>
    public static byte[] SerializeDataContractJsonBinary(this object source, Type[] knownTypes = null)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      using (MemoryStream memoryStream = new MemoryStream())
      {
        (knownTypes == null || knownTypes.Length == 0 ? new DataContractJsonSerializer(source.GetType()) : new DataContractJsonSerializer(source.GetType(), (IEnumerable<Type>) knownTypes)).WriteObject((Stream) memoryStream, source);
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
        throw new ArgumentNullException(nameof (source));
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      DataContractJsonSerializer contractJsonSerializer = knownTypes == null || knownTypes.Length == 0 ? new DataContractJsonSerializer(type) : new DataContractJsonSerializer(type, (IEnumerable<Type>) knownTypes);
      using (MemoryStream memoryStream = new MemoryStream(source))
      {
        memoryStream.Position = 0L;
        return contractJsonSerializer.ReadObject((Stream) memoryStream);
      }
    }

    /// <summary>Deserializes Json bytes to object.</summary>
    /// <param name="source">Json string object.</param>
    /// <param name="knownTypes">A <see cref="T:System.Type" /> array of object types to serialize.</param>
    /// <returns>Instance of object.</returns>
    public static object DeserializeDataContractJsonBinary(this byte[] source, Type[] knownTypes)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (knownTypes == null || knownTypes.Length < 1)
        throw new ArgumentException("knownTypes is null or empty.", nameof (knownTypes));
      using (MemoryStream memoryStream = new MemoryStream(source))
      {
        using (XmlReader reader = XmlReader.Create((Stream) memoryStream))
        {
          string rootNodeName = XElement.Load(reader).Name.LocalName;
          Type type = ((IEnumerable<Type>) knownTypes).FirstOrDefault<Type>((Func<Type, bool>) (p => p.Name == rootNodeName));
          if (type == null)
            throw new InvalidOperationException();
          memoryStream.Position = 0L;
          return new DataContractJsonSerializer(type, (IEnumerable<Type>) knownTypes).ReadObject((Stream) memoryStream);
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
        throw new ArgumentNullException(nameof (source));
      DataContractJsonSerializer contractJsonSerializer = knownTypes == null || knownTypes.Length == 0 ? new DataContractJsonSerializer(typeof (T)) : new DataContractJsonSerializer(typeof (T), (IEnumerable<Type>) knownTypes);
      using (MemoryStream memoryStream = new MemoryStream(source))
      {
        memoryStream.Position = 0L;
        return (T) contractJsonSerializer.ReadObject((Stream) memoryStream);
      }
    }

    /// <summary>Serializes object to Soap string.</summary>
    /// <param name="source">The object to serialize.</param>
    /// <returns>Soap string.</returns>
    public static string SerializeSoapString(this object source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new SoapFormatter().Serialize((Stream) memoryStream, source);
        memoryStream.Position = 0L;
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load((Stream) memoryStream);
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
        throw new ArgumentNullException(nameof (source));
      string fullPath = Path.GetFullPath(filename);
      string directoryName = Path.GetDirectoryName(fullPath);
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
      using (FileStream fileStream = File.OpenWrite(fullPath))
      {
        new SoapFormatter().Serialize((Stream) fileStream, source);
        return fullPath;
      }
    }

    /// <summary>Deserializes Soap string to object.</summary>
    /// <param name="source">The Soap string to deserialize.</param>
    /// <returns>Instance of object.</returns>
    public static object DeserializeSoapString(this string source)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(source);
      SoapFormatter soapFormatter = new SoapFormatter();
      using (MemoryStream memoryStream = new MemoryStream())
      {
        xmlDocument.Save((Stream) memoryStream);
        memoryStream.Position = 0L;
        return soapFormatter.Deserialize((Stream) memoryStream);
      }
    }

    /// <summary>Deserializes Soap string to object.</summary>
    /// <typeparam name="T">Type of the returns object.</typeparam>
    /// <param name="source">The Soap string to deserialize.</param>
    /// <returns>Instance of T.</returns>
    public static T DeserializeSoapString<T>(this string source)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(source);
      SoapFormatter soapFormatter = new SoapFormatter();
      using (MemoryStream memoryStream = new MemoryStream())
      {
        xmlDocument.Save((Stream) memoryStream);
        memoryStream.Position = 0L;
        return (T) soapFormatter.Deserialize((Stream) memoryStream);
      }
    }

    /// <summary>Deserializes Soap string to object, read from file.</summary>
    /// <param name="source">File name.</param>
    /// <returns>Instance of object.</returns>
    public static object ReadSoap(this string source)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      string fullPath = Path.GetFullPath(source);
      if (!File.Exists(fullPath))
        throw new FileNotFoundException("The specified file does not exist.", fullPath);
      using (FileStream fileStream = File.OpenRead(fullPath))
        return new SoapFormatter().Deserialize((Stream) fileStream);
    }

    /// <summary>Deserializes Soap string to object, read from file.</summary>
    /// <typeparam name="T">Type of the returns object.</typeparam>
    /// <param name="source">File name.</param>
    /// <returns>Instance of T.</returns>
    public static T ReadSoap<T>(this string source)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      string fullPath = Path.GetFullPath(source);
      if (!File.Exists(fullPath))
        throw new FileNotFoundException("The specified file does not exist.", fullPath);
      using (FileStream fileStream = File.OpenRead(fullPath))
        return (T) new SoapFormatter().Deserialize((Stream) fileStream);
    }

    /// <summary>Serializes object to Soap bytes.</summary>
    /// <param name="source">The object to serialize.</param>
    /// <returns>Bytes representation of the source object.</returns>
    public static byte[] SerializeSoapBinary(this object source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new SoapFormatter().Serialize((Stream) memoryStream, source);
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
        throw new ArgumentNullException(nameof (source));
      SoapFormatter soapFormatter = new SoapFormatter();
      using (MemoryStream memoryStream = new MemoryStream(source))
      {
        memoryStream.Position = 0L;
        return soapFormatter.Deserialize((Stream) memoryStream);
      }
    }

    /// <summary>Deserializes Soap bytes to object.</summary>
    /// <typeparam name="T">Type of the returns object.</typeparam>
    /// <param name="source">The bytes to deserialize.</param>
    /// <returns>Instance of Soap object.</returns>
    public static T DeserializeSoapBinary<T>(this byte[] source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      SoapFormatter soapFormatter = new SoapFormatter();
      using (MemoryStream memoryStream = new MemoryStream(source))
      {
        memoryStream.Position = 0L;
        return (T) soapFormatter.Deserialize((Stream) memoryStream);
      }
    }

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
    public static string SerializeXmlString(this object source, bool indent = false, bool omitXmlDeclaration = true, bool removeDefaultNamespace = true, Type[] extraTypes = null)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      XmlSerializer xmlSerializer = extraTypes == null || extraTypes.Length == 0 ? new XmlSerializer(source.GetType()) : new XmlSerializer(source.GetType(), extraTypes);
      using (MemoryStream memoryStream1 = new MemoryStream())
      {
        using (StreamReader streamReader = new StreamReader((Stream) memoryStream1))
        {
          MemoryStream memoryStream2 = memoryStream1;
          XmlWriterSettings settings = new XmlWriterSettings()
          {
            OmitXmlDeclaration = omitXmlDeclaration,
            Indent = indent,
            Encoding = (Encoding) new UTF8Encoding(false),
            CloseOutput = true
          };
          using (XmlWriter xmlWriter = XmlWriter.Create((Stream) memoryStream2, settings))
          {
            if (removeDefaultNamespace)
            {
              XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
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
    public static string WriteXml(this object source, string filename, bool overwrite = false, bool indent = true, bool omitXmlDeclaration = true, bool removeDefaultNamespace = true, Type[] extraTypes = null)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (string.IsNullOrEmpty(filename))
        throw new ArgumentNullException(nameof (filename));
      string fullPath = Path.GetFullPath(filename);
      string directoryName = Path.GetDirectoryName(fullPath);
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
      XmlSerializer xmlSerializer = extraTypes == null || extraTypes.Length == 0 ? new XmlSerializer(source.GetType()) : new XmlSerializer(source.GetType(), extraTypes);
      string outputFileName = fullPath;
      XmlWriterSettings settings = new XmlWriterSettings()
      {
        OmitXmlDeclaration = omitXmlDeclaration,
        Indent = indent,
        Encoding = (Encoding) new UTF8Encoding(false),
        CloseOutput = true
      };
      using (XmlWriter xmlWriter = XmlWriter.Create(outputFileName, settings))
      {
        if (removeDefaultNamespace)
        {
          XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
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
        throw new ArgumentNullException(nameof (source));
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      using (StringReader stringReader = new StringReader(source))
        return (extraTypes == null || extraTypes.Length == 0 ? new XmlSerializer(type) : new XmlSerializer(type, extraTypes)).Deserialize((TextReader) stringReader);
    }

    /// <summary>Deserializes Xml string to object.</summary>
    /// <param name="source">The Xml string to deserialize.</param>
    /// <param name="knownTypes">A <see cref="T:System.Type" /> array of object types to serialize.</param>
    /// <returns>Instance of object.</returns>
    public static object DeserializeXmlString(this string source, Type[] knownTypes)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      if (knownTypes == null || knownTypes.Length < 1)
        throw new ArgumentException("knownTypes is null or empty.", nameof (knownTypes));
      Type type = (Type) null;
      using (StringReader stringReader = new StringReader(source))
      {
        string rootNodeName = XElement.Load((TextReader) stringReader).Name.LocalName;
        type = ((IEnumerable<Type>) knownTypes).FirstOrDefault<Type>((Func<Type, bool>) (p => p.Name.Equals(rootNodeName, StringComparison.OrdinalIgnoreCase)));
        if (type == null)
          throw new InvalidOperationException();
      }
      using (StringReader stringReader = new StringReader(source))
        return new XmlSerializer(type, knownTypes).Deserialize((TextReader) stringReader);
    }

    /// <summary>Deserializes Xml string to object.</summary>
    /// <typeparam name="T">Type of the returns object.</typeparam>
    /// <param name="source">The Xml string to deserialize.</param>
    /// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
    /// <returns>Instance of T.</returns>
    public static T DeserializeXmlString<T>(this string source, Type[] extraTypes = null)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      using (StringReader stringReader = new StringReader(source))
        return (T) (extraTypes == null || extraTypes.Length == 0 ? new XmlSerializer(typeof (T)) : new XmlSerializer(typeof (T), extraTypes)).Deserialize((TextReader) stringReader);
    }

    /// <summary>Deserializes Xml string to object, read from file.</summary>
    /// <param name="source">File name.</param>
    /// <param name="type">Type of object.</param>
    /// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
    /// <returns>Instance of object.</returns>
    public static object ReadXml(this string source, Type type, Type[] extraTypes = null)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      string fullPath = Path.GetFullPath(source);
      if (!File.Exists(fullPath))
        throw new FileNotFoundException("The specified file does not exist.", fullPath);
      using (FileStream fileStream = File.OpenRead(fullPath))
        return (extraTypes == null || extraTypes.Length == 0 ? new XmlSerializer(type) : new XmlSerializer(type, extraTypes)).Deserialize((Stream) fileStream);
    }

    /// <summary>Deserializes Xml string to object, read from file.</summary>
    /// <param name="source">File name.</param>
    /// <param name="knownTypes">A <see cref="T:System.Type" /> array of object types to serialize.</param>
    /// <returns>Instance of object.</returns>
    public static object ReadXml(this string source, Type[] knownTypes)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      if (knownTypes == null || knownTypes.Length < 1)
        throw new ArgumentException("knownTypes is null or empty.", nameof (knownTypes));
      string fullPath = Path.GetFullPath(source);
      if (!File.Exists(fullPath))
        throw new FileNotFoundException("The specified file does not exist.", fullPath);
      string rootNodeName = XElement.Load(fullPath).Name.LocalName;
      Type type = ((IEnumerable<Type>) knownTypes).FirstOrDefault<Type>((Func<Type, bool>) (p => p.Name.Equals(rootNodeName, StringComparison.OrdinalIgnoreCase)));
      if (type == null)
        throw new InvalidOperationException();
      using (FileStream fileStream = File.OpenRead(fullPath))
        return new XmlSerializer(type, knownTypes).Deserialize((Stream) fileStream);
    }

    /// <summary>Deserializes Xml string to object, read from file.</summary>
    /// <typeparam name="T">Type of the returns object.</typeparam>
    /// <param name="source">File name.</param>
    /// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
    /// <returns>Instance of T.</returns>
    public static T ReadXml<T>(this string source, Type[] extraTypes = null)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      string fullPath = Path.GetFullPath(source);
      if (!File.Exists(fullPath))
        throw new FileNotFoundException("The specified file does not exist.", fullPath);
      using (FileStream fileStream = File.OpenRead(fullPath))
        return (T) (extraTypes == null || extraTypes.Length == 0 ? new XmlSerializer(typeof (T)) : new XmlSerializer(typeof (T), extraTypes)).Deserialize((Stream) fileStream);
    }

    /// <summary>Serializes object to Xml bytes.</summary>
    /// <param name="source">The object to serialize.</param>
    /// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
    /// <returns>Bytes representation of the source object.</returns>
    public static byte[] SerializeXmlBinary(this object source, Type[] extraTypes = null)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      using (MemoryStream memoryStream = new MemoryStream())
      {
        (extraTypes == null || extraTypes.Length == 0 ? new XmlSerializer(source.GetType()) : new XmlSerializer(source.GetType(), extraTypes)).Serialize((Stream) memoryStream, source);
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
        throw new ArgumentNullException(nameof (source));
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      using (MemoryStream memoryStream = new MemoryStream(source))
        return (extraTypes == null || extraTypes.Length == 0 ? new XmlSerializer(type) : new XmlSerializer(type, extraTypes)).Deserialize((Stream) memoryStream);
    }

    /// <summary>Deserializes Xml bytes to object.</summary>
    /// <param name="source">The bytes to deserialize.</param>
    /// <param name="knownTypes">A <see cref="T:System.Type" /> array of object types to serialize.</param>
    /// <returns>Instance of Xml object.</returns>
    public static object DeserializeXmlBinary(this byte[] source, Type[] knownTypes = null)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (knownTypes == null || knownTypes.Length < 1)
        throw new ArgumentException("knownTypes is null or empty.", nameof (knownTypes));
      using (MemoryStream memoryStream = new MemoryStream(source))
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load((Stream) memoryStream);
        string rootNodeName = xmlDocument.LocalName;
        Type type = ((IEnumerable<Type>) knownTypes).FirstOrDefault<Type>((Func<Type, bool>) (p => p.Name == rootNodeName));
        if (type == null)
          throw new InvalidOperationException();
        memoryStream.Position = 0L;
        return new XmlSerializer(type, knownTypes).Deserialize((Stream) memoryStream);
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
        throw new ArgumentNullException(nameof (source));
      using (MemoryStream memoryStream = new MemoryStream(source))
        return (T) (knownTypes == null || knownTypes.Length == 0 ? new XmlSerializer(typeof (T)) : new XmlSerializer(typeof (T), knownTypes)).Deserialize((Stream) memoryStream);
    }

    /// <summary>Serializes DataContract object to Xml string.</summary>
    /// <param name="source">The DataContract object to serialize.</param>
    /// <param name="indent">Whether to write individual elements on new lines and indent.</param>
    /// <param name="omitXmlDeclaration">Whether to write an Xml declaration.</param>
    /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
    /// <returns>An Xml encoded string representation of the source DataContract object.</returns>
    public static string SerializeDataContractXmlString(this object source, bool indent = false, bool omitXmlDeclaration = true, Type[] knownTypes = null)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      DataContractSerializer contractSerializer = knownTypes == null || knownTypes.Length == 0 ? new DataContractSerializer(source.GetType()) : new DataContractSerializer(source.GetType(), (IEnumerable<Type>) knownTypes);
      using (MemoryStream memoryStream1 = new MemoryStream())
      {
        using (StreamReader streamReader = new StreamReader((Stream) memoryStream1))
        {
          MemoryStream memoryStream2 = memoryStream1;
          XmlWriterSettings settings = new XmlWriterSettings()
          {
            OmitXmlDeclaration = omitXmlDeclaration,
            Indent = indent,
            Encoding = (Encoding) new UTF8Encoding(false),
            CloseOutput = true
          };
          using (XmlWriter writer = XmlWriter.Create((Stream) memoryStream2, settings))
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
    public static string WriteDataContractXml(this object source, string filename, bool overwrite = false, bool indent = true, bool omitXmlDeclaration = true, Type[] knownTypes = null)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (string.IsNullOrEmpty(filename))
        throw new ArgumentNullException(nameof (filename));
      string fullPath = Path.GetFullPath(filename);
      string directoryName = Path.GetDirectoryName(fullPath);
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
      DataContractSerializer contractSerializer = knownTypes == null || knownTypes.Length == 0 ? new DataContractSerializer(source.GetType()) : new DataContractSerializer(source.GetType(), (IEnumerable<Type>) knownTypes);
      string outputFileName = fullPath;
      XmlWriterSettings settings = new XmlWriterSettings()
      {
        OmitXmlDeclaration = omitXmlDeclaration,
        Indent = indent,
        Encoding = (Encoding) new UTF8Encoding(false),
        CloseOutput = true
      };
      using (XmlWriter writer = XmlWriter.Create(outputFileName, settings))
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
        throw new ArgumentNullException(nameof (source));
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      DataContractSerializer contractSerializer = knownTypes == null || knownTypes.Length == 0 ? new DataContractSerializer(type) : new DataContractSerializer(type, (IEnumerable<Type>) knownTypes);
      StringReader stringReader = new StringReader(source);
      XmlReaderSettings settings = new XmlReaderSettings()
      {
        CheckCharacters = false,
        IgnoreComments = true,
        IgnoreWhitespace = true,
        IgnoreProcessingInstructions = true,
        CloseInput = true
      };
      using (XmlReader reader = XmlReader.Create((TextReader) stringReader, settings))
        return contractSerializer.ReadObject(reader);
    }

    /// <summary>Deserializes DataContract Xml string to object.</summary>
    /// <param name="source">The DataContract Xml string to deserialize.</param>
    /// <param name="knownTypes">A <see cref="T:System.Type" /> array of object types to serialize.</param>
    /// <returns>Instance of DataContract object.</returns>
    public static object DeserializeDataContractXmlString(this string source, Type[] knownTypes)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      if (knownTypes == null || knownTypes.Length < 1)
        throw new ArgumentException("knownTypes is null or empty.", nameof (knownTypes));
      Type type = (Type) null;
      using (StringReader stringReader = new StringReader(source))
      {
        string rootNodeName = XElement.Load((TextReader) stringReader).Name.LocalName;
        type = ((IEnumerable<Type>) knownTypes).FirstOrDefault<Type>((Func<Type, bool>) (p => p.Name == rootNodeName));
        if (type == null)
          throw new InvalidOperationException();
      }
      DataContractSerializer contractSerializer = new DataContractSerializer(type, (IEnumerable<Type>) knownTypes);
      StringReader stringReader1 = new StringReader(source);
      XmlReaderSettings settings = new XmlReaderSettings()
      {
        CheckCharacters = false,
        IgnoreComments = true,
        IgnoreWhitespace = true,
        IgnoreProcessingInstructions = true,
        CloseInput = true
      };
      using (XmlReader reader = XmlReader.Create((TextReader) stringReader1, settings))
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
        throw new ArgumentNullException(nameof (source));
      DataContractSerializer contractSerializer = knownTypes == null || knownTypes.Length == 0 ? new DataContractSerializer(typeof (T)) : new DataContractSerializer(typeof (T), (IEnumerable<Type>) knownTypes);
      StringReader stringReader = new StringReader(source);
      XmlReaderSettings settings = new XmlReaderSettings()
      {
        CheckCharacters = false,
        IgnoreComments = true,
        IgnoreWhitespace = true,
        IgnoreProcessingInstructions = true,
        CloseInput = true
      };
      using (XmlReader reader = XmlReader.Create((TextReader) stringReader, settings))
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
        throw new ArgumentNullException(nameof (source));
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      string fullPath = Path.GetFullPath(source);
      if (!File.Exists(fullPath))
        throw new FileNotFoundException("The specified file does not exist.", fullPath);
      using (FileStream fileStream = File.OpenRead(fullPath))
        return (knownTypes == null || knownTypes.Length == 0 ? new DataContractSerializer(type) : new DataContractSerializer(type, (IEnumerable<Type>) knownTypes)).ReadObject((Stream) fileStream);
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
        throw new ArgumentNullException(nameof (source));
      if (knownTypes == null || knownTypes.Length < 1)
        throw new ArgumentException("knownTypes is null or empty.", nameof (knownTypes));
      string fullPath = Path.GetFullPath(source);
      if (!File.Exists(fullPath))
        throw new FileNotFoundException("The specified file does not exist.", fullPath);
      string rootNodeName = XElement.Load(fullPath).Name.LocalName;
      Type type = ((IEnumerable<Type>) knownTypes).FirstOrDefault<Type>((Func<Type, bool>) (p => p.Name == rootNodeName));
      if (type == null)
        throw new InvalidOperationException();
      using (FileStream fileStream = File.OpenRead(fullPath))
        return new DataContractSerializer(type, (IEnumerable<Type>) knownTypes).ReadObject((Stream) fileStream);
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
        throw new ArgumentNullException(nameof (source));
      string fullPath = Path.GetFullPath(source);
      if (!File.Exists(fullPath))
        throw new FileNotFoundException("The specified file does not exist.", fullPath);
      using (FileStream fileStream = File.OpenRead(fullPath))
        return (T) (knownTypes == null || knownTypes.Length == 0 ? new DataContractSerializer(typeof (T)) : new DataContractSerializer(typeof (T), (IEnumerable<Type>) knownTypes)).ReadObject((Stream) fileStream);
    }

    /// <summary>Serializes DataContract object to bytes.</summary>
    /// <param name="source">The DataContract object to serialize.</param>
    /// <param name="knownTypes">A <see cref="T:System.Type" /> array that may be present in the object graph.</param>
    /// <returns>Bytes representation of the source DataContract object.</returns>
    public static byte[] SerializeDataContractXmlBinary(this object source, Type[] knownTypes = null)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      using (MemoryStream memoryStream = new MemoryStream())
      {
        (knownTypes == null || knownTypes.Length == 0 ? new DataContractSerializer(source.GetType()) : new DataContractSerializer(source.GetType(), (IEnumerable<Type>) knownTypes)).WriteObject((Stream) memoryStream, source);
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
        throw new ArgumentNullException(nameof (source));
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      DataContractSerializer contractSerializer = knownTypes == null || knownTypes.Length == 0 ? new DataContractSerializer(type) : new DataContractSerializer(type, (IEnumerable<Type>) knownTypes);
      using (MemoryStream memoryStream = new MemoryStream(source))
      {
        memoryStream.Position = 0L;
        return contractSerializer.ReadObject((Stream) memoryStream);
      }
    }

    /// <summary>Deserializes DataContract bytes to object.</summary>
    /// <param name="source">The bytes to deserialize.</param>
    /// <param name="knownTypes">A <see cref="T:System.Type" /> array of object types to serialize.</param>
    /// <returns>Instance of DataContract object.</returns>
    public static object DeserializeDataContractXmlBinary(this byte[] source, Type[] knownTypes = null)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (knownTypes == null || knownTypes.Length < 1)
        throw new ArgumentException("knownTypes is null or empty.", nameof (knownTypes));
      using (MemoryStream memoryStream = new MemoryStream(source))
      {
        using (XmlReader reader = XmlReader.Create((Stream) memoryStream))
        {
          string rootNodeName = XElement.Load(reader).Name.LocalName;
          Type type = ((IEnumerable<Type>) knownTypes).FirstOrDefault<Type>((Func<Type, bool>) (p => p.Name == rootNodeName));
          if (type == null)
            throw new InvalidOperationException();
          memoryStream.Position = 0L;
          return new DataContractSerializer(type, (IEnumerable<Type>) knownTypes).ReadObject((Stream) memoryStream);
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
        throw new ArgumentNullException(nameof (source));
      DataContractSerializer contractSerializer = knownTypes == null || knownTypes.Length == 0 ? new DataContractSerializer(typeof (T)) : new DataContractSerializer(typeof (T), (IEnumerable<Type>) knownTypes);
      using (MemoryStream memoryStream = new MemoryStream(source))
      {
        memoryStream.Position = 0L;
        return (T) contractSerializer.ReadObject((Stream) memoryStream);
      }
    }

    /// <summary>Serializes object to Json string.</summary>
    /// <param name="source">Object to serialize.</param>
    /// <param name="omitTypeInfo">Whether to omit type information.</param>
    /// <returns>Json string.</returns>
    public static string SerializeJsonString(this object source, bool omitTypeInfo = false)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      string input = JsonSerializer.Serialize(source);
      if (omitTypeInfo)
        return SerializationExtensions.JsonTypeInfoRegex.Replace(input, string.Empty);
      return input;
    }

    /// <summary>Serializes object to Json string, write to file.</summary>
    /// <param name="source">Object to serialize.</param>
    /// <param name="filename">File name.</param>
    /// <param name="overwrite">Whether overwrite exists file.</param>
    /// <param name="omitTypeInfo">Whether to omit type information.</param>
    /// <returns>File full path.</returns>
    public static string WriteJson(this object source, string filename, bool overwrite = false, bool omitTypeInfo = false)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (string.IsNullOrEmpty(filename))
        throw new ArgumentNullException(nameof (filename));
      string fullPath = Path.GetFullPath(filename);
      string directoryName = Path.GetDirectoryName(fullPath);
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
      string str = JsonSerializer.Serialize(source);
      if (omitTypeInfo)
        str = SerializationExtensions.JsonTypeInfoRegex.Replace(str, string.Empty);
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
        throw new ArgumentNullException(nameof (source));
      return JsonSerializer.Deserialize(source, type);
    }

    /// <summary>Deserializes Json string to object.</summary>
    /// <typeparam name="T">Type of the returns object.</typeparam>
    /// <param name="source">Json string object.</param>
    /// <returns>Instance of object.</returns>
    public static T DeserializeJsonString<T>(this string source)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      return JsonSerializer.Deserialize<T>(source);
    }

    /// <summary>Deserializes Json string to object, read from file.</summary>
    /// <param name="source">File name.</param>
    /// <param name="type">Type of object.</param>
    /// <returns>Instance of object.</returns>
    public static object ReadJson(this string source, Type type = null)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      string fullPath = Path.GetFullPath(source);
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
        throw new ArgumentNullException(nameof (source));
      string fullPath = Path.GetFullPath(source);
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
        throw new ArgumentNullException(nameof (source));
      return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(source));
    }

    /// <summary>Deserializes Json bytes to object.</summary>
    /// <param name="source">Json string object.</param>
    /// <param name="type">Type of object.</param>
    /// <returns>Instance of object.</returns>
    public static object DeserializeJsonBinary(this byte[] source, Type type = null)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return JsonSerializer.Deserialize(Encoding.UTF8.GetString(source), type);
    }

    /// <summary>Deserializes Json bytes to object.</summary>
    /// <typeparam name="T">Type of the returns object.</typeparam>
    /// <param name="source">Json string object.</param>
    /// <returns>Instance of object.</returns>
    public static T DeserializeJsonBinary<T>(this byte[] source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(source));
    }

    /// <summary>Serializes object to bytes.</summary>
    /// <param name="source">Source object.</param>
    /// <returns>Byte array.</returns>
    public static byte[] SerializeBinary(this object source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) memoryStream, source);
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
        throw new ArgumentNullException(nameof (source));
      if (string.IsNullOrEmpty(filename))
        throw new ArgumentNullException(nameof (filename));
      string fullPath = Path.GetFullPath(filename);
      string directoryName = Path.GetDirectoryName(fullPath);
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
      using (FileStream fileStream = File.OpenWrite(fullPath))
      {
        new BinaryFormatter().Serialize((Stream) fileStream, source);
        return fullPath;
      }
    }

    /// <summary>Deserializes bytes to object.</summary>
    /// <param name="source">Byte array.</param>
    /// <returns>Instance object.</returns>
    public static object DeserializeBinary(this byte[] source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (source.Length == 0)
        throw new ArgumentOutOfRangeException(nameof (source));
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      using (MemoryStream memoryStream = new MemoryStream(source))
      {
        memoryStream.Position = 0L;
        return binaryFormatter.Deserialize((Stream) memoryStream);
      }
    }

    /// <summary>Deserializes bytes to object, read from file.</summary>
    /// <param name="source">File name.</param>
    /// <returns>Instance object.</returns>
    public static object ReadBinary(this string source)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      string fullPath = Path.GetFullPath(source);
      if (!File.Exists(fullPath))
        throw new FileNotFoundException("The specified file does not exist.", fullPath);
      using (FileStream fileStream = File.OpenRead(fullPath))
        return new BinaryFormatter().Deserialize((Stream) fileStream);
    }

    /// <summary>Deserializes bytes to object.</summary>
    /// <typeparam name="T">The type of returns object.</typeparam>
    /// <param name="source">Byte array.</param>
    /// <returns>Instance of T.</returns>
    public static T DeserializeBinary<T>(this byte[] source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (source.Length == 0)
        throw new ArgumentOutOfRangeException(nameof (source));
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      using (MemoryStream memoryStream = new MemoryStream(source))
      {
        memoryStream.Position = 0L;
        return (T) binaryFormatter.Deserialize((Stream) memoryStream);
      }
    }

    /// <summary>Deserializes bytes to object, read from file.</summary>
    /// <typeparam name="T">The type of returns object.</typeparam>
    /// <param name="source">File name.</param>
    /// <returns>Instance of T.</returns>
    public static T ReadBinary<T>(this string source)
    {
      if (string.IsNullOrEmpty(source))
        throw new ArgumentNullException(nameof (source));
      string fullPath = Path.GetFullPath(source);
      if (!File.Exists(fullPath))
        throw new FileNotFoundException("The specified file does not exist.", fullPath);
      using (FileStream fileStream = File.OpenRead(fullPath))
        return (T) new BinaryFormatter().Deserialize((Stream) fileStream);
    }
  }
}
