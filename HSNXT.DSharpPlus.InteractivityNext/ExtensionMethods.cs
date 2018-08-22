using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpPlus.Interactivity
{
    public static class ExtensionMethods
    {
        public static InteractivityExtension UseInteractivity(this DiscordClient c, InteractivityConfiguration cfg)
        {
            if (c.GetExtension<InteractivityExtension>() != null)
                throw new Exception("Interactivity module is already enabled for this client!");

            var m = new InteractivityExtension(cfg);
            c.AddExtension(m);
            return m;
        }

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

        public static InteractivityExtension GetInteractivity(this DiscordClient c)
        {
            return c.GetExtension<InteractivityExtension>();
        }

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

        internal static IEnumerable<string> SplitWords(this string str, int chunkSize)
        {
            var words = str.Split(' ');
            var part = new StringBuilder();
            foreach (var word in words)
            {
                if (word.Length >= chunkSize)
                {
                    foreach (var chunk in Split(word, chunkSize))
                    {
                        yield return chunk;
                    }
                }
                else if (part.Length + word.Length < chunkSize)
                {
                    if (part.Length != 0)
                        part.Append(' ');

                    part.Append(word);
                }
                else
                {
                    yield return part.ToString();
                    part.Clear();
                    part.Append(word);
                }
            }
        }
    }
}