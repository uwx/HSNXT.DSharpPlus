﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSharpPlus
{
    /// <summary>
    /// 
    /// </summary>
    public class DiscordMessage : SnowflakeObject
    {
        /// <summary>
        /// ID of the channel the message was sent in
        /// </summary>
        [JsonProperty("channel_id")]
        public ulong ChannelID { get; internal set; }
        /// <summary> 
        /// The channel the message was sent in
        /// </summary>
        [JsonIgnore]
        public DiscordChannel Parent => DiscordClient.InternalGetChannel(ChannelID).Result;
        /// <summary>
        /// The author of this message
        /// </summary>
        [JsonProperty("author")]
        public DiscordUser Author { get; internal set; }
        /// <summary>
        /// Contents of the message
        /// </summary>
        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; internal set; }
        /// <summary>
        /// When this message was sent
        /// </summary>
        [JsonProperty("timestamp")]
        public string TimestampRaw { get; internal set; }
        /// <summary>
        /// When this message was sent
        /// </summary>
        [JsonIgnore]
        public DateTime Timestamp => DateTime.Parse(this.TimestampRaw);
        /// <summary>
        /// When this message was edited
        /// </summary>
        [JsonProperty("edited_timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public string EditedTimestampRaw { get; internal set; }
        /// <summary>
        /// When this message was edited
        /// </summary>
        [JsonIgnore]
        public DateTime EditedTimestamp => DateTime.Parse(this.EditedTimestampRaw);
        /// <summary>
        /// Whether this was a tts message
        /// </summary>
        [JsonProperty("tts")]
        public bool TTS { get; internal set; }
        /// <summary>
        /// Whether this message mentions everyone
        /// </summary>
        [JsonProperty("mention_everyone")]
        public bool MentionEveryone { get; internal set; }
        /// <summary>
        /// Users specifically mentioned in the message
        /// </summary>
        [JsonProperty("mentions")]
        public List<DiscordUser> Mentions { get; internal set; }
        /// <summary>
        /// Roles specifically mention in the message
        /// </summary>
        [JsonProperty("mentioned_roles")]
        public List<DiscordRole> MentionedRoles { get; internal set; }
        /// <summary>
        /// Any attached files
        /// </summary>
        [JsonProperty("attachments")]
        public List<DiscordAttachment> Attachments { get; internal set; }
        /// <summary>
        /// Any embedded content
        /// </summary>
        [JsonProperty("embeds")]
        public List<DiscordEmbed> Embeds { get; internal set; }
        /// <summary>
        /// Reactions of the message
        /// </summary>
        [JsonProperty("reactions")]
        public List<DiscordReaction> Reactions { get; internal set; }
        /// <summary>
        /// Used for validating a message was sent
        /// </summary>
        [JsonProperty("nonce")]
        public ulong? Nonce { get; internal set; }
        /// <summary>
        /// Whether this message is pinned
        /// </summary>
        [JsonProperty("pinned")]
        public bool Pinned { get; internal set; }
        /// <summary>
        /// If generated by webhook, this is the webhook's id
        /// </summary>
        [JsonProperty("webhook_id")]
        public ulong? WebhookID { get; internal set; }
        /// <summary>
        /// DateTime of when the message was received. based off machine time
        /// </summary>
        [JsonIgnore]
        public DateTime Received { get; internal set; }

        /// <summary>
        /// Edit the message
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<DiscordMessage> Edit(string content) => await DiscordClient.InternalEditMessage(ChannelID, ID, content);
        /// <summary>
        /// Deletes the message
        /// </summary>
        /// <returns></returns>
        public async Task Delete() => await DiscordClient.InternalDeleteMessage(ChannelID, ID);
        /// <summary>
        /// Pin the message
        /// </summary>
        /// <returns></returns>
        public async Task Pin() => await DiscordClient.InternalAddPinnedChannelMessage(ChannelID, ID);
        /// <summary>
        /// Unpin the message
        /// </summary>
        /// <returns></returns>
        public async Task Unpin() => await DiscordClient.InternalDeletePinnedChannelMessage(ChannelID, ID);
        /// <summary>
        /// Respond to the message
        /// </summary>
        /// <param name="content"></param>
        /// <param name="tts"></param>
        /// <param name="embed"></param>
        /// <returns></returns>
        public async Task<DiscordMessage> Respond(string content, bool tts = false, DiscordEmbed embed = null) => await DiscordClient.InternalCreateMessage(ChannelID, content, tts, embed);
        /// <summary>
        /// Respond to the message
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filepath"></param>
        /// <param name="filename"></param>
        /// <param name="tts"></param>
        /// <returns></returns>
        public async Task<DiscordMessage> Respond(string content, string filepath, string filename, bool tts = false) 
            => await DiscordClient.InternalUploadFile(ChannelID, filepath, filename, content, tts);

        /// <summary>
        /// Creates a reaction to this message
        /// </summary>
        /// <param name="emoji">The emoji you want to react with, either an emoji or name:id</param>
        /// <returns></returns>
        public async Task CreateReaction(string emoji) => await DiscordClient.InternalCreateReaction(ChannelID, ID, emoji);
        /// <summary>
        /// Deletes your own reaction
        /// </summary>
        /// <param name="emoji">Emoji for the reaction you want to remove, either an emoji or name:id</param>
        /// <returns></returns>
        public async Task DeleteOwnReaction(string emoji) => await DiscordClient.InternalDeleteOwnReaction(ChannelID, ID, emoji);
        /// <summary>
        /// Deletes another user's reaction.
        /// </summary>
        /// <param name="emoji">Emoji for the reaction you want to remove, either an emoji or name:id</param>
        /// <param name="Member">Member you want to remove the reaction for</param>
        /// <returns></returns>
        public async Task DeleteReaction(string emoji, DiscordMember Member) => await DiscordClient.InternalDeleteUserReaction(ChannelID, ID, Member.User.ID, emoji);
        /// <summary>
        /// Deletes another user's reaction.
        /// </summary>
        /// <param name="emoji">Emoji for the reaction you want to remove, either an emoji or name:id</param>
        /// <param name="UserID">User ID of the member you want to remove the reaction for</param>
        /// <returns></returns>
        public async Task DeleteReaction(string emoji, ulong UserID) => await DiscordClient.InternalDeleteUserReaction(ChannelID, ID, UserID, emoji);
        /// <summary>
        /// Gets users that reacted with this emoji
        /// </summary>
        /// <param name="emoji">Either an emoji or a name:id</param>
        /// <returns></returns>
        public async Task<List<DiscordUser>> GetReactions(string emoji) => await DiscordClient.InternalGetReactions(ChannelID, ID, emoji);
        /// <summary>
        /// Deletes all reactions for this message
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAllReactions() => await DiscordClient.InternalDeleteAllReactions(ChannelID, ID);
    }
}
