using System;
using System.Threading.Tasks;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static T With<T>(this T obj, Action action)
        {
            action();
            return obj;
        }

        public static T With<T, T0>(this T obj, Action<T0> action, T0 arg0)
        {
            action(arg0);
            return obj;
        }

        public static T With<T, T0, T1>(this T obj, Action<T0, T1> action, T0 arg0, T1 arg1)
        {
            action(arg0, arg1);
            return obj;
        }

        public static T With<T, T0, T1, T2>(this T obj, Action<T0, T1, T2> action, T0 arg0, T1 arg1, T2 arg2)
        {
            action(arg0, arg1, arg2);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3>(this T obj, Action<T0, T1, T2, T3> action, T0 arg0, T1 arg1, T2 arg2,
            T3 arg3)
        {
            action(arg0, arg1, arg2, arg3);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4>(this T obj, Action<T0, T1, T2, T3, T4> action, T0 arg0, T1 arg1,
            T2 arg2, T3 arg3, T4 arg4)
        {
            action(arg0, arg1, arg2, arg3, arg4);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5>(this T obj, Action<T0, T1, T2, T3, T4, T5> action, T0 arg0,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            action(arg0, arg1, arg2, arg3, arg4, arg5);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6>(this T obj, Action<T0, T1, T2, T3, T4, T5, T6> action,
            T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            action(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7>(this T obj,
            Action<T0, T1, T2, T3, T4, T5, T6, T7> action, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
            T6 arg6, T7 arg7)
        {
            action(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8>(this T obj,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> action, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
            T6 arg6, T7 arg7, T8 arg8)
        {
            action(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this T obj,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
            T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            action(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this T obj,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            action(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this T obj,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            action(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this T obj,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            action(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this T obj,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action, T0 arg0, T1 arg1, T2 arg2,
            T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            action(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this T obj,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action, T0 arg0, T1 arg1, T2 arg2,
            T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10,
            T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            action(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this T obj,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action, T0 arg0, T1 arg1,
            T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10
                arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            action(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14,
                arg15);
            return obj;
        }

        public static T With<T, T0, TResult>(this T obj, Func<T0, TResult> func, T0 arg0)
        {
            _ = func(arg0);
            return obj;
        }

        public static T With<T, T0, T1, TResult>(this T obj, Func<T0, T1, TResult> func, T0 arg0, T1 arg1)
        {
            _ = func(arg0, arg1);
            return obj;
        }

        public static T With<T, T0, T1, T2, TResult>(this T obj, Func<T0, T1, T2, TResult> func, T0 arg0, T1 arg1,
            T2 arg2)
        {
            _ = func(arg0, arg1, arg2);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, TResult>(this T obj, Func<T0, T1, T2, T3, TResult> func, T0 arg0,
            T1 arg1, T2 arg2, T3 arg3)
        {
            _ = func(arg0, arg1, arg2, arg3);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, TResult>(this T obj, Func<T0, T1, T2, T3, T4, TResult> func,
            T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            _ = func(arg0, arg1, arg2, arg3, arg4);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, TResult>(this T obj, Func<T0, T1, T2, T3, T4, T5, TResult> func,
            T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            _ = func(arg0, arg1, arg2, arg3, arg4, arg5);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, TResult> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
            T6 arg6)
        {
            _ = func(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, TResult> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
            T6 arg6, T7 arg7)
        {
            _ = func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            _ = func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            _ = func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            _ = func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            _ = func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func, T0 arg0, T1 arg1, T2 arg2,
            T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11
                arg11, T12 arg12)
        {
            _ = func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func, T0 arg0, T1 arg1, T2 arg2,
            T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            _ = func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func, T0 arg0, T1 arg1,
            T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            _ = func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
            return obj;
        }

        public static T With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            this T obj, Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func,
            T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            _ = func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14,
                arg15);
            return obj;
        }

        public static async Task<T> With<T, T0, TResult>(this T obj, Func<T0, Task<TResult>> func, T0 arg0)
        {
            await func(arg0);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, TResult>(this T obj, Func<T0, T1, Task<TResult>> func, T0 arg0,
            T1 arg1)
        {
            await func(arg0, arg1);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, TResult>(this T obj, Func<T0, T1, T2, Task<TResult>> func,
            T0 arg0, T1 arg1, T2 arg2)
        {
            await func(arg0, arg1, arg2);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, TResult>(this T obj,
            Func<T0, T1, T2, T3, Task<TResult>> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            await func(arg0, arg1, arg2, arg3);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, Task<TResult>> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            await func(arg0, arg1, arg2, arg3, arg4);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, Task<TResult>> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, Task<TResult>> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
            T6 arg6)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6, T7, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, Task<TResult>> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            T5 arg5, T6 arg6, T7 arg7)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, Task<TResult>> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, Task<TResult>> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task<TResult>> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task<TResult>> func, T0 arg0, T1 arg1, T2 arg2,
            T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task<TResult>> func, T0 arg0, T1 arg1, T2 arg2,
            T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            this T obj, Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task<TResult>> func, T0 arg0,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8
            , T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            this T obj, Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task<TResult>> func,
            T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7,
            T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                arg14);
            return obj;
        }

        public static async Task<T>
            With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this T obj,
                Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task<TResult>> func, T0 arg0,
                T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
                T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                arg14, arg15);
            return obj;
        }

        public static async Task<T> With<T, T0>(this T obj, Func<T0, Task> func, T0 arg0)
        {
            await func(arg0);
            return obj;
        }

        public static async Task<T> With<T, T0, T1>(this T obj, Func<T0, T1, Task> func, T0 arg0, T1 arg1)
        {
            await func(arg0, arg1);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2>(this T obj, Func<T0, T1, T2, Task> func, T0 arg0,
            T1 arg1, T2 arg2)
        {
            await func(arg0, arg1, arg2);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3>(this T obj, Func<T0, T1, T2, T3, Task> func,
            T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            await func(arg0, arg1, arg2, arg3);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4>(this T obj,
            Func<T0, T1, T2, T3, T4, Task> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            await func(arg0, arg1, arg2, arg3, arg4);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, Task> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, Task> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6, T7>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, Task> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
            T6 arg6, T7 arg7)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, Task> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
            T6 arg6, T7 arg7, T8 arg8)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, Task> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this T obj,
            Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10
            , T11 arg11, T12 arg12)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
            this T obj, Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task> func, T0 arg0, T1 arg1,
            T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12,
            T13 arg13)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            return obj;
        }

        public static async Task<T> With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            this T obj, Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task> func, T0 arg0,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9
                arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                arg14);
            return obj;
        }

        public static async Task<T>
            With<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this T obj,
                Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task> func, T0 arg0, T1 arg1,
                T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8
                    arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            await func(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                arg14, arg15);
            return obj;
        }
    }
}