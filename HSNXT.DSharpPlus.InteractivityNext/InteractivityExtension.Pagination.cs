using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace HSNXT.DSharpPlus.InteractivityNext
{
    public partial class InteractivityExtension
    {
        // TODO WIP (and docs)

        // perform reactions initialization (called first time and whenever reactions are cleared)
        public delegate Task DoControlReactFunc<in TS>(TS ctx)
            where TS : BaseControlState;
        // single update of control instance
        public delegate Task DoControlUpdateFunc<in TS>(DiscordEmoji emoji, TS ctx, CancellationTokenSource exitTrigger)
            where TS : BaseControlState;
        // called once control is done (you can also use Dispose on your context but this is async and provides the last
        // reaction)
        public delegate Task DoControlCleanupFunc<in TS>(DiscordEmoji lastReaction, TS ctx)
            where TS : BaseControlState;
        
        [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
        public async Task ControlMessageAsync<TS>(
            DiscordMessage msg, TS controlState, DoControlReactFunc<TS> reactFunc, DoControlUpdateFunc<TS> updateFunc,
            DoControlCleanupFunc<TS> cleanupFunc, DiscordUser controller = null, CancellationToken? ct = null,
            TimeSpan? timeout = null)
            where TS : BaseControlState
        {
            if (msg == null)
                throw new ArgumentNullException(nameof(msg));
            if (controlState == null)
                throw new ArgumentNullException(nameof(controlState));
            if (reactFunc == null)
                throw new ArgumentNullException(nameof(reactFunc));
            if (updateFunc == null)
                throw new ArgumentNullException(nameof(updateFunc));
            if (cleanupFunc == null)
                throw new ArgumentNullException(nameof(cleanupFunc));
            if (controller == null)
                controller = msg.Author;
            
            controlState.Message = msg;
            await reactFunc(controlState);
            
            DiscordEmoji lastReaction = null;
            
            // wrap around existing cancellation token, and make a timer that can be reset that cancels our new token
            // source
            using (controlState)
            using (var cts = ct.HasValue
                ? CancellationTokenSource.CreateLinkedTokenSource(ct.Value)
                : new CancellationTokenSource())
            using (var timer = new Timer(_ => cts.Cancel()))
            {
                timer.Change(timeout ?? Config.Timeout, Timeout.InfiniteTimeSpan);
                
                await _reactionCollectionHandler.HandleCancellableVoidAsync(
                    ReactionAddHandler, ReactionRemoveHandler, ReactionClearHandler, cts.Token, Timeout.InfiniteTimeSpan);

                await cleanupFunc(lastReaction, controlState);

                async Task ReactionAddHandler(MessageReactionAddEventArgs e)
                {
                    if (e.Message.Id != msg.Id || e.User.Id != controller.Id)
                        return;

                    timer.Change(timeout ?? Config.Timeout, Timeout.InfiniteTimeSpan);
                    lastReaction = e.Emoji;
                    await updateFunc(e.Emoji, controlState, cts);
                }

                async Task ReactionRemoveHandler(MessageReactionRemoveEventArgs e)
                {
                    if (e.Message.Id != msg.Id || e.User.Id != controller.Id)
                        return;

                    timer.Change(timeout ?? Config.Timeout, Timeout.InfiniteTimeSpan);
                    lastReaction = e.Emoji;
                    await updateFunc(e.Emoji, controlState, cts);
                }

                async Task ReactionClearHandler(MessageReactionsClearEventArgs e)
                {
                    if (e.Message.Id != msg.Id)
                        return;

                    timer.Change(timeout ?? Config.Timeout, Timeout.InfiniteTimeSpan);
                    await reactFunc(controlState);
                }
            }
        }
        
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
            var initialContent = startingPage.Content;
            var initialEmbed = startingPage.Embed;
            if (pages.Length == 1)
            {
                await channel.SendMessageAsync(initialContent, embed: initialEmbed);
                return;
            }
            
            var finishBehaviour = finishBehaviourOverride ?? Config.PaginationBehavior;

            emojis = emojis ?? Config.DefaultPaginationEmojis;

            // ReSharper disable ArgumentsStyleNamedExpression ArgumentsStyleOther
            await ControlMessageAsync(
                msg: await channel.SendMessageAsync(initialContent, embed: initialEmbed),
                controlState: PaginationState.GetDefault(pages, timeout ?? Config.Timeout), 
                reactFunc: React, updateFunc: Update, cleanupFunc: Cleanup, controller: user, ct, timeout
            );
            // ReSharper restore ArgumentsStyleNamedExpression ArgumentsStyleOther
            
            Task React(PaginationState ctx) 
                => GeneratePaginationReactions(ctx.Message, emojis);

            Task Update(DiscordEmoji emoji, PaginationState ctx, CancellationTokenSource cts) 
                => DoPagination(emoji, ctx, cts, emojis);

            Task Cleanup(DiscordEmoji lastReaction, PaginationState ctx)
            {
                switch (finishBehaviour)
                {
                    case FinishBehaviour.Ignore:
                        return Task.CompletedTask;
                    case FinishBehaviour.DeleteMessage:
                        return ctx.Message.DeleteAsync();
                    case FinishBehaviour.DeleteReactions:
                        return ctx.Message.DeleteAllReactionsAsync();
                    default:
                        throw new InvalidOperationException($"Unknown finish behaviour {finishBehaviour}");
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
        public IEnumerable<Page> GeneratePagesInEmbeds(string input, DiscordEmbed source = null)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input string may not be null or empty", nameof(input));

            source = source ?? new DiscordEmbed();
            
            var split = input.Split(2000);
            var page = 1;
            foreach (var s in split)
            {
                yield return new Page
                {
                    Embed = new DiscordEmbedBuilder(source)
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
        /// <param name="state">The pagination instance itself</param>
        /// <param name="cts">Cancellation token that will be triggered if stop was triggered</param>
        /// <param name="emojis">The emojis to use for pagination controls</param>
        /// <returns>Task that resolves once the new page is loaded</returns>
        public async Task DoPagination(DiscordEmoji emoji, PaginationState state, CancellationTokenSource cts, PaginationEmojis emojis)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            if (emoji == null)
                throw new ArgumentNullException(nameof(emoji));
            if (cts == null)
                throw new ArgumentNullException(nameof(cts));
            if (emojis == null)
                throw new ArgumentNullException(nameof(emojis));

            var lastIndex = state.CurrentIndex;
            
            if (emoji == emojis.SkipLeft)
            {
                state.CurrentIndex = 0;
            }
            else if (emoji == emojis.Left)
            {
                if (state.CurrentIndex > 0)
                    state.CurrentIndex--;
            }
            else if (emoji == emojis.Stop)
            {
                cts.Cancel();
                return;
            }
            else if (emoji == emojis.Right)
            {
                if (state.CurrentIndex < state.Pages.Count - 1)
                    state.CurrentIndex++;
            }
            else if (emoji == emojis.SkipRight)
            {
                state.CurrentIndex = state.Pages.Count - 1;
            }
            else
            {
                return;
            }

            if (lastIndex != state.CurrentIndex)
            {
                var currentPage = state.Pages[state.CurrentIndex];
                await state.Message.ModifyAsync(currentPage.Content, currentPage.Embed);
            }
        }
    }
}