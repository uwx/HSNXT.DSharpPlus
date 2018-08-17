using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace DSharpPlus.Interactivity
{
    internal class ReactionVerifier : DiscordEventAwaiter<MessageReactionAddEventArgs, ReactionContext>
    {
        private readonly Func<DiscordEmoji, bool> _predicate;
        
        internal DiscordChannel Channel { get; set; }
        internal DiscordUser User { get; set; }

        public ReactionVerifier(InteractivityExtension interactivity, Func<DiscordEmoji, bool> predicate)
            : base(interactivity)
        {
            _predicate = predicate;
        }

        protected override Task<ReactionContext> CheckResult(MessageReactionAddEventArgs e)
        {
            if (Channel != null && e.Channel != Channel) return null;
            if (User != null && e.User != User) return null;
            
            if (!_predicate(e.Emoji)) return null;

            return Task.FromResult(new ReactionContext(Interactivity, e.Emoji, e.Message));
        }
    }
}