using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using JetBrains.Annotations;

namespace HSNXT.DSharpPlus.CNextNotifier
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SuppressErrorAttribute : Attribute
    {
        public Type ExceptionType { get; }
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
        /// <param name="client">Client to enable CNextNotifier for.</param>
        /// <returns>Created <see cref="CommandsNextExtension"/>.</returns>
        public static CNextNotifier UseCNextNotifier(this DiscordClient client)
        {
            if (client.GetExtension<CNextNotifier>() != null)
                throw new InvalidOperationException("CNextNotifier is already enabled for that client!");

            var ext = new CNextNotifier();
            client.AddExtension(ext);
            return ext;
        }

        /// <summary>
        /// Enables the CNextNotifier module on all shards in this <see cref="DiscordShardedClient"/>.
        /// </summary>
        /// <param name="client">Client to enable CNextNotifier for.</param>
        /// <returns>A dictionary of created <see cref="CommandsNextExtension"/>, indexed by shard id.</returns>
        public static IReadOnlyDictionary<int, CNextNotifier> UseCNextNotifier(this DiscordShardedClient client)
        {
            var dictionary = new Dictionary<int, CNextNotifier>();
            foreach (var aclient in client.ShardClients.Select(e => e.Value))
            {
                var ext = aclient.GetExtension<CNextNotifier>() ?? aclient.UseCNextNotifier();
                dictionary.Add(aclient.ShardId, ext);
            }
            return dictionary;
        }
    }

    public class CNextNotifier : BaseExtension
    {
        private readonly Dictionary<Command, SuppressErrorAttribute[]> _suppressedCommands
            = new Dictionary<Command, SuppressErrorAttribute[]>();

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
                    _suppressedCommands[command] = suppressErrorAttrs;
                }
            }
        }

        private Task OnCommandErrored(CommandErrorEventArgs args)
        {
            var type = args.GetType();

            if (!_suppressedCommands.TryGetValue(args.Command, out var attrs)) return Task.CompletedTask;
            
            var firstAttr = attrs.FirstOrDefault(e => e.ExceptionType.IsAssignableFrom(type));
            if (firstAttr == null) return Task.CompletedTask;
            
            args.Handled = true;
            
            var ctx = args.Context;
            return ctx.RespondAsync(
                firstAttr.Message
                    .Replace("{exception}", args.Exception.Message)
                    .Replace("{trace}", args.Exception.StackTrace)
                    .Replace("{traceFull}", args.Exception.ToString())
                    .Replace("{messageContent}", ctx.Message.Content)
                    .Replace("{command}", ctx.Command.QualifiedName)
                    .Replace("{args}", ctx.RawArgumentString)
                    .Replace("{invoker}", $"{ctx.User.Username}#{ctx.User.Discriminator}")
                    .Replace("{@invoker}", ctx.User.Mention)
            );

        }
    }
}