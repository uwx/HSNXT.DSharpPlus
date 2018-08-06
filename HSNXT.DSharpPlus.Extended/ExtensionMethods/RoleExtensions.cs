using System;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.Extended.ExtensionMethods
{
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
        
        /*/// <summary>
        /// Gets all members in the guild with this role.
        /// </summary>
        /// <param name="this">this object</param>
        /// <returns>an enumerable containing the members in the guild that have the role <c>this</c>.</returns>
        public static IEnumerable<DiscordMember> GetMembers(this DiscordRole @this)
            => @this.Guild.Members.Where(e => e.Roles.Any(r => r.Id == @this.Id));*/
    }
}
