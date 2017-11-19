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
using System.Linq.Expressions;
using System.Reflection;

namespace HSNXT.ComLib
{
    /// <summary>
    /// This class provides helper expression methods.
    /// </summary>
    public class ExpressionHelper
    {        

        /// <summary>
        /// Get the property name from the expression.
        /// e.g. GetPropertyName(Person)( p => p.FirstName);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string GetPropertyName<T>(Expression<Func<T, object>> exp)
        {
            MemberExpression memberExpression = null;

            // Get memberexpression.
            if (exp.Body is MemberExpression)
                memberExpression = exp.Body as MemberExpression;

            if (exp.Body is UnaryExpression)
            {
                var unaryExpression = exp.Body as UnaryExpression;
                if (unaryExpression.Operand is MemberExpression)
                    memberExpression = unaryExpression.Operand as MemberExpression;
            }

            if (memberExpression == null)
                throw new InvalidOperationException("Not a member access.");

            var info = memberExpression.Member as PropertyInfo;
            return info.Name;
        }


        /// <summary>
        /// Get the property name from the expression.
        /// e.g. GetPropertyName(Person)( p => p.FirstName);
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string GetPropertyName(Expression<Func<object>> exp)
        {
            var memberExpression = exp.Body as MemberExpression;
            if (memberExpression == null) throw new InvalidOperationException("Not a member access.");

            var info = memberExpression.Member as PropertyInfo;
            return info.Name;
        }


        /// <summary>
        /// Get the property name from the expression.
        /// e.g. GetPropertyName(Person)( p => p.FirstName);
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static object GetPropertyNameAndValue(Expression<Func<object>> exp, ref string propName)
        {
            var memberExpression = exp.Body as MemberExpression;
            PropertyInfo propInfo = null;
            if (exp.Body is MemberExpression)
            {
                propInfo = ((MemberExpression)exp.Body).Member as PropertyInfo;
            }
            else if (exp.Body is UnaryExpression)
            {
                var op = ((UnaryExpression)exp.Body).Operand;
                propInfo = ((MemberExpression)op).Member as PropertyInfo;
            }

            var val = exp.Compile().DynamicInvoke();
            propName = propInfo.Name;
            return val;
        }


        /// <summary>
        /// Delete using the expression.
        /// e.g. entity.LogLevel == 1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        static void Build<T>(Expression<Func<T, bool>> predicate)
        {
            var format = "{0} {1} {2}";
            var exp = (BinaryExpression)predicate.Body;
            var left = (MemberExpression)exp.Left;

            var fieldName = left.Member.Name;
            var operand = RepositoryExpressionTypeHelper.GetText(exp.NodeType);
            var fieldVal = "";
            object rval = null;

            if (exp.Right is ConstantExpression)
            {
                var right = (ConstantExpression)exp.Right;
                fieldVal = RepositoryExpressionValueHelper.GetVal(right.Value);
            }
            else if (exp.Right is MemberExpression)
            {
                var right = (MemberExpression)exp.Right;
                rval = Expression.Lambda(exp.Right).Compile().DynamicInvoke();
            }

            // Check for data types.
            var propInfo = left.Member as PropertyInfo;

            // String ? Encode single quotes. ' = ''
            if (propInfo.PropertyType == typeof(string))
            {
                fieldVal = $"'{fieldVal.Replace("'", "''")}'";
            }
            else if (propInfo.PropertyType == typeof(DateTime))
            {
                fieldVal = "'" + ((DateTime)rval).ToShortDateString() + "'";
            }

            var val = string.Format(format, fieldName, operand, fieldVal);
            Console.WriteLine(val);
        }
    }


    class RepositoryExpressionValueHelper
    {
        public static string GetVal(object val)
        {
            if (val.GetType() == typeof(bool) || val.GetType() == typeof(Boolean))
            {
                var boolVal = (bool)val;
                return boolVal ? "1" : "0";
            }
            return val.ToString();
        }
    }


    class RepositoryExpressionTypeHelper
    {
        private static readonly IDictionary<ExpressionType, string> _map = new Dictionary<ExpressionType, string>();

        static RepositoryExpressionTypeHelper()
        {
            _map[ExpressionType.Equal] = "=";
            _map[ExpressionType.NotEqual] = "<>";
            _map[ExpressionType.GreaterThanOrEqual] = ">=";
            _map[ExpressionType.LessThanOrEqual] = "<=";
            _map[ExpressionType.LessThan] = "<";
            _map[ExpressionType.GreaterThan] = ">";
        }


        /// <summary>
        /// Get the sql text equivalent of the expression type.
        /// </summary>
        /// <param name="expType"></param>
        /// <returns></returns>
        public static string GetText(ExpressionType expType)
        {
            if (!_map.ContainsKey(expType))
                throw new ArgumentException("expresion type :" + expType + "not supported.");

            return _map[expType];
        }
    }
}
