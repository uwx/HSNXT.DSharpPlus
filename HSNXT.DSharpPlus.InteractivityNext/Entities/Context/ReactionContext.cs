using DSharpPlus.Entities;

namespace DSharpPlus.Interactivity
{
    public class ReactionContext : InteractivityContext
    {
        public DiscordMessage Message { get; }

        public DiscordUser User => Message.Author;

        public DiscordChannel Channel => Message.Channel;

        public DiscordGuild Guild => Channel.Guild;

        public DiscordEmoji Emoji { get; internal set; }

        public ReactionContext(InteractivityExtension interactivity, DiscordEmoji emoji, DiscordMessage message) 
            : base(interactivity)
        {
            Message = message;
            Emoji = emoji;
        }
    }
}
