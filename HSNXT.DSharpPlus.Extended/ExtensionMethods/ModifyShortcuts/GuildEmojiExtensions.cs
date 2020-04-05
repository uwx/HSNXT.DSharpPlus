using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.Extended.ExtensionMethods.ModifyShortcuts
{
    /// <summary>
    /// Shortcuts for <see cref="DiscordGuildEmoji.ModifyAsync"/> in <see cref="DiscordGuildEmoji"/>.
    /// </summary>
    public static class GuildEmojiExtensions
    {
        /// <summary>
        /// Renames this emoji.
        /// </summary>
        /// <param name="this">This emoji</param>
        /// <param name="name">New name for this emoji.</param>
        /// <param name="reason">Reason for audit log.</param>
        /// <returns>The modified emoji.</returns>
        public static Task<DiscordGuildEmoji> RenameAsync(this DiscordGuildEmoji @this, string name,
            string reason = null)
            => @this.ModifyAsync(name, null, reason);

        /// <summary>
        /// Modifies the list of roles for which this emoji is available.
        /// This works only if your application is whitelisted as integration.
        /// </summary>
        /// <param name="this">This emoji</param>
        /// <param name="roles">Roles for which this emoji will be available.</param>
        /// <param name="reason">Reason for audit log.</param>
        /// <returns>The modified emoji.</returns>
        public static Task<DiscordGuildEmoji> SetRolesAsync(this DiscordGuildEmoji @this,
            IEnumerable<DiscordRole> roles, string reason = null)
            => @this.ModifyAsync(null, roles, reason);
    }
}