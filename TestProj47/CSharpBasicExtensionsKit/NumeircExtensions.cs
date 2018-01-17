// Decompiled with JetBrains decompiler
// Type: CSharpBasicExtensionsKit.NumeircExtensions
// Assembly: CSharpBasicExtensionsKit, Version=1.0.6.0, Culture=neutral, PublicKeyToken=null
// MVID: EC83AE8C-D918-490B-8983-23C0E093D8F5
// Assembly location: ...\bin\Debug\CSharpBasicExtensionsKit.dll

using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static int ToInt(this string value)
        {
            if (!double.TryParse(value, out var result))
                return 0;
            return (int) Math.Round(result);
        }

        public static int? ToIntNullable(this string value)
        {
            if (!double.TryParse(value, out var result))
                return new int?();
            return (int) Math.Round(result);
        }
    }
}