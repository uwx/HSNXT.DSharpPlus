using System;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace DSharpPlus.Interactivity
{
    public partial class InteractivityExtension
    {
		public Task<MessageContext> WaitForMessageAsync(
			Func<DiscordMessage, bool> predicate, TimeSpan? timeout = null, CancellationTokenSource cts = null)
		{
			if (predicate == null)
				throw new ArgumentNullException(nameof(predicate));

			var verifier = new MessageVerifier(this, predicate);
			return _messageCreatedVerifiers.HandleCancellableAsync(verifier, cts, timeout ?? Config.Timeout);
		}

		public Task<MessageContext> WaitForMessageAsync(
			Func<DiscordMessage, bool> predicate, DiscordChannel channel, TimeSpan? timeout = null, CancellationTokenSource cts = null)
		{
			if (predicate == null)
				throw new ArgumentNullException(nameof(predicate));

			var verifier = new MessageVerifier(this, predicate)
			{
				Channel = channel
			};
			return _messageCreatedVerifiers.HandleCancellableAsync(verifier, cts, timeout ?? Config.Timeout);
		}

		public Task<MessageContext> WaitForMessageAsync(
			Func<DiscordMessage, bool> predicate, DiscordUser author, TimeSpan? timeout = null, CancellationTokenSource cts = null)
		{
			if (predicate == null)
				throw new ArgumentNullException(nameof(predicate));

			var verifier = new MessageVerifier(this, predicate)
			{
				Author = author
			};
			return _messageCreatedVerifiers.HandleCancellableAsync(verifier, cts, timeout ?? Config.Timeout);
		}

		public Task<MessageContext> WaitForMessageAsync(
			Func<DiscordMessage, bool> predicate, DiscordChannel channel, DiscordUser author, TimeSpan? timeout = null, CancellationTokenSource cts = null)
		{
			if (predicate == null)
				throw new ArgumentNullException(nameof(predicate));

			var verifier = new MessageVerifier(this, predicate)
			{
				Channel = channel,
				Author = author
			};
			return _messageCreatedVerifiers.HandleCancellableAsync(verifier, cts, timeout ?? Config.Timeout);
		}
    }
}