using DSharpPlus;

namespace HSNXT.DSharpPlus.Extended
{
    public abstract class ClientModule : BaseExtension
    {
        protected ClientModule(DiscordClient client)
        {
            Client = client;
        }
        
        protected override void Setup(DiscordClient client)
        {
            // empty. we set up the client directly in the constructor, to give extensions full access to everything
            // where they can assign readonly variables and the like. the setup pattern doesn't really serve any purpose
            // since it gets called as soon as AddExtension gets called anyway.
        }
    }
    
    public abstract class ClientModule<TConfiguration> : BaseExtension
    {
        protected TConfiguration Configuration { get; }

        protected ClientModule(DiscordClient client, TConfiguration config)
        {
            Client = client;
            Configuration = config;
        }
        
        protected override void Setup(DiscordClient client)
        {
            // empty. we set up the client directly in the constructor, to give extensions full access to everything
            // where they can assign readonly variables and the like. the setup pattern doesn't really serve any purpose
            // since it gets called as soon as AddExtension gets called anyway.
        }
    }
}