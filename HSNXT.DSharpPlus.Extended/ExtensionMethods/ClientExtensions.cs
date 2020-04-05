using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.Extended.ExtensionMethods
{
    /// <summary>
    /// Extensions for <see cref="DiscordClient"/> and <see cref="DiscordShardedClient"/>
    /// </summary>
    public static class ClientExtensions
    {
        /// <summary>
        /// Searches for an user cached in any of the guilds the client is present in.
        /// </summary>
        /// <param name="client">The client to use to search</param>
        /// <param name="name">The username of the user to look for</param>
        /// <param name="discriminator">The discriminator number of the user to loo kfor</param>
        /// <param name="ignoreCase">Whether or not to ignore case when searching, defaults to false</param>
        /// <returns>The user that was found if there was a matching user in one of the guilds shared by the client;
        /// otherwise, <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="name"/> or <paramref name="discriminator"/> are null
        /// </exception>
        public static DiscordUser FindUser(this DiscordClient client, string name, string discriminator, 
            bool ignoreCase = false)
        {
            if (name == null || discriminator == null)
                throw new ArgumentNullException($"{nameof(name)}/{nameof(discriminator)}");
            
            var compare = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            
            return client.Guilds.Values
                .SelectMany(e => e.Members.Values)
                .FirstOrDefault(member => name.Equals(member.Username, compare)
                                          && discriminator.Equals(member.Discriminator, compare));
        }

        /// <summary>
        /// Searches for an user cached in any of the guilds any of the currently initialized shards in the current
        /// client is present in.
        /// </summary>
        /// <param name="client">The client to use to search</param>
        /// <param name="name">The username of the user to look for</param>
        /// <param name="discriminator">The discriminator number of the user to loo kfor</param>
        /// <param name="ignoreCase">Whether or not to ignore case when searching, defaults to false</param>
        /// <returns>The user that was found if there was a matching user in one of the guilds shared by the client;
        /// otherwise, <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="name"/> or <paramref name="discriminator"/> are null
        /// </exception>
        public static DiscordUser FindUser(this DiscordShardedClient client, string name, string discriminator, 
            bool ignoreCase = false)
        {
            if (name == null || discriminator == null)
                throw new ArgumentNullException($"{nameof(name)}/{nameof(discriminator)}");
            
            var compare = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            
            return client.ShardClients.Values
                .SelectMany(e => e.Guilds.Values)
                .SelectMany(e => e.Members.Values)
                .FirstOrDefault(member => name.Equals(member.Username, compare)
                                          && discriminator.Equals(member.Discriminator, compare));
        }

        /// <summary>
        /// Searches for all cached instances of a member in any of the guilds the client is present in.
        /// </summary>
        /// <param name="client">The client to use to search</param>
        /// <param name="name">The username of the user to look for</param>
        /// <param name="discriminator">The discriminator number of the user to loo kfor</param>
        /// <param name="ignoreCase">Whether or not to ignore case when searching, defaults to false</param>
        /// <returns>The matching member instances for the user found in all of the client's guilds, or an empty list if
        /// there were none.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="name"/> or <paramref name="discriminator"/> are null
        /// </exception>
        public static IReadOnlyList<DiscordMember> FindAllMembers(this DiscordClient client,
            string name, string discriminator, bool ignoreCase = false)
        {
            if (name == null || discriminator == null)
                throw new ArgumentNullException($"{nameof(name)}/{nameof(discriminator)}");
            
            var compare = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            ulong? idCached = default;
            
            return client.Guilds.Values
                .SelectMany(e => e.Members.Values)
                .Where(member =>
                {
                    if (idCached.HasValue)
                        return member.Id == idCached.Value;

                    if (!name.Equals(member.Username, compare) || !discriminator.Equals(member.Discriminator, compare))
                        return false;
                    
                    idCached = member.Id;
                    return true;
                })
                .ToArray();
        }

        /// <summary>
        /// Searches for all cached instances of a member in any of the guilds any of the currently initialized shards
        /// in the current client is present in.
        /// </summary>
        /// <param name="client">The client to use to search</param>
        /// <param name="name">The username of the user to look for</param>
        /// <param name="discriminator">The discriminator number of the user to loo kfor</param>
        /// <param name="ignoreCase">Whether or not to ignore case when searching, defaults to false</param>
        /// <returns>The matching member instances for the user found in all of the client's guilds, or an empty list if
        /// there were none.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="name"/> or <paramref name="discriminator"/> are null
        /// </exception>
        public static IReadOnlyList<DiscordMember> FindAllMembers(this DiscordShardedClient client,
            string name, string discriminator, bool ignoreCase = false)
        {
            if (name == null || discriminator == null)
                throw new ArgumentNullException($"{nameof(name)}/{nameof(discriminator)}");
            
            var compare = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            ulong? idCached = default;
            
            return client.ShardClients.Values
                .SelectMany(e => e.Guilds.Values)
                .SelectMany(e => e.Members.Values)
                .Where(member =>
                {
                    if (idCached.HasValue)
                        return member.Id == idCached.Value;

                    if (!name.Equals(member.Username, compare) || !discriminator.Equals(member.Discriminator, compare))
                        return false;
                    
                    idCached = member.Id;
                    return true;
                })
                .ToArray();
        }
    }
}