using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace DSharpPlus.Interactivity
{
    internal class MessageVerifier : DiscordEventAwaiter<MessageCreateEventArgs, MessageContext>
    {
        private readonly Predicate<DiscordMessage> _predicate;

        public MessageVerifier(InteractivityExtension interactivity, Predicate<DiscordMessage> predicate)
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