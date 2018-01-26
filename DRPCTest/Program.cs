using System;
using System.Diagnostics;
using System.IO;
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
        public long ApiVersion { get; set; }

        [JsonProperty("audiodelay")]
        public long AudioDelay { get; set; }

        [JsonProperty("audiofilters")]
        public Audiofilters AudioFilters { get; set; }

        [JsonProperty("currentplid")]
        public long CurrentPlayingId { get; set; }

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
        public long SubtitleDelay { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("videoeffects")]
        public Videoeffects VideoEffects { get; set; }

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
        public long AverageDemuxBitrate { get; set; }

        [JsonProperty("averageinputbitrate")]
        public long AverageInputBitrate { get; set; }

        [JsonProperty("decodedaudio")]
        public long DecodedAudio { get; set; }

        [JsonProperty("decodedvideo")]
        public long DecodedVideo { get; set; }

        [JsonProperty("demuxbitrate")]
        public long DemuxBitrate { get; set; }

        [JsonProperty("demuxcorrupted")]
        public long DemuxCorrupted { get; set; }

        [JsonProperty("demuxdiscontinuity")]
        public long DemuxDiscontinuity { get; set; }

        [JsonProperty("demuxreadbytes")]
        public long DemuxReadBytes { get; set; }

        [JsonProperty("demuxreadpackets")]
        public long DemuxReadPackets { get; set; }

        [JsonProperty("displayedpictures")]
        public long DisplayedPictures { get; set; }

        [JsonProperty("inputbitrate")]
        public long InputBitrate { get; set; }

        [JsonProperty("lostabuffers")]
        public long LostABuffers { get; set; }

        [JsonProperty("lostpictures")]
        public long LostPictures { get; set; }

        [JsonProperty("playedabuffers")]
        public long PlayedABuffers { get; set; }

        [JsonProperty("readbytes")]
        public long ReadBytes { get; set; }

        [JsonProperty("readpackets")]
        public long ReadPackets { get; set; }

        [JsonProperty("sendbitrate")]
        public long SendBitrate { get; set; }

        [JsonProperty("sentbytes")]
        public long SentBytes { get; set; }

        [JsonProperty("sentpackets")]
        public long SentPackets { get; set; }
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
        private static DiscordRpc.EventHandlers _handlers;
        private static DiscordRpc.RichPresence _presence;

        public static void Main()
        {
            new Process
            {
                StartInfo =
                {
                    FileName = @"C:\Portable Programs\VLC\vlc.exe"
                }
            }.Start();
            MainAsync().GetAwaiter().GetResult();
        }

        public static async Task MainAsync()
        {
            _presence = new DiscordRpc.RichPresence()
            {
                //the user's current party status
                state = "Nothing playing",
                //what the player is currently doing
                details = "Idle",
                //unix timestamp for the start of the game
                //startTimestamp = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond,
                //unix timestamp for when the game will end
                // if not present shows 'elapsed'
                //endTimestamp = (long) ((DateTime.Now.Ticks / (double)TimeSpan.TicksPerMillisecond) + 10 * 60e3),
                //name of the uploaded image for the large profile artwork
                largeImageKey = "vlc_big",
                //tooltip for the largeImageKey
                //largeImageText = "frank tooltip",
                //name of the uploaded image for the small profile artwork
                smallImageKey = "sleep",
                //tootltip for the smallImageKey
                //smallImageText = "moosic",
                ////id of the player's party, lobby, or group
                //partyId = "party1234",
                //current size of the player's party, lobby, or group
                //partySize = 1,
                //maximum size of the player's party, lobby, or group
                //partyMax = 6,
                ////unique hashed string for Spectate and Join
                //matchSecret = "xyzzy",
                ////unique hased string for chat invitations and Ask to Join
                //joinSecret = "join",
                ////unique hased string for Spectate button
                //spectateSecret = "look",
                ////marks the matchSecret as a game session with a specific beginning and end
                //instance = false,
            };
            
            _handlers = new DiscordRpc.EventHandlers();
            _handlers.readyCallback += ReadyCallback;
            _handlers.disconnectedCallback += DisconnectedCallback;
            _handlers.errorCallback += ErrorCallback;
            _handlers.joinCallback += JoinCallback;
            _handlers.spectateCallback += SpectateCallback;
            _handlers.requestCallback += RequestCallback;
            Console.WriteLine("c");
            DiscordRpc.Initialize("374693731632152576", ref _handlers, true, null);
            Console.WriteLine("b");
            DiscordRpc.RunCallbacks();
            Console.WriteLine("a");
            DiscordRpc.UpdatePresence(ref _presence);
            
            while(true)
            {
                try
                {
                    await Task.Delay(3000);

                    var status = await new GetRequest("http://127.0.0.1:8080/requests/status.json")
                    {
                        Auth = ("", "dumbo"), //vlc username is emtpty
                    }.AsJsonAsync<VLCStatus>();

                    var playlist = await new GetRequest("http://127.0.0.1:8080/requests/playlist.xml")
                    {
                        Auth = ("", "dumbo"), //vlc username is emtpty
                    }.AsStringAsync();

                    _presence.largeImageText = status.Body?.Version ?? null;
                        
                    if (status.Body?.Information?.Category?.Meta == null || playlist.Body?.Length == 0)
                    {
                        _presence.state = "Nothing playing";
                        _presence.details = "Idle";
                        _presence.smallImageKey = "stopped";
                        _presence.smallImageText = null;
                        _presence.partySize = 0;
                        _presence.partyMax = 0;
                        _presence.startTimestamp = 0;
                        _presence.endTimestamp = 0;
                        DiscordRpc.UpdatePresence(ref _presence);
                        Console.WriteLine("nulled");
                        continue;
                    }

                    var xml = new XmlDocument();
                    xml.LoadXml(playlist.Body);
                    var leaves = xml.GetElementsByTagName("leaf");
                    var total = leaves.Count;
                    var current = 1 + leaves.Cast<XmlNode>().ToList()
                                      .FindIndex(c =>
                                          status.Body.CurrentPlayingId ==
                                          int.Parse(c.Attributes?["id"]?.InnerText ?? "-1"));

                    var meta = status.Body.Information.Category.Meta;

                    // playing/paused <title>
                    _presence.details =
                        $"{meta.Title}";
                    
                    // by <artist> (or just file name if other fields are not present)
                    _presence.state = !string.IsNullOrEmpty(meta.Artist) 
                        ? $"by {meta.Artist}" 
                        : Path.GetFileNameWithoutExtension($"{meta.Filename}");

                    _presence.partySize = current;
                    _presence.partyMax = total;

                    _presence.smallImageKey = 
                        status.Body.State == "playing" ? "play" : 
                        status.Body.State == "paused" ? "pause" : 
                        status.Body.State == "stopped" ? "stop" : "mosic";
                    
                    _presence.smallImageText = "Album: " + meta.Album + (meta.Year != null ? (" (" + meta.Year + ")") : "");

                    if (status.Body.State == "playing")
                    {
                        _presence.startTimestamp =
                            DateTimeOffset.UtcNow.AddSeconds(-status.Body.Time).ToUnixTimeSeconds();
                        _presence.endTimestamp =
                            DateTimeOffset.UtcNow.AddSeconds(status.Body.Length - status.Body.Time).ToUnixTimeSeconds();
                    }

                    Console.WriteLine($"hi!!!{status.Body.Time},{status.Body.Length}");
                    DiscordRpc.UpdatePresence(ref _presence);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
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
            DiscordRpc.UpdatePresence(ref _presence);
        }
    }
}