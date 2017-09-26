// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

using System;
using System.Data;
using System.Data.SqlClient;

namespace TestProj47
{
    public static partial class Extensions
    {
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
    }
}