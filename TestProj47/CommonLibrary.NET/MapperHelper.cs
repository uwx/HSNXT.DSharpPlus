﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using HSNXT.ComLib.Reflection;

namespace HSNXT.ComLib.MapperSupport
{
    /// <summary>
    /// Helper mapper methods.
    /// </summary>
    public class MapperHelper
    {
        private static readonly IDictionary<Type, Func<string, object>> _propertyTypeMappers = new Dictionary<Type, Func<string, object>>();


        static MapperHelper()
        {
            _propertyTypeMappers[typeof(bool)] = Extensions.ToBoolObject; 
            _propertyTypeMappers[typeof(int)] = Extensions.ToIntObject;
            _propertyTypeMappers[typeof(long)] = Extensions.ToLongObject;
            _propertyTypeMappers[typeof(float)] = Extensions.ToFloatObject;
            _propertyTypeMappers[typeof(double)] = Extensions.ToDoubleObject;
            _propertyTypeMappers[typeof(TimeSpan)] = Extensions.ToTimeObject;
            _propertyTypeMappers[typeof(DateTime)] = Extensions.ToDateTimeObject;            
        }    
    

        /// <summary>
        /// Map the properties in the data to the properties of the item T using the propMap supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The object to set the properties on.</param>
        /// <param name="counterOrRefId">An counter to help associate errors in mapping w/ a specific item index or reference id.</param>
        /// <param name="propMap">Property map containing the names of the properties that can be mapped.</param>
        /// <param name="data">The source of the data to map.</param>
        public static void MapTo<T>(T item, int counterOrRefId, IDictionary<string, PropertyInfo> propMap, IDictionary data)
        {
            MapTo(item, counterOrRefId, propMap, data, null);
        }


