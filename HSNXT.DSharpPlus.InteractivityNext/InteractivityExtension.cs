using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace HSNXT.DSharpPlus.InteractivityNext
{
    /// <summary>
    /// The main interactivity extension class.
    /// </summary>
    public partial class InteractivityExtension : BaseExtension
    {
        private InteractivityConfiguration Config { get; }
        
        private AwaiterHolder<MessageVerifier, MessageCreateEventArgs, MessageContext> _messageCreatedVerifiers;
        private AwaiterHolder<ReactionVerifier, MessageReactionAddEventArgs, ReactionContext> _reactionAddedVerifiers;
        
        private ReactionCancellationAwaiterHolder _reactionCollectionHandler;

        internal InteractivityExtension(InteractivityConfiguration cfg)
        {
            Config = cfg.Clone();
        }

        protected internal override void Setup(DiscordClient client)
        {
            Client = client;
            Config.SetDefaults(Client);
            
            _messageCreatedVerifiers = new AwaiterHolder<MessageVerifier, MessageCreateEventArgs, MessageContext>(
                ev => Client.MessageCreated += ev.Trigger,
                ev => Client.MessageCreated -= ev.Trigger
            );
            
            _reactionAddedVerifiers = new AwaiterHolder<ReactionVerifier, MessageReactionAddEventArgs, ReactionContext>(
                ev => Client.MessageReactionAdded += ev.Trigger,
                ev => Client.MessageReactionAdded -= ev.Trigger
            );
            
            _reactionCollectionHandler = new ReactionCancellationAwaiterHolder(this);
        }

        /// <summary>
        /// Creates a reaction-based poll. Resolves to a <see cref="ReactionCollectionContext"/> containing the relevant
        /// emote reactions.
        /// </summary>
        /// <param name="message">The message to run the poll on</param>
        /// <param name="emojiSource">The poll selection options. Each emote will be added to the message as a reaction.
        /// Default are thumbs up and thumbs down emojis.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a <see cref="ReactionCollectionContext"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="message"/> is null</exception>
        /// <exception cref="InvalidOperationException">If <paramref name="emojiSource"/> is empty</exception>
        public async Task<ReactionCollectionContext> CreatePollAsync(DiscordMessage message,
            IEnumerable<DiscordEmoji> emojiSource = null, TimeSpan? timeout = null, CancellationToken? ct = null)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            
            var emojis = emojiSource as DiscordEmoji[] ?? emojiSource?.ToArray() ?? Config.DefaultPollOptionsArray;
            if (emojis.Length == 0)
                throw new InvalidOperationException("A minimum of one emoji is required to execute this method!");

            foreach (var em in emojis)
            {
                await message.CreateReactionAsync(em);
            }

            return await _reactionCollectionHandler.HandleCancellableAsync(
                ReactionAddHandler, ReactionRemoveHandler, ReactionClearHandler, ct, timeout ?? Config.Timeout);
            
            async Task ReactionAddHandler(ReactionCollectionContext rcc, MessageReactionAddEventArgs e)
            {
                if (e.Message.Id != message.Id || e.Client.CurrentUser.Id == e.User.Id)
                    return;

                await Task.Yield();
                if (emojis.Count(x => x == e.Emoji) > 0)
                {
                    if (rcc.VotingMembers.Contains(e.User.Id)) // don't allow to vote twice
                        await e.Message.DeleteReactionAsync(e.Emoji, e.User);
                    else
                        rcc.AddReaction(e.Emoji, e.User.Id);
                }
                else
                {
                    // remove unrelated reactions
                    await e.Message.DeleteReactionAsync(e.Emoji, e.User);
                }
            }

            async Task ReactionRemoveHandler(ReactionCollectionContext rcc, MessageReactionRemoveEventArgs e)
            {
                if (e.Message.Id != message.Id || e.Client.CurrentUser.Id == e.User.Id)
                    return;

                await Task.Yield();
                if (emojis.Count(x => x == e.Emoji) > 0)
                {
                    rcc.RemoveReaction(e.Emoji, e.User.Id);
                }
            }

            async Task ReactionClearHandler(ReactionCollectionContext rcc, MessageReactionsClearEventArgs e)
            {
                if (e.Message.Id != message.Id)
                    return;

                await Task.Yield();
                rcc.ClearReactions();
                foreach (var em in emojis)
                {
                    await message.CreateReactionAsync(em);
                }
            }
        }

        /// <summary>
        /// Collects all new reactions to a message until a timer expires or the task is canceled.
        /// </summary>
        /// <param name="message">The message to collect reactions for</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task resolving to a <see cref="ReactionCollectionContext"/> containing the reactions.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="message"/> is null</exception>
        public async Task<ReactionCollectionContext> CollectReactionsAsync(DiscordMessage message, 
            TimeSpan? timeout = null, CancellationToken? ct = null)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            return await _reactionCollectionHandler.HandleCancellableAsync(
                ReactionAddHandler, ReactionRemoveHandler, ReactionClearHandler, ct, timeout ?? Config.Timeout);

            Task ReactionAddHandler(ReactionCollectionContext rcc, MessageReactionAddEventArgs e)
            {
                if (e.Message.Id == message.Id)
                    rcc.AddReaction(e.Emoji);

                return Task.FromResult(false);
            }

            Task ReactionRemoveHandler(ReactionCollectionContext rcc, MessageReactionRemoveEventArgs e)
            {
                if (e.Message.Id == message.Id)
                    rcc.RemoveReaction(e.Emoji, e.User.Id);

                return Task.FromResult(false);
            }

            Task ReactionClearHandler(ReactionCollectionContext rcc, MessageReactionsClearEventArgs e)
            {
                if (e.Message.Id == message.Id)
                    rcc.ClearReactions();

                return Task.FromResult(false);
            }
        }
    }
}
