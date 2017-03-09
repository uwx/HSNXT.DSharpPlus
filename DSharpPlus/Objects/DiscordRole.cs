﻿using Newtonsoft.Json;

namespace DSharpPlus
{
    /// <summary>
    ///
    /// </summary>
    public class DiscordRole : SnowflakeObject
    {
        /// <summary>
        /// Role name
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// Integer representation of a hexadecimal color code
        /// </summary>
        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public int Color { get; set; }
        /// <summary>
        /// Whether this role is pinned
        /// </summary>
        [JsonProperty("hoist", NullValueHandling = NullValueHandling.Ignore)]
        public bool Hoist { get; set; }
        /// <summary>
        /// Position of this role
        /// </summary>
        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public int Position { get; set; }
        /// <summary>
        /// Permission bit set
        /// </summary>
        [JsonProperty("permissions", NullValueHandling = NullValueHandling.Ignore)]
        public Permission Permissions { get; set; }
        /// <summary>
        /// Whether this role is managed by an integration
        /// </summary>
        [JsonProperty("managed", NullValueHandling = NullValueHandling.Ignore)]
        public bool Managed { get; set; }
        /// <summary>
        /// Whether this role is mentionable
        /// </summary>
        [JsonProperty("mentionable", NullValueHandling = NullValueHandling.Ignore)]
        public bool Mentionable { get; set; }
        /// <summary>
        /// Mentions the role similar to how a client would, if the role is mentionable
        /// </summary>
        public string Mention => Formatter.Mention(this);
        public PermissionLevel CheckPermission(Permission permission)
        {
            if ((Permissions & permission) != 0)
                return PermissionLevel.Allowed;
            return PermissionLevel.Unset;
        }

        public void AddPermission(Permission permission) { Permissions = DiscordClient.InternalAddPermission(Permissions, permission); }

        public void RemovePermission(Permission permission) { Permissions = DiscordClient.InternalRemovePermission(Permissions, permission); }
    }
}
