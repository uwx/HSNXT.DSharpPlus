using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace DSharpPlus.Extended
{
    public static class ChannelExtensions
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

            return @this.ModifyAsync(e =>
            {
                e.Name = name;
                e.AuditLogReason = reason;
            });
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

            return @this.ModifyAsync(e =>
            {
                e.Topic = topic;
                e.AuditLogReason = reason;
            });
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

            return @this.ModifyAsync(e =>
            {
                e.Parent = parent;
                e.AuditLogReason = reason;
            });
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

            return @this.ModifyAsync(e =>
            {
                e.Bitrate = bitrate;
                e.AuditLogReason = reason;
            });
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

            return @this.ModifyAsync(e =>
            {
                e.Userlimit = userLimit;
                e.AuditLogReason = reason;
            });
        }

        /// <summary>
        /// Deletes multiple messages without the 100 message limit
        /// </summary>
        /// <param name="chan">This object</param>
        /// <param name="messages">the messages to delete</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns></returns>
        public static async Task DeleteManyMessagesAsync(this DiscordChannel chan, IEnumerable<DiscordMessage> messages,
            string reason = null)
        {
            // don't enumerate more than once
            var msgs = messages as List<DiscordMessage> ?? new List<DiscordMessage>(messages);

            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (msgs.Count == 0)
            {
                return;
            }

            if (msgs.Count == 1)
            {
                await chan.DeleteMessageAsync(msgs[0], reason);
            }
            else if (msgs.Count <= 100)
            {
                await chan.DeleteMessagesAsync(msgs, reason);
            }
            else
            {
                foreach (var msgsSub in SplitList(msgs, 100))
                {
                    await chan.DeleteMessagesAsync(msgs, reason);
                }
            }
        }

        private static IEnumerable<List<T>> SplitList<T>(List<T> locations, int nSize)
        {
            for (var i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }
    }
}