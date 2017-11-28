using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.Net.WebSocket;

namespace BotTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            MainTask().GetAwaiter().GetResult();
            
        }

        private static async Task MainTask()
        {
            // First we'll want to initialize our DiscordClient..
            var client = new DiscordClient(new DiscordConfiguration()
            {
                AutoReconnect = true, // Whether you want DSharpPlus to automatically reconnect
                LargeThreshold = 250, // Total number of members where the gateway will stop sending offline members in the guild member list
                LogLevel = LogLevel.Debug, // Minimum log level you want to use
                Token = "Mzc0NjkzNzMxNjMyMTUyNTc2.DP8ZIw.KIhCKhhMoYdgJ8Mxcr_IkllmrPY", // Your token
                TokenType = TokenType.Bot, // Your token type. Most likely "Bot"
                UseInternalLogHandler = true, // Whether you want to use the internal log handler
            });

            if (Environment.OSVersion.Platform == PlatformID.Win32NT ||
                Environment.OSVersion.Platform == PlatformID.Win32S ||
                Environment.OSVersion.Platform == PlatformID.Win32Windows)
            {
                client.SetWebSocketClient<WebSocket4NetClient>();
            }

            // Now we'll want to define our events
            client.DebugLogger.LogMessage(LogLevel.Info, "Bot", "Initializing events", DateTime.Now);

            // First off, the MessageCreated event.
            client.DebugLogger.LogMessage(LogLevel.Info, "Bot", "Initializing MessageCreated", DateTime.Now);

            client.MessageCreated += MessageCreated;
            
            await client.ConnectAsync();
            
            client.DebugLogger.LogMessage(LogLevel.Info, "Bot", "Connected", DateTime.Now);

            await Task.Delay(-1);
        }

        private static async Task MessageCreated(MessageCreateEventArgs e)
        {
            if (e.Message.Content.StartsWith("bouphe1"))
            {
                _ = Task.Run(async () =>
                {
                    await Task.Yield();
                    
                    var d = new List<DataPoint>();
                    for (var i = 0; i < 10; i++)
                        d.Add(new DataPoint(i, i));
                    using (var s = new MemoryStream())
                    {
                        GeneratePlot(d, s);
                        s.Position = 0;
                        await e.Channel.SendFileAsync(s, "graph.jpg");
                    }
                    Console.WriteLine("ok");
                });
            }
        }
        
        public static void GeneratePlot(IList<DataPoint> series, Stream outputStream)
        {
            using (var ch = new Chart())
            {
                ch.ChartAreas.Add(new ChartArea());

                var s = new Series {ChartType = SeriesChartType.FastLine};
                foreach (var pnt in series) s.Points.Add(pnt);
                ch.Series.Add(s);
                ch.SaveImage(outputStream, ChartImageFormat.Jpeg);
            }
        }
    }
}