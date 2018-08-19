using System;
using System.Collections.Generic;
using System.Linq;
using DSharpPlus.Entities;

namespace DSharpPlus.Interactivity
{
    public sealed class InteractivityConfiguration
    {
        /// <summary>
        /// <para>Sets the default interactivity action timeout.</para>
        /// <para>Defaults to 1 minute.</para>
        /// </summary>
        public TimeSpan Timeout { internal get; set; } = TimeSpan.FromMinutes(1);

        /// <summary>
        /// <para>Sets the default pagination timeout.</para>
        /// <para>Defaults to 2 minutes.</para>
        /// </summary>
        public TimeSpan PaginationTimeout { internal get; set; } = TimeSpan.FromMinutes(2);

        /// <summary>
        /// <para>Sets the default pagination timeout behaviour.</para>
        /// <para>Defaults to <see cref="TimeoutBehaviour.DeleteReactions"/>.</para>
        /// </summary>
        public TimeoutBehaviour PaginationBehavior { internal get; set; } = TimeoutBehaviour.DeleteReactions;

        /// <summary>
        /// Default emotes to use as reactions for polling
        /// </summary>
        public IEnumerable<DiscordEmoji> DefaultPollOptions { internal get; set; }

        /// <summary>
        /// Format string for the page header when using <see cref="InteractivityExtension.GeneratePagesInEmbeds"/>
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
        /// Format string for the page header when using <see cref="InteractivityExtension.GeneratePagesInStrings"/>
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
        /// Creates a new instance of <see cref="InteractivityConfiguration"/>, copying the properties of another configuration.
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