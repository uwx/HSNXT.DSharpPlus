// Decompiled with JetBrains decompiler
// Type: TestProj47.ArrayExtensions
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        /// Performs the specified action on each element of the specified array.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="source">Source array.</param>
        /// <param name="action">Method for element.</param>
        /// <param name="throwOnError">true to throw any exception that occurs.-or- false to ignore any exception that occurs.</param>
        // ReSharper disable once MethodOverloadWithOptionalParameter
        public static void ForEach<T>(this T[] source, Action<T> action, bool throwOnError = false)
        {
            if (source == null)
            {
                if (throwOnError)
                    throw new ArgumentNullException(nameof(source));
            }
            else if (action == null)
            {
                if (throwOnError)
                    throw new ArgumentNullException(nameof(action));
            }
            else
            {
                foreach (var t in source)
                {
                    try
                    {
                        action(t);
                    }
                    catch
                    {
                        if (throwOnError)
                            throw;
                    }
                }
            }
        }

        /// <summary>
        /// Adds all elements of the suffixArray to the end of the sourceArray.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="suffixArray">The array whose elements should be added to the end of the sourceArray.</param>
        /// <param name="sourceArray">The target array.</param>
        /// <param name="useDeepClone">Whether use deep clone of the element in suffixArray.</param>
        public static void AddRangeTo<T>(this T[] suffixArray, ref T[] sourceArray, bool useDeepClone = false)
        {
            if (suffixArray == null || suffixArray.Length == 0)
                return;
            if (sourceArray == null)
            {
                var length = suffixArray.Length;
                sourceArray = new T[length];
                if (useDeepClone)
                {
                    for (var index = 0; index < length; ++index)
                        sourceArray[index] = suffixArray[index].CloneDeep();
                }
                else
                    Array.Copy(suffixArray, sourceArray, length);
            }
            else
            {
                var length1 = sourceArray.Length;
                var length2 = suffixArray.Length;
                Array.Resize(ref sourceArray, length1 + length2);
                if (useDeepClone)
                {
                    for (var index = 0; index < length2; ++index)
                        sourceArray[index + length1] = suffixArray[index].CloneDeep();
                }
                else
                    suffixArray.CopyTo(sourceArray, length1);
            }
        }

        /// <summary>
        /// Find the first occurrence of value type array in another value type array.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="source">The array to search in.</param>
        /// <param name="pattern">The array to find.</param>
        /// <returns>The first position of the found array or -1 if not found.</returns>
        public static int FindArrayIndex<T>(this T[] source, T[] pattern)
            where T : IComparable, IConvertible, IEquatable<T>
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (pattern == null)
                throw new ArgumentNullException(nameof(pattern));
            if (pattern.Length == 0)
                return 0;
            var num1 = -1;
            var num2 = source.Length - pattern.Length;
            while ((num1 = Array.IndexOf(source, pattern[0], num1 + 1)) <= num2 && num1 != -1)
            {
                var index = 0;
                while (source[num1 + index].Equals(pattern[index]))
                {
                    if (++index == pattern.Length)
                        return num1;
                }
            }
            return -1;
        }

        /// <summary>
        /// Find the first occurrence of string array in another string array.
        /// </summary>
        /// <param name="source">The array to search in.</param>
        /// <param name="pattern">The array to find.</param>
        /// <param name="ignoreCase">true to ignore case when comparing the string to seek; otherwise, false.</param>
        /// <returns>The first position of the found array or -1 if not found.</returns>
        public static int FindArrayIndex(this string[] source, string[] pattern, bool ignoreCase = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (pattern == null)
                throw new ArgumentNullException(nameof(pattern));
            if (pattern.Length == 0)
                return 0;
            var num1 = -1;
            var num2 = source.Length - pattern.Length;
            var comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            while ((num1 = Array.IndexOf(source, pattern[0], num1 + 1)) <= num2 && num1 != -1)
            {
                var index = 0;
                while (source[num1 + index].Equals(pattern[index], comparisonType))
                {
                    if (++index == pattern.Length)
                        return num1;
                }
            }
            return -1;
        }

        /// <summary>
        /// Find the last occurrence of value type array in another value type array.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <param name="source">The array to search in.</param>
        /// <param name="pattern">The array to find.</param>
        /// <returns>The last position of the found array or -1 if not found.</returns>
        public static int FindArrayLastIndex<T>(this T[] source, T[] pattern)
            where T : IComparable, IConvertible, IEquatable<T>
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (pattern == null)
                throw new ArgumentNullException(nameof(pattern));
            if (pattern.Length == 0)
                return source.Length - 1;
            var num = source.Length - pattern.Length + 1;
            while ((num = Array.LastIndexOf(source, pattern[0], num - 1)) >= 0 && num != -1)
            {
                var index = 0;
                while (source[num + index].Equals(pattern[index]))
                {
                    if (++index == pattern.Length)
                        return num;
                }
            }
            return -1;
        }

        /// <summary>
        /// Find the last occurrence of string array in another string array.
        /// </summary>
        /// <param name="source">The array to search in.</param>
        /// <param name="pattern">The array to find.</param>
        /// <param name="ignoreCase">true to ignore case when comparing the string to seek; otherwise, false.</param>
        /// <returns>The last position of the found array or -1 if not found.</returns>
        public static int FindArrayLastIndex(this string[] source, string[] pattern, bool ignoreCase = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (pattern == null)
                throw new ArgumentNullException(nameof(pattern));
            if (pattern.Length == 0)
                return source.Length - 1;
            var num = source.Length - pattern.Length + 1;
            var comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            while ((num = Array.LastIndexOf(source, pattern[0], num - 1)) >= 0 && num != -1)
            {
                var index = 0;
                while (source[num + index].Equals(pattern[index], comparisonType))
                {
                    if (++index == pattern.Length)
                        return num;
                }
            }
            return -1;
        }
    }
}