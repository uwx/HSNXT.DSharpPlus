﻿using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace DSharpPlus.Extended
{
    public static class DiscordChannelExtensions
    {
        /// <summary>
        /// Renames the current channel.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="name">New name for the channel.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns></returns>
        public static Task RenameAsync(this DiscordChannel @this, string name, string reason = null)
        {
            if (@this.Guild == null)
                throw new InvalidOperationException("Cannot rename non-guild channels.");

            return @this.ModifyAsync(name, null, null, default, null, null, reason);
        }

        /// <summary>
        /// Sets this text channel's topic.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="topic">New topic for the channel.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns></returns>
        public static Task SetTopicAsync(this DiscordChannel @this, string topic, string reason = null)
        {
            if (@this.Guild == null)
                throw new InvalidOperationException("Cannot set topic of non-guild channels.");
            if (@this.Type != ChannelType.Text)
                throw new InvalidOperationException("Cannot set topic of non-text channels.");

            return @this.ModifyAsync(null, null, topic, default, null, null, reason);
        }

        /// <summary>
        /// Sets this channel's parent category.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="parent">Category to put this channel in.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns></returns>
        public static Task SetParentAsync(this DiscordChannel @this, Optional<DiscordChannel> parent,
            string reason = null)
        {
            if (@this.Guild == null)
                throw new InvalidOperationException("Cannot set parent of non-guild channels.");

            return @this.ModifyAsync(null, null, null, parent, null, null, reason);
        }

        /// <summary>
        /// Sets this voice channel's bitrate in kbps.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="bitrate">New voice bitrate for the channel.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns></returns>
        public static Task SetBitrateAsync(this DiscordChannel @this, int bitrate, string reason = null)
        {
            if (@this.Guild == null)
                throw new InvalidOperationException("Cannot set user limit of non-guild channels.");
            if (@this.Type != ChannelType.Voice)
                throw new InvalidOperationException("Cannot set user limit of non-voice channels.");

            return @this.ModifyAsync(null, null, null, default, bitrate, null, reason);
        }

        /// <summary>
        /// Sets this voice channel's connected user limit.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="userLimit">New user limit for the channel.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns></returns>
        public static Task SetUserLimitAsync(this DiscordChannel @this, int userLimit, string reason = null)
        {
            if (@this.Guild == null)
                throw new InvalidOperationException("Cannot set user limit of non-guild channels.");
            if (@this.Type != ChannelType.Voice)
                throw new InvalidOperationException("Cannot set user limit of non-voice channels.");

            return @this.ModifyAsync(null, null, null, default, null, userLimit, reason);
        }
    }
}