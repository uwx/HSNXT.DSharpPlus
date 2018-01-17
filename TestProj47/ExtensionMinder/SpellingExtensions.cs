// Decompiled with JetBrains decompiler
// Type: ExtensionMinder.Extensions
// Assembly: ExtensionMinder, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9895DAEA-F52E-4F0C-B5FD-FB311AEFF61C
// Assembly location: ...\bin\Debug\ExtensionMinder.dll

using System;
using System.Collections.Generic;
using System.Linq;
using SV = HSNXT.SpellingVariants;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static IEnumerable<string> SpellingVariants(this string word, SpellingVariants spellingVariants)
        {
            switch (spellingVariants)
            {
                case HSNXT.SpellingVariants.None:
                    return new[]
                    {
                        word
                    };
                case HSNXT.SpellingVariants.One:
                    return Edits(word);
                case HSNXT.SpellingVariants.Two:
                    return Edits(word)
                        .SelectMany(Edits)
                        .ToArray();
            }
            return null;
        }

        private static IEnumerable<string> Edits(string word)
        {
            var set = new HashSet<string>();
            var splits = Splits(set, word);
            var enumerable = splits as Tuple<string, string>[] ?? splits.ToArray();
            set.AddRange(Deletes(enumerable));
            set.AddRange(Transposes(enumerable));
            set.AddRange(Replaces(enumerable));
            set.AddRange(Inserts(enumerable));
            return set;
        }

        private static IEnumerable<Tuple<string, string>> Splits(IEnumerable<string> set, string word)
        {
            return set.Select((t, index) =>
                    new Tuple<string, string>(word.Substring(0, index), word.Substring(index, word.Length - index)))
                .ToList();
        }

        private static IEnumerable<string> Deletes(IEnumerable<Tuple<string, string>> splits)
        {
            return splits.Where(x => x.Item2.Length > 0).Select(x => x.Item1 + x.Item2.Substring(1));
        }

        private static IEnumerable<string> Transposes(IEnumerable<Tuple<string, string>> splits)
        {
            return splits.Where(x => x.Item2.Length > 1).Select(x =>
                x.Item1 + (object) x.Item2[1] + (object) x.Item2[0] + x.Item2.Substring(2));
        }

        private static IEnumerable<string> Replaces(IEnumerable<Tuple<string, string>> splits)
        {
            return "abcdefghijklmnopqrstuvwxyz".SelectMany(x => Replaces(splits, x));
        }

        private static IEnumerable<string> Replaces(IEnumerable<Tuple<string, string>> splits, char letter)
        {
            return splits.Where(x => x.Item2.Length > 0).Select(x => x.Item1 + (object) letter + x.Item2.Substring(1));
        }

        private static IEnumerable<string> Inserts(IEnumerable<Tuple<string, string>> splits)
        {
            return "abcdefghijklmnopqrstuvwxyz".SelectMany(x => Inserts(splits, x));
        }

        private static IEnumerable<string> Inserts(IEnumerable<Tuple<string, string>> splits, char letter)
        {
            return splits.Select(x => x.Item1 + (object) letter + x.Item2);
        }
    }
}