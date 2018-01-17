// Decompiled with JetBrains decompiler
// Type: CSharpBasicExtensionsKit.EnumerableExtensions
// Assembly: CSharpBasicExtensionsKit, Version=1.0.6.0, Culture=neutral, PublicKeyToken=null
// MVID: EC83AE8C-D918-490B-8983-23C0E093D8F5
// Assembly location: ...\bin\Debug\CSharpBasicExtensionsKit.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static bool IsEnumEmpty<T>(this IEnumerable<T> sequence)
        {
            var enumerable = sequence as T[] ?? sequence.ToArray();
            if (!enumerable.IsNull())
                return !enumerable.Any();
            return true;
        }

        public static bool IsEnumNotEmpty<T>(this IEnumerable<T> sequence)
        {
            var enumerable = sequence as T[] ?? sequence.ToArray();
            return enumerable.IsNotNull() && enumerable.Any();
        }

        public static IEnumerable<T> IfEnumNotEmpty<T>(this IEnumerable<T> sequence, Action<IEnumerable<T>> action)
        {
            var ifEnumNotEmpty = sequence as T[] ?? sequence.ToArray();
            if (ifEnumNotEmpty.IsEnumNotEmpty())
                action(ifEnumNotEmpty);
            return !ifEnumNotEmpty.IsNull() ? ifEnumNotEmpty : new T[0];
        }

        public static IEnumerable<TRet> IfEnumNotEmpty<T, TRet>(this IEnumerable<T> sequence,
            Func<IEnumerable<T>, IEnumerable<TRet>> action)
        {
            var enumerable = sequence as T[] ?? sequence.ToArray();
            return !enumerable.IsEnumNotEmpty() ? new TRet[0] : action(enumerable);
        }

        public static IEnumerable<T> IfEnumEmpty<T>(this IEnumerable<T> sequence, Action action)
        {
            var ifEnumEmpty = sequence as T[] ?? sequence.ToArray();
            if (ifEnumEmpty.IsEnumEmpty())
                action();
            return !ifEnumEmpty.IsNull() ? ifEnumEmpty : new T[0];
        }

        public static IEnumerable<TRet> IfEnumEmpty<T, TRet>(this IEnumerable<T> sequence,
            Func<IEnumerable<TRet>> action)
        {
            IEnumerable<TRet> rets = null;
            if (sequence.IsEnumEmpty())
                rets = action();
            return rets ?? new TRet[0];
        }
    }
}