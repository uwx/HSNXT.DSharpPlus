using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using ConcurrentCollections;
using DSharpPlus.EventArgs;

namespace DSharpPlus.Interactivity
{
    internal class AwaiterHolder<TMachine, TEventArgs, TContextResult>
        where TMachine : DiscordEventAwaiter<TEventArgs, TContextResult>
        where TEventArgs : DiscordEventArgs // not necessary, but is here for consistency
        where TContextResult : InteractivityContext
    {
        /// <summary>
        /// <p>
        ///     To be triggered with a <see cref="AwaiterHolder{TMachine,TEventArgs,TContextResult}"/> when the
        ///     event handler for the event representing that instance is to be registered or unregistered.
        /// </p>
        /// <p>
        ///     Implementations should add or remove an event handler pointing to
        ///     <see cref="AwaiterHolder{TMachine,TEventArgs,TContextResult}"/>.<see cref="AwaiterHolder{TMachine,TEventArgs,TContextResult}.HandleAsync"/>
        /// </p>
        /// </summary>
        /// <param name="self">The instance.</param>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// new DiscordAwaiterHolder<Machine, EventArgs, ContextResult>(
        ///     ev => obj.Event += ev.Trigger,
        ///     ev => obj.Event -= ev.Trigger
        /// );
        /// ]]>
        /// </code>
        /// </example>
        public delegate void SubscribeAction(AwaiterHolder<TMachine, TEventArgs, TContextResult> self);

        private readonly SubscribeAction _subscribe;
        private readonly SubscribeAction _unsubscribe;
        private readonly ConcurrentHashSet<TMachine> _eventHandlers = new ConcurrentHashSet<TMachine>();

        private bool _isSubscribed;

        public AwaiterHolder(SubscribeAction subscribe, SubscribeAction unsubscribe)
        {
            _subscribe = subscribe;
            _unsubscribe = unsubscribe;
        }

        // TODO locking?
        public async Task Trigger(TEventArgs e)
        {
            if (!_isSubscribed)
            {
                throw new InvalidAsynchronousStateException(
                    $"Didn't call {nameof(_unsubscribe)} when all event handlers are removed, " +
                    $"or {nameof(_isSubscribed)} was updated before calling it");
            }

            foreach (var handler in _eventHandlers)
            {
                try
                {
                    await handler.MoveNext(e);
                }
                catch (Exception ex)
                {
                    // raise the exception in the stack frame that awaited Resolve() instead of the event handler here
                    handler.Result.SetException(ex);
                }
            }
        }

        public void Subscribe(TMachine handler)
        {
            _eventHandlers.Add(handler);

            // ReSharper disable once InvertIf
            if (!_isSubscribed)
            {
                _isSubscribed = true;
                _subscribe(this);
            }
        }

        public void Unsubscribe(TMachine handler)
        {
            if (!_isSubscribed)
            {
                throw new ArgumentException("There are no events to unsubscribe", nameof(handler));
            }

            if (!UnsubscribeLazy(handler))
            {
                throw new ArgumentException("Could not find event to remove", nameof(handler));
            }
        }

        // unsubscribe without erroring if already unsubscribed
        private bool UnsubscribeLazy(TMachine handler)
        {
            var success = _eventHandlers.TryRemove(handler);

            // ReSharper disable once InvertIf
            if (_eventHandlers.Count == 0)
            {
                _unsubscribe(this);
                _isSubscribed = false;
            }

            return success;
        }

        public Task<TContextResult> HandleAsync(TMachine handler)
        {
            Subscribe(handler);
            return handler.Result.Task;
        }

        public async Task<TContextResult> HandleCancellableAsync(
            TMachine handler, CancellationToken? ct, TimeSpan timeout)
        {
            Subscribe(handler);
            try
            {
                using (var cts = ct.HasValue 
                    ? CancellationTokenSource.CreateLinkedTokenSource(ct.Value)
                    : new CancellationTokenSource()) // if no cancellation token provider just do an empty CTS
                {
                    // append timeout to the cancellation token source
                    cts.CancelAfter(timeout);

                    // this callback will be executed when token is cancelled
                    void OnCancel()
                    {
                        handler.Result.TrySetResult(null);
                    }

                    // `using` block means the registration handle is disposed when we return
                    using (cts.Token.Register(OnCancel))
                    {
                        // return await so it doesn't dispose before the task completes
                        return await handler.Result.Task;
                    }
                }
            }
            finally
            {
                // cancelling won't unsubscribe the handler, so we have to do it manually
                UnsubscribeLazy(handler);
            }
        }
    }
}