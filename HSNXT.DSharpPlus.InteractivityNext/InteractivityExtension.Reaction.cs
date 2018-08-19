using System;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace DSharpPlus.Interactivity
{
    public partial class InteractivityExtension
    {
        #region Main (predicate)

        /// <summary>
        /// Returns a task that resolves to a reaction context containing a reaction matching a predicate
        /// <paramref name="predicate"/>, or <c>null</c> if the cancellation token <paramref name="ct"/> is canceled or
        /// the waiting period times out as specified by <paramref name="timeout"/>.
        /// </summary>
        /// <param name="predicate">Function that takes in a received reaction and returns whether or not that is the
        /// reaction to resolve the task with.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="ReactionContext"/> containing the reaction, or <c>null</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> is null</exception>
        public Task<ReactionContext> WaitForReactionAsync(
            Func<DiscordEmoji, bool> predicate, TimeSpan? timeout = null, CancellationToken? ct = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var verifier = new ReactionVerifier(this, predicate);
            return _reactionAddedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a reaction context containing a reaction in channel
        /// <paramref name="channel"/> matching a predicate <paramref name="predicate"/>, or <c>null</c> if the
        /// cancellation token <paramref name="ct"/> is canceled or the waiting period times out as specified by
        /// <paramref name="timeout"/>.
        /// </summary>
        /// <param name="predicate">Function that takes in a received reaction and returns whether or not that is the
        /// reaction to resolve the task with.</param>
        /// <param name="channel">Channel that the reaction must be sent in for it to be evaluated.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="ReactionContext"/> containing the reaction, or <c>null</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> is null</exception>
        public Task<ReactionContext> WaitForReactionAsync(
            Func<DiscordEmoji, bool> predicate, DiscordChannel channel, TimeSpan? timeout = null,
            CancellationToken? ct = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var verifier = new ReactionVerifier(this, predicate)
            {
                Channel = channel
            };
            return _reactionAddedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a reaction context containing a reaction sent by <paramref name="author"/>
        /// matching a predicate <paramref name="predicate"/>, or <c>null</c> if the cancellation token
        /// <paramref name="ct"/> is canceled or the waiting period times out as specified by
        /// <paramref name="timeout"/>.
        /// </summary>
        /// <param name="predicate">Function that takes in a received reaction and returns whether or not that is the
        /// reaction to resolve the task with.</param>
        /// <param name="author">User that must have sent the reaction for it to be evaluated.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="ReactionContext"/> containing the reaction, or <c>null</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> is null</exception>
        public Task<ReactionContext> WaitForReactionAsync(
            Func<DiscordEmoji, bool> predicate, DiscordUser author, TimeSpan? timeout = null,
            CancellationToken? ct = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var verifier = new ReactionVerifier(this, predicate)
            {
                User = author
            };
            return _reactionAddedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a reaction context containing a reaction sent by <paramref name="author"/>
        /// in channel <paramref name="channel"/> matching a predicate <paramref name="predicate"/>, or <c>null</c> if
        /// the cancellation token <paramref name="ct"/> is canceled or the waiting period times out as specified by
        /// <paramref name="timeout"/>.
        /// </summary>
        /// <param name="predicate">Function that takes in a received reaction and returns whether or not that is the
        /// reaction to resolve the task with.</param>
        /// <param name="channel">Channel that the reaction must be sent in for it to be evaluated.</param>
        /// <param name="author">User that must have sent the reaction for it to be evaluated.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="ReactionContext"/> containing the reaction, or <c>null</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> is null</exception>
        public Task<ReactionContext> WaitForReactionAsync(
            Func<DiscordEmoji, bool> predicate, DiscordChannel channel, DiscordUser author, TimeSpan? timeout = null,
            CancellationToken? ct = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var verifier = new ReactionVerifier(this, predicate)
            {
                Channel = channel,
                User = author
            };
            return _reactionAddedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        #endregion

        #region Main (no predicate)

        /// <summary>
        /// Returns a task that resolves to a reaction context containing the first new reaction the client receives,
        /// or <c>null</c> if the cancellation token <paramref name="ct"/> is canceled or the waiting period times out
        /// as specified by <paramref name="timeout"/>.
        /// </summary>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="ReactionContext"/> containing the reaction, or <c>null</c>.
        /// </returns>
        public Task<ReactionContext> WaitForReactionAsync(
            TimeSpan? timeout = null, CancellationToken? ct = null)
        {
            var verifier = new ReactionVerifier(this, null);
            return _reactionAddedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a reaction context containing a reaction in channel
        /// <paramref name="channel"/>, or <c>null</c> if the cancellation token
        /// <paramref name="ct"/> is canceled or the waiting period times out as specified by
        /// <paramref name="timeout"/>.
        /// </summary>
        /// <param name="channel">Channel that the reaction must be sent in for it to be evaluated.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="ReactionContext"/> containing the reaction, or <c>null</c>.
        /// </returns>
        public Task<ReactionContext> WaitForReactionAsync(
            DiscordChannel channel, TimeSpan? timeout = null, CancellationToken? ct = null)
        {
            var verifier = new ReactionVerifier(this, null)
            {
                Channel = channel
            };
            return _reactionAddedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a reaction context containing a reaction sent by <paramref name="author"/>,
        /// or <c>null</c> if the cancellation token <paramref name="ct"/> is canceled or the waiting period times out
        /// as specified by <paramref name="timeout"/>.
        /// </summary>
        /// <param name="author">User that must have sent the reaction for it to be evaluated.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="ReactionContext"/> containing the reaction, or <c>null</c>.
        /// </returns>
        public Task<ReactionContext> WaitForReactionAsync(
            DiscordUser author, TimeSpan? timeout = null, CancellationToken? ct = null)
        {
            var verifier = new ReactionVerifier(this, null)
            {
                User = author
            };
            return _reactionAddedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a reaction context containing a reaction sent by <paramref name="author"/>
        /// in channel <paramref name="channel"/>, or <c>null</c> if the cancellation token <paramref name="ct"/> is
        /// canceled or the waiting period times out as specified by <paramref name="timeout"/>.
        /// </summary>
        /// <param name="channel">Channel that the reaction must be sent in for it to be evaluated.</param>
        /// <param name="author">User that must have sent the reaction for it to be evaluated.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="ReactionContext"/> containing the reaction, or <c>null</c>.
        /// </returns>
        public Task<ReactionContext> WaitForReactionAsync(
            DiscordChannel channel, DiscordUser author, TimeSpan? timeout = null, CancellationToken? ct = null)
        {
            var verifier = new ReactionVerifier(this, null)
            {
                Channel = channel,
                User = author
            };
            return _reactionAddedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        #endregion

        #region Emoji (predicate)

        /// <summary>
        /// Returns a task that resolves to a reaction context containing a reaction with an emoji
        /// <paramref name="emoji"/> matching a predicate <paramref name="predicate"/>, or <c>null</c> if the
        /// cancellation token <paramref name="ct"/> is canceled or the waiting period times out as specified by
        /// <paramref name="timeout"/>.
        /// </summary>
        /// <param name="predicate">Function that takes in a received reaction and returns whether or not that is the
        /// reaction to resolve the task with.</param>
        /// <param name="emoji">The reaction emoji to look for.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="ReactionContext"/> containing the reaction, or <c>null</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> is null</exception>
        public Task<ReactionContext> WaitForReactionAsync(
            Func<DiscordEmoji, bool> predicate, DiscordEmoji emoji, TimeSpan? timeout = null,
            CancellationToken? ct = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if (emoji == null)
                throw new ArgumentNullException(nameof(emoji));

            var verifier = new ReactionVerifier(this, predicate) {Emoji = emoji};
            return _reactionAddedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a reaction context containing a reaction with an emoji
        /// <paramref name="emoji"/> in channel <paramref name="channel"/> matching a predicate
        /// <paramref name="predicate"/>, or <c>null</c> if the cancellation token <paramref name="ct"/> is canceled or
        /// the waiting period times out as specified by <paramref name="timeout"/>.
        /// </summary>
        /// <param name="predicate">Function that takes in a received reaction and returns whether or not that is the
        /// reaction to resolve the task with.</param>
        /// <param name="channel">Channel that the reaction must be sent in for it to be evaluated.</param>
        /// <param name="emoji">The reaction emoji to look for.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="ReactionContext"/> containing the reaction, or <c>null</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> is null</exception>
        public Task<ReactionContext> WaitForReactionAsync(
            Func<DiscordEmoji, bool> predicate, DiscordChannel channel, DiscordEmoji emoji, TimeSpan? timeout = null,
            CancellationToken? ct = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if (emoji == null)
                throw new ArgumentNullException(nameof(emoji));

            var verifier = new ReactionVerifier(this, predicate)
            {
                Emoji = emoji,
                Channel = channel
            };
            return _reactionAddedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a reaction context containing a reaction with an emoji
        /// <paramref name="emoji"/> sent by <paramref name="author"/> matching a predicate
        /// <paramref name="predicate"/>, or <c>null</c> if the cancellation token <paramref name="ct"/> is canceled or
        /// the waiting period times out as specified by <paramref name="timeout"/>.
        /// </summary>
        /// <param name="predicate">Function that takes in a received reaction and returns whether or not that is the
        /// reaction to resolve the task with.</param>
        /// <param name="author">User that must have sent the reaction for it to be evaluated.</param>
        /// <param name="emoji">The reaction emoji to look for.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="ReactionContext"/> containing the reaction, or <c>null</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> is null</exception>
        public Task<ReactionContext> WaitForReactionAsync(
            Func<DiscordEmoji, bool> predicate, DiscordUser author, DiscordEmoji emoji, TimeSpan? timeout = null,
            CancellationToken? ct = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if (emoji == null)
                throw new ArgumentNullException(nameof(emoji));

            var verifier = new ReactionVerifier(this, predicate)
            {
                Emoji = emoji,
                User = author
            };
            return _reactionAddedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a reaction context containing a reaction with an emoji
        /// <paramref name="emoji"/> sent by <paramref name="author"/> in channel <paramref name="channel"/> matching a
        /// predicate <paramref name="predicate"/>, or <c>null</c> if the cancellation token <paramref name="ct"/> is
        /// canceled or the waiting period times out as specified by <paramref name="timeout"/>.
        /// </summary>
        /// <param name="predicate">Function that takes in a received reaction and returns whether or not that is the
        /// reaction to resolve the task with.</param>
        /// <param name="channel">Channel that the reaction must be sent in for it to be evaluated.</param>
        /// <param name="author">User that must have sent the reaction for it to be evaluated.</param>
        /// <param name="emoji">The reaction emoji to look for.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="ReactionContext"/> containing the reaction, or <c>null</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> is null</exception>
        public Task<ReactionContext> WaitForReactionAsync(
            Func<DiscordEmoji, bool> predicate, DiscordChannel channel, DiscordUser author, DiscordEmoji emoji,
            TimeSpan? timeout = null, CancellationToken? ct = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if (emoji == null)
                throw new ArgumentNullException(nameof(emoji));

            var verifier = new ReactionVerifier(this, predicate)
            {
                Emoji = emoji,
                Channel = channel,
                User = author
            };
            return _reactionAddedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        #endregion

        #region Emoji (no predicate)

        /// <summary>
        /// Returns a task that resolves to a reaction context containing a reaction with an emoji
        /// <paramref name="emoji"/>, or <c>null</c> if the cancellation token <paramref name="ct"/> is canceled or the
        /// waiting period times out as specified by <paramref name="timeout"/>.
        /// </summary>
        /// <param name="emoji">The reaction emoji to look for.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="ReactionContext"/> containing the reaction, or <c>null</c>.
        /// </returns>
        public Task<ReactionContext> WaitForReactionAsync(
            DiscordEmoji emoji, TimeSpan? timeout = null, CancellationToken? ct = null)
        {
            if (emoji == null)
                throw new ArgumentNullException(nameof(emoji));

            var verifier = new ReactionVerifier(this, null) {Emoji = emoji};
            return _reactionAddedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a reaction context containing a reaction with an emoji
        /// <paramref name="emoji"/> in channel <paramref name="channel"/>, or <c>null</c> if the cancellation token
        /// <paramref name="ct"/> is canceled or the waiting period times out as specified by
        /// <paramref name="timeout"/>.
        /// </summary>
        /// <param name="channel">Channel that the reaction must be sent in for it to be evaluated.</param>
        /// <param name="emoji">The reaction emoji to look for.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="ReactionContext"/> containing the reaction, or <c>null</c>.
        /// </returns>
        public Task<ReactionContext> WaitForReactionAsync(
            DiscordChannel channel, DiscordEmoji emoji, TimeSpan? timeout = null, CancellationToken? ct = null)
        {
            if (emoji == null)
                throw new ArgumentNullException(nameof(emoji));

            var verifier = new ReactionVerifier(this, null)
            {
                Emoji = emoji,
                Channel = channel
            };
            return _reactionAddedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a reaction context containing a reaction with an emoji
        /// <paramref name="emoji"/> sent by <paramref name="author"/>, or <c>null</c> if the cancellation token
        /// <paramref name="ct"/> is canceled or the waiting period times out as specified by
        /// <paramref name="timeout"/>.
        /// </summary>
        /// <param name="author">User that must have sent the reaction for it to be evaluated.</param>
        /// <param name="emoji">The reaction emoji to look for.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="ReactionContext"/> containing the reaction, or <c>null</c>.
        /// </returns>
        public Task<ReactionContext> WaitForReactionAsync(
            DiscordUser author, DiscordEmoji emoji, TimeSpan? timeout = null, CancellationToken? ct = null)
        {
            if (emoji == null)
                throw new ArgumentNullException(nameof(emoji));

            var verifier = new ReactionVerifier(this, null)
            {
                Emoji = emoji,
                User = author
            };
            return _reactionAddedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a reaction context containing a reaction with an emoji
        /// <paramref name="emoji"/> sent by <paramref name="author"/> in channel <paramref name="channel"/>, or
        /// <c>null</c> if the cancellation token <paramref name="ct"/> is canceled or the waiting period times out as
        /// specified by <paramref name="timeout"/>.
        /// </summary>
        /// <param name="channel">Channel that the reaction must be sent in for it to be evaluated.</param>
        /// <param name="author">User that must have sent the reaction for it to be evaluated.</param>
        /// <param name="emoji">The reaction emoji to look for.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="ReactionContext"/> containing the reaction, or <c>null</c>.
        /// </returns>
        public Task<ReactionContext> WaitForReactionAsync(
            DiscordChannel channel, DiscordUser author, DiscordEmoji emoji, TimeSpan? timeout = null,
            CancellationToken? ct = null)
        {
            if (emoji == null)
                throw new ArgumentNullException(nameof(emoji));

            var verifier = new ReactionVerifier(this, null)
            {
                Emoji = emoji,
                Channel = channel,
                User = author
            };
            return _reactionAddedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        #endregion
    }
}