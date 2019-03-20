using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace DSharpPlus.Interactivity
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
            Config.SetDefaults(client);
            
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
        
        #region Pagination
        /// <summary>
        /// Sends a paginated message. A paginated message is a message that can be controlled and updated dynamically
        /// by clicking on certain reactions.
        /// </summary>
        /// <param name="channel">Channel to send the paginated message to</param>
        /// <param name="user">The user that is allowed to interact with the paginated message</param>
        /// <param name="messagePages">Pages for this message</param>
        /// <param name="ct">Cancellation token that can be used to end the pagination.</param>
        /// <param name="timeout">Timeout override</param>
        /// <param name="timeoutBehaviourOverride">Timeout behaviour override</param>
        /// <param name="emojis">Pagination emoji override</param>
        /// <returns></returns>
        [SuppressMessage("ReSharper", "AccessToDisposedClosure")] // i'm confident that my code starts and ends properly
        public async Task SendPaginatedMessage(DiscordChannel channel, DiscordUser user,
            IEnumerable<Page> messagePages, CancellationToken? ct = null, TimeSpan? timeout = null, 
            TimeoutBehaviour? timeoutBehaviourOverride = null, PaginationEmojis emojis = null)
        {

            if (channel == null)
                throw new ArgumentNullException(nameof(channel));
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (messagePages == null)
                throw new ArgumentNullException(nameof(messagePages));
            
            var pages = messagePages as List<Page> ?? messagePages.ToList();

            var startingPage = pages.FirstOrDefault() 
                               ?? throw new ArgumentException("Must have at least one page", nameof(messagePages));
            var initialContent = startingPage.Content ?? "";
            var initialEmbed = startingPage.Embed;
            if (pages.Count == 1)
            {
                await channel.SendMessageAsync(initialContent, embed: initialEmbed);
                return;
            }
            
            var timeoutBehaviour = timeoutBehaviourOverride ?? Config.PaginationBehavior;

            var msg = await channel.SendMessageAsync(initialContent, embed: initialEmbed);
            var paginatedMessage = new PaginatedMessage
            {
                CurrentIndex = 0,
                Pages = pages,
                Timeout = timeout ?? Config.Timeout
            };

            emojis = emojis ?? Config.DefaultPaginationEmojis;

            await GeneratePaginationReactions(msg, emojis);
            
            // wrap around existing cancellation token, and make a timer that can be reset that cancels our new token
            // source
            using (var cts = ct.HasValue
                ? CancellationTokenSource.CreateLinkedTokenSource(ct.Value)
                : new CancellationTokenSource())
            using (var timer = new Timer(_ => cts.Cancel()))
            {
                timer.Change(timeout ?? Config.Timeout, Timeout.InfiniteTimeSpan);
                
                await _reactionCollectionHandler.HandleCancellableVoidAsync(
                    ReactionAddHandler, ReactionRemoveHandler, ReactionClearHandler, cts.Token, timeout ?? Config.Timeout);

                switch (timeoutBehaviour)
                {
                    case TimeoutBehaviour.Ignore:
                        await msg.DeleteAllReactionsAsync();
                        break;
                    case TimeoutBehaviour.DeleteMessage:
                        await msg.DeleteAsync();
                        break;
                    case TimeoutBehaviour.DeleteReactions:
                        await msg.DeleteAllReactionsAsync();
                        break;
                }

                async Task ReactionAddHandler(MessageReactionAddEventArgs e)
                {
                    if (e.Message.Id != msg.Id || e.User.Id != user.Id)
                        return;

                    timer.Change(timeout ?? Config.Timeout, Timeout.InfiniteTimeSpan);
                    await DoPagination(e.Emoji, msg, paginatedMessage, cts, emojis);
                }

                async Task ReactionRemoveHandler(MessageReactionRemoveEventArgs e)
                {
                    if (e.Message.Id != msg.Id || e.User.Id != user.Id)
                        return;

                    timer.Change(timeout ?? Config.Timeout, Timeout.InfiniteTimeSpan);
                    await DoPagination(e.Emoji, msg, paginatedMessage, cts, emojis);
                }

                async Task ReactionClearHandler(MessageReactionsClearEventArgs e)
                {
                    if (e.Message.Id != msg.Id)
                        return;

                    timer.Change(timeout ?? Config.Timeout, Timeout.InfiniteTimeSpan);
                    await GeneratePaginationReactions(msg, emojis);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<Page> GeneratePagesInEmbeds(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input string may not be null or empty", nameof(input));
            
            var split = input.Split(2000);
            var page = 1;
            foreach (var s in split)
            {
                yield return new Page
                {
                    Embed = new DiscordEmbed
                    {
                        Title = string.Format(Config.DefaultPageHeader, page),
                        Description = s
                    }
                };
                page++;
            }
        }

        public IEnumerable<Page> GeneratePagesInStrings(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input string may not be null or empty", nameof(input));

            var split = input.Split(1900);
            var page = 1;
            foreach (var s in split)
            {
                yield return new Page
                {
                    Content = string.Format(Config.DefaultStringPageHeader, page, s)
                };
                page++;
            }
        }

        public Task SendSplitMessage(DiscordChannel channel, DiscordUser user, string input,
            CancellationToken? ct = null, TimeSpan? timeout = null, 
            TimeoutBehaviour? timeoutBehaviourOverride = null, PaginationEmojis emojis = null)
        {
            if (input.Length < 2000)
            {
                return channel.SendMessageAsync(input);
            }

            return SendPaginatedMessage(channel, user, GeneratePagesInStrings(input), ct, timeout,
                timeoutBehaviourOverride, emojis);
        }

        public async Task GeneratePaginationReactions(DiscordMessage message, PaginationEmojis emojis)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            foreach (var paginationEmoji in emojis)
            {
                if (paginationEmoji != null)
                    await message.CreateReactionAsync(paginationEmoji);
            }
        }

        public async Task DoPagination(DiscordEmoji emoji, DiscordMessage message, PaginatedMessage messagePage, CancellationTokenSource cts, PaginationEmojis emojis)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (messagePage == null)
                throw new ArgumentNullException(nameof(messagePage));
            if (emoji == null)
                throw new ArgumentNullException(nameof(emoji));
            if (cts == null)
                throw new ArgumentNullException(nameof(cts));

            if (emoji == emojis.SkipLeft)
            {
                messagePage.CurrentIndex = 0;
            }
            else if (emoji == emojis.Left)
            {
                if (messagePage.CurrentIndex != 0)
                    messagePage.CurrentIndex--;
            }
            else if (emoji == emojis.Stop)
            {
                cts.Cancel();
            }
            else if (emoji == emojis.Right)
            {
                if (messagePage.CurrentIndex != messagePage.Pages.Count() - 1)
                    messagePage.CurrentIndex++;
            }
            else if (emoji == emojis.SkipRight)
            {
                messagePage.CurrentIndex = messagePage.Pages.Count() - 1;
            }
            else
            {
                return;
            }

            var pagesArr = messagePage.Pages as Page[] ?? messagePage.Pages.ToArray();
            var currentPage = pagesArr[messagePage.CurrentIndex];
            await message.ModifyAsync(currentPage.Content ?? "", currentPage.Embed);
        }
        #endregion
    }
}
