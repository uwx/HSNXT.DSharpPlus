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
        /// <param name="messagePages">
        /// Pages for this message, you can create your own <see cref="Page"/> instances or use either
        /// <see cref="GeneratePagesInEmbeds"/> (for embeds) or <see cref="GeneratePagesInStrings"/> (for text-based
        /// messages)
        /// </param>
        /// <param name="ct">Cancellation token that can be used to end the pagination.</param>
        /// <param name="timeout">Timeout until pagination ends. If not specified, defaults to 
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="finishBehaviourOverride">
        /// Action to perform on the message once the pagination ends. Defaults to
        /// <see cref="InteractivityConfiguration.PaginationBehavior"/>
        /// </param>
        /// <param name="emojis">
        /// The emojis to use for pagination controls. Defaults to
        /// <see cref="InteractivityConfiguration.DefaultPaginationEmojis"/>
        /// </param>
        /// <returns>A task that resolves once the pagination process finishes</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="channel"/>, <paramref name="user"/> or <paramref name="messagePages"/> is null
        /// </exception>
        [SuppressMessage("ReSharper", "AccessToDisposedClosure")] // i'm confident that my code starts and ends properly
        public async Task SendPaginatedMessage(DiscordChannel channel, DiscordUser user, IEnumerable<Page> messagePages,
            CancellationToken? ct = null, TimeSpan? timeout = null, FinishBehaviour? finishBehaviourOverride = null,
            PaginationEmojis emojis = null)
        {

            if (channel == null)
                throw new ArgumentNullException(nameof(channel));
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (messagePages == null)
                throw new ArgumentNullException(nameof(messagePages));

            var pages = messagePages as Page[] ?? messagePages.ToArray();

            var startingPage = pages.FirstOrDefault() 
                               ?? throw new ArgumentException("Must have at least one page", nameof(messagePages));
            var initialContent = startingPage.Content ?? "";
            var initialEmbed = startingPage.Embed;
            if (pages.Length == 1)
            {
                await channel.SendMessageAsync(initialContent, embed: initialEmbed);
                return;
            }
            
            var timeoutBehaviour = finishBehaviourOverride ?? Config.PaginationBehavior;

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
                    case FinishBehaviour.Ignore:
                        await msg.DeleteAllReactionsAsync();
                        break;
                    case FinishBehaviour.DeleteMessage:
                        await msg.DeleteAsync();
                        break;
                    case FinishBehaviour.DeleteReactions:
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
        /// Turns a long string into an enumerable containing pages holding parts of the input string, split at around
        /// the Discord length limit, placed in embeds.
        /// </summary>
        /// <param name="input">The string to paginate</param>
        /// <returns>An enumerable of pages</returns>
        /// <exception cref="ArgumentException">If <paramref name="input"/> is null or empty</exception>
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

        /// <summary>
        /// Turns a long string into an enumerable containing pages holding parts of the input string, split at around
        /// the Discord length limit, <b>not</b> placed in embeds.
        /// </summary>
        /// <param name="input">The string to paginate</param>
        /// <returns>An enumerable of pages</returns>
        /// <exception cref="ArgumentException">If <paramref name="input"/> is null or empty</exception>
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

        /// <summary>
        /// Sends a paginated message containing plain text based on a simple input string split at around the message
        /// length limit. How much gets split per page is implementation-defined.
        /// </summary>
        /// <param name="channel">Channel to send the paginated message to</param>
        /// <param name="user">The user that is allowed to interact with the paginated message</param>
        /// <param name="input">
        /// Pages for this message, you can create your own <see cref="Page"/> instances or use either
        /// <see cref="GeneratePagesInEmbeds"/> (for embeds) or <see cref="GeneratePagesInStrings"/> (for text-based
        /// messages)
        /// </param>
        /// <param name="ct">Cancellation token that can be used to end the pagination.</param>
        /// <param name="timeout">Timeout until pagination ends. If not specified, defaults to 
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="finishBehaviourOverride">
        /// Action to perform on the message once the pagination ends. Defaults to
        /// <see cref="InteractivityConfiguration.PaginationBehavior"/>
        /// </param>
        /// <param name="emojis">
        /// The emojis to use for pagination controls. Defaults to
        /// <see cref="InteractivityConfiguration.DefaultPaginationEmojis"/>
        /// </param>
        /// <returns>A task that resolves once the pagination process finishes</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="channel"/>, <paramref name="user"/></exception>
        /// <exception cref="ArgumentException">If <paramref name="input"/> is null or empty</exception>
        public Task SendSplitMessage(DiscordChannel channel, DiscordUser user, string input,
            CancellationToken? ct = null, TimeSpan? timeout = null, FinishBehaviour? finishBehaviourOverride = null,
            PaginationEmojis emojis = null)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input string may not be null or empty", nameof(input));

            if (input.Length < 2000)
            {
                return channel.SendMessageAsync(input);
            }

            return SendPaginatedMessage(channel, user, GeneratePagesInStrings(input), ct, timeout,
                finishBehaviourOverride, emojis);
        }

        /// <summary>
        /// Appends all available pagination emojis as reactions to a message. This is usually only useful when creating
        /// custom pagination handling.
        /// </summary>
        /// <param name="message">The message to react to</param>
        /// <param name="emojis">The pagination emojis to add reactions for</param>
        /// <returns>Task that completes when all the reactions have been added</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="message"/> is null</exception>
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

        /// <summary>
        /// Performs a single iteration of pagination. This is usually only useful when creating custom pagination
        /// handling.
        /// </summary>
        /// <param name="emoji">An emoji representing the action to be performed on the pagination instance</param>
        /// <param name="message">The message that the pagination belongs to</param>
        /// <param name="messagePage">The pagination instance itself</param>
        /// <param name="cts">Cancellation token that will be triggered if stop was triggered</param>
        /// <param name="emojis">The emojis to use for pagination controls</param>
        /// <returns>Task that resolves once the new page is loaded</returns>
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

            var lastIndex = messagePage.CurrentIndex;
            
            if (emoji == emojis.SkipLeft)
            {
                messagePage.CurrentIndex = 0;
            }
            else if (emoji == emojis.Left)
            {
                if (messagePage.CurrentIndex > 0)
                    messagePage.CurrentIndex--;
            }
            else if (emoji == emojis.Stop)
            {
                cts.Cancel();
                return;
            }
            else if (emoji == emojis.Right)
            {
                if (messagePage.CurrentIndex < messagePage.Pages.Count - 1)
                    messagePage.CurrentIndex++;
            }
            else if (emoji == emojis.SkipRight)
            {
                messagePage.CurrentIndex = messagePage.Pages.Count - 1;
            }
            else
            {
                return;
            }

            if (lastIndex != messagePage.CurrentIndex)
            {
                var currentPage = messagePage.Pages[messagePage.CurrentIndex];
                await message.ModifyAsync(currentPage.Content, currentPage.Embed);
            }
        }

        // TODO WIP
        public abstract class BaseControlContext : IDisposable
        {
            public DiscordMessage Message { get; internal set; }

            protected internal BaseControlContext()
            {
            }

            public virtual void Dispose()
            {
            }
        }
        
        // perform reactions initialization (called first time and whenever reactions are cleared)
        public delegate Task DoControlReactFunc<in TCtx>(TCtx ctx)
            where TCtx : BaseControlContext;
        // single update of control instance
        public delegate Task DoControlUpdateFunc<in TCtx>(DiscordEmoji emoji, TCtx ctx, CancellationTokenSource exitTrigger)
            where TCtx : BaseControlContext;
        // called once control is done (you can also use Dispose on your context but this is async and provides the last
        // reaction)
        public delegate Task DoControlCleanupFunc<in TCtx>(DiscordEmoji lastReaction, TCtx ctx)
            where TCtx : BaseControlContext;
        
        [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
        public async Task ControlMessageAsync<TCtx>(
            DiscordMessage msg, TCtx context, DoControlReactFunc<TCtx> reactFunc, DoControlUpdateFunc<TCtx> updateFunc,
            DoControlCleanupFunc<TCtx> cleanupFunc, DiscordUser controller = null, CancellationToken? ct = null,
            TimeSpan? timeout = null)
            where TCtx : BaseControlContext
        {
                
            if (msg == null)
                throw new ArgumentNullException(nameof(msg));
            if (controller == null)
                controller = msg.Author;
            
            context.Message = msg;
            await reactFunc(context);
            
            DiscordEmoji lastReaction = null;
            
            // wrap around existing cancellation token, and make a timer that can be reset that cancels our new token
            // source
            using (context)
            using (var cts = ct.HasValue
                ? CancellationTokenSource.CreateLinkedTokenSource(ct.Value)
                : new CancellationTokenSource())
            using (var timer = new Timer(_ => cts.Cancel()))
            {
                timer.Change(timeout ?? Config.Timeout, Timeout.InfiniteTimeSpan);
                
                await _reactionCollectionHandler.HandleCancellableVoidAsync(
                    ReactionAddHandler, ReactionRemoveHandler, ReactionClearHandler, cts.Token, timeout ?? Config.Timeout);

                await cleanupFunc(lastReaction, context);

                async Task ReactionAddHandler(MessageReactionAddEventArgs e)
                {
                    if (e.Message.Id != msg.Id || e.User.Id != controller.Id)
                        return;

                    timer.Change(timeout ?? Config.Timeout, Timeout.InfiniteTimeSpan);
                    lastReaction = e.Emoji;
                    await updateFunc(e.Emoji, context, cts);
                }

                async Task ReactionRemoveHandler(MessageReactionRemoveEventArgs e)
                {
                    if (e.Message.Id != msg.Id || e.User.Id != controller.Id)
                        return;

                    timer.Change(timeout ?? Config.Timeout, Timeout.InfiniteTimeSpan);
                    lastReaction = e.Emoji;
                    await updateFunc(e.Emoji, context, cts);
                }

                async Task ReactionClearHandler(MessageReactionsClearEventArgs e)
                {
                    if (e.Message.Id != msg.Id)
                        return;

                    timer.Change(timeout ?? Config.Timeout, Timeout.InfiniteTimeSpan);
                    await reactFunc(context);
                }
            }
        }
        #endregion
    }
}
