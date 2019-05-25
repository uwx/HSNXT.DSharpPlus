using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace HSNXT.DSharpPlus.CNextNotifier
{
    /// <summary>
    /// Can be applied onto a command overload method to automatically suppress an exception of a certain type or all
    /// types that extend it (unless a more specific attribute is present)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SuppressErrorAttribute : Attribute
    {
        /// <summary>
        /// The base type of the exception to handle with a message
        /// </summary>
        public Type ExceptionType { get; }
        
        /// <summary>
        /// The message to handle the exception with, either by sending a plain message formatted with arguments (the
        /// default) or by calling a custom handler defined when instantiating the extension. In the case of the default
        /// handler, the following substrings have special replacements:
        /// <list type="bullet">
        ///     <item><description>
        ///         {exceptionType} becomes the name of the type of the exception that was thrown
        ///         (<c>CommandErrorEventArgs.Exception.GetType().Name</c>)
        ///     </description></item>
        ///     <item><description>
        ///         {exception} becomes the message string of the exception that was thrown
        ///         (<c>CommandErrorEventArgs.Exception.Message</c>)
        ///     </description></item>
        ///     <item><description>
        ///         {trace} becomes the stack trace of the exception that was thrown
        ///         (<c>CommandErrorEventArgs.Exception.StackTrace</c>)
        ///     </description></item>
        ///     <item><description>
        ///         {traceFull} becomes the string value of the exception that was thrown, usually including the type of
        ///         the exception, exception message, stack trace, and inner exceptions
        ///         (<c>CommandErrorEventArgs.Exception.ToString()</c>)
        ///     </description></item>
        ///     <item><description>
        ///         {messageContent} becomes the content of the message of that invoked the command which failed
        ///         (<c>CommandContext.Message.Content</c>)
        ///     </description></item>
        ///     <item><description>
        ///         {command} becomes the qualified name (including all parents) of the command that failed
        ///         (<c>CommandContext.Command.QualifiedName</c>)
        ///     </description></item>
        ///     <item><description>
        ///         {args} becomes the raw argument string of the command invocation that failed
        ///         (<c>CommandContext.RawArgumentString</c>)
        ///     </description></item>
        ///     <item><description>
        ///         {invoker} becomes the formatted name of the user that invoked the failing command
        ///         (<c>{CommandContext.User.Username}#{CommandContext.User.Discriminator}</c>)
        ///     </description></item>
        ///     <item><description>
        ///         {@invoker} becomes the mention string for the user that invoked the failing command
        ///         (<c>CommandContext.User.Mention</c>)
        ///     </description></item>
        /// </list>
        /// </summary>
        public string Message { get; }
        
        public SuppressErrorAttribute(Type type, string message)
        {
            ExceptionType = type;
            Message = message;
        }
    }

    public static class ExtensionMethods
    {
        /// <summary>
        /// Enables the CNextNotifier module on this <see cref="DiscordClient"/>.
        /// </summary>
        /// <remarks>
        /// The extension must be registered after <see cref="CommandsNextExtension"/> is, but it should be registered
        /// prior to registering event handlers for the <see cref="CommandsNextExtension.CommandErrored"/>, if any.
        /// </remarks>
        /// <param name="client">Client to enable CNextNotifier for.</param>
        /// <param name="customHandler">A custom handler for suppressed messages, replacing the default handler.</param>
        /// <returns>Created <see cref="NotifierExtension"/>.</returns>
        public static NotifierExtension UseCNextNotifier(
            this DiscordClient client,
            Func<CommandErrorEventArgs, SuppressErrorAttribute, Task> customHandler = null
        )
        {
            if (client.GetExtension<NotifierExtension>() != null)
                throw new InvalidOperationException("CNextNotifier is already enabled for that client!");

            var ext = new NotifierExtension(customHandler);
            client.AddExtension(ext);
            return ext;
        }

        /// <summary>
        /// Enables the CNextNotifier module on all shards in this <see cref="DiscordShardedClient"/>.
        /// </summary>
        /// <remarks>
        /// The extension must be registered after <see cref="CommandsNextExtension"/> is, but it should be registered
        /// prior to registering event handlers for the <see cref="CommandsNextExtension.CommandErrored"/>, if any.
        /// </remarks>
        /// <param name="client">Client to enable CNextNotifier for.</param>
        /// <param name="customHandler">A custom handler for suppressed messages, replacing the default handler.</param>
        /// <returns>A dictionary of created <see cref="NotifierExtension"/> instances, indexed by shard id.</returns>
        public static IReadOnlyDictionary<int, NotifierExtension> UseCNextNotifier(
            this DiscordShardedClient client,
            Func<CommandErrorEventArgs, SuppressErrorAttribute, Task> customHandler = null
        )
        {
            var dictionary = new Dictionary<int, NotifierExtension>();
            foreach (var kvp in client.ShardClients)
            {
                var ext = kvp.Value.GetExtension<NotifierExtension>() ?? kvp.Value.UseCNextNotifier(customHandler);
                dictionary.Add(kvp.Value.ShardId, ext);
            }
            return dictionary;
        }
    }

    public class NotifierExtension : BaseExtension
    {
        private readonly Func<CommandErrorEventArgs, SuppressErrorAttribute, Task> _customHandler;

        private readonly Dictionary<Command, SuppressErrorAttribute[]> _suppressedCommands
            = new Dictionary<Command, SuppressErrorAttribute[]>();

        internal NotifierExtension(Func<CommandErrorEventArgs, SuppressErrorAttribute, Task> customHandler = null)
        {
            _customHandler = customHandler;
        }

        protected override void Setup(DiscordClient client)
        {
            var cnext = client.GetExtension<CommandsNextExtension>();
            if (cnext == null)
            {
                throw new ArgumentException("Please initialize CommandsNext before CNextNotifier!", nameof(client));
            }

            foreach (var kvp in cnext.RegisteredCommands)
            {
                RecurseCommands(kvp.Value);
            }
            cnext.CommandErrored += OnCommandErrored;
        }

        /// <summary>
        /// Releases all event handlers registered by this extension.
        /// </summary>
        public void TearDown()
        {
            Client.GetExtension<CommandsNextExtension>().CommandErrored -= OnCommandErrored;
        }

        private void RecurseCommands(Command command)
        {
            if (command is CommandGroup group)
            {
                foreach (var child in group.Children)
                {
                    RecurseCommands(child);
                }
            }
            else
            {
                var suppressErrorAttrs = command.CustomAttributes.OfType<SuppressErrorAttribute>().ToArray();
                if (suppressErrorAttrs.Length != 0)
                {
                    // sort attributes descending by inheritance so you can declare handlers for subtypes and supertypes
                    // of exceptions at the same time, without worrying about the order
                    _suppressedCommands[command] = suppressErrorAttrs
                        .OrderByDescending(attr =>
                        {
                            var depth = 0;
                            for (var type = attr.ExceptionType; type != null; type = type.BaseType)
                                depth++;
                            return depth;
                        })
                        .ToArray();
                }
            }
        }

        private Task OnCommandErrored(CommandErrorEventArgs args)
        {
            var type = args.GetType();

            if (!_suppressedCommands.TryGetValue(args.Command, out var attrs))
            {
                return Task.CompletedTask;
            }
            
            var firstAttr = attrs.FirstOrDefault(e => e.ExceptionType.IsAssignableFrom(type));
            if (firstAttr == null)
            {
                return Task.CompletedTask;
            }

            args.Handled = true;

            return _customHandler != null
                ? _customHandler(args, firstAttr)
                : args.Context.RespondAsync(ApplySpecialReplacements(firstAttr.Message, args));
        }

        /// <summary>
        /// Applies the default special format replacements to a string. The replacements are outlined in
        /// <see cref="SuppressErrorAttribute.Message"/>.
        /// </summary>
        /// <param name="str">The string to format</param>
        /// <param name="args">The event args that will be used to format the string</param>
        /// <returns>The formatted string</returns>
        public static string ApplySpecialReplacements(string str, CommandErrorEventArgs args)
        {
            var ctx = args.Context;
            return str
                .Replace("{exceptionType}", args.Exception.GetType().Name)
                .Replace("{exception}", args.Exception.Message)
                .Replace("{trace}", args.Exception.StackTrace)
                .Replace("{traceFull}", args.Exception.ToString())
                .Replace("{messageContent}", ctx.Message.Content)
                .Replace("{command}", ctx.Command.QualifiedName)
                .Replace("{args}", ctx.RawArgumentString)
                .Replace("{invoker}", $"{ctx.User.Username}#{ctx.User.Discriminator}")
                .Replace("{@invoker}", ctx.User.Mention);
        }
    }
}