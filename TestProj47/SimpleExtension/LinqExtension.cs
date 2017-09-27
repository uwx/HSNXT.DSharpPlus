// Decompiled with JetBrains decompiler
// Type: SimpleExtension.LinqExtension
// Assembly: SimpleExtension, Version=0.0.23.0, Culture=neutral, PublicKeyToken=null
// MVID: B43B1EC6-29EF-47CC-B98E-E2FE4FC2095C
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\SimpleExtension.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleExtension
{
    public static class LinqExtension
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
            if (pList != null && pList.Any())
                return pList.ElementAt(pRandomSeed.Next(pList.Count()));
            return default;
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> pList)
        {
            return pList.Shuffle(new Random());
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