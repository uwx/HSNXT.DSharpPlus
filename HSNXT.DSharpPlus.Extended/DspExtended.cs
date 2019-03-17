using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using HSNXT.DSharpPlus.Extended.EventArgs;
using HSNXT.DSharpPlus.Extended.AsyncListeners;

namespace HSNXT.DSharpPlus.Extended
{
    /// <summary>
    /// Extension class to install the <see cref="DspExtended"/> module.
    /// </summary>
    public static class DspExtendedLoader
    {
        /// <summary>
        /// Registers the D#+ Extended module on the client. You can also use the returned value to change settings,
        /// hook events etc. Note that this is not required to be able to use the extension methods.
        /// </summary>
        public static DspExtended UseDspExtended(this DiscordClient client) 
            => ModuleLoader.UseModule<DspExtended>(client);
        
        /// <summary>
        /// Registers an independent instance of the D#+ Extended module on each shard. You can also use the returned
        /// values to change settings, hook events etc. Note that this is not required to be able to use the extension
        /// methods.
        /// </summary>
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
    public class DspExtended : ClientModule, IDisposable
    {
        internal CommandsNextWrapper CNext { get; set; }
        internal DspExtendedLogger Log { get; }
        
        private readonly Watchdog _watchdog;
        
        // refcount of message event handlers
        private int _messageEvents;

        /// <summary>
        /// Sets the log sources for D#+ Extended. This can be set by the library consumer to one or more
        /// <see cref="LogSource"/> flags ORed together to select which sources to log, or <see cref="LogSource.None"/>
        /// to disable logging entirely.
        /// </summary>
        public LogSource LogSources { internal get; set; } = LogSource.Default;

        internal DspExtended(DiscordClient client) : base(client)
        {
            Log = new DspExtendedLogger(this);
            CNext = new CommandsNextWrapper(Client);

            _watchdog = new Watchdog((handleSrc, reason, thread, time) =>
            {
                reason = reason ?? $"The thread at {thread.Name ?? "<no name>"} with ID {thread.ManagedThreadId}";
                
                Log[LogSource.TimeoutExceeded]($"{reason} has taken at least {time} to complete its current operation. This may be a deadlock");
            }, log: Log[LogSource.WatchdogFine]);
        }

        /// <summary>
        /// Registers async listeners for every type (with matching method attributes) in an assembly. 
        /// </summary>
        /// <param name="assembly">
        /// The assembly to register. If not specified, defaults to the assembly containing the current program's entry
        /// point.
        /// </param>
        /// <remarks>
        /// This method should be used with care and is usually not recommended as it performs an iteration through
        /// every single type in the assembly. Consider an overload that accepts an individual type such as
        /// <see cref="Register"/> or <see cref="Register{T}"/>.
        /// </remarks>
        public void RegisterAssembly(Assembly assembly = null)
        {
            if (assembly == null) assembly = Assembly.GetEntryAssembly();
            foreach (var type in assembly.GetTypes())
            {
                Register(type);
            }
        }

        /// <summary>
        /// Registers async listeners for a type based on a generic type parameter.
        /// </summary>
        /// <typeparam name="T">The type to register</typeparam>
        /// <remarks>
        /// This method will do nothing if the specified type has no methods marked with an
        /// <see cref="AsyncListenerAttribute"/>.
        /// </remarks>
        public void Register<T>() => Register(typeof(T));

        /// <summary>
        /// Registers async listeners for a type based on a <see cref="Type"/> instance.
        /// </summary>
        /// <param name="type">The type to register</param>
        /// <remarks>
        /// This method will do nothing if the specified type has no methods marked with an
        /// <see cref="AsyncListenerAttribute"/>.
        /// </remarks>
        public void Register(Type type) => AsyncListenerHandler.InstallListeners(this, type);

        internal void EventErrorHandler(string evname, Exception ex, params object[] args)
        {
            if (evname.StartsWith("Invoke")) evname = evname.Substring("Invoke".Length);

            using (_watchdog.AcquireHandle(this, evname + " event error handler"))
            {
                Log[LogSource.EventError]($"An {ex.GetType()} occured in {evname}.");
                try
                {
                    ExtensionErrored(new ExtensionErrorEventArgs(this)
                    {
                        EventName = evname,
                        Exception = ex
                    }).NoCapt().GetAwaiter().GetResult();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"(Critical) An {e} occurred in the D#+ Extended error handling.");
                }
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
        /// Fired whenever an error occurs in an async event listener (a method tagged with
        /// <see cref="AsyncListenerAttribute"/>).
        /// </summary>
        public event AsyncEventHandler<ExtensionErrorEventArgs> AsyncEventErrored;
        internal Task InvokeAsyncEventErrored(ExtensionErrorEventArgs args) => Try(AsyncEventErrored, args);
        
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
            Log[LogSource.EventInvoke]($"Invoking event {ev} with argument: [{arg}]");
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

        public void Dispose()
        {
            _watchdog?.Dispose();
        }
    }

