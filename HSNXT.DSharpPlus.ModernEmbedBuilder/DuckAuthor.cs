namespace HSNXT.DSharpPlus.ModernEmbedBuilder
{
    public class DuckAuthor
    {
        /// <summary>
        /// Implicitly create a DuckAuthor from a name.
        /// </summary>
        public static implicit operator DuckAuthor(string name) => new DuckAuthor
        {
            Name = name,
        };
        
        /// <summary>
        /// Implicitly create a DuckAuthor from a name and url.
        /// </summary>
        public static implicit operator DuckAuthor((string name, string url) args) => new DuckAuthor
        {
            Name = args.name,
            Url = args.url,
        };
        
        /// <summary>
        /// Implicitly create a DuckAuthor from a name, url and icon url.
        /// </summary>
        public static implicit operator DuckAuthor((string name, string url, string iconUrl) args) => new DuckAuthor
        {
            Name = args.name,
            Url = args.url,
            IconUrl = args.iconUrl,
        };

        /// <summary>
        /// Gets the author's name.
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = ModernEmbedBuilder.CheckLength("Author name", value, 256);
        }
        private string _name;

        /// <summary>
        /// Gets the url of the author.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets the url of the author's icon.
        /// </summary>
        public string IconUrl
        {
            get => _iconUri?.ToString();
            set => _iconUri = string.IsNullOrEmpty(value) ? new MebUri?() : new MebUri(value);
        }
        private MebUri? _iconUri;
    }
}