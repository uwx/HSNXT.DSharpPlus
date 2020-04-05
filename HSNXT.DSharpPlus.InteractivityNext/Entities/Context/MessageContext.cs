using System.Collections.Generic;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.InteractivityNext
{
    public class MessageContext : InteractivityContext
    {
        public DiscordMessage Message { get; }

        public DiscordUser User => Message.Author;

        public DiscordChannel Channel => Message.Channel;

        public DiscordGuild Guild => Channel.Guild;

        public IReadOnlyList<DiscordChannel> MentionedChannels => Message.MentionedChannels;

        public IReadOnlyList<DiscordRole> MentionedRoles => Message.MentionedRoles;

        public IReadOnlyList<DiscordUser> MentionedUsers => Message.MentionedUsers;

        public MessageContext(InteractivityExtension interactivity, DiscordMessage message) : base(interactivity)
        {
            Message = message;
        }
    }
}
