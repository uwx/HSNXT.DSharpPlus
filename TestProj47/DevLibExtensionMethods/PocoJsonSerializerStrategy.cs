// Decompiled with JetBrains decompiler
// Type: TestProj47.PocoJsonSerializerStrategy
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace HSNXT
{
    /// <summary>Class PocoJsonSerializerStrategy.</summary>
    internal class PocoJsonSerializerStrategy
    {
        /// <summary>Field ArrayConstructorParameterTypes.</summary>
        internal static readonly Type[] ArrayConstructorParameterTypes = new Type[]
        {
            typeof(int)
        };

        /// <summary>Field EmptyTypes.</summary>
        internal static readonly Type[] EmptyTypes = new Type[0];

        /// <summary>Field Iso8601Format.</summary>
        private static readonly string[] Iso8601Format = new string[]
        {
            "yyyy-MM-dd\\THH:mm:ss.FFFFFFF\\Z",
            "yyyy-MM-dd\\THH:mm:ss\\Z",
            "yyyy-MM-dd\\THH:mm:ssK"
        };

        /// <summary>Field ConstructorCache.</summary>
        private readonly IDictionary<Type, ReflectionUtilities.ConstructorDelegate> _constructorCache;

        /// <summary>Field GetCache.</summary>
        private readonly IDictionary<Type, IDictionary<string, ReflectionUtilities.GetDelegate>> _getCache;

        /// <summary>Field SetCache.</summary>
        private readonly IDictionary<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtilities.SetDelegate>>>
            _setCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:HSNXT.PocoJsonSerializerStrategy" /> class.
        /// </summary>
        public PocoJsonSerializerStrategy()
        {
            this._constructorCache =
                new ReflectionUtilities.ThreadSafeDictionary<Type, ReflectionUtilities.ConstructorDelegate>(
                    this.ConstructorDelegateFactory);
            this._getCache =
                new ReflectionUtilities.ThreadSafeDictionary<Type, IDictionary<string, ReflectionUtilities.GetDelegate>
                >(this.GetterValueFactory);
            this._setCache =
                new ReflectionUtilities.ThreadSafeDictionary<Type,
                    IDictionary<string, KeyValuePair<Type, ReflectionUtilities.SetDelegate>>>(this.SetterValueFactory);
        }

        /// <summary>Deserializes the object.</summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        /// <returns>The System.Object.</returns>
        public virtual object DeserializeObject(object value, Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (type.IsEnum)
            {
                return value == null ? ((IList) Enum.GetValues(type))[0] : Enum.ToObject(type, value);
            }
            var str = value as string;
            if (type == typeof(Guid) && string.IsNullOrEmpty(str))
                return new Guid();
            if (value == null)
                return null;
            object source = null;
            if (str != null)
            {
                if (str.Length != 0)
                {
                    if (type == typeof(DateTime) || ReflectionUtilities.IsNullableType(type) &&
                        Nullable.GetUnderlyingType(type) == typeof(DateTime))
                        return DateTime.ParseExact(str, Iso8601Format, CultureInfo.InvariantCulture,
                            DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
                    if (type == typeof(DateTimeOffset) || ReflectionUtilities.IsNullableType(type) &&
                        Nullable.GetUnderlyingType(type) == typeof(DateTimeOffset))
                        return DateTimeOffset.ParseExact(str, Iso8601Format, CultureInfo.InvariantCulture,
                            DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
                    if (type == typeof(Guid) || ReflectionUtilities.IsNullableType(type) &&
                        Nullable.GetUnderlyingType(type) == typeof(Guid))
                        return new Guid(str);
                    if (type == typeof(Uri))
                    {
                        Uri result;
                        if (Uri.IsWellFormedUriString(str, UriKind.RelativeOrAbsolute) &&
                            Uri.TryCreate(str, UriKind.RelativeOrAbsolute, out result))
                            return result;
                        return null;
                    }
                    if (type == typeof(string))
                        return str;
                    return Convert.ChangeType(str, type, CultureInfo.InvariantCulture);
                }
                source = type != typeof(Guid)
                    ? (!ReflectionUtilities.IsNullableType(type) || Nullable.GetUnderlyingType(type) != typeof(Guid)
                        ? str
                        : null)
                    : (object) new Guid();
                if (!ReflectionUtilities.IsNullableType(type) && Nullable.GetUnderlyingType(type) == typeof(Guid))
                    return str;
            }
            else if (value is bool)
                return value;
            var flag1 = value is long;
            var flag2 = value is double;
            if (flag1 && type == typeof(long) || flag2 && type == typeof(double))
                return value;
            if (flag2 && type != typeof(double) || flag1 && type != typeof(long))
            {
                var obj = type == typeof(int) || type == typeof(long) || type == typeof(double) ||
                          type == typeof(float) || (type == typeof(bool) || type == typeof(Decimal) ||
                                                    (type == typeof(byte) || type == typeof(short)))
                    ? Convert.ChangeType(value, type, CultureInfo.InvariantCulture)
                    : value;
                if (ReflectionUtilities.IsNullableType(type))
                    return ReflectionUtilities.ToNullableType(obj, type);
                return obj;
            }
            var dictionary1 = value as IDictionary<string, object>;
            if (dictionary1 != null)
            {
                var dictionary2 = dictionary1;
                if (ReflectionUtilities.IsTypeDictionary(type))
                {
                    var genericTypeArguments = ReflectionUtilities.GetGenericTypeArguments(type);
                    var type1 = genericTypeArguments[0];
                    var type2 = genericTypeArguments[1];
                    var dictionary3 =
                        (IDictionary) this._constructorCache[typeof(Dictionary<,>).MakeGenericType(type1, type2)]();
                    foreach (var keyValuePair in dictionary2)
                        dictionary3.Add(keyValuePair.Key, this.DeserializeObject(keyValuePair.Value, type2));
                    source = dictionary3;
                }
                else if (type == typeof(object))
                {
                    source = value;
                }
                else
                {
                    source = this._constructorCache[type]();
                    foreach (var keyValuePair in this._setCache[type])
                    {
                        object obj;
                        if (dictionary2.TryGetValue(keyValuePair.Key, out obj))
                        {
                            obj = this.DeserializeObject(obj, keyValuePair.Value.Key);
                            keyValuePair.Value.Value(source, obj);
                        }
                    }
                }
            }
            else
            {
                var objectList1 = value as IList<object>;
                if (objectList1 != null)
                {
                    var objectList2 = objectList1;
                    IList list = null;
                    if (type.IsArray)
                    {
                        list = (IList) this._constructorCache[type]((object) objectList2.Count);
                        var num = 0;
                        foreach (var obj in objectList2)
                            list[num++] = this.DeserializeObject(obj, type.GetElementType());
                    }
                    else if (ReflectionUtilities.IsTypeGenericCollectionInterface(type) ||
                             ReflectionUtilities.IsAssignableFrom(typeof(IList), type))
                    {
                        var genericListElementType = ReflectionUtilities.GetGenericListElementType(type);
                        var constructorDelegate = this._constructorCache[type];
                        if (constructorDelegate == null)
                            constructorDelegate =
                                this._constructorCache[typeof(List<>).MakeGenericType(genericListElementType)];
                        var objArray = new object[]
                        {
                            objectList2.Count
                        };
                        list = (IList) constructorDelegate(objArray);
                        foreach (var obj in objectList2)
                            list.Add(this.DeserializeObject(obj, genericListElementType));
                    }
                    source = list;
                }
            }
            return source;
        }

        /// <summary>Try to serialize non primitive object.</summary>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        /// <returns>true if serialize succeeded; otherwise, false.</returns>
        public virtual bool TrySerializeNonPrimitiveObject(object input, out object output)
        {
            if (!this.TrySerializeKnownTypes(input, out output))
                return this.TrySerializeUnknownTypes(input, out output);
            return true;
        }

        /// <summary>Constructor delegate.</summary>
        /// <param name="key">The key.</param>
        /// <returns>The delegate.</returns>
        internal virtual ReflectionUtilities.ConstructorDelegate ConstructorDelegateFactory(Type key)
        {
            return ReflectionUtilities.GetConstructor(key, key.IsArray ? ArrayConstructorParameterTypes : EmptyTypes);
        }

        /// <summary>Getters the value factory.</summary>
        /// <param name="type">The type.</param>
        /// <returns>The delegate.</returns>
        internal virtual IDictionary<string, ReflectionUtilities.GetDelegate> GetterValueFactory(Type type)
        {
            IDictionary<string, ReflectionUtilities.GetDelegate> dictionary =
                new Dictionary<string, ReflectionUtilities.GetDelegate>();
            foreach (var property in ReflectionUtilities.GetProperties(type))
            {
                if (property.CanRead)
                {
                    var getterMethodInfo = ReflectionUtilities.GetGetterMethodInfo(property);
                    if (!getterMethodInfo.IsStatic && getterMethodInfo.IsPublic)
                        dictionary[this.MapClrMemberNameToJsonFieldName(property.Name)] =
                            ReflectionUtilities.GetGetMethod(property);
                }
            }
            foreach (var field in ReflectionUtilities.GetFields(type))
            {
                if (!field.IsStatic && field.IsPublic)
                    dictionary[this.MapClrMemberNameToJsonFieldName(field.Name)] =
                        ReflectionUtilities.GetGetMethod(field);
            }
            return dictionary;
        }

        /// <summary>Setters the value factory.</summary>
        /// <param name="type">The type.</param>
        /// <returns>The delegate.</returns>
        internal virtual IDictionary<string, KeyValuePair<Type, ReflectionUtilities.SetDelegate>>
            SetterValueFactory(Type type)
        {
            IDictionary<string, KeyValuePair<Type, ReflectionUtilities.SetDelegate>> dictionary =
                new Dictionary<string, KeyValuePair<Type, ReflectionUtilities.SetDelegate>>();
            foreach (var property in ReflectionUtilities.GetProperties(type))
            {
                if (property.CanWrite)
                {
                    var setterMethodInfo = ReflectionUtilities.GetSetterMethodInfo(property);
                    if (!setterMethodInfo.IsStatic && setterMethodInfo.IsPublic)
                        dictionary[this.MapClrMemberNameToJsonFieldName(property.Name)] =
                            new KeyValuePair<Type, ReflectionUtilities.SetDelegate>(property.PropertyType,
                                ReflectionUtilities.GetSetMethod(property));
                }
            }
            foreach (var field in ReflectionUtilities.GetFields(type))
            {
                if (!field.IsInitOnly && !field.IsStatic && field.IsPublic)
                    dictionary[this.MapClrMemberNameToJsonFieldName(field.Name)] =
                        new KeyValuePair<Type, ReflectionUtilities.SetDelegate>(field.FieldType,
                            ReflectionUtilities.GetSetMethod(field));
            }
            return dictionary;
        }

        /// <summary>Maps the name of the color member name to json field.</summary>
        /// <param name="clrPropertyName">The name of the property.</param>
        /// <returns>The System.String.</returns>
        protected virtual string MapClrMemberNameToJsonFieldName(string clrPropertyName)
        {
            return clrPropertyName;
        }

        /// <summary>Serializes the enum.</summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>The System.Object.</returns>
        protected virtual object SerializeEnum(Enum enumValue)
        {
            return Convert.ToDouble(enumValue, CultureInfo.InvariantCulture);
        }

        /// <summary>Tries the serialize known types.</summary>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        /// <returns>true if serialize succeeded; otherwise, false.</returns>
        protected virtual bool TrySerializeKnownTypes(object input, out object output)
        {
            var flag = true;
            if (input is DateTime)
                output = ((DateTime) input).ToUniversalTime().ToString(Iso8601Format[0], CultureInfo.InvariantCulture);
            else if (input is DateTimeOffset)
                output = ((DateTimeOffset) input).ToUniversalTime()
                    .ToString(Iso8601Format[0], CultureInfo.InvariantCulture);
            else if (input is Guid)
                output = ((Guid) input).ToString("D");
            else if ((object) (input as Uri) != null)
            {
                output = input.ToString();
            }
            else
            {
                var enumValue = input as Enum;
                if (enumValue != null)
                {
                    output = this.SerializeEnum(enumValue);
                }
                else
                {
                    flag = false;
                    output = null;
                }
            }
            return flag;
        }

        /// <summary>Tries the serialize unknown types.</summary>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        /// <returns>true if serialize succeeded; otherwise, false.</returns>
        protected virtual bool TrySerializeUnknownTypes(object input, out object output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            output = null;
            var type = input.GetType();
            if (type.FullName == null)
                return false;
            IDictionary<string, object> dictionary = new JsonObject(StringComparer.OrdinalIgnoreCase);
            foreach (var keyValuePair in this._getCache[type])
            {
                if (keyValuePair.Value != null)
                    dictionary.Add(this.MapClrMemberNameToJsonFieldName(keyValuePair.Key), keyValuePair.Value(input));
            }
            output = dictionary;
            return true;
        }
    }
}