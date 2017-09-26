// Decompiled with JetBrains decompiler
// Type: DevLib.ExtensionMethods.CollectionExtensions
// Assembly: DevLib.ExtensionMethods, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\DevLib.ExtensionMethods.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DevLib.ExtensionMethods
{
  /// <summary>Collection Extensions.</summary>
  public static class CollectionExtensions
  {
    /// <summary>Check Type inherit IEnumerable interface or not.</summary>
    /// <param name="source">Source Type.</param>
    /// <returns>true if the source Type inherit IEnumerable interface; otherwise, false.</returns>
    public static bool IsEnumerable(this Type source)
    {
      if (source != typeof (string))
        return source.GetInterface("IEnumerable") != null;
      return false;
    }

    /// <summary>Check Type inherit IDictionary interface or not.</summary>
    /// <param name="source">Source Type.</param>
    /// <returns>true if the source Type inherit IDictionary interface; otherwise, false.</returns>
    public static bool IsDictionary(this Type source)
    {
      return source.GetInterface("IDictionary") != null;
    }

    /// <summary>
    /// Gets the element Type of the specified type which inherit IEnumerable interface.
    /// </summary>
    /// <param name="source">Source Type which inherit IEnumerable interface.</param>
    /// <returns>The System.Type of the element in the source list.</returns>
    public static Type GetEnumerableElementType(this Type source)
    {
      if (source.GetInterface("IEnumerable") == null)
        return (Type) null;
      if (!source.IsArray)
        return source.GetGenericArguments()[0];
      return source.GetElementType();
    }

    /// <summary>Update value, if not contain key then add value.</summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    /// <param name="source">The source dictionary.</param>
    /// <param name="sourceKey">The key of the element to update.</param>
    /// <param name="sourceValue">The value of element to update.</param>
    public static void Update<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey sourceKey, TValue sourceValue)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if ((object) sourceKey == null)
        throw new ArgumentNullException(nameof (sourceKey));
      if (source.ContainsKey(sourceKey))
        source[sourceKey] = sourceValue;
      else
        source.Add(sourceKey, sourceValue);
    }

    /// <summary>
    /// Performs the specified action on each element of the specified IDictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the collection.</typeparam>
    /// <typeparam name="TValue">The type of values in the collection.</typeparam>
    /// <param name="source">The source dictionary.</param>
    /// <param name="action">Method for element.</param>
    public static void ForEach<TKey, TValue>(this IDictionary<TKey, TValue> source, Action<TKey, TValue> action)
    {
      if (source == null || source.Count == 0 || action == null)
        return;
      foreach (KeyValuePair<TKey, TValue> keyValuePair in (IEnumerable<KeyValuePair<TKey, TValue>>) source)
        action(keyValuePair.Key, keyValuePair.Value);
    }

    /// <summary>
    /// Gets the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="key">The key of the value to get.</param>
    /// <returns>The value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter.</returns>
    public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
    {
      TValue obj;
      source.TryGetValue(key, out obj);
      return obj;
    }

    /// <summary>
    /// Adds the elements of the specified dictionary to the source.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="dictionary">The dictionary to add.</param>
    /// <param name="forceUpdate">true to update the source dictionary if the key exists; otherwise, ignore the value.</param>
    public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> dictionary, bool forceUpdate = true)
    {
      foreach (KeyValuePair<TKey, TValue> keyValuePair in (IEnumerable<KeyValuePair<TKey, TValue>>) dictionary)
      {
        if (forceUpdate || !source.ContainsKey(keyValuePair.Key))
          source[keyValuePair.Key] = keyValuePair.Value;
      }
    }

    /// <summary>
    /// Gets the value associated with the specified key, if the key is found; otherwise, create a value, add it to the source and return it.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="key">The key.</param>
    /// <param name="builder">The function to create a value.</param>
    /// <returns>The value associated with the specified key, if the key is found; otherwise, the new value created.</returns>
    public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, Func<TValue> builder)
    {
      TValue obj;
      if (!source.TryGetValue(key, out obj))
      {
        obj = builder();
        source.Add(key, obj);
      }
      return obj;
    }

    /// <summary>Determines whether a sequence is null or empty.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">Source IEnumerable.</param>
    /// <returns>true if the source sequence is empty; otherwise, false.</returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
    {
      if (source != null)
        return source.Count<T>() == 0;
      return true;
    }

    /// <summary>Determines whether a sequence is NOT null NOR empty.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">Source IEnumerable.</param>
    /// <returns>true if the source sequence is NOT empty; otherwise, false.</returns>
    public static bool IsNotNullNorEmpty<T>(this IEnumerable<T> source)
    {
      if (source != null)
        return source.Count<T>() > 0;
      return false;
    }

    /// <summary>
    /// Returns an empty enumeration of the same type if source is null; otherwise, return source itself.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">Source IEnumerable.</param>
    /// <returns>An empty enumeration of the same type if source is null; otherwise, source itself.</returns>
    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
    {
      return source ?? Enumerable.Empty<T>();
    }

    /// <summary>Returns an enumeration of the specified source type.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">The source.</param>
    /// <returns>An enumeration of the source type </returns>
    public static IEnumerable<T> Enumerate<T>(this T source)
    {
      yield return source;
    }

    /// <summary>
    /// Searches for all elements that matches the conditions defined by the specified predicate, and returns the zero-based index of the all occurrence within the entire System.Collections.Generic.List{T}.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="source">Source IEnumerable{T}.</param>
    /// <param name="match">The System.Predicate{T} delegate that defines the conditions of the element to search for.</param>
    /// <returns>A list of the zero-based index of the all occurrence of elements that matches the conditions defined by match, if found; otherwise, empty list.</returns>
    public static List<int> FindAllIndex<T>(this IEnumerable<T> source, Predicate<T> match)
    {
      List<int> intList = new List<int>();
      if (source == null)
        return intList;
      int num = 0;
      foreach (T obj in source)
      {
        if (match(obj))
          intList.Add(num);
        ++num;
      }
      return intList;
    }

    /// <summary>
    /// Searches for all elements that matches the conditions defined by the specified predicate, and returns the zero-based index of the all occurrence within the entire System.Collections.Generic.List{T}.
    /// </summary>
    /// <param name="source">Source IEnumerable.</param>
    /// <param name="predicate">The delegate that defines the conditions of the element to search for.</param>
    /// <returns>A list of the zero-based index of the all occurrence of elements that matches the conditions defined by match, if found; otherwise, empty list.</returns>
    public static List<int> FindAllIndex(this IEnumerable source, Func<object, bool> predicate)
    {
      List<int> intList = new List<int>();
      if (source == null)
        return intList;
      int num = 0;
      foreach (object obj in source)
      {
        if (predicate(obj))
          intList.Add(num);
        ++num;
      }
      return intList;
    }

    /// <summary>
    /// Returns a list of IEnumerable{T} that contains the sub collection of source that are delimited by elements of a specified predicate.
    /// The elements of each sub collection are started with the specified predicate element.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="source">Source IEnumerable{T}.</param>
    /// <param name="match">The System.Predicate{T} delegate that defines the conditions of the element to search for.</param>
    /// <returns>A list of IEnumerable{T} that contains the sub collection of source.</returns>
    public static List<List<T>> SplitByStartsWith<T>(this IEnumerable<T> source, Predicate<T> match)
    {
      List<List<T>> objListList = new List<List<T>>();
      List<int> allIndex = source.FindAllIndex<T>(match);
      if (allIndex.Count < 1)
      {
        objListList.Add(source.ToList<T>());
        return objListList;
      }
      int count1 = 0;
      foreach (int num in allIndex)
      {
        if (num != 0)
        {
          int count2 = num - count1;
          objListList.Add(source.Skip<T>(count1).Take<T>(count2).ToList<T>());
          count1 = num;
        }
      }
      objListList.Add(source.Skip<T>(count1).ToList<T>());
      return objListList;
    }

    /// <summary>
    /// Returns a list of IEnumerable that contains the sub collection of source that are delimited by elements of a specified predicate.
    /// The elements of each sub collection are started with the specified predicate element.
    /// </summary>
    /// <param name="source">Source IEnumerable.</param>
    /// <param name="predicate">The delegate that defines the conditions of the element to search for.</param>
    /// <returns>A list of IEnumerable that contains the sub collection of source.</returns>
    public static List<List<object>> SplitByStartsWith(this IEnumerable source, Func<object, bool> predicate)
    {
      List<List<object>> objectListList = new List<List<object>>();
      List<int> allIndex = source.FindAllIndex(predicate);
      IEnumerable<object> source1 = source.Cast<object>();
      if (allIndex.Count < 1)
      {
        objectListList.Add(source1.ToList<object>());
        return objectListList;
      }
      int count1 = 0;
      foreach (int num in allIndex)
      {
        if (num != 0)
        {
          int count2 = num - count1;
          objectListList.Add(source1.Skip<object>(count1).Take<object>(count2).ToList<object>());
          count1 = num;
        }
      }
      objectListList.Add(source1.Skip<object>(count1).ToList<object>());
      return objectListList;
    }

    /// <summary>
    /// Returns a list of IEnumerable{T} that contains the sub collection of source that are delimited by elements of a specified predicate.
    /// The elements of each sub collection are ended with the specified predicate element.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="source">Source IEnumerable{T}.</param>
    /// <param name="match">The System.Predicate{T} delegate that defines the conditions of the element to search for.</param>
    /// <returns>A list of IEnumerable{T} that contains the sub collection of source.</returns>
    public static List<List<T>> SplitByEndsWith<T>(this IEnumerable<T> source, Predicate<T> match)
    {
      List<List<T>> objListList = new List<List<T>>();
      List<int> allIndex = source.FindAllIndex<T>(match);
      if (allIndex.Count < 1)
      {
        objListList.Add(source.ToList<T>());
        return objListList;
      }
      int count1 = 0;
      foreach (int num in allIndex)
      {
        int count2 = num + 1 - count1;
        objListList.Add(source.Skip<T>(count1).Take<T>(count2).ToList<T>());
        count1 = num + 1;
      }
      if (count1 < source.Count<T>())
        objListList.Add(source.Skip<T>(count1).ToList<T>());
      return objListList;
    }

    /// <summary>
    /// Returns a list of IEnumerable that contains the sub collection of source that are delimited by elements of a specified predicate.
    /// The elements of each sub collection are ended with the specified predicate element.
    /// </summary>
    /// <param name="source">Source IEnumerable.</param>
    /// <param name="predicate">The delegate that defines the conditions of the element to search for.</param>
    /// <returns>A list of IEnumerable that contains the sub collection of source.</returns>
    public static List<List<object>> SplitByEndsWith(this IEnumerable source, Func<object, bool> predicate)
    {
      List<List<object>> objectListList = new List<List<object>>();
      List<int> allIndex = source.FindAllIndex(predicate);
      IEnumerable<object> source1 = source.Cast<object>();
      if (allIndex.Count < 1)
      {
        objectListList.Add(source1.ToList<object>());
        return objectListList;
      }
      int count1 = 0;
      foreach (int num in allIndex)
      {
        int count2 = num + 1 - count1;
        objectListList.Add(source1.Skip<object>(count1).Take<object>(count2).ToList<object>());
        count1 = num + 1;
      }
      if (count1 < source1.Count<object>())
        objectListList.Add(source1.Skip<object>(count1).ToList<object>());
      return objectListList;
    }

    /// <summary>
    /// Returns a list of IEnumerable{T} that contains the sub collection of source that are split by fix size.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="source">Source IEnumerable{T}.</param>
    /// <param name="batchSize">The size of each group item.</param>
    /// <returns>A list of IEnumerable{T} that contains the sub collection of source.</returns>
    public static List<List<T>> GroupBatch<T>(this IEnumerable<T> source, int batchSize)
    {
      if (source == null)
        return (List<List<T>>) null;
      List<List<T>> objListList = new List<List<T>>();
      int num = source.Count<T>();
      if (num < 1)
        return objListList;
      if (batchSize < 1)
      {
        objListList.Add(source.ToList<T>());
        return objListList;
      }
      int count = 0;
      do
      {
        objListList.Add(source.Skip<T>(count).Take<T>(batchSize).ToList<T>());
        count += batchSize;
        num -= batchSize;
      }
      while (num > 0);
      return objListList;
    }

    /// <summary>
    /// Returns a list of IEnumerable that contains the sub collection of source that are split by fix size.
    /// </summary>
    /// <param name="source">Source IEnumerable.</param>
    /// <param name="batchSize">The size of each group item.</param>
    /// <returns>A list of IEnumerable that contains the sub collection of source.</returns>
    public static List<List<object>> GroupBatch(this IEnumerable source, int batchSize)
    {
      if (source == null)
        return (List<List<object>>) null;
      List<List<object>> objectListList = new List<List<object>>();
      IEnumerable<object> source1 = source.Cast<object>();
      int num = source1.Count<object>();
      if (num < 1)
        return objectListList;
      if (batchSize < 1)
      {
        objectListList.Add(source1.ToList<object>());
        return objectListList;
      }
      int count = 0;
      do
      {
        objectListList.Add(source1.Skip<object>(count).Take<object>(batchSize).ToList<object>());
        count += batchSize;
        num -= batchSize;
      }
      while (num > 0);
      return objectListList;
    }

    /// <summary>
    /// Determines whether the source collection is a subset of the specified superset.
    /// </summary>
    /// <typeparam name="T">The type of the item in collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="superset">The superset collection.</param>
    /// <returns>true if the source collection is a subset of the specified superset; otherwise, false.</returns>
    public static bool IsSubsetOf<T>(this IEnumerable<T> source, IEnumerable<T> superset)
    {
      return source.All<T>((Func<T, bool>) (subsetItem => superset.Any<T>((Func<T, bool>) (supersetItem => supersetItem.Equals((object) subsetItem)))));
    }

    /// <summary>
    /// Determines whether the source collection is superset of the specified subset.
    /// </summary>
    /// <typeparam name="T">The type of the item in collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="subset">The subset collection.</param>
    /// <returns>true if the source collection is superset of the specified subset; otherwise, false.</returns>
    public static bool IsSupersetOf<T>(this IEnumerable<T> source, IEnumerable<T> subset)
    {
      return subset.All<T>((Func<T, bool>) (subsetItem => source.Any<T>((Func<T, bool>) (supersetItem => supersetItem.Equals((object) subsetItem)))));
    }
  }
}
