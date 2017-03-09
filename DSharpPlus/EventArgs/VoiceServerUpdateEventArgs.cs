﻿using System;

namespace DSharpPlus
{
    public class VoiceServerUpdateEventArgs : EventArgs
    {
        internal string VoiceToken { get; set; }
        public ulong GuildID { get; internal set; }
        public DiscordGuild Guild => DiscordClient.InternalGetGuild(GuildID).Result;
        public string Endpoint { get; internal set; }
    }
}
