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
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using HSNXT.ComLib.Data;
using HSNXT.ComLib.Models;
using HSNXT.ComLib.Reflection;

namespace HSNXT.ComLib.OrmLite
{
    /// <summary>
    /// Interface to be implemented by
    /// ORM lite builder classes.
    /// </summary>
    public interface IOrmLiteBuilder
    {
        /// <summary>
        /// Get/set the entity to use.
        /// </summary>
        object Entity { get; set; }


        /// <summary>
        /// Creates Insert statements for all entity properties.
        /// </summary>
        /// <returns></returns>
        string BuildInsert();


        /// <summary>
        /// Creates Update statements for all entity properties.
        /// </summary>
        /// <returns></returns>
        string BuildUpdate();


        /// <summary>
        /// Returns statement that retrieves id of inserted entity.
        /// </summary>
        string IdentityRetrievalStatement { get; }
    }


    /// <summary>
    /// This class provides an implementation of a lightweight ORM builder.
    /// </summary>
    public class OrmLiteBuilder : IOrmLiteBuilder
    {
        #region IOrmLiteBuilder Members
        
        /// <summary>
        /// Entity to build sql insert/update for.
        /// </summary>
        public object Entity { get; set; }


        /// <summary>
        /// Generate an very generic single table insert statement that includes all the properties
        /// of the entity.
        /// </summary>
        /// <returns>String with Insert statement.</returns>
        public virtual string BuildInsert()
        {
            var entity = Entity;
            // Get all properties
            var props = ReflectionUtils.GetAllProperties(entity, null);

            var bufferIns = new StringBuilder();
            var bufferVals = new StringBuilder();
            bufferIns.Append(string.Format("INSERT INTO {0}s (" + entity.GetType().Name));
            bufferIns.Append("[" + props[0].Name + "] ");
            bufferVals.Append(" VALUES (" + GetValue(entity, props[0]));

            for(var ndx = 1; ndx < props.Count; ndx++)
            {
                var prop = props[ndx];
                
                // Add to insert.
                bufferIns.Append($", [{prop.Name}]");

                // Add to values.
                var val = GetValue(entity, prop);
                bufferVals.Append(", " + val);
            }
            bufferIns.Append(")");
            bufferVals.Append(")");
            bufferIns.Append(bufferVals);
            bufferIns.Append(" " + this.IdentityRetrievalStatement);
            return bufferIns.ToString();
        }


        /// <summary>
        /// Generate a generic single table update statement that includes
        /// all the properties of the entity.
        /// </summary>
        /// <returns>String with Update statement.</returns>
        public string BuildUpdate()
        {
            var entity = Entity;
            // Get all properties
            var props = ReflectionUtils.GetAllProperties(entity, null);

            var id = Convert.ToInt32(ReflectionUtils.GetPropertyValue(entity, "Id"));
            var buffer = new StringBuilder();
            buffer.Append(string.Format("UPDATE {0}s (" + entity.GetType().Name));
            buffer.Append(" SET [" + props[0].Name + "] = " + GetValue(entity, props[0]));

            for (var ndx = 1; ndx < props.Count; ndx++)
            {
                var prop = props[ndx];
                var val = GetValue(entity, prop);
                buffer.Append($", [{prop.Name}] = {val}");
            }
            buffer.Append(" where Id = " + id + ";");
            return buffer.ToString();
        }


        /// <summary>
        /// Provide the statement for getting the inserted entity id(sql identity).
        /// </summary>
        public virtual string IdentityRetrievalStatement => "select scope_identity();";

        #endregion


