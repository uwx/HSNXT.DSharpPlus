using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;

//using System.Data.SqlServerCe;

// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System;
//using System.Data;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     An IDataReader extension method that query if '@this' contains column.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnIndex">Zero-based index of the column.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool ContainsColumn(this IDataReader @this, int columnIndex)
        {
            try
            {
                // Check if FieldCount is implemented first
                return @this.FieldCount > columnIndex;
            }
            catch (Exception)
            {
                try
                {
                    return @this[columnIndex] != null;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        ///     An IDataReader extension method that query if '@this' contains column.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool ContainsColumn(this IDataReader @this, string columnName)
        {
            try
            {
                // Check if GetOrdinal is implemented first
                return @this.GetOrdinal(columnName) != -1;
            }
            catch (Exception)
            {
                try
                {
                    return @this[columnName] != null;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        ///     An IDataReader extension method that applies an operation to all items in this collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="action">The action.</param>
        /// <returns>An IDataReader.</returns>
        public static IDataReader ForEach(this IDataReader @this, Action<IDataReader> action)
        {
            while (@this.Read())
            {
                action(@this);
            }

            return @this;
        }

        /// <summary>
        ///     Gets the column names in this collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>An enumerator that allows foreach to be used to get the column names in this collection.</returns>
        public static IEnumerable<string> GetColumnNames(this IDataRecord @this)
        {
            return Enumerable.Range(0, @this.FieldCount)
                .Select(@this.GetName)
                .ToList();
        }

        /// <summary>
        ///     An IDataReader extension method that gets value as.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <returns>The value as.</returns>
        public static T GetValueAs<T>(this IDataReader @this, int index)
        {
            return (T) @this.GetValue(index);
        }

        /// <summary>
        ///     An IDataReader extension method that gets value as.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>The value as.</returns>
        public static T GetValueAs<T>(this IDataReader @this, string columnName)
        {
            return (T) @this.GetValue(@this.GetOrdinal(columnName));
        }

        /// <summary>
        ///     An IDataReader extension method that gets value as or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <returns>The value as or default.</returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, int index)
        {
            try
            {
                return (T) @this.GetValue(index);
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value as or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value as or default.</returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, int index, T defaultValue)
        {
            try
            {
                return (T) @this.GetValue(index);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value as or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The value as or default.</returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, int index,
            Func<IDataReader, int, T> defaultValueFactory)
        {
            try
            {
                return (T) @this.GetValue(index);
            }
            catch
            {
                return defaultValueFactory(@this, index);
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value as or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>The value as or default.</returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, string columnName)
        {
            try
            {
                return (T) @this.GetValue(@this.GetOrdinal(columnName));
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value as or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value as or default.</returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, string columnName, T defaultValue)
        {
            try
            {
                return (T) @this.GetValue(@this.GetOrdinal(columnName));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value as or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The value as or default.</returns>
        public static T GetValueAsOrDefault<T>(this IDataReader @this, string columnName,
            Func<IDataReader, string, T> defaultValueFactory)
        {
            try
            {
                return (T) @this.GetValue(@this.GetOrdinal(columnName));
            }
            catch
            {
                return defaultValueFactory(@this, columnName);
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value to.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <returns>The value to.</returns>
        public static T GetValueTo<T>(this IDataReader @this, int index)
        {
            return @this.GetValue(index).To<T>();
        }

        /// <summary>
        ///     An IDataReader extension method that gets value to.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>The value to.</returns>
        public static T GetValueTo<T>(this IDataReader @this, string columnName)
        {
            return @this.GetValue(@this.GetOrdinal(columnName)).To<T>();
        }

        /// <summary>
        ///     An IDataReader extension method that gets value to or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <returns>The value to or default.</returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, int index)
        {
            try
            {
                return @this.GetValue(index).To<T>();
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value to or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value to or default.</returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, int index, T defaultValue)
        {
            try
            {
                return @this.GetValue(index).To<T>();
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value to or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The value to or default.</returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, int index,
            Func<IDataReader, int, T> defaultValueFactory)
        {
            try
            {
                return @this.GetValue(index).To<T>();
            }
            catch
            {
                return defaultValueFactory(@this, index);
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value to or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>The value to or default.</returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, string columnName)
        {
            try
            {
                return @this.GetValue(@this.GetOrdinal(columnName)).To<T>();
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value to or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value to or default.</returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, string columnName, T defaultValue)
        {
            try
            {
                return @this.GetValue(@this.GetOrdinal(columnName)).To<T>();
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     An IDataReader extension method that gets value to or default.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValueFactory">The default value factory.</param>
        /// <returns>The value to or default.</returns>
        public static T GetValueToOrDefault<T>(this IDataReader @this, string columnName,
            Func<IDataReader, string, T> defaultValueFactory)
        {
            try
            {
                return @this.GetValue(@this.GetOrdinal(columnName)).To<T>();
            }
            catch
            {
                return defaultValueFactory(@this, columnName);
            }
        }

        /// <summary>
        ///     An IDataReader extension method that query if '@this' is database null.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="name">The name.</param>
        /// <returns>true if database null, false if not.</returns>
        public static bool IsDbNull(this IDataReader @this, string name)
        {
            return @this.IsDBNull(@this.GetOrdinal(name));
        }

        /// <summary>
        ///     An IDataReader extension method that converts the @this to a data table.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a DataTable.</returns>
        public static DataTable ToDataTable(this IDataReader @this)
        {
            var dt = new DataTable();
            dt.Load(@this);
            return dt;
        }

        /// <summary>
        ///     Enumerates to entities in this collection.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an IEnumerable&lt;T&gt;</returns>
        public static IEnumerable<T> ToEntities<T>(this IDataReader @this) where T : new()
        {
            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            var list = new List<T>();

            var hash = new HashSet<string>(Enumerable.Range(0, @this.FieldCount)
                .Select(@this.GetName));

            while (@this.Read())
            {
                var entity = new T();

                foreach (var property in properties)
                {
                    if (hash.Contains(property.Name))
                    {
                        var valueType = property.PropertyType;
                        property.SetValue(entity, @this[property.Name].To(valueType), null);
                    }
                }

                foreach (var field in fields)
                {
                    if (hash.Contains(field.Name))
                    {
                        var valueType = field.FieldType;
                        field.SetValue(entity, @this[field.Name].To(valueType));
                    }
                }

                list.Add(entity);
            }

            return list;
        }

        /// <summary>
        ///     An IDataReader extension method that converts the @this to an entity.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a T.</returns>
        public static T ToEntity<T>(this IDataReader @this) where T : new()
        {
            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            var entity = new T();

            var hash = new HashSet<string>(Enumerable.Range(0, @this.FieldCount)
                .Select(@this.GetName));

            foreach (var property in properties)
            {
                if (hash.Contains(property.Name))
                {
                    var valueType = property.PropertyType;
                    property.SetValue(entity, @this[property.Name].To(valueType), null);
                }
            }

            foreach (var field in fields)
            {
                if (hash.Contains(field.Name))
                {
                    var valueType = field.FieldType;
                    field.SetValue(entity, @this[field.Name].To(valueType));
                }
            }

            return entity;
        }

        /// <summary>
        ///     An IDataReader extension method that converts the @this to an expando object.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a dynamic.</returns>
        public static dynamic ToExpandoObject(this IDataReader @this)
        {
            var columnNames = Enumerable.Range(0, @this.FieldCount)
                .Select(x => new KeyValuePair<int, string>(x, @this.GetName(x)))
                .ToDictionary(pair => pair.Key);

            dynamic entity = new ExpandoObject();
            var expandoDict = (IDictionary<string, object>) entity;

            Enumerable.Range(0, @this.FieldCount)
                .ToList()
                .ForEach(x => expandoDict.Add(columnNames[x].Value, @this[x]));

            return entity;
        }

        /// <summary>
        ///     Enumerates to expando objects in this collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an IEnumerable&lt;dynamic&gt;</returns>
        public static IEnumerable<dynamic> ToExpandoObjects(this IDataReader @this)
        {
            var columnNames = Enumerable.Range(0, @this.FieldCount)
                .Select(x => new KeyValuePair<int, string>(x, @this.GetName(x)))
                .ToDictionary(pair => pair.Key);

            var list = new List<dynamic>();

            while (@this.Read())
            {
                dynamic entity = new ExpandoObject();
                var expandoDict = (IDictionary<string, object>) entity;

                Enumerable.Range(0, @this.FieldCount)
                    .ToList()
                    .ForEach(x => expandoDict.Add(columnNames[x].Value, @this[x]));

                list.Add(entity);
            }

            return list;
        }
    }
}