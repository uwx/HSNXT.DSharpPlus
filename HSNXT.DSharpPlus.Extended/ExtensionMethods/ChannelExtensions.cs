using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
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
        
        /// <summary>
        /// Synchronizes the current channel's permissions to match the parent category's. This operation is analogous
        /// to the equivalent permission setting in the user client.
        /// </summary>
        /// <param name="channel">The current channel</param>
        /// <param name="parent">Optional, the channel which the current channel's permissions will be synchronized to.
        /// By default, this is the current channel's parent channel category.</param>
        /// <returns>A task that completes when the operation has finished.</returns>
        /// <remarks>
        /// You must provide either a channel that has a parent category or the <paramref name="parent"/> argument.
        /// Depending on the amount of overwrites of the parent channel, this operation may take a very long time.
        /// This method was contributed by Glockness.
        /// </remarks>
        public static async Task SyncPermissionsWithParent(this DiscordChannel channel, DiscordChannel parent = null)
        {
            parent = parent
                     ?? (channel.ParentId != null ? channel.Parent : null)
                     ?? throw new ArgumentException("Must provide either a channel with a parent or an explicit parent");

            // existing permissions are cached for atomicity, since this operation could take a very long time
            var permissionOverwrites = channel.PermissionOverwrites.ToArray();
            foreach (var permission in permissionOverwrites)
            {
                await permission.DeleteAsync();
            }

            var categoryPermissions = parent.PermissionOverwrites.ToArray();
            foreach (var permission in categoryPermissions)
            {
                switch (permission.Type)
                {
                    case OverwriteType.Member:
                        var member = await permission.GetMemberAsync();
                        await channel.AddOverwriteAsync(member, permission.Allowed, permission.Denied);
                        break;
                    case OverwriteType.Role:
                        var role = await permission.GetRoleAsync();
                        await channel.AddOverwriteAsync(role, permission.Allowed, permission.Denied);
                        break;
                    // should it throw if the permission type is neither of these?
                }
            }
        }
    }
}
