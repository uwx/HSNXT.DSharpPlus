#if MYSQL
using System;
using System.Linq.Expressions;
using System.Runtime.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Collections.Specialized;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Data;
using System.Drawing;
using System.Web;
using System.Globalization;
using System.ComponentModel;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Data.Entity.Design.PluralizationServices;
using System.Security;
using System.Xml.Linq;
using System.Xml;
using System.Collections.ObjectModel;
using System.Data.Common;
//using System.Data.SqlServerCe;
using System.Drawing.Drawing2D;
using System.Security.AccessControl;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.Web.UI;
using System.Windows.Forms;
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System;
//using System.Data;
//using System.Data.SqlClient;

namespace TestProj47
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A SqlConnection extension method that executes the data set operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A DataSet.</returns>
        public static DataSet ExecuteDataSet(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType, SqlTransaction transaction)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                command.CommandText = cmdText;
                command.CommandType = commandType;
                command.Transaction = transaction;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                var ds = new DataSet();
                using (var dataAdapter = new SqlDataAdapter(command))
                {
                    dataAdapter.Fill(ds);
                }

                return ds;
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the data set operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>A DataSet.</returns>
        public static DataSet ExecuteDataSet(this SqlConnection @this, Action<SqlCommand> commandFactory)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                commandFactory(command);

                var ds = new DataSet();
                using (var dataAdapter = new SqlDataAdapter(command))
                {
                    dataAdapter.Fill(ds);
                }

                return ds;
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the data set operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <returns>A DataSet.</returns>
        public static DataSet ExecuteDataSet(this SqlConnection @this, string cmdText)
        {
            return @this.ExecuteDataSet(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the data set operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A DataSet.</returns>
        public static DataSet ExecuteDataSet(this SqlConnection @this, string cmdText, SqlTransaction transaction)
        {
            return @this.ExecuteDataSet(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the data set operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A DataSet.</returns>
        public static DataSet ExecuteDataSet(this SqlConnection @this, string cmdText, CommandType commandType)
        {
            return @this.ExecuteDataSet(cmdText, null, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the data set operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A DataSet.</returns>
        public static DataSet ExecuteDataSet(this SqlConnection @this, string cmdText, CommandType commandType,
            SqlTransaction transaction)
        {
            return @this.ExecuteDataSet(cmdText, null, commandType, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the data set operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <returns>A DataSet.</returns>
        public static DataSet ExecuteDataSet(this SqlConnection @this, string cmdText, SqlParameter[] parameters)
        {
            return @this.ExecuteDataSet(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the data set operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A DataSet.</returns>
        public static DataSet ExecuteDataSet(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            SqlTransaction transaction)
        {
            return @this.ExecuteDataSet(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the data set operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A DataSet.</returns>
        public static DataSet ExecuteDataSet(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType)
        {
            return @this.ExecuteDataSet(cmdText, parameters, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the data table operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A DataTable.</returns>
        public static DataTable ExecuteDataTable(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType, SqlTransaction transaction)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                command.CommandText = cmdText;
                command.CommandType = commandType;
                command.Transaction = transaction;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                var ds = new DataSet();
                using (var dataAdapter = new SqlDataAdapter(command))
                {
                    dataAdapter.Fill(ds);
                }

                return ds.Tables[0];
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the data table operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>A DataTable.</returns>
        public static DataTable ExecuteDataTable(this SqlConnection @this, Action<SqlCommand> commandFactory)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                commandFactory(command);

                var ds = new DataSet();
                using (var dataAdapter = new SqlDataAdapter(command))
                {
                    dataAdapter.Fill(ds);
                }

                return ds.Tables[0];
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the data table operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <returns>A DataTable.</returns>
        public static DataTable ExecuteDataTable(this SqlConnection @this, string cmdText)
        {
            return @this.ExecuteDataTable(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the data table operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A DataTable.</returns>
        public static DataTable ExecuteDataTable(this SqlConnection @this, string cmdText, SqlTransaction transaction)
        {
            return @this.ExecuteDataTable(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the data table operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A DataTable.</returns>
        public static DataTable ExecuteDataTable(this SqlConnection @this, string cmdText, CommandType commandType)
        {
            return @this.ExecuteDataTable(cmdText, null, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the data table operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A DataTable.</returns>
        public static DataTable ExecuteDataTable(this SqlConnection @this, string cmdText, CommandType commandType,
            SqlTransaction transaction)
        {
            return @this.ExecuteDataTable(cmdText, null, commandType, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the data table operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <returns>A DataTable.</returns>
        public static DataTable ExecuteDataTable(this SqlConnection @this, string cmdText, SqlParameter[] parameters)
        {
            return @this.ExecuteDataTable(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the data table operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A DataTable.</returns>
        public static DataTable ExecuteDataTable(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            SqlTransaction transaction)
        {
            return @this.ExecuteDataTable(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the data table operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A DataTable.</returns>
        public static DataTable ExecuteDataTable(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType)
        {
            return @this.ExecuteDataTable(cmdText, parameters, commandType, null);
        }

        /// <summary>
        ///     Enumerates execute entities in this collection.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An enumerator that allows foreach to be used to process execute entities in this collection.</returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SqlConnection @this, string cmdText,
            SqlParameter[] parameters, CommandType commandType, SqlTransaction transaction) where T : new()
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                command.CommandText = cmdText;
                command.CommandType = commandType;
                command.Transaction = transaction;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                using (IDataReader reader = command.ExecuteReader())
                {
                    return reader.ToEntities<T>();
                }
            }
        }

        /// <summary>
        ///     Enumerates execute entities in this collection.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>An enumerator that allows foreach to be used to process execute entities in this collection.</returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SqlConnection @this, Action<SqlCommand> commandFactory)
            where T : new()
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                commandFactory(command);

                using (IDataReader reader = command.ExecuteReader())
                {
                    return reader.ToEntities<T>();
                }
            }
        }

        /// <summary>
        ///     Enumerates execute entities in this collection.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <returns>An enumerator that allows foreach to be used to process execute entities in this collection.</returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SqlConnection @this, string cmdText) where T : new()
        {
            return @this.ExecuteEntities<T>(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        ///     Enumerates execute entities in this collection.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An enumerator that allows foreach to be used to process execute entities in this collection.</returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SqlConnection @this, string cmdText,
            SqlTransaction transaction) where T : new()
        {
            return @this.ExecuteEntities<T>(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        ///     Enumerates execute entities in this collection.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An enumerator that allows foreach to be used to process execute entities in this collection.</returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SqlConnection @this, string cmdText,
            CommandType commandType) where T : new()
        {
            return @this.ExecuteEntities<T>(cmdText, null, commandType, null);
        }

        /// <summary>
        ///     Enumerates execute entities in this collection.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An enumerator that allows foreach to be used to process execute entities in this collection.</returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SqlConnection @this, string cmdText,
            CommandType commandType, SqlTransaction transaction) where T : new()
        {
            return @this.ExecuteEntities<T>(cmdText, null, commandType, transaction);
        }

        /// <summary>
        ///     Enumerates execute entities in this collection.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <returns>An enumerator that allows foreach to be used to process execute entities in this collection.</returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SqlConnection @this, string cmdText,
            SqlParameter[] parameters) where T : new()
        {
            return @this.ExecuteEntities<T>(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        ///     Enumerates execute entities in this collection.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An enumerator that allows foreach to be used to process execute entities in this collection.</returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SqlConnection @this, string cmdText,
            SqlParameter[] parameters, SqlTransaction transaction) where T : new()
        {
            return @this.ExecuteEntities<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        ///     Enumerates execute entities in this collection.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An enumerator that allows foreach to be used to process execute entities in this collection.</returns>
        public static IEnumerable<T> ExecuteEntities<T>(this SqlConnection @this, string cmdText,
            SqlParameter[] parameters, CommandType commandType) where T : new()
        {
            return @this.ExecuteEntities<T>(cmdText, parameters, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the entity operation.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A T.</returns>
        public static T ExecuteEntity<T>(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType, SqlTransaction transaction) where T : new()
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                command.CommandText = cmdText;
                command.CommandType = commandType;
                command.Transaction = transaction;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                using (IDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    return reader.ToEntity<T>();
                }
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the entity operation.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>A T.</returns>
        public static T ExecuteEntity<T>(this SqlConnection @this, Action<SqlCommand> commandFactory) where T : new()
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                commandFactory(command);

                using (IDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    return reader.ToEntity<T>();
                }
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the entity operation.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <returns>A T.</returns>
        public static T ExecuteEntity<T>(this SqlConnection @this, string cmdText) where T : new()
        {
            return @this.ExecuteEntity<T>(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the entity operation.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A T.</returns>
        public static T ExecuteEntity<T>(this SqlConnection @this, string cmdText, SqlTransaction transaction)
            where T : new()
        {
            return @this.ExecuteEntity<T>(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the entity operation.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A T.</returns>
        public static T ExecuteEntity<T>(this SqlConnection @this, string cmdText, CommandType commandType)
            where T : new()
        {
            return @this.ExecuteEntity<T>(cmdText, null, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the entity operation.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A T.</returns>
        public static T ExecuteEntity<T>(this SqlConnection @this, string cmdText, CommandType commandType,
            SqlTransaction transaction) where T : new()
        {
            return @this.ExecuteEntity<T>(cmdText, null, commandType, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the entity operation.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <returns>A T.</returns>
        public static T ExecuteEntity<T>(this SqlConnection @this, string cmdText, SqlParameter[] parameters)
            where T : new()
        {
            return @this.ExecuteEntity<T>(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the entity operation.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A T.</returns>
        public static T ExecuteEntity<T>(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            SqlTransaction transaction) where T : new()
        {
            return @this.ExecuteEntity<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the entity operation.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A T.</returns>
        public static T ExecuteEntity<T>(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType) where T : new()
        {
            return @this.ExecuteEntity<T>(cmdText, parameters, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the expando object operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A dynamic.</returns>
        public static dynamic ExecuteExpandoObject(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType, SqlTransaction transaction)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                command.CommandText = cmdText;
                command.CommandType = commandType;
                command.Transaction = transaction;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                using (IDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    return reader.ToExpandoObject();
                }
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the expando object operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>A dynamic.</returns>
        public static dynamic ExecuteExpandoObject(this SqlConnection @this, Action<SqlCommand> commandFactory)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                commandFactory(command);

                using (IDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    return reader.ToExpandoObject();
                }
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the expando object operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <returns>A dynamic.</returns>
        public static dynamic ExecuteExpandoObject(this SqlConnection @this, string cmdText)
        {
            return @this.ExecuteExpandoObject(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the expando object operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A dynamic.</returns>
        public static dynamic ExecuteExpandoObject(this SqlConnection @this, string cmdText, SqlTransaction transaction)
        {
            return @this.ExecuteExpandoObject(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the expando object operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A dynamic.</returns>
        public static dynamic ExecuteExpandoObject(this SqlConnection @this, string cmdText, CommandType commandType)
        {
            return @this.ExecuteExpandoObject(cmdText, null, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the expando object operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A dynamic.</returns>
        public static dynamic ExecuteExpandoObject(this SqlConnection @this, string cmdText, CommandType commandType,
            SqlTransaction transaction)
        {
            return @this.ExecuteExpandoObject(cmdText, null, commandType, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the expando object operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <returns>A dynamic.</returns>
        public static dynamic ExecuteExpandoObject(this SqlConnection @this, string cmdText, SqlParameter[] parameters)
        {
            return @this.ExecuteExpandoObject(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the expando object operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A dynamic.</returns>
        public static dynamic ExecuteExpandoObject(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            SqlTransaction transaction)
        {
            return @this.ExecuteExpandoObject(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the expando object operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A dynamic.</returns>
        public static dynamic ExecuteExpandoObject(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType)
        {
            return @this.ExecuteExpandoObject(cmdText, parameters, commandType, null);
        }

        /// <summary>
        ///     Enumerates execute expando objects in this collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process execute expando objects in this collection.
        /// </returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlConnection @this, string cmdText,
            SqlParameter[] parameters, CommandType commandType, SqlTransaction transaction)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                command.CommandText = cmdText;
                command.CommandType = commandType;
                command.Transaction = transaction;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                using (IDataReader reader = command.ExecuteReader())
                {
                    return reader.ToExpandoObjects();
                }
            }
        }

        /// <summary>
        ///     Enumerates execute expando objects in this collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process execute expando objects in this collection.
        /// </returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlConnection @this,
            Action<SqlCommand> commandFactory)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                commandFactory(command);

                using (IDataReader reader = command.ExecuteReader())
                {
                    return reader.ToExpandoObjects();
                }
            }
        }

        /// <summary>
        ///     Enumerates execute expando objects in this collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process execute expando objects in this collection.
        /// </returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlConnection @this, string cmdText)
        {
            return @this.ExecuteExpandoObjects(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        ///     Enumerates execute expando objects in this collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process execute expando objects in this collection.
        /// </returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlConnection @this, string cmdText,
            SqlTransaction transaction)
        {
            return @this.ExecuteExpandoObjects(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        ///     Enumerates execute expando objects in this collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process execute expando objects in this collection.
        /// </returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlConnection @this, string cmdText,
            CommandType commandType)
        {
            return @this.ExecuteExpandoObjects(cmdText, null, commandType, null);
        }

        /// <summary>
        ///     Enumerates execute expando objects in this collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process execute expando objects in this collection.
        /// </returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlConnection @this, string cmdText,
            CommandType commandType, SqlTransaction transaction)
        {
            return @this.ExecuteExpandoObjects(cmdText, null, commandType, transaction);
        }

        /// <summary>
        ///     Enumerates execute expando objects in this collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process execute expando objects in this collection.
        /// </returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlConnection @this, string cmdText,
            SqlParameter[] parameters)
        {
            return @this.ExecuteExpandoObjects(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        ///     Enumerates execute expando objects in this collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process execute expando objects in this collection.
        /// </returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlConnection @this, string cmdText,
            SqlParameter[] parameters, SqlTransaction transaction)
        {
            return @this.ExecuteExpandoObjects(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        ///     Enumerates execute expando objects in this collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process execute expando objects in this collection.
        /// </returns>
        public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlConnection @this, string cmdText,
            SqlParameter[] parameters, CommandType commandType)
        {
            return @this.ExecuteExpandoObjects(cmdText, parameters, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the non query operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        public static void ExecuteNonQuery(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType, SqlTransaction transaction)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                command.CommandText = cmdText;
                command.CommandType = commandType;
                command.Transaction = transaction;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the non query operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="commandFactory">The command factory.</param>
        public static void ExecuteNonQuery(this SqlConnection @this, Action<SqlCommand> commandFactory)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                commandFactory(command);

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the non query operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        public static void ExecuteNonQuery(this SqlConnection @this, string cmdText)
        {
            @this.ExecuteNonQuery(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the non query operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="transaction">The transaction.</param>
        public static void ExecuteNonQuery(this SqlConnection @this, string cmdText, SqlTransaction transaction)
        {
            @this.ExecuteNonQuery(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the non query operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        public static void ExecuteNonQuery(this SqlConnection @this, string cmdText, CommandType commandType)
        {
            @this.ExecuteNonQuery(cmdText, null, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the non query operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        public static void ExecuteNonQuery(this SqlConnection @this, string cmdText, CommandType commandType,
            SqlTransaction transaction)
        {
            @this.ExecuteNonQuery(cmdText, null, commandType, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the non query operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        public static void ExecuteNonQuery(this SqlConnection @this, string cmdText, SqlParameter[] parameters)
        {
            @this.ExecuteNonQuery(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the non query operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="transaction">The transaction.</param>
        public static void ExecuteNonQuery(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            SqlTransaction transaction)
        {
            @this.ExecuteNonQuery(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the non query operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        public static void ExecuteNonQuery(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType)
        {
            @this.ExecuteNonQuery(cmdText, parameters, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A SqlDataReader.</returns>
        public static SqlDataReader ExecuteReader(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType, SqlTransaction transaction)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                command.CommandText = cmdText;
                command.CommandType = commandType;
                command.Transaction = transaction;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                return command.ExecuteReader();
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>A SqlDataReader.</returns>
        public static SqlDataReader ExecuteReader(this SqlConnection @this, Action<SqlCommand> commandFactory)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                commandFactory(command);

                return command.ExecuteReader();
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <returns>A SqlDataReader.</returns>
        public static SqlDataReader ExecuteReader(this SqlConnection @this, string cmdText)
        {
            return @this.ExecuteReader(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A SqlDataReader.</returns>
        public static SqlDataReader ExecuteReader(this SqlConnection @this, string cmdText, SqlTransaction transaction)
        {
            return @this.ExecuteReader(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A SqlDataReader.</returns>
        public static SqlDataReader ExecuteReader(this SqlConnection @this, string cmdText, CommandType commandType)
        {
            return @this.ExecuteReader(cmdText, null, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A SqlDataReader.</returns>
        public static SqlDataReader ExecuteReader(this SqlConnection @this, string cmdText, CommandType commandType,
            SqlTransaction transaction)
        {
            return @this.ExecuteReader(cmdText, null, commandType, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <returns>A SqlDataReader.</returns>
        public static SqlDataReader ExecuteReader(this SqlConnection @this, string cmdText, SqlParameter[] parameters)
        {
            return @this.ExecuteReader(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A SqlDataReader.</returns>
        public static SqlDataReader ExecuteReader(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            SqlTransaction transaction)
        {
            return @this.ExecuteReader(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>A SqlDataReader.</returns>
        public static SqlDataReader ExecuteReader(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType)
        {
            return @this.ExecuteReader(cmdText, parameters, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An object.</returns>
        public static object ExecuteScalar(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType, SqlTransaction transaction)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                command.CommandText = cmdText;
                command.CommandType = commandType;
                command.Transaction = transaction;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                return command.ExecuteScalar();
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>An object.</returns>
        public static object ExecuteScalar(this SqlConnection @this, Action<SqlCommand> commandFactory)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                commandFactory(command);

                return command.ExecuteScalar();
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <returns>An object.</returns>
        public static object ExecuteScalar(this SqlConnection @this, string cmdText)
        {
            return @this.ExecuteScalar(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An object.</returns>
        public static object ExecuteScalar(this SqlConnection @this, string cmdText, SqlTransaction transaction)
        {
            return @this.ExecuteScalar(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An object.</returns>
        public static object ExecuteScalar(this SqlConnection @this, string cmdText, CommandType commandType)
        {
            return @this.ExecuteScalar(cmdText, null, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An object.</returns>
        public static object ExecuteScalar(this SqlConnection @this, string cmdText, CommandType commandType,
            SqlTransaction transaction)
        {
            return @this.ExecuteScalar(cmdText, null, commandType, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <returns>An object.</returns>
        public static object ExecuteScalar(this SqlConnection @this, string cmdText, SqlParameter[] parameters)
        {
            return @this.ExecuteScalar(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An object.</returns>
        public static object ExecuteScalar(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            SqlTransaction transaction)
        {
            return @this.ExecuteScalar(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An object.</returns>
        public static object ExecuteScalar(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType)
        {
            return @this.ExecuteScalar(cmdText, parameters, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarAs<T>(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType, SqlTransaction transaction)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                command.CommandText = cmdText;
                command.CommandType = commandType;
                command.Transaction = transaction;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                return (T) command.ExecuteScalar();
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarAs<T>(this SqlConnection @this, Action<SqlCommand> commandFactory)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                commandFactory(command);

                return (T) command.ExecuteScalar();
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarAs<T>(this SqlConnection @this, string cmdText)
        {
            return @this.ExecuteScalarAs<T>(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarAs<T>(this SqlConnection @this, string cmdText, SqlTransaction transaction)
        {
            return @this.ExecuteScalarAs<T>(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarAs<T>(this SqlConnection @this, string cmdText, CommandType commandType)
        {
            return @this.ExecuteScalarAs<T>(cmdText, null, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarAs<T>(this SqlConnection @this, string cmdText, CommandType commandType,
            SqlTransaction transaction)
        {
            return @this.ExecuteScalarAs<T>(cmdText, null, commandType, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarAs<T>(this SqlConnection @this, string cmdText, SqlParameter[] parameters)
        {
            return @this.ExecuteScalarAs<T>(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarAs<T>(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            SqlTransaction transaction)
        {
            return @this.ExecuteScalarAs<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarAs<T>(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType)
        {
            return @this.ExecuteScalarAs<T>(cmdText, parameters, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarTo<T>(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType, SqlTransaction transaction)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                command.CommandText = cmdText;
                command.CommandType = commandType;
                command.Transaction = transaction;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                return command.ExecuteScalar().To<T>();
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarTo<T>(this SqlConnection @this, Action<SqlCommand> commandFactory)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                commandFactory(command);

                return command.ExecuteScalar().To<T>();
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarTo<T>(this SqlConnection @this, string cmdText)
        {
            return @this.ExecuteScalarTo<T>(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarTo<T>(this SqlConnection @this, string cmdText, SqlTransaction transaction)
        {
            return @this.ExecuteScalarTo<T>(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarTo<T>(this SqlConnection @this, string cmdText, CommandType commandType)
        {
            return @this.ExecuteScalarTo<T>(cmdText, null, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarTo<T>(this SqlConnection @this, string cmdText, CommandType commandType,
            SqlTransaction transaction)
        {
            return @this.ExecuteScalarTo<T>(cmdText, null, commandType, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarTo<T>(this SqlConnection @this, string cmdText, SqlParameter[] parameters)
        {
            return @this.ExecuteScalarTo<T>(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarTo<T>(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            SqlTransaction transaction)
        {
            return @this.ExecuteScalarTo<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the scalar operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An object.</returns>
        public static T ExecuteScalarTo<T>(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType)
        {
            return @this.ExecuteScalarTo<T>(cmdText, parameters, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the XML reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An XmlReader.</returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType, SqlTransaction transaction)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                command.CommandText = cmdText;
                command.CommandType = commandType;
                command.Transaction = transaction;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                return command.ExecuteXmlReader();
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the XML reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>An XmlReader.</returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, Action<SqlCommand> commandFactory)
        {
            using (SqlCommand command = @this.CreateCommand())
            {
                commandFactory(command);

                return command.ExecuteXmlReader();
            }
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the XML reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <returns>An XmlReader.</returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, string cmdText)
        {
            return @this.ExecuteXmlReader(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the XML reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An XmlReader.</returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, string cmdText, SqlTransaction transaction)
        {
            return @this.ExecuteXmlReader(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the XML reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An XmlReader.</returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, string cmdText, CommandType commandType)
        {
            return @this.ExecuteXmlReader(cmdText, null, commandType, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the XML reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An XmlReader.</returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, string cmdText, CommandType commandType,
            SqlTransaction transaction)
        {
            return @this.ExecuteXmlReader(cmdText, null, commandType, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the XML reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <returns>An XmlReader.</returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, string cmdText, SqlParameter[] parameters)
        {
            return @this.ExecuteXmlReader(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the XML reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>An XmlReader.</returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            SqlTransaction transaction)
        {
            return @this.ExecuteXmlReader(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        ///     A SqlConnection extension method that executes the XML reader operation.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="cmdText">The command text.</param>
        /// <param name="parameters">Options for controlling the operation.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>An XmlReader.</returns>
        public static XmlReader ExecuteXmlReader(this SqlConnection @this, string cmdText, SqlParameter[] parameters,
            CommandType commandType)
        {
            return @this.ExecuteXmlReader(cmdText, parameters, commandType, null);
        }
    }
}
#endif