// Decompiled with JetBrains decompiler
// Type: TestProj47.ObjectExtensions
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace HSNXT
{
    public static partial class Extensions

    {
        /// <summary>If object is null, invoke method.</summary>
        /// <typeparam name="T">The type of input object.</typeparam>
        /// <param name="source">Object to check.</param>
        /// <param name="action">Delegate method.
        /// <example>E.g. <code>source =&gt; DoSomething(source);</code></example>
        /// </param>
        /// <returns>Source object.</returns>
        public static T IfNull<T>(this T source, Action<T> action)
        {
            if (source == null)
                action(source);
            return source;
        }

        /// <summary>
        /// Invoke System.Console.WriteLine() or System.Console.Write().
        /// </summary>
        /// <typeparam name="T">The type of input object.</typeparam>
        /// <param name="source">Source object.</param>
        /// <param name="appendObj">Append object to display.</param>
        /// <param name="withNewLine">Whether followed by the current line terminator.</param>
        /// <returns>The input object.</returns>
        public static T ConsoleOutput<T>(this T source, object appendObj = null, bool withNewLine = true)
        {
            if (appendObj == null)
            {
                if (withNewLine)
                    Console.WriteLine(source);
                else
                    Console.Write(source);
                return source;
            }
            if (appendObj is string && (appendObj as string).Contains("{0}"))
            {
                if (withNewLine)
                    Console.WriteLine(appendObj as string, source);
                else
                    Console.Write(appendObj as string, source);
                return source;
            }
            if (withNewLine)
                Console.WriteLine("{0}{1}", source, appendObj);
            else
                Console.Write("{0}{1}", source, appendObj);
            return source;
        }

        /// <summary>Perform a deep Copy of the object.</summary>
        /// <typeparam name="T">The type of input object.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T CloneDeep<T>(this T source)
        {
            if (source == null)
                return default;
            try
            {
                var binaryFormatter = new BinaryFormatter();
                using (var memoryStream = new MemoryStream())
                {
                    binaryFormatter.Serialize(memoryStream, source);
                    memoryStream.Position = 0L;
                    return (T) binaryFormatter.Deserialize(memoryStream);
                }
            }
            catch
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                using (var memoryStream = new MemoryStream())
                {
                    xmlSerializer.Serialize(memoryStream, source);
                    memoryStream.Position = 0L;
                    return (T) xmlSerializer.Deserialize(memoryStream);
                }
            }
        }

        /// <summary>
        /// Converts an object to the specified target type or returns the default value if those two types are not convertible.
        /// </summary>
        /// <typeparam name="T">The type of returns object.</typeparam>
        /// <param name="source">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="throwOnError">true to throw any exception that occurs.-or- false to ignore any exception that occurs.</param>
        /// <returns>The target type object.</returns>
        public static T ConvertTo<T>(this object source, T defaultValue = default, bool throwOnError = false)
        {
            if (source == null)
            {
                if (throwOnError)
                    throw new ArgumentNullException(nameof(source));
                return default;
            }
            try
            {
                var type = typeof(T);
                if (source.GetType() == type)
                    return (T) source;
                var converter1 = TypeDescriptor.GetConverter(source);
                if (converter1 != null && converter1.CanConvertTo(type))
                    return (T) converter1.ConvertTo(source, type);
                var converter2 = TypeDescriptor.GetConverter(type);
                if (converter2 != null && converter2.CanConvertFrom(source.GetType()))
                    return (T) converter2.ConvertFrom(source);
                throw new InvalidOperationException();
            }
            catch
            {
                if (!throwOnError)
                    return defaultValue;
                throw;
            }
        }

        /// <summary>
        /// Converts an object to the specified target type or returns null if those two types are not convertible.
        /// </summary>
        /// <param name="source">The value.</param>
        /// <param name="targetType">The type of returns object.</param>
        /// <param name="throwOnError">true to throw any exception that occurs.-or- false to ignore any exception that occurs.</param>
        /// <returns>The target type object.</returns>
        public static object ConvertTo(this object source, Type targetType, bool throwOnError = false)
        {
            if (source == null)
            {
                if (throwOnError)
                    throw new ArgumentNullException(nameof(source));
                return null;
            }
            try
            {
                if (source.GetType() == targetType)
                    return source;
                var converter1 = TypeDescriptor.GetConverter(source);
                if (converter1 != null && converter1.CanConvertTo(targetType))
                    return converter1.ConvertTo(source, targetType);
                var converter2 = TypeDescriptor.GetConverter(targetType);
                if (converter2 != null && converter2.CanConvertFrom(source.GetType()))
                    return converter2.ConvertFrom(source);
                throw new InvalidOperationException();
            }
            catch
            {
                if (!throwOnError)
                    return null;
                throw;
            }
        }

        /// <summary>
        /// Copies the readable and writable public property values from the target object to the source and
        /// optionally allows for the ignoring of any number of properties.
        /// </summary>
        /// <remarks>The source and target objects must be of the same type.</remarks>
        /// <param name="source">The source object.</param>
        /// <param name="copySource">The object to copy from.</param>
        /// <param name="ignoreProperties">An array of property names to ignore.</param>
        /// <returns>The original object.</returns>
        public static object CopyPropertiesFrom(this object source, object copySource, params string[] ignoreProperties)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (copySource == null)
                throw new ArgumentNullException("target");
            var type = copySource.GetType();
            if (source.GetType() != type)
                throw new ArgumentException("The target type must be the same as the source");
            var stringList = new List<string>();
            if (ignoreProperties != null && ignoreProperties.Length > 0)
            {
                foreach (var ignoreProperty in ignoreProperties)
                {
                    if (!ignoreProperty.IsNullOrWhiteSpace() && !stringList.Contains(ignoreProperty))
                        stringList.Add(ignoreProperty);
                }
            }
            foreach (var property in type.GetProperties())
            {
                if (property.CanWrite && property.CanRead && !stringList.Contains(property.Name))
                {
                    var obj = property.GetValue(copySource, null);
                    property.SetValue(source, obj, null);
                }
            }
            return source;
        }

        /// <summary>
        /// Copies the property values from the dictionary to the source and
        /// optionally allows for the ignoring of any number of properties.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="copySource">The dictionary to copy from.</param>
        /// <param name="ignoreProperties">An array of property names to ignore.</param>
        /// <returns>The original object.</returns>
        public static object CopyPropertiesFromDictionary(this object source, Dictionary<string, object> copySource,
            params string[] ignoreProperties)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (copySource == null)
                throw new ArgumentNullException("target");
            var type = source.GetType();
            var stringList = new List<string>();
            if (ignoreProperties != null && ignoreProperties.Length > 0)
            {
                foreach (var ignoreProperty in ignoreProperties)
                {
                    if (!ignoreProperty.IsNullOrWhiteSpace() && !stringList.Contains(ignoreProperty))
                        stringList.Add(ignoreProperty);
                }
            }
            foreach (var property in type.GetProperties())
            {
                if (property.CanWrite && !stringList.Contains(property.Name) && copySource.ContainsKey(property.Name))
                    property.SetValue(source, copySource[property.Name], null);
            }
            return source;
        }

        /// <summary>
        /// Copies the property values from the dictionary to the source and
        /// optionally allows for the ignoring of any number of properties.
        /// </summary>
        /// <param name="source">The dictionary to copy from.</param>
        /// <param name="target">The object to copy to.</param>
        /// <param name="ignoreProperties">An array of property names to ignore.</param>
        /// <returns>The original object.</returns>
        public static object CopyPropertiesTo(this Dictionary<string, object> source, object target,
            params string[] ignoreProperties)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            var type = target.GetType();
            var stringList = new List<string>();
            if (ignoreProperties != null && ignoreProperties.Length > 0)
            {
                foreach (var ignoreProperty in ignoreProperties)
                {
                    if (!ignoreProperty.IsNullOrWhiteSpace() && !stringList.Contains(ignoreProperty))
                        stringList.Add(ignoreProperty);
                }
            }
            foreach (var property in type.GetProperties())
            {
                if (property.CanWrite && !stringList.Contains(property.Name) && source.ContainsKey(property.Name))
                    property.SetValue(source, source[property.Name], null);
            }
            return source;
        }

        /// <summary>
        /// Creates a new object and copies the property values from the dictionary and
        /// optionally allows for the ignoring of any number of properties.
        /// </summary>
        /// <param name="source">The dictionary.</param>
        /// <param name="type">The result object type.</param>
        /// <param name="ignoreProperties">An array of property names to ignore.</param>
        /// <returns>The result object.</returns>
        public static object ToObject(this Dictionary<string, object> source, Type type,
            params string[] ignoreProperties)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            var instance = type.CreateInstance();
            var stringList = new List<string>();
            if (ignoreProperties != null && ignoreProperties.Length > 0)
            {
                foreach (var ignoreProperty in ignoreProperties)
                {
                    if (!ignoreProperty.IsNullOrWhiteSpace() && !stringList.Contains(ignoreProperty))
                        stringList.Add(ignoreProperty);
                }
            }
            foreach (var property in type.GetProperties())
            {
                if (property.CanWrite && !stringList.Contains(property.Name) && source.ContainsKey(property.Name))
                    property.SetValue(instance, source[property.Name], null);
            }
            return instance;
        }

        /// <summary>
        /// Creates a new object and copies the property values from the dictionary and
        /// optionally allows for the ignoring of any number of properties.
        /// </summary>
        /// <typeparam name="T">The result object type.</typeparam>
        /// <param name="source">The dictionary.</param>
        /// <param name="ignoreProperties">An array of property names to ignore.</param>
        /// <returns>The result object.</returns>
        public static T ToObject<T>(this Dictionary<string, object> source, params string[] ignoreProperties)
            where T : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var instance = (T) typeof(T).CreateInstance();
            var stringList = new List<string>();
            if (ignoreProperties != null && ignoreProperties.Length > 0)
            {
                foreach (var ignoreProperty in ignoreProperties)
                {
                    if (!ignoreProperty.IsNullOrWhiteSpace() && !stringList.Contains(ignoreProperty))
                        stringList.Add(ignoreProperty);
                }
            }
            foreach (var property in typeof(T).GetProperties())
            {
                if (property.CanWrite && !stringList.Contains(property.Name) && source.ContainsKey(property.Name))
                    property.SetValue(instance, source[property.Name], null);
            }
            return instance;
        }

        /// <summary>
        /// Tries to dispose the object if not null and if it implements IDisposable.
        /// </summary>
        /// <param name="source">The source to dispose.</param>
        /// <returns>true if call Dispose succeeded; otherwise, false.</returns>
        public static bool TryDispose(this object source)
        {
            var disposable = source as IDisposable;
            if (disposable == null)
                return false;
            disposable.Dispose();
            return true;
        }
    }
}