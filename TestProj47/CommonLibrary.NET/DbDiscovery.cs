#if NetFX
/*
 * Author: Kishore Reddy
 * Url: http://commonlibrarynet.codeplex.com/
 * Title: CommonLibrary.NET
 * Copyright: � 2009 Kishore Reddy
 * License: LGPL License
 * LicenseUrl: http://commonlibrarynet.codeplex.com/license
 * Description: A C# based .NET 3.5 Open-Source collection of reusable components.
 * Usage: Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Data;
using System.IO;
using System.Text;

namespace HSNXT.ComLib.Data
{    
    /// <summary>
    /// TO_DO:
    /// Class containing various methods for discovering various objects in a database.
    /// eg.
    /// 1. Find tables
    /// 2. Find schemas
    /// 3. Find stored procedures
    /// 4. Find Views
    /// </summary>
    public class DBSchema : Database
    {
        /// <summary>
        /// Default construction
        /// </summary>
        public DBSchema() { }


        /// <summary>
        /// Initialize using connection.
        /// </summary>
        /// <param name="connection"></param>
        public DBSchema(ConnectionInfo connection)
            : base(connection)
        {
        }


        /// <summary>
        /// Drop the table from the database.
        /// </summary>
        /// <param name="tableName"></param>
        public void DropTable(string tableName)
        {
            var checkDelete = GetDropTable(tableName, false);
            this.ExecuteNonQuery(checkDelete, CommandType.Text, null);
        }


        /// <summary>
        /// Creates a DROP TABLE statement preceeded
        /// by an optional corresponding IF EXISTS statement.
        /// </summary>
        /// <param name="tableName">Name of table to use.</param>
        /// <param name="includeGo">True to generate IF EXISTS statement.</param>
        /// <returns>Generated string.</returns>
        public string GetDropTable(string tableName, bool includeGo)
        {
            // IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
            // DROP TABLE [dbo].[Categories]
            var checkDelete = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + tableName + "]') AND type in (N'U'))";
            checkDelete += Environment.NewLine + "DROP TABLE [dbo].[" + tableName + "]";
            if( includeGo)
                checkDelete += Environment.NewLine + "go";
            return checkDelete;
        }


        /// <summary>
        /// Get the drop command for the procedure giving the table name and procedure name.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="procName"></param>
        /// <returns></returns>
        public string GetDropProc(string tableName, string procName)
        {
            // IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
            // DROP TABLE [dbo].[Categories]
            var dropCommand = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + tableName +"_" + procName + "]') AND type in (N'P', N'PC'))" + Environment.NewLine
                    + "DROP PROCEDURE [dbo].[" + tableName + "_" + procName + "]";

            return dropCommand;
        }
       


        /// <summary>
        /// Outputs all schemas to a file.
        /// </summary>
        public void GetTables()
        {
            WriteSchema("all", "", null);
            WriteSchema("tables", "Tables", null);
            WriteSchema("columns", "Columns", null);
            WriteSchema("columns_cat", "Columns", new[] { "WorkshopsProd2", null, "wk_Categories", null });
            WriteSchema("restrictions", "Restrictions",null);
        }



        private void WriteSchema(string fileName, string schemaName, string[] restrictionValues)
        {
            DataTable table = null;
            using (var con = GetConnection())
            {
                con.Open();
                if (!string.IsNullOrEmpty(schemaName))
                    table = con.GetSchema(schemaName, restrictionValues);
                else
                    table = con.GetSchema();
            }
            var schemaFileName = string.IsNullOrEmpty(schemaName) ? "schema" : schemaName;
            table.WriteXml("f:/tests/" + fileName + ".xml");
                        
            var buffer = new StringBuilder();
            foreach (DataColumn col in table.Columns)
            {
                buffer.Append("Column: name = " + col.ColumnName + " type = " + col.DataType + Environment.NewLine);
            }
            for (var ndx = 0; ndx < table.Rows.Count; ndx++)
            {
                buffer.Append("Record: " + ndx + " ");
                for (var col = 0; col < table.Columns.Count; col++)
                {
                    buffer.Append(table.Rows[ndx][col] + ", ");
                }
                buffer.Append(Environment.NewLine);
            }
            var info = buffer.ToString();
            File.WriteAllText("f:/tests/" + fileName + "_data.xml", info);
                      
        }
    }
}
#endif