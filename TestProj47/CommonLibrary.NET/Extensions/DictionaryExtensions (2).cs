using System;
using System.Collections;
using System.Collections.Generic;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        /// Convert to correct type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T ConvertTo<T>(object input)
        {
            object result = default(T);
            if (typeof(T) == typeof(int))
                result = Convert.ToInt32(input);
            else if (typeof(T) == typeof(long))
                result = Convert.ToInt64(input);
            else if (typeof(T) == typeof(string))
                result = Convert.ToString(input);
            else if (typeof(T) == typeof(bool))
                result = Convert.ToBoolean(input);
            else if (typeof(T) == typeof(double))
                result = Convert.ToDouble(input);
            else if (typeof(T) == typeof(DateTime))
                result = Convert.ToDateTime(input);
            else
                result = input;

            return (T)result;
        }


        #region Extensions for IDictionary
        /// <summary>
        /// Get typed value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="d"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this IDictionary d, string key)
        {
            var result = d[key];
            if (result == null) return default;
            var converted = ConvertTo<T>(result);
            return converted;
        }


        /// <summary>
        /// Get using default value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="d"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetOrDefault<T>(this IDictionary d, string key, T defaultValue)
        {
            if (!d.Contains(key)) return defaultValue;

            return Get<T>(d, key);
        }


        /// <summary>
        /// Get section key value.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="sectionName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(this IDictionary d, string sectionName, string key)
        {
            if (!d.Contains(sectionName)) return null;
            var section = d[sectionName] as IDictionary;
            if (!section.Contains(key)) return null;
            return section[key];
        }


        /// <summary>
        /// Get typed section key value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="d"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this IDictionary d, string section, string key)
        {
            var result = Get(d, section, key);
            if (result == null) return default;

            var converted = ConvertTo<T>(result);
            return converted;
        }


        /// <summary>
        /// Get section/key value if present, default value otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="d"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetOrDefault<T>(this IDictionary d, string section, string key, T defaultValue)
        {
            if (string.IsNullOrEmpty(section)) return defaultValue;

            // Validate and return default value.
            if (!d.Contains(section, key)) return defaultValue;
            return Get<T>(d, section, key);
        }


        /// <summary>
        /// Get a IDictionary.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public static IDictionary Section(this IDictionary d, string section)
        {
            if (d == null || d.Count == 0) return null;

            if(d.Contains(section)) 
                return d[section] as IDictionary;

            return null;
        }


        /// <summary>
        /// Whether the section/key is there.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="sectionName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Contains(this IDictionary d, string sectionName, string key)
        {
            var section = Section(d, sectionName);
            if (section == null) return false;

            return section.Contains(key);
        }
        #endregion


        #region Extensions for IDictionary<string, object>
        /// <summary>
        /// Get typed value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="d"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this IDictionary<string, object> d, string key)
        {
            var result = d[key];
            if (result == null) return default;
            var converted = ConvertTo<T>(result);
            return converted;
        }


        /// <summary>
        /// Get using default value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="d"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetOrDefault<T>(this IDictionary<string, object> d, string key, T defaultValue)
        {
            if (!d.ContainsKey(key)) return defaultValue;

            return Get<T>(d, key);
        }


        /// <summary>
        /// Get section key value.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="sectionName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(this IDictionary<string, object> d, string sectionName, string key)
        {
            if (!d.ContainsKey(sectionName)) return null;
            var section = d[sectionName] as IDictionary;
            if (!section.Contains(key)) return null;
            return section[key];
        }


        /// <summary>
        /// Get typed section key value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="d"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this IDictionary<string, object> d, string section, string key)
        {
            var result = Get(d, section, key);
            if (result == null) return default;

            var converted = ConvertTo<T>(result);
            return converted;
        }


        /// <summary>
        /// Get section/key value if present, default value otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="d"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Get<T>(this IDictionary<string, object> d, string section, string key, T defaultValue)
        {
            if (string.IsNullOrEmpty(section)) return defaultValue;

            // Validate and return default value.
            if (!d.Contains(section, key)) return defaultValue;
            return Get<T>(d, section, key);
        }


        /// <summary>
        /// Get a IDictionary.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public static IDictionary<string, object> GetSection(this IDictionary<string, object> d, string section)
        {
            if (d.ContainsKey(section))
                return d[section] as IDictionary<string, object>;

            return null;
        }


        /// <summary>
        /// Whether the section/key is there.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="sectionName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Contains(this IDictionary<string, object> d, string sectionName, string key)
        {
            var section = GetSection(d, sectionName);
            if (section == null) return false;

            return section.ContainsKey(key);
        }
        #endregion
    }
}
