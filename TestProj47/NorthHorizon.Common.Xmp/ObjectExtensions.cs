using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using HSNXT.aResources;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        /// Executes the specified action if <paramref name="obj"/> is of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The desired type.</typeparam>
        /// <param name="obj">The target object.</param>
        /// <param name="action">The action to perform.</param>
        /// <returns>Whether or not the action was able to be performed.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj")]
        public static bool As<T>(this object obj, Action<T> action) where T : class
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (!(obj is T target))
                return false;

            action(target);
            return true;
        }

        /// <summary>
        /// Executes the specified action if <paramref name="obj"/> is of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The desired type.</typeparam>
        /// <param name="obj">The target object.</param>
        /// <param name="action">The action to perform.</param>
        /// <returns>Whether or not the action was able to be performed.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj")]
        public static bool AsValueType<T>(this object obj, Action<T> action) where T : struct
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (!(obj is T variable)) return false;
            action(variable);
            return true;
        }

        /// <summary>
        /// Gets a specified property in a chain of member accesses, checking for null at each node.
        /// </summary>
        /// <typeparam name="TRoot">The type of the root object.</typeparam>
        /// <typeparam name="TValue">The type of the final node.</typeparam>
        /// <param name="root">The target object</param>
        /// <param name="getExpression">The expression representing the member access chain.</param>
        /// <returns>The value of the target member or <code>default(TValue)</code></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static TValue ChainGet<TRoot, TValue>(this TRoot root, Expression<Func<TRoot, TValue>> getExpression)
        {
            return ChainGet(root, getExpression, out _);
        }

        /// <summary>
        /// Gets a specified property in a chain of member accesses, checking for null at each node.
        /// </summary>
        /// <typeparam name="TRoot">The type of the root object.</typeparam>
        /// <typeparam name="TValue">The type of the final node.</typeparam>
        /// <param name="root">The target object</param>
        /// <param name="getExpression">The expression representing the member access chain.</param>
        /// <param name="success">Whether or not the chain was completely evaluated.</param>
        /// <returns>The value of the target member or <code>default(TValue)</code></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"),
         SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters"),
         SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#")]
        public static TValue ChainGet<TRoot, TValue>(this TRoot root, Expression<Func<TRoot, TValue>> getExpression,
            out bool success)
        {
            // it's ok if root is null!

            if (getExpression == null)
                throw new ArgumentNullException(nameof(getExpression));

            var members = new Stack<MemberAccessInfo>();

            var expr = getExpression.Body;
            while (expr != null)
            {
                if (expr.NodeType == ExpressionType.Parameter)
                    break;

                if (!(expr is MemberExpression memberExpr))
                    throw new ArgumentException(Resource1.Extensions_ChainGet_Given_expression_is_not_a_member_access_chain,
                        nameof(getExpression));

                members.Push(new MemberAccessInfo(memberExpr.Member));

                expr = memberExpr.Expression;
            }

            object node = root;
            foreach (var member in members)
            {
                if (node == null)
                {
                    success = false;
                    return default;
                }

                node = member.GetValue(node);
            }

            success = true;
            return (TValue) node;
        }

        private class MemberAccessInfo
        {
            private readonly PropertyInfo _propertyInfo;
            private readonly FieldInfo _fieldInfo;

            public MemberAccessInfo(MemberInfo info)
            {
                _propertyInfo = info as PropertyInfo;
                _fieldInfo = info as FieldInfo;
            }

            public object GetValue(object target)
            {
                if (_propertyInfo != null)
                    return _propertyInfo.GetValue(target, null);
                else if (_fieldInfo != null)
                    return _fieldInfo.GetValue(target);
                else
                    throw new InvalidOperationException();
            }
        }
    }
}