    /// <summary>
    /// Internal logging class that allows the library consumer to have in-depth control over what gets logged by D#+
    /// Extended.
    /// </summary>
    internal class DspExtendedLogger
    {
        private static readonly IReadOnlyDictionary<LogSource, (string name, LogLevel level)> SourceInfos =
            Enum.GetNames(typeof(LogSource))
                .ToDictionary(
                    name =>
                        Enum.TryParse<LogSource>(name, out var result)
                            ? result
                            : throw new InvalidOperationException("Did not find enum value (should never happen)"),
                    name =>
                    (
                        name,
                        typeof(LogSource)
                            .GetField(name, BindingFlags.Public | BindingFlags.Static)
                            ?.GetCustomAttribute<SeverityAttribute>()
                            .Level
                        ?? throw new InvalidOperationException("Did not find enum member (should never happen)")
                    )
                );
        
        private readonly DspExtended _dspExtended;
        private DebugLogger Logger => _dspExtended.Client.DebugLogger;
        private LogSource LogSources => _dspExtended.LogSources;

        public Action<string> this[LogSource source] => message =>
        {
            if (!LogSources.HasFlag(source)) return;
            var (name, level) = SourceInfos[source];
            Logger.LogMessage(level, $"D#+Ex|{name}", message, DateTime.Now);
        };
        
        public DspExtendedLogger(DspExtended dspExtended)
        {
            _dspExtended = dspExtended;
        }
    }

    /// <summary>
    /// A type of console message in D#+ Extended. This can be used to control what gets logged by the library through.
    /// </summary>
    [Flags]
    public enum LogSource
    {
        /// <summary>
        /// Don't log anything. This only affects log messages generated by D#+ Extended.
        /// </summary>
        [Severity(LogLevel.Debug)] None = 0,
        /// <summary>
        /// Log everything from every category. Not recommended unless you want your console to get spammed.
        /// </summary>
        [Severity(LogLevel.Debug)] All = byte.MaxValue,
        /// <summary>
        /// Use the default logging settings. This is implementation-defined, but usually will log everything that isn't
        /// debug info.
        /// </summary>
        [Severity(LogLevel.Debug)] Default = EventError | TimeoutExceeded | AsyncEventError,
        
        // actual categories
        
        /// <summary>
        /// Logged when an error is encountered in a D#+ Extended event handler. Does not affect base D#+'s events, and
        /// does not stop <see cref="DspExtended.ExtensionErrored"/> from being called.
        /// </summary>
        [Severity(LogLevel.Error)] EventError = 1 << 0,
        /// <summary>
        /// Logged when a D#+ Extended event handler takes over 10 seconds to complete. Does not apply to async listener
        /// methods.
        /// </summary>
        [Severity(LogLevel.Warning)] TimeoutExceeded = 1 << 1,
        /// <summary>
        /// Logged when a D#+ Extended event is invoked. Does not apply to events from base D#+, or to async listener
        /// methods.
        /// </summary>
        [Severity(LogLevel.Debug)] EventInvoke = 1 << 2,
        /// <summary>
        /// Logged when an async listener method (one that is tagged with <see cref="AsyncListenerAttribute"/> and has
        /// been registered through <see cref="DspExtended"/>.Register/RegisterAssembly) is invoked.
        /// </summary>
        [Severity(LogLevel.Debug)] AsyncEventInvoke = 1 << 3,
        /// <summary>
        /// Logged when an error is encountered in an async listener method (one that is tagged with
        /// <see cref="AsyncListenerAttribute"/> and has been registered through
        /// <see cref="DspExtended"/>.Register/RegisterAssembly).
        /// </summary>
        [Severity(LogLevel.Error)] AsyncEventError = 1 << 4,
        /// <summary>
        /// Used for fine debug logging for the <see cref="Watchdog"/>. This usually does not need to be enabled unless
        /// you are testing the watchdog.
        /// </summary>
        [Severity(LogLevel.Debug)] WatchdogFine = 1 << 5,
    }

    /// <summary>
    /// Marks the severity of a <see cref="LogSource"/> message.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    internal class SeverityAttribute : Attribute
    {
        public LogLevel Level { get; }

        public SeverityAttribute(LogLevel level)
        {
            Level = level;
        }
    }
}