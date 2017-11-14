using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using unirest_net.http;

namespace DRPCTest
{
    public class DiscordRpc
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ReadyCallback();
    
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DisconnectedCallback(int errorCode, string message);
    
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ErrorCallback(int errorCode, string message);
    
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void JoinCallback(string secret);
    
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SpectateCallback(string secret);
    
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RequestCallback(JoinRequest request);
    
        [StructLayout(LayoutKind.Sequential)]
        public struct EventHandlers
        {
            public ReadyCallback readyCallback;
            public DisconnectedCallback disconnectedCallback;
            public ErrorCallback errorCallback;
            public JoinCallback joinCallback;
            public SpectateCallback spectateCallback;
            public RequestCallback requestCallback;
        }
    
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct RichPresence
        {
            [MarshalAs(UnmanagedType.LPUTF8Str)] public string state; /* max 128 bytes */
            [MarshalAs(UnmanagedType.LPUTF8Str)] public string details; /* max 128 bytes */
            public long startTimestamp;
            public long endTimestamp;
            public string largeImageKey; /* max 32 bytes */
            [MarshalAs(UnmanagedType.LPUTF8Str)] public string largeImageText; /* max 128 bytes */
            public string smallImageKey; /* max 32 bytes */
            [MarshalAs(UnmanagedType.LPUTF8Str)] public string smallImageText; /* max 128 bytes */
            public string partyId; /* max 128 bytes */
            public int partySize;
            public int partyMax;
            public string matchSecret; /* max 128 bytes */
            public string joinSecret; /* max 128 bytes */
            public string spectateSecret; /* max 128 bytes */
            public bool instance;
        }
    
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct JoinRequest
        {
            public string userId;
            public string username;
            public string avatar;
        }
    
        public enum Reply
        {
            No = 0,
            Yes = 1,
            Ignore = 2
        }
    
        [DllImport("discord-rpc", EntryPoint = "Discord_Initialize", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Initialize(string applicationId, ref EventHandlers handlers, bool autoRegister, string optionalSteamId);
    
        [DllImport("discord-rpc", EntryPoint = "Discord_Shutdown", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Shutdown();
    
        [DllImport("discord-rpc", EntryPoint = "Discord_RunCallbacks", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RunCallbacks();
    
        [DllImport("discord-rpc", EntryPoint = "Discord_UpdatePresence", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UpdatePresence(ref RichPresence presence);
    
        [DllImport("discord-rpc", EntryPoint = "Discord_Respond", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Respond(string userId, Reply reply);
    }

    public class VLCStatus
    {
        [JsonProperty("apiversion")]
        public long Apiversion { get; set; }

        [JsonProperty("audiodelay")]
        public long Audiodelay { get; set; }

        [JsonProperty("audiofilters")]
        public Audiofilters Audiofilters { get; set; }

        [JsonProperty("currentplid")]
        public long Currentplid { get; set; }

        [JsonProperty("equalizer")]
        public object[] Equalizer { get; set; }

        [JsonProperty("fullscreen")]
        public long Fullscreen { get; set; }

        [JsonProperty("information")]
        public Information Information { get; set; }

        [JsonProperty("length")]
        public long Length { get; set; }

        [JsonProperty("loop")]
        public bool Loop { get; set; }

        [JsonProperty("position")]
        public double Position { get; set; }

        [JsonProperty("random")]
        public bool Random { get; set; }

        [JsonProperty("rate")]
        public long Rate { get; set; }

        [JsonProperty("repeat")]
        public bool Repeat { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("stats")]
        public Stats Stats { get; set; }

        [JsonProperty("subtitledelay")]
        public long Subtitledelay { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("videoeffects")]
        public Videoeffects Videoeffects { get; set; }

        [JsonProperty("volume")]
        public long Volume { get; set; }
    }

    public class Videoeffects
    {
        [JsonProperty("brightness")]
        public long Brightness { get; set; }

        [JsonProperty("contrast")]
        public long Contrast { get; set; }

        [JsonProperty("gamma")]
        public long Gamma { get; set; }

        [JsonProperty("hue")]
        public long Hue { get; set; }

        [JsonProperty("saturation")]
        public long Saturation { get; set; }
    }

    public class Stats
    {
        [JsonProperty("averagedemuxbitrate")]
        public long Averagedemuxbitrate { get; set; }

        [JsonProperty("averageinputbitrate")]
        public long Averageinputbitrate { get; set; }

        [JsonProperty("decodedaudio")]
        public long Decodedaudio { get; set; }

        [JsonProperty("decodedvideo")]
        public long Decodedvideo { get; set; }

        [JsonProperty("demuxbitrate")]
        public long Demuxbitrate { get; set; }

        [JsonProperty("demuxcorrupted")]
        public long Demuxcorrupted { get; set; }

        [JsonProperty("demuxdiscontinuity")]
        public long Demuxdiscontinuity { get; set; }

        [JsonProperty("demuxreadbytes")]
        public long Demuxreadbytes { get; set; }

        [JsonProperty("demuxreadpackets")]
        public long Demuxreadpackets { get; set; }

        [JsonProperty("displayedpictures")]
        public long Displayedpictures { get; set; }

        [JsonProperty("inputbitrate")]
        public long Inputbitrate { get; set; }

        [JsonProperty("lostabuffers")]
        public long Lostabuffers { get; set; }

        [JsonProperty("lostpictures")]
        public long Lostpictures { get; set; }

        [JsonProperty("playedabuffers")]
        public long Playedabuffers { get; set; }

        [JsonProperty("readbytes")]
        public long Readbytes { get; set; }

        [JsonProperty("readpackets")]
        public long Readpackets { get; set; }

        [JsonProperty("sendbitrate")]
        public long Sendbitrate { get; set; }

        [JsonProperty("sentbytes")]
        public long Sentbytes { get; set; }

        [JsonProperty("sentpackets")]
        public long Sentpackets { get; set; }
    }

    public class Information
    {
        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("chapter")]
        public long Chapter { get; set; }

        [JsonProperty("chapters")]
        public object[] Chapters { get; set; }

        [JsonProperty("title")]
        public long Title { get; set; }

        [JsonProperty("titles")]
        public object[] Titles { get; set; }
    }

    public class Category
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("Stream 0")]
        public Stream0 Stream0 { get; set; }
    }

    public class Stream0
    {
        [JsonProperty("Bitrate")]
        public string Bitrate { get; set; }

        [JsonProperty("Bits_per_sample")]
        public string BitsPerSample { get; set; }

        [JsonProperty("Channels")]
        public string Channels { get; set; }

        [JsonProperty("Codec")]
        public string Codec { get; set; }

        [JsonProperty("Sample_rate")]
        public string SampleRate { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }
    }

    public class Meta
    {
        [JsonProperty("ALBUM ARTIST")]
        public string AlbumArtist { get; set; }

        [JsonProperty("album")]
        public string Album { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("artwork_url")]
        public string ArtworkUrl { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("genre")]
        public string Genre { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("YEAR")]
        public string Year { get; set; }
    }

    public class Audiofilters
    {
        [JsonProperty("filter_0")]
        public string Filter0 { get; set; }
    }

    internal class Program
    {
        private static DiscordRpc.EventHandlers handlers;
        private static DiscordRpc.RichPresence presence;

        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        public static async Task MainAsync()
        {
            presence = new DiscordRpc.RichPresence()
            {
                //the user's current party status
                state = "loading",
                //what the player is currently doing
                details = "loading",
                //unix timestamp for the start of the game
                //startTimestamp = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond,
                //unix timestamp for when the game will end
                // if not present shows 'elapsed'
                //endTimestamp = (long) ((DateTime.Now.Ticks / (double)TimeSpan.TicksPerMillisecond) + 10 * 60e3),
                //name of the uploaded image for the large profile artwork
                largeImageKey = "bs",
                //tooltip for the largeImageKey
                //largeImageText = "frank tooltip",
                //name of the uploaded image for the small profile artwork
                smallImageKey = "mosic",
                //tootltip for the smallImageKey
                //smallImageText = "moosic",
                ////id of the player's party, lobby, or group
                //partyId = "party1234",
                //current size of the player's party, lobby, or group
                partySize = 1,
                //maximum size of the player's party, lobby, or group
                partyMax = 6,
                ////unique hashed string for Spectate and Join
                //matchSecret = "xyzzy",
                ////unique hased string for chat invitations and Ask to Join
                //joinSecret = "join",
                ////unique hased string for Spectate button
                //spectateSecret = "look",
                ////marks the matchSecret as a game session with a specific beginning and end
                //instance = false,
            };
            
            handlers = new DiscordRpc.EventHandlers();
            handlers.readyCallback += ReadyCallback;
            handlers.disconnectedCallback += DisconnectedCallback;
            handlers.errorCallback += ErrorCallback;
            handlers.joinCallback += JoinCallback;
            handlers.spectateCallback += SpectateCallback;
            handlers.requestCallback += RequestCallback;
            Console.WriteLine("c");
            DiscordRpc.Initialize("374693731632152576", ref handlers, true, null);
            Console.WriteLine("b");
            DiscordRpc.RunCallbacks();
            Console.WriteLine("a");
            DiscordRpc.UpdatePresence(ref presence);
            
            while(true)
            {
                await Task.Delay(3000);

                var status = await new GetRequest("http://127.0.0.1:8080/requests/status.json")
                {
                    Auth = ("", "dumbo"),//vlc username is emtpty
                }.AsJsonAsync<VLCStatus>();

                var playlist = await new GetRequest("http://127.0.0.1:8080/requests/playlist.xml")
                {
                    Auth = ("", "dumbo"), //vlc username is emtpty
                }.AsStringAsync();

                var xml = new XmlDocument();
                xml.LoadXml(playlist.Body);
                var leaves = xml.GetElementsByTagName("leaf");
                var total = leaves.Count;
                var current = 1+leaves.Cast<XmlNode>().ToList()
                    .FindIndex(c => status.Body.Currentplid == int.Parse(c.Attributes?["id"]?.InnerText ?? "-1"));

                var meta = status.Body.Information.Category.Meta;
                presence.state = $"{meta.Artist} - {meta.Title}";

                presence.details = $"{status.Body.State} {status.Body.Time / 60}:{status.Body.Time % 60:00} / {status.Body.Length / 60}:{status.Body.Length % 60:00}";

                presence.partySize = current;
                presence.partyMax = total;

                //presence.startTimestamp =
                //    DateTimeOffset.Now.AddSeconds(-status.Body.Time).ToUnixTimeMilliseconds();
                //presence.endTimestamp =
                //    DateTimeOffset.Now.AddSeconds(status.Body.Length-status.Body.Time).ToUnixTimeMilliseconds();

                Console.WriteLine($"hi!!!{status.Body.Time},{status.Body.Length}");
                Console.WriteLine($"Now:{DateTimeOffset.Now};Then{DateTimeOffset.Now.AddSeconds(-status.Body.Time)};Now-Then{DateTimeOffset.Now - DateTimeOffset.Now.AddSeconds(-status.Body.Time)}");
                Console.WriteLine($"Now:{DateTimeOffset.Now.ToUnixTimeMilliseconds()};Then{DateTimeOffset.Now.AddSeconds(-status.Body.Time).ToUnixTimeMilliseconds()};Now-Then{(DateTimeOffset.Now - DateTimeOffset.Now.AddSeconds(-status.Body.Time)).TotalMilliseconds}");
                DiscordRpc.UpdatePresence(ref presence);
            }
        }

        private static void RequestCallback(DiscordRpc.JoinRequest request)
        {
            throw new NotImplementedException();
        }

        private static void SpectateCallback(string secret)
        {
            throw new NotImplementedException();
        }

        private static void JoinCallback(string secret)
        {
            throw new NotImplementedException();
        }

        private static void ErrorCallback(int errorcode, string message)
        {
            Console.WriteLine($"Errored {errorcode}: {message}");
        }

        private static void DisconnectedCallback(int errorcode, string message)
        {
            Console.WriteLine($"DC'd {errorcode}: {message}");
        }

        private static void ReadyCallback()
        {
            Console.WriteLine($"Ready");
            DiscordRpc.UpdatePresence(ref presence);
        }
    }
}