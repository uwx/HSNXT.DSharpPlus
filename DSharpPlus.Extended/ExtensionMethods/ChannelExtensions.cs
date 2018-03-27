using System;
using System.Collections.Generic;
using System.Linq;
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

        public static async Task<IReadOnlyList<DiscordMessage>> GetManyMessagesBeforeAsync(this DiscordChannel chan,
            DiscordMessage msg, int amount)
        {
            return await GetManyMessages(msg, amount, chan.GetMessagesBeforeAsync);
        }


        public static async Task<IReadOnlyList<DiscordMessage>> GetManyMessagesAfterAsync(this DiscordChannel chan,
            DiscordMessage msg, int amount)
        {
            return await GetManyMessages(msg, amount, chan.GetMessagesAfterAsync);
        }

#pragma warning disable 618
        public static async Task<IReadOnlyList<DiscordMessage>> GetManyMessagesAsync(this DiscordChannel chan, int amount)
        {
            if (amount <= 100)
            {
                return await chan.GetMessagesAsync(amount);
            }
            var lastMessage = await chan.GetMessagesAsync(1);
            if (lastMessage.Count == 0)
            {
                return new EmptyList<DiscordMessage>();
            }
            return await GetManyMessages(lastMessage[0], amount, chan.GetMessagesBeforeAsync);
        }
#pragma warning restore 618
        
        // ReSharper disable once SuggestBaseTypeForParameter
        private static async Task<IReadOnlyList<DiscordMessage>> GetManyMessages(DiscordMessage msg, int amount,
            Func<ulong, int, Task<IReadOnlyList<DiscordMessage>>> messageGetFunction)
        {
            if (amount <= 100)
            {
                return await messageGetFunction(msg.Id, amount);
            }

            var list = new List<DiscordMessage>(await messageGetFunction(msg.Id, 100));
            // we're out of messages
            if (list.Count < 100)
            {
                return list;
            }
            for (var i = 100; i < amount; i += 100)
            {
                var take = Math.Min(100, amount - i);
                var curList = await messageGetFunction(list[list.Count - 1].Id, take);
                list.AddRange(curList);
                // we're out of messages
                if (curList.Count < take)
                {
                    break;
                }
            }

            return list;
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