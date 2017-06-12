﻿namespace DSharpPlus
{
    public class MessageReactionAddEventArgs : DiscordEventArgs
    {
        public DiscordMessage Message { get; internal set; }
        public DiscordChannel Channel { get; internal set; }
        public DiscordUser User { get; internal set; }
        public DiscordEmoji Emoji { get; internal set; }

        public MessageReactionAddEventArgs(DiscordClient client) : base(client) { }
    }
}
