using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using HSNXT.DSharpPlus.Extended.EventArgs;

#if !IS_LITE_VERSION
using HSNXT.DSharpPlus.Extended.AsyncListeners;
#endif

namespace HSNXT.DSharpPlus.Extended
{
    /// <summary>
    /// Extension class to install the <see cref="DspExtended"/> module.
    /// </summary>
    public static class DspExtendedLoader
    {
        public static DspExtended UseDspExtended(this DiscordClient client) 
            => ModuleLoader.UseModule<DspExtended>(client);
        
        public static Task<IReadOnlyDictionary<int, DspExtended>> UseDspExtendedAsync(this DiscordShardedClient client) 
            => ModuleLoader.UseModuleAsync<DspExtended>(client);
    }

    /// <summary>
    /// An extension for DSharpPlus adding a variety of extension methods, and more. Some features include, but are not
    /// limited to:
    /// <list type="bullet">
    ///     <item><description>
    ///         Extension methods for <see cref="DiscordChannel"/>, <see cref="DiscordMember"/> and
    ///         <see cref="DiscordRole"/>
    ///     </description></item>
    ///     <item><description>
    ///         Module system that works on top of DSharpPlus' extension system. See the documentation on
    ///         <see cref="ModuleLoader"/> to get started!
    ///     </description></item>
    ///     <item><description>
    ///         A <see cref="MentionReceived"/> event
    ///     </description></item>
    /// </list>
    /// The full version includes some more contested goodies:
    /// <list type="bullet">
    ///     <item><description>
    ///         Async listener methods registered through <see cref="AsyncListenerAttribute"/>
    ///     </description></item>
    ///     <item><description>
    ///         Shortcuts for modifying specific members of entities with <see cref="HSNXT.DSharpPlus.Extended.ExtensionMethods.ModifyShortcuts"/>
    ///     </description></item>
    /// </list>
    /// </summary>
    /// <example>
    /// The extension methods do not require any extra setup - simply import them and use away. To install the main
    /// extension, call <see cref="DspExtendedLoader"/>.<see cref="DspExtendedLoader.UseDspExtended"/> or
    /// <see cref="DspExtendedLoader.UseDspExtendedAsync"/>. You can use the methods on the resulting instance to
    /// customize the fucntionality. If you're using any of the CommandsNext-related extensions, always register
    /// D#+ Extended <i>after</i> CommandsNext, or you will encounter crashes.
    /// </example>
    public class DspExtended : ClientModule
    {
        internal CommandsNextWrapper CNext { get; set; }
        
        private int _messageEvents;
        //private Watchdog _watchdog;

        internal DspExtended(DiscordClient client) : base(client)
        {
            CNext = new CommandsNextWrapper(client);
        }

#if !IS_LITE_VERSION
        /// <summary>
        /// Registers async listeners for an assembly
        /// </summary>
        /// <param name="assembly">the assembly to register</param>
        public void RegisterAssembly(Assembly assembly = null)
        {
            if (assembly == null) assembly = Assembly.GetEntryAssembly();
            AsyncListenerHandler.InstallListeners(Client, CNext.Value, assembly.GetTypes());
        }
#endif
        
        internal void EventErrorHandler(string evname, Exception ex, params object[] args)
        {
            if (evname.StartsWith("Invoke")) evname = evname.Substring("Invoke".Length);
            
            ExtensionMessageReceived?.Invoke(this, new EventErrorArgs(evname, ex));
            Client.DebugLogger.LogMessage(LogLevel.Error, "DspExtended", $"An {ex.GetType()} occured in {evname}.", DateTime.Now);
            try
            {
//                using (_watchdog.AcquireHandle())
//                {
                    ExtensionErrored(new ExtensionErrorEventArgs(Client, this)
                    {
                        EventName = evname,
                        Exception = ex
                    }).NoCapt().GetAwaiter().GetResult();
//                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"(Critical) An {e} occurred in the D#+ Extended error handling.");
            }
        }
        
        private Task OnMessageCreated(MessageCreateEventArgs e)
        {
            if (e.MentionedUsers.Contains(Client.CurrentUser))
            {
                return InvokeMentionReceived(new MentionReceivedEventArgs(Client)
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
        /// <remarks>
        /// The error handling for this event is done in the invoker rather than <see cref="AsyncEvent{T}"/>, to prevent
        /// infinite error loops.
        /// </remarks>
        public event AsyncEventHandler<ExtensionErrorEventArgs> ExtensionErrored;
        
        /// <summary>
        /// Fired whenever a message is logged by D#+ Extended.
        /// </summary>
        /// <remarks>
        /// <b>Hooking this event is optional, and should only be done if logging is disabled, either on the D#+ side or
        /// the extension side, otherwise you will get duplicate messages!</b>
        /// </remarks>
        public event Action<DspExtended, EventErrorArgs> ExtensionMessageReceived;
        
        /// <summary>
        /// Fired when the client's user gets mentioned.
        /// </summary>
        public event AsyncEventHandler<MentionReceivedEventArgs> MentionReceived // TODO could use InteractivityNext's smart lazy event hooks
        {
            add
            {
                lock (_mentionReceived)
                {
                    if (++_messageEvents != 0) Client.MessageCreated += OnMessageCreated;

                    _mentionReceived += value;
                }
            }
            remove
            {
                lock (_mentionReceived)
                {
                    if (--_messageEvents == 0) Client.MessageCreated -= OnMessageCreated;

                    // ReSharper disable once DelegateSubtraction (only applies when subtracting lists of delegates)
                    _mentionReceived -= value;
                }
            }
        }
        private AsyncEventHandler<MentionReceivedEventArgs> _mentionReceived; // this does not need to be initialized
        private Task InvokeMentionReceived(MentionReceivedEventArgs args) => Try(_mentionReceived, args);
        
        private async Task Try<T>(AsyncEventHandler<T> ev, T arg, [CallerMemberName] string callerName = "<unknown>")
            where T : AsyncEventArgs
        {
            Console.WriteLine($"Invoking event {ev} with argument: [{arg}]");
            try
            {
                if (ev != null)
                {
                    var delegates = ev.GetInvocationList();
                    foreach (var del in delegates)
                    {
                        if (arg.Handled) return;
                        
                        await ((AsyncEventHandler<T>)del)(arg);
                    }
                }
            }
            catch (Exception e)
            {
                EventErrorHandler(callerName, e, arg);
            }
        }
        
        /// <summary>
        /// Represents arguments for the <see cref="DspExtended.ExtensionMessageReceived"/> event.
        /// </summary>
        public readonly struct EventErrorArgs
        {
            public string EventName { get; }

            public string Message => Exception.Message;

            public DateTimeOffset Timestamp { get; }
            
            public Exception Exception { get; }

            internal EventErrorArgs(string eventName, Exception exception)
            {
                EventName = eventName;
                Exception = exception;
                Timestamp = DateTimeOffset.Now;
            }

            public override string ToString()
            {
                return $"[{Timestamp:yyyy-MM-dd HH:mm:ss zzz}] [{EventName}] {Message}:\n{Exception}";
            }
        }
    }
}