// Decompiled with JetBrains decompiler
// Type: CSharpBasicExtensionsKit.EnumerableExtensions
// Assembly: CSharpBasicExtensionsKit, Version=1.0.6.0, Culture=neutral, PublicKeyToken=null
// MVID: EC83AE8C-D918-490B-8983-23C0E093D8F5
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\CSharpBasicExtensionsKit.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProj47
{
    public static partial class Extensions
    {
    public static bool IsEnumEmpty<T>(this IEnumerable<T> sequence)
    {
      if (!sequence.IsNull())
        return !sequence.Any<T>();
      return true;
    }

    public static bool IsEnumNotEmpty<T>(this IEnumerable<T> sequence)
    {
      if (sequence.IsNotNull())
        return sequence.Any<T>();
      return false;
    }

    public static IEnumerable<T> IfEnumNotEmpty<T>(this IEnumerable<T> sequence, Action<IEnumerable<T>> action)
    {
      if (sequence.IsEnumNotEmpty<T>())
        action(sequence);
      if (!sequence.IsNull())
        return sequence;
      return (IEnumerable<T>) new T[0];
    }

    public static IEnumerable<TRet> IfEnumNotEmpty<T, TRet>(this IEnumerable<T> sequence, Func<IEnumerable<T>, IEnumerable<TRet>> action)
    {
      if (!sequence.IsEnumNotEmpty<T>())
        return (IEnumerable<TRet>) new TRet[0];
      return action(sequence);
    }

    public static IEnumerable<T> IfEnumEmpty<T>(this IEnumerable<T> sequence, Action action)
    {
      if (sequence.IsEnumEmpty<T>())
        action();
      if (!sequence.IsNull())
        return sequence;
      return (IEnumerable<T>) new T[0];
    }

    public static IEnumerable<TRet> IfEnumEmpty<T, TRet>(this IEnumerable<T> sequence, Func<IEnumerable<TRet>> action)
    {
      IEnumerable<TRet> rets = (IEnumerable<TRet>) null;
      if (sequence.IsEnumEmpty<T>())
        rets = action();
      return rets ?? (IEnumerable<TRet>) new TRet[0];
    }
  }
}
