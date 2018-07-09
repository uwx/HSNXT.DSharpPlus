using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DSharpPlus.CommandsNext._Introspection
{
    internal static class ExpressionsBase
    {
        internal const BindingFlags PropertyStaticPublic = BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Static;
        internal const BindingFlags PropertyStaticNonPublic = BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Static;

        // By BJurado, from http://forums.asp.net/t/1709783.aspx/1
        internal static MemberExpression StaticPropertyOrField(IReflect type, string propertyOrFieldName)
        {
            var property = type.GetProperty(propertyOrFieldName, PropertyStaticPublic);
            if (property != null)
                return Expression.Property(null, property);
                
            var field = type.GetField(propertyOrFieldName, PropertyStaticPublic);
            if (field != null)
                return Expression.Field(null, field);
                
            property = type.GetProperty(propertyOrFieldName, PropertyStaticNonPublic);
            if (property != null)
                return Expression.Property(null, property);
                
            field = type.GetField(propertyOrFieldName, PropertyStaticNonPublic);
            if (field != null)
                return Expression.Field(null, field);
                
            throw new ArgumentException($"{propertyOrFieldName} NotAMemberOfType {type}");
        }

        internal static MethodInfo Method<T>(string name, params Type[] parameters)
        {
            var method = typeof(T).GetMethod(name, PropertyStaticPublic, null, parameters, null);
            if (method == null)
                method = typeof(T).GetMethod(name, PropertyStaticNonPublic, null, parameters, null);
            if (method == null)
                throw new Exception($"Could not find method {typeof(T)}::{name}");
            return method;
        }

        internal static ParameterExpression ParameterInstance<T>() => Expression.Parameter(typeof(T), "instance");
    }
}