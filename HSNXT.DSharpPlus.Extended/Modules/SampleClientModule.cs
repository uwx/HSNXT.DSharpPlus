#if MODULE_SAMPLE
using DSharpPlus;

namespace HSNXT.DSharpPlus.Extended
{
    public static class SampleClientModuleLoader
    {
        public static SampleClientModule UseSampleModule(this DiscordClient client, object config) 
            => ModuleLoader.UseModule<SampleClientModule, object>(client, config);
    }

    public class SampleClientModule : ClientModule<object>
    {
        public SampleClientModule(DiscordClient client, object config) : base(client, config)
        {
        }
    }
}
#endif