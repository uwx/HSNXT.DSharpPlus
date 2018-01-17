using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;
#if NetFX
using System.Web.Script.Serialization;
#endif

//using System.Data.SqlServerCe;

// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Text;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     An object extension method that serialize an object to binary.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string SerializeBinary<T>(this T @this)
        {
            var binaryWrite = new BinaryFormatter();

            using (var memoryStream = new MemoryStream())
            {
                binaryWrite.Serialize(memoryStream, @this);
                return Encoding.Default.GetString(memoryStream.ToArray());
            }
        }

        /// <summary>
        ///     An object extension method that serialize an object to binary.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>A string.</returns>
        public static string SerializeBinary<T>(this T @this, Encoding encoding)
        {
            var binaryWrite = new BinaryFormatter();

            using (var memoryStream = new MemoryStream())
            {
                binaryWrite.Serialize(memoryStream, @this);
                return encoding.GetString(memoryStream.ToArray());
            }
        }

#if NetFX
        /// <summary>
        ///     A T extension method that serialize java script.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string SerializeJavaScript<T>(this T @this)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(@this);
        }
#endif
        
        /// <summary>
        ///     A T extension method that serialize an object to Json.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The Json string.</returns>
        public static string SerializeJson<T>(this T @this)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            using (var memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, @this);
                return Encoding.Default.GetString(memoryStream.ToArray());
            }
        }

        /// <summary>
        ///     A T extension method that serialize an object to Json.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The Json string.</returns>
        public static string SerializeJson<T>(this T @this, Encoding encoding)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            using (var memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, @this);
                return encoding.GetString(memoryStream.ToArray());
            }
        }

        /// <summary>
        ///     An object extension method that serialize a string to XML.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The string representation of the Xml Serialization.</returns>
        public static string SerializeXml(this object @this)
        {
            var xmlSerializer = new XmlSerializer(@this.GetType());

            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, @this);
                using (var streamReader = new StringReader(stringWriter.GetStringBuilder().ToString()))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        /// <summary>
        ///     A string extension method that deserialize a string binary as &lt;T&gt;.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The desrialize binary as &lt;T&gt;</returns>
        public static T DeserializeBinary<T>(this string @this)
        {
            using (var stream = new MemoryStream(Encoding.Default.GetBytes(@this)))
            {
                var binaryRead = new BinaryFormatter();
                return (T) binaryRead.Deserialize(stream);
            }
        }

        /// <summary>
        ///     A string extension method that deserialize a string binary as &lt;T&gt;.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The desrialize binary as &lt;T&gt;</returns>
        public static T DeserializeBinary<T>(this string @this, Encoding encoding)
        {
            using (var stream = new MemoryStream(encoding.GetBytes(@this)))
            {
                var binaryRead = new BinaryFormatter();
                return (T) binaryRead.Deserialize(stream);
            }
        }

#if NetFX
        /// <summary>
        ///     A string extension method that deserialize a string binary as &lt;T&gt;.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The desrialize binary as &lt;T&gt;</returns>
        public static T DeserializeJavaScript<T>(this string @this)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(@this);
        }
#endif
        
        /// <summary>
        ///     A string extension method that deserialize a Json string to object.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The object deserialized.</returns>
        public static T DeserializeJson<T>(this string @this)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            using (var stream = new MemoryStream(Encoding.Default.GetBytes(@this)))
            {
                return (T) serializer.ReadObject(stream);
            }
        }

        /// <summary>
        ///     A string extension method that deserialize a Json string to object.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The object deserialized.</returns>
        public static T DeserializeJson<T>(this string @this, Encoding encoding)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            using (var stream = new MemoryStream(encoding.GetBytes(@this)))
            {
                return (T) serializer.ReadObject(stream);
            }
        }

        /// <summary>
        ///     A string extension method that deserialize an Xml as &lt;T&gt;.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The desieralize Xml as &lt;T&gt;</returns>
        public static T DeserializeXml<T>(this string @this)
        {
            var x = new XmlSerializer(typeof(T));
            var r = new StringReader(@this);

            return (T) x.Deserialize(r);
        }
    }
}