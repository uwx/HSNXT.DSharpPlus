#if DEBUG
using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.CNextNotifier
{
    public class ExampleCommands : BaseCommandModule
    {
        [Command("crash")]
        [Description("Produces an exception of a determined type")]
        [SuppressError(typeof(ArgumentException), "An argument exception ({exceptionType}) was thrown: {exception}")]
        [SuppressError(typeof(TypeLoadException), "The type \"{args}\" was not found in mscorlib or the current assembly")]
        public async Task Crash(CommandContext ctx, string exceptionType = "System.Exception")
        {
            // guard to ensure we don't get warnings for unused code, this does nothing
            if (exceptionType != null)
            {
                var type = Type.GetType(exceptionType);
                if (type == null)
                    throw new TypeLoadException("Type not found");
                throw Activator.CreateInstance(type) as Exception;
            }
            
            await ctx.RespondAsync("This should never be called");
        }
    }
    
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            // then we want to instantiate our client
            var client = new DiscordClient(new DiscordConfiguration
            {
                Token = "<token>",

                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            });

            var commands = client.UseCommandsNext(new CommandsNextConfiguration
            {
                // let's use the string prefix defined in config.json
                StringPrefixes = new[] { "+" },
            });
            
            commands.RegisterCommands(typeof(Program).Assembly);

            client.UseCNextNotifier();
            
            // let's hook some command events, so we know what's 
            // going on
            commands.CommandExecuted += Commands_CommandExecuted;
            commands.CommandErrored += Commands_CommandErrored;

            // finally, let's connect and log in
            await client.ConnectAsync();

            // and this is to prevent premature quitting
            await Task.Delay(-1);
        }

        private static Task Commands_CommandExecuted(CommandExecutionEventArgs e)
        {
            // let's log the name of the command and user
            e.Context.Client.DebugLogger.LogMessage(LogLevel.Info, "ExampleBot", $"{e.Context.User.Username} successfully executed '{e.Command.QualifiedName}'", DateTime.Now);

            // since this method is not async, let's return
            // a completed task, so that no additional work
            // is done
            return Task.CompletedTask;
        }

        private static async Task Commands_CommandErrored(CommandErrorEventArgs e)
        {
            // let's log the error details
            e.Context.Client.DebugLogger.LogMessage(LogLevel.Error, "ExampleBot", $"{e.Context.User.Username} tried executing '{e.Command?.QualifiedName ?? "<unknown command>"}' but it errored: {e.Exception.GetType()}: {e.Exception.Message ?? "<no message>"}", DateTime.Now);

            // let's check if the error is a result of lack
            // of required permissions
            if (e.Exception is ChecksFailedException)
            {
                // yes, the user lacks required permissions, 
                // let them know

                var emoji = DiscordEmoji.FromName(e.Context.Client, ":no_entry:");

                // let's wrap the response into an embed
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Access denied",
                    Description = $"{emoji} You do not have the permissions required to execute this command.",
                    Color = new DiscordColor(0xFF0000) // red
                    // there are also some pre-defined colors available
                    // as static members of the DiscordColor struct
                };
                await e.Context.RespondAsync("", embed: embed);
            }
        }
    }
}
#endif