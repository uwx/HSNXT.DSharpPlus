// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public static partial class Extensions
{
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
    public static IEnumerable<T> ExecuteEntities<T>(this SqlConnection @this, string cmdText, SqlParameter[] parameters, CommandType commandType, SqlTransaction transaction) where T : new()
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
    public static IEnumerable<T> ExecuteEntities<T>(this SqlConnection @this, Action<SqlCommand> commandFactory) where T : new()
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
    public static IEnumerable<T> ExecuteEntities<T>(this SqlConnection @this, string cmdText, SqlTransaction transaction) where T : new()
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
    public static IEnumerable<T> ExecuteEntities<T>(this SqlConnection @this, string cmdText, CommandType commandType) where T : new()
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
    public static IEnumerable<T> ExecuteEntities<T>(this SqlConnection @this, string cmdText, CommandType commandType, SqlTransaction transaction) where T : new()
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
    public static IEnumerable<T> ExecuteEntities<T>(this SqlConnection @this, string cmdText, SqlParameter[] parameters) where T : new()
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
    public static IEnumerable<T> ExecuteEntities<T>(this SqlConnection @this, string cmdText, SqlParameter[] parameters, SqlTransaction transaction) where T : new()
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
    public static IEnumerable<T> ExecuteEntities<T>(this SqlConnection @this, string cmdText, SqlParameter[] parameters, CommandType commandType) where T : new()
    {
        return @this.ExecuteEntities<T>(cmdText, parameters, commandType, null);
    }
}