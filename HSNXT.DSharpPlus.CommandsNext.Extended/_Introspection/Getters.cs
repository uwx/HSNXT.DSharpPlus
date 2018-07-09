using System;
using System.Linq.Expressions;

namespace DSharpPlus.CommandsNext._Introspection
{
    public static class Getters
    {
        public static Func<T, TResult> MemberInstance<T, TResult>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            
            return Expression.Lambda<Func<T, TResult>>(
                Expression.PropertyOrField(instance, name) // return instance.name
                , instance // (instance) => ...
            ).Compile();
        }

        public static Func<TR> MemberStatic<T, TR>(string name)
        {
            return Expression.Lambda<Func<TR>>(
                ExpressionsBase.StaticPropertyOrField(typeof(T), name) // () => return T.name
            ).Compile();
        }
    }
}