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

namespace HSNXT.DSharpPlus.CNextNotifier
{
    internal struct CmdSuppressPair
    {
        public bool Set;
        public string Name { get; set; }
        public string FirstGroup { get; set; }
        public string[] OtherGroups { get; set; }
        public SuppressErrorAttribute Attr { get; set; }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class SuppressErrorAttribute : Attribute
    {
        public string Message { get; }
        public Type ExceptionType { get; }

        public SuppressErrorAttribute(Type type, string message)
        {
            this.ExceptionType = type;
            this.Message = message;
        }
    }

    public static class ExtensionMethods
    {
        /// <summary>
        /// Enables CommandsNext module on this <see cref="T:DSharpPlus.DiscordClient" />.
        /// </summary>
        /// <param name="client">Client to enable CNextNotifier for.</param>
        /// <param name="assembly">Assembly to detect commands from. Defaults to the assembly calling this method.</param>
        /// <returns>Created <see cref="T:DSharpPlus.CommandsNext.CommandsNextExtension" />.</returns>
        public static CNextNotifier UseCNextNotifier(this DiscordClient client, Assembly assembly = null)
        {
            if (client.GetExtension<CNextNotifier>() != null)
                throw new InvalidOperationException("CNextNotifier is already enabled for that client!");

            var ext = new CNextNotifier(assembly);
            client.AddExtension(ext);
            return ext;
        }

        /// <summary>
        /// Enables CNextNotifier module on all shards in this <see cref="T:DSharpPlus.DiscordShardedClient" />.
        /// </summary>
        /// <param name="client">Client to enable CNextNotifier for.</param>
        /// <param name="assembly">Assembly to detect commands from. Defaults to the assembly calling this method.</param>
        /// <returns>A dictionary of created <see cref="T:DSharpPlus.CommandsNext.CommandsNextExtension" />, indexed by shard id.</returns>
        public static IReadOnlyDictionary<int, CNextNotifier> UseCNextNotifier(this DiscordShardedClient client,
            Assembly assembly = null)
        {
            var dictionary = new Dictionary<int, CNextNotifier>();
            foreach (var aclient in client.ShardClients.Select(e => e.Value))
            {
                var ext = aclient.GetExtension<CNextNotifier>() ?? aclient.UseCNextNotifier(assembly);
                dictionary.Add(aclient.ShardId, ext);
            }
            return new ReadOnlyDictionary<int, CNextNotifier>(dictionary);
        }
    }

    public class CNextNotifier : BaseExtension
    {
        private readonly CustomDictionary<Command, CmdSuppressPair> _cache =
            new CustomDictionary<Command, CmdSuppressPair>();

        private readonly SemaphoreSlim _cacheAccessorSemaphore = new SemaphoreSlim(1, 1);
        private CmdSuppressPair[] _commandsToSuppress;
        private readonly Assembly _assembly;

        public CNextNotifier(Assembly assembly = null)
        {
            _assembly = assembly ?? Assembly.GetCallingAssembly();
        }

        protected override void Setup(DiscordClient client)
        {
            _commandsToSuppress = _assembly.GetTypes()
                .SelectMany(t => t.GetMethods())
                .Select(m =>
                    (
                    m,
                    cmd: m.GetCustomAttribute<CommandAttribute>(),
                    attr: m.GetCustomAttribute<SuppressErrorAttribute>()
                    )
                ).Where(e => e.cmd != null && e.attr != null)
                .Select(e =>
                {
                    var list = new List<string>();
                    var group = e.m.DeclaringType;
                    while ((group = group.DeclaringType) != null)
                    {
                        list.Add(group.GetCustomAttribute<GroupAttribute>()?.Name);
                    }
                    return new CmdSuppressPair()
                    {
                        Set = true,
                        Name = e.cmd.Name,
                        FirstGroup = e.m.DeclaringType.GetCustomAttribute<GroupAttribute>()?.Name,
                        OtherGroups = list.ToArray(),
                        Attr = e.attr,
                    };
                }).ToArray();

            var cnext = client.GetExtension<CommandsNextExtension>();
            if (cnext == null)
            {
                throw new ArgumentException(nameof(client), "Please initialize CommandsNext before CNextNotifier!");
            }
            cnext.CommandErrored += OnCommandErrored;
        }

        private async Task OnCommandErrored(CommandErrorEventArgs e)
        {
            var type = e.GetType();
            await _cacheAccessorSemaphore.WaitAsync();

            var pos = _cache.InitOrGetPosition(e.Command);
            var curr = _cache.GetAtPosition(pos);

            // it's a struct so we can't do 'null' so we just use a boolean
            if (curr.Set)
            {
                await SendErrorMessage(e, curr);
                return;
            }

            var entry = _commandsToSuppress.FirstOrDefault(c =>
            {
                // wrong type or wrong command name
                if (c.Attr.ExceptionType != type || e.Command.Name != c.Name)
                {
                    return false;
                }
                // command has no immediate parent
                CommandGroup parent;
                if ((parent = e.Command.Parent) == null)
                {
                    // if both are null, we found a match: both have no immediate parents.
                    // if one is null but not the other, we found a false: one has an immediate parent, but the other doesn't.
                    return c.FirstGroup == null;
                }
                // check immediate parent's name
                if (parent.Name != c.FirstGroup)
                {
                    return false;
                }
                // build the group hierarchy and check for it.
                var list = new List<string>();
                while ((parent = parent.Parent) != null)
                {
                    list.Add(parent.Name);
                }
                var groupHierarchy = list.ToArray();
                return groupHierarchy.Equals(c.OtherGroups);
            });

            // if this tuple is null, there is nothing we can do.
            // **there is nothing we can do.**
            if (entry.Equals(default))
            {
                // release the semaphore. no need to do anything with the dictionary here.
                _cacheAccessorSemaphore.Release();
                return;
            }

            // cache this entry at position.
            _cache.StoreAtPosition(pos, entry);

            _cacheAccessorSemaphore.Release();

            await SendErrorMessage(e, curr);
        }

        private static Task SendErrorMessage(CommandErrorEventArgs e, CmdSuppressPair suppressPair)
        {
            e.Handled = true;
            return e.Context.RespondAsync(suppressPair.Attr.Message.Replace("{exception}", e.Exception.Message));
        }
    }
}