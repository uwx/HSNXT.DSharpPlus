using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace DSharpPlus.Extended
{
    public static class DiscordMemberExtensions
    {
        /// <summary>
        /// Renames this member.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="nickname">Nickname to set for this member.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns></returns>
        public static async Task RenameAsync(this DiscordMember @this, string nickname, string reason = null)
        {
            await @this.ModifyAsync(nickname, null, null, null, null, reason).ConfigureAwait(false);
        }

        /// <summary>
        /// Move this member to a voice channel.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="voiceChannel">Voice channel to put the member into.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns></returns>
        public static async Task MoveToAsync(this DiscordMember @this, DiscordChannel voiceChannel,
            string reason = null)
        {
            if (voiceChannel.Type != ChannelType.Voice)
                throw new ArgumentException("Given channel is not a voice channel.", nameof(voiceChannel));

            await @this.ModifyAsync(null, null, null, null, voiceChannel, reason).ConfigureAwait(false);
        }
    }
}