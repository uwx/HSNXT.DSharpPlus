#if SQLSERVERCE
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;
using System.Data;
using System.Data.SqlServerCe;

namespace TestProj47 {
public static partial class Extensions
{
    /// <summary>
    ///     A SqlCeConnection extension method that executes the data table operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A DataTable.</returns>
    public static DataTable ExecuteDataTable(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType, SqlCeTransaction transaction)
    {
        using (SqlCeCommand command = @this.CreateCommand())
        {
            command.CommandText = cmdText;
            command.CommandType = commandType;
            command.Transaction = transaction;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            var ds = new DataSet();
            using (var dataAdapter = new SqlCeDataAdapter(command))
            {
                dataAdapter.Fill(ds);
            }

            return ds.Tables[0];
        }
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the data table operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="commandFactory">The command factory.</param>
    /// <returns>A DataTable.</returns>
    public static DataTable ExecuteDataTable(this SqlCeConnection @this, Action<SqlCeCommand> commandFactory)
    {
        using (SqlCeCommand command = @this.CreateCommand())
        {
            commandFactory(command);

            var ds = new DataSet();
            using (var dataAdapter = new SqlCeDataAdapter(command))
            {
                dataAdapter.Fill(ds);
            }

            return ds.Tables[0];
        }
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the data table operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <returns>A DataTable.</returns>
    public static DataTable ExecuteDataTable(this SqlCeConnection @this, string cmdText)
    {
        return @this.ExecuteDataTable(cmdText, null, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the data table operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A DataTable.</returns>
    public static DataTable ExecuteDataTable(this SqlCeConnection @this, string cmdText, SqlCeTransaction transaction)
    {
        return @this.ExecuteDataTable(cmdText, null, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the data table operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns>A DataTable.</returns>
    public static DataTable ExecuteDataTable(this SqlCeConnection @this, string cmdText, CommandType commandType)
    {
        return @this.ExecuteDataTable(cmdText, null, commandType, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the data table operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A DataTable.</returns>
    public static DataTable ExecuteDataTable(this SqlCeConnection @this, string cmdText, CommandType commandType, SqlCeTransaction transaction)
    {
        return @this.ExecuteDataTable(cmdText, null, commandType, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the data table operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <returns>A DataTable.</returns>
    public static DataTable ExecuteDataTable(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters)
    {
        return @this.ExecuteDataTable(cmdText, parameters, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the data table operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A DataTable.</returns>
    public static DataTable ExecuteDataTable(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, SqlCeTransaction transaction)
    {
        return @this.ExecuteDataTable(cmdText, parameters, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the data table operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns>A DataTable.</returns>
    public static DataTable ExecuteDataTable(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType)
    {
        return @this.ExecuteDataTable(cmdText, parameters, commandType, null);
    }
}
}
#endif