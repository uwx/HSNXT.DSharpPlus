// Decompiled with JetBrains decompiler
// Type: CSharpBasicExtensionsKit.StringExtensions
// Assembly: CSharpBasicExtensionsKit, Version=1.0.6.0, Culture=neutral, PublicKeyToken=null
// MVID: EC83AE8C-D918-490B-8983-23C0E093D8F5
// Assembly location: ...\bin\Debug\CSharpBasicExtensionsKit.dll

using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static string IfEmpty(this string value, Action action)
        {
            if (value.IsEmpty())
                action();
            return value;
        }

        public static string IfEmpty(this string value, Func<string> action)
        {
            return !value.IsEmpty() ? value : action();
        }

        public static string IfNotEmpty(this string value, Action action)
        {
            if (value.IsNotEmpty())
                action();
            return value;
        }

        public static string IfNotEmpty(this string value, Action<string> action)
        {
            if (value.IsNotEmpty())
                action(value);
            return value;
        }

        public static string IfNotEmpty(this string value, Func<string, string> action)
        {
            return !value.IsNotEmpty() ? value : action(value);
        }

        public static TRet IfNotEmpty<TRet>(this string value, Func<string, TRet> action)
        {
            return !value.IsNotEmpty() ? default : action(value);
        }

        public static string Call(this Func<string> action)
        {
            return !action.IsNull() ? action() : string.Empty;
        }

        public static string IfNullEmptyStr(this string source)
        {
            return source.IsEmpty() ? string.Empty : source;
        }
    }
}