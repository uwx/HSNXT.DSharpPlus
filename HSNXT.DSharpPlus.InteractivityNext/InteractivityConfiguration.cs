using System;
using System.Collections.Generic;
using System.Linq;
using DSharpPlus.Entities;

namespace DSharpPlus.Interactivity
{
    /// <summary>
    /// Configuration options for <see cref="InteractivityExtension"/>.
    /// </summary>
    public sealed class InteractivityConfiguration
    {
        /// <summary>
        /// <p>Sets the default interactivity action timeout.</p>
        /// <p>Defaults to 1 minute.</p>
        /// </summary>
        public TimeSpan Timeout { internal get; set; } = TimeSpan.FromMinutes(1);

        /// <summary>
        /// <p>Sets the default pagination timeout.</p>
        /// <p>Defaults to 2 minutes.</p>
        /// </summary>
        public TimeSpan PaginationTimeout { internal get; set; } = TimeSpan.FromMinutes(2);

        /// <summary>
        /// <p>Sets the default pagination timeout behaviour.</p>
        /// <p>Defaults to <see cref="TimeoutBehaviour.DeleteReactions"/>.</p>
        /// </summary>
        public TimeoutBehaviour PaginationBehavior { internal get; set; } = TimeoutBehaviour.DeleteReactions;

        /// <summary>
        /// Default emotes to use as reactions for polling
        /// </summary>
        public IEnumerable<DiscordEmoji> DefaultPollOptions { internal get; set; }

        /// <summary>
        /// Format string for the page header when using <see cref="InteractivityExtension.GeneratePagesInEmbeds"/>.
        /// <p></p>
        /// <p>The parameters provided to the format string are as follows:</p>
        /// <list type="bullet">
        ///     <item><description>
        ///         0: The page number
        ///     </description></item>
        ///     <item><description>
        ///         1: The amount of pages
        ///     </description></item>
        /// </list>
        /// </summary>
        public string DefaultPageHeader
        {
            internal get => _defaultPageHeader;
            set => _defaultPageHeader = value ?? throw new ArgumentNullException(nameof(value));
        }

        private string _defaultPageHeader = "Page {0} of {1}";

        /// <summary>
        /// Format string for the page header when using <see cref="InteractivityExtension.GeneratePagesInStrings"/>.
        /// <p></p>
        /// <p>The parameters provided to the format string are as follows:</p>
        /// <list type="bullet">
        ///     <item><description>
        ///         0: The page number
        ///     </description></item>
        ///     <item><description>
        ///         1: The amount of pages
        ///     </description></item>
        ///     <item><description>
        ///         2: The page content
        ///     </description></item>
        /// </list>
        /// </summary>
        public string DefaultStringPageHeader
        {
            internal get => _defaultStringPageHeader;
            set => _defaultStringPageHeader = value ?? throw new ArgumentNullException(nameof(value));
        }

        private string _defaultStringPageHeader = "**Page {0} of {1}:**\n\n{2}";

        public PaginationEmojis DefaultPaginationEmojis
        {
            internal get => _defaultPaginationEmojis;
            set => _defaultPaginationEmojis = value ?? throw new ArgumentNullException(nameof(value));
        }

        private PaginationEmojis _defaultPaginationEmojis;

        /// <summary>
        /// Creates a new instance of <see cref="InteractivityConfiguration"/>.
        /// </summary>
        public InteractivityConfiguration()
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="InteractivityConfiguration"/>, copying the properties of another
        /// configuration.
        /// </summary>
        /// <param name="other">Configuration the properties of which are to be copied.</param>
        public InteractivityConfiguration(InteractivityConfiguration other)
        {
            PaginationBehavior = other.PaginationBehavior;
            PaginationTimeout = other.PaginationTimeout;
            Timeout = other.Timeout;
        }
    }
}