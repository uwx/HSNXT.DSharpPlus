using System;
using System.Reflection;
using DSharpPlus.EventArgs;

namespace DSharpPlus.CommandsNext._Introspection
{
    // ReSharper disable once TypeParameterCanBeVariant
    public delegate T ObjectActivator<T>(params object[] args);

    public partial class ProxiedDiscordClient : IDisposable
    {
        public const bool False = false;
        public const bool True = true;
        
        private readonly DiscordClient _client;

        // from new event error
        private static readonly Action<DiscordClient, string, Exception> CallEventErrorHandler =
            Callers.InstanceVoid2<DiscordClient, string, Exception>("EventErrorHandler");

        // from old event error
        //private readonly AsyncEvent<ClientErrorEventArgs> _clientErrored;

        public ProxiedDiscordClient(DiscordClient client)
        {
            _client = client;
            
            // from old event error
            //_clientErrored = GetClientErrored(client);
        }
        
        public static implicit operator ProxiedDiscordClient(DiscordClient client) => new ProxiedDiscordClient(client);
        public static implicit operator DiscordClient(ProxiedDiscordClient client) => client._client;

        // new EventErrorHandler


        public void EventErrorHandler(string evname, Exception ex) => CallEventErrorHandler(_client, evname, ex);

        // old EventErrorHandler
        /*
        private static readonly Func<DiscordClient, AsyncEvent<ClientErrorEventArgs>> GetClientErrored =
            Getters.MemberInstance<DiscordClient, AsyncEvent<ClientErrorEventArgs>>("_clientErrored");
        
        private static readonly Action<ClientErrorEventArgs, string> SetEventName =
            Setters.MemberInstanceClass<ClientErrorEventArgs, string>("EventName");
        
        private static readonly Action<ClientErrorEventArgs, Exception> SetException =
            Setters.MemberInstanceClass<ClientErrorEventArgs, Exception>("Exception");

        private static readonly ConstructorInfo ClientErrorEventArgsConstructor =
            typeof(ClientErrorEventArgs).GetConstructor(BindingFlags.NonPublic, null, new[] {typeof(DiscordClient)}, null)
            ?? throw new IntrospectionSetupException($"Did not find new {nameof(ClientErrorEventArgs)}({nameof(DiscordClient)}) constructor in {nameof(ClientErrorEventArgs)}");

        private static readonly ObjectActivator<ClientErrorEventArgs> NewClientErrorEventArgs = Constructors.GetActivator<ClientErrorEventArgs>(ClientErrorEventArgsConstructor);

        public void EventErrorHandler(string evname, Exception ex)
        {
            DebugLogger.LogMessage(LogLevel.Error, "DSharpPlus", $"An {ex.GetType()} occured in {evname}.",
                DateTime.Now);

            var eventArgs = NewClientErrorEventArgs(_client);
            SetEventName(eventArgs, evname);
            SetException(eventArgs, ex);

            _clientErrored.InvokeAsync(eventArgs).ConfigureAwait(false).GetAwaiter().GetResult();
        }*/
    }

    public class ProxiedDiscordMessage
    {

    }
}