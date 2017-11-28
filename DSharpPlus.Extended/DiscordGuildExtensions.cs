using System;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace DSharpPlus.Extended
{
    public static class DiscordGuildExtensions
    {
        /// <summary>
        /// Renames this guild.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="name">New name.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns>The modified guild object.</returns>
        public static Task<DiscordGuild> RenameAsync(this DiscordGuild @this, string name, string reason = null)
        {
            return @this.ModifyAsync(name, null, null,
                null, null, null, null, null, null, null, null, reason);
        }

        /// <summary>
        /// Sets this guild's voice region.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="region">New voice region.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns>The modified guild object.</returns>
        public static Task<DiscordGuild> SetVoiceRegionAsync(this DiscordGuild @this, DiscordVoiceRegion region,
            string reason = null)
        {
            return @this.ModifyAsync(null, region, null,
                null, null, null, null, null, null, null, null, reason);
        }

        /// <summary>
        /// Sets this guild's icon.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="icon">New icon.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns>The modified guild object.</returns>
        public static Task<DiscordGuild> SetIconAsync(this DiscordGuild @this, Stream icon, string reason = null)
        {
            return @this.ModifyAsync(null, null, null,
                null, null, null, null, null, null, null, icon, reason);
        }

        /// <summary>
        /// Sets this guild's verification level.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="verificationLevel">New verification level.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns>The modified guild object.</returns>
        public static Task<DiscordGuild> SetVerificationLevelAsync(this DiscordGuild @this,
            VerificationLevel verificationLevel, string reason = null)
        {
            return @this.ModifyAsync(null, null, null,
                verificationLevel, null, null, null, null, null, null, null, reason);
        }

        /// <summary>
        /// Sets this guild's default message notifications.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="defaultMessageNotifications">New default notification settings.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns>The modified guild object.</returns>
        public static Task<DiscordGuild> SetDefaultMessageNotificationsAsync(this DiscordGuild @this,
            DefaultMessageNotifications defaultMessageNotifications, string reason = null)
        {
            return @this.ModifyAsync(null, null, null, null,
                defaultMessageNotifications, null, null, null, null, null, null, reason);
        }

        /// <summary>
        /// Sets this guild's MFA requirement setting.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="mfaLevel">New MFA requirement setting.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns>The modified guild object.</returns>
        public static Task<DiscordGuild> SetMfaAsync(this DiscordGuild @this, MfaLevel mfaLevel, string reason = null)
        {
            return @this.ModifyAsync(null, null, null,
                null, null, mfaLevel, null, null, null, null, null, reason);
        }

        /// <summary>
        /// Sets this guild's explicit content filter setting.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="explicitContentFilter">New explicit content filter setting.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns>The modified guild object.</returns>
        public static Task<DiscordGuild> SetExplicitContentFilterAsync(this DiscordGuild @this,
            ExplicitContentFilter explicitContentFilter, string reason = null)
        {
            return @this.ModifyAsync(null, null, null,
                null, null, null, explicitContentFilter, null, null, null, null, reason);
        }

        /// <summary>
        /// Sets this guild's AFK voice channel.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="afkChannel">New voice AFK channel.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns>The modified guild object.</returns>
        public static Task<DiscordGuild> SetAfkChannelAsync(this DiscordGuild @this, DiscordChannel afkChannel,
            string reason = null)
        {
            if (afkChannel.Type != ChannelType.Voice)
                throw new ArgumentException("AFK channel needs to be a voice channel.");

            return @this.ModifyAsync(null, null, null,
                null, null, null, null, afkChannel, null, null, null, reason);
        }

        /// <summary>
        /// Sets this guild's AFK timeout (in seconds).
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="afkTimeout">New timeout after users are going to be moved to the voice AFK channel in seconds.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns>The modified guild object.</returns>
        public static Task<DiscordGuild> SetAfkTimeoutAsync(this DiscordGuild @this, int afkTimeout,
            string reason = null)
        {
            return @this.ModifyAsync(null, null, null,
                null, null, null, null, null, afkTimeout, null, null, reason);
        }

        /// <summary>
        /// Sets this guild's owner.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="owner">New owner. This can only be changed by current owner.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns>The modified guild object.</returns>
        public static Task<DiscordGuild> SetOwnerAsync(this DiscordGuild @this, DiscordMember owner,
            string reason = null)
        {
            return @this.ModifyAsync(null, null, null,
                null, null, null, null, null, null, owner, null, reason);
        }

        /// <summary>
        /// Sets this guild's invite splash.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="splash">New invite splash.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns>The modified guild object.</returns>
        public static Task<DiscordGuild> SetInviteSplashAsync(this DiscordGuild @this, Stream splash,
            string reason = null)
        {
            return @this.ModifyAsync(null, null, null,
                null, null, null, null, null, null, null, splash, reason);
        }
    }
}