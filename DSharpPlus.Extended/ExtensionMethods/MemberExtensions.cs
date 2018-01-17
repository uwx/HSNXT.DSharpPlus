using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace DSharpPlus.Extended
{
    public static class MemberExtensions
    {
        /// <summary>
        /// Renames this member.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="nickname">Nickname to set for this member.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns></returns>
        public static Task RenameAsync(this DiscordMember @this, string nickname, string reason = null)
        {
            return @this.ModifyAsync(e =>
            {
                e.Nickname = nickname;
                e.AuditLogReason = reason;
            });
        }

        /// <summary>
        /// Move this member to a voice channel.
        /// </summary>
        /// <param name="this">This object</param>
        /// <param name="voiceChannel">Voice channel to put the member into.</param>
        /// <param name="reason">Reason for audit logs.</param>
        /// <returns></returns>
        public static Task MoveToAsync(this DiscordMember @this, DiscordChannel voiceChannel,
            string reason = null)
        {
            if (voiceChannel.Type != ChannelType.Voice)
            {
                throw new ArgumentException("Given channel is not a voice channel.", nameof(voiceChannel));
            }

            return @this.ModifyAsync(e =>
            {
                e.VoiceChannel = voiceChannel;
                e.AuditLogReason = reason;
            });
        }

        /// <summary>
        /// Checks if a given member can interact with another member (kick, ban, modify permissions).
        /// Note that this only checks the role position and not the actual permission.
        /// </summary>
        /// <param name="this">this object</param>
        /// <param name="target">the member to check against</param>
        /// <returns>true if this member can interact with the target</returns>
        /// <exception cref="ArgumentNullException">if target is null</exception>
        public static bool CanInteract(DiscordMember @this, DiscordMember target)
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
        public static Permissions GetPermissions(DiscordMember @this) =>
            @this.Roles.Select(e => e.Permissions).Aggregate((a, b) => a | b);
    }
}