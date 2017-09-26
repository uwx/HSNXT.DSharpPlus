// Decompiled with JetBrains decompiler
// Type: CSharpBasicExtensionsKit.ActionExtensions
// Assembly: CSharpBasicExtensionsKit, Version=1.0.6.0, Culture=neutral, PublicKeyToken=null
// MVID: EC83AE8C-D918-490B-8983-23C0E093D8F5
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\CSharpBasicExtensionsKit.dll

using System;

namespace TestProj47
{
    public static partial class Extensions
    {
        public static void Call(this Action action)
        {
            action.IfNotNull<Action>((Action<Action>) (x => x()));
        }

        public static void Call<T>(this Action<T> action, T param)
        {
            action.IfNotNull<Action<T>>((Action<Action<T>>) (x => x(param)));
        }

        public static void Call<T1, T2>(this Action<T1, T2> action, T1 param1, T2 param2)
        {
            action.IfNotNull<Action<T1, T2>>((Action<Action<T1, T2>>) (x => x(param1, param2)));
        }
    }
}