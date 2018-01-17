// Decompiled with JetBrains decompiler
// Type: ExtensionMinder.ReflectionExtensions
// Assembly: ExtensionMinder, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9895DAEA-F52E-4F0C-B5FD-FB311AEFF61C
// Assembly location: ...\bin\Debug\ExtensionMinder.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static string GetMemberName<T>(this T instance, Expression<Func<T, object>> expression)
        {
            return StaticReflection.GetMemberName(expression);
        }

        public static string GetMemberName<T>(this T instance, Expression<Action<T>> expression)
        {
            return StaticReflection.GetMemberName(expression);
        }

        public static bool PublicInstancePropertiesEqual<T>(this T self, T to, params string[] ignore) where T : class
        {
            if (self == null || to == null)
                return self == to;
            var type = typeof(T);
            var ignoreList = new List<string>(ignore);
            return !type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(pi => !ignoreList.Contains(pi.Name)).Select(pi => new
                {
                    pi,
                    selfValue = type.GetProperty(pi.Name)?.GetValue(self, null)
                }).Select(p1 => new
                {
                    tf0 = p1,
                    toValue = type.GetProperty(p1.pi.Name)?.GetValue(to, null)
                }).Where(p0 =>
                {
                    if (p0.tf0.selfValue == p0.toValue)
                        return false;
                    if (p0.tf0.selfValue != null)
                        return !p0.tf0.selfValue.Equals(p0.toValue);
                    return true;
                }).Select(param0 => param0.tf0.selfValue).Any();
        }
    }
}