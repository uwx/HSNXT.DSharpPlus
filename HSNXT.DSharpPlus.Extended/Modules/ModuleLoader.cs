using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus;

namespace HSNXT.DSharpPlus.Extended
{
    /// <summary>
    /// The main class for a client module. A module is just a layer of abstraction to make creating client extensions
    /// less of a burden, as it gives you access to the client in the constructor rather than an initialization method.
    /// </summary>
    /// <example>
    /// Without configuration:
    /// <code>
    /// public static class SampleModuleLoader
    /// {
    ///     public static SampleModule UseSampleModule(this DiscordClient client) 
    ///         => ModuleLoader.UseModule&lt;SampleModule>(client);
    /// 
    ///     public static Task&lt;IReadOnlyDictionary&lt;int, SampleModule>> UseSampleModuleAsync(this DiscordShardedClient client) 
    ///         => ModuleLoader.UseModuleAsync&lt;SampleModule>(client);
    /// }
    ///
    /// public class SampleModuleConfiguration
    /// {
    ///     // configuration
    /// }
    ///
    /// public class SampleModule : ClientModule
    /// {
    ///     public SampleModule(DiscordClient client) : base(client)
    ///     {
    ///     }
    /// }
    /// </code>
    ///
    /// With configuration:
    /// <code>
    /// public static class SampleModuleLoader
    /// {
    ///     public static SampleModule UseSampleModule(this DiscordClient client, SampleModuleConfiguration config) 
    ///         => ModuleLoader.UseModule&lt;SampleModule, SampleModuleConfiguration>(client, config);
    ///     
    ///     public static Task&lt;IReadOnlyDictionary&lt;int, SampleModule>> UseSampleModuleAsync(this DiscordShardedClient client) 
    ///         => ModuleLoader.UseModuleAsync&lt;SampleModule, SampleModuleConfiguration>(client);
    /// }
    ///
    /// public class SampleModuleConfiguration
    /// {
    ///     // configuration class
    /// }
    ///
    /// public class SampleModule : ClientModule&lt;SampleModuleConfiguration>
    /// {
    ///     public SampleModule(DiscordClient client, SampleModuleConfiguration config) : base(client, config)
    ///     {
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <remarks>
    /// The module classes must provide a constructor identical to either or both of the ones provided in the example,
    /// or you will get a runtime error! This behavior might change to a compile-time check in the future if an update
    /// to the language allows it.
    /// </remarks>
    public static class ModuleLoader
    {
        /// <summary>
        /// Instantiates a module for a client. Avoid calling this method directly unless you are making your own
        /// module.
        /// </summary>
        /// <param name="client">The client to register the module to</param>
        /// <typeparam name="TModule">The type of the module to instantiate.</typeparam>
        /// <example>
        /// </example>
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
        
        /// <summary>
        /// Instantiates a module once for every shard in the client. Avoid calling this method directly unless you are
        /// making your own module.
        /// </summary>
        /// <param name="client"></param>
        /// <typeparam name="TModule"></typeparam>
        /// <returns></returns>
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