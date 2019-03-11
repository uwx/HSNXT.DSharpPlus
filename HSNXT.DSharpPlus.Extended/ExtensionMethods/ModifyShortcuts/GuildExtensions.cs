using System;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.Extended.ExtensionMethods.ModifyShortcuts
{
    /// <summary>
    /// Shortcuts for <see cref="DiscordGuild.ModifyAsync"/> in <see cref="DiscordGuild"/>.
    /// </summary>
    public static class GuildExtensions
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
            return @this.ModifyAsync(e =>
            {
                e.Name = name;
                e.AuditLogReason = reason;
            });
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
            return @this.ModifyAsync(e =>
            {
                e.Region = region;
                e.AuditLogReason = reason;
            });
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
            return @this.ModifyAsync(e =>
            {
                e.Icon = icon;
                e.AuditLogReason = reason;
            });
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
            return @this.ModifyAsync(e =>
            {
                e.VerificationLevel = verificationLevel;
                e.AuditLogReason = reason;
            });
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
            return @this.ModifyAsync(e =>
            {
                e.DefaultMessageNotifications = defaultMessageNotifications;
                e.AuditLogReason = reason;
            });
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
            return @this.ModifyAsync(e =>
            {
                e.MfaLevel = mfaLevel;
                e.AuditLogReason = reason;
            });
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
            return @this.ModifyAsync(e =>
            {
                e.ExplicitContentFilter = explicitContentFilter;
                e.AuditLogReason = reason;
            });
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

            return @this.ModifyAsync(e =>
            {
                e.AfkChannel = afkChannel;
                e.AuditLogReason = reason;
            });
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
            return @this.ModifyAsync(e =>
            {
                e.AfkTimeout = afkTimeout;
                e.AuditLogReason = reason;
            });
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
            return @this.ModifyAsync(e =>
            {
                e.Owner = owner;
                e.AuditLogReason = reason;
            });
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
            return @this.ModifyAsync(e =>
            {
                e.Splash = splash;
                e.AuditLogReason = reason;
            });
        }
    }
}