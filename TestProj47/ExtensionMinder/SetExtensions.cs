// Decompiled with JetBrains decompiler
// Type: ExtensionMinder.SetExtensions
// Assembly: ExtensionMinder, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9895DAEA-F52E-4F0C-B5FD-FB311AEFF61C
// Assembly location: ...\bin\Debug\ExtensionMinder.dll

using System.Collections.Generic;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static void AddRange(this ISet<string> set, IEnumerable<string> other)
        {
            foreach (var str in other)
            {
                if (!string.IsNullOrWhiteSpace(str))
                    set.Add(str);
            }
        }
    }
}