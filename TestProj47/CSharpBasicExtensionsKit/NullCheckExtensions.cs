// Decompiled with JetBrains decompiler
// Type: CSharpBasicExtensionsKit.NullCheckExtensions
// Assembly: CSharpBasicExtensionsKit, Version=1.0.6.0, Culture=neutral, PublicKeyToken=null
// MVID: EC83AE8C-D918-490B-8983-23C0E093D8F5
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\CSharpBasicExtensionsKit.dll

using System;

namespace CSharpBasicExtensionsKit
{
  public static class NullCheckExtensions
  {
    public static bool IsNull(this object obj)
    {
      return obj == null;
    }

    public static bool IsNotNull(this object obj)
    {
      return obj != null;
    }

    public static T IfNull<T>(this T value, Action action)
    {
      if (((object) value).IsNull())
        action();
      return value;
    }

    public static TRet IfNull<T, TRet>(this T source, Func<TRet> func) where TRet : class
    {
      if (!((object) source).IsNull())
        return (object) source as TRet;
      return func();
    }

    public static T IfNotNull<T>(this T source, Action action)
    {
      if (((object) source).IsNotNull())
        action();
      return source;
    }

    public static T IfNotNull<T>(this T source, Action<T> action)
    {
      if (((object) source).IsNotNull())
        action(source);
      return source;
    }

    public static TR IfNotNull<T, TR>(this T source, Func<T, TR> action)
    {
      if (!((object) source).IsNotNull())
        return default (TR);
      return action(source);
    }

    public static TR IfNotNull<T, TR>(this T source, Func<TR> action)
    {
      if (((object) source).IsNotNull())
        return action();
      return default (TR);
    }
  }
}
