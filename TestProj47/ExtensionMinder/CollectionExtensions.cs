// Decompiled with JetBrains decompiler
// Type: ExtensionMinder.CollectionExtensions
// Assembly: ExtensionMinder, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9895DAEA-F52E-4F0C-B5FD-FB311AEFF61C
// Assembly location: ...\bin\Debug\ExtensionMinder.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HSNXT
{
    public static partial class Extensions
    {
        [DebuggerStepThrough]
        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) return null;
            // ReSharper disable PossibleMultipleEnumeration
            foreach (var obj in source)
                action(obj);
            return source;
            // ReSharper restore PossibleMultipleEnumeration
        }

        public static TSource MaxBy1<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            return source.MaxBy(selector, Comparer<TKey>.Default);
        }

        public static TSource MaxBy1<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector,
            IComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));
            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    throw new InvalidOperationException("Sequence contains no elements");
                var source1 = enumerator.Current;
                var y = selector(source1);
                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current;
                    var x = selector(current);
                    if (comparer.Compare(x, y) > 0)
                    {
                        source1 = current;
                        y = x;
                    }
                }
                return source1;
            }
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> Map<T>(this IEnumerable<T> source, Func<T, T> action)
        {
            return source.Select(action);
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> GetDuplicates<T>(this IEnumerable<T> source)
        {
            return source.GroupBy(item => item).Where(g => g.Count() > 1).Select(g => g.Key);
        }

        [DebuggerStepThrough]
        public static bool In<T>(this T source, IEnumerable<T> list)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            return list.Contains(source);
        }

        [DebuggerStepThrough]
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> enumerable)
        {
            foreach (var obj in enumerable)
                collection.Add(obj);
        }

        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }

        public static IEnumerable<IList<object>> AsTableWith(this IEnumerable first, params IEnumerable[] lists)
        {
            var iterator1 = first.GetEnumerator();
            var enumerators = lists.Select(x => x.GetEnumerator()).ToList();
            while (iterator1.MoveNext())
            {
                var result = new List<object>
                {
                    iterator1.Current
                };
                enumerators.Each(x =>
                {
                    x.MoveNext();
                    result.Add(x.Current);
                });
                yield return result;
            }
        }
    }
}