#if MYSQL
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright (c) ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System.Data;
using MySql.Data.MySqlClient;

namespace TestProj47 {
public static partial class Extensions
{
    /// <summary>
    ///     Executes the query, and returns the first result set as DataTable.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>A DataTable that is equivalent to the first result set.</returns>
    public static DataTable ExecuteDataTable(this MySqlCommand @this)
    {
        var dt = new DataTable();
        using (var dataAdapter = new MySqlDataAdapter(@this))
        {
            dataAdapter.Fill(dt);
        }

        return dt;
    }
}
}
#endif