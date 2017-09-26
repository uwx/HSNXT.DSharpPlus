// Decompiled with JetBrains decompiler
// Type: CSharpBasicExtensionsKit.BooleanExtensions
// Assembly: CSharpBasicExtensionsKit, Version=1.0.6.0, Culture=neutral, PublicKeyToken=null
// MVID: EC83AE8C-D918-490B-8983-23C0E093D8F5
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\CSharpBasicExtensionsKit.dll

using System;

namespace TestProj47
{
    public static partial class Extensions
    {

    public static bool IfTrue(this bool expression, Action<bool> action)
    {
      if (expression)
        action(expression);
      return expression;
    }

    public static T IfTrue<T>(this bool expression, Func<bool, T> action)
    {
      if (expression)
        return action(expression);
      return default (T);
    }

    public static bool IfFalse(this bool expression, Action<bool> action)
    {
      if (!expression)
        action(expression);
      return expression;
    }

    public static T IfFalse<T>(this bool expression, Func<bool, T> action)
    {
      if (!expression)
        return action(expression);
      return default (T);
    }
  }
}
