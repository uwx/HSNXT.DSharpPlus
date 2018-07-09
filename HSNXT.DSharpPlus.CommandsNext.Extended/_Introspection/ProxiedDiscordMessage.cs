using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

// ReSharper disable InconsistentNaming

namespace DSharpPlus.CommandsNext._Introspection
{
    public partial class ProxiedDiscordMessage
    {
        public const bool False = false;
        public const bool True = true;

        private readonly DiscordMessage _msg;

        private static readonly ConstructorInfo DiscordMessageConstructor =
            typeof(DiscordMessage).GetConstructor(BindingFlags.NonPublic, null, new Type[] { }, null)
            ?? throw new IntrospectionSetupException($"Did not find new {nameof(DiscordMessage)} constructor");

        private static readonly ObjectActivator<DiscordMessage> NewDiscordMessage =
            Constructors.GetActivator<DiscordMessage>(DiscordMessageConstructor);

        private static readonly Action<DiscordMessage, ulong> SetChannelId =
            Setters.MemberInstanceClass<DiscordMessage, ulong>("ChannelId");

        private static readonly Action<DiscordMessage, DiscordUser> SetAuthor =
            Setters.MemberInstanceClass<DiscordMessage, DiscordUser>("Author");

        private static readonly Action<DiscordMessage, string> SetContent =
            Setters.MemberInstanceClass<DiscordMessage, string>("Content");

        private static readonly Action<DiscordMessage, bool> SetIsTTS =
            Setters.MemberInstanceClass<DiscordMessage, bool>("IsTTS");

        private static readonly Action<DiscordMessage, bool> SetMentionEveryone =
            Setters.MemberInstanceClass<DiscordMessage, bool>("MentionEveryone");

        private static readonly Action<DiscordMessage, bool> SetPinned =
            Setters.MemberInstanceClass<DiscordMessage, bool>("Pinned");
        
        private static readonly Action<DiscordMessage, ulong> SetId =
            Setters.MemberInstanceClass<DiscordMessage, ulong>("Id");
        
        private static readonly Action<DiscordMessage, List<DiscordAttachment>> Set_attachments =
            Setters.MemberInstanceClass<DiscordMessage, List<DiscordAttachment>>("_attachments");
        private static readonly Func<DiscordMessage, List<DiscordAttachment>> Get_attachments =
            Getters.MemberInstance<DiscordMessage, List<DiscordAttachment>>("_attachments");
        
        private static readonly Action<DiscordMessage, List<DiscordEmbed>> Set_embeds =
            Setters.MemberInstanceClass<DiscordMessage, List<DiscordEmbed>>("_embeds");
        private static readonly Func<DiscordMessage, List<DiscordEmbed>> Get_embeds =
            Getters.MemberInstance<DiscordMessage, List<DiscordEmbed>>("_embeds");
        
        private static readonly Action<DiscordMessage, string> SetTimestampRaw =
            Setters.MemberInstanceClass<DiscordMessage, string>("TimestampRaw");
        private static readonly Func<DiscordMessage, string> GetTimestampRaw =
            Getters.MemberInstance<DiscordMessage, string>("TimestampRaw");
        
        private static readonly Action<DiscordMessage, List<DiscordReaction>> Set_reactions =
            Setters.MemberInstanceClass<DiscordMessage, List<DiscordReaction>>("_reactions");
        private static readonly Func<DiscordMessage, List<DiscordReaction>> Get_reactions =
            Getters.MemberInstance<DiscordMessage, List<DiscordReaction>>("_reactions");

        private static readonly Action<DiscordMessage, List<DiscordUser>> Set_mentionedUsers =
            Setters.MemberInstanceClass<DiscordMessage, List<DiscordUser>>("_mentionedUsers");
        private static readonly Func<DiscordMessage, List<DiscordUser>> Get_mentionedUsers =
            Getters.MemberInstance<DiscordMessage, List<DiscordUser>>("_mentionedUsers");

        private static readonly Action<DiscordMessage, List<DiscordRole>> Set_mentionedRoles =
            Setters.MemberInstanceClass<DiscordMessage, List<DiscordRole>>("_mentionedRoles");
        private static readonly Func<DiscordMessage, List<DiscordRole>> Get_mentionedRoles =
            Getters.MemberInstance<DiscordMessage, List<DiscordRole>>("_mentionedRoles");

        private static readonly Action<DiscordMessage, List<DiscordChannel>> Set_mentionedChannels =
            Setters.MemberInstanceClass<DiscordMessage, List<DiscordChannel>>("_mentionedChannels");
        private static readonly Func<DiscordMessage, List<DiscordChannel>> Get_mentionedChannels =
            Getters.MemberInstance<DiscordMessage, List<DiscordChannel>>("_mentionedChannels");

        //
        // ----
        //

        public ProxiedDiscordMessage() => _msg = NewDiscordMessage();
        private ProxiedDiscordMessage(DiscordMessage client) => _msg = client;

        public ProxiedDiscordClient Discord { get; set; }

        public static implicit operator ProxiedDiscordMessage(DiscordMessage client) =>
            new ProxiedDiscordMessage(client);

        public static implicit operator DiscordMessage(ProxiedDiscordMessage client) => client._msg;

        //
        // ----
        //

        public Task<DiscordMessage> ModifyAsync(Optional<string> content = default,
            Optional<DiscordEmbed> embed = default) => _msg.ModifyAsync(content, embed);

        public Task DeleteAsync(string reason = null) => _msg.DeleteAsync(reason);
        public Task PinAsync() => _msg.PinAsync();
        public Task UnpinAsync() => _msg.UnpinAsync();

        public Task<DiscordMessage> RespondAsync(string content = null, bool tts = False, DiscordEmbed embed = null) =>
            _msg.RespondAsync(content, tts, embed);

        public Task<DiscordMessage> RespondWithFileAsync(Stream file_data, string file_name, string content = null,
            bool tts = False, DiscordEmbed embed = null) =>
            _msg.RespondWithFileAsync(file_data, file_name, content, tts, embed);

        public Task<DiscordMessage> RespondWithFileAsync(FileStream file_data, string content = null, bool tts = False,
            DiscordEmbed embed = null) => _msg.RespondWithFileAsync(file_data, content, tts, embed);

        public Task<DiscordMessage> RespondWithFileAsync(string file_path, string content = null, bool tts = False,
            DiscordEmbed embed = null) => _msg.RespondWithFileAsync(file_path, content, tts, embed);

        public Task<DiscordMessage> RespondWithFilesAsync(Dictionary<string, Stream> files, string content = null,
            bool tts = False, DiscordEmbed embed = null) => _msg.RespondWithFilesAsync(files, content, tts, embed);

        public Task CreateReactionAsync(DiscordEmoji emoji) => _msg.CreateReactionAsync(emoji);
        public Task DeleteOwnReactionAsync(DiscordEmoji emoji) => _msg.DeleteOwnReactionAsync(emoji);

        public Task DeleteReactionAsync(DiscordEmoji emoji, DiscordUser user, string reason = null) =>
            _msg.DeleteReactionAsync(emoji, user, reason);

        public Task<IReadOnlyList<DiscordUser>> GetReactionsAsync(DiscordEmoji emoji) => _msg.GetReactionsAsync(emoji);
        public Task DeleteAllReactionsAsync(string reason = null) => _msg.DeleteAllReactionsAsync(reason);
        public Task AcknowledgeAsync() => _msg.AcknowledgeAsync();
        public override string ToString() => _msg.ToString();
        public override bool Equals(object obj) => _msg.Equals(obj);
        public bool Equals(DiscordMessage e) => _msg.Equals(e);
        public override int GetHashCode() => _msg.GetHashCode();
        public DiscordChannel Channel => _msg.Channel;

        public ulong ChannelId
        {
            get => _msg.ChannelId;
            set => SetChannelId(_msg, value);
        }

        public DiscordUser Author
        {
            get => _msg.Author;
            set => SetAuthor(_msg, value);
        }

        public string Content
        {
            get => _msg.Content;
            set => SetContent(_msg, value);
        }

        public DateTimeOffset Timestamp => _msg.Timestamp;
        public DateTimeOffset EditedTimestamp => _msg.EditedTimestamp;
        public bool IsEdited => _msg.IsEdited;

        public bool IsTTS
        {
            get => _msg.IsTTS;
            set => SetIsTTS(_msg, value);
        }

        public bool MentionEveryone
        {
            get => _msg.MentionEveryone;
            set => SetMentionEveryone(_msg, value);
        }

        public IReadOnlyList<DiscordUser> MentionedUsers => _msg.MentionedUsers;
        public IReadOnlyList<DiscordRole> MentionedRoles => _msg.MentionedRoles;
        public IReadOnlyList<DiscordChannel> MentionedChannels => _msg.MentionedChannels;
        public IReadOnlyList<DiscordAttachment> Attachments => _msg.Attachments;
        public IReadOnlyList<DiscordEmbed> Embeds => _msg.Embeds;
        public IReadOnlyList<DiscordReaction> Reactions => _msg.Reactions;

        public bool Pinned
        {
            get => _msg.Pinned;
            set => SetPinned(_msg, value);
        }

        public ulong? WebhookId => _msg.WebhookId;
        public MessageType? MessageType => _msg.MessageType;
        public bool WebhookMessage => _msg.WebhookMessage;

        public ulong Id
        {
            get => _msg.Id;
            set => SetId(_msg, value);
        }

        public DateTimeOffset CreationTimestamp => _msg.CreationTimestamp;


        public List<DiscordAttachment> _attachments
        {
            get => Get_attachments(_msg);
            set => Set_attachments(_msg, value);
        }

        public List<DiscordEmbed> _embeds
        {
            get => Get_embeds(_msg);
            set => Set_embeds(_msg, value);
        }

        public string TimestampRaw
        {
            get => GetTimestampRaw(_msg);
            set => SetTimestampRaw(_msg, value);
        }

        public List<DiscordReaction> _reactions
        {
            get => Get_reactions(_msg);
            set => Set_reactions(_msg, value);
        }

        public List<DiscordUser> _mentionedUsers
        {
            get => Get_mentionedUsers(_msg);
            set => Set_mentionedUsers(_msg, value);
        }

        public List<DiscordRole> _mentionedRoles
        {
            get => Get_mentionedRoles(_msg);
            set => Set_mentionedRoles(_msg, value);
        }

        public List<DiscordChannel> _mentionedChannels
        {
            get => Get_mentionedChannels(_msg);
            set => Set_mentionedChannels(_msg, value);
        }
    }

    public class ProxiedMessageCreateEventArgs
    {
        private static readonly ConstructorInfo MessageCreateEventArgsConstructor =
            typeof(MessageCreateEventArgs).GetConstructor(BindingFlags.NonPublic, null, new Type[] { typeof(DiscordClient) }, null)
            ?? throw new IntrospectionSetupException($"Did not find new {nameof(MessageCreateEventArgs)}({nameof(DiscordClient)}) constructor");

        private static readonly ObjectActivator<MessageCreateEventArgs> NewMessageCreateEventArgs =
            Constructors.GetActivator<MessageCreateEventArgs>(MessageCreateEventArgsConstructor);
        
        private static readonly Action<MessageCreateEventArgs, DiscordMessage> SetMessage =
            Setters.MemberInstanceClass<MessageCreateEventArgs, DiscordMessage>("Message");

        private readonly MessageCreateEventArgs _messageCreateEventArgs;

        public ProxiedMessageCreateEventArgs(ProxiedDiscordClient client)
        {
            _messageCreateEventArgs = NewMessageCreateEventArgs(client);
        }
        
        public static implicit operator MessageCreateEventArgs(ProxiedMessageCreateEventArgs proxy) => proxy._messageCreateEventArgs;
        public DiscordMessage Message
        {
            set => SetMessage(_messageCreateEventArgs, value);
        }
    }
}