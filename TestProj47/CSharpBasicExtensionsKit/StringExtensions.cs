// Decompiled with JetBrains decompiler
// Type: CSharpBasicExtensionsKit.StringExtensions
// Assembly: CSharpBasicExtensionsKit, Version=1.0.6.0, Culture=neutral, PublicKeyToken=null
// MVID: EC83AE8C-D918-490B-8983-23C0E093D8F5
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\CSharpBasicExtensionsKit.dll

using System;

namespace TestProj47
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
            if (!value.IsEmpty())
                return value;
            return action();
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
            if (!value.IsNotEmpty())
                return value;
            return action(value);
        }

        public static TRet IfNotEmpty<TRet>(this string value, Func<string, TRet> action)
        {
            if (!value.IsNotEmpty())
                return default(TRet);
            return action(value);
        }

        public static string Call(this Func<string> action)
        {
            if (!action.IsNull())
                return action();
            return string.Empty;
        }

        public static string IfNullEmptyStr(this string source)
        {
            if (source.IsEmpty())
                return string.Empty;
            return source;
        }
    }
}