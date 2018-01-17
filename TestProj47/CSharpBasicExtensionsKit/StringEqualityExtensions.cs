// Decompiled with JetBrains decompiler
// Type: CSharpBasicExtensionsKit.StringEqualityExtensions
// Assembly: CSharpBasicExtensionsKit, Version=1.0.6.0, Culture=neutral, PublicKeyToken=null
// MVID: EC83AE8C-D918-490B-8983-23C0E093D8F5
// Assembly location: ...\bin\Debug\CSharpBasicExtensionsKit.dll

using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static bool IsEqualTo(this string str, string value)
        {
            if (str.IsNull() && value.IsNull())
                return true;
            if (str.IsNull() || value.IsNull())
                return false;
            return str.Equals(value, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsNotEqualTo(this string str, string value)
        {
            if (str.IsNull() && value.IsNull())
                return false;
            if (str.IsNull() || value.IsNull())
                return true;
            return !str.Equals(value, StringComparison.CurrentCultureIgnoreCase);
        }

        public static string IfNotEqualTo(this string left, string right, Action<string, string> action)
        {
            if (left.IsNotEqualTo(right))
                action(left, right);
            return left;
        }

        public static TRet IfNotEqualTo<TRet>(this string left, string right, Func<string, string, TRet> func)
        {
            if (left.IsNotEqualTo(right))
                return func(left, right);
            return default;
        }

        public static string IfEqualTo(this string left, string right, Action<string, string> action)
        {
            if (left.IsEqualTo(right))
                action(left, right);
            return left;
        }

        public static TRet IfEqualTo<TRet>(this string left, string right, Func<string, string, TRet> func)
        {
            if (left.IsEqualTo(right))
                return func(left, right);
            return default;
        }
    }
}