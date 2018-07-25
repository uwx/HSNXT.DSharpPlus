using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace DSharpPlus.Interactivity
{
    internal class MessageVerifier : DiscordEventAwaiter<MessageCreateEventArgs, MessageContext>
    {
        private readonly Func<DiscordMessage, bool> _predicate;

        public MessageVerifier(InteractivityExtension interactivity, Func<DiscordMessage, bool> predicate)
            : base(interactivity)
        {
            _predicate = predicate;
        }

        protected override Task<MessageContext> CheckResult(MessageCreateEventArgs e) 
            => Task.FromResult(_predicate(e.Message)
                ? new MessageContext(Interactivity, e.Message)
                : null);
    }
}