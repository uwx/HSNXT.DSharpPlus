using System;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace DSharpPlus.Interactivity
{
    public partial class InteractivityExtension
    {
		public Task<ReactionContext> WaitForReactionAsync(
			Func<DiscordEmoji, bool> predicate, TimeSpan? timeout = null, CancellationTokenSource cts = null)
		{
			if (predicate == null)
				throw new ArgumentNullException(nameof(predicate));

			var verifier = new ReactionVerifier(this, predicate);
			return _reactionAddedVerifiers.HandleCancellableAsync(verifier, cts, timeout ?? Config.Timeout);
		}

		public Task<ReactionContext> WaitForReactionAsync(
			Func<DiscordEmoji, bool> predicate, DiscordChannel channel, TimeSpan? timeout = null, CancellationTokenSource cts = null)
		{
			if (predicate == null)
				throw new ArgumentNullException(nameof(predicate));

			var verifier = new ReactionVerifier(this, predicate)
			{
				Channel = channel
			};
			return _reactionAddedVerifiers.HandleCancellableAsync(verifier, cts, timeout ?? Config.Timeout);
		}

		public Task<ReactionContext> WaitForReactionAsync(
			Func<DiscordEmoji, bool> predicate, DiscordUser author, TimeSpan? timeout = null, CancellationTokenSource cts = null)
		{
			if (predicate == null)
				throw new ArgumentNullException(nameof(predicate));

			var verifier = new ReactionVerifier(this, predicate)
			{
				User = author
			};
			return _reactionAddedVerifiers.HandleCancellableAsync(verifier, cts, timeout ?? Config.Timeout);
		}

		public Task<ReactionContext> WaitForReactionAsync(
			Func<DiscordEmoji, bool> predicate, DiscordChannel channel, DiscordUser author, TimeSpan? timeout = null, CancellationTokenSource cts = null)
		{
			if (predicate == null)
				throw new ArgumentNullException(nameof(predicate));

			var verifier = new ReactionVerifier(this, predicate)
			{
				Channel = channel,
				User = author
			};
			return _reactionAddedVerifiers.HandleCancellableAsync(verifier, cts, timeout ?? Config.Timeout);
		}
    }
}