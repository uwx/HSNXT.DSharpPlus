using System;
using System.Collections.Generic;
using System.Linq;

namespace HSNXT
{
    public partial class Extensions
    {

        /// <summary>
        /// Take a collection, and returns it if it has items, or returns empty collection.
        /// </summary>
        /// <typeparam name="TItem">The type contained in the collection</typeparam>
        /// <param name="items">The collection to check</param>
        /// <returns>null if null or empty, otherwise the original collection</returns>
        public static IEnumerable<TItem> OrEmptyIfNull<TItem>(this IEnumerable<TItem> items)
        {
            if (items == null) return Enumerable.Empty<TItem>();
            return items;
        }

        /// <summary>
        /// Take a collection, and returns it if it has items, or returns null.
        /// </summary>
        /// <typeparam name="TItem">The type contained in the collection</typeparam>
        /// <param name="items">The collection to check</param>
        /// <returns>null if null or empty, otherwise the original collection</returns>
        public static IEnumerable<TItem> OrNullIfEmpty<TItem>(this IEnumerable<TItem> items)
        {
            var isEmpty = IsNullOrEmpty(items);

            if (isEmpty) return null;

            return items;
        }

        /// <summary>
        /// Take two enumerables and performs a full outer join using the given key selectors. The resulting
        /// set of pairs is processed using the given <paramref name="resultSelector"/> func.
        /// Note that this function gives no sort guarantees.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left-hand enumeration</typeparam>
        /// <typeparam name="TRight">The type of the right-hand enumeration</typeparam>
        /// <typeparam name="TKey">The join key type</typeparam>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="left">The left-hand enumeration</param>
        /// <param name="right">The right-hand enumeration</param>
        /// <param name="leftKeySelector">The left-hand key selector</param>
        /// <param name="rightKeySelector">The right-hand key selector</param>
        /// <param name="resultSelector">The result selector</param>
        /// <param name="keyEqualityComparer">The key equality comparer to use (optional)</param>
        /// <returns>The complete full outer join of the two input enumerations</returns>
        public static IEnumerable<TResult> FullOuterJoin<TLeft, TRight, TKey, TResult>(this IEnumerable<TLeft> left,
                                                                                       IEnumerable<TRight> right,
                                                                                       Func<TLeft, TKey> leftKeySelector,
                                                                                       Func<TRight, TKey> rightKeySelector,
                                                                                       Func<TLeft, TRight, TResult> resultSelector,
                                                                                       IEqualityComparer<TKey> keyEqualityComparer = null)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));
            if (leftKeySelector == null) throw new ArgumentNullException(nameof(leftKeySelector));
            if (rightKeySelector == null) throw new ArgumentNullException(nameof(rightKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            if (keyEqualityComparer == null)
            {
                keyEqualityComparer = EqualityComparer<TKey>.Default;
            }

            var allKeys = new HashSet<TKey>(keyEqualityComparer);

            var leftLookup = left.ToLookup(e => { var key = leftKeySelector(e); allKeys.Add(key); return key; }, keyEqualityComparer);
            var rightLookup = right.ToLookup(e => { var key = rightKeySelector(e); allKeys.Add(key); return key; }, keyEqualityComparer);

            foreach (var key in allKeys)
            {
                var leftValues = leftLookup[key].DefaultIfEmpty().ToArray();
                var rightValues = rightLookup[key].DefaultIfEmpty().ToArray();

                foreach (var l in leftValues)
                {
                    foreach (var r in rightValues)
                    {
                        yield return resultSelector(l, r);
                    }
                }
            }
        }
    }
}
