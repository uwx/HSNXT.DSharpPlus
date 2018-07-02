namespace DSharpPlus.ModernEmbedBuilder
{
    public class DuckAuthor
    {
        /// <summary>
        /// Implicitly create a DuckAuthor from a name.
        /// </summary>
        public static implicit operator DuckAuthor(string name) => new DuckAuthor()
        {
            Name = name,
        };
        
        /// <summary>
        /// Implicitly create a DuckAuthor from a name and url.
        /// </summary>
        public static implicit operator DuckAuthor((string name, string url) args) => new DuckAuthor()
        {
            Name = args.name,
            Url = args.url,
        };
        
        /// <summary>
        /// Implicitly create a DuckAuthor from a name, url and icon url.
        /// </summary>
        public static implicit operator DuckAuthor((string name, string url, string iconUrl) args) => new DuckAuthor()
        {
            Name = args.name,
            Url = args.url,
            IconUrl = args.iconUrl,
        };
        
        /// <summary>
        /// Gets the footer's text.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the url of the footer's icon.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets the proxied url of the footer's icon.
        /// </summary>
        public string IconUrl { get; set; }
    }
}