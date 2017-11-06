using System.Collections.Generic;
using DSharpPlus.Entities;
using Newtonsoft.Json;

namespace DSharpPlus.Net.Abstractions
{   
    internal sealed class RestEmojiCreatePayload
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
        
        [JsonProperty("roles", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<DiscordRole> Roles { get; set; }
    }
}