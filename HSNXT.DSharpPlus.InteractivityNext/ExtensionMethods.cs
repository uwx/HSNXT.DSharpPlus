using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DSharpPlus.Interactivity
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Installs the Interactivity extension for this client.
        /// </summary>
        /// <param name="c">The client to install the extension for</param>
        /// <param name="cfg">The configuration for the extension</param>
        /// <returns>The newly created extension</returns>
        /// <exception cref="InvalidOperationException">
        /// If the Interactivity extension has already been enabled for this client
        /// </exception>
        public static InteractivityExtension UseInteractivity(this DiscordClient c, InteractivityConfiguration cfg)
        {
            if (c.GetExtension<InteractivityExtension>() != null)
                throw new InvalidOperationException("Interactivity module is already enabled for this client!");

            var m = new InteractivityExtension(cfg);
            c.AddExtension(m);
            return m;
        }

        /// <summary>
        /// Installs the Interactivity extension for every shard in this client.
        /// </summary>
        /// <param name="c">The client to install the extension for</param>
        /// <param name="cfg">The configuration for the extension</param>
        /// <returns>A mapping of shard ids to extension instances</returns>
        /// <exception cref="InvalidOperationException">
        /// If the Interactivity extension has already been enabled for this client
        /// </exception>
        public static async Task<IReadOnlyDictionary<int, InteractivityExtension>> UseInteractivityAsync(this DiscordShardedClient c, InteractivityConfiguration cfg)
        {
            var modules = new Dictionary<int, InteractivityExtension>();
            await c.InitializeShardsAsync().ConfigureAwait(false);

            foreach (var shard in c.ShardClients.Select(xkvp => xkvp.Value))
            {
                var m = shard.GetExtension<InteractivityExtension>() ?? shard.UseInteractivity(cfg);

                modules[shard.ShardId] = m;
            }

            return new ReadOnlyDictionary<int, InteractivityExtension>(modules);
        }

        /// <summary>
        /// Gets the active Interactivity extension for this client.
        /// </summary>
        /// <param name="c">Client to get the Interactivity extension from.</param>
        /// <returns>The extension, or null if not activated.</returns>
        public static InteractivityExtension GetInteractivity(this DiscordClient c)
        {
            return c.GetExtension<InteractivityExtension>();
        }

        /// <summary>
        /// Gets the active CommandsNext modules for all shards in this client.
        /// </summary>
        /// <param name="c">Client to get the Interactivity extensions from.</param>
        /// <returns>A mapping of shard ids to extension instances</returns>
        public static IReadOnlyDictionary<int, InteractivityExtension> GetInteractivity(this DiscordShardedClient c)
        {
            c.InitializeShardsAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            var modules = c.ShardClients
                .Select(xkvp => xkvp.Value)
                .ToDictionary(shard => shard.ShardId, shard => shard.GetExtension<InteractivityExtension>());

            return new ReadOnlyDictionary<int, InteractivityExtension>(modules);
        }

        internal static IEnumerable<string> Split(this string str, int chunkSize)
        {
            var len = str.Length;
            var i = 0;

            while (i < len)
            {
                var size = Math.Min(len - i, chunkSize);
                yield return str.Substring(i, size);
                i += size;
            }
        }
    }
}