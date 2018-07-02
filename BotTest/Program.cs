using System;
using System.IO;
using System.Threading.Tasks;
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
            var cfg = new DiscordConfiguration()
            {
                AutoReconnect = true, // Whether you want DSharpPlus to automatically reconnect
                LargeThreshold =
                    250, // Total number of members where the gateway will stop sending offline members in the guild member list
                LogLevel = LogLevel.Debug, // Minimum log level you want to use
                Token = File.ReadAllText("../token.txt").Trim(), // Your token
                TokenType = TokenType.Bot, // Your token type. Most likely "Bot"
                UseInternalLogHandler = true, // Whether you want to use the internal log handler
                WebSocketClientFactory = WebSocket4NetCoreClient.CreateNew
            };
            var client = new DiscordClient(cfg);

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
            try
            {
                if (e.Message.Content.StartsWith("bouphe1"))
                {
                    Console.WriteLine("Channels:" + e.Message.MentionedChannels);
                    Console.WriteLine("Roles:" + e.Message.MentionedRoles);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}