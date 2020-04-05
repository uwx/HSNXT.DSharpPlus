using System;
using System.Collections.Generic;
using System.Linq;
using DSharpPlus;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.InteractivityNext
{
    public sealed class InteractivityConfiguration
    {
        /// <summary>
        /// Sets the default interactivity action timeout. Defaults to 1 minute.
        /// </summary>
        public TimeSpan Timeout { internal get; set; } = TimeSpan.FromMinutes(1);

        /// <summary>
        /// Sets the default pagination timeout. Defaults to 2 minutes.
        /// </summary>
        public TimeSpan PaginationTimeout { internal get; set; } = TimeSpan.FromMinutes(2);

        /// <summary>
        /// Sets the default pagination finish behaviour. Defaults to <see cref="FinishBehaviour.Ignore"/>.
        /// </summary>
        public FinishBehaviour PaginationBehavior { internal get; set; } = FinishBehaviour.Ignore;

        /// <summary>
        /// Sets the default emotes to use as reactions for polling.
        /// </summary>
        public IEnumerable<DiscordEmoji> DefaultPollOptions
        {
            set => DefaultPollOptionsArray = value.ToArray();
        }
        internal DiscordEmoji[] DefaultPollOptionsArray;

        /// <summary>
        /// Sets the format string for the page header when using
        /// <see cref="InteractivityExtension.GeneratePagesInEmbeds"/>.
        /// </summary>
        public string DefaultPageHeader
        {
            internal get => _defaultPageHeader;
            set => _defaultPageHeader = value ?? throw new ArgumentNullException(nameof(value));
        }
        private string _defaultPageHeader = "Page {0}";

        /// <summary>
        /// Sets the format string for the page header when using
        /// <see cref="InteractivityExtension.GeneratePagesInStrings"/>.
        /// </summary>
        public string DefaultStringPageHeader
        {
            internal get => _defaultStringPageHeader;
            set => _defaultStringPageHeader = value ?? throw new ArgumentNullException(nameof(value));
        }
        private string _defaultStringPageHeader = "**Page {0}:**\n\n{1}";

        /// <summary>
        /// Sets the default emojis to use for the page controls for pagination.
        /// </summary>
        public PaginationEmojis DefaultPaginationEmojis
        {
            internal get => _defaultPaginationEmojis;
            // clone and discard old value so it cannot be modified further
            set => _defaultPaginationEmojis = value?.Clone() ?? throw new ArgumentNullException(nameof(value));
        }
        private PaginationEmojis _defaultPaginationEmojis;

        internal InteractivityConfiguration Clone()
        {
            return (InteractivityConfiguration) MemberwiseClone();
        }
        
        internal void SetDefaults(DiscordClient client)
        {
            DefaultPollOptionsArray = DefaultPollOptionsArray ?? new[]
            {
                DiscordEmoji.FromName(client, ":thumbsdown:"),
                DiscordEmoji.FromName(client, ":thumbsup:"),
            };
            _defaultPaginationEmojis = _defaultPaginationEmojis ?? new PaginationEmojis(client);
        }
    }
}
