using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.EventArgs;

namespace HSNXT.DSharpPlus.InteractivityNext
{
    /// <summary>
    /// <p>An extension of <see cref="AwaiterHolder{TMachine,TEventArgs,TContextResult}"/> that, instead of hooking a
    /// single event, hooks the 3 main reaction-related events at once. As such, usages need to provide all 3 events.
    /// This is used for collecting reactions, as they need to process not only added reactions but also removed and
    /// cleared ones.</p>
    ///
    /// <p>Importantly, pending hooks for this class will never finish in the conventional way; as it is
    /// intended for indefinitely long-running operations. The awaiter task completes when either the passed
    /// cancellation token is triggered or when the timeout is reached.</p>
    /// </summary>
    internal class ReactionCancellationAwaiterHolder
    {
        private readonly InteractivityExtension _interactivity;
        private readonly AwaiterHolder<CancelVerifier, DiscordEventArgs, InteractivityContext> _holder;

        // wraps around a handler function and calls it with generic event args; never returns success on checkresult
        private class CancelVerifier : DiscordEventAwaiter<DiscordEventArgs, InteractivityContext>
        {
            private readonly Func<DiscordEventArgs, Task> _handler;

            public CancelVerifier(InteractivityExtension interactivity, Func<DiscordEventArgs, Task> handler)
                : base(interactivity)
            {
                _handler = handler;
            }

            protected override async Task<InteractivityContext> CheckResult(DiscordEventArgs args)
            {
                await _handler(args);
                return null; // never success, so only unsubscribe when cancelled
            }
        }

        public ReactionCancellationAwaiterHolder(InteractivityExtension interactivity)
        {
            _interactivity = interactivity;

            void Subscribe(AwaiterHolder<CancelVerifier, DiscordEventArgs, InteractivityContext> ev)
            {
                interactivity.Client.MessageReactionAdded += ev.Trigger;
                interactivity.Client.MessageReactionRemoved += ev.Trigger;
                interactivity.Client.MessageReactionsCleared += ev.Trigger;
            }

            void Unsubscribe(AwaiterHolder<CancelVerifier, DiscordEventArgs, InteractivityContext> ev)
            {
                interactivity.Client.MessageReactionAdded -= ev.Trigger;
                interactivity.Client.MessageReactionRemoved -= ev.Trigger;
                interactivity.Client.MessageReactionsCleared -= ev.Trigger;
            }

            _holder = new AwaiterHolder<CancelVerifier, DiscordEventArgs, InteractivityContext>(Subscribe, Unsubscribe);
        }

        public Task HandleCancellableVoidAsync(
            Func<MessageReactionAddEventArgs, Task> addHandler,
            Func<MessageReactionRemoveEventArgs, Task> removeHandler,
            Func<MessageReactionsClearEventArgs, Task> clearHandler,
            CancellationToken? ct, TimeSpan timeout)
        {
            return _holder.HandleCancellableAsync(new CancelVerifier(_interactivity, args =>
            {
                switch (args)
                {
                    case MessageReactionAddEventArgs addArgs:
                        return addHandler(addArgs);
                    case MessageReactionRemoveEventArgs removeArgs:
                        return removeHandler(removeArgs);
                    case MessageReactionsClearEventArgs clearArgs:
                        return clearHandler(clearArgs);
                    default:
                        return Task.CompletedTask;
                }
            }), ct, timeout);
        }

        public async Task<ReactionCollectionContext> HandleCancellableAsync(
            Func<ReactionCollectionContext, MessageReactionAddEventArgs, Task> addHandler,
            Func<ReactionCollectionContext, MessageReactionRemoveEventArgs, Task> removeHandler,
            Func<ReactionCollectionContext, MessageReactionsClearEventArgs, Task> clearHandler, 
            CancellationToken? ct, TimeSpan timeout)
        {
            var collection = new ReactionCollectionContext(_interactivity);
            
            await _holder.HandleCancellableAsync(new CancelVerifier(_interactivity, args =>
            {
                switch (args)
                {
                    case MessageReactionAddEventArgs addArgs:
                        return addHandler(collection, addArgs);
                    case MessageReactionRemoveEventArgs removeArgs:
                        return removeHandler(collection, removeArgs);
                    case MessageReactionsClearEventArgs clearArgs:
                        return clearHandler(collection, clearArgs);
                    default:
                        return Task.CompletedTask;
                }
            }), ct, timeout);

            return collection;
        }
    }
}