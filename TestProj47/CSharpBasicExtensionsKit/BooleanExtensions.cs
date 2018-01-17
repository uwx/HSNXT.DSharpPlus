// Decompiled with JetBrains decompiler
// Type: CSharpBasicExtensionsKit.BooleanExtensions
// Assembly: CSharpBasicExtensionsKit, Version=1.0.6.0, Culture=neutral, PublicKeyToken=null
// MVID: EC83AE8C-D918-490B-8983-23C0E093D8F5
// Assembly location: ...\bin\Debug\CSharpBasicExtensionsKit.dll

using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static bool IfTrue(this bool expression, Action<bool> action)
        {
            if (expression)
                action(true);
            return expression;
        }

        public static T IfTrue<T>(this bool expression, Func<bool, T> action)
        {
            return expression ? action(true) : default;
        }

        public static bool IfFalse(this bool expression, Action<bool> action)
        {
            if (!expression)
                action(false);
            return expression;
        }

        public static T IfFalse<T>(this bool expression, Func<bool, T> action)
        {
            return !expression ? action(false) : default;
        }
    }
}