        /// <summary>
        /// Gets the value of the entity's property via reflection
        /// and converts the value to appropriatedly escaped string value.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="prop">The prop.</param>
        /// <returns>Value of the property.</returns>
        protected string GetValue(object entity, PropertyInfo prop)
        {
            // String.
            if (prop.PropertyType == typeof(string))
            {
                var val = prop.GetValue(entity, null) as string;
                return string.Format("'{0}'" + DataUtils.Encode(val));
            }
            // DateTime.
            if (prop.PropertyType == typeof(DateTime))
            {
                var date = (DateTime)prop.GetValue(entity, null);
                return $"'{date.ToString("yyyy-MM-dd")}'";
            }
            // Bool
            if (prop.PropertyType == typeof(bool))
            {
                var boolVal = (bool)prop.GetValue(entity, null);
                return boolVal ? "1" : "0";
            }
            // Numeric
            if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(long)
                || prop.PropertyType == typeof(float) || prop.PropertyType == typeof(double)
                || prop.PropertyType == typeof(decimal))
            {
                var number = Convert.ToDouble(prop.GetValue(entity, null));
                return number.ToString();
            }
            return string.Empty;
        }
    }



    /// <summary>
    /// Orm sql builder.
    /// </summary>
    public abstract class OrmSqlStaticBuilder
    {
        /// <summary>
        /// TYpe of database provider.
        /// </summary>
        protected string _databaseType;

        
        /// <summary>
        /// Context information for generation.
        /// </summary>
        protected ModelContext _ctx;


        /// <summary>
        /// Model name.
        /// </summary>
        protected string _modelName;


        /// <summary>
        /// Table name.
        /// </summary>
        protected string _tableName;


        /// <summary>
        /// Number of tabs to use after a new line.
        /// </summary>
        protected int _tabCount;


        /// <summary>
        /// Concantenated tab characters.
        /// </summary>
        protected string _tab;


        /// <summary>
        /// Unitialized instance.
        /// </summary>
        public OrmSqlStaticBuilder() { }


        /// <summary>
        /// Initialize this instance.
        /// </summary>
        /// <param name="ctx">Context with generation information.</param>
        /// <param name="modelName">Name of model.</param>
        /// <param name="tableName">Name of table to target.</param>
        /// <param name="tabCount">Number of tabs after each new line.</param>
        public OrmSqlStaticBuilder(ModelContext ctx, string modelName, string tableName, int tabCount)
        {
            Init( ctx, modelName, tableName, tabCount);
        }            
            
        
        /// <summary>
        /// Initialize this instance.
        /// </summary>
        /// <param name="ctx">Context with generation information.</param>
        /// <param name="modelName">Name of model.</param>
        /// <param name="tableName">Name of table to target.</param>
        /// <param name="tabCount">Number of tabs after each new line.</param>
        public void Init(ModelContext ctx, string modelName, string tableName, int tabCount)
        {
            _tabCount = tabCount;
            _ctx = ctx;
            _modelName = modelName;
            _tableName = string.IsNullOrEmpty(tableName) ? modelName + "s" : tableName;

            for (var ndx = 0; ndx < _tabCount; ndx++)
                _tab += "\t";
        }


        /// <summary>
        /// Generate the sql for insert.
        /// </summary>
        /// <returns>String with Insert statement.</returns>
        public virtual string Insert()
        {
            var buffer = new StringBuilder();
            var columnNames = new List<string>();
            var columnValues = new List<string>();
            SetupColumns(columnNames, columnValues);

            var sql = "\"insert into " + _tableName + " ( [" + columnNames[0] + "]";
            for (var ndx = 1; ndx < columnNames.Count; ndx++)
            {
                sql += ", [" + columnNames[ndx] + "]";
                if (ndx % 5 == 0)
                {
                    sql += "\" + " + Environment.NewLine + _tab + "\"";
                }
            }            
            sql += ") \" + " + Environment.NewLine + " \"VALUES (\" + " + columnValues[0];
            for (var ndx = 1; ndx < columnValues.Count; ndx++)
            {
                sql += " + \",\" + " + columnValues[ndx];
                if (ndx % 4 == 0) sql +=  Environment.NewLine + _tab;
            }
            sql += "+ \");\" +  IdentityStatement;";

            return sql;
        }


        /// <summary>
        /// Provide the statement for getting the inserted entity id(sql identity).
        /// </summary>
        public abstract string IdentityRetrievalStatement { get; }


        /// <summary>
        /// Generate the sql for update.
        /// </summary>
        /// <returns>String with Update statement.</returns>
        public virtual string Update()
        {
            var buffer = new StringBuilder();
            var columnNames = new List<string>();
            var columnValues = new List<string>();

            SetupColumns(columnNames, columnValues);

            var sql = "\"update " + _tableName + $" set [{columnNames[0]}] = \" + {columnValues[0]} + ";
            for (var ndx = 1; ndx < columnValues.Count; ndx++)
            {
                sql += $"\", [{columnNames[ndx]}] = \" + {columnValues[ndx]} + ";
                if (ndx % 5 == 0) sql += Environment.NewLine + _tab;
            }

            sql += " \" where Id = \" + entity.Id;";
            return sql;
        }


        /// <summary>
        /// Setup the columns.
        /// </summary>
        /// <param name="columnNames">List with the column names.</param>
        /// <param name="columnValues">List with the column values.</param>
        public void SetupColumns(List<string> columnNames, List<string> columnValues)
        {
            Process((objProp, model, prop) =>
            {
                columnNames.Add(prop.ColumnName);
                var objPropName = model.IsComposite ? "entity." + objProp + "." : "entity.";
                columnValues.Add(GetValue(objPropName, prop));
            });
        }


        /// <summary>
        /// Process the model, by handling all it's properties, compositions, and includes and calling
        /// the delegate given  to handle the properties.
        /// </summary>
        /// <param name="propHandler">Delegate to handle the properties.</param>
        public virtual void Process(Action<string, Model, PropInfo> propHandler)
        {
            var iterator = new ModelIterator();
            
            // Now add the columns of the model itself.
            iterator.OnPropertyProcess += (ctx, model, prop) =>
            {
                var objProp = string.Empty;
                if (model.Name != _modelName && model.IsComposite)
                    objProp = model.Name;

                if (prop.Name != "Id")
                {
                    propHandler(objProp, model, prop);
                }
                return true;
            };
            
            // Now add the columns of the composite model
            iterator.OnCompositeProcess += (ctx, model, composition) =>
            {
                composition.ModelRef.IsComposite = true;
                iterator.ProcessModel(ctx, composition.ModelRef);
                return true;
            };

            // Now add the columns of the composite model
            iterator.OnIncludeProcess += (ctx, model, include) =>
            {
                iterator.ProcessModel(ctx, include.ModelRef);
                return true;
            };
            iterator.Process(_ctx, _modelName);
        }


        /// <summary>
        /// Get the value.
        /// </summary>
        /// <param name="objectPropName">Property name.</param>
        /// <param name="prop">Instance of property info.</param>
        /// <returns>Property value.</returns>
        public virtual string GetValue(string objectPropName, PropInfo prop)
        {
            if (prop.DataType == typeof(string) || prop.DataType == typeof(StringClob))
                return $"\"'\" + DataUtils.Encode({objectPropName + prop.Name}) + \"'\"";

            if (prop.DataType == typeof(DateTime))
                return "\"'\" + " + objectPropName + prop.Name + ".ToString(\"yyyy-MM-dd\") + \"'\"";

            if (prop.DataType == typeof(bool))
                return " Convert.ToSByte(" + objectPropName + prop.Name + ") ";

            if (prop.DataType == typeof(int) || prop.DataType == typeof(long)
                || prop.DataType == typeof(float) || prop.DataType == typeof(double))
                return " " + objectPropName + prop.Name + " ";

            return " " + objectPropName + prop.Name + " ";
        }
    }


    /// <summary>
    /// This is a software factory to generate an instance of 
    /// ORM SQL static builder based on a provider name.
    /// 
    /// Currently, only an ORM SQL builder for System.Data.SqlClient
    /// is supported and returned by default.
    /// </summary>
    public class OrmSqlStaticBuilderFactory
    {
        /// <summary>
        /// Return an instance of an ORM SQL builder
        /// based on a database provider name.
        /// </summary>
        /// <param name="providerName">Name of database provider.</param>
        /// <returns>Instance of ORM SQL builder.</returns>
        /// <remarks>Currently, an instance of OrmSqlStaticBuilderSqlServer
        /// is always returned.</remarks>
        public static OrmSqlStaticBuilder GetBuilder(string providerName)
        {
            return new OrmSqlStaticBuilderSqlServer();
        }
    }



    /// <summary>
    /// Sql builder for Sql Server.
    /// </summary>
    public class OrmSqlStaticBuilderSqlServer : OrmSqlStaticBuilder
    {
        /// <summary>
        /// Unitialized instance.
        /// </summary>
        public OrmSqlStaticBuilderSqlServer() { } 


        /// <summary>
        /// Initialize this instance.
        /// </summary>
        /// <param name="ctx">Context with generation information.</param>
        /// <param name="modelName">Name of model.</param>
        /// <param name="tableName">Name of table to target.</param>
        /// <param name="tabCount">Number of tabs after each new line.</param>
        public OrmSqlStaticBuilderSqlServer(ModelContext ctx, string modelName, string tableName, int tabCount)
            : base(ctx, modelName, tableName, tabCount)
        {
            _databaseType = "System.Data.SqlClient";
        }


        /// <summary>
        /// Provide the statement for getting the inserted entity id(sql identity).
        /// </summary>
        public override string IdentityRetrievalStatement => "select scope_identity();";
    }
}
