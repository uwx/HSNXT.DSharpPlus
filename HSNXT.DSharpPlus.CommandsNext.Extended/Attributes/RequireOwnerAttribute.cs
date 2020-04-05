using System;
using System.Linq;
using System.Threading.Tasks;

namespace DSharpPlus.CommandsNext.Attributes
{
    /// <summary>
    /// Defines that usage of this command is restricted to the owner of the bot.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class RequireOwnerAttribute : CheckBaseAttribute
    {
        public override Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help)
        {
            var cfg = ctx.Config;
            var app = ctx.Client.CurrentApplication;
            var me = ctx.Client.CurrentUser;

            return Task.FromResult(
                app != null
                    ? app.Owners.Any(x => x.Id == ctx.User.Id)
                    : ctx.User.Id == me.Id
            );
        }
    }
}