        /// <summary>
        /// Map the properties in the data to the properties of the item T using the propMap supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="counterOrRefId">An counter to help associate errors in mapping w/ a specific item index or reference id.</param>
        /// <param name="item">The object to set the properties on.</param>
        /// <param name="propMap">Property map containing the names of the properties that can be mapped.</param>
        /// <param name="data">The source of the data to map.</param>
        /// <param name="errors">Error collection.</param>
        public static void MapTo<T>(T item, int counterOrRefId, IDictionary<string, PropertyInfo> propMap, IDictionary data, IErrors errors)
        {
            var handledProps = new Dictionary<string, bool>();
            foreach (DictionaryEntry entry in data)
            {
                var propname = entry.Key as string;
                var val = entry.Value;
                PropertyInfo prop = null;
                if (propMap.ContainsKey(propname))
                {
                    prop = propMap[propname];
                    SetProperty(prop, item, counterOrRefId, errors, val);
                }
                else if (propMap.ContainsKey(propname.ToLower().Trim()))
                {
                    prop = propMap[propname.Trim().ToLower()];
                    SetProperty(prop, item, counterOrRefId, errors, val);
                }
                else if (propname.Contains("."))
                {
                    var objectname = propname.Substring(0, propname.IndexOf("."));
                    if (!handledProps.ContainsKey(objectname))
                    {
                        if (propMap.ContainsKey(objectname.ToLower()))
                        {
                            prop = propMap[objectname.ToLower().Trim()];
                            // Composite object.
                            var obj = Activator.CreateInstance(prop.PropertyType);
                            prop.SetValue(item, obj, null);
                            MapTo(obj, data, objectname, errors);
                            handledProps[objectname] = true;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Maps all the keys/values in the data dictionary to the 
        /// </summary>
        /// <param name="obj">The object to map</param>
        /// <param name="data">The data to map to the object.</param>
        /// <param name="namefilter">Filter on the keys. e.g. "Location." will only map keys that contain "Location."</param>
        /// <param name="errors">Error list for collecting errors.</param>
        public static void MapTo(object obj, IDictionary data, string namefilter, IErrors errors)
        {
            // 1. Get all the public, instance, writable properties.
            var type = obj.GetType();
            var propMap = ReflectionUtils.GetPropertiesAsMap(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, false);

            // 2. Making keys/filter case insensitive. Location. = location.
            namefilter = namefilter.Trim().ToLower();

            // 3. Iterate through all the keys.
            foreach (DictionaryEntry entry in data)
            {
                var keyname = entry.Key as string;
                keyname = keyname.Trim().ToLower();
                PropertyInfo prop = null;

                // 4. key in data matches filter?
                if (keyname.Contains(namefilter))
                {
                    // Get "City" from "Location.City";
                    var propname = keyname.Substring(keyname.IndexOf(".") + 1);
                    propname.Trim().ToLower();

                    // 5. propname exists in the data type?
                    if (propMap.ContainsKey(propname))
                    {
                        // 6. Finally map 
                        prop = propMap[propname];
                        var val = entry.Value;
                        var valConverted = ReflectionTypeChecker.ConvertToSameType(prop, val);
                        prop.SetValue(obj, valConverted, null);
                    }
                }
            }
        }


        /// <summary>
        /// Maps prop/values from the object and passes the column/propertyname and it's value as an object back to the callback.
        /// The property name is delimited by "." for nested objects.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="excludeProps"></param>
        /// <param name="propMap">KeyValue list of propnames and their propinfo. NOTE: nested objects should have their key as "obj.property.property"</param>
        /// <param name="callback"></param>
        public static void MapFrom(object obj, IDictionary<string, string> excludeProps, IList<KeyValuePair<string, PropertyInfo>> propMap, Action<string, object> callback)
        {
            if (propMap == null || propMap.Count == 0)
                return;

            for (var ndx = 0; ndx < propMap.Count; ndx++)
            {
                var property = propMap[ndx];
                var propName = property.Key;
                                        
                if (excludeProps != null && !excludeProps.ContainsKey(propName))
                {
                    // Top level property.
                    if (!property.Key.Contains("."))
                    {
                        var val = property.Value.GetValue(obj, null);
                        callback(propName, val);
                    }
                    // Only process properties that begin with the filter e.g. "Address.".
                    else
                    {
                        var pair = GetProperty(obj, propName);
                        if (pair.Key != null && pair.Value != null)
                        {
                            var val = pair.Value.GetValue(pair.Key, null);
                            callback(propName, val);
                        }
                        else
                            callback(propName, null);
                    }
                }
            }
        }


        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="propName">Name of the prop. Can be nested like "Post.Address.City"</param>
        /// <returns>Returns the property and the object that the property belongs to.</returns>
        public static KeyValuePair<object, PropertyInfo> GetProperty(object obj, string propName)
        {            
            var indexOfDot = propName.IndexOf(".");

            // 1. No "." top level property.
            if (indexOfDot < 0)
                return new KeyValuePair<object, PropertyInfo>(obj, obj.GetType().GetProperty(propName));

            // 1. E.g. Post.Address.Coordinates.Latitute.
            // 2. Need to get the object "Coordinates" and the Property "Latitute".
            var startOfTrim = 0;          
            var subobjectName = propName.Substring(startOfTrim, indexOfDot);
            var subPropName = propName.Substring(indexOfDot + 1);
            var prop = obj.GetType().GetProperty(subobjectName);
            var subobj = prop.GetValue(obj, null);
            var nextIndexOfDot = propName.IndexOf(".", indexOfDot + 1); 
           
            while(nextIndexOfDot > 0)
            {
                startOfTrim = nextIndexOfDot + 1;
                indexOfDot = nextIndexOfDot;                
                subobjectName = propName.Substring(startOfTrim, indexOfDot);
                subPropName = propName.Substring(indexOfDot + 1);
                subobj = obj.GetType().GetProperty(subobjectName);
                nextIndexOfDot = propName.IndexOf(".", indexOfDot + 1);
            }
            prop = subobj.GetType().GetProperty(subPropName);
            return new KeyValuePair<object, PropertyInfo>(subobj, prop);
        }


        /// <summary>
        /// Set the property using the value provided.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="item"></param>
        /// <param name="counterOrRefId"></param>
        /// <param name="errors"></param>
        /// <param name="val"></param>
        public static void SetProperty(PropertyInfo prop, object item, int counterOrRefId, IErrors errors, object val)
        {
            try
            {
                // Found prop. Can now map.
                if (prop != null)
                {
                    object convertedVal = null;
                    // Handle special conversions. e.g. $105 or 9am etc.
                    if (_propertyTypeMappers.ContainsKey(prop.PropertyType) && val != null && val.GetType() == typeof(string))
                    {
                        var converter = _propertyTypeMappers[prop.PropertyType];
                        convertedVal = converter((string)val);
                    }
                    else
                    {
                        convertedVal = ReflectionTypeChecker.ConvertToSameType(prop, val);
                    }
                    ReflectionUtils.SetProperty(item, prop, convertedVal, true);
                }
            }
            catch (Exception ex)
            {
                if (errors != null)
                {
                    var err = $"Unable to map property '{prop.Name}' for counter/refId '{counterOrRefId}'";
                    err += Environment.NewLine + ex.Message;
                    errors.Add(err);
                }
            }
        }


        /// <summary>
        /// Recurse into the type to get all the instance, public, settable properties of the types it's 
        /// nested objects that are properties.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="objectName"></param>
        /// <param name="propNames"></param>
        public static void GetProps(Type type, string objectName, IList<KeyValuePair<string, PropertyInfo>> propNames)
        {
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);

            // 3. Iterate through all the keys.
            foreach (var property in props)
            {
                var fullName = string.IsNullOrEmpty(objectName) ? property.Name : objectName + "." + property.Name;
                if (TypeHelper.IsBasicType(property.PropertyType))
                {
                    propNames.Add(new KeyValuePair<string, PropertyInfo>(fullName, property));
                }
                else // recurse
                {
                    GetProps(property.PropertyType, fullName, propNames);
                }
            }            
        }
    }
}
