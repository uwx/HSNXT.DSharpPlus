using System;
using System.Linq.Expressions;

namespace DSharpPlus.CommandsNext._Introspection
{
    public static class Setters
    {
        public static StructSetter<T, TV> MemberInstanceStruct<T, TV>(string name) where T : struct
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var value = Expression.Parameter(typeof(TV), "value");

            return Expression.Lambda<StructSetter<T, TV>>(
                Expression.Assign(Expression.PropertyOrField(instance, name), value) // instance.name = value
                , instance, value).Compile(); // (ref instance, value) => ...
        }

        public static Action<T, TValue> MemberInstanceClass<T, TValue>(string name) where T : class
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var value = Expression.Parameter(typeof(TValue), "value");

            return Expression.Lambda<Action<T, TValue>>(
                Expression.Assign(Expression.PropertyOrField(instance, name), value) // instance.name = value
                , instance, value).Compile(); // (instance, value) => ...
        }

        public static Action<TValue> MemberStatic<T, TValue>(string name)
        {
            var value = Expression.Parameter(typeof(TValue), "value");

            return Expression.Lambda<Action<TValue>>(
                Expression.Assign(ExpressionsBase.StaticPropertyOrField(typeof(T), name), value) // T.name = value
                , value).Compile(); // (value) => ...
        }
    }
}