// Decompiled with JetBrains decompiler
// Type: SimpleExtension.LinqExtension
// Assembly: SimpleExtension, Version=0.0.23.0, Culture=neutral, PublicKeyToken=null
// MVID: B43B1EC6-29EF-47CC-B98E-E2FE4FC2095C
// Assembly location: ...\bin\Debug\SimpleExtension.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static IEnumerable<T> YieldOneDefault<T>(this IEnumerable<T> values)
        {
            yield return default;
            foreach (var obj in values)
                yield return obj;
        }

        public static T RandomElement<T>(this IQueryable<T> q, Expression<Func<T, bool>> e)
        {
            var random = new Random();
            q = q.Where(e);
            return q.Skip(random.Next(q.Count())).FirstOrDefault();
        }

        public static T Random<T>(this IEnumerable<T> pList, Random pRandomSeed)
        {
            if (pList == null) return default;
            var en = pList as T[] ?? pList.ToArray();
            return en.Any() ? en.ElementAt(pRandomSeed.Next(en.Length)) : default;
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random pRandomSeed)
        {
            var list = source.ToList();
            var pRandomSeed1 = pRandomSeed;
            list.Shuffle(pRandomSeed1);
            return list;
        }

        public static void Shuffle<T>(this IList<T> pList)
        {
            pList.Shuffle(new Random());
        }

        public static void Shuffle<T>(this IList<T> pList, Random pRandomSeed)
        {
            var count = pList.Count;
            while (count > 1)
            {
                var index = pRandomSeed.Next(count--);
                var p = pList[count];
                pList[count] = pList[index];
                pList[index] = p;
            }
        }

        public static IEnumerable<IEnumerable<TValue>> Chunks<TValue>(this IEnumerable<TValue> values, int chunkSize)
        {
            return values.Select((v, i) => (v, groupIndex: i / chunkSize))
                .GroupBy(x => x.groupIndex)
                .Select(g => g.Select(x => x.v));
        }
    }
}