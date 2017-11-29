using System;
using System.Linq;
using HSNXT;

namespace Wither
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
#if WHICH            
            var a = "";
            for (var i = 1; i <= 16; i++)
            {
                var sm = "T".Repeat(i).Select((e,j) => $"{e}{j}");
                a += $@"
        public static T With<T,{sm.StringJoin(",")}>(this T obj, Action<{sm.StringJoin(",")}> action, {"T".Repeat(i).Select((e,j) => $"{e}{j} arg{j}").StringJoin(", ")})
        {{
            action({"a".Repeat(i).Select((e,j) => $"{e}rg{j}").StringJoin(", ")});
            return obj;
        }}";
            }
            Console.WriteLine(a);
#elif WHICH2
            var a = "";
            for (var i = 1; i <= 16; i++)
            {
                var sm = "T".Repeat(i).Select((e,j) => $"{e}{j}");
                a += $@"
        public static T With<T,{sm.StringJoin(",")},TResult>(this T obj, Func<{sm.StringJoin(",")},TResult> func, {"T".Repeat(i).Select((e,j) => $"{e}{j} arg{j}").StringJoin(", ")})
        {{
            _ = func({"a".Repeat(i).Select((e,j) => $"{e}rg{j}").StringJoin(", ")});
            return obj;
        }}";
            }
            Console.WriteLine(a);
#elif WHICH3
            var a = "";
            for (var i = 1; i <= 16; i++)
            {
                var sm = "T".Repeat(i).Select((e,j) => $"{e}{j}");
                a += $@"
        public static async Task<T> With<T,{sm.StringJoin(",")},TResult>(this T obj, Func<{sm.StringJoin(",")},Task<TResult>> func, {"T".Repeat(i).Select((e,j) => $"{e}{j} arg{j}").StringJoin(", ")})
        {{
            _ = await func({"a".Repeat(i).Select((e,j) => $"{e}rg{j}").StringJoin(", ")});
            return obj;
        }}";
            }
            Console.WriteLine(a);
#else
            var a = "";
            for (var i = 1; i <= 16; i++)
            {
                var sm = "T".Repeat(i).Select((e,j) => $"{e}{j}");
                a += $@"
        public static async Task With<T,{sm.StringJoin(",")},TResult>(this T obj, Func<{sm.StringJoin(",")},Task> func, {"T".Repeat(i).Select((e,j) => $"{e}{j} arg{j}").StringJoin(", ")})
        {{
            _ = await func({"a".Repeat(i).Select((e,j) => $"{e}rg{j}").StringJoin(", ")});
            return obj;
        }}";
            }
            Console.WriteLine(a);
#endif
        }
    }
}