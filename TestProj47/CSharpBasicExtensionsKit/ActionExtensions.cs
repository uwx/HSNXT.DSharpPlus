// Decompiled with JetBrains decompiler
// Type: CSharpBasicExtensionsKit.ActionExtensions
// Assembly: CSharpBasicExtensionsKit, Version=1.0.6.0, Culture=neutral, PublicKeyToken=null
// MVID: EC83AE8C-D918-490B-8983-23C0E093D8F5
// Assembly location: ...\bin\Debug\CSharpBasicExtensionsKit.dll

using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static void Call(this Action action)
        {
            action.IfNotNull(x => x());
        }

        public static void Call<T>(this Action<T> action, T param)
        {
            action.IfNotNull(x => x(param));
        }

        public static void Call<T1, T2>(this Action<T1, T2> action, T1 param1, T2 param2)
        {
            action.IfNotNull(x => x(param1, param2));
        }
    }
}