using System;
using System.Collections.Generic;
using System.Linq;

namespace HSNXT
{
    public static class ListExtensions
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if (items == null)
                return;

            list.AddRange(items.Where(predicate));
        }

        public static void AddRange<TList, TItem>(this IList<TList> list, IEnumerable<TItem> items,
            Converter<TItem, TList> converter)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));
            if (items == null)
                return;

            list.AddRange(items.Select(item => converter(item)));
        }

        public static void AddRange<TList, TItem>(this IList<TList> list, IEnumerable<TItem> items,
            Func<TItem, bool> predicate,
            Converter<TItem, TList> converter)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));
            if (items == null)
                return;

            list.AddRange(items.Where(predicate).Select(item => converter(item)));
        }

        public static int IndexOf<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (var i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                    return i;
            }
            return -1;
        }

        public static IEnumerable<int> IndexOfAll<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (var i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                    yield return i;
            }
        }

        public static int LastIndexOf<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (predicate(list[i]))
                    return i;
            }
            return -1;
        }

        public static bool Remove<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (var i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public static int RemoveAll<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var count = 0;
            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                    count++;
                }
            }
            return count;
        }

        public static TOutput[] ToArray<TInput, TOutput>(this IEnumerable<TInput> source,
            Converter<TInput, TOutput> converter)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            var array = new TOutput[source.Count()];
            var counter = 0;
            foreach (var item in source)
                array[counter++] = converter(item);
            return array;
        }

        public static T[] ToArray<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return source.Where(predicate).ToArray();
        }

        public static TOutput[] ToArray<TInput, TOutput>(this IEnumerable<TInput> source, Func<TInput, bool> predicate,
            Converter<TInput, TOutput> converter)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            return source.Where(predicate).Select(item => converter(item)).ToArray();
        }
    }
}