using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HSNXT.Test.DiscordRPC.Base;

namespace HSNXT.Test.DiscordRPC.NotepadPlusPlus
{
    class Program
    {
        private static DiscordRpc.EventHandlers _handlers;
        private static DiscordRpc.RichPresence _presence;
        private static readonly Dictionary<string, long> StartTimes = new Dictionary<string, long>();

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
            _presence = new DiscordRpc.RichPresence
            {
                state = "Idle",
                largeImageKey = "npp_logo",
                largeImageText = "Notepad++",
            };
            
            _handlers = new DiscordRpc.EventHandlers();
            _handlers.readyCallback += ReadyCallback;
            _handlers.disconnectedCallback += DisconnectedCallback;
            _handlers.errorCallback += ErrorCallback;
            _handlers.joinCallback += JoinCallback;
            _handlers.spectateCallback += SpectateCallback;
            _handlers.requestCallback += RequestCallback;
            Console.WriteLine("c");
            DiscordRpc.Initialize("420428166536888320", ref _handlers, true, null);
            Console.WriteLine("b");
            DiscordRpc.RunCallbacks();
            Console.WriteLine("a");
            DiscordRpc.UpdatePresence(ref _presence);
            
            while(true)
            {
                try
                {
                    await Task.Delay(3000);

                    var handle = Process.GetProcessesByName("notepad++")
                                     .Select(e => e.MainWindowTitle)
                                     .FirstOrDefault(e => e.EndsWith("Notepad++")) ?? "null";
                    var re = Regex.Match(handle, @"\*?(.+?) - Notepad\+\+");
                    Console.WriteLine(re + " / " + re.Success);
                    if (!re.Success)
                    {
                        _presence.details = "";
                        _presence.state = "Idle";
                        _presence.startTimestamp = 0;
                        _presence.largeImageKey = "npp_logo";
                        _presence.largeImageText = "Notepad++";
                        
                        DiscordRpc.UpdatePresence(ref _presence);
                        continue;
                    }

                    var group1 = re.Groups[1].Value;
                    var fname = Path.GetFileName(group1);
                    var ext = Path.GetExtension(group1);
                    _presence.details = "Editing:";
                    _presence.state = handle.StartsWith("*") ? fname + " (unsaved)" : fname;
                    _presence.startTimestamp = StartTime(group1);
                    _presence.largeImageKey = "npp_logo";
                    _presence.largeImageText = "Notepad++";
                    

                    DiscordRpc.UpdatePresence(ref _presence);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static long StartTime(string group1)
        {
            return StartTimes.ContainsKey(group1)
                ? StartTimes[group1]
                : (StartTimes[group1] = DateTimeOffset.UtcNow.ToUnixTimeSeconds());
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