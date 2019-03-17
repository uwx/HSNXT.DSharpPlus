using System;
using System.Text.RegularExpressions;
using DSharpPlus.CommandsNext.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using M = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;

namespace DSharpPlus.Test
{
    [TestClass]
    public class NetStandard1_1Shims
    {
        [M] public void TimeSpanConverter() => Console.WriteLine(new TimeSpanConverter());
        [M] public void DiscordUserConverter() => Console.WriteLine(new DiscordUserConverter());
        [M] public void DiscordMemberConverter() => Console.WriteLine(new DiscordMemberConverter());
        [M] public void DiscordChannelConverter() => Console.WriteLine(new DiscordChannelConverter());
        [M] public void DiscordRoleConverter() => Console.WriteLine(new DiscordRoleConverter());
        [M] public void DiscordGuildConverter() => Console.WriteLine(new DiscordGuildConverter());
        [M] public void DiscordMessageConverter() => Console.WriteLine(new DiscordMessageConverter());
        [M] public void DiscordEmojiConverter() => Console.WriteLine(new DiscordEmojiConverter());
        [M] public void DiscordColorConverter() => Console.WriteLine(new DiscordColorConverter());
    }
}