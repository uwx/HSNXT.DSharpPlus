using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace DSharpPlus.CommandsNext._Introspection
{
    public partial class ProxiedDiscordClient : IDisposable
    {
        public void Dispose ( ) => _client. Dispose ( );
        public void AddExtension ( BaseExtension ext ) => _client. AddExtension ( ext );
        public T GetExtension < T > ( ) where T : BaseExtension => _client. GetExtension < T > ( );
        public Task ConnectAsync ( ) => _client. ConnectAsync ( );
        public Task ReconnectAsync ( Boolean startNewSession = False ) => _client. ReconnectAsync ( startNewSession );
        public Task DisconnectAsync ( ) => _client. DisconnectAsync ( );
        public Task < DiscordUser > GetUserAsync ( UInt64 userId ) => _client. GetUserAsync ( userId );
        public Task < DiscordChannel > GetChannelAsync ( UInt64 id ) => _client. GetChannelAsync ( id );
        public Task < DiscordMessage > SendMessageAsync ( DiscordChannel channel , String content = null , Boolean isTTS = False , DiscordEmbed embed = null ) => _client. SendMessageAsync ( channel , content , isTTS , embed );
        public Task < DiscordGuild > CreateGuildAsync ( String name , String region = null , Optional < Stream > icon = default , Nullable < VerificationLevel > verificationLevel = default , Nullable < DefaultMessageNotifications > defaultMessageNotifications = default ) => _client. CreateGuildAsync ( name , region , icon , verificationLevel , defaultMessageNotifications );
        public Task < DiscordGuild > GetGuildAsync ( UInt64 id ) => _client. GetGuildAsync ( id );
        public Task < DiscordInvite > GetInviteByCodeAsync ( String code ) => _client. GetInviteByCodeAsync ( code );
        public Task < IReadOnlyList < DiscordConnection > > GetConnectionsAsync ( ) => _client. GetConnectionsAsync ( );
        public Task < DiscordWebhook > GetWebhookAsync ( UInt64 id ) => _client. GetWebhookAsync ( id );
        public Task < DiscordWebhook > GetWebhookWithTokenAsync ( UInt64 id , String token ) => _client. GetWebhookWithTokenAsync ( id , token );
        public Task UpdateStatusAsync ( DiscordActivity activity = null , Nullable < UserStatus > userStatus = default , Nullable < DateTimeOffset > idleSince = default ) => _client. UpdateStatusAsync ( activity , userStatus , idleSince );
        public Task < DiscordApplication > GetApplicationAsync ( UInt64 id ) => _client. GetApplicationAsync ( id );
        public Task < DiscordUser > UpdateCurrentUserAsync ( String username = null , Optional < Stream > avatar = default ) => _client. UpdateCurrentUserAsync ( username , avatar );
        public Task SyncGuildsAsync ( params DiscordGuild[] guilds ) => _client. SyncGuildsAsync ( guilds );
        public Task < DiscordApplication > GetCurrentApplicationAsync ( ) => _client. GetCurrentApplicationAsync ( );
        public Task < IReadOnlyList < DiscordVoiceRegion > > ListVoiceRegionsAsync ( ) => _client. ListVoiceRegionsAsync ( );
        public Task InitializeAsync ( ) => _client. InitializeAsync ( );
        public override String ToString ( ) => _client. ToString ( );
        public override Boolean Equals ( Object obj ) => _client. Equals ( obj );
        public override Int32 GetHashCode ( ) => _client. GetHashCode ( );
        public Int32 GatewayVersion { get => _client. GatewayVersion ; }
        public Uri GatewayUri { get => _client. GatewayUri ; }
        public Int32 ShardCount { get => _client. ShardCount ; }
        public Int32 ShardId { get => _client. ShardId ; }
        public IReadOnlyList < DiscordDmChannel > PrivateChannels { get => _client. PrivateChannels ; }
        public IReadOnlyDictionary < UInt64 , DiscordGuild > Guilds { get => _client. Guilds ; }
        public Int32 Ping { get => _client. Ping ; }
        public IReadOnlyDictionary < UInt64 , DiscordPresence > Presences { get => _client. Presences ; }
        public DebugLogger DebugLogger { get => _client. DebugLogger ; }
        public String VersionString { get => _client. VersionString ; }
        public DiscordUser CurrentUser { get => _client. CurrentUser ; }
        public DiscordApplication CurrentApplication { get => _client. CurrentApplication ; }
        public IReadOnlyDictionary < String , DiscordVoiceRegion > VoiceRegions { get => _client. VoiceRegions ; }
        public event AsyncEventHandler < ClientErrorEventArgs > ClientErrored { add => _client. ClientErrored += value; remove => _client. ClientErrored -= value; }
        public event AsyncEventHandler < SocketErrorEventArgs > SocketErrored { add => _client. SocketErrored += value; remove => _client. SocketErrored -= value; }
        public event AsyncEventHandler SocketOpened { add => _client. SocketOpened += value; remove => _client. SocketOpened -= value; }
        public event AsyncEventHandler < SocketCloseEventArgs > SocketClosed { add => _client. SocketClosed += value; remove => _client. SocketClosed -= value; }
        public event AsyncEventHandler < ReadyEventArgs > Ready { add => _client. Ready += value; remove => _client. Ready -= value; }
        public event AsyncEventHandler < ReadyEventArgs > Resumed { add => _client. Resumed += value; remove => _client. Resumed -= value; }
        public event AsyncEventHandler < ChannelCreateEventArgs > ChannelCreated { add => _client. ChannelCreated += value; remove => _client. ChannelCreated -= value; }
        public event AsyncEventHandler < DmChannelCreateEventArgs > DmChannelCreated { add => _client. DmChannelCreated += value; remove => _client. DmChannelCreated -= value; }
        public event AsyncEventHandler < ChannelUpdateEventArgs > ChannelUpdated { add => _client. ChannelUpdated += value; remove => _client. ChannelUpdated -= value; }
        public event AsyncEventHandler < ChannelDeleteEventArgs > ChannelDeleted { add => _client. ChannelDeleted += value; remove => _client. ChannelDeleted -= value; }
        public event AsyncEventHandler < DmChannelDeleteEventArgs > DmChannelDeleted { add => _client. DmChannelDeleted += value; remove => _client. DmChannelDeleted -= value; }
        public event AsyncEventHandler < ChannelPinsUpdateEventArgs > ChannelPinsUpdated { add => _client. ChannelPinsUpdated += value; remove => _client. ChannelPinsUpdated -= value; }
        public event AsyncEventHandler < GuildCreateEventArgs > GuildCreated { add => _client. GuildCreated += value; remove => _client. GuildCreated -= value; }
        public event AsyncEventHandler < GuildCreateEventArgs > GuildAvailable { add => _client. GuildAvailable += value; remove => _client. GuildAvailable -= value; }
        public event AsyncEventHandler < GuildUpdateEventArgs > GuildUpdated { add => _client. GuildUpdated += value; remove => _client. GuildUpdated -= value; }
        public event AsyncEventHandler < GuildDeleteEventArgs > GuildDeleted { add => _client. GuildDeleted += value; remove => _client. GuildDeleted -= value; }
        public event AsyncEventHandler < GuildDeleteEventArgs > GuildUnavailable { add => _client. GuildUnavailable += value; remove => _client. GuildUnavailable -= value; }
        public event AsyncEventHandler < GuildDownloadCompletedEventArgs > GuildDownloadCompleted { add => _client. GuildDownloadCompleted += value; remove => _client. GuildDownloadCompleted -= value; }
        public event AsyncEventHandler < MessageCreateEventArgs > MessageCreated { add => _client. MessageCreated += value; remove => _client. MessageCreated -= value; }
        public event AsyncEventHandler < PresenceUpdateEventArgs > PresenceUpdated { add => _client. PresenceUpdated += value; remove => _client. PresenceUpdated -= value; }
        public event AsyncEventHandler < GuildBanAddEventArgs > GuildBanAdded { add => _client. GuildBanAdded += value; remove => _client. GuildBanAdded -= value; }
        public event AsyncEventHandler < GuildBanRemoveEventArgs > GuildBanRemoved { add => _client. GuildBanRemoved += value; remove => _client. GuildBanRemoved -= value; }
        public event AsyncEventHandler < GuildEmojisUpdateEventArgs > GuildEmojisUpdated { add => _client. GuildEmojisUpdated += value; remove => _client. GuildEmojisUpdated -= value; }
        public event AsyncEventHandler < GuildIntegrationsUpdateEventArgs > GuildIntegrationsUpdated { add => _client. GuildIntegrationsUpdated += value; remove => _client. GuildIntegrationsUpdated -= value; }
        public event AsyncEventHandler < GuildMemberAddEventArgs > GuildMemberAdded { add => _client. GuildMemberAdded += value; remove => _client. GuildMemberAdded -= value; }
        public event AsyncEventHandler < GuildMemberRemoveEventArgs > GuildMemberRemoved { add => _client. GuildMemberRemoved += value; remove => _client. GuildMemberRemoved -= value; }
        public event AsyncEventHandler < GuildMemberUpdateEventArgs > GuildMemberUpdated { add => _client. GuildMemberUpdated += value; remove => _client. GuildMemberUpdated -= value; }
        public event AsyncEventHandler < GuildRoleCreateEventArgs > GuildRoleCreated { add => _client. GuildRoleCreated += value; remove => _client. GuildRoleCreated -= value; }
        public event AsyncEventHandler < GuildRoleUpdateEventArgs > GuildRoleUpdated { add => _client. GuildRoleUpdated += value; remove => _client. GuildRoleUpdated -= value; }
        public event AsyncEventHandler < GuildRoleDeleteEventArgs > GuildRoleDeleted { add => _client. GuildRoleDeleted += value; remove => _client. GuildRoleDeleted -= value; }
        public event AsyncEventHandler < MessageAcknowledgeEventArgs > MessageAcknowledged { add => _client. MessageAcknowledged += value; remove => _client. MessageAcknowledged -= value; }
        public event AsyncEventHandler < MessageUpdateEventArgs > MessageUpdated { add => _client. MessageUpdated += value; remove => _client. MessageUpdated -= value; }
        public event AsyncEventHandler < MessageDeleteEventArgs > MessageDeleted { add => _client. MessageDeleted += value; remove => _client. MessageDeleted -= value; }
        public event AsyncEventHandler < MessageBulkDeleteEventArgs > MessagesBulkDeleted { add => _client. MessagesBulkDeleted += value; remove => _client. MessagesBulkDeleted -= value; }
        public event AsyncEventHandler < TypingStartEventArgs > TypingStarted { add => _client. TypingStarted += value; remove => _client. TypingStarted -= value; }
        public event AsyncEventHandler < UserSettingsUpdateEventArgs > UserSettingsUpdated { add => _client. UserSettingsUpdated += value; remove => _client. UserSettingsUpdated -= value; }
        public event AsyncEventHandler < UserUpdateEventArgs > UserUpdated { add => _client. UserUpdated += value; remove => _client. UserUpdated -= value; }
        public event AsyncEventHandler < VoiceStateUpdateEventArgs > VoiceStateUpdated { add => _client. VoiceStateUpdated += value; remove => _client. VoiceStateUpdated -= value; }
        public event AsyncEventHandler < VoiceServerUpdateEventArgs > VoiceServerUpdated { add => _client. VoiceServerUpdated += value; remove => _client. VoiceServerUpdated -= value; }
        public event AsyncEventHandler < GuildMembersChunkEventArgs > GuildMembersChunked { add => _client. GuildMembersChunked += value; remove => _client. GuildMembersChunked -= value; }
        public event AsyncEventHandler < UnknownEventArgs > UnknownEvent { add => _client. UnknownEvent += value; remove => _client. UnknownEvent -= value; }
        public event AsyncEventHandler < MessageReactionAddEventArgs > MessageReactionAdded { add => _client. MessageReactionAdded += value; remove => _client. MessageReactionAdded -= value; }
        public event AsyncEventHandler < MessageReactionRemoveEventArgs > MessageReactionRemoved { add => _client. MessageReactionRemoved += value; remove => _client. MessageReactionRemoved -= value; }
        public event AsyncEventHandler < MessageReactionsClearEventArgs > MessageReactionsCleared { add => _client. MessageReactionsCleared += value; remove => _client. MessageReactionsCleared -= value; }
        public event AsyncEventHandler < WebhooksUpdateEventArgs > WebhooksUpdated { add => _client. WebhooksUpdated += value; remove => _client. WebhooksUpdated -= value; }
        public event AsyncEventHandler < HeartbeatEventArgs > Heartbeated { add => _client. Heartbeated += value; remove => _client. Heartbeated -= value; }
    }
}
