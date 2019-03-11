using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus;

namespace HSNXT.DSharpPlus.Extended
{
    /// <summary>
    /// 
    /// </summary>
    public static class ModuleLoader
    {
        public static TModule UseModule<TModule>(
            DiscordClient client
        ) where TModule : ClientModule
        {
            if (client.GetExtension<TModule>() != null)
                throw new InvalidOperationException($"{typeof(TModule)} is already enabled for this client.");

            var extension = Activator.CreateInstance<TModule>();
            client.AddExtension(extension);
            return extension;
        }
        
        public static async Task<IReadOnlyDictionary<int, TModule>> UseModuleAsync<TModule>(
            DiscordShardedClient client
        ) where TModule : ClientModule
        {
            var modules = new Dictionary<int, TModule>();
            await ReflectionUtils.InitializeShardsAsync(client).NoCapt();

            var initializer = ReflectionUtils.NewModule<TModule>();
            foreach (var shard in client.ShardClients.Values)
            {
                var module = initializer(shard);
                shard.AddExtension(module);
                modules[shard.ShardId] = module;
            }

            return modules;
        }
        
        public static TModule UseModule<TModule, TConfiguration>(
            DiscordClient client, 
            TConfiguration config
        ) where TModule : ClientModule<TConfiguration>
        {
            if (client.GetExtension<TModule>() != null)
                throw new InvalidOperationException($"{typeof(TModule)} is already enabled for this client.");

            var extension = (TModule)Activator.CreateInstance(typeof(TModule), config);
            client.AddExtension(extension);
            return extension;
        }
        
        public static async Task<IReadOnlyDictionary<int, TModule>> UseModuleAsync<TModule, TConfiguration>(
            DiscordShardedClient client,
            TConfiguration config
        ) where TModule : ClientModule<TConfiguration>
        {
            var modules = new Dictionary<int, TModule>();
            await ReflectionUtils.InitializeShardsAsync(client).NoCapt();

            var initializer = ReflectionUtils.NewModule<TModule, TConfiguration>();
            foreach (var shard in client.ShardClients.Values)
            {
                var module = initializer(shard, config);
                shard.AddExtension(module);
                modules[shard.ShardId] = module;
            }

            return modules;
        }
    }
}