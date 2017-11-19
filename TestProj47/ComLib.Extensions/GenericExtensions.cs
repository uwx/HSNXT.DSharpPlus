﻿using System;
using System.IO;
using System.Xml.Serialization;

namespace HSNXT
{
    public static partial class Extensions
    {
        private static readonly Random Rand = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// Checks if object is null or default
        /// <para>
        /// Checks for strings as well if null or empty/null or whitespace.
        /// </para>
        /// </summary>
        public static bool IsNullOrEmpty<T>(this T obj)
        {
            if (obj == null) return true;
            if (!(obj is string)) return false;
            return string.IsNullOrEmpty(obj.ToString()) || string.IsNullOrWhiteSpace(obj.ToString());
        }

        /// <summary>
        /// Serialize the object into a string.
        /// </summary>
        public static string Serialize<T>(this T obj)
        {
            var xmlSerializer = new XmlSerializer(obj.GetType());

            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
        }

        /// <summary>
        /// Deserializes string into object.
        /// </summary>
        public static T Deserialize<T>(this string data)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(data))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
        
        /// <summary>
        /// Returns a random entry in an array
        /// </summary>
        /// <returns>Random entry in array</returns>
        public static T GetRandom<T>(this T[] val)
        {
            return val[Rand.Next(val.Length)];
        }
    }
}
