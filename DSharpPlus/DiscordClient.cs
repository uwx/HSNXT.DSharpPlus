﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.Voice;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;

namespace DSharpPlus
{
    /// <summary>
    /// A Discord api wrapper
    /// </summary>
    public class DiscordClient : IDisposable
    {
        #region Events
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler SocketOpened;
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<CloseEventArgs> SocketClosed;
        /// <summary>
        /// The ready event is dispatched when a client completed the initial handshake.
        /// </summary>
        public event EventHandler Ready;
        /// <summary>
        /// Sent when a new channel is created.
        /// </summary>
        public event EventHandler<ChannelCreateEventArgs> ChannelCreated;
        /// <summary>
        /// Sent when a new dm channel is created.
        /// </summary>
        public event EventHandler<DMChannelCreateEventArgs> DMChannelCreated;
        /// <summary>
        /// Sent when a channel is updated.
        /// </summary>
        public event EventHandler<ChannelUpdateEventArgs> ChannelUpdated;
        /// <summary>
        /// Sent when a channel is deleted
        /// </summary>
        public event EventHandler<ChannelDeleteEventArgs> ChannelDeleted;
        /// <summary>
        /// Sent when a dm channel is deleted
        /// </summary>
        public event EventHandler<DMChannelDeleteEventArgs> DMChannelDeleted;
        /// <summary>
        /// Sent when the user joins a new guild.
        /// </summary>
        public event EventHandler<GuildCreateEventArgs> GuildCreated;
        /// <summary>
        /// Sent when a guild is becoming available.
        /// </summary>
        public event EventHandler<GuildCreateEventArgs> GuildAvailable;
        /// <summary>
        /// Sent when a guild is updated.
        /// </summary>
        public event EventHandler<GuildUpdateEventArgs> GuildUpdated;
        /// <summary>
        /// Sent when the user leaves or is removed from a guild.
        /// </summary>
        public event EventHandler<GuildDeleteEventArgs> GuildDeleted;
        /// <summary>
        /// Sent when a guild becomes unavailable.
        /// </summary>
        public event EventHandler<GuildDeleteEventArgs> GuildUnavailable;
        /// <summary>
        /// Sent when a message is created.
        /// </summary>
        public event EventHandler<MessageCreateEventArgs> MessageCreated;

        /// <summary>
        /// Sent when a presence has been updated.
        /// </summary>
        public event EventHandler<PresenceUpdateEventArgs> PresenceUpdate;

        /// <summary>
        /// Sent when a guild ban gets added
        /// </summary>
        public event EventHandler<GuildBanAddEventArgs> GuildBanAdd;

        /// <summary>
        /// Sent when a guild ban gets removed
        /// </summary>
        public event EventHandler<GuildBanRemoveEventArgs> GuildBanRemove;

        /// <summary>
        /// Sent when a guilds emojis get updated
        /// </summary>
        public event EventHandler<GuildEmojisUpdateEventArgs> GuildEmojisUpdate;

        /// <summary>
        /// Sent when a guild integration is updated.
        /// </summary>
        public event EventHandler<GuildIntegrationsUpdateEventArgs> GuildIntegrationsUpdate;

        /// <summary>
        /// Sent when a new user joins a guild.
        /// </summary>
        public event EventHandler<GuildMemberAddEventArgs> GuildMemberAdd;

        /// <summary>
        /// Sent when a user is removed from a guild (leave/kick/ban).
        /// </summary>
        public event EventHandler<GuildMemberRemoveEventArgs> GuildMemberRemove;

        /// <summary>
        /// Sent when a guild member is updated.
        /// </summary>
        public event EventHandler<GuildMemberUpdateEventArgs> GuildMemberUpdate;

        /// <summary>
        /// Sent when a guild role is created.
        /// </summary>
        public event EventHandler<GuildRoleCreateEventArgs> GuildRoleCreate;

        /// <summary>
        /// Sent when a guild role is updated.
        /// </summary>
        public event EventHandler<GuildRoleUpdateEventArgs> GuildRoleUpdate;

        /// <summary>
        /// Sent when a guild role is updated.
        /// </summary>
        public event EventHandler<GuildRoleDeleteEventArgs> GuildRoleDelete;

        /// <summary>
        /// Sent when a message is updated.
        /// </summary>
        public event EventHandler<MessageUpdateEventArgs> MessageUpdate;

        /// <summary>
        /// Sent when a message is deleted.
        /// </summary>
        public event EventHandler<MessageDeleteEventArgs> MessageDelete;

        /// <summary>
        /// Sent when multiple messages are deleted at once.
        /// </summary>
        public event EventHandler<MessageBulkDeleteEventArgs> MessageBulkDelete;

        /// <summary>
        /// Sent when a user starts typing in a channel.
        /// </summary>
        public event EventHandler<TypingStartEventArgs> TypingStart;

        /// <summary>
        /// Sent when the current user updates their settings.
        /// </summary>
        public event EventHandler<UserSettingsUpdateEventArgs> UserSettingsUpdate;

        /// <summary>
        /// Sent when properties about the user change.
        /// </summary>
        public event EventHandler<UserUpdateEventArgs> UserUpdate;

        /// <summary>
        /// Sent when someone joins/leaves/moves voice channels.
        /// </summary>
        public event EventHandler<VoiceStateUpdateEventArgs> VoiceStateUpdate;

        /// <summary>
        /// Sent when a guild's voice server is updated.
        /// </summary>
        public event EventHandler<VoiceServerUpdateEventArgs> VoiceServerUpdate;

        /// <summary>
        /// Sent in response to Gateway Request Guild Members.
        /// </summary>
        public event EventHandler<GuildMembersChunkEventArgs> GuildMembersChunk;

        /// <summary>
        /// Sent when an unknown event gets received.
        /// </summary>
        public event EventHandler<UnknownEventArgs> UnknownEvent;

        /// <summary>
        /// Sent when a reaction gets added to a message.
        /// </summary>
        public event EventHandler<MessageReactionAddEventArgs> MessageReactionAdd;

        /// <summary>
        /// Sent when a reaction gets removed from a message.
        /// </summary>
        public event EventHandler<MessageReactionRemoveEventArgs> MessageReactionRemove;

        /// <summary>
        /// Sent when all reactions get removed from a message.
        /// </summary>
        public event EventHandler<MessageReactionRemoveAllEventArgs> MessageReactionRemoveAll;

        public event EventHandler<WebhooksUpdateEventArgs> WebhooksUpdate;

        public event EventHandler<UserSpeakingEventArgs> UserSpeaking;
        public event EventHandler<VoiceReceivedEventArgs> VoiceReceived;
        #endregion

        #region Internal Variables
        internal static CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        internal static CancellationToken _cancelToken = _cancelTokenSource.Token;

        internal static DiscordConfig config;

        internal static List<IModule> _modules = new List<IModule>();

        internal static WebSocketClient _websocketClient;
        internal static int _sequence = 0;
        internal static string _sessionToken = "";
        internal static string _sessionID = "";
        internal static int _heartbeatInterval;
        internal Thread _heartbeatThread;
        internal static DateTime _lastHeartbeat;
        internal static bool _waitingForAck = false;

        internal static DiscordVoiceClient _voiceClient;
        internal static Dictionary<uint, ulong> _ssrcDict = new Dictionary<uint, ulong>();

        internal static Dictionary<ulong, DiscordPresence> _presences = new Dictionary<ulong, DiscordPresence>();
        #endregion

        #region Public Variables
        internal static DebugLogger _debugLogger = new DebugLogger();
        /// <summary>
        /// 
        /// </summary>
        public DebugLogger DebugLogger => _debugLogger;

        internal static int _gatewayVersion;
        /// <summary>
        /// Gateway protocol version
        /// </summary>
        public int GatewayVersion => _gatewayVersion;

        public DiscordVoiceClient VoiceClient => _voiceClient;

        internal static string _gatewayUrl = "";
        /// <summary>
        /// Gateway url
        /// </summary>
        public string GatewayUrl => _gatewayUrl;

        internal static int _shardCount = 1;
        /// <summary>
        /// Number of shards the bot is connected with
        /// </summary>
        public int Shards => _shardCount;

        internal static DiscordUser _me;
        /// <summary>
        /// The current user
        /// </summary>
        public DiscordUser Me => _me;

        internal static List<DiscordDMChannel> _privateChannels = new List<DiscordDMChannel>();
        /// <summary>
        /// List of DM Channels
        /// </summary>
        public List<DiscordDMChannel> PrivateChannels => _privateChannels;

        internal static Dictionary<ulong, DiscordGuild> _guilds = new Dictionary<ulong, DiscordGuild>();
        /// <summary>
        /// List of Guilds
        /// </summary>
        public Dictionary<ulong, DiscordGuild> Guilds => _guilds;
        #endregion

        /// <summary>
        /// Intializes a new instance of DiscordClient
        /// </summary>
        public DiscordClient()
        {

            Task.Run(async () =>
            {
                await InternalSetup();
            });
        }

        /// <summary>
        /// Initializes a new instance of DiscordClient
        /// </summary>
        /// <param name="config">Overwrites the default config</param>
        public DiscordClient(DiscordConfig config)
        {
            DiscordClient.config = config;

            Task.Run(async () =>
            {
                await InternalSetup();
            });
        }

        internal async Task InternalSetup()
        {
            await Task.Run(() =>
            {
                if (config.UseInternalLogHandler)
                    DebugLogger.LogMessageReceived += (sender, e) => DebugLogger.LogHandler(sender, e);
            });
        }

        /// <summary>
        /// Connects to the gateway
        /// </summary>
        /// <param name="shard">Shard to connect from</param>
        /// <returns></returns>
        public async Task Connect(int shard = 0) => await InternalConnect(shard);

        /// <summary>
        /// Connects to the gateway
        /// </summary>
        /// <param name="tokenOverride"></param>
        /// <param name="tokenType"></param>
        /// <param name="shard">Shard to connect from</param>
        /// <returns></returns>
        public async Task Connect(string tokenOverride, TokenType tokenType, int shard = 0)
        {
            config.Token = tokenOverride;
            config.TokenType = tokenType;

            await InternalConnect(shard);
        }

        /// <summary>
        /// Adds a new module to the module list
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public IModule AddModule(IModule module)
        {
            module.Setup(this);
            _modules.Add(module);
            return module;
        }

        /// <summary>
        /// Gets a module from the module list by type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetModule<T>() where T : class, IModule
        {
            return _modules.Find(x => x.GetType() == typeof(T)) as T;
        }

        public async Task Reconnect() => await InternalReconnect();

        public async Task Reconnect(string tokenOverride, TokenType tokenType, int shard = 0) => await InternalReconnect(tokenOverride, tokenType, shard);

        internal async Task InternalReconnect()
        {
            await Disconnect();
            await Connect();
        }

        internal async Task InternalReconnect(string tokenOverride, TokenType tokenType, int shard)
        {
            await Disconnect();
            // delay task by 2 seconds to make sure everything gets closed correctly
            await Task.Delay(1000);
            await Connect(tokenOverride, tokenType, shard);
        }

        internal async Task InternalConnect(int shard)
        {
            await InternalUpdateGateway();
            _me = await InternalGetCurrentUser();

            _websocketClient = new WebSocketClient(_gatewayUrl + "?v=5&encoding=json");
            _websocketClient.SocketOpened += async (sender, e) =>
            {
                _privateChannels = new List<DiscordDMChannel>();
                _guilds = new Dictionary<ulong, DiscordGuild>();

                if (_sessionID == "")
                    await SendIdentify(shard);
                else
                    await SendResume();
                SocketOpened?.Invoke(sender, e);
            };
            _websocketClient.SocketClosed += async (sender, e) =>
            {
                await Task.Run(() =>
                {
                    _heartbeatThread.Abort();

                    _debugLogger.LogMessage((e.WasClean ? LogLevel.Debug : LogLevel.Critical), "Websocket", $"Connection closed: {e.Reason} [WasClean: {e.WasClean}]", DateTime.Now);

                    if (!e.WasClean && config.AutoReconnect)
                    {
                        _websocketClient.Disconnect();
                        _websocketClient.Connect();
                        DebugLogger.LogMessage(LogLevel.Critical, "Internal", "Bot crashed. Reconnecting", DateTime.Now);
                    }
                    SocketClosed?.Invoke(sender, e);
                });
            };
            _websocketClient.SocketMessage += async (sender, e) => await HandleSocketMessage(e.Data, shard);
            _websocketClient.Connect();

            _voiceClient = new DiscordVoiceClient();
            _voiceClient.UserSpeaking += async (sender, e) =>
            {
                await Task.Run(() =>
                {
                    UserSpeaking?.Invoke(this, e);
                });
            };
            _voiceClient.VoiceReceived += async (sender, e) =>
            {
                await Task.Run(() =>
                {
                    VoiceReceived?.Invoke(this,
                        _ssrcDict.ContainsKey(e.SSRC)
                            ? new VoiceReceivedEventArgs(e.SSRC, _ssrcDict[e.SSRC], e.Voice, e.VoiceLength)
                            : e);
                });
            };
        }

        internal async Task InternalUpdateGuild(DiscordGuild guild)
        {
            await Task.Run(() =>
            {
                if (Guilds[guild.ID] == null)
                    Guilds.Add(guild.ID, guild);
                else
                    Guilds[guild.ID] = guild;
            });
        }

        internal static async Task InternalUpdateGateway()
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Gateway;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            if (config.TokenType == TokenType.Bot)
                url += Endpoints.Bot;

            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);

            JObject jObj = JObject.Parse(response.Response);
            _gatewayUrl = jObj.Value<string>("url");
            if (jObj["shards"] != null)
                _shardCount = jObj.Value<int>("shards");
        }

        /// <summary>
        /// Disconnects from the gateway
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Disconnect()
        {
            return await Task.Run(() =>
            {
                _cancelTokenSource.Cancel();
                _websocketClient.Disconnect();

                return true;
            });
        }

        #region Public Functions
        /// <summary>
        /// Gets a user
        /// </summary>
        /// <param name="user">userid or @me</param>
        /// <returns></returns>
        public async Task<DiscordUser> GetUser(string user) => await InternalGetUser(user);
        /// <summary>
        /// Deletes a channel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteChannel(ulong id) => await InternalDeleteChannel(id);
        /// <summary>
        /// Deletes a channel
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public async Task DeleteChannel(DiscordChannel channel) => await InternalDeleteChannel(channel.ID);
        /// <summary>
        /// Gets a message
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="MessageID"></param>
        /// <returns></returns>
        public async Task<DiscordMessage> GetMessage(DiscordChannel channel, ulong MessageID) => await InternalGetMessage(channel.ID, MessageID);
        /// <summary>
        /// Gets a message
        /// </summary>
        /// <param name="ChannelID"></param>
        /// <param name="MessageID"></param>
        /// <returns></returns>
        public async Task<DiscordMessage> GetMessage(ulong ChannelID, ulong MessageID) => await InternalGetMessage(ChannelID, MessageID);
        /// <summary>
        /// Gets a channel
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<DiscordChannel> GetChannel(ulong ID) => await InternalGetChannel(ID);
        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="ChannelID"></param>
        /// <param name="content"></param>
        /// <param name="tts"></param>
        /// <returns></returns>
        public async Task<DiscordMessage> SendMessage(ulong ChannelID, string content, bool tts = false) => await InternalCreateMessage(ChannelID, content, tts);
        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="Channel"></param>
        /// <param name="content"></param>
        /// <param name="tts"></param>
        /// <returns></returns>
        public async Task<DiscordMessage> SendMessage(DiscordChannel Channel, string content, bool tts = false) => await InternalCreateMessage(Channel.ID, content, tts);
        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="Channel"></param>
        /// <param name="content"></param>
        /// <param name="tts"></param>
        /// <returns></returns>
        public async Task<DiscordMessage> SendMessage(DiscordDMChannel Channel, string content, bool tts = false) => await InternalCreateMessage(Channel.ID, content, tts);
        /// <summary>
        /// Creates a guild. Only for whitelisted bots
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<DiscordGuild> CreateGuild(string name) => await InternalCreateGuildAsync(name);
        /// <summary>
        /// Gets a guild
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DiscordGuild> GetGuild(ulong id) => await InternalGetGuild(id);
        /// <summary>
        /// Deletes a guild
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<DiscordGuild> DeleteGuild(ulong ID) => await InternalDeleteGuild(ID);
        /// <summary>
        /// Deletes a guild
        /// </summary>
        /// <param name="Guild"></param>
        /// <returns></returns>
        public async Task<DiscordGuild> DeleteGuild(DiscordGuild Guild) => await InternalDeleteGuild(Guild.ID);
        /// <summary>
        /// Gets a channel
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<DiscordChannel> GetChannelByID(ulong ID) => await InternalGetChannel(ID);
        /// <summary>
        /// Gets an invite
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<DiscordInvite> GetInviteByCode(string code) => await InternalGetInvite(code);
        /// <summary>
        /// Gets a list of connections
        /// </summary>
        /// <returns></returns>
        public async Task<List<DiscordConnection>> GetConnections() => await InternalGetUsersConnections();
        /// <summary>
        /// Gets a list of regions
        /// </summary>
        /// <returns></returns>
        public async Task<List<DiscordVoiceRegion>> ListRegions() => await InternalListVoiceRegions();
        /// <summary>
        /// Gets a webhook
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<DiscordWebhook> GetWebhook(ulong ID) => await InternalGetWebhook(ID);
        /// <summary>
        /// Gets a webhook
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<DiscordWebhook> GetWebhookWithToken(ulong ID, string token) => await InternalGetWebhookWithToken(ID, token);
        /// <summary>
        /// Creates a dm
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<DiscordDMChannel> CreateDM(ulong UserID) => await InternalCreateDM(UserID);
        /// <summary>
        /// Updates current user's status
        /// </summary>
        /// <param name="game">Game you're playing</param>
        /// <param name="idle_since"></param>
        /// <returns></returns>
        public async Task UpdateStatus(string game = "", int idle_since = -1) => InternalUpdateStatus(game, idle_since);

        /// <summary>
        /// Modifies a guild member
        /// </summary>
        /// <param name="GuildID">Guild's ID</param>
        /// <param name="MemberID">Member's ID</param>
        /// <param name="Nickname">Member's (new) Nickname</param>
        /// <param name="Roles">Member's roles</param>
        /// <param name="Muted">Wether this member has been muted or not (voice)</param>
        /// <param name="Deaf">Wether this member has been deafened or not (voice)</param>
        /// <param name="VoiceChannelID">Voice channel ID for moving this user around</param>
        /// <returns></returns>
        public async Task ModifyMember(ulong GuildID, ulong MemberID, string Nickname = null, List<ulong> Roles = null, bool Muted = false, bool Deaf = false, ulong VoiceChannelID = 0) =>
            await InternalModifyGuildMember(GuildID, MemberID, Nickname, Roles, Muted, Deaf, VoiceChannelID);

        /// <summary>
        /// Gets the current API appication.
        /// </summary>
        /// <returns></returns>
        public async Task<DiscordApplication> GetCurrentApp() => await InternalGetApplicationInfo("@me");

        /// <summary>
        /// Gets cached presence for a user.
        /// </summary>
        /// <param name="UserID">User's ID</param>
        /// <returns></returns>
        public DiscordPresence GetUserPresence(ulong UserID) => InternalGetUserPresence(UserID);
        /// <summary>
        /// Lists guild members
        /// </summary>
        /// <param name="guildID">Guild's ID</param>
        /// <param name="limit">limit of members to return</param>
        /// <param name="after">index to start from</param>
        /// <returns></returns>
        public async Task<List<DiscordMember>> ListGuildMembers(ulong guildID, int limit, int after) => await InternalListGuildMembers(guildID, limit, after);
        #endregion

        #region Websocket
        internal async Task HandleSocketMessage(string data, int shard)
        {
            JObject obj = JObject.Parse(data);
            switch (obj.Value<int>("op"))
            {
                case 0: await OnDispatch(obj); break;
                case 1: await OnHeartbeat(); break;
                case 7: await OnReconnect(); break;
                case 9: await OnInvalidateSession(obj, shard); break;
                case 10: await OnHello(obj); break;
                case 11: await OnHeartbeatAck(); break;
                default:
                    {
                        DebugLogger.LogMessage(LogLevel.Warning, "Websocket", $"Unknown OP-Code: {obj.Value<int>("op")}\n{obj}", DateTime.Now);
                        break;
                    }
            }
        }

        internal async Task OnDispatch(JObject obj)
        {
            switch (obj.Value<string>("t").ToLower())
            {
                case "ready": await OnReadyEvent(obj); break;
                case "channel_create": await OnChannelCreateEvent(obj); break;
                case "channel_update": await OnChannelUpdateEvent(obj); break;
                case "channel_delete": await OnChannelDeleteEvent(obj); break;
                case "guild_create": await OnGuildCreateEvent(obj); break;
                case "guild_update": await OnGuildUpdateEvent(obj); break;
                case "guild_delete": await OnGuildDeleteEvent(obj); break;
                case "guild_ban_add": await OnGuildBanAddEvent(obj); break;
                case "guild_ban_remove": await OnGuildBanRemoveEvent(obj); break;
                case "guild_emojis_update": await OnGuildEmojisUpdateEvent(obj); break;
                case "guild_integrations_update": await OnGuildIntegrationsUpdateEvent(obj); break;
                case "guild_member_add": await OnGuildMemberAddEvent(obj); break;
                case "guild_member_remove": await OnGuildMemberRemoveEvent(obj); break;
                case "guild_member_update": await OnGuildMemberUpdateEvent(obj); break;
                case "guild_member_chunk": await OnGuildMembersChunkEvent(obj); break;
                case "guild_role_create": await OnGuildRoleCreateEvent(obj); break;
                case "guild_role_update": await OnGuildRoleUpdateEvent(obj); break;
                case "guild_role_delete": await OnGuildRoleDeleteEvent(obj); break;
                case "message_create": await OnMessageCreateEvent(obj); break;
                case "message_update": await OnMessageUpdateEvent(obj); break;
                case "message_delete": await OnMessageDeleteEvent(obj); break;
                case "message_delete_bulk": await OnMessageBulkDeleteEvent(obj); break;
                case "presence_update": await OnPresenceUpdateEvent(obj); break;
                case "typing_start": await OnTypingStartEvent(obj); break;
                case "user_settings_update": await OnUserSettingsUpdateEvent(obj); break;
                case "user_update": await OnUserUpdateEvent(obj); break;
                case "voice_state_update": await OnVoiceStateUpdateEvent(obj); break;
                case "voice_server_update": await OnVoiceServerUpdateEvent(obj); break;
                case "message_reaction_add": await OnMessageReactionAdd(obj); break;
                case "message_reaction_remove": await OnMessageReactionRemove(obj); break;
                case "message_reaction_remove_all": await OnMessageReactionRemoveAll(obj); break;
                case "webhooks_update": await OnWebhooksUpdate(obj); break;
                default:
                    await OnUnknownEvent(obj);
                    DebugLogger.LogMessage(LogLevel.Warning, "Websocket", $"Unknown event: {obj.Value<string>("t")}\n{obj["d"]}", DateTime.Now);
                    break;
            }
        }

        #region Events
        internal async Task OnReadyEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                _gatewayVersion = obj["d"]["v"].ToObject<int>();
                _me = obj["d"]["user"].ToObject<DiscordUser>();
                _privateChannels = obj["d"]["private_channels"].ToObject<List<DiscordDMChannel>>();
                if (config.TokenType != TokenType.User)
                {
                    foreach (JObject guild in obj["d"]["guilds"])
                    {
                        if (!_guilds.ContainsKey(guild.Value<ulong>("id")))
                            _guilds.Add(guild.Value<ulong>("id"), guild.ToObject<DiscordGuild>());
                    }
                }
                _sessionID = obj["d"]["session_id"].ToString();

                Ready?.Invoke(this, new EventArgs());
            });
        }
        internal async Task OnChannelCreateEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                if (obj["d"]["is_private"] != null && obj["d"]["is_private"].ToObject<bool>())
                {
                    DiscordDMChannel channel = obj["d"].ToObject<DiscordDMChannel>();
                    _privateChannels.Add(channel);

                    DMChannelCreated?.Invoke(this, new DMChannelCreateEventArgs { Channel = channel });
                }
                else
                {
                    DiscordChannel channel = obj["d"].ToObject<DiscordChannel>();

                    _guilds[channel.GuildID].Channels.Add(channel);

                    ChannelCreated?.Invoke(this, new ChannelCreateEventArgs { Channel = channel, Guild = _guilds[channel.GuildID] });
                }
            });
        }
        internal async Task OnChannelUpdateEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                DiscordChannel channel = obj["d"].ToObject<DiscordChannel>();

                if (_guilds != null && _guilds.ContainsKey(channel.GuildID)
                && _guilds[channel.GuildID]?.Channels?.Find(x => x.ID == channel.ID) != null)
                {
                    int channelIndex = _guilds[channel.GuildID].Channels.FindIndex(x => x.ID == channel.ID);
                    _guilds[channel.GuildID].Channels[channelIndex] = channel;
                }
                else
                {
                    if (_guilds[channel.GuildID].Channels == null)
                        _guilds[channel.GuildID].Channels = new List<DiscordChannel>();
                    _guilds[channel.GuildID].Channels.Add(channel);
                }

                ChannelUpdated?.Invoke(this, new ChannelUpdateEventArgs { Channel = channel, Guild = _guilds[channel.GuildID] });
            });
        }
        internal async Task OnChannelDeleteEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                if (obj["d"]["is_private"] != null && obj["d"]["is_private"].ToObject<bool>())
                {
                    DiscordDMChannel channel = obj["d"].ToObject<DiscordDMChannel>();
                    int channelIndex = _privateChannels.FindIndex(x => x.ID == channel.ID);
                    _privateChannels.RemoveAt(channelIndex);

                    DMChannelDeleted?.Invoke(this, new DMChannelDeleteEventArgs { Channel = channel });
                }
                else
                {
                    DiscordChannel channel = obj["d"].ToObject<DiscordChannel>();
                    _guilds[channel.GuildID].Channels.RemoveAll(x => x.ID == channel.ID);

                    ChannelDeleted?.Invoke(this, new ChannelDeleteEventArgs { Channel = channel, Guild = _guilds[channel.GuildID] });
                }
            });
        }
        internal async Task OnGuildCreateEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                DiscordGuild guild = obj["d"].ToObject<DiscordGuild>();

                foreach (DiscordChannel channel in guild.Channels)
                    if (channel.GuildID == 0) channel.GuildID = guild.ID;

                foreach (DiscordPresence Presence in guild.Presences)
                {
                    if (_presences.ContainsKey(Presence.UserID))
                        _presences.Remove(Presence.UserID);
                    if (_presences != null && Presence != null)
                    {
                        try
                        {
                            _presences.Add(Presence.UserID, Presence);
                        }
                        catch (NullReferenceException)
                        {

                        }
                    }
                }

                if (_guilds.ContainsKey(obj["d"].Value<ulong>("id")))
                {
                    _guilds[guild.ID] = guild;

                    GuildAvailable?.Invoke(this, new GuildCreateEventArgs { Guild = guild });
                }
                else
                {
                    _guilds.Add(guild.ID, guild);

                    GuildCreated?.Invoke(this, new GuildCreateEventArgs { Guild = guild });
                }
            });
        }
        internal async Task OnGuildUpdateEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                DiscordGuild guild = _guilds[obj["d"].Value<ulong>("id")];
                if (guild != null)
                {
                    guild = obj["d"].ToObject<DiscordGuild>();
                    _guilds[guild.ID] = guild;

                    GuildUpdated?.Invoke(this, new GuildUpdateEventArgs { Guild = guild });
                }
                else
                {
                    guild = obj["d"].ToObject<DiscordGuild>();
                    _guilds.Add(guild.ID, guild);

                    GuildUpdated?.Invoke(this, new GuildUpdateEventArgs { Guild = guild });
                }
            });
        }
        internal async Task OnGuildDeleteEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                if (_guilds.ContainsKey(obj["d"].Value<ulong>("id")))
                {
                    if (obj["d"]["unavailable"] != null)
                    {
                        DiscordGuild guild = obj["d"].ToObject<DiscordGuild>();

                        _guilds[guild.ID] = guild;

                        GuildUnavailable?.Invoke(this, new GuildDeleteEventArgs { ID = obj["d"].Value<ulong>("id"), Unavailable = true });
                    }
                    else
                    {
                        _guilds.Remove(obj["d"].Value<ulong>("id"));

                        GuildDeleted?.Invoke(this, new GuildDeleteEventArgs { ID = obj["d"].Value<ulong>("id") });
                    }
                }
            });
        }
        internal async Task OnPresenceUpdateEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                ulong userID = (ulong)obj["d"]["user"]["id"];
                DiscordPresence p = obj["d"].ToObject<DiscordPresence>();
                if (_presences.ContainsKey(userID))
                    _presences[userID] = p;
                else
                    _presences.Add(userID, p);

                PresenceUpdateEventArgs args = obj["d"].ToObject<PresenceUpdateEventArgs>();
                PresenceUpdate?.Invoke(this, args);
            });
        }
        internal async Task OnGuildBanAddEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                DiscordUser user = obj["d"].ToObject<DiscordUser>();
                ulong guildID = ulong.Parse(obj["d"]["guild_id"].ToString());
                GuildBanAddEventArgs args = new GuildBanAddEventArgs { User = user, GuildID = guildID };
                GuildBanAdd?.Invoke(this, args);
            });
        }
        internal async Task OnGuildBanRemoveEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                DiscordUser user = obj["d"].ToObject<DiscordUser>();
                ulong guildID = ulong.Parse(obj["d"]["guild_id"].ToString());
                GuildBanRemoveEventArgs args = new GuildBanRemoveEventArgs { User = user, GuildID = guildID };
                GuildBanRemove?.Invoke(this, args);
            });
        }
        internal async Task OnGuildEmojisUpdateEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                ulong guildID = ulong.Parse(obj["d"]["guild_id"].ToString());
                List<DiscordEmoji> emojis = new List<DiscordEmoji>();
                foreach (JObject em in (JArray)obj["d"]["emojis"])
                {
                    emojis.Add(em.ToObject<DiscordEmoji>());
                }
                _guilds[guildID].Emojis = emojis;
                GuildEmojisUpdateEventArgs arga = new GuildEmojisUpdateEventArgs { GuildID = guildID, Emojis = emojis };
                GuildEmojisUpdate?.Invoke(this, arga);
            });
        }
        internal async Task OnGuildIntegrationsUpdateEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                ulong guildID = ulong.Parse(obj["d"]["guild_id"].ToString());
                GuildIntegrationsUpdateEventArgs args = new GuildIntegrationsUpdateEventArgs { GuildID = guildID };
                GuildIntegrationsUpdate?.Invoke(this, args);
            });
        }
        internal async Task OnGuildMemberAddEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                DiscordMember user = obj["d"].ToObject<DiscordMember>();
                ulong guildID = ulong.Parse(obj["d"]["guild_id"].ToString());
                _guilds[guildID].Members.Add(user);
                _guilds[guildID].MemberCount = _guilds[guildID].Members.Count;
                GuildMemberAddEventArgs args = new GuildMemberAddEventArgs { Member = user, GuildID = guildID };
                GuildMemberAdd?.Invoke(this, args);
            });
        }
        internal async Task OnGuildMemberRemoveEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                DiscordUser user = obj["d"]["user"].ToObject<DiscordUser>();
                ulong guildID = ulong.Parse(obj["d"]["guild_id"].ToString());
                if (_guilds[guildID].Members.Find(x => x.User.ID == user.ID) != null)
                {
                    int index = _guilds[guildID].Members.FindIndex(x => x.User.ID == user.ID);
                    _guilds[guildID].Members.RemoveAt(index);
                    _guilds[guildID].MemberCount = _guilds[guildID].Members.Count;
                }
                GuildMemberRemoveEventArgs args = new GuildMemberRemoveEventArgs { User = user, GuildID = guildID };
                GuildMemberRemove?.Invoke(this, args);
            });
        }
        internal async Task OnGuildMemberUpdateEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                DiscordUser user = obj["d"]["user"].ToObject<DiscordUser>();
                ulong guildID = ulong.Parse(obj["d"]["guild_id"].ToString());
                string nick = "";
                nick = obj["d"]["nick"].ToString();
                List<ulong> roles = new List<ulong>();
                if (obj["d"]["roles"] != null)
                {
                    JArray rolesjson = (JArray)obj["d"]["roles"];
                    foreach (var role in rolesjson)
                    {
                        roles.Add(ulong.Parse(role.ToString()));
                    }
                }
                int index = _guilds[guildID].Members.FindIndex(x => x.User.ID == user.ID);
                if (_guilds[guildID].Members.Find(x => x.User.ID == user.ID) != null)
                {
                    DiscordMember m = _guilds[guildID].Members[index];
                    m.Nickname = nick;
                    m.Roles = roles;
                    _guilds[guildID].Members[index] = m;
                }else
                {
                    DiscordMember m = new DiscordMember()
                    {
                        User = user,
                        Nickname = nick,
                        Roles = roles,
                    };
                    _guilds[guildID].Members.Add(m);
                }
                GuildMemberUpdateEventArgs args = new GuildMemberUpdateEventArgs { User = user, GuildID = guildID, Roles = roles, NickName = nick };
                GuildMemberUpdate?.Invoke(this, args);
            });
        }
        internal async Task OnGuildRoleCreateEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                ulong guildID = ulong.Parse(obj["d"]["guild_id"].ToString());
                DiscordRole role = obj["d"]["role"].ToObject<DiscordRole>();
                _guilds[guildID].Roles.Add(role);
                GuildRoleCreateEventArgs args = new GuildRoleCreateEventArgs { GuildID = guildID, Role = role };
                GuildRoleCreate?.Invoke(this, args);
            });
        }
        internal async Task OnGuildRoleUpdateEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                ulong guildID = ulong.Parse(obj["d"]["guild_id"].ToString());
                DiscordRole role = obj["d"]["role"].ToObject<DiscordRole>();
                int index = _guilds[guildID].Roles.FindIndex(x => x.ID == role.ID);
                _guilds[guildID].Roles[index] = role;
                GuildRoleUpdateEventArgs args = new GuildRoleUpdateEventArgs { GuildID = guildID, Role = role };
                GuildRoleUpdate?.Invoke(this, args);
            });
        }
        internal async Task OnGuildRoleDeleteEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                ulong guildID = ulong.Parse(obj["d"]["guild_id"].ToString());
                ulong roleID = obj["d"]["role_id"].ToObject<ulong>();
                int index = _guilds[guildID].Roles.FindIndex(x => x.ID == roleID);
                _guilds[guildID].Roles.RemoveAt(index);
                GuildRoleDeleteEventArgs args = new GuildRoleDeleteEventArgs { GuildID = guildID, RoleID = roleID };
                GuildRoleDelete?.Invoke(this, args);
            });
        }
        internal async Task OnMessageCreateEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                DiscordMessage message;
                try
                {
                    message = obj["d"].ToObject<DiscordMessage>();
                }
                catch (JsonSerializationException)
                {
                    JObject msg = (JObject)obj["d"];
                    msg["nonce"] = 0;
                    message = msg.ToObject<DiscordMessage>();
                }

                if (_privateChannels.Find(x => x.ID == message.ChannelID) == null)
                {
                    int channelindex = _guilds[message.Parent.Parent.ID].Channels.FindIndex(x => x.ID == message.ChannelID);
                    _guilds[message.Parent.Parent.ID].Channels[channelindex].LastMessageID = message.ID;
                }
                else
                {
                    int channelindex = _privateChannels.FindIndex(x => x.ID == message.ChannelID);
                    _privateChannels[channelindex].LastMessageID = message.ID;
                }

                List<DiscordMember> MentionedUsers = new List<DiscordMember>();
                List<DiscordRole> MentionedRoles = new List<DiscordRole>();
                List<DiscordChannel> MentionedChannels = new List<DiscordChannel>();
                List<DiscordEmoji> UsedEmojis = new List<DiscordEmoji>();
                if (message.Content != null && message.Content != "" && _guilds.ContainsKey(message.Parent.Parent.ID))
                {
                    foreach (ulong user in Utils.GetUserMentions(message))
                    {
                        if (message.Parent != null && message.Parent.Parent != null && _guilds[message.Parent.Parent.ID] != null
                        && _guilds[message.Parent.Parent.ID].Members != null
                        && _guilds[message.Parent.Parent.ID].Members.Find(x => x.User.ID == user) != null)
                            MentionedUsers.Add(_guilds[message.Parent.Parent.ID].Members.Find(x => x.User.ID == user));
                    }

                    foreach (ulong role in Utils.GetRoleMentions(message))
                    {
                        if (message.Parent != null && message.Parent.Parent != null && _guilds[message.Parent.Parent.ID] != null
                        && _guilds[message.Parent.Parent.ID].Roles != null
                        && _guilds[message.Parent.Parent.ID].Roles.Find(x => x.ID == role) != null)
                            MentionedRoles.Add(_guilds[message.Parent.Parent.ID].Roles.Find(x => x.ID == role));
                    }

                    foreach (ulong channel in Utils.GetChannelMentions(message))
                    {
                        if (message.Parent != null && message.Parent.Parent != null && _guilds[message.Parent.Parent.ID] != null
                        && _guilds[message.Parent.Parent.ID].Channels != null
                        && _guilds[message.Parent.Parent.ID].Channels.Find(x => x.ID == channel) != null)
                            MentionedChannels.Add(_guilds[message.Parent.Parent.ID].Channels.Find(x => x.ID == channel));
                    }
                    /*
                    foreach (ulong emoji in Utils.GetEmojis(message))
                    {
                        if (message.Parent != null && message.Parent.Parent != null && _guilds[message.Parent.Parent.ID] != null
                        && _guilds[message.Parent.Parent.ID].Emojis != null
                        && _guilds[message.Parent.Parent.ID].Emojis.Find(x => x.ID == emoji) != null)
                            UsedEmojis.Add(_guilds[message.Parent.Parent.ID].Emojis.Find(x => x.ID == emoji));
                    }
                    */
                }
                MessageCreateEventArgs args = new MessageCreateEventArgs { Message = message, MentionedUsers = MentionedUsers, MentionedRoles = MentionedRoles, MentionedChannels = MentionedChannels, UsedEmojis = UsedEmojis };
                MessageCreated?.Invoke(this, args);
            });
        }
        internal async Task OnMessageUpdateEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                DiscordMessage message;
                message = obj["d"].ToObject<DiscordMessage>();

                List<DiscordMember> MentionedUsers = new List<DiscordMember>();
                List<DiscordRole> MentionedRoles = new List<DiscordRole>();
                List<DiscordChannel> MentionedChannels = new List<DiscordChannel>();
                List<DiscordEmoji> UsedEmojis = new List<DiscordEmoji>();
                if (message.Content != null && message.Content != "" && _guilds.ContainsKey(message.Parent.Parent.ID))
                {
                    foreach (ulong user in Utils.GetUserMentions(message))
                    {
                        if (message.Parent != null && message.Parent.Parent != null && _guilds[message.Parent.Parent.ID] != null
                        && _guilds[message.Parent.Parent.ID].Members != null
                        && _guilds[message.Parent.Parent.ID].Members.Find(x => x.User.ID == user) != null)
                            MentionedUsers.Add(_guilds[message.Parent.Parent.ID].Members.Find(x => x.User.ID == user));
                    }

                    foreach (ulong role in Utils.GetRoleMentions(message))
                    {
                        if (message.Parent != null && message.Parent.Parent != null && _guilds[message.Parent.Parent.ID] != null
                        && _guilds[message.Parent.Parent.ID].Roles != null
                        && _guilds[message.Parent.Parent.ID].Roles.Find(x => x.ID == role) != null)
                            MentionedRoles.Add(_guilds[message.Parent.Parent.ID].Roles.Find(x => x.ID == role));
                    }

                    foreach (ulong channel in Utils.GetChannelMentions(message))
                    {
                        if (message.Parent != null && message.Parent.Parent != null && _guilds[message.Parent.Parent.ID] != null
                        && _guilds[message.Parent.Parent.ID].Channels != null
                        && _guilds[message.Parent.Parent.ID].Channels.Find(x => x.ID == channel) != null)
                            MentionedChannels.Add(_guilds[message.Parent.Parent.ID].Channels.Find(x => x.ID == channel));
                    }
                    /*
                    foreach (ulong emoji in Utils.GetEmojis(message))
                    {
                        if (message.Parent != null && message.Parent.Parent != null && _guilds[message.Parent.Parent.ID] != null
                        && _guilds[message.Parent.Parent.ID].Emojis != null
                        && _guilds[message.Parent.Parent.ID].Emojis.Find(x => x.ID == emoji) != null)
                            UsedEmojis.Add(_guilds[message.Parent.Parent.ID].Emojis.Find(x => x.ID == emoji));
                    }
                    */
                }

                MessageUpdateEventArgs args = new MessageUpdateEventArgs { Message = message, MentionedUsers = MentionedUsers, MentionedRoles = MentionedRoles, MentionedChannels = MentionedChannels, UsedEmojis = UsedEmojis };
                MessageUpdate?.Invoke(this, args);
            });
        }
        internal async Task OnMessageDeleteEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                ulong ID = ulong.Parse(obj["d"]["id"].ToString());
                ulong channelID = ulong.Parse(obj["d"]["channel_id"].ToString());
                MessageDeleteEventArgs args = new MessageDeleteEventArgs { ChannelID = channelID, MessageID = ID };
                MessageDelete?.Invoke(this, args);
            });
        }
        internal async Task OnMessageBulkDeleteEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                JArray IDsJson = (JArray)obj["d"]["ids"];
                List<ulong> ids = new List<ulong>();
                foreach (JToken t in IDsJson)
                {
                    ids.Add(ulong.Parse(t.ToString()));
                }
                ulong channelID = ulong.Parse(obj["d"]["channel_id"].ToString());
                MessageBulkDeleteEventArgs args = new MessageBulkDeleteEventArgs { MessageIDs = ids, ChannelID = channelID };
                MessageBulkDelete?.Invoke(this, args);
            });
        }
        internal async Task OnTypingStartEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                ulong channelID = ulong.Parse(obj["d"]["channel_id"].ToString());
                ulong userID = ulong.Parse(obj["d"]["user_id"].ToString());
                TypingStartEventArgs args = new TypingStartEventArgs { ChannelID = channelID, UserID = userID };
                TypingStart?.Invoke(this, args);
            });
        }
        internal async Task OnUserSettingsUpdateEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                DiscordUser user = obj["d"].ToObject<DiscordUser>();
                UserSettingsUpdateEventArgs args = new UserSettingsUpdateEventArgs { User = user };
                UserSettingsUpdate?.Invoke(this, args);
            });
        }
        internal async Task OnUserUpdateEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                DiscordUser user = obj["d"].ToObject<DiscordUser>();
                UserUpdateEventArgs args = new UserUpdateEventArgs { User = user };
                UserUpdate?.Invoke(this, args);
            });
        }
        internal async Task OnVoiceStateUpdateEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                ulong userID = ulong.Parse(obj["d"]["user_id"].ToString());
                string session_id = obj["d"]["session_id"].ToString();
                VoiceStateUpdateEventArgs args = new VoiceStateUpdateEventArgs { UserID = userID, SessionID = session_id };
                VoiceStateUpdate?.Invoke(this, args);
            });
        }
        internal async Task OnVoiceServerUpdateEvent(JObject obj)
        {
            await Task.Run(async () =>
            {
                ulong guildID = ulong.Parse(obj["d"]["guild_id"].ToString());
                string endpoint = obj["d"]["endpoint"].ToString();
                string token = obj["d"]["token"].ToString();

                VoiceServerUpdateEventArgs args = new VoiceServerUpdateEventArgs { GuildID = guildID, Endpoint = endpoint, VoiceToken = token };
                VoiceServerUpdate?.Invoke(this, args);
                await _voiceClient.Init(token, guildID, endpoint);
            });
        }
        internal async Task OnGuildMembersChunkEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                ulong guildID = ulong.Parse(obj["d"]["guild_id"].ToString());
                List<DiscordMember> members = new List<DiscordMember>();
                foreach (JObject mem in (JArray)obj["d"]["members"])
                {
                    members.Add(mem.ToObject<DiscordMember>());
                }
                _guilds[guildID].Members = members;
                _guilds[guildID].MemberCount = members.Count;
                GuildMembersChunkEventArgs args = new GuildMembersChunkEventArgs { GuildID = guildID, Members = members };
                GuildMembersChunk?.Invoke(this, args);
            });
        }

        internal async Task OnUnknownEvent(JObject obj)
        {
            await Task.Run(() =>
            {
                string name = obj["t"].ToString();
                string json = obj["d"].ToString();
                UnknownEventArgs args = new UnknownEventArgs { EventName = name, Json = json };
                UnknownEvent?.Invoke(this, args);
            });
        }

        internal async Task OnMessageReactionAdd(JObject obj)
        {
            await Task.Run(() =>
            {
                ulong channelid = ulong.Parse(obj["d"]["channel_id"].ToString());
                ulong messageid = ulong.Parse(obj["d"]["message_id"].ToString());
                ulong userid = ulong.Parse(obj["d"]["user_id"].ToString());
                DiscordEmoji emoji = obj["d"]["emoji"].ToObject<DiscordEmoji>();
                MessageReactionAddEventArgs args = new MessageReactionAddEventArgs
                {
                    ChannelID = channelid,
                    MessageID = messageid,
                    UserID = userid,
                    Emoji = emoji
                };
                MessageReactionAdd?.Invoke(this, args);
            });
        }

        internal async Task OnMessageReactionRemove(JObject obj)
        {
            await Task.Run(() =>
            {
                ulong channelid = ulong.Parse(obj["d"]["channel_id"].ToString());
                ulong messageid = ulong.Parse(obj["d"]["message_id"].ToString());
                ulong userid = ulong.Parse(obj["d"]["user_id"].ToString());
                DiscordEmoji emoji = obj["d"]["emoji"].ToObject<DiscordEmoji>();
                MessageReactionRemoveEventArgs args = new MessageReactionRemoveEventArgs
                {
                    ChannelID = channelid,
                    MessageID = messageid,
                    UserID = userid,
                    Emoji = emoji
                };
                MessageReactionRemove?.Invoke(this, args);
            });
        }

        internal async Task OnMessageReactionRemoveAll(JObject obj)
        {
            await Task.Run(() =>
            {
                ulong channelid = ulong.Parse(obj["d"]["channel_id"].ToString());
                ulong messageid = ulong.Parse(obj["d"]["message_id"].ToString());
                MessageReactionRemoveAllEventArgs args = new MessageReactionRemoveAllEventArgs
                {
                    ChannelID = channelid,
                    MessageID = messageid
                };
                MessageReactionRemoveAll?.Invoke(this, args);
            });
        }

        internal async Task OnWebhooksUpdate(JObject obj)
        {
            await Task.Run(() =>
            {
                ulong channelid = ulong.Parse(obj["d"]["channel_id"].ToString());
                ulong guildid = ulong.Parse(obj["d"]["guild_id"].ToString());
                WebhooksUpdateEventArgs args = new WebhooksUpdateEventArgs
                {
                    ChannelID = channelid,
                    GuildID = guildid
                };
                WebhooksUpdate?.Invoke(this, args);
            });
        }
        #endregion

        internal async Task OnHeartbeat()
        {
            await Task.Run(() =>
            {
                _debugLogger.LogMessage(LogLevel.Debug, "Websocket", "Received Heartbeat - Sending Ack.", DateTime.Now);

                JObject obj = new JObject()
                {
                    { "op", 11 }
                };
            });
        }

        internal async Task OnReconnect()
        {
            await Task.Run(() =>
            {
                _debugLogger.LogMessage(LogLevel.Info, "Websocket", "Received OP 7 - Reconnect. ", DateTime.Now);

                _websocketClient.Disconnect();
                _websocketClient.Connect();
            });
        }

        internal async Task OnInvalidateSession(JObject obj, int shard)
        {
            if (obj.Value<bool>("d"))
            {
                _debugLogger.LogMessage(LogLevel.Debug, "Websocket", "Received true in OP 9 - Waiting a few second and sending resume again.", DateTime.Now);
                await Task.Delay(5000);
                await SendResume();
            }
            else
            {
                _debugLogger.LogMessage(LogLevel.Debug, "Websocket", "Recieved false in OP 9 - Starting a new session", DateTime.Now);
                _sessionID = "";
                await SendIdentify(shard);
            }
        }

        internal async Task OnHello(JObject obj)
        {
            await Task.Run(() =>
            {
                _waitingForAck = false;
                _heartbeatInterval = obj["d"].Value<int>("heartbeat_interval");
                _heartbeatThread = new Thread(StartHeartbeating);
                _heartbeatThread.Start();
            });
        }

        internal async Task OnHeartbeatAck()
        {
            await Task.Run(() =>
            {
                _waitingForAck = false;

                _debugLogger.LogMessage(LogLevel.Unnecessary, "Websocket", "Received WebSocket Heartbeat Ack", DateTime.Now);
                _debugLogger.LogMessage(LogLevel.Debug, "Websocket", $"Ping {(DateTime.Now - _lastHeartbeat).Milliseconds}ms", DateTime.Now);
            });
        }

        internal async void StartHeartbeating()
        {
            _debugLogger.LogMessage(LogLevel.Unnecessary, "Websocket", "Starting Heartbeat", DateTime.Now);
            while (!_cancelToken.IsCancellationRequested)
            {
                await SendHeartbeat();
                Thread.Sleep(_heartbeatInterval);
            }
        }

        internal static void InternalUpdateStatus(string game = "", int idle_since = -1)
        {
            JObject update = new JObject();
            if (idle_since > -1)
                update.Add("idle_since", idle_since);
            else
                update.Add("idle_since", null);

            update.Add("game", game != "" ? new JObject { { "name", game } } : null);

            JObject obj = new JObject
            {
                { "op", 3 },
                { "d", update }
            };

            _websocketClient._socket.Send(obj.ToString());
        }

        internal async Task SendHeartbeat()
        {
            if (_waitingForAck)
            {
                _debugLogger.LogMessage(LogLevel.Critical, "Websocket", "Missed a heartbeat ack. Reconnecting.", DateTime.Now);
                await Reconnect();
            }

            _debugLogger.LogMessage(LogLevel.Unnecessary, "Websocket", "Sending Heartbeat", DateTime.Now);
            JObject obj = new JObject
            {
                { "op", 1 },
                { "d", _sequence }
            };
            _websocketClient._socket.Send(obj.ToString());
            _lastHeartbeat = DateTime.Now;
            _waitingForAck = true;
        }

        internal async Task SendIdentify(int shard)
        {
            await Task.Run(() =>
            {
                JObject obj = new JObject
                {
                    { "op", 2 },
                    { "d", new JObject
                        {
                            { "token", Utils.GetFormattedToken() },
                            { "properties", new JObject
                            {
                                { "$os", "linux" },
                                { "$browser", "DSharpPlus 1.0" },
                                { "$device", "DSharpPlus 1.0" },
                                { "$referrer", "" },
                                { "$referring_domain", "" }
                            } },
                            { "compress", false },
                            { "large_threshold" , config.LargeThreshold },
                            { "shards", new JArray { shard, _shardCount } }
                        }
                    }
                };
                _websocketClient._socket.Send(obj.ToString());
            });
        }

        internal async Task SendResume()
        {
            await Task.Run(() =>
            {
                JObject obj = new JObject
                {
                    { "op", 6 },
                    { "d", new JObject
                        {
                            { "token", _sessionToken },
                            { "session_id", _sessionID },
                            { "seq", _sequence }
                        }
                    }
                };
                _websocketClient._socket.Send(obj.ToString());
            });
        }

        internal static async Task SendVoiceStateUpdate(DiscordChannel channel, bool mute = false, bool deaf = false)
        {
            await Task.Run(() =>
            {
                JObject obj = new JObject
                {
                    { "op", 4 },
                    { "d", new JObject
                        {
                            { "guild_id", channel.Parent.ID },
                            { "channel_id", channel?.ID },
                            { "self_mute", mute },
                            { "self_deaf", deaf }
                        }
                    }
                };
                _websocketClient._socket.Send(obj.ToString());
            });
        }
        #endregion

        #region Voice
        internal static async Task OpenVoiceConnection(DiscordChannel channel, bool mute = false, bool deaf = false)
        {
            await SendVoiceStateUpdate(channel, mute, deaf);
        }
        #endregion

        internal static ulong GetGuildIdFromChannelID(ulong channelid)
        {
            foreach (DiscordGuild guild in _guilds.Values)
            {
                if (guild.Channels.Find(x => x.ID == channelid) != null) return guild.ID;
            }
            return 0;
        }

        internal static int GetChannelIndex(ulong channelid)
        {
            foreach (DiscordGuild guild in _guilds.Values)
            {
                if (guild.Channels.Find(x => x.ID == channelid) != null) return guild.Channels.FindIndex(x => x.ID == channelid);
            }
            return 0;
        }

        internal static DiscordUser InternalGetCachedUser(ulong userid)
        {
            foreach (DiscordGuild guild in _guilds.Values)
            {
                if (guild.Members.Find(x => x.User.ID == userid) != null) return guild.Members.Find(x => x.User.ID == userid).User;
            }
            return new DiscordUser()
            {
                ID = userid
            };
        }

        internal static DiscordPresence InternalGetUserPresence(ulong userid)
        {
            if (_presences.ContainsKey(userid))
                return _presences[userid];

            return new DiscordPresence()
            {
                InternalUser = new DiscordUser()
                {
                    ID = userid
                },
                Status = "offline",
                InternalGame = null
            };
        }

        #region HTTP Actions
        #region Guild
        //
        internal static async Task<DiscordGuild> InternalCreateGuildAsync(string name)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds;
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add("Authorization", Utils.GetFormattedToken());
            JObject payload = new JObject { { "name", name } };

            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, headers, payload.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);

            DiscordGuild guild = JsonConvert.DeserializeObject<DiscordGuild>(response.Response);
            return guild;
        }


        internal static async void InternalDeleteGuildAsync(ulong id)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + $"/{id}";
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add("Authorization", Utils.GetFormattedToken());

            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task<DiscordGuild> InternalModifyGuild(ulong GuildID, string name = "", string region = "", int verification_level = -1, int default_message_notifications = -1,
            ulong akfchannelid = 0, int afktimeout = -1, string icon = "", ulong ownerID = 0, string splash = "")
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID;
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add("Authorization", Utils.GetFormattedToken());
            JObject j = new JObject();
            if (name != "")
                j.Add("name", name);
            if (region != "")
                j.Add("region", region);
            if (verification_level != -1)
                j.Add("verification_level", verification_level);
            if (default_message_notifications != -1)
                j.Add("default_message_notifications", default_message_notifications);
            if (akfchannelid != 0)
                j.Add("akf_channel_id", akfchannelid);
            if (afktimeout != -1)
                j.Add("akf_timeout", afktimeout);
            if (icon != "")
                j.Add("icon", icon);
            if (ownerID != 0)
                j.Add("owner_id", ownerID);
            if (splash != "")
                j.Add("splash", splash);

            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.PATCH, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordGuild>(response.Response);
        }

        internal static async Task<List<DiscordMember>> InternalGetGuildBans(ulong GuildID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Bans;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            JArray j = JArray.Parse(response.Response);
            List<DiscordMember> bans = new List<DiscordMember>();
            foreach (JObject obj in j)
            {
                bans.Add(obj.ToObject<DiscordMember>());
            }
            return bans;
        }

        internal static async Task InternalCreateGuildBan(ulong GuildID, ulong UserID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Bans + "/" + UserID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.PUT, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task InternalRemoveGuildBan(ulong GuildID, ulong UserID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Bans + "/" + UserID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task InternalLeaveGuild(ulong GuildID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Users + "/@me" + Endpoints.Guilds + "/" + GuildID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task<DiscordGuild> InternalCreateGuild(string name, string region, string icon, int verification_level, int default_message_notifications)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            j.Add("name", name);
            j.Add("region", region);
            j.Add("icon", icon);
            j.Add("verification_level", verification_level);
            j.Add("default_message_notifications", default_message_notifications);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordGuild>(response.Response);
        }

        internal static async Task<DiscordMember> InternalAddGuildMember(ulong GuildID, ulong UserID, string AccessToken, string nick = "", List<DiscordRole> roles = null,
        bool muted = false, bool deafened = false)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Members + "/" + UserID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            j.Add("access_token", AccessToken);
            if (nick != "")
                j.Add("nick", nick);
            if (roles != null)
            {
                JArray r = new JArray();
                foreach (DiscordRole role in roles)
                {
                    r.Add(JsonConvert.SerializeObject(role));
                }
                j.Add("roles", r);
            }
            if (muted)
                j.Add("mute", true);
            if (deafened)
                j.Add("deaf", true);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.PUT, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordMember>(response.Response);
        }

        internal static async Task<List<DiscordMember>> InternalListGuildMembers(ulong GuildID, int limit, int after)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Members + $"?limit={limit}&after={after}";
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();

            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            JArray ja = JArray.Parse(response.Response);
            List<DiscordMember> members = new List<DiscordMember>();
            foreach (JObject m in ja)
            {
                members.Add(m.ToObject<DiscordMember>());
            }
            return members;
        }
        #endregion
        #region Channel

        internal static async Task<DiscordChannel> InternalCreateGuildChannelAsync(ulong id, string name, ChannelType type)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + $"/{id}" + Endpoints.Channels;
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add("Authorization", Utils.GetFormattedToken());
            JObject payload = new JObject { { "name", name }, { "type", type.ToString() }, { "permission_overwrites", null } };

            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, headers, payload.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);

            return JsonConvert.DeserializeObject<DiscordChannel>(response.Response);
        }

        internal static async Task<DiscordChannel> InternalGetChannel(ulong ID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordChannel>(response.Response);
        }

        internal static async Task InternalDeleteChannel(ulong ID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ID;
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add("Authorization", Utils.GetFormattedToken());
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task<DiscordMessage> InternalGetMessage(ulong ChannelID, ulong MessageID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Messages + "/" + MessageID;
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add("Authorization", Utils.GetFormattedToken());
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordMessage>(response.Response);
        }

        internal static async Task<DiscordMessage> InternalCreateMessage(ulong ChannelID, string content, bool tts, DiscordEmbed embed = null)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Messages;
            JObject j = new JObject();
            j.Add("content", content);
            j.Add("tts", tts);
            if (embed != null)
            {
                JObject jembed = JObject.FromObject(embed);
                if (embed.Timestamp == new DateTime())
                {
                    jembed.Remove("timestamp");
                }
                else
                {
                    jembed["timestamp"] = embed.Timestamp.ToUniversalTime().ToString("s", CultureInfo.InvariantCulture);
                }
                j.Add("embed", jembed);
            }
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add("Authorization", Utils.GetFormattedToken());
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordMessage>(response.Response);
        }

        internal static async Task<DiscordMessage> InternalUploadFile(ulong ChannelID, string path, string filename, string content = "", bool tts = false)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Messages;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            NameValueCollection values = new NameValueCollection();
            if (content != "")
                values.Add("content", content);
            if (tts)
                values.Add("tts", tts.ToString());
            WebRequest request = await WebRequest.CreateMultipartRequestAsync(url, WebRequestMethod.POST, headers, values, path, filename);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordMessage>(response.Response);
        }

        internal static async Task<List<DiscordChannel>> InternalGetGuildChannels(ulong GuildID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Channels;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            JArray j = JArray.Parse(response.Response);
            List<DiscordChannel> channels = new List<DiscordChannel>();
            foreach (JObject jj in j)
            {
                channels.Add(JsonConvert.DeserializeObject<DiscordChannel>(jj.ToString()));
            }
            return channels;
        }

        internal static async Task<DiscordChannel> InternalCreateChannel(ulong GuildID, string name, ChannelType type, int bitrate = 0, int userlimit = 0)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Channels;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            j.Add("name", name);
            j.Add("type", type == ChannelType.Text ? "text" : "voice");

            if (type == ChannelType.Voice)
            {
                j.Add("bitrate", bitrate);
                j.Add("userlimit", userlimit);
            }
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordChannel>(j.ToString());
        }

        // TODO
        internal static async Task InternalModifyGuildChannelPosition(ulong GuildID, ulong ChannelID, int position)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Channels;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            j.Add("id", ChannelID);
            j.Add("position", position);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.PATCH, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task<List<DiscordMessage>> InternalGetChannelMessages(ulong ChannelID, ulong around = 0, ulong before = 0, ulong after = 0, int limit = -1)
        {
            // ONLY ONE OUT OF around, before or after MAY BE USED.
            // THESE ARE MESSAGE ID's

            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Messages;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            if (around != 0)
                j.Add("around", around);
            if (before != 0)
                j.Add("before", before);
            if (after != 0)
                j.Add("after", after);
            if (limit > -1)
                j.Add("limit", limit);

            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            JArray ja = JArray.Parse(response.Response);
            List<DiscordMessage> messages = new List<DiscordMessage>();
            foreach (JObject jo in ja)
            {
                messages.Add(jo.ToObject<DiscordMessage>());
            }
            return messages;
        }

        internal static async Task<DiscordMessage> InternalGetChannelMessage(ulong ChannelID, ulong MessageID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Messages + "/" + MessageID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordMessage>(response.Response);
        }

        internal static async Task<DiscordMessage> InternalEditMessage(ulong ChannelID, ulong MessageID, string content)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Messages + "/" + MessageID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            j.Add("content", content);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.PATCH, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordMessage>(response.Response);
        }

        internal static async Task InternalDeleteMessage(ulong ChannelID, ulong MessageID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Messages + "/" + MessageID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task InternalBulkDeleteMessages(ulong ChannelID, List<ulong> MessageIDs)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Messages + Endpoints.BulkDelete;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            JArray msgs = new JArray();
            foreach (ulong messageID in MessageIDs)
            {
                msgs.Add(messageID);
            }
            j.Add("messages", msgs);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task<List<DiscordInvite>> InternalGetChannelInvites(ulong ChannelID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Invites;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            JArray ja = JArray.Parse(response.Response);
            List<DiscordInvite> invites = new List<DiscordInvite>();
            foreach (JObject jo in ja)
            {
                invites.Add(JsonConvert.DeserializeObject<DiscordInvite>(jo.ToString()));
            }
            return invites;
        }

        internal static async Task<DiscordInvite> InternalCreateChannelInvite(ulong ChannelID, int max_age = 86400, int max_uses = 0, bool temporary = false, bool unique = false)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Invites;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            j.Add("max_age", max_age);
            j.Add("max_uses", max_uses);
            j.Add("temporary", temporary);
            j.Add("unique", unique);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordInvite>(response.Response);
        }

        internal static async Task InternalDeleteChannelPermission(ulong ChannelID, ulong OverwriteID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Permissions + "/" + OverwriteID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task InternalTriggerTypingIndicator(ulong ChannelID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Typing;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task<List<DiscordMessage>> InternalGetPinnedMessages(ulong ChannelID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Pins;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            JArray j = JArray.Parse(response.Response);
            List<DiscordMessage> messages = new List<DiscordMessage>();
            foreach (JObject obj in j)
            {
                messages.Add(JsonConvert.DeserializeObject<DiscordMessage>(obj.ToString()));
            }
            return messages;
        }

        internal static async Task InternalAddPinnedChannelMessage(ulong ChannelID, ulong MessageID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Pins + "/" + MessageID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.PUT, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task InternalDeletePinnedChannelMessage(ulong ChannelID, ulong MessageID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Pins + "/" + MessageID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task InternalGroupDMAddRecipient(ulong ChannelID, ulong UserID, string AccessToken)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Recipients + "/" + UserID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            j.Add("access_token", AccessToken);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.PUT, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task InternalGroupDMRemoveRecipient(ulong ChannelID, ulong UserID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Recipients + "/" + UserID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }


        internal static async Task InternalEditChannelPermissions(ulong ChannelID, ulong OverwriteID, int allow, int deny, string type)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Permissions + "/" + OverwriteID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            j.Add("allow", allow);
            j.Add("deny", deny);
            j.Add("type", type);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.PUT, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task<DiscordDMChannel> InternalCreateDM(ulong RecipientID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Users + "/@me" + Endpoints.Channels;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            j.Add("recipient_id", RecipientID);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordDMChannel>(response.Response);
        }

        internal static async Task<DiscordDMChannel> InternalCreateGroupDM(List<string> access_tokens)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Users + "/@me" + Endpoints.Channels;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JArray tokens = new JArray();
            foreach (string token in access_tokens)
            {
                tokens.Add(token);
            }
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, headers, tokens.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordDMChannel>(response.Response);
        }
        #endregion
        #region Member
        internal static async Task<List<DiscordMember>> InternalGetGuildMembers(ulong guild_id, int member_count)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + guild_id + Endpoints.Members;
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add("Authorization", Utils.GetFormattedToken());

            List<DiscordMember> result = new List<DiscordMember>();
            int pages = (int)Math.Ceiling((double)member_count / 1000);
            ulong lastId = 0;

            for (int i = 0; i < pages; i++)
            {
                WebRequest request = await WebRequest.CreateRequestAsync($"{url}?limit=1000&after={lastId}", WebRequestMethod.GET, headers);
                WebResponse response = await WebWrapper.HandleRequestAsync(request);

                List<DiscordMember> items = JsonConvert.DeserializeObject<List<DiscordMember>>(response.Response);
                result.AddRange(items);
                lastId = items[items.Count - 1].User.ID;
            }
            return result;
        }

        internal static async Task<DiscordUser> InternalGetUser(string user)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Users + $"/{user}";
            WebHeaderCollection headers = Utils.GetBaseHeaders();

            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);

            return JsonConvert.DeserializeObject<DiscordUser>(response.Response);
        }

        internal static async Task<DiscordMember> InternalGetGuildMember(ulong GuildID, ulong MemberID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Members + "/" + MemberID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordMember>(response.Response);
        }

        internal static async Task InternalRemoveGuildMember(ulong GuildID, ulong UserID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Members + "/" + UserID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task<DiscordUser> InternalGetCurrentUser()
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Users + "/@me";
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordUser>(response.Response);
        }

        internal static async Task<DiscordUser> InternalGetUser(ulong UserID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Users + "/" + UserID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordUser>(response.Response);
        }

        internal static async Task<DiscordUser> InternalModifyCurrentUser(string username = "", string base64avatar = "")
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Users + "/@me";
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            if (username != "")
                j.Add("", username);
            if (base64avatar != "")
                j.Add("avatar", base64avatar);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.PATCH, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordUser>(response.Response);
        }

        internal static async Task<List<DiscordGuild>> InternalGetCurrentUserGuilds()
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Users + "/@me" + Endpoints.Guilds;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            List<DiscordGuild> guilds = new List<DiscordGuild>();
            foreach (JObject j in JArray.Parse(response.Response))
            {
                guilds.Add(JsonConvert.DeserializeObject<DiscordGuild>(j.ToString()));
            }
            return guilds;
        }

        internal static async Task InternalModifyGuildMember(ulong GuildID, ulong UserID, string nick = null,
            List<ulong> roles = null, bool muted = false, bool deafened = false, ulong VoiceChannelID = 0)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Members + "/" + UserID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            if (nick != null)
                j.Add("nick", nick);
            if (roles != null)
            {
                JArray r = new JArray();
                foreach (ulong role in roles)
                {
                    r.Add(role);
                }
                j.Add("roles", r);
            }
            if (muted)
                j.Add("mute", true);
            if (deafened)
                j.Add("deaf", true);
            if (VoiceChannelID != 0)
                j.Add("channel_id", VoiceChannelID);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.PATCH, headers, j.ToString());
            await WebWrapper.HandleRequestAsync(request);
        }

        #endregion
        #region Roles
        // TODO
        internal static List<DiscordRole> InternalGetGuildRoles(ulong guild_id)
        {
            return new List<DiscordRole>();
        }

        // TODO
        internal static List<DiscordRole> InternalModifyGuildRolePosition(ulong guild_id, ulong id, int position)
        {
            return new List<DiscordRole>();
        }

        internal static async Task<DiscordGuild> InternalGetGuild(ulong GuildID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            DiscordGuild guild = JsonConvert.DeserializeObject<DiscordGuild>(response.Response);
            if (_guilds.ContainsKey(GuildID))
            {
                _guilds[GuildID] = guild;
            }
            else
            {
                _guilds.Add(guild.ID, guild);
            }
            return guild;
        }

        internal static async Task<DiscordGuild> InternalModifyGuild(string name = "", string region = "", string icon = "", int verification_level = -1,
            int default_message_notifications = -1, ulong afk_channel_id = 0, int afk_timeout = -1, ulong owner_id = 0, string splash = "")
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            if (name != "")
                j.Add("name", name);
            if (region != "")
                j.Add("region", region);
            if (icon != "")
                j.Add("icon", icon);
            if (verification_level > -1)
                j.Add("verification_level", verification_level);
            if (default_message_notifications > -1)
                j.Add("default_message_notifications", default_message_notifications);
            if (afk_channel_id > 0)
                j.Add("afk_channel_id", afk_channel_id);
            if (afk_timeout > -1)
                j.Add("afk_timeout", afk_timeout);
            if (owner_id > 0)
                j.Add("owner_id", owner_id);
            if (splash != "")
                j.Add("splash", splash);

            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.PATCH, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordGuild>(response.Response);
        }

        internal static async Task<DiscordGuild> InternalDeleteGuild(ulong GuildID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordGuild>(response.Response);
        }

        internal static async Task<DiscordRole> InternalModifyGuildRole(ulong GuildID, ulong RoleID, string name, int permissions, int position, int color, bool separate, bool mentionable)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Roles + RoleID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            j.Add("name", name);
            j.Add("permissions", permissions);
            j.Add("position", position);
            j.Add("color", color);
            j.Add("hoist", separate);
            j.Add("mentionable", mentionable);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.PATCH, headers, j.ToString());
            WebResponse response = await request.HandleRequestAsync();
            return JsonConvert.DeserializeObject<DiscordRole>(response.Response);
        }

        internal static async Task<DiscordRole> InternalDeleteRole(ulong GuildID, ulong RoleID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Roles + "/" + RoleID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordRole>(response.Response);
        }

        internal static async Task<DiscordRole> InternalCreateGuildRole(ulong GuildID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordRole>(response.Response);
        }

        #endregion
        #region Prune
        // TODO
        internal static async Task<int> InternalGetGuildPruneCount(ulong GuildID, int days)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Prune;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject payload = new JObject();
            payload.Add("days", days);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers, payload.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            JObject j = JObject.Parse(response.Response);
            return int.Parse(j["pruned"].ToString());
        }

        // TODO
        internal static async Task<int> InternalBeginGuildPrune(ulong GuildID, int days)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Prune;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject payload = new JObject();
            payload.Add("days", days);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, headers, payload.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            JObject j = JObject.Parse(response.Response);
            return int.Parse(j["pruned"].ToString());
        }
        #endregion
        #region GuildVarious

        internal static async Task<List<DiscordIntegration>> InternalGetGuildIntegrations(ulong GuildID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Integrations;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await request.HandleRequestAsync();
            JArray j = JArray.Parse(response.Response);
            List<DiscordIntegration> integrations = new List<DiscordIntegration>();
            foreach (JObject obj in j)
            {
                integrations.Add(JsonConvert.DeserializeObject<DiscordIntegration>(obj.ToString()));
            }
            return integrations;
        }

        internal static async Task<DiscordIntegration> InternalCreateGuildIntegration(ulong GuildID, string type, ulong ID)
        {
            // Attach from user
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Integrations;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            j.Add("type", type);
            j.Add("id", ID);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, headers, j.ToString());
            WebResponse response = await request.HandleRequestAsync();
            return JsonConvert.DeserializeObject<DiscordIntegration>(response.Response);
        }

        internal static async Task<DiscordIntegration> InternalModifyGuildIntegration(ulong GuildID, ulong IntegrationID, int expire_behaviour,
            int expire_grace_period, bool enable_emoticons)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Integrations + "/" + IntegrationID;
            JObject j = new JObject();
            j.Add("expire_behaviour", expire_behaviour);
            j.Add("expire_grace_period", expire_grace_period);
            j.Add("enable_emoticons", enable_emoticons);
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.PATCH, headers, j.ToString());
            WebResponse response = await request.HandleRequestAsync();
            return JsonConvert.DeserializeObject<DiscordIntegration>(response.Response);
        }

        internal static async Task InternalDeleteGuildIntegration(ulong GuildID, DiscordIntegration integration)
        {
            ulong IntegrationID = integration.ID;
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Integrations + "/" + IntegrationID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = JObject.FromObject(integration);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE, headers, j.ToString());
            WebResponse response = await request.HandleRequestAsync();
        }

        internal static async Task InternalSyncGuildIntegration(ulong GuildID, ulong IntegrationID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Integrations + "/" + IntegrationID + Endpoints.Sync;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, headers);
            WebResponse response = await request.HandleRequestAsync();
        }

        internal static async Task<DiscordGuildEmbed> InternalGetGuildEmbed(ulong GuildID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Embed;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await request.HandleRequestAsync();
            return JsonConvert.DeserializeObject<DiscordGuildEmbed>(response.Response);
        }

        internal static async Task<DiscordGuildEmbed> InternalModifyGuildEmbed(ulong GuildID, DiscordGuildEmbed embed)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Embed;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = JObject.FromObject(embed);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.PATCH, headers, j.ToString());
            WebResponse response = await request.HandleRequestAsync();
            return JsonConvert.DeserializeObject<DiscordGuildEmbed>(response.Response);
        }

        internal static async Task<List<DiscordVoiceRegion>> InternalGetGuildVoiceRegions(ulong GuildID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Regions;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await request.HandleRequestAsync();
            JArray j = JArray.Parse(response.Response);
            List<DiscordVoiceRegion> regions = new List<DiscordVoiceRegion>();
            foreach (JObject obj in j)
            {
                regions.Add(JsonConvert.DeserializeObject<DiscordVoiceRegion>(obj.ToString()));
            }
            return regions;
        }

        internal static async Task<List<DiscordInvite>> InternalGetGuildInvites(ulong GuildID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Invites;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await request.HandleRequestAsync();
            JArray j = JArray.Parse(response.Response);
            List<DiscordInvite> invites = new List<DiscordInvite>();
            foreach (JObject obj in j)
            {
                invites.Add(JsonConvert.DeserializeObject<DiscordInvite>(obj.ToString()));
            }
            return invites;
        }

        #endregion
        #region Invite
        internal static async Task<DiscordInvite> InternalGetInvite(string InviteCode)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Invites + "/" + InviteCode;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordInvite>(response.Response);
        }

        internal static async Task<DiscordInvite> InternalDeleteInvite(string InviteCode)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Invites + "/" + InviteCode;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordInvite>(response.Response);
        }

        internal static async Task<DiscordInvite> InternalAcceptInvite(string InviteCode)
        {
            // USER ONLY
            string url = Utils.GetAPIBaseUri() + Endpoints.Invites + "/" + InviteCode;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordInvite>(response.Response);
        }
        #endregion
        #region Connections
        internal static async Task<List<DiscordConnection>> InternalGetUsersConnections()
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Users + "/@me" + Endpoints.Connections;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            List<DiscordConnection> connections = new List<DiscordConnection>();
            foreach (JObject j in JArray.Parse(response.Response))
            {
                connections.Add(JsonConvert.DeserializeObject<DiscordConnection>(j.ToString()));
            }
            return connections;
        }
        #endregion
        #region Voice
        internal static async Task<List<DiscordVoiceRegion>> InternalListVoiceRegions()
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Voice + Endpoints.Regions;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            List<DiscordVoiceRegion> regions = new List<DiscordVoiceRegion>();
            JArray j = JArray.Parse(response.Response);
            foreach (JObject obj in j)
            {
                regions.Add(JsonConvert.DeserializeObject<DiscordVoiceRegion>(obj.ToString()));
            }
            return regions;
        }
        #endregion
        #region Webhooks
        internal static async Task<DiscordWebhook> InternalCreateWebhook(ulong ChannelID, string name, string base64avatar)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Webhooks;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            j.Add("name", name);
            j.Add("avatar", base64avatar);

            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);

            return JsonConvert.DeserializeObject<DiscordWebhook>(response.Response);
        }

        internal static async Task<List<DiscordWebhook>> InternalGetChannelWebhooks(ulong ChannelID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Webhooks;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            List<DiscordWebhook> webhooks = new List<DiscordWebhook>();
            foreach (JObject j in JArray.Parse(response.Response))
            {
                webhooks.Add(JsonConvert.DeserializeObject<DiscordWebhook>(j.ToString()));
            }
            return webhooks;
        }

        internal static async Task<List<DiscordWebhook>> InternalGetGuildWebhooks(ulong GuildID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Guilds + "/" + GuildID + Endpoints.Webhooks;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            List<DiscordWebhook> webhooks = new List<DiscordWebhook>();
            foreach (JObject j in JArray.Parse(response.Response))
            {
                webhooks.Add(JsonConvert.DeserializeObject<DiscordWebhook>(j.ToString()));
            }
            return webhooks;
        }

        internal static async Task<DiscordWebhook> InternalGetWebhook(ulong WebhookID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Webhooks + "/" + WebhookID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordWebhook>(response.Response);
        }

        // Auth header not required
        internal static async Task<DiscordWebhook> InternalGetWebhookWithToken(ulong WebhookID, string WebhookToken)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Webhooks + "/" + WebhookID + "/" + WebhookToken;
            WebRequest request = await WebRequest.CreateRequestAsync(url);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            DiscordWebhook wh = JsonConvert.DeserializeObject<DiscordWebhook>(response.Response);
            wh.Token = WebhookToken;
            wh.ID = WebhookID;
            return wh;
        }

        internal static async Task<DiscordWebhook> InternalModifyWebhook(ulong WebhookID, string name, string base64avatar)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Webhooks + "/" + WebhookID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            JObject j = new JObject();
            j.Add("name", name);
            j.Add("avatar", base64avatar);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.PATCH, headers, j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordWebhook>(response.Response);
        }

        internal static async Task<DiscordWebhook> InternalModifyWebhook(ulong WebhookID, string name, string base64avatar, string WebhookToken)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Webhooks + "/" + WebhookID + "/" + WebhookToken;
            JObject j = new JObject();
            j.Add("name", name);
            j.Add("avatar", base64avatar);
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.PATCH, payload: j.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JsonConvert.DeserializeObject<DiscordWebhook>(response.Response);
        }

        internal static async Task InternalDeleteWebhook(ulong WebhookID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Webhooks + "/" + WebhookID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task InternalDeleteWebhook(ulong WebhookID, string WebhookToken)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Webhooks + "/" + WebhookID + "/" + WebhookToken;
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task InternalExecuteWebhook(ulong WebhookID, string WebhookToken, string content = "", string username = "", string avatar_url = "",
            bool tts = false, List<DiscordEmbed> embeds = null)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Webhooks + "/" + WebhookID + "/" + WebhookToken;
            JObject req = new JObject();
            if (content != "")
                req.Add("content", content);
            if (username != "")
                req.Add("username", username);
            if (avatar_url != "")
                req.Add("avatar_url", avatar_url);
            if (tts)
                req.Add("tts", tts);
            if (embeds != null)
            {
                JArray arr = new JArray();
                foreach (DiscordEmbed e in embeds)
                {
                    arr.Add(JsonConvert.SerializeObject(e));
                }
                req.Add(arr);
            }
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, payload: req.ToString());
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task InternalExecuteWebhookSlack(ulong WebhookID, string WebhookToken, string jsonpayload)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Webhooks + "/" + WebhookID + "/" + WebhookToken + Endpoints.Slack;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, payload: jsonpayload);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task InternalExecuteWebhookGithub(ulong WebhookID, string WebhookToken, string jsonpayload)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Webhooks + "/" + WebhookID + "/" + WebhookToken + Endpoints.Github;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.POST, payload: jsonpayload);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        #endregion
        #region Reactions
        internal static async Task InternalCreateReaction(ulong ChannelID, ulong MessageID, string Emoji)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Messages + "/" + MessageID + Endpoints.Reactions + "/" + Emoji + "/@me";
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.PUT, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task InternalDeleteOwnReaction(ulong ChannelID, ulong MessageID, string Emoji)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Messages + "/" + MessageID + Endpoints.Reactions + "/" + Emoji + "/@me";
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task InternalDeleteUserReaction(ulong ChannelID, ulong MessageID, ulong UserID, string Emoji)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Messages + "/" + MessageID + Endpoints.Reactions + "/" + Emoji + "/" + UserID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }

        internal static async Task<List<DiscordUser>> InternalGetReactions(ulong ChannelID, ulong MessageID, string Emoji)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Messages + "/" + MessageID + Endpoints.Reactions + "/" + Emoji;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            List<DiscordUser> reacters = new List<DiscordUser>();
            foreach (JObject obj in JArray.Parse(response.Response))
            {
                reacters.Add(obj.ToObject<DiscordUser>());
            }
            return reacters;
        }

        internal static async Task InternalDeleteAllReactions(ulong ChannelID, ulong MessageID)
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.Channels + "/" + ChannelID + Endpoints.Messages + "/" + MessageID + Endpoints.Reactions;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.DELETE, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
        }
        #endregion
        #region Misc
        internal static async Task<DiscordApplication> InternalGetApplicationInfo(string ID = "@me")
        {
            string url = Utils.GetAPIBaseUri() + Endpoints.OAuth2 + Endpoints.Applications + "/" + ID;
            WebHeaderCollection headers = Utils.GetBaseHeaders();
            WebRequest request = await WebRequest.CreateRequestAsync(url, WebRequestMethod.GET, headers);
            WebResponse response = await WebWrapper.HandleRequestAsync(request);
            return JObject.Parse(response.Response).ToObject<DiscordApplication>();
        }
        #endregion
        #endregion

        ~DiscordClient()
        {
            Dispose();
        }

        private bool disposed;
        /// <summary>
        /// Disposes your DiscordClient.
        /// </summary>
        public async void Dispose()
        {
            if (disposed)
                return;

            GC.SuppressFinalize(this);

            await Disconnect();

            _cancelTokenSource.Cancel();
            _guilds = null;
            _heartbeatThread.Abort();
            _heartbeatThread = null;
            _me = null;
            _modules = null;
            _privateChannels = null;
            _ssrcDict = null;
            _voiceClient.Dispose();
            _websocketClient.Dispose();

            disposed = true;
        }
    }
}
