using System;
using System.Linq;
using System.Text.RegularExpressions;
using DSharpPlus;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.Extended.ExtensionMethods
{
    /// <summary>
    /// Extensions for <see cref="DiscordMessage"/>
    /// </summary>
    public static class MessageExtensions
    {
        // welcome to regex hell, kid
        private static readonly Regex MessageFormatMatcher = new Regex(
            @"<(?:(?:@[!&]?|#)|a?:[a-zA-Z0-9_~]+:)[0-9]+>", RegexOptions.Compiled
        );

        /// <summary>
        /// Converts a <see cref="DiscordMessage"/> into a string as an user would see it, that is, with all ID-based
        /// mentions, links and emojis replaced with their long, full-name versions.
        /// </summary>
        /// <param name="message">The message to humanize</param>
        /// <returns>The humanized message</returns>
        /// <exception cref="InvalidOperationException">
        /// If <see cref="DiscordMessage.MentionedUsers"/> does not point to valid members, and there are nickname user
        /// mentions in the message.
        /// </exception>
        /// <remarks>
        /// Your scientists were so preoccupied with whether or not they could, they didn't stop to think if they
        /// should.
        /// </remarks>
        public static string Humanize(this DiscordMessage message)
        {
            string Slice(string str, int from, int to)
                => str.Substring(from, (to == -1 ? str.Length : to) - from);

            return MessageFormatMatcher.Replace(message.Content, match =>
            {
                if (match.Value[1] == '@') // User or Role
                {
                    if (match.Value[2] == '!') // User (Nickname)
                    {
                        var id = ulong.Parse(Slice(match.Value, "<@!".Length, -">".Length));
                        var user = message.MentionedUsers.FirstOrDefault(e => e.Id == id);
                        if (!(user is DiscordMember member))
                        {
                            throw new InvalidOperationException("User should be a Member, let me know");
                        }

                        return $"@{member.Nickname}#{member.Discriminator}";
                    }

                    if (match.Value[2] == '&') // Role
                    {
                        var id = ulong.Parse(Slice(match.Value, "<@&".Length, -">".Length));
                        var role = message.MentionedRoles.FirstOrDefault(e => e.Id == id);
                        return $"@{role.Name}";
                    }
                    
                    // else, User
                    {
                        var id = ulong.Parse(Slice(match.Value, "<@".Length, -">".Length));
                        var user = message.MentionedUsers.FirstOrDefault(e => e.Id == id);
                        return $"@{user.Username}#{user.Discriminator}";
                    }
                }

                if (match.Value[1] == '#') // Channel
                {
                    var id = ulong.Parse(Slice(match.Value, "<#".Length, -">".Length));
                    var channel = message.Channel.Guild.GetChannel(id);
                    return channel.Type == ChannelType.Text
                        ? $"#{channel.Name}"
                        : channel.Name; // cannot usually mention other types of channels, results vary wildly
                }
                
                // else, Emoji or Animated Emoji
                {
                    var id = ulong.Parse(Slice(match.Value, match.Value.LastIndexOf(':') + 1, -">".Length));
                    var emoji = message.Channel.Guild.Emojis[id];
                    return emoji.GetDiscordName();
                }
            });
        }
    }
}