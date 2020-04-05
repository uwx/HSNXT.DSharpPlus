using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace HSNXT.DSharpPlus.Extended.CNext
{
    /// <summary>
    /// Defines that usage of this command is restricted to members with specified role, or higher.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public sealed class RequireRoleOrHigherAttribute : CheckBaseAttribute
    {
        /// <summary>
        /// Gets the name of the role required to execute this command.
        /// </summary>
        public string RoleName { get; }

        /// <summary>
        /// Defines that usage of this command is restricted to members with specified role, or higher.
        /// </summary>
        /// <param name="roleName">Name of the role to be verified by this check.</param>
        public RequireRoleOrHigherAttribute(string roleName) => RoleName = roleName;

        public override Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help)
        {
            if (ctx.Guild == null || ctx.Member == null)
                return Task.FromResult(false);

            var targetPosition = ctx.Guild.Roles.Values.FirstOrDefault(e => e.Name == RoleName)?.Position;
            if (targetPosition == null)
                return Task.FromResult(false);
            
            var ourPosition = ctx.Member.Roles.Max(e => e.Position);
            // we don't need to check the default value because the default value is, well, the same as the @everyone role position
            
            return Task.FromResult(ourPosition >= targetPosition);
        }
    }
}
