// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSNXT.Z.Data.Test
{
    [TestClass]
    public class System_Data_IDataRecord_IndexOf
    {
        [TestMethod]
        public void IndexOf()
        {
            const string sql = @"
SELECT 1 AS A
";
            //// Examples
            //using (var conn = new SqlConnection(My.Config.ConnectionString.UnitTest.ConnectionString))
            //{
            //    using (SqlCommand @this = conn.CreateCommand())
            //    {
            //        conn.Open();
            //        @this.CommandText = sql;
            //        using (SqlDataReader reader = @this.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                int result = reader.IndexOf("A"); // return 0;

            //                // UnitTest
            //                Assert.AreEqual(0, result);
            //            }
            //        }
            //    }
            //}
        }
    }
}