﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DSharpPlus
{
    /// <summary>
    /// 
    /// </summary>
    public class DiscordMember : DiscordUser
    {
        public DiscordMember() { }
        public DiscordMember(DiscordUser user)
        {
            this.AvatarHash = user.AvatarHash;
            this.Discord = user.Discord;
            this.Discriminator = user.Discriminator;
            this.Email = user.Email;
            this.Id = user.Id;
            this.IsBot = user.IsBot;
            this.MFAEnabled = user.MFAEnabled;
            this.Username = user.Username;
            this.Verified = user.Verified;
        }

        /// <summary>
        /// This users guild nickname
        /// </summary>
        [JsonProperty("nick", NullValueHandling = NullValueHandling.Ignore)]
        public string Nickname { get; internal set; }
        /// <summary>
        /// List of role object id's
        /// </summary>
        [JsonProperty("roles", NullValueHandling = NullValueHandling.Ignore)]
        public List<ulong> Roles { get; internal set; }
        /// <summary>
        /// Date the user joined the guild
        /// </summary>
        [JsonProperty("joined_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime JoinedAt { get; internal set; }
        /// <summary>
        /// If the user is deafened
        /// </summary>
        [JsonProperty("is_deafened", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsDeafened { get; internal set; }
        /// <summary>
        /// If the user is muted
        /// </summary>
        [JsonProperty("is_muted", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsMuted { get; internal set; }

        public Task<DiscordDmChannel> SendDmAsync() => this.Discord._rest_client.InternalCreateDM(this.Id);
    }
}
