using DSharpPlus;

namespace HSNXT.DSharpPlus.InteractivityNext
{
    public class InteractivityContext
    {
        public InteractivityExtension Interactivity { get; }

        public DiscordClient Client => Interactivity.Client;

        protected InteractivityContext(InteractivityExtension interactivity)
        {
            Interactivity = interactivity;
        }
    }
}