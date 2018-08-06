using System.IO;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.Extended.ExtensionMethods.ModifyShortcuts
{
    public static class WebhookExtensions
    {
        /// <summary>
        /// Changes this webhook's name.
        /// </summary>
        /// <param name="this">This webhook</param>
        /// <param name="name">New default name for this webhook.</param>
        /// <returns>The modified webhook.</returns>
        public static Task<DiscordWebhook> SetNameAsync(this DiscordWebhook @this, string name)
            => @this.ModifyAsync(name);

        /// <summary>
        /// Changes this webhook's avatar.
        /// </summary>
        /// <param name="this">This webhook</param>
        /// <param name="avatar">The target webhook avatar</param>
        /// <returns>The modified webhook.</returns>
        public static Task<DiscordWebhook> SetAvatarAsync(this DiscordWebhook @this, Stream avatar)
            => @this.ModifyAsync(null, avatar);
    }
}