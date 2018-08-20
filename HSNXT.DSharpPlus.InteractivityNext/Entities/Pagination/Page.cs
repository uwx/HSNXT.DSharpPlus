using DSharpPlus.Entities;

namespace DSharpPlus.Interactivity
{
    /// <summary>
    /// Represents an individual page for <see cref="InteractivityExtension.SendPaginatedMessage"/>.
    /// </summary>
    public readonly struct Page
    {
        public string Content { get; }
        public DiscordEmbed Embed { get; }

        /// <summary>
        /// Creates a page containing a text message.
        /// </summary>
        /// <param name="content">The page contents.</param>
        public Page(string content)
        {
            Content = content;
            Embed = null;
        }

        /// <summary>
        /// Creates a page containing an embed.
        /// </summary>
        /// <param name="embed">The page contents.</param>
        public Page(DiscordEmbed embed)
        {
            Content = null;
            Embed = embed;
        }

        /// <summary>
        /// Creates a page containing both an embed and an attached message.
        /// </summary>
        /// <param name="content">The page text message.</param>
        /// <param name="embed">The page embed.</param>
        public Page(string content, DiscordEmbed embed)
        {
            Content = content;
            Embed = embed;
        }
    }
}