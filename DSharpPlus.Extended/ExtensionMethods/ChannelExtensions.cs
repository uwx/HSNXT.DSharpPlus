﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace DSharpPlus.Extended
{
    public static class ChannelExtensions
    {
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

        public static async Task GetManyMessagesBeforeAsync(this DiscordChannel chan, DiscordMessage msg, int amount)
        {
            // TODO
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