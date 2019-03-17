using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HSNXT.DSharpPlus.Extended.EventArgs;

namespace HSNXT.DSharpPlus.Extended.AsyncListeners
{
    internal static class AsyncListenerHandler
    {
        public static void InstallListeners(DspExtended dspExtended, Type type)
        {
            // find all methods with AsyncListener attr
            var listenerMethods =
                from m in type.GetMethods()
                let attribute = m.GetCustomAttribute<AsyncListenerAttribute>(true)
                where attribute != null
                select new AsyncListenerMethod(m, attribute);

            foreach (var listener in listenerMethods)
            {
                listener.Register(dspExtended);
            }
        }
    }

    internal readonly struct AsyncListenerMethod
    {
        public MethodInfo Method { get; }
        public AsyncListenerAttribute Attribute { get; }
        
        public AsyncListenerMethod(MethodInfo method, AsyncListenerAttribute attribute)
        {
            Method = method;
            Attribute = attribute;
        }

        // TODO linq expressions, reflection magic to replace the big fat switch block and the two handler methods
        public void Register(DspExtended dspExtended)
        {
            var client = dspExtended.Client;
            var cnext = dspExtended.CNext;
            
            // this variable is part of a closure so it stores a copy of ourselves on the heap since normally we're only
            // on the stack
            var self = this;
            
            var eventType = self.Attribute.Target.ToString();
            
            // nope, there's no cleaner way to do this. sorry
            Task OnEventWithArgs(object e)
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        dspExtended.Log[LogSource.AsyncEventInvoke]($"Invoking event {eventType}");
                        await (Task) self.Method.Invoke(null, new[] {client, e});
                    }
                    catch (Exception ex)
                    {
                        dspExtended.Log[LogSource.AsyncEventError]($"Uncaught exception in event handler context for {eventType}: {ex}");
                        dspExtended.Log[LogSource.AsyncEventError](ex.StackTrace);
                        await dspExtended.InvokeAsyncEventErrored(new ExtensionErrorEventArgs(dspExtended)
                        {
                            EventName = eventType,
                            Exception = ex
                        });
                    }
                });
                return Task.CompletedTask;
            }

            Task OnEventVoid()
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        dspExtended.Log[LogSource.AsyncEventInvoke]($"Invoking event {eventType}");
                        await (Task) self.Method.Invoke(null, new object[] {client});
                    }
                    catch (Exception ex)
                    {
                        dspExtended.Log[LogSource.AsyncEventError]($"Uncaught exception in event handler context for {eventType}: {ex}");
                        dspExtended.Log[LogSource.AsyncEventError](ex.StackTrace);
                        await dspExtended.InvokeAsyncEventErrored(new ExtensionErrorEventArgs(dspExtended)
                        {
                            EventName = eventType,
                            Exception = ex
                        });
                    }
                });
                return Task.CompletedTask;
            }

            switch (Attribute.Target)
            {
                case EventTypes.ClientErrored:
                    client.ClientErrored += OnEventWithArgs;
                    break;
                case EventTypes.SocketErrored:
                    client.SocketErrored += OnEventWithArgs;
                    break;
                case EventTypes.SocketOpened:
                    client.SocketOpened += OnEventVoid;
                    break;
                case EventTypes.SocketClosed:
                    client.SocketClosed += OnEventWithArgs;
                    break;
                case EventTypes.Ready:
                    client.Ready += OnEventWithArgs;
                    break;
                case EventTypes.Resumed:
                    client.Resumed += OnEventWithArgs;
                    break;
                case EventTypes.ChannelCreated:
                    client.ChannelCreated += OnEventWithArgs;
                    break;
                case EventTypes.DmChannelCreated:
                    client.DmChannelCreated += OnEventWithArgs;
                    break;
                case EventTypes.ChannelUpdated:
                    client.ChannelUpdated += OnEventWithArgs;
                    break;
                case EventTypes.ChannelDeleted:
                    client.ChannelDeleted += OnEventWithArgs;
                    break;
                case EventTypes.DmChannelDeleted:
                    client.DmChannelDeleted += OnEventWithArgs;
                    break;
                case EventTypes.ChannelPinsUpdated:
                    client.ChannelPinsUpdated += OnEventWithArgs;
                    break;
                case EventTypes.GuildCreated:
                    client.GuildCreated += OnEventWithArgs;
                    break;
                case EventTypes.GuildAvailable:
                    client.GuildAvailable += OnEventWithArgs;
                    break;
                case EventTypes.GuildUpdated:
                    client.GuildUpdated += OnEventWithArgs;
                    break;
                case EventTypes.GuildDeleted:
                    client.GuildDeleted += OnEventWithArgs;
                    break;
                case EventTypes.GuildUnavailable:
                    client.GuildUnavailable += OnEventWithArgs;
                    break;
                case EventTypes.MessageCreated:
                    client.MessageCreated += OnEventWithArgs;
                    break;
                case EventTypes.PresenceUpdated:
                    client.PresenceUpdated += OnEventWithArgs;
                    break;
                case EventTypes.GuildBanAdded:
                    client.GuildBanAdded += OnEventWithArgs;
                    break;
                case EventTypes.GuildBanRemoved:
                    client.GuildBanRemoved += OnEventWithArgs;
                    break;
                case EventTypes.GuildEmojisUpdated:
                    client.GuildEmojisUpdated += OnEventWithArgs;
                    break;
                case EventTypes.GuildIntegrationsUpdated:
                    client.GuildIntegrationsUpdated += OnEventWithArgs;
                    break;
                case EventTypes.GuildMemberAdded:
                    client.GuildMemberAdded += OnEventWithArgs;
                    break;
                case EventTypes.GuildMemberRemoved:
                    client.GuildMemberRemoved += OnEventWithArgs;
                    break;
                case EventTypes.GuildMemberUpdated:
                    client.GuildMemberUpdated += OnEventWithArgs;
                    break;
                case EventTypes.GuildRoleCreated:
                    client.GuildRoleCreated += OnEventWithArgs;
                    break;
                case EventTypes.GuildRoleUpdated:
                    client.GuildRoleUpdated += OnEventWithArgs;
                    break;
                case EventTypes.GuildRoleDeleted:
                    client.GuildRoleDeleted += OnEventWithArgs;
                    break;
                case EventTypes.MessageAcknowledged:
                    client.MessageAcknowledged += OnEventWithArgs;
                    break;
                case EventTypes.MessageUpdated:
                    client.MessageUpdated += OnEventWithArgs;
                    break;
                case EventTypes.MessageDeleted:
                    client.MessageDeleted += OnEventWithArgs;
                    break;
                case EventTypes.MessagesBulkDeleted:
                    client.MessagesBulkDeleted += OnEventWithArgs;
                    break;
                case EventTypes.TypingStarted:
                    client.TypingStarted += OnEventWithArgs;
                    break;
                case EventTypes.UserSettingsUpdated:
                    client.UserSettingsUpdated += OnEventWithArgs;
                    break;
                case EventTypes.UserUpdated:
                    client.UserUpdated += OnEventWithArgs;
                    break;
                case EventTypes.VoiceStateUpdated:
                    client.VoiceStateUpdated += OnEventWithArgs;
                    break;
                case EventTypes.VoiceServerUpdated:
                    client.VoiceServerUpdated += OnEventWithArgs;
                    break;
                case EventTypes.GuildMembersChunked:
                    client.GuildMembersChunked += OnEventWithArgs;
                    break;
                case EventTypes.UnknownEvent:
                    client.UnknownEvent += OnEventWithArgs;
                    break;
                case EventTypes.MessageReactionAdded:
                    client.MessageReactionAdded += OnEventWithArgs;
                    break;
                case EventTypes.MessageReactionRemoved:
                    client.MessageReactionRemoved += OnEventWithArgs;
                    break;
                case EventTypes.MessageReactionsCleared:
                    client.MessageReactionsCleared += OnEventWithArgs;
                    break;
                case EventTypes.WebhooksUpdated:
                    client.WebhooksUpdated += OnEventWithArgs;
                    break;
                case EventTypes.Heartbeated:
                    client.Heartbeated += OnEventWithArgs;
                    break;
#if !IS_LITE_VERSION
                case EventTypes.CommandExecuted:
                    cnext.Value.CommandErrored += OnEventWithArgs;
                    break;
                case EventTypes.CommandErrored:
                    cnext.Value.CommandErrored += OnEventWithArgs;
                    break;
#endif
            }
        }
    }
}