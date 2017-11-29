using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;

//using System.Data.SqlServerCe;

#if SQLSERVERCE
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
//using System.Data;
////using System.Data.SqlServerCe;
namespace TestProj47 {
public static partial class Extensions
{
    /// <summary>
    ///     Executes the query, and returns the result set as DataSet.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A DataSet that is equivalent to the result set.</returns>
    public static DataSet ExecuteDataSet(this SqlCeCommand @this)
    {
        var ds = new DataSet();
        using (var dataAdapter = new SqlCeDataAdapter(@this))
        {
            dataAdapter.Fill(ds);
        }

        return ds;
    }
}
}
#endif
#if SQLSERVERCE
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
//using System.Data;
////using System.Data.SqlServerCe;
namespace TestProj47 {
public static partial class Extensions
{
    /// <summary>
    ///     Executes the query, and returns the first result set as DataTable.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A DataTable that is equivalent to the first result set.</returns>
    public static DataTable ExecuteDataTable(this SqlCeCommand @this)
    {
        var dt = new DataTable();
        using (var dataAdapter = new SqlCeDataAdapter(@this))
        {
            dataAdapter.Fill(dt);
        }

        return dt;
    }
}
}
#endif
#if SQLSERVERCE
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
//using System;
//using System.Data;
////using System.Data.SqlServerCe;
namespace TestProj47 {
public static partial class Extensions
{
    /// <summary>
    ///     A SqlCeConnection extension method that executes the data set operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A DataSet.</returns>
    public static DataSet ExecuteDataSet(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType, SqlCeTransaction transaction)
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

            return ds;
        }
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the data set operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="commandFactory">The command factory.</param>
    /// <returns>A DataSet.</returns>
    public static DataSet ExecuteDataSet(this SqlCeConnection @this, Action<SqlCeCommand> commandFactory)
    {
        using (SqlCeCommand command = @this.CreateCommand())
        {
            commandFactory(command);

            var ds = new DataSet();
            using (var dataAdapter = new SqlCeDataAdapter(command))
            {
                dataAdapter.Fill(ds);
            }

            return ds;
        }
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the data set operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <returns>A DataSet.</returns>
    public static DataSet ExecuteDataSet(this SqlCeConnection @this, string cmdText)
    {
        return @this.ExecuteDataSet(cmdText, null, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the data set operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A DataSet.</returns>
    public static DataSet ExecuteDataSet(this SqlCeConnection @this, string cmdText, SqlCeTransaction transaction)
    {
        return @this.ExecuteDataSet(cmdText, null, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the data set operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns>A DataSet.</returns>
    public static DataSet ExecuteDataSet(this SqlCeConnection @this, string cmdText, CommandType commandType)
    {
        return @this.ExecuteDataSet(cmdText, null, commandType, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the data set operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A DataSet.</returns>
    public static DataSet ExecuteDataSet(this SqlCeConnection @this, string cmdText, CommandType commandType, SqlCeTransaction transaction)
    {
        return @this.ExecuteDataSet(cmdText, null, commandType, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the data set operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <returns>A DataSet.</returns>
    public static DataSet ExecuteDataSet(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters)
    {
        return @this.ExecuteDataSet(cmdText, parameters, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the data set operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A DataSet.</returns>
    public static DataSet ExecuteDataSet(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, SqlCeTransaction transaction)
    {
        return @this.ExecuteDataSet(cmdText, parameters, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the data set operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns>A DataSet.</returns>
    public static DataSet ExecuteDataSet(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType)
    {
        return @this.ExecuteDataSet(cmdText, parameters, commandType, null);
    }
}
}
#endif
#if SQLSERVERCE
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
//using System;
//using System.Data;
////using System.Data.SqlServerCe;
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
#if SQLSERVERCE
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
//using System;
//using System.Collections.Generic;
//using System.Data;
////using System.Data.SqlServerCe;
//using Z.Data.SQLite;
namespace TestProj47 {
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
    public static IEnumerable<T> ExecuteEntities<T>(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType, SqlCeTransaction transaction) where T : new()
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
    public static IEnumerable<T> ExecuteEntities<T>(this SqlCeConnection @this, Action<SqlCeCommand> commandFactory) where T : new()
    {
        using (SqlCeCommand command = @this.CreateCommand())
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
    public static IEnumerable<T> ExecuteEntities<T>(this SqlCeConnection @this, string cmdText) where T : new()
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
    public static IEnumerable<T> ExecuteEntities<T>(this SqlCeConnection @this, string cmdText, SqlCeTransaction transaction) where T : new()
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
    public static IEnumerable<T> ExecuteEntities<T>(this SqlCeConnection @this, string cmdText, CommandType commandType) where T : new()
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
    public static IEnumerable<T> ExecuteEntities<T>(this SqlCeConnection @this, string cmdText, CommandType commandType, SqlCeTransaction transaction) where T : new()
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
    public static IEnumerable<T> ExecuteEntities<T>(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters) where T : new()
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
    public static IEnumerable<T> ExecuteEntities<T>(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, SqlCeTransaction transaction) where T : new()
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
    public static IEnumerable<T> ExecuteEntities<T>(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType) where T : new()
    {
        return @this.ExecuteEntities<T>(cmdText, parameters, commandType, null);
    }
}
}
#endif
#if SQLSERVERCE
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
//using System;
//using System.Data;
////using System.Data.SqlServerCe;
//using Z.Data.SQLite;
namespace TestProj47 {
public static partial class Extensions
{
    /// <summary>
    ///     A SqlCeConnection extension method that executes the entity operation.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A T.</returns>
    public static T ExecuteEntity<T>(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType, SqlCeTransaction transaction) where T : new()
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

            using (IDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                return reader.ToEntity<T>();
            }
        }
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the entity operation.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="commandFactory">The command factory.</param>
    /// <returns>A T.</returns>
    public static T ExecuteEntity<T>(this SqlCeConnection @this, Action<SqlCeCommand> commandFactory) where T : new()
    {
        using (SqlCeCommand command = @this.CreateCommand())
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
    ///     A SqlCeConnection extension method that executes the entity operation.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <returns>A T.</returns>
    public static T ExecuteEntity<T>(this SqlCeConnection @this, string cmdText) where T : new()
    {
        return @this.ExecuteEntity<T>(cmdText, null, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the entity operation.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A T.</returns>
    public static T ExecuteEntity<T>(this SqlCeConnection @this, string cmdText, SqlCeTransaction transaction) where T : new()
    {
        return @this.ExecuteEntity<T>(cmdText, null, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the entity operation.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns>A T.</returns>
    public static T ExecuteEntity<T>(this SqlCeConnection @this, string cmdText, CommandType commandType) where T : new()
    {
        return @this.ExecuteEntity<T>(cmdText, null, commandType, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the entity operation.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A T.</returns>
    public static T ExecuteEntity<T>(this SqlCeConnection @this, string cmdText, CommandType commandType, SqlCeTransaction transaction) where T : new()
    {
        return @this.ExecuteEntity<T>(cmdText, null, commandType, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the entity operation.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <returns>A T.</returns>
    public static T ExecuteEntity<T>(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters) where T : new()
    {
        return @this.ExecuteEntity<T>(cmdText, parameters, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the entity operation.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A T.</returns>
    public static T ExecuteEntity<T>(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, SqlCeTransaction transaction) where T : new()
    {
        return @this.ExecuteEntity<T>(cmdText, parameters, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the entity operation.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns>A T.</returns>
    public static T ExecuteEntity<T>(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType) where T : new()
    {
        return @this.ExecuteEntity<T>(cmdText, parameters, commandType, null);
    }
}
}
#endif
#if SQLSERVERCE
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
//using System;
//using System.Data;
////using System.Data.SqlServerCe;
//using Z.Data.SQLite;
namespace TestProj47 {
public static partial class Extensions
{
    /// <summary>
    ///     A SqlCeConnection extension method that executes the expando object operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A dynamic.</returns>
    public static dynamic ExecuteExpandoObject(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType, SqlCeTransaction transaction)
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

            using (IDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                return reader.ToExpandoObject();
            }
        }
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the expando object operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="commandFactory">The command factory.</param>
    /// <returns>A dynamic.</returns>
    public static dynamic ExecuteExpandoObject(this SqlCeConnection @this, Action<SqlCeCommand> commandFactory)
    {
        using (SqlCeCommand command = @this.CreateCommand())
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
    ///     A SqlCeConnection extension method that executes the expando object operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <returns>A dynamic.</returns>
    public static dynamic ExecuteExpandoObject(this SqlCeConnection @this, string cmdText)
    {
        return @this.ExecuteExpandoObject(cmdText, null, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the expando object operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A dynamic.</returns>
    public static dynamic ExecuteExpandoObject(this SqlCeConnection @this, string cmdText, SqlCeTransaction transaction)
    {
        return @this.ExecuteExpandoObject(cmdText, null, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the expando object operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns>A dynamic.</returns>
    public static dynamic ExecuteExpandoObject(this SqlCeConnection @this, string cmdText, CommandType commandType)
    {
        return @this.ExecuteExpandoObject(cmdText, null, commandType, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the expando object operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A dynamic.</returns>
    public static dynamic ExecuteExpandoObject(this SqlCeConnection @this, string cmdText, CommandType commandType, SqlCeTransaction transaction)
    {
        return @this.ExecuteExpandoObject(cmdText, null, commandType, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the expando object operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <returns>A dynamic.</returns>
    public static dynamic ExecuteExpandoObject(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters)
    {
        return @this.ExecuteExpandoObject(cmdText, parameters, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the expando object operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A dynamic.</returns>
    public static dynamic ExecuteExpandoObject(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, SqlCeTransaction transaction)
    {
        return @this.ExecuteExpandoObject(cmdText, parameters, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the expando object operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns>A dynamic.</returns>
    public static dynamic ExecuteExpandoObject(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType)
    {
        return @this.ExecuteExpandoObject(cmdText, parameters, commandType, null);
    }
}
}
#endif
#if SQLSERVERCE
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
//using System;
//using System.Collections.Generic;
//using System.Data;
////using System.Data.SqlServerCe;
//using Z.Data.SQLite;
namespace TestProj47 {
public static partial class Extensions
{
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
    public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType, SqlCeTransaction transaction)
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
    public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlCeConnection @this, Action<SqlCeCommand> commandFactory)
    {
        using (SqlCeCommand command = @this.CreateCommand())
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
    public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlCeConnection @this, string cmdText)
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
    public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlCeConnection @this, string cmdText, SqlCeTransaction transaction)
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
    public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlCeConnection @this, string cmdText, CommandType commandType)
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
    public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlCeConnection @this, string cmdText, CommandType commandType, SqlCeTransaction transaction)
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
    public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters)
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
    public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, SqlCeTransaction transaction)
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
    public static IEnumerable<dynamic> ExecuteExpandoObjects(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType)
    {
        return @this.ExecuteExpandoObjects(cmdText, parameters, commandType, null);
    }
}
}
#endif
#if SQLSERVERCE
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
//using System;
//using System.Data;
////using System.Data.SqlServerCe;
namespace TestProj47 {
public static partial class Extensions
{
    /// <summary>
    ///     A SqlCeConnection extension method that executes the non query operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    public static void ExecuteNonQuery(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType, SqlCeTransaction transaction)
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

            command.ExecuteNonQuery();
        }
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the non query operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="commandFactory">The command factory.</param>
    public static void ExecuteNonQuery(this SqlCeConnection @this, Action<SqlCeCommand> commandFactory)
    {
        using (SqlCeCommand command = @this.CreateCommand())
        {
            commandFactory(command);

            command.ExecuteNonQuery();
        }
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the non query operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    public static void ExecuteNonQuery(this SqlCeConnection @this, string cmdText)
    {
        @this.ExecuteNonQuery(cmdText, null, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the non query operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="transaction">The transaction.</param>
    public static void ExecuteNonQuery(this SqlCeConnection @this, string cmdText, SqlCeTransaction transaction)
    {
        @this.ExecuteNonQuery(cmdText, null, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the non query operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    public static void ExecuteNonQuery(this SqlCeConnection @this, string cmdText, CommandType commandType)
    {
        @this.ExecuteNonQuery(cmdText, null, commandType, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the non query operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    public static void ExecuteNonQuery(this SqlCeConnection @this, string cmdText, CommandType commandType, SqlCeTransaction transaction)
    {
        @this.ExecuteNonQuery(cmdText, null, commandType, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the non query operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    public static void ExecuteNonQuery(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters)
    {
        @this.ExecuteNonQuery(cmdText, parameters, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the non query operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="transaction">The transaction.</param>
    public static void ExecuteNonQuery(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, SqlCeTransaction transaction)
    {
        @this.ExecuteNonQuery(cmdText, parameters, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the non query operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    public static void ExecuteNonQuery(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType)
    {
        @this.ExecuteNonQuery(cmdText, parameters, commandType, null);
    }
}
}
#endif
#if SQLSERVERCE
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
//using System;
//using System.Data;
////using System.Data.SqlServerCe;
namespace TestProj47 {
public static partial class Extensions
{
    /// <summary>
    ///     A SqlCeConnection extension method that executes the reader operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A SqlCeDataReader.</returns>
    public static SqlCeDataReader ExecuteReader(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType, SqlCeTransaction transaction)
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

            return command.ExecuteReader();
        }
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the reader operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="commandFactory">The command factory.</param>
    /// <returns>A SqlCeDataReader.</returns>
    public static SqlCeDataReader ExecuteReader(this SqlCeConnection @this, Action<SqlCeCommand> commandFactory)
    {
        using (SqlCeCommand command = @this.CreateCommand())
        {
            commandFactory(command);

            return command.ExecuteReader();
        }
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the reader operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <returns>A SqlCeDataReader.</returns>
    public static SqlCeDataReader ExecuteReader(this SqlCeConnection @this, string cmdText)
    {
        return @this.ExecuteReader(cmdText, null, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the reader operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A SqlCeDataReader.</returns>
    public static SqlCeDataReader ExecuteReader(this SqlCeConnection @this, string cmdText, SqlCeTransaction transaction)
    {
        return @this.ExecuteReader(cmdText, null, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the reader operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns>A SqlCeDataReader.</returns>
    public static SqlCeDataReader ExecuteReader(this SqlCeConnection @this, string cmdText, CommandType commandType)
    {
        return @this.ExecuteReader(cmdText, null, commandType, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the reader operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A SqlCeDataReader.</returns>
    public static SqlCeDataReader ExecuteReader(this SqlCeConnection @this, string cmdText, CommandType commandType, SqlCeTransaction transaction)
    {
        return @this.ExecuteReader(cmdText, null, commandType, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the reader operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <returns>A SqlCeDataReader.</returns>
    public static SqlCeDataReader ExecuteReader(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters)
    {
        return @this.ExecuteReader(cmdText, parameters, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the reader operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A SqlCeDataReader.</returns>
    public static SqlCeDataReader ExecuteReader(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, SqlCeTransaction transaction)
    {
        return @this.ExecuteReader(cmdText, parameters, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the reader operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns>A SqlCeDataReader.</returns>
    public static SqlCeDataReader ExecuteReader(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType)
    {
        return @this.ExecuteReader(cmdText, parameters, commandType, null);
    }
}
}
#endif
#if SQLSERVERCE
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
//using System;
//using System.Data;
////using System.Data.SqlServerCe;
namespace TestProj47 {
public static partial class Extensions
{
    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>An object.</returns>
    public static object ExecuteScalar(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType, SqlCeTransaction transaction)
    {
        using (var command = @this.CreateCommand())
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
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="commandFactory">The command factory.</param>
    /// <returns>An object.</returns>
    public static object ExecuteScalar(this SqlCeConnection @this, Action<SqlCeCommand> commandFactory)
    {
        using (var command = @this.CreateCommand())
        {
            commandFactory(command);

            return command.ExecuteScalar();
        }
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <returns>An object.</returns>
    public static object ExecuteScalar(this SqlCeConnection @this, string cmdText)
    {
        return @this.ExecuteScalar(cmdText, null, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>An object.</returns>
    public static object ExecuteScalar(this SqlCeConnection @this, string cmdText, SqlCeTransaction transaction)
    {
        return @this.ExecuteScalar(cmdText, null, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns>An object.</returns>
    public static object ExecuteScalar(this SqlCeConnection @this, string cmdText, CommandType commandType)
    {
        return @this.ExecuteScalar(cmdText, null, commandType, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>An object.</returns>
    public static object ExecuteScalar(this SqlCeConnection @this, string cmdText, CommandType commandType, SqlCeTransaction transaction)
    {
        return @this.ExecuteScalar(cmdText, null, commandType, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <returns>An object.</returns>
    public static object ExecuteScalar(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters)
    {
        return @this.ExecuteScalar(cmdText, parameters, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>An object.</returns>
    public static object ExecuteScalar(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, SqlCeTransaction transaction)
    {
        return @this.ExecuteScalar(cmdText, parameters, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns>An object.</returns>
    public static object ExecuteScalar(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType)
    {
        return @this.ExecuteScalar(cmdText, parameters, commandType, null);
    }
}
}
#endif
#if SQLSERVERCE
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
//using System;
//using System.Data;
////using System.Data.SqlServerCe;
namespace TestProj47 {
public static partial class Extensions
{
    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarAs<T>(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType, SqlCeTransaction transaction)
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

            return (T) command.ExecuteScalar();
        }
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="commandFactory">The command factory.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarAs<T>(this SqlCeConnection @this, Action<SqlCeCommand> commandFactory)
    {
        using (SqlCeCommand command = @this.CreateCommand())
        {
            commandFactory(command);

            return (T) command.ExecuteScalar();
        }
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarAs<T>(this SqlCeConnection @this, string cmdText)
    {
        return @this.ExecuteScalarAs<T>(cmdText, null, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarAs<T>(this SqlCeConnection @this, string cmdText, SqlCeTransaction transaction)
    {
        return @this.ExecuteScalarAs<T>(cmdText, null, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarAs<T>(this SqlCeConnection @this, string cmdText, CommandType commandType)
    {
        return @this.ExecuteScalarAs<T>(cmdText, null, commandType, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarAs<T>(this SqlCeConnection @this, string cmdText, CommandType commandType, SqlCeTransaction transaction)
    {
        return @this.ExecuteScalarAs<T>(cmdText, null, commandType, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarAs<T>(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters)
    {
        return @this.ExecuteScalarAs<T>(cmdText, parameters, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarAs<T>(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, SqlCeTransaction transaction)
    {
        return @this.ExecuteScalarAs<T>(cmdText, parameters, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarAs<T>(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType)
    {
        return @this.ExecuteScalarAs<T>(cmdText, parameters, commandType, null);
    }
}
}
#endif
#if SQLSERVERCE
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
//using System;
//using System.Data;
////using System.Data.SqlServerCe;
//using Z.Data.SQLite;
namespace TestProj47 {
public static partial class Extensions
{
    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarTo<T>(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType, SqlCeTransaction transaction)
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

            return command.ExecuteScalar().To<T>();
        }
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="commandFactory">The command factory.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarTo<T>(this SqlCeConnection @this, Action<SqlCeCommand> commandFactory)
    {
        using (SqlCeCommand command = @this.CreateCommand())
        {
            commandFactory(command);

            return command.ExecuteScalar().To<T>();
        }
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarTo<T>(this SqlCeConnection @this, string cmdText)
    {
        return @this.ExecuteScalarTo<T>(cmdText, null, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarTo<T>(this SqlCeConnection @this, string cmdText, SqlCeTransaction transaction)
    {
        return @this.ExecuteScalarTo<T>(cmdText, null, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarTo<T>(this SqlCeConnection @this, string cmdText, CommandType commandType)
    {
        return @this.ExecuteScalarTo<T>(cmdText, null, commandType, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarTo<T>(this SqlCeConnection @this, string cmdText, CommandType commandType, SqlCeTransaction transaction)
    {
        return @this.ExecuteScalarTo<T>(cmdText, null, commandType, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarTo<T>(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters)
    {
        return @this.ExecuteScalarTo<T>(cmdText, parameters, CommandType.Text, null);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarTo<T>(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, SqlCeTransaction transaction)
    {
        return @this.ExecuteScalarTo<T>(cmdText, parameters, CommandType.Text, transaction);
    }

    /// <summary>
    ///     A SqlCeConnection extension method that executes the scalar operation.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="cmdText">The command text.</param>
    /// <param name="parameters">Options for controlling the operation.</param>
    /// <param name="commandType">Type of the command.</param>
    /// <returns>An object.</returns>
    public static T ExecuteScalarTo<T>(this SqlCeConnection @this, string cmdText, SqlCeParameter[] parameters, CommandType commandType)
    {
        return @this.ExecuteScalarTo<T>(cmdText, parameters, commandType, null);
    }
}
}
#endif
#if SQLSERVERCE
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
//using System.Collections.Generic;
////using System.Data.SqlServerCe;
namespace TestProj47 {
public static partial class Extensions
{
    /// <summary>
    ///     A SqlParameterCollection extension method that adds a range with value to 'values'.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="values">The values.</param>
    public static void AddRangeWithValue(this SqlCeParameterCollection @this, Dictionary<string, object> values)
    {
        foreach (var keyValuePair in values)
        {
            @this.AddWithValue(keyValuePair.Key, keyValuePair.Value);
        }
    }
}
}
#endif// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Reflection;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Enumerates to entities in this collection.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an IEnumerable&lt;T&gt;</returns>
        public static IEnumerable<T> ToEntitiesSqLite<T>(this IDataReader @this) where T : new()
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
    }
} // Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Reflection;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     An IDataReader extension method that converts the @this to an entity.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a T.</returns>
        public static T ToEntitySqLite<T>(this IDataReader @this) where T : new()
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
    }
} // Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System.Collections.Generic;
//using System.Data;
//using System.Dynamic;
//using System.Linq;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     An IDataReader extension method that converts the @this to an expando object.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a dynamic.</returns>
        public static dynamic ToExpandoObject2(this IDataReader @this)
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
    }
} // Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System.Collections.Generic;
//using System.Data;
//using System.Dynamic;
//using System.Linq;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Enumerates to expando objects in this collection.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as an IEnumerable&lt;dynamic&gt;</returns>
        public static IEnumerable<dynamic> ToExpandoObjects2(this IDataReader @this)
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
} // Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     An object extension method that cast anonymous type to the specified type.
        /// </summary>
        /// <typeparam name="T">Generic type parameter. The specified type.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The object as the specified type.</returns>
        public static T As2<T>(this object @this)
        {
            return (T) @this;
        }
    }
} // Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System;
//using System.ComponentModel;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A System.Object extension method that toes the given this.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">this.</param>
        /// <returns>A T.</returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_To
        ///           {
        ///               [TestMethod]
        ///               public void To()
        ///               {
        ///                   string nullValue = null;
        ///                   string value = &quot;1&quot;;
        ///                   object dbNullValue = DBNull.Value;
        /// 
        ///                   // Exemples
        ///                   var result1 = value.To&lt;int&gt;(); // return 1;
        ///                   var result2 = value.To&lt;int?&gt;(); // return 1;
        ///                   var result3 = nullValue.To&lt;int?&gt;(); // return null;
        ///                   var result4 = dbNullValue.To&lt;int?&gt;(); // return null;
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(1, result2.Value);
        ///                   Assert.IsFalse(result3.HasValue);
        ///                   Assert.IsFalse(result4.HasValue);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_To
        ///           {
        ///               [TestMethod]
        ///               public void To()
        ///               {
        ///                   string nullValue = null;
        ///                   string value = &quot;1&quot;;
        ///                   object dbNullValue = DBNull.Value;
        /// 
        ///                   // Exemples
        ///                   var result1 = value.To&lt;int&gt;(); // return 1;
        ///                   var result2 = value.To&lt;int?&gt;(); // return 1;
        ///                   var result3 = nullValue.To&lt;int?&gt;(); // return null;
        ///                   var result4 = dbNullValue.To&lt;int?&gt;(); // return null;
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(1, result2.Value);
        ///                   Assert.IsFalse(result3.HasValue);
        ///                   Assert.IsFalse(result4.HasValue);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        public static T To2<T>(this object @this)
        {
            if (@this != null)
            {
                var targetType = typeof(T);

                if (@this.GetType() == targetType)
                {
                    return (T) @this;
                }

                var converter = TypeDescriptor.GetConverter(@this);
                if (converter != null)
                {
                    if (converter.CanConvertTo(targetType))
                    {
                        return (T) converter.ConvertTo(@this, targetType);
                    }
                }

                converter = TypeDescriptor.GetConverter(targetType);
                if (converter != null)
                {
                    if (converter.CanConvertFrom(@this.GetType()))
                    {
                        return (T) converter.ConvertFrom(@this);
                    }
                }

                if (@this == DBNull.Value)
                {
                    return (T) (object) null;
                }
            }

            return (T) @this;
        }

        /// <summary>
        ///     A System.Object extension method that toes the given this.
        /// </summary>
        /// <param name="this">this.</param>
        /// <param name="type">The type.</param>
        /// <returns>An object.</returns>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        /// 
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_To
        ///           {
        ///               [TestMethod]
        ///               public void To()
        ///               {
        ///                   string nullValue = null;
        ///                   string value = &quot;1&quot;;
        ///                   object dbNullValue = DBNull.Value;
        /// 
        ///                   // Exemples
        ///                   var result1 = value.To&lt;int&gt;(); // return 1;
        ///                   var result2 = value.To&lt;int?&gt;(); // return 1;
        ///                   var result3 = nullValue.To&lt;int?&gt;(); // return null;
        ///                   var result4 = dbNullValue.To&lt;int?&gt;(); // return null;
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(1, result2.Value);
        ///                   Assert.IsFalse(result3.HasValue);
        ///                   Assert.IsFalse(result4.HasValue);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// <example>
        ///     <code>
        ///       using System;
        ///       using Microsoft.VisualStudio.TestTools.UnitTesting;
        ///       using Z.ExtensionMethods.Object;
        /// 
        ///       namespace ExtensionMethods.Examples
        ///       {
        ///           [TestClass]
        ///           public class System_Object_To
        ///           {
        ///               [TestMethod]
        ///               public void To()
        ///               {
        ///                   string nullValue = null;
        ///                   string value = &quot;1&quot;;
        ///                   object dbNullValue = DBNull.Value;
        /// 
        ///                   // Exemples
        ///                   var result1 = value.To&lt;int&gt;(); // return 1;
        ///                   var result2 = value.To&lt;int?&gt;(); // return 1;
        ///                   var result3 = nullValue.To&lt;int?&gt;(); // return null;
        ///                   var result4 = dbNullValue.To&lt;int?&gt;(); // return null;
        /// 
        ///                   // Unit Test
        ///                   Assert.AreEqual(1, result1);
        ///                   Assert.AreEqual(1, result2.Value);
        ///                   Assert.IsFalse(result3.HasValue);
        ///                   Assert.IsFalse(result4.HasValue);
        ///               }
        ///           }
        ///       }
        /// </code>
        /// </example>
        /// ###
        /// <typeparam name="T">Generic type parameter.</typeparam>
        public static object To2(this object @this, Type type)
        {
            if (@this != null)
            {
                var targetType = type;

                if (@this.GetType() == targetType)
                {
                    return @this;
                }

                var converter = TypeDescriptor.GetConverter(@this);
                if (converter != null)
                {
                    if (converter.CanConvertTo(targetType))
                    {
                        return converter.ConvertTo(@this, targetType);
                    }
                }

                converter = TypeDescriptor.GetConverter(targetType);
                if (converter != null)
                {
                    if (converter.CanConvertFrom(@this.GetType()))
                    {
                        return converter.ConvertFrom(@this);
                    }
                }

                if (@this == DBNull.Value)
                {
                    return null;
                }
            }

            return @this;
        }
    }
}