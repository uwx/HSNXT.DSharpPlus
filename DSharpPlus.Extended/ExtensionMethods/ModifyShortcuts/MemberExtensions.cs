﻿using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace DSharpPlus.Extended.ModifyShortcuts
{
    public static class MemberExtensions
    {
        /// <summary>
        /// Renames this member.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="nickname">Nickname to set for this member.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns></returns>
        public static Task RenameAsync(this DiscordMember @this, string nickname, string reason = null)
        {
            return @this.ModifyAsync(e =>
            {
                e.Nickname = nickname;
                e.AuditLogReason = reason;
            });
        }

        /// <summary>
        /// Move this member to a voice channel.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="voiceChannel">Voice channel to put the member into.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns></returns>
        public static Task MoveToAsync(this DiscordMember @this, DiscordChannel voiceChannel,
            string reason = null)
        {
            if (voiceChannel.Type != ChannelType.Voice)
            {
                throw new ArgumentException("Given channel is not a voice channel.", nameof(voiceChannel));
            }

            return @this.ModifyAsync(e =>
            {
                e.VoiceChannel = voiceChannel;
                e.AuditLogReason = reason;
            });
        }
    }
}