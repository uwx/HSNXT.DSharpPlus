using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using HSNXT.aResources;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        /// Executes the specified action with each item as the parameter.
        /// </summary>
        /// <param name="collection">The target collection</param>
        /// <param name="action">The action to perform.</param>
        public static void ForEach(this IEnumerable collection, Action<object> action)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (var item in collection)
                action(item);
        }

        /// <summary>
        /// Determines whether the given enumerable has a number of items.
        /// </summary>
        /// <param name="source">The enumerable to query.</param>
        /// <param name="countExpression">An expression declaring the desired number of items.</param>
        /// <returns>Whether the enumerable has the desired number of items.</returns>
        /// <remarks>
        /// This extension method supports the following operators:
        /// <list type="bullet">
        ///     <item><c>==</c></item>
        ///     <item><c>!=</c></item>
        ///     <item><c><![CDATA[<]]></c></item>
        ///     <item><c><![CDATA[<=]]></c></item>
        ///     <item><c><![CDATA[>]]></c></item>
        ///     <item><c><![CDATA[>=]]></c></item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <code><![CDATA[list.Has(count => count > 15)]]></code>
        /// <code>><![CDATA[list.Has(count => x < count)]]></code>
        /// </example>
        public static bool Has<T>(this IEnumerable<T> source, Expression<Func<long, bool>> countExpression)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (countExpression == null) throw new ArgumentNullException(nameof(countExpression));

            long? leftBoundOffset, rightBoundOffset;

            if (!(countExpression.Body is BinaryExpression operation))
                throw new ArgumentException(Resource1.InvalidExpression, nameof(countExpression));

            bool isInverted = false, isNegated = false;
            Expression valueExpression;

            var parameterExpression = countExpression.Parameters.Single();

            // we assume c => c < x. If they give us c => x < c, we need to eval the LHS and set a flag.
            if (operation.Left == parameterExpression)
                valueExpression = operation.Right;
            else if (operation.Right != parameterExpression)
                valueExpression = operation.Left;
            else
                throw new ArgumentException(Resource1.CountParameterMissing, nameof(countExpression));

            // translate to v + delta_l < c < v + delta_r
            switch (countExpression.Body.NodeType)
            {
                case ExpressionType.NotEqual:
                    isNegated = true;
                    goto case ExpressionType.Equal;

                case ExpressionType.Equal:
                    leftBoundOffset = -1;
                    rightBoundOffset = 1;
                    break;

                case ExpressionType.GreaterThan:
                    if (isInverted)
                    {
                        isInverted = false;
                        goto case ExpressionType.LessThan;
                    }

                    leftBoundOffset = 0;
                    rightBoundOffset = null;
                    break;

                case ExpressionType.GreaterThanOrEqual:
                    if (isInverted)
                    {
                        isInverted = false;
                        goto case ExpressionType.LessThanOrEqual;
                    }

                    leftBoundOffset = -1;
                    rightBoundOffset = null;
                    break;

                case ExpressionType.LessThan:
                    if (isInverted)
                    {
                        isInverted = false;
                        goto case ExpressionType.GreaterThan;
                    }

                    leftBoundOffset = null;
                    rightBoundOffset = 0;
                    break;

                case ExpressionType.LessThanOrEqual:
                    if (isInverted)
                    {
                        isInverted = false;
                        goto case ExpressionType.GreaterThanOrEqual;
                    }

                    leftBoundOffset = null;
                    rightBoundOffset = 1;
                    break;

                default:
                    throw new ArgumentException(Resource1.InvalidExpression, nameof(countExpression));
            }

            long value;

            try
            {
                value = (long) Expression.Lambda(valueExpression).Compile().DynamicInvoke();
            }
            catch (InvalidOperationException error)
            {
                throw new ArgumentException(Resource1.ErrorExecutingCountComparisonValueExpression,
                    nameof(countExpression), error);
            }

            var leftBound = value + leftBoundOffset;
            var rightBound = value + rightBoundOffset;

            using (var enumerator = source.GetEnumerator())
            {
                long count = 0;

                if (leftBound.HasValue)
                    // this determines leftBound < count
                    while (count <= leftBound)
                    {
                        if (!enumerator.MoveNext())
                            return isNegated;

                        count++;
                    }

                if (rightBound.HasValue)
                {
                    // this determines count < rightBound
                    while (count < rightBound)
                    {
                        if (!enumerator.MoveNext())
                            return !isNegated;

                        count++;
                    }

                    return isNegated;
                }

                return true;
            }
        }

        /// <summary>
        /// Yields a new list with an item added to the end.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="collection">The target collection</param>
        /// <param name="item">The item to append.</param>
        /// <returns>A new list with <paramref name="item"/> added to the end.</returns>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> collection, T item)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (item == null)
                throw new ArgumentNullException(nameof(item));

            foreach (var colItem in collection)
                yield return colItem;

            yield return item;
        }

        /// <summary>
        /// Yields a new list with an item added to the beginning.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="collection">The target collection</param>
        /// <param name="item">The item to prepend.</param>
        /// <returns>A new list with <paramref name="item"/> added to the beginning.</returns>
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> collection, T item)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (item == null)
                throw new ArgumentNullException(nameof(item));

            yield return item;

            foreach (var colItem in collection)
                yield return colItem;
        }

        /// <summary>
        /// Returns the specified collection as an <see cref="ObservableCollection{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="collection">The target collection</param>
        /// <returns>
        /// An <see cref="ObservableCollection{T}"/> with all of the items
        /// in <paramref name="collection"/>. This is not necessarily a new object.
        /// </returns>
        public static ObservableCollection<T> AsObservable<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            return collection as ObservableCollection<T> ?? new ObservableCollection<T>(collection);
        }

        /// <summary>
        /// Returns the specified collection as an <see cref="HashSet{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="collection">The target collection</param>
        /// <returns>
        /// An <see cref="HashSet{T}"/> with all of the items
        /// in <paramref name="collection"/>. This is not necessarily a new object.
        /// </returns>
        public static HashSet<T> AsHashSet<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            return collection as HashSet<T> ?? new HashSet<T>(collection);
        }

        /// <summary>
        /// Gets the argument that produces the maximum value 
        /// yielded from the specified function.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <typeparam name="TValue">The type of the value yielded from the specified function.</typeparam>
        /// <param name="collection">The target collection.</param>
        /// <param name="function">The function used to produce values.</param>
        /// <returns>The argument that produces the highest value.</returns>
        public static T ArgMax<T, TValue>(this IEnumerable<T> collection, Func<T, TValue> function)
            where TValue : IComparable<TValue>
        {
            return ArgComp(collection, function, GreaterThan);
        }

        private static bool GreaterThan<T>(T first, T second) where T : IComparable<T>
        {
            return first.CompareTo(second) > 0;
        }

        /// <summary>
        /// Gets the argument that produces the minimum value 
        /// yielded from the specified function.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <typeparam name="TValue">The type of the value yielded from the specified function.</typeparam>
        /// <param name="collection">The target collection.</param>
        /// <param name="function">The function used to produce values.</param>
        /// <returns>The argument that produces the least value.</returns>
        public static T ArgMin<T, TValue>(this IEnumerable<T> collection, Func<T, TValue> function)
            where TValue : IComparable<TValue>
        {
            return ArgComp(collection, function, LessThan);
        }

        private static bool LessThan<T>(T first, T second) where T : IComparable<T>
        {
            return first.CompareTo(second) < 0;
        }

        private static T ArgComp<T, TValue>(IEnumerable<T> collection, Func<T, TValue> function,
            Func<TValue, TValue, bool> accept)
            where TValue : IComparable<TValue>
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (function == null)
                throw new ArgumentNullException(nameof(function));

            var isSet = false;
            var maxArg = default(T);
            var maxValue = default(TValue);

            foreach (var item in collection)
            {
                var value = function(item);
                if (!isSet || accept(value, maxValue))
                {
                    maxArg = item;
                    maxValue = value;
                    isSet = true;
                }
            }

            return maxArg;
        }
    }
}