using System.Collections.Generic;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using JetBrains.Annotations;

namespace HSNXT.DSharpPlus.Extended
{
    /// <summary>
    /// Represents arguments for the <see cref="DspExtended.MentionReceived"/> event.
    /// </summary>
    [PublicAPI]
    public class MentionReceivedEventArgs : DiscordEventArgs
    {
        /// <summary>Gets the mention message that was created.</summary>
        public DiscordMessage Message { get; internal set; }

        /// <summary>Gets the channel the mention message message belongs to.</summary>
        public DiscordChannel Channel => Message.Channel;

        /// <summary>Gets the guild the mention message message belongs to.</summary>
        public DiscordGuild Guild => Channel.Guild;

        /// <summary>Gets the author of the mention message.</summary>
        public DiscordUser Author => Message.Author;

        /// <summary>Gets the collection of mentioned users.</summary>
        public IReadOnlyList<DiscordUser> MentionedUsers { get; internal set; }

        /// <summary>Gets the collection of mentioned roles.</summary>
        public IReadOnlyList<DiscordRole> MentionedRoles { get; internal set; }

        /// <summary>Gets the collection of mentioned channels.</summary>
        public IReadOnlyList<DiscordChannel> MentionedChannels { get; internal set; }

        public MentionReceivedEventArgs(DiscordClient client) : base(client)
        {
        }
    }
}