using System.Collections.Generic;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        /// Gets string of all items in the collection.
        /// </summary>
        public static string ToString<T>(this IList<T> collection, char seperator = ',')
        {
            return $"{collection.GetType().Name} {{ {string.Join($" {seperator} ", collection)} }}";
        }
    }
}
