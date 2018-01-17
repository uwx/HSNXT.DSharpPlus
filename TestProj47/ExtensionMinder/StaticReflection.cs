// Decompiled with JetBrains decompiler
// Type: ExtensionMinder.StaticReflection
// Assembly: ExtensionMinder, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9895DAEA-F52E-4F0C-B5FD-FB311AEFF61C
// Assembly location: ...\bin\Debug\ExtensionMinder.dll

using System;
using System.Linq.Expressions;

namespace HSNXT
{
    public static class StaticReflection
    {
        public static string GetMemberName<T>(this T instance, Expression<Func<T, object>> expression)
        {
            return GetMemberName(expression);
        }

        public static string GetMemberName<T>(Expression<Func<T, object>> expression)
        {
            if (expression == null)
                throw new ArgumentException("The expression cannot be null.");
            return GetMemberName(expression.Body);
        }

        public static string GetMemberName<T>(this T instance, Expression<Action<T>> expression)
        {
            return GetMemberName(expression);
        }

        public static string GetMemberName<T>(Expression<Action<T>> expression)
        {
            if (expression == null)
                throw new ArgumentException("The expression cannot be null.");
            return GetMemberName(expression.Body);
        }

        private static string GetMemberName(Expression expression)
        {
            switch (expression)
            {
                case null:
                    throw new ArgumentException("The expression cannot be null.");
                case MemberExpression memberExpression:
                    return memberExpression.Member.Name;
                case MethodCallExpression callExpression:
                    return callExpression.Method.Name;
                case UnaryExpression unaryExpression:
                    return GetMemberName(unaryExpression);
            }
            throw new ArgumentException("Invalid expression");
        }

        private static string GetMemberName(UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MethodCallExpression expression)
                return expression.Method.Name;
            return ((MemberExpression) unaryExpression.Operand).Member.Name;
        }
    }
}