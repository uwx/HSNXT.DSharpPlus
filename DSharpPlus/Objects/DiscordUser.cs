﻿using DSharpPlus.Objects.Transport;
using Newtonsoft.Json;

namespace DSharpPlus
{
    /// <summary>
    /// Represents a Discord user.
    /// </summary>
    public class DiscordUser : SnowflakeObject
    {
        internal DiscordUser() { }
        internal DiscordUser(TransportUser transport)
        {
            this.Id = transport.Id;
            this.Username = transport.Username;
            this.DiscriminatorInt = transport.DiscriminatorInt;
            this.AvatarHash = transport.AvatarHash;
            this.IsBot = transport.IsBot;
            this.MfaEnabled = transport.MfaEnabled;
            this.Verified = transport.Verified;
            this.Email = transport.Email;
        }

        /// <summary>
        /// Gets this user's username.
        /// </summary>
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; internal set; }
        
        /// <summary>
        /// Gets the user's 4-digit tag.
        /// </summary>
        [JsonIgnore]
        public string Discriminator => this.DiscriminatorInt.ToString("0000");
        [JsonProperty("discriminator", NullValueHandling = NullValueHandling.Ignore)]
        internal int DiscriminatorInt { get; set; }

        /// <summary>
        /// Gets the user's avatar hash.
        /// </summary>
        [JsonProperty("avatar", NullValueHandling = NullValueHandling.Ignore)]
        public string AvatarHash { get; internal set; }

        /// <summary>
        /// Gets the user's avatar URL.
        /// </summary>
        [JsonIgnore]
        public string AvatarUrl => AvatarHash.StartsWith("a_")? $"https://cdn.discordapp.com/avatars/{Id}/{AvatarHash}.gif" : $"https://cdn.discordapp.com/avatars/{Id}/{AvatarHash}.jpg";

        /// <summary>
        /// Gets whether the user is a bot.
        /// </summary>
        [JsonProperty("bot", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsBot { get; internal set; }

        /// <summary>
        /// Gets whether the user has multi-factor authentication enabled.
        /// </summary>
        [JsonProperty("mfa_enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MfaEnabled { get; internal set; }

        /// <summary>
        /// Gets whether the user is verified.
        /// </summary>
        [JsonProperty("verified", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Verified { get; internal set; }

        /// <summary>
        /// Gets the user's email address.
        /// </summary>
        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; internal set; }

        /// <summary>
        /// Gets the user's mention string.
        /// </summary>
        public string Mention => Formatter.Mention(this);
        
        /// <summary>
        /// Gets this user's presence.
        /// </summary>
        public DiscordPresence Presence => this.Discord.Presences.ContainsKey(this.Id) ? this.Discord.Presences[this.Id] : null;
    }
}
