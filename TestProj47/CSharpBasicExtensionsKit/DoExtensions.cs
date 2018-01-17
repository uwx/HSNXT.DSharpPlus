// Decompiled with JetBrains decompiler
// Type: CSharpBasicExtensionsKit.DoExtensions
// Assembly: CSharpBasicExtensionsKit, Version=1.0.6.0, Culture=neutral, PublicKeyToken=null
// MVID: EC83AE8C-D918-490B-8983-23C0E093D8F5
// Assembly location: ...\bin\Debug\CSharpBasicExtensionsKit.dll

using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static T Do<T>(this T target, Action<T> action) where T : class
        {
            action(target);
            return target;
        }

        public static TRet Do<T, TRet>(this T target, Func<T, TRet> action) where T : class
        {
            return action(target);
        }
    }
}