﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.ModernEmbedBuilder
{
    public class ModernEmbedBuilder
    {
        /// <summary>
        /// Gets or sets the embed message's content. Only applies when using <see cref="Send"/>
        /// </summary>
        public string Content
        {
            get => _content;
            set => _content = CheckLength("Message content", value, 2000);
        }
        private string _content;

        /// <summary>
        /// Gets or sets the embed's title.
        /// </summary>
        public string Title
        {
            get => _title;
            set => _title = CheckLength("Title", value, 256);
        }
        private string _title;

        /// <summary>
        /// Gets or sets the embed's description.
        /// </summary>
        public string Description
        {
            get => _description;
            set => _description = CheckLength("Description", value, 2048);
        }
        private string _description;

        /// <summary>
        /// Gets or sets the url for the embed's title.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the embed's color.
        /// </summary>
        public (byte r, byte g, byte b) ColorRGB
        {
            get => (_color.R, _color.G, _color.B);
            set => _color = new DiscordColor(value.r, value.g, value.b);
        }
        
        /// <summary>
        /// Gets or sets the embed's color.
        /// </summary>
        public DuckColor Color
        {
            get => _color;
            set => _color = value;
        }
        private DiscordColor _color;

        /// <summary>
        /// Gets or sets the embed's timestamp.
        /// </summary>
        public DuckTimestamp Timestamp
        {
            get => _timestamp;
            set => _timestamp = value;
        }
        private DateTimeOffset? _timestamp;

        /// <summary>
        /// Gets or sets the embed's image url.
        /// </summary>
        public string ImageUrl
        {
            get => _imageUri?.ToString();
            set => _imageUri = string.IsNullOrEmpty(value) ? new MebUri?() : new MebUri(value);
        }
        private MebUri? _imageUri;

        /// <summary>
        /// Gets or sets the thumbnail's image url.
        /// </summary>
        public string ThumbnailUrl
        {
            get => _thumbnailUri?.ToString();
            set => _thumbnailUri = string.IsNullOrEmpty(value) ? new MebUri?() : new MebUri(value);
        }
        private MebUri? _thumbnailUri;

        /// <summary>
        /// Gets or sets the footer.
        /// </summary>
        public DuckFooter Footer { get; set; }

        /// <summary>
        /// Gets or sets the footer's text.
        /// </summary>
        public string FooterText
        {
            get => Footer?.Text;
            set
            {
                if (Footer != null)
                {
                    Footer.Text = value;
                }
                else
                {
                    Footer = new DuckFooter { Text = value };
                }
            }
        }

        /// <summary>
        /// Gets or sets the footer's icon url.
        /// </summary>
        public string FooterIcon
        {
            get => Footer?.IconUrl;
            set
            {
                if (Footer != null)
                {
                    Footer.IconUrl = value;
                }
                else
                {
                    Footer = new DuckFooter { IconUrl = value };
                }
            }
        }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public DuckAuthor Author { get; set; }

        /// <summary>
        /// Gets or sets the footer's text.
        /// </summary>
        public string AuthorName
        {
            get => Author?.Name;
            set
            {
                if (Author != null) Author.Name = value;
                else Author = new DuckAuthor {Name = value};
            }
        }

        /// <summary>
        /// Gets or sets the footer's icon url.
        /// </summary>
        public string AuthorUrl
        {
            get => Author?.Url;
            set
            {
                if (Author != null) Author.Url = value;
                else Author = new DuckAuthor {Url = value};
            }
        }

        /// <summary>
        /// Gets or sets the footer's icon url.
        /// </summary>
        public string AuthorIcon
        {
            get => Author?.IconUrl;
            set
            {
                if (Author != null) Author.IconUrl = value;
                else Author = new DuckAuthor {IconUrl = value};
            }
        }

        /// <summary>
        /// Gets or sets the embed's fields.
        /// </summary>
        public List<DuckField> Fields
        {
            get => _fields;
            set
            {
                if (_fields.Count > 25)
                    throw new InvalidOperationException("Cannot add more than 25 fields.");
                
                _fields = value;
            }
        }
        private List<DuckField> _fields = new List<DuckField>();

        internal static string CheckLength(string type, string value, int limit)
        {
            return value != null && value.Length > limit
                ? throw new ArgumentException($"${type} length cannot exceed {limit} characters.", nameof(value))
                : value;
        }

        /// <summary>
        /// Builds this embed
        /// </summary>
        /// <returns></returns>
        public DiscordEmbed Build()
        {
            var b = new DiscordEmbedBuilder
            {
                Title = _title,
                Description = _description,
                Url = Url,
                Color = _color,
                Timestamp = _timestamp,
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail {
                    Url = ThumbnailUrl,
                },
                ImageUrl = ImageUrl,
            };

            // dont care about nulls
            b.WithFooter(Footer?.Text, Footer?.IconUrl);
            b.WithAuthor(Author?.Name, Author?.Url, Author?.IconUrl);
            
            foreach (var f in Fields)
            {
                b.AddField(f.Name, f.Value, f.Inline);
            }
            
            return b;
        }

        /// <summary>
        /// Sends this embed directly to a channel.
        /// </summary>
        /// <param name="channel">The channel to send to</param>
        /// <param name="tts">Whether or not to enable text-to-speech</param>
        /// <returns>Task that resolves once the message containing the embed is sent</returns>
        public Task<DiscordMessage> Send(DiscordChannel channel, bool tts = false) 
            => channel.SendMessageAsync(Content, tts, Build());

        /// <summary>
        /// Adds a field to this embed.
        /// </summary>
        /// <param name="name">Name of the field to add.</param>
        /// <param name="value">Value of the field to add.</param>
        /// <param name="inline">Whether the field is to be inline or not.</param>
        /// <returns>This embed builder.</returns>
        public ModernEmbedBuilder AddField(string name, string value, bool inline = false)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                if (name == null)
                    throw new ArgumentNullException(nameof(name));
                throw new ArgumentException("Field name cannot be empty or whitespace.");
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                throw new ArgumentException("Field value cannot be empty or whitespace.");
            }

            if (Fields.Count >= 25)
                throw new InvalidOperationException("Cannot add more than 25 fields.");

            Fields.Add(new DuckField
            {
                Name = CheckLength("Field name", name, 256),
                Value = CheckLength("Field value", value, 1024),
                Inline = inline,
            });
            return this;
        }

        /// <summary>
        /// Implicitly converts this builder to an embed.
        /// </summary>
        /// <param name="builder">Builder to convert.</param>
        public static implicit operator DiscordEmbed(ModernEmbedBuilder builder) =>
            builder?.Build();

        /// <summary>
        /// Implicitly converts this builder to an optional embed.
        /// </summary>
        /// <param name="builder">Builder to convert.</param>
        public static implicit operator Optional<DiscordEmbed>(ModernEmbedBuilder builder) =>
            builder?.Build();

    }
}
