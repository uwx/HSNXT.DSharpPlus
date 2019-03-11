using System;
using System.Linq.Expressions;
using System.Reflection;

namespace HSNXT.DSharpPlus.Extended
{
    public class Expressions
    {
        private static ParameterExpression ParameterInstance<T>() => Expression.Parameter(typeof(T), "instance");

        internal static Func<T, TResult> CallInstanceMethod0<T, TResult>(string name)
        {
            var instance = ParameterInstance<T>();
            
            return Expression.Lambda<Func<T, TResult>>(
                Expression.Call(instance, name, null), instance // return instance.name()
            ).Compile(); // (instance) => ...
        }
        
        internal static Func<T> Constructor0<T>()
        {
            return Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
        }

        internal static Func<TArg1, T> Constructor1<T, TArg1>()
        {
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var constructor = typeof(T).GetConstructor(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, 
                null, new[] {typeof(TArg1)}, null);
            
            if (constructor == null)
                throw new ArgumentNullException(nameof(constructor),
                    $"No matching constructor found for {typeof(T)} that takes a {typeof(TArg1)} as argument 1");

            return Expression.Lambda<Func<TArg1, T>>(
                Expression.New(constructor, arg1), arg1
            ).Compile();
        }

        internal static Func<TArg1, TArg2, T> Constructor2<T, TArg1, TArg2>()
        {
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            
            var constructor = typeof(T).GetConstructor(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, 
                null, new[] {typeof(TArg1), typeof(TArg2)}, null);
            
            if (constructor == null)
                throw new ArgumentNullException(nameof(constructor),
                    $"No matching constructor found for {typeof(T)} that takes [{typeof(TArg1)},{typeof(TArg2)}] arguments");

            return Expression.Lambda<Func<TArg1, TArg2, T>>(
                Expression.New(constructor, arg1, arg2), arg1, arg2
            ).Compile();
        }
        
        internal static Func<T, TResult> GetMember<T, TResult>(string name)
        {
            var instance = ParameterInstance<T>();
            
            return Expression.Lambda<Func<T, TResult>>(
                Expression.PropertyOrField(instance, name) // return instance.name
                , instance // (instance) => ...
            ).Compile();
        }

        internal static Action<T, TValue> SetMember<T, TValue>(string name) where T : class
        {
            var instance = ParameterInstance<T>();
            var value = Expression.Parameter(typeof(TValue), "value");

            return Expression.Lambda<Action<T, TValue>>(
                Expression.Assign(Expression.PropertyOrField(instance, name), value) // instance.name = value
                , instance, value).Compile(); // (ref instance, value) => ...
        }
    }
}