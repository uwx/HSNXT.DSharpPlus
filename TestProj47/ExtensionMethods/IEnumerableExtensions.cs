// Decompiled with JetBrains decompiler
// Type: ExtensionMethods.IEnumerableExtensions
// Assembly: ExtensionMethods, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A78E21D-808B-4A21-BFAA-D781C27D2CD5
// Assembly location: ...\bin\Debug\ExtensionMethods.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using HSNXT.Helpers;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        /// Creates a Collection&lt;T&gt; from an IEnumerable&lt;T&gt;.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The IEnumerable&lt;T&gt; to create a Collection&lt;T&gt; from.</param>
        /// <returns></returns>
        /// <exception cref="T:System.ArgumentNullException">source is null.</exception>
        public static Collection<TSource> ToCollection<TSource>(this IEnumerable<TSource> source)
        {
            // ReSharper disable PossibleMultipleEnumeration
            Check.NotNull(source, nameof(source));
            if (source is IList<TSource> list)
                return new Collection<TSource>(list);
            var collection = new Collection<TSource>();
            foreach (var source1 in source)
                collection.Add(source1);
            return collection;
        }
    }
}