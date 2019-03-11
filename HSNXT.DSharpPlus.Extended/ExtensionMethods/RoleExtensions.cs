using System;
using System.Collections.Generic;
using System.Linq;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.Extended.ExtensionMethods
{
    /// <summary>
    /// Extensions for <see cref="DiscordRole"/>
    /// </summary>
    public static class RoleExtensions
    {
        /// <summary>
        /// Checks if a given role can interact with another role (kick, ban, modify permissions).
        /// Note that this only checks the role position and not the actual permission.
        /// </summary>
        /// <param name="this">this object</param>
        /// <param name="target">the role to check against</param>
        /// <returns>true if this role can interact with the target</returns>
        /// <exception cref="ArgumentNullException">if target is null</exception>
        public static bool CanInteract(this DiscordRole @this, DiscordRole target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            
            return target.Position < @this.Position;
        }
        
        /// <summary>
        /// Gets all cached members in the guild with this role.
        /// </summary>
        /// <param name="this">this object</param>
        /// <returns>an enumerable containing the members in the guild that are present in the member cache and that
        /// have the role <paramref name="this"/>.</returns>
        public static IEnumerable<DiscordMember> GetMembers(this DiscordRole @this)
        {
            // keep it outside so it doesn't create a closure on every iteration (i think?)
            bool IsSame(DiscordRole r) => r.Id == @this.Id;

            return ReflectionUtils.GetClient(@this).Guilds[
                ReflectionUtils.GetGuildId(@this)
            ].Members.Where(e => e.Roles.Any(IsSame));
        }
    }
}
