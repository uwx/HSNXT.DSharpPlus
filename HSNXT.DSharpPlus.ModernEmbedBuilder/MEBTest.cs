#if !TEST
using System;
using System.Collections.Generic;
using DSharpPlus.Entities;

namespace DSharpPlus.ModernEmbedBuilder.Test
{
    public class MebTest
    {
        public void Test()
        {
            DiscordEmbed a = new ModernEmbedBuilder
            {
                Footer = "text",
                Title = "title",
                Description = "descy",
                Url = "http://bonerbrew.club/",
                Color = (255, 5, 5),
                Timestamp = DuckTimestamp.Now,
                AuthorName = "author name",
                AuthorUrl = "author url",
                AuthorIcon = "icon url"
            };
            DiscordEmbed b = new ModernEmbedBuilder
            {
                Color = 0xFF0000,
                Timestamp = 50000, // in utc seconds
                Footer = ("text", "icon url"),
                ThumbnailUrl = "http://example.com/ex.png",
                ImageUrl = "http://example.com/bigImage.png",
                Author = ("name", "url", "icon url")
            };
            DiscordEmbed c = new ModernEmbedBuilder
            {
                Color = new DiscordColor("0xgay faggot"),
                FooterIcon = "icon url",
                FooterText = "footer text",
                Fields =
                {
                    "Field with just name",
                    ("ay Name", "ay Value"),
                    ("ay Name", "ay Value", inline: true),
                    // more esoteric
                    new List<string>
                    {
                        "ay Name",
                        "ay Value",
                        "true"
                    },
                    // even more esoteric
                    new Dictionary<string, string>
                    {
                        ["name"] = "ay Name",
                        ["value"] = "ay Value",
                        ["inline"] = "true"
                    }
                }
            };
            
#if DEBUG
            Console.WriteLine($"{a}\n{b}\n{c}");
#endif

            new ModernEmbedBuilder
            {

            }.Send(null);
        }
    }
}
#endif