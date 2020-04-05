#if MODULE_SAMPLE
using DSharpPlus;

namespace HSNXT.DSharpPlus.Extended
{
    public static class SampleModuleLoader
    {
        public static SampleModule UseSampleModule(this DiscordClient client, SampleModuleConfiguration config) 
            => ModuleLoader.UseModule<SampleModule, SampleModuleConfiguration>(client, config);

        public static Task<IReadOnlyDictionary<int, SampleModule>> UseSampleModuleAsync(this DiscordShardedClient client) 
            => ModuleLoader.UseModuleAsync<SampleModule, SampleModuleConfiguration>(client);
    }

    public class SampleModuleConfiguration
    {
        // configuration
    }

    public class SampleModule : ClientModule<SampleModuleConfiguration>
    {
        public SampleModule(DiscordClient client, SampleModuleConfiguration config) : base(client, config)
        {
        }
    }
}

    public static class SampleModuleLoader
    {
        public static SampleModule UseSampleModule(this DiscordClient client) 
            => ModuleLoader.UseModule<SampleModule>(client);

        public static Task<IReadOnlyDictionary<int, SampleModule>> UseSampleModuleAsync(this DiscordShardedClient client) 
            => ModuleLoader.UseModuleAsync<SampleModule>(client);
    }

    public class SampleModuleConfiguration
    {
        // configuration
    }

    public class SampleModule : ClientModule
    {
        public SampleModule(DiscordClient client) : base(client)
        {
        }
    }

#endif