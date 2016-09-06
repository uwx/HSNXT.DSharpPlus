﻿using System;
using SharpCord.Objects;

namespace SharpCord
{
    public class DiscordBanRemovedEventArgs : EventArgs
    {
        public DiscordServer Guild { get; internal set; }
        public DiscordMember MemberStub { get; internal set; }
    }
}