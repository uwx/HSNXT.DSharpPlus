using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using HSNXT.Test.DiscordRPC.Base;
using HSNXT.Test.VLCRPC.Unirest;

namespace HSNXT.Test.VLCRPC
{
    internal class Program
    {
        private static DiscordRpc.EventHandlers _handlers;
        private static DiscordRpc.RichPresence _presence;

        public static async Task Main()
        {
            Console.WriteLine("he1!");
//            new Process
//            {
//                StartInfo =
//                {
//                    FileName = @"C:\Portable Programs\VLC\vlc.exe"
//                }
//            }.Start();
            Console.WriteLine("hleol!");
            await MainAsync();
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