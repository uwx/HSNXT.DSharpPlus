using System;
using System.Linq.Expressions;

namespace DSharpPlus.CommandsNext._Introspection
{
    public static class Callers
    {
        public static Func<T, TResult> Instance0<T, TResult>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            
            return Expression.Lambda<Func<T, TResult>>(
                Expression.Call(instance, name, null), instance // return instance.name()
            ).Compile(); // (instance) => ...
        }

        public static Func<TResult> Static0<T, TResult>(string name)
        {
            return Expression.Lambda<Func<TResult>>(
                Expression.Call(ExpressionsBase.Method<T>(name)) // return T.name()
            ).Compile(); // () => ...
        }

        public static Action<T> InstanceVoid0<T>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            
            return Expression.Lambda<Action<T>>(
                Expression.Call(instance, name, null), instance // instance.name()
            ).Compile(); // (instance) => ...
        }

        public static Action StaticVoid0<T>(string name)
        {
            return Expression.Lambda<Action>(
                Expression.Call(ExpressionsBase.Method<T>(name)) // T.name()
            ).Compile(); // () => ...
        }
        
        public static Func<T, TArg0, TResult> Instance1<T, TArg0, TResult>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");

            return Expression.Lambda<Func<T, TArg0, TResult>>(
                Expression.Call(instance, name, null, arg0), instance, arg0 // return instance.name(arg0)
            ).Compile(); // (instance, arg0) => ...
        }

        public static Func<TArg0, TResult> Static1<T, TArg0, TResult>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            
            return Expression.Lambda<Func<TArg0, TResult>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0)), arg0), arg0 // return T.name(arg0)
            ).Compile(); // (arg0) => ...
        }

        public static Action<T, TArg0> InstanceVoid1<T, TArg0>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            
            return Expression.Lambda<Action<T, TArg0>>(
                Expression.Call(instance, name, null, arg0), instance, arg0 // instance.name(arg0)
            ).Compile(); // (instance, arg0) => ...
        }

        public static Action<TArg0> StaticVoid1<T, TArg0>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            
            return Expression.Lambda<Action<TArg0>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0)), arg0), arg0 // T.name(arg0)
            ).Compile(); // (arg0) => ...
        }
        public static Func<T, TArg0, TArg1, TResult> Instance2<T, TArg0, TArg1, TResult>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");

            return Expression.Lambda<Func<T, TArg0, TArg1, TResult>>(
                Expression.Call(instance, name, null, arg0, arg1), instance, arg0, arg1 // return instance.name(arg0, arg1)
            ).Compile(); // (instance, arg0, arg1) => ...
        }

        public static Func<TArg0, TArg1, TResult> Static2<T, TArg0, TArg1, TResult>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            
            return Expression.Lambda<Func<TArg0, TArg1, TResult>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1)), arg0, arg1), arg0, arg1 // return T.name(arg0, arg1)
            ).Compile(); // (arg0, arg1) => ...
        }

        public static Action<T, TArg0, TArg1> InstanceVoid2<T, TArg0, TArg1>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            
            return Expression.Lambda<Action<T, TArg0, TArg1>>(
                Expression.Call(instance, name, null, arg0, arg1), instance, arg0, arg1 // instance.name(arg0, arg1)
            ).Compile(); // (instance, arg0, arg1) => ...
        }

        public static Action<TArg0, TArg1> StaticVoid2<T, TArg0, TArg1>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            
            return Expression.Lambda<Action<TArg0, TArg1>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1)), arg0, arg1), arg0, arg1 // T.name(arg0, arg1)
            ).Compile(); // (arg0, arg1) => ...
        }
        public static Func<T, TArg0, TArg1, TArg2, TResult> Instance3<T, TArg0, TArg1, TArg2, TResult>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");

            return Expression.Lambda<Func<T, TArg0, TArg1, TArg2, TResult>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2), instance, arg0, arg1, arg2 // return instance.name(arg0, arg1, arg2)
            ).Compile(); // (instance, arg0, arg1, arg2) => ...
        }

        public static Func<TArg0, TArg1, TArg2, TResult> Static3<T, TArg0, TArg1, TArg2, TResult>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            
            return Expression.Lambda<Func<TArg0, TArg1, TArg2, TResult>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2)), arg0, arg1, arg2), arg0, arg1, arg2 // return T.name(arg0, arg1, arg2)
            ).Compile(); // (arg0, arg1, arg2) => ...
        }

        public static Action<T, TArg0, TArg1, TArg2> InstanceVoid3<T, TArg0, TArg1, TArg2>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            
            return Expression.Lambda<Action<T, TArg0, TArg1, TArg2>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2), instance, arg0, arg1, arg2 // instance.name(arg0, arg1, arg2)
            ).Compile(); // (instance, arg0, arg1, arg2) => ...
        }

        public static Action<TArg0, TArg1, TArg2> StaticVoid3<T, TArg0, TArg1, TArg2>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            
            return Expression.Lambda<Action<TArg0, TArg1, TArg2>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2)), arg0, arg1, arg2), arg0, arg1, arg2 // T.name(arg0, arg1, arg2)
            ).Compile(); // (arg0, arg1, arg2) => ...
        }
        public static Func<T, TArg0, TArg1, TArg2, TArg3, TResult> Instance4<T, TArg0, TArg1, TArg2, TArg3, TResult>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");

            return Expression.Lambda<Func<T, TArg0, TArg1, TArg2, TArg3, TResult>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3), instance, arg0, arg1, arg2, arg3 // return instance.name(arg0, arg1, arg2, arg3)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3) => ...
        }

        public static Func<TArg0, TArg1, TArg2, TArg3, TResult> Static4<T, TArg0, TArg1, TArg2, TArg3, TResult>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            
            return Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TResult>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3)), arg0, arg1, arg2, arg3), arg0, arg1, arg2, arg3 // return T.name(arg0, arg1, arg2, arg3)
            ).Compile(); // (arg0, arg1, arg2, arg3) => ...
        }

        public static Action<T, TArg0, TArg1, TArg2, TArg3> InstanceVoid4<T, TArg0, TArg1, TArg2, TArg3>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            
            return Expression.Lambda<Action<T, TArg0, TArg1, TArg2, TArg3>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3), instance, arg0, arg1, arg2, arg3 // instance.name(arg0, arg1, arg2, arg3)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3) => ...
        }

        public static Action<TArg0, TArg1, TArg2, TArg3> StaticVoid4<T, TArg0, TArg1, TArg2, TArg3>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            
            return Expression.Lambda<Action<TArg0, TArg1, TArg2, TArg3>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3)), arg0, arg1, arg2, arg3), arg0, arg1, arg2, arg3 // T.name(arg0, arg1, arg2, arg3)
            ).Compile(); // (arg0, arg1, arg2, arg3) => ...
        }
        public static Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TResult> Instance5<T, TArg0, TArg1, TArg2, TArg3, TArg4, TResult>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");

            return Expression.Lambda<Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TResult>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4), instance, arg0, arg1, arg2, arg3, arg4 // return instance.name(arg0, arg1, arg2, arg3, arg4)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4) => ...
        }

        public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TResult> Static5<T, TArg0, TArg1, TArg2, TArg3, TArg4, TResult>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            
            return Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4)), arg0, arg1, arg2, arg3, arg4), arg0, arg1, arg2, arg3, arg4 // return T.name(arg0, arg1, arg2, arg3, arg4)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4) => ...
        }

        public static Action<T, TArg0, TArg1, TArg2, TArg3, TArg4> InstanceVoid5<T, TArg0, TArg1, TArg2, TArg3, TArg4>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            
            return Expression.Lambda<Action<T, TArg0, TArg1, TArg2, TArg3, TArg4>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4), instance, arg0, arg1, arg2, arg3, arg4 // instance.name(arg0, arg1, arg2, arg3, arg4)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4) => ...
        }

        public static Action<TArg0, TArg1, TArg2, TArg3, TArg4> StaticVoid5<T, TArg0, TArg1, TArg2, TArg3, TArg4>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            
            return Expression.Lambda<Action<TArg0, TArg1, TArg2, TArg3, TArg4>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4)), arg0, arg1, arg2, arg3, arg4), arg0, arg1, arg2, arg3, arg4 // T.name(arg0, arg1, arg2, arg3, arg4)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4) => ...
        }
        public static Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult> Instance6<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");

            return Expression.Lambda<Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5), instance, arg0, arg1, arg2, arg3, arg4, arg5 // return instance.name(arg0, arg1, arg2, arg3, arg4, arg5)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5) => ...
        }

        public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult> Static6<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            
            return Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5)), arg0, arg1, arg2, arg3, arg4, arg5), arg0, arg1, arg2, arg3, arg4, arg5 // return T.name(arg0, arg1, arg2, arg3, arg4, arg5)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5) => ...
        }

        public static Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5> InstanceVoid6<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            
            return Expression.Lambda<Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5), instance, arg0, arg1, arg2, arg3, arg4, arg5 // instance.name(arg0, arg1, arg2, arg3, arg4, arg5)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5) => ...
        }

        public static Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5> StaticVoid6<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            
            return Expression.Lambda<Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5)), arg0, arg1, arg2, arg3, arg4, arg5), arg0, arg1, arg2, arg3, arg4, arg5 // T.name(arg0, arg1, arg2, arg3, arg4, arg5)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5) => ...
        }
        public static Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> Instance7<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");

            return Expression.Lambda<Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6 // return instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6) => ...
        }

        public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> Static7<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            
            return Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6)), arg0, arg1, arg2, arg3, arg4, arg5, arg6), arg0, arg1, arg2, arg3, arg4, arg5, arg6 // return T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6) => ...
        }

        public static Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> InstanceVoid7<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            
            return Expression.Lambda<Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6 // instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6) => ...
        }

        public static Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> StaticVoid7<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            
            return Expression.Lambda<Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6)), arg0, arg1, arg2, arg3, arg4, arg5, arg6), arg0, arg1, arg2, arg3, arg4, arg5, arg6 // T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6) => ...
        }
        public static Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult> Instance8<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");

            return Expression.Lambda<Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7 // return instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => ...
        }

        public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult> Static8<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            
            return Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7)), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7 // return T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => ...
        }

        public static Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> InstanceVoid8<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            
            return Expression.Lambda<Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7 // instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => ...
        }

        public static Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> StaticVoid8<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            
            return Expression.Lambda<Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7)), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7 // T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7) => ...
        }
        public static Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult> Instance9<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");

            return Expression.Lambda<Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 // return instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => ...
        }

        public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult> Static9<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            
            return Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8)), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 // return T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => ...
        }

        public static Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> InstanceVoid9<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            
            return Expression.Lambda<Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 // instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => ...
        }

        public static Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> StaticVoid9<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            
            return Expression.Lambda<Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8)), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 // T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => ...
        }
        public static Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult> Instance10<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");

            return Expression.Lambda<Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 // return instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => ...
        }

        public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult> Static10<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            
            return Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9)), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 // return T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => ...
        }

        public static Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> InstanceVoid10<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            
            return Expression.Lambda<Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 // instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => ...
        }

        public static Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> StaticVoid10<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            
            return Expression.Lambda<Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9)), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 // T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => ...
        }
        public static Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult> Instance11<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");

            return Expression.Lambda<Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 // return instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => ...
        }

        public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult> Static11<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            
            return Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10)), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 // return T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => ...
        }

        public static Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> InstanceVoid11<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            
            return Expression.Lambda<Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 // instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => ...
        }

        public static Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> StaticVoid11<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            
            return Expression.Lambda<Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10)), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 // T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => ...
        }
        public static Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult> Instance12<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            var arg11 = Expression.Parameter(typeof(TArg11), "arg11");

            return Expression.Lambda<Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 // return instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => ...
        }

        public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult> Static12<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            var arg11 = Expression.Parameter(typeof(TArg11), "arg11");
            
            return Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11)), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 // return T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => ...
        }

        public static Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> InstanceVoid12<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            var arg11 = Expression.Parameter(typeof(TArg11), "arg11");
            
            return Expression.Lambda<Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 // instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => ...
        }

        public static Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> StaticVoid12<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            var arg11 = Expression.Parameter(typeof(TArg11), "arg11");
            
            return Expression.Lambda<Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11)), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 // T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => ...
        }
        public static Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult> Instance13<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            var arg11 = Expression.Parameter(typeof(TArg11), "arg11");
            var arg12 = Expression.Parameter(typeof(TArg12), "arg12");

            return Expression.Lambda<Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 // return instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => ...
        }

        public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult> Static13<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            var arg11 = Expression.Parameter(typeof(TArg11), "arg11");
            var arg12 = Expression.Parameter(typeof(TArg12), "arg12");
            
            return Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12)), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 // return T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => ...
        }

        public static Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> InstanceVoid13<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            var arg11 = Expression.Parameter(typeof(TArg11), "arg11");
            var arg12 = Expression.Parameter(typeof(TArg12), "arg12");
            
            return Expression.Lambda<Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 // instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => ...
        }

        public static Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> StaticVoid13<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            var arg11 = Expression.Parameter(typeof(TArg11), "arg11");
            var arg12 = Expression.Parameter(typeof(TArg12), "arg12");
            
            return Expression.Lambda<Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12)), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 // T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => ...
        }
        public static Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult> Instance14<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            var arg11 = Expression.Parameter(typeof(TArg11), "arg11");
            var arg12 = Expression.Parameter(typeof(TArg12), "arg12");
            var arg13 = Expression.Parameter(typeof(TArg13), "arg13");

            return Expression.Lambda<Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 // return instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => ...
        }

        public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult> Static14<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            var arg11 = Expression.Parameter(typeof(TArg11), "arg11");
            var arg12 = Expression.Parameter(typeof(TArg12), "arg12");
            var arg13 = Expression.Parameter(typeof(TArg13), "arg13");
            
            return Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13)), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 // return T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => ...
        }

        public static Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> InstanceVoid14<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            var arg11 = Expression.Parameter(typeof(TArg11), "arg11");
            var arg12 = Expression.Parameter(typeof(TArg12), "arg12");
            var arg13 = Expression.Parameter(typeof(TArg13), "arg13");
            
            return Expression.Lambda<Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 // instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => ...
        }

        public static Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> StaticVoid14<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            var arg11 = Expression.Parameter(typeof(TArg11), "arg11");
            var arg12 = Expression.Parameter(typeof(TArg12), "arg12");
            var arg13 = Expression.Parameter(typeof(TArg13), "arg13");
            
            return Expression.Lambda<Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13)), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 // T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => ...
        }
        public static Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult> Instance15<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            var arg11 = Expression.Parameter(typeof(TArg11), "arg11");
            var arg12 = Expression.Parameter(typeof(TArg12), "arg12");
            var arg13 = Expression.Parameter(typeof(TArg13), "arg13");
            var arg14 = Expression.Parameter(typeof(TArg14), "arg14");

            return Expression.Lambda<Func<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14 // return instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => ...
        }

        public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult> Static15<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            var arg11 = Expression.Parameter(typeof(TArg11), "arg11");
            var arg12 = Expression.Parameter(typeof(TArg12), "arg12");
            var arg13 = Expression.Parameter(typeof(TArg13), "arg13");
            var arg14 = Expression.Parameter(typeof(TArg14), "arg14");
            
            return Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13), typeof(TArg14)), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14 // return T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => ...
        }

        public static Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> InstanceVoid15<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(string name)
        {
            var instance = ExpressionsBase.ParameterInstance<T>();
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            var arg11 = Expression.Parameter(typeof(TArg11), "arg11");
            var arg12 = Expression.Parameter(typeof(TArg12), "arg12");
            var arg13 = Expression.Parameter(typeof(TArg13), "arg13");
            var arg14 = Expression.Parameter(typeof(TArg14), "arg14");
            
            return Expression.Lambda<Action<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>>(
                Expression.Call(instance, name, null, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14), instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14 // instance.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14)
            ).Compile(); // (instance, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => ...
        }

        public static Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> StaticVoid15<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(string name)
        {
            var arg0 = Expression.Parameter(typeof(TArg0), "arg0");
            var arg1 = Expression.Parameter(typeof(TArg1), "arg1");
            var arg2 = Expression.Parameter(typeof(TArg2), "arg2");
            var arg3 = Expression.Parameter(typeof(TArg3), "arg3");
            var arg4 = Expression.Parameter(typeof(TArg4), "arg4");
            var arg5 = Expression.Parameter(typeof(TArg5), "arg5");
            var arg6 = Expression.Parameter(typeof(TArg6), "arg6");
            var arg7 = Expression.Parameter(typeof(TArg7), "arg7");
            var arg8 = Expression.Parameter(typeof(TArg8), "arg8");
            var arg9 = Expression.Parameter(typeof(TArg9), "arg9");
            var arg10 = Expression.Parameter(typeof(TArg10), "arg10");
            var arg11 = Expression.Parameter(typeof(TArg11), "arg11");
            var arg12 = Expression.Parameter(typeof(TArg12), "arg12");
            var arg13 = Expression.Parameter(typeof(TArg13), "arg13");
            var arg14 = Expression.Parameter(typeof(TArg14), "arg14");
            
            return Expression.Lambda<Action<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>>(
                Expression.Call(ExpressionsBase.Method<T>(name, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13), typeof(TArg14)), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14 // T.name(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14)
            ).Compile(); // (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => ...
        }
    }
}