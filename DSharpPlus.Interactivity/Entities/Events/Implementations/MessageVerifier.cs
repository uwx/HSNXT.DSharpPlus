using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace DSharpPlus.Interactivity
{
    internal class MessageVerifier : DiscordEventAwaiter<MessageCreateEventArgs, MessageContext>
    {
        private readonly Func<DiscordMessage, bool> _predicate;
        
        internal DiscordChannel Channel { get; set; }
        internal DiscordUser Author { get; set; }

        public MessageVerifier(InteractivityExtension interactivity, Func<DiscordMessage, bool> predicate)
            : base(interactivity)
        {
            _predicate = predicate;
        }

        protected override Task<MessageContext> CheckResult(MessageCreateEventArgs e)
        {
            if (Channel != null && e.Channel != Channel) return null;
            if (Author != null && e.Author != Author) return null;
            
            if (!_predicate(e.Message)) return null;
            
            return Task.FromResult(new MessageContext(Interactivity, e.Message));
        }
    }
}