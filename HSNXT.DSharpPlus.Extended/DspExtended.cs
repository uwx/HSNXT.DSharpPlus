using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using HSNXT.DSharpPlus.Extended.AsyncListeners;
using HSNXT.DSharpPlus.Extended.EventArgs;

namespace HSNXT.DSharpPlus.Extended
{
    public static class DspExtendedModuleLoader
    {
        public static DspExtended UseDspExtended(this DiscordClient client)
        {
            return new DspExtended(client);
        }
    }

    public class DspExtended
    {
        internal DiscordClient Client { get; set; }
        internal CommandsNextExtension CNext { get; set; }
        
        internal DspExtended(DiscordClient client)
        {
            Client = client;
            CNext = client.GetCommandsNext(); 
            
            _extensionErrored = new AsyncEvent<ExtensionErrorEventArgs>(EventErrorHandler, "ExtensionErrored");
            _mentionReceived = new AsyncEvent<MentionReceivedEventArgs>(EventErrorHandler, "MentionReceived");
        }

        /// <summary>
        /// Registers async listeners for an assembly
        /// </summary>
        /// <param name="assembly">the assembly to register</param>
        public void RegisterAssembly(Assembly assembly = null)
        {
            if (assembly == null) assembly = Assembly.GetEntryAssembly();
            AsyncListenerHandler.InstallListeners(Client, CNext, assembly.GetTypes());
        }
        
        internal void EventErrorHandler(string evname, Exception ex)
        {
            Client.DebugLogger.LogMessage(LogLevel.Error, "DspExtended", $"An {ex.GetType()} occured in {evname}.", DateTime.Now);
            _extensionErrored.InvokeAsync(new ExtensionErrorEventArgs(Client, this)
            {
                EventName = evname,
                Exception = ex
            }).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        
        private Task OnMessageCreated(MessageCreateEventArgs e)
        {
            if (e.MentionedUsers.Contains(Client.CurrentUser))
            {
                return _mentionReceived.InvokeAsync(new MentionReceivedEventArgs(Client)
                {
                    Message = e.Message,
                    MentionedChannels = e.MentionedChannels,
                    MentionedRoles = e.MentionedRoles,
                    MentionedUsers = e.MentionedUsers,
                });
            }

            return Task.CompletedTask;
        }
        
        /// <summary>
        /// Fired whenever an error occurs within an extension event handler.
        /// </summary>
        public event AsyncEventHandler<ExtensionErrorEventArgs> ExtensionErrored
        {
            add => _extensionErrored.Register(value);
            remove => _extensionErrored.Unregister(value);
        }
        private readonly AsyncEvent<ExtensionErrorEventArgs> _extensionErrored;
        
        /// <summary>
        /// Fired when the client's user gets mentioned.
        /// </summary>
        public event AsyncEventHandler<MentionReceivedEventArgs> MentionReceived
        {
            add
            {
                CheckEventsAdding();
                _mentionReceived.Register(value);
            }
            remove
            {
                CheckEventsRemoving();
                _mentionReceived.Unregister(value);
            }
        }
        private readonly AsyncEvent<MentionReceivedEventArgs> _mentionReceived;
        
        private void CheckEventsRemoving()
        {
            _messageEvents--;
            if (_messageEvents == 0)
            {
                Client.MessageCreated -= OnMessageCreated;
            }
        }

        private void CheckEventsAdding()
        {
            _messageEvents++;
            if (_messageEvents == 0)
            {
                Client.MessageCreated += OnMessageCreated;
            }
        }

        private int _messageEvents;
    }
}