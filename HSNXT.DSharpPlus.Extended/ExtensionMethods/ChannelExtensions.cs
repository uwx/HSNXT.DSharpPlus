using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.Extended.ExtensionMethods
{
    /// <summary>
    /// Extensions for <see cref="DiscordChannel"/>
    /// </summary>
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
        /// <param name="reason">Reason string for display in audit logs. Defaults to having no reason string. The
        /// reason string will have a suffix appended to it containing the index of the current part of the operation
        /// and the total amount of parts in the operation.</param>
        /// <returns>A task that completes when the operation has finished.</returns>
        /// <remarks>
        /// You must provide either a channel that has a parent category or the <paramref name="parent"/> argument.
        /// Depending on the amount of overwrites of the parent channel, this operation may take a very long time.
        /// This method was contributed by Glockness.
        /// </remarks>
        public static async Task SyncPermissionsWithParent(
            this DiscordChannel channel, DiscordChannel parent = null, string reason = null)
        {
            parent = parent
                     ?? (channel.ParentId != null ? channel.Parent : null)
                     ?? throw new ArgumentException("Must provide either a channel with a parent or an explicit parent");

            var childRoles = new Dictionary<ulong, DiscordOverwrite>();
            var childMembers = new Dictionary<ulong, DiscordOverwrite>();

            var parentRoles = new Dictionary<ulong, DiscordOverwrite>();
            var parentMembers = new Dictionary<ulong, DiscordOverwrite>();

            foreach (var permission in channel.PermissionOverwrites)
            {
                if (permission.Type == OverwriteType.Role)
                    childRoles.Add(permission.Id, permission);
                else
                    childMembers.Add(permission.Id, permission);

            }

            foreach (var permission in parent.PermissionOverwrites)
            {
                if (permission.Type == OverwriteType.Role)
                    parentRoles.Add(permission.Id, permission);
                else
                    parentMembers.Add(permission.Id, permission);
            }
            
            // DiscordOverwrite has no equality check support (yet) so we have to compare the ids themselves (using a 
            // hash table for simplicity and maybe performance)
            var toUpdate = new List<(DiscordOverwrite par, DiscordOverwrite child)>();
            var toDelete = new List<DiscordOverwrite>();

            // update all roles/members in child that are also in parent (matches)
            // delete all roles/members in child that are not in parent (leftovers)
            foreach (var (id, child) in childRoles)
            {
                if (parentRoles.TryGetValue(id, out var par))
                {
                    toUpdate.Add((par, child));
                }
                else
                {
                    toDelete.Add(child);
                }
            }

            foreach (var (id, child) in childMembers)
            {
                if (parentMembers.TryGetValue(id, out var par))
                {
                    toUpdate.Add((par, child));
                }
                else
                {
                    toDelete.Add(child);
                }
            }

            // add all role/members that are in parent but not in child (new)
            var rolesToAdd = parentRoles.Values
                .Except<DiscordOverwrite>(childRoles.Values, new SnowflakeEqualityComparer())
                .Select(e => (e, ReflectionUtils.CreateSkeletonRole(e.Id)))
                .ToArray();
            var membersToAdd = parentMembers.Values
                .Except<DiscordOverwrite>(childMembers.Values, new SnowflakeEqualityComparer())
                .Select(e => (e, ReflectionUtils.CreateSkeletonMember(e.Id)))
                .ToArray();

            var totalOpCount = toUpdate.Count + toDelete.Count + rolesToAdd.Length + membersToAdd.Length;
            var opIndex = 1;

            string GetReason() => reason != null ? $"{reason} [{opIndex++}/{totalOpCount}]" : null;

            foreach (var (par, child) in toUpdate)
            {
                await child.UpdateAsync(par.Allowed, par.Denied, GetReason());
            }

            foreach (var (overwrite, role) in rolesToAdd)
            {
                await channel.AddOverwriteAsync(role, overwrite.Allowed, overwrite.Denied, GetReason());
            }

            foreach (var (overwrite, member) in membersToAdd)
            {
                await channel.AddOverwriteAsync(member, overwrite.Allowed, overwrite.Denied, GetReason());
            }

            foreach (var item in toDelete)
            {
                await item.DeleteAsync(GetReason());
            }
        }
    }
}
