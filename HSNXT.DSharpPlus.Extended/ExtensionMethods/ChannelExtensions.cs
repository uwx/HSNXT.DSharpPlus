using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.Extended.ExtensionMethods
{
    public static class ChannelExtensions
    {
        [Obsolete("Update to 4.0.0-beta-484 and use DeleteMessagesAsync instead")]
        public static Task DeleteManyMessagesAsync(this DiscordChannel chan, IEnumerable<DiscordMessage> messages,
            string reason = null)
            => chan.DeleteMessagesAsync(messages, reason);

        [Obsolete("Update to 4.0.0-beta-481 and use GetMessagesBeforeAsync instead")]
        public static Task<IReadOnlyList<DiscordMessage>> GetManyMessagesBeforeAsync(this DiscordChannel chan,
            DiscordMessage msg, int amount)
            => chan.GetMessagesBeforeAsync(msg.Id, amount);

        [Obsolete("Update to 4.0.0-beta-481 and use GetMessagesAfterAsync instead")]
        public static Task<IReadOnlyList<DiscordMessage>> GetManyMessagesAfterAsync(this DiscordChannel chan,
            DiscordMessage msg, int amount)
            => chan.GetMessagesAfterAsync(msg.Id, amount);

        [Obsolete("Update to 4.0.0-beta-481 and use GetMessagesAsync instead")]
        public static Task<IReadOnlyList<DiscordMessage>> GetManyMessagesAsync(this DiscordChannel chan, int amount)
            => chan.GetMessagesAsync(amount);
    }
}
