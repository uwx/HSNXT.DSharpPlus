using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace HSNXT.DSharpPlus.Extended.AsyncListeners
{
    internal static class AsyncListenerHandler
    {
        public static IEnumerable<ListenerMethod> ListenerMethods { get; private set; }
        
        public static void InstallListeners(DiscordClient client, CommandsNextExtension cnext, IEnumerable<Type> types)
        {
            // find all methods with AsyncListener attr
            ListenerMethods =
                from t in types
                from m in t.GetMethods()
                let attribute = m.GetCustomAttribute<AsyncListenerAttribute>(true)
                where attribute != null
                select new ListenerMethod { Method = m, Attribute = attribute };

            foreach (var listener in ListenerMethods)
            {
                listener.Attribute.Register(cnext, client, listener.Method);
            }
        }
    }

    internal class ListenerMethod
    {
        public MethodInfo Method { get; internal set; }
        public AsyncListenerAttribute Attribute { get; internal set; }
    }
}