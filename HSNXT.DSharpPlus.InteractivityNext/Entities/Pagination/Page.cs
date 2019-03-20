using DSharpPlus.Entities;

namespace DSharpPlus.Interactivity
{
    /// <summary>
    /// Represents a single "unpaginated" message. One or more of these can be used for pagination, although it will
    /// only really be useful if you have more than a single page.
    /// </summary>
    public class Page
    {
        /// <summary>
        /// Text content for the message
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// Embed for the message
        /// </summary>
        public DiscordEmbed Embed { get; set; }
    }
}