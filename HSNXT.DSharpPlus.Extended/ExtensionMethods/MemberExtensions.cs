using System;
using System.Linq;
using DSharpPlus;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.Extended.ExtensionMethods
{
    public static class MemberExtensions
    {
        /// <summary>
        /// Checks if a given member can interact with another member (kick, ban, modify permissions).
        /// Note that this only checks the role position and not the actual permission.
        /// </summary>
        /// <param name="this">this object</param>
        /// <param name="target">the member to check against</param>
        /// <returns>true if this member can interact with the target</returns>
        /// <exception cref="ArgumentNullException">if target is null</exception>
        public static bool CanInteract(this DiscordMember @this, DiscordMember target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            var guild = @this.Guild;
            if (guild != target.Guild)
            {
                throw new ArgumentException("Provided members must both be Member objects of the same Guild!",
                    nameof(target));
            }

            if (guild.Owner == @this) return true;
            if (guild.Owner == target) return false;

            var issuerRole = @this.Roles.FirstOrDefault();
            var targetRole = target.Roles.FirstOrDefault();
            return issuerRole != null && (targetRole == null || issuerRole.CanInteract(targetRole));
        }

        /// <summary>
        /// Gets this member's permissions in the guild they're part of.
        /// </summary>
        public static Permissions GetPermissions(this DiscordMember @this) =>
            @this.Roles.Select(e => e.Permissions).Aggregate((a, b) => a | b);
    }
}
