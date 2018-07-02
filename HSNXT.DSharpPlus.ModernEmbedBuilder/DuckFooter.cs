namespace DSharpPlus.ModernEmbedBuilder
{
    public class DuckFooter
    {
        /// <summary>
        /// Implicitly create a DuckFooter from a text string.
        /// </summary>
        public static implicit operator DuckFooter(string text) => new DuckFooter()
        {
            Text = text,
        };
        
        /// <summary>
        /// Implicitly create a DuckFooter from a text string and icon url.
        /// </summary>
        public static implicit operator DuckFooter((string text, string iconUrl) args) => new DuckFooter()
        {
            Text = args.text,
            IconUrl = args.iconUrl,
        };
        
        /// <summary>
        /// Gets the footer's text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets the url of the footer's icon.
        /// </summary>
        public string IconUrl { get; set; }
    }
}