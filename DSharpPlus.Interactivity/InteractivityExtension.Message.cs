using System;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace DSharpPlus.Interactivity
{
    public partial class InteractivityExtension
    {
        #region Predicate

        /// <summary>
        /// Returns a task that resolves to a message context containing a message matching a predicate
        /// <paramref name="predicate"/>, or <c>null</c> if the cancellation token <paramref name="ct"/> is canceled or
        /// the waiting period times out as specified by <paramref name="timeout"/>.
        /// </summary>
        /// <param name="predicate">Function that takes in a received message and returns whether or not that is the
        /// message to resolve the task with.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="MessageContext"/> containing the message, or <c>null</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> is null</exception>
        public Task<MessageContext> WaitForMessageAsync(
            Func<DiscordMessage, bool> predicate, TimeSpan? timeout = null, CancellationToken? ct = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var verifier = new MessageVerifier(this, predicate);
            return _messageCreatedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a message context containing a message in channel <paramref name="channel"/>
        /// matching a predicate <paramref name="predicate"/>, or <c>null</c> if the cancellation token
        /// <paramref name="ct"/> is canceled or the waiting period times out as specified by
        /// <paramref name="timeout"/>.
        /// </summary>
        /// <param name="predicate">Function that takes in a received message and returns whether or not that is the
        /// message to resolve the task with.</param>
        /// <param name="channel">Channel that the message must be sent in for it to be evaluated.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="MessageContext"/> containing the message, or <c>null</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> is null</exception>
        public Task<MessageContext> WaitForMessageAsync(
            Func<DiscordMessage, bool> predicate, DiscordChannel channel, TimeSpan? timeout = null,
            CancellationToken? ct = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var verifier = new MessageVerifier(this, predicate)
            {
                Channel = channel
            };
            return _messageCreatedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a message context containing a message sent by <paramref name="author"/>
        /// matching a predicate <paramref name="predicate"/>, or <c>null</c> if the cancellation token
        /// <paramref name="ct"/> is canceled or the waiting period times out as specified by
        /// <paramref name="timeout"/>.
        /// </summary>
        /// <param name="predicate">Function that takes in a received message and returns whether or not that is the
        /// message to resolve the task with.</param>
        /// <param name="author">User that must have sent the message for it to be evaluated.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="MessageContext"/> containing the message, or <c>null</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> is null</exception>
        public Task<MessageContext> WaitForMessageAsync(
            Func<DiscordMessage, bool> predicate, DiscordUser author, TimeSpan? timeout = null,
            CancellationToken? ct = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var verifier = new MessageVerifier(this, predicate)
            {
                Author = author
            };
            return _messageCreatedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a message context containing a message sent by <paramref name="author"/>
        /// in channel <paramref name="channel"/> matching a predicate <paramref name="predicate"/>, or <c>null</c> if
        /// the cancellation token <paramref name="ct"/> is canceled or the waiting period times out as specified by
        /// <paramref name="timeout"/>.
        /// </summary>
        /// <param name="predicate">Function that takes in a received message and returns whether or not that is the
        /// message to resolve the task with.</param>
        /// <param name="channel">Channel that the message must be sent in for it to be evaluated.</param>
        /// <param name="author">User that must have sent the message for it to be evaluated.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="MessageContext"/> containing the message, or <c>null</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> is null</exception>
        public Task<MessageContext> WaitForMessageAsync(
            Func<DiscordMessage, bool> predicate, DiscordChannel channel, DiscordUser author, TimeSpan? timeout = null,
            CancellationToken? ct = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var verifier = new MessageVerifier(this, predicate)
            {
                Channel = channel,
                Author = author
            };
            return _messageCreatedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        #endregion

        #region No predicate

        /// <summary>
        /// Returns a task that resolves to a message context containing the first new message the client receives,
        /// or <c>null</c> if the cancellation token <paramref name="ct"/> is canceled or the waiting period times out
        /// as specified by <paramref name="timeout"/>.
        /// </summary>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="MessageContext"/> containing the message, or <c>null</c>.
        /// </returns>
        public Task<MessageContext> WaitForMessageAsync(
            TimeSpan? timeout = null, CancellationToken? ct = null)
        {
            var verifier = new MessageVerifier(this, null);
            return _messageCreatedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a message context containing a message in channel
        /// <paramref name="channel"/>, or <c>null</c> if the cancellation token
        /// <paramref name="ct"/> is canceled or the waiting period times out as specified by
        /// <paramref name="timeout"/>.
        /// </summary>
        /// <param name="channel">Channel that the message must be sent in for it to be returned.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="MessageContext"/> containing the message, or <c>null</c>.
        /// </returns>
        public Task<MessageContext> WaitForMessageAsync(
            DiscordChannel channel, TimeSpan? timeout = null, CancellationToken? ct = null)
        {
            var verifier = new MessageVerifier(this, null)
            {
                Channel = channel
            };
            return _messageCreatedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a message context containing a message sent by <paramref name="author"/>,
        /// or <c>null</c> if the cancellation token <paramref name="ct"/> is canceled or the waiting period times out
        /// as specified by <paramref name="timeout"/>.
        /// </summary>
        /// <param name="author">User that must have sent the message for it to be returned.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="MessageContext"/> containing the message, or <c>null</c>.
        /// </returns>
        public Task<MessageContext> WaitForMessageAsync(
            DiscordUser author, TimeSpan? timeout = null, CancellationToken? ct = null)
        {
            var verifier = new MessageVerifier(this, null)
            {
                Author = author
            };
            return _messageCreatedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        /// <summary>
        /// Returns a task that resolves to a message context containing a message sent by <paramref name="author"/>
        /// in channel <paramref name="channel"/>, or <c>null</c> if the cancellation token <paramref name="ct"/> is
        /// canceled or the waiting period times out as specified by <paramref name="timeout"/>.
        /// </summary>
        /// <param name="channel">Channel that the message must be sent in for it to be returned.</param>
        /// <param name="author">User that must have sent the message for it to be returned.</param>
        /// <param name="timeout">Timeout for the waiting period. If not specified, defaults to
        /// <see cref="InteractivityConfiguration.Timeout"/>.</param>
        /// <param name="ct">Cancellation token that can be used to end the waiting period early.</param>
        /// <returns>Task that resolves to a new <see cref="MessageContext"/> containing the message, or <c>null</c>.
        /// </returns>
        public Task<MessageContext> WaitForMessageAsync(
            DiscordChannel channel, DiscordUser author, TimeSpan? timeout = null, CancellationToken? ct = null)
        {
            var verifier = new MessageVerifier(this, null)
            {
                Channel = channel,
                Author = author
            };
            return _messageCreatedVerifiers.HandleCancellableAsync(verifier, ct, timeout ?? Config.Timeout);
        }

        #endregion
    }
}