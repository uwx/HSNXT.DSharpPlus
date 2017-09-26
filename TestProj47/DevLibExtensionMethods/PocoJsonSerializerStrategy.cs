// Decompiled with JetBrains decompiler
// Type: DevLib.ExtensionMethods.PocoJsonSerializerStrategy
// Assembly: DevLib.ExtensionMethods, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\DevLib.ExtensionMethods.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace DevLib.ExtensionMethods
{
  /// <summary>Class PocoJsonSerializerStrategy.</summary>
  internal class PocoJsonSerializerStrategy
  {
    /// <summary>Field ArrayConstructorParameterTypes.</summary>
    internal static readonly Type[] ArrayConstructorParameterTypes = new Type[1]
    {
      typeof (int)
    };
    /// <summary>Field EmptyTypes.</summary>
    internal static readonly Type[] EmptyTypes = new Type[0];
    /// <summary>Field Iso8601Format.</summary>
    private static readonly string[] Iso8601Format = new string[3]
    {
      "yyyy-MM-dd\\THH:mm:ss.FFFFFFF\\Z",
      "yyyy-MM-dd\\THH:mm:ss\\Z",
      "yyyy-MM-dd\\THH:mm:ssK"
    };
    /// <summary>Field ConstructorCache.</summary>
    private IDictionary<Type, ReflectionUtilities.ConstructorDelegate> ConstructorCache;
    /// <summary>Field GetCache.</summary>
    private IDictionary<Type, IDictionary<string, ReflectionUtilities.GetDelegate>> GetCache;
    /// <summary>Field SetCache.</summary>
    private IDictionary<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtilities.SetDelegate>>> SetCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:DevLib.ExtensionMethods.PocoJsonSerializerStrategy" /> class.
    /// </summary>
    public PocoJsonSerializerStrategy()
    {
      this.ConstructorCache = (IDictionary<Type, ReflectionUtilities.ConstructorDelegate>) new ReflectionUtilities.ThreadSafeDictionary<Type, ReflectionUtilities.ConstructorDelegate>(new ReflectionUtilities.ThreadSafeDictionaryValueFactory<Type, ReflectionUtilities.ConstructorDelegate>(this.ConstructorDelegateFactory));
      this.GetCache = (IDictionary<Type, IDictionary<string, ReflectionUtilities.GetDelegate>>) new ReflectionUtilities.ThreadSafeDictionary<Type, IDictionary<string, ReflectionUtilities.GetDelegate>>(new ReflectionUtilities.ThreadSafeDictionaryValueFactory<Type, IDictionary<string, ReflectionUtilities.GetDelegate>>(this.GetterValueFactory));
      this.SetCache = (IDictionary<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtilities.SetDelegate>>>) new ReflectionUtilities.ThreadSafeDictionary<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtilities.SetDelegate>>>(new ReflectionUtilities.ThreadSafeDictionaryValueFactory<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtilities.SetDelegate>>>(this.SetterValueFactory));
    }

    /// <summary>Deserializes the object.</summary>
    /// <param name="value">The value.</param>
    /// <param name="type">The type.</param>
    /// <returns>The System.Object.</returns>
    public virtual object DeserializeObject(object value, Type type)
    {
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      if (type.IsEnum)
      {
        if (value == null)
          return ((IList) Enum.GetValues(type))[0];
        return Enum.ToObject(type, value);
      }
      string str = value as string;
      if (type == typeof (Guid) && string.IsNullOrEmpty(str))
        return (object) new Guid();
      if (value == null)
        return (object) null;
      object source = (object) null;
      if (str != null)
      {
        if (str.Length != 0)
        {
          if (type == typeof (DateTime) || ReflectionUtilities.IsNullableType(type) && Nullable.GetUnderlyingType(type) == typeof (DateTime))
            return (object) DateTime.ParseExact(str, PocoJsonSerializerStrategy.Iso8601Format, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
          if (type == typeof (DateTimeOffset) || ReflectionUtilities.IsNullableType(type) && Nullable.GetUnderlyingType(type) == typeof (DateTimeOffset))
            return (object) DateTimeOffset.ParseExact(str, PocoJsonSerializerStrategy.Iso8601Format, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
          if (type == typeof (Guid) || ReflectionUtilities.IsNullableType(type) && Nullable.GetUnderlyingType(type) == typeof (Guid))
            return (object) new Guid(str);
          if (type == typeof (Uri))
          {
            Uri result;
            if (Uri.IsWellFormedUriString(str, UriKind.RelativeOrAbsolute) && Uri.TryCreate(str, UriKind.RelativeOrAbsolute, out result))
              return (object) result;
            return (object) null;
          }
          if (type == typeof (string))
            return (object) str;
          return Convert.ChangeType((object) str, type, (IFormatProvider) CultureInfo.InvariantCulture);
        }
        source = type != typeof (Guid) ? (!ReflectionUtilities.IsNullableType(type) || Nullable.GetUnderlyingType(type) != typeof (Guid) ? (object) str : (object) null) : (object) new Guid();
        if (!ReflectionUtilities.IsNullableType(type) && Nullable.GetUnderlyingType(type) == typeof (Guid))
          return (object) str;
      }
      else if (value is bool)
        return value;
      bool flag1 = value is long;
      bool flag2 = value is double;
      if (flag1 && type == typeof (long) || flag2 && type == typeof (double))
        return value;
      if (flag2 && type != typeof (double) || flag1 && type != typeof (long))
      {
        object obj = type == typeof (int) || type == typeof (long) || (type == typeof (double) || type == typeof (float)) || (type == typeof (bool) || type == typeof (Decimal) || (type == typeof (byte) || type == typeof (short))) ? Convert.ChangeType(value, type, (IFormatProvider) CultureInfo.InvariantCulture) : value;
        if (ReflectionUtilities.IsNullableType(type))
          return ReflectionUtilities.ToNullableType(obj, type);
        return obj;
      }
      IDictionary<string, object> dictionary1 = value as IDictionary<string, object>;
      if (dictionary1 != null)
      {
        IDictionary<string, object> dictionary2 = dictionary1;
        if (ReflectionUtilities.IsTypeDictionary(type))
        {
          Type[] genericTypeArguments = ReflectionUtilities.GetGenericTypeArguments(type);
          Type type1 = genericTypeArguments[0];
          Type type2 = genericTypeArguments[1];
          IDictionary dictionary3 = (IDictionary) this.ConstructorCache[typeof (Dictionary<,>).MakeGenericType(type1, type2)](new object[0]);
          foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) dictionary2)
            dictionary3.Add((object) keyValuePair.Key, this.DeserializeObject(keyValuePair.Value, type2));
          source = (object) dictionary3;
        }
        else if (type == typeof (object))
        {
          source = value;
        }
        else
        {
          source = this.ConstructorCache[type](new object[0]);
          foreach (KeyValuePair<string, KeyValuePair<Type, ReflectionUtilities.SetDelegate>> keyValuePair in (IEnumerable<KeyValuePair<string, KeyValuePair<Type, ReflectionUtilities.SetDelegate>>>) this.SetCache[type])
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
        IList<object> objectList1 = value as IList<object>;
        if (objectList1 != null)
        {
          IList<object> objectList2 = objectList1;
          IList list = (IList) null;
          if (type.IsArray)
          {
            list = (IList) this.ConstructorCache[type](new object[1]
            {
              (object) objectList2.Count
            });
            int num = 0;
            foreach (object obj in (IEnumerable<object>) objectList2)
              list[num++] = this.DeserializeObject(obj, type.GetElementType());
          }
          else if (ReflectionUtilities.IsTypeGenericCollectionInterface(type) || ReflectionUtilities.IsAssignableFrom(typeof (IList), type))
          {
            Type genericListElementType = ReflectionUtilities.GetGenericListElementType(type);
            ReflectionUtilities.ConstructorDelegate constructorDelegate = this.ConstructorCache[type];
            if (constructorDelegate == null)
              constructorDelegate = this.ConstructorCache[typeof (List<>).MakeGenericType(genericListElementType)];
            object[] objArray = new object[1]
            {
              (object) objectList2.Count
            };
            list = (IList) constructorDelegate(objArray);
            foreach (object obj in (IEnumerable<object>) objectList2)
              list.Add(this.DeserializeObject(obj, genericListElementType));
          }
          source = (object) list;
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
      return ReflectionUtilities.GetConstructor(key, key.IsArray ? PocoJsonSerializerStrategy.ArrayConstructorParameterTypes : PocoJsonSerializerStrategy.EmptyTypes);
    }

    /// <summary>Getters the value factory.</summary>
    /// <param name="type">The type.</param>
    /// <returns>The delegate.</returns>
    internal virtual IDictionary<string, ReflectionUtilities.GetDelegate> GetterValueFactory(Type type)
    {
      IDictionary<string, ReflectionUtilities.GetDelegate> dictionary = (IDictionary<string, ReflectionUtilities.GetDelegate>) new Dictionary<string, ReflectionUtilities.GetDelegate>();
      foreach (PropertyInfo property in ReflectionUtilities.GetProperties(type))
      {
        if (property.CanRead)
        {
          MethodInfo getterMethodInfo = ReflectionUtilities.GetGetterMethodInfo(property);
          if (!getterMethodInfo.IsStatic && getterMethodInfo.IsPublic)
            dictionary[this.MapClrMemberNameToJsonFieldName(property.Name)] = ReflectionUtilities.GetGetMethod(property);
        }
      }
      foreach (FieldInfo field in ReflectionUtilities.GetFields(type))
      {
        if (!field.IsStatic && field.IsPublic)
          dictionary[this.MapClrMemberNameToJsonFieldName(field.Name)] = ReflectionUtilities.GetGetMethod(field);
      }
      return dictionary;
    }

    /// <summary>Setters the value factory.</summary>
    /// <param name="type">The type.</param>
    /// <returns>The delegate.</returns>
    internal virtual IDictionary<string, KeyValuePair<Type, ReflectionUtilities.SetDelegate>> SetterValueFactory(Type type)
    {
      IDictionary<string, KeyValuePair<Type, ReflectionUtilities.SetDelegate>> dictionary = (IDictionary<string, KeyValuePair<Type, ReflectionUtilities.SetDelegate>>) new Dictionary<string, KeyValuePair<Type, ReflectionUtilities.SetDelegate>>();
      foreach (PropertyInfo property in ReflectionUtilities.GetProperties(type))
      {
        if (property.CanWrite)
        {
          MethodInfo setterMethodInfo = ReflectionUtilities.GetSetterMethodInfo(property);
          if (!setterMethodInfo.IsStatic && setterMethodInfo.IsPublic)
            dictionary[this.MapClrMemberNameToJsonFieldName(property.Name)] = new KeyValuePair<Type, ReflectionUtilities.SetDelegate>(property.PropertyType, ReflectionUtilities.GetSetMethod(property));
        }
      }
      foreach (FieldInfo field in ReflectionUtilities.GetFields(type))
      {
        if (!field.IsInitOnly && !field.IsStatic && field.IsPublic)
          dictionary[this.MapClrMemberNameToJsonFieldName(field.Name)] = new KeyValuePair<Type, ReflectionUtilities.SetDelegate>(field.FieldType, ReflectionUtilities.GetSetMethod(field));
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
      return (object) Convert.ToDouble((object) enumValue, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>Tries the serialize known types.</summary>
    /// <param name="input">The input.</param>
    /// <param name="output">The output.</param>
    /// <returns>true if serialize succeeded; otherwise, false.</returns>
    protected virtual bool TrySerializeKnownTypes(object input, out object output)
    {
      bool flag = true;
      if (input is DateTime)
        output = (object) ((DateTime) input).ToUniversalTime().ToString(PocoJsonSerializerStrategy.Iso8601Format[0], (IFormatProvider) CultureInfo.InvariantCulture);
      else if (input is DateTimeOffset)
        output = (object) ((DateTimeOffset) input).ToUniversalTime().ToString(PocoJsonSerializerStrategy.Iso8601Format[0], (IFormatProvider) CultureInfo.InvariantCulture);
      else if (input is Guid)
        output = (object) ((Guid) input).ToString("D");
      else if ((object) (input as Uri) != null)
      {
        output = (object) input.ToString();
      }
      else
      {
        Enum enumValue = input as Enum;
        if (enumValue != null)
        {
          output = this.SerializeEnum(enumValue);
        }
        else
        {
          flag = false;
          output = (object) null;
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
        throw new ArgumentNullException(nameof (input));
      output = (object) null;
      Type type = input.GetType();
      if (type.FullName == null)
        return false;
      IDictionary<string, object> dictionary = (IDictionary<string, object>) new JsonObject((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach (KeyValuePair<string, ReflectionUtilities.GetDelegate> keyValuePair in (IEnumerable<KeyValuePair<string, ReflectionUtilities.GetDelegate>>) this.GetCache[type])
      {
        if (keyValuePair.Value != null)
          dictionary.Add(this.MapClrMemberNameToJsonFieldName(keyValuePair.Key), keyValuePair.Value(input));
      }
      output = (object) dictionary;
      return true;
    }
  }
}
