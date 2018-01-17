// Decompiled with JetBrains decompiler
// Type: TestProj47.ReflectionUtilities
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace HSNXT
{
    /// <summary>Class ReflectionUtilities.</summary>
    internal class ReflectionUtilities
    {
        /// <summary>The empty objects</summary>
        private static readonly object[] EmptyObjects = new object[0];

        /// <summary>Gets the attribute.</summary>
        /// <param name="info">The information.</param>
        /// <param name="type">The type.</param>
        /// <returns>The Attribute.</returns>
        public static Attribute GetAttribute(MemberInfo info, Type type)
        {
            if (info == null || type == null || !Attribute.IsDefined(info, type))
                return null;
            return Attribute.GetCustomAttribute(info, type);
        }

        /// <summary>Gets the attribute.</summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="attributeType">Type of the attribute.</param>
        /// <returns>The Attribute.</returns>
        public static Attribute GetAttribute(Type objectType, Type attributeType)
        {
            if (objectType == null || attributeType == null || !Attribute.IsDefined(objectType, attributeType))
                return null;
            return Attribute.GetCustomAttribute(objectType, attributeType);
        }

        /// <summary>Gets the constructor.</summary>
        /// <param name="constructorInfo">The constructor information.</param>
        /// <returns>The ConstructorDelegate.</returns>
        public static ConstructorDelegate GetConstructor(ConstructorInfo constructorInfo)
        {
            return GetConstructorByReflection(constructorInfo);
        }

        /// <summary>Gets the constructor.</summary>
        /// <param name="type">The type.</param>
        /// <param name="argsType">Type of the arguments.</param>
        /// <returns>The ConstructorDelegate.</returns>
        public static ConstructorDelegate GetConstructor(Type type, params Type[] argsType)
        {
            return GetConstructorByReflection(type, argsType);
        }

        /// <summary>Gets the constructor by reflection.</summary>
        /// <param name="constructorInfo">The constructor information.</param>
        /// <returns>The ConstructorDelegate.</returns>
        public static ConstructorDelegate GetConstructorByReflection(ConstructorInfo constructorInfo)
        {
            if (IsEnumerable(constructorInfo.DeclaringType))
                return args => (object) CreateIList(constructorInfo.DeclaringType, args);
            return args => constructorInfo.Invoke(args);
        }

        /// <summary>Gets the constructor by reflection.</summary>
        /// <param name="type">The type.</param>
        /// <param name="argsType">Type of the arguments.</param>
        /// <returns>The ConstructorDelegate.</returns>
        public static ConstructorDelegate GetConstructorByReflection(Type type, params Type[] argsType)
        {
            var constructorInfo = GetConstructorInfo(type, argsType);
            if (constructorInfo != null)
                return GetConstructorByReflection(constructorInfo);
            return null;
        }

        /// <summary>Gets the constructor information.</summary>
        /// <param name="type">The type.</param>
        /// <param name="argsType">Type of the arguments.</param>
        /// <returns>The ConstructorInfo.</returns>
        public static ConstructorInfo GetConstructorInfo(Type type, params Type[] argsType)
        {
            foreach (var constructor in GetConstructors(type))
            {
                var parameters = constructor.GetParameters();
                if (argsType.Length == parameters.Length)
                {
                    var index = 0;
                    var flag = true;
                    foreach (var parameter in constructor.GetParameters())
                    {
                        if (parameter.ParameterType != argsType[index])
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                        return constructor;
                }
            }
            return null;
        }

        /// <summary>Gets the constructors.</summary>
        /// <param name="type">The type.</param>
        /// <returns>The IEnumerable.</returns>
        public static IEnumerable<ConstructorInfo> GetConstructors(Type type)
        {
            return type.GetConstructors();
        }

        /// <summary>Gets the fields.</summary>
        /// <param name="type">The type.</param>
        /// <returns>The IEnumerable.</returns>
        public static IEnumerable<FieldInfo> GetFields(Type type)
        {
            return type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                                  BindingFlags.NonPublic);
        }

        /// <summary>Gets the type of the generic list element.</summary>
        /// <param name="type">The type.</param>
        /// <returns>The Type.</returns>
        public static Type GetGenericListElementType(Type type)
        {
            foreach (var type1 in type.GetInterfaces())
            {
                if (IsTypeGeneric(type1) && type1.GetGenericTypeDefinition() == typeof(IList<>))
                    return GetGenericTypeArguments(type1)[0];
            }
            return GetGenericTypeArguments(type)[0];
        }

        /// <summary>Gets the generic type arguments.</summary>
        /// <param name="type">The type.</param>
        /// <returns>The Type[].</returns>
        public static Type[] GetGenericTypeArguments(Type type)
        {
            return type.GetGenericArguments();
        }

        /// <summary>Gets the get method.</summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>The GetDelegate.</returns>
        public static GetDelegate GetGetMethod(PropertyInfo propertyInfo)
        {
            return GetGetMethodByReflection(propertyInfo);
        }

        /// <summary>Gets the get method.</summary>
        /// <param name="fieldInfo">The field information.</param>
        /// <returns>The GetDelegate.</returns>
        public static GetDelegate GetGetMethod(FieldInfo fieldInfo)
        {
            return GetGetMethodByReflection(fieldInfo);
        }

        /// <summary>Gets the get method by reflection.</summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>The GetDelegate.</returns>
        public static GetDelegate GetGetMethodByReflection(PropertyInfo propertyInfo)
        {
            var methodInfo = GetGetterMethodInfo(propertyInfo);
            return source => methodInfo.Invoke(source, EmptyObjects);
        }

        /// <summary>Gets the get method by reflection.</summary>
        /// <param name="fieldInfo">The field information.</param>
        /// <returns>The GetDelegate.</returns>
        public static GetDelegate GetGetMethodByReflection(FieldInfo fieldInfo)
        {
            return source => fieldInfo.GetValue(source);
        }

        /// <summary>Gets the getter method information.</summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>The MethodInfo.</returns>
        public static MethodInfo GetGetterMethodInfo(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetGetMethod(true);
        }

        /// <summary>Gets the properties.</summary>
        /// <param name="type">The type.</param>
        /// <returns>The IEnumerable.</returns>
        public static IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                                      BindingFlags.NonPublic);
        }

        /// <summary>Gets the set method.</summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>The SetDelegate.</returns>
        public static SetDelegate GetSetMethod(PropertyInfo propertyInfo)
        {
            return GetSetMethodByReflection(propertyInfo);
        }

        /// <summary>Gets the set method.</summary>
        /// <param name="fieldInfo">The field information.</param>
        /// <returns>The SetDelegate.</returns>
        public static SetDelegate GetSetMethod(FieldInfo fieldInfo)
        {
            return GetSetMethodByReflection(fieldInfo);
        }

        /// <summary>Gets the set method by reflection.</summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>The SetDelegate.</returns>
        public static SetDelegate GetSetMethodByReflection(PropertyInfo propertyInfo)
        {
            var methodInfo = GetSetterMethodInfo(propertyInfo);
            return (source, value) => methodInfo.Invoke(source, new object[1]
            {
                value
            });
        }

        /// <summary>Gets the set method by reflection.</summary>
        /// <param name="fieldInfo">The field information.</param>
        /// <returns>The SetDelegate.</returns>
        public static SetDelegate GetSetMethodByReflection(FieldInfo fieldInfo)
        {
            return (source, value) => fieldInfo.SetValue(source, value);
        }

        /// <summary>Gets the setter method information.</summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>The MethodInfo.</returns>
        public static MethodInfo GetSetterMethodInfo(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetSetMethod(true);
        }

        /// <summary>
        /// Determines whether the type1 is assignable from the type2.
        /// </summary>
        /// <param name="type1">The type1.</param>
        /// <param name="type2">The type2.</param>
        /// <returns>true if the type1 is assignable from type2; otherwise, false.</returns>
        public static bool IsAssignableFrom(Type type1, Type type2)
        {
            return type1.IsAssignableFrom(type2);
        }

        /// <summary>
        /// Determines whether the specified type is nullable type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>true if the specified type is nullable type; otherwise, false.</returns>
        public static bool IsNullableType(Type type)
        {
            if (type.IsGenericType)
                return type.GetGenericTypeDefinition() == typeof(Nullable<>);
            return false;
        }

        /// <summary>Determines whether the specified type is dictionary.</summary>
        /// <param name="type">The type.</param>
        /// <returns>true if the specified type is dictionary]; otherwise, false.</returns>
        public static bool IsTypeDictionary(Type type)
        {
            if (typeof(IDictionary).IsAssignableFrom(type))
                return true;
            if (!type.IsGenericType)
                return false;
            return type.GetGenericTypeDefinition() == typeof(IDictionary<,>);
        }

        /// <summary>Determines whether the specified type is generic.</summary>
        /// <param name="type">The type.</param>
        /// <returns>true if the specified type is generic type; otherwise, false.</returns>
        public static bool IsTypeGeneric(Type type)
        {
            return type.IsGenericType;
        }

        /// <summary>
        /// Determines whether the specified type is generic collection.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>true if the specified type is generic collection; otherwise, false.</returns>
        public static bool IsTypeGenericCollectionInterface(Type type)
        {
            if (!IsTypeGeneric(type))
                return false;
            var genericTypeDefinition = type.GetGenericTypeDefinition();
            if (genericTypeDefinition != typeof(IList<>) && genericTypeDefinition != typeof(ICollection<>))
                return genericTypeDefinition == typeof(IEnumerable<>);
            return true;
        }

        /// <summary>Determines whether the specified type is value type.</summary>
        /// <param name="type">The type.</param>
        /// <returns>true if the specified type is value type; otherwise, false.</returns>
        public static bool IsValueType(Type type)
        {
            return type.IsValueType;
        }

        /// <summary>Converts to nullable.</summary>
        /// <param name="obj">The object.</param>
        /// <param name="nullableType">Type of the nullable.</param>
        /// <returns>The System.Object.</returns>
        public static object ToNullableType(object obj, Type nullableType)
        {
            if (obj != null)
                return Convert.ChangeType(obj, Nullable.GetUnderlyingType(nullableType), CultureInfo.InvariantCulture);
            return null;
        }

        /// <summary>
        /// Creates an instance of IList by the specified type which inherit IEnumerable interface.
        /// </summary>
        /// <param name="source">Source Type which inherit IEnumerable interface.</param>
        /// <param name="lengths">An array of 32-bit integers that represent the size of each dimension of the list to create.</param>
        /// <returns>A reference to the newly created IList object.</returns>
        private static IList CreateIList(Type source, params object[] lengths)
        {
            if (!IsEnumerable(source) || IsDictionary(source))
                return null;
            if (source.IsArray)
            {
                if (IsNullOrEmpty(lengths))
                    return Array.CreateInstance(source.GetElementType(), 0);
                var numArray = new long[lengths.Length];
                for (var index = 0; index < lengths.Length; ++index)
                {
                    var length = lengths[index];
                    try
                    {
                        numArray[index] = (long) length;
                    }
                    catch
                    {
                        numArray[index] = (int) length;
                    }
                }
                return Array.CreateInstance(source.GetElementType(), numArray);
            }
            if (IsNullOrEmpty(lengths))
                return (IList) Activator.CreateInstance(source);
            return (IList) Activator.CreateInstance(source, lengths[0]);
        }

        /// <summary>Check Type inherit IDictionary interface or not.</summary>
        /// <param name="source">Source Type.</param>
        /// <returns>true if the source Type inherit IDictionary interface; otherwise, false.</returns>
        private static bool IsDictionary(Type source)
        {
            return source.GetInterface("IDictionary") != null;
        }

        /// <summary>Check Type inherit IEnumerable interface or not.</summary>
        /// <param name="source">Source Type.</param>
        /// <returns>true if the source Type inherit IEnumerable interface; otherwise, false.</returns>
        private static bool IsEnumerable(Type source)
        {
            if (source != typeof(string))
                return source.GetInterface("IEnumerable") != null;
            return false;
        }

        /// <summary>Determines whether a sequence is null or empty.</summary>
        /// <param name="source">Source IEnumerable.</param>
        /// <returns>true if the source sequence is empty; otherwise, false.</returns>
        private static bool IsNullOrEmpty(Array source)
        {
            if (source != null)
                return source.Length == 0;
            return true;
        }

        /// <summary>Delegate ConstructorDelegate</summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The System.Object.</returns>
        public delegate object ConstructorDelegate(params object[] args);

        /// <summary>Delegate GetDelegate</summary>
        /// <param name="source">The source.</param>
        /// <returns>The System.Object.</returns>
        public delegate object GetDelegate(object source);

        /// <summary>Delegate SetDelegate</summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The value.</param>
        public delegate void SetDelegate(object source, object value);

        /// <summary>Delegate ThreadSafeDictionaryValueFactory</summary>
        /// <typeparam name="TKey">The type of the t key.</typeparam>
        /// <typeparam name="TValue">The type of the t value.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        public delegate TValue ThreadSafeDictionaryValueFactory<TKey, TValue>(TKey key);

        /// <summary>
        /// Class ThreadSafeDictionary. This class cannot be inherited.
        /// </summary>
        /// <typeparam name="TKey">The type of the t key.</typeparam>
        /// <typeparam name="TValue">The type of the t value.</typeparam>
        public sealed class ThreadSafeDictionary<TKey, TValue> : IDictionary<TKey, TValue>,
            ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
        {
            /// <summary>Field _syncRoot.</summary>
            private readonly object _syncRoot = new object();

            /// <summary>Field _valueFactory.</summary>
            private readonly ThreadSafeDictionaryValueFactory<TKey, TValue> _valueFactory;

            /// <summary>Field _dictionary.</summary>
            private Dictionary<TKey, TValue> _dictionary;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:HSNXT.ReflectionUtilities.ThreadSafeDictionary`2" /> class.
            /// </summary>
            /// <param name="valueFactory">The value factory.</param>
            public ThreadSafeDictionary(ThreadSafeDictionaryValueFactory<TKey, TValue> valueFactory)
            {
                this._valueFactory = valueFactory;
            }

            /// <summary>
            /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
            /// </summary>
            /// <value>The count.</value>
            public int Count => this._dictionary.Count;

            /// <summary>
            /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
            /// </summary>
            /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
            public bool IsReadOnly => throw new NotSupportedException();

            /// <summary>
            /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2" />.
            /// </summary>
            /// <value>The keys.</value>
            public ICollection<TKey> Keys => this._dictionary.Keys;

            /// <summary>
            /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2" />.
            /// </summary>
            /// <value>The values.</value>
            public ICollection<TValue> Values => this._dictionary.Values;

            /// <summary>Gets or sets the element with the specified key.</summary>
            /// <param name="key">The key.</param>
            /// <returns>The Value.</returns>
            public TValue this[TKey key]
            {
                get => this.Get(key);
                set => throw new NotSupportedException();
            }

            /// <summary>
            /// Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2" />.
            /// </summary>
            /// <param name="key">The object to use as the key of the element to add.</param>
            /// <param name="value">The object to use as the value of the element to add.</param>
            public void Add(TKey key, TValue value)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
            /// </summary>
            /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            public void Add(KeyValuePair<TKey, TValue> item)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
            /// </summary>
            public void Clear()
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
            /// </summary>
            /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            /// <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
            public bool Contains(KeyValuePair<TKey, TValue> item)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key.
            /// </summary>
            /// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2" />.</param>
            /// <returns>true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the key; otherwise, false.</returns>
            public bool ContainsKey(TKey key)
            {
                return this._dictionary.ContainsKey(key);
            }

            /// <summary>Copies to.</summary>
            /// <param name="array">The array.</param>
            /// <param name="arrayIndex">Index of the array.</param>
            public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>An enumerator that can be used to iterate through the collection.</returns>
            public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            {
                return this._dictionary.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this._dictionary.GetEnumerator();
            }

            /// <summary>
            /// Removes the element with the specified key from the <see cref="T:System.Collections.Generic.IDictionary`2" />.
            /// </summary>
            /// <param name="key">The key of the element to remove.</param>
            /// <returns>true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
            public bool Remove(TKey key)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
            /// </summary>
            /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            /// <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            public bool Remove(KeyValuePair<TKey, TValue> item)
            {
                throw new NotSupportedException();
            }

            /// <summary>Gets the value associated with the specified key.</summary>
            /// <param name="key">The key whose value to get.</param>
            /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
            /// <returns>true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
            public bool TryGetValue(TKey key, out TValue value)
            {
                value = this[key];
                return true;
            }

            /// <summary>Adds the value.</summary>
            /// <param name="key">The key.</param>
            /// <returns>The value.</returns>
            private TValue AddValue(TKey key)
            {
                var obj1 = this._valueFactory(key);
                lock (this._syncRoot)
                {
                    if (this._dictionary == null)
                    {
                        this._dictionary = new Dictionary<TKey, TValue>();
                        this._dictionary[key] = obj1;
                    }
                    else
                    {
                        TValue obj2;
                        if (this._dictionary.TryGetValue(key, out obj2))
                            return obj2;
                        var dictionary = new Dictionary<TKey, TValue>(this._dictionary);
                        dictionary[key] = obj1;
                        this._dictionary = dictionary;
                    }
                }
                return obj1;
            }

            /// <summary>Gets the specified key.</summary>
            /// <param name="key">The key.</param>
            /// <returns>The Value.</returns>
            private TValue Get(TKey key)
            {
                TValue obj;
                if (this._dictionary == null || !this._dictionary.TryGetValue(key, out obj))
                    return this.AddValue(key);
                return obj;
            }
        }
    }
}