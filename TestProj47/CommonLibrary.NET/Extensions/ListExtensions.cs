using System.Collections.Generic;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        /// AddRange of items of same type to IList 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="itemsToAdd"></param>
        public static IList<T> AddRange<T>(this IList<T> items, IList<T> itemsToAdd)
        {
            if (items == null || itemsToAdd == null)
                return items;

            foreach (var item in itemsToAdd)
                items.Add(item);

            return items;
        }
    }
}
