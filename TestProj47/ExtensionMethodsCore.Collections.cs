// Decompiled with JetBrains decompiler
// Type: Extensions
// Assembly: ExtensionMethodsCore.Collections, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CE9DD4F1-668E-4D0B-937C-49597777AEF5
// Assembly location: ...\bin\Debug\ExtensionMethodsCore.Collections.dll

using System.Collections.Generic;
using System.Linq;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static bool IsNotNullOrEmpty<T>(this ICollection<T> @this)
        {
            return @this != null && (uint) @this.Count > 0U;
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> @this)
        {
            return @this == null || @this.Count == 0;
        }

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> @this)
        {
            return @this != null && @this.Any<T>();
        }
    }
}