using DSharpPlus;

namespace HSNXT.DSharpPlus.Extended
{
    /// <summary>
    /// A base type for modules without a configuration parameter. Children of this class must provide a constructor
    /// taking a single <see cref="DiscordClient"/> parameter that calls the base constructor with the same signature.
    /// Different or missing constructors will cause a runtime error.
    /// </summary>
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
    
    /// <summary>
    /// A base type for modules with a configuration parameter. Children of this class must provide a constructor taking
    /// two parameters, a <see cref="DiscordClient"/> and one with the same type as
    /// <typeparamref name="TConfiguration"/>, and it must call the base constructor with the same signature.
    /// Different or missing constructors will cause a runtime error.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of the configuration class of the module</typeparam>
    public abstract class ClientModule<TConfiguration> : BaseExtension
    {
        /// <summary>
        /// The settings for this module, as provided when it was instantiated
        /// </summary>
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