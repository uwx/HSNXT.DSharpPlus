using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using DSharpPlus.EventArgs;

namespace DSharpPlus.Interactivity
{
    internal class DiscordAwaiterHolder<TMachine, TEventArgs, TContextResult> : ISubscribable<TMachine> 
        where TMachine : DiscordEventAwaiter<TEventArgs, TContextResult>
        where TEventArgs : DiscordEventArgs // not necessary, but is here for consistency
        where TContextResult : InteractivityContext
    {
        public delegate void ChangeSubscription(DiscordAwaiterHolder<TMachine, TEventArgs, TContextResult> self);

        private readonly ChangeSubscription _subscribe;
        private readonly ChangeSubscription _unsubscribe;
        private readonly IList<TMachine> _eventHandlers = new List<TMachine>();
        private bool _isSubscribed = false;

        public DiscordAwaiterHolder(
            ChangeSubscription subscribe, 
            ChangeSubscription unsubscribe
        )
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
                    "Didn't call _unsubscribe when all event handlers are removed, or _isSubscribed was updated " +
                    "before calling it");
            }
			
            for (var i = _eventHandlers.Count - 1; i >= 0; i--)
            {
                var eventHandler = _eventHandlers[i];
                if (await eventHandler.MoveNext(e))
                {
                    _eventHandlers.RemoveAt(i);
                }
            }

            if (_eventHandlers.Count == 0)
            {
                _unsubscribe(this);
                _isSubscribed = false;
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

            if (!_eventHandlers.Remove(handler))
            {
                throw new ArgumentException("Could not find event to remove", nameof(handler));
            }
			
            // ReSharper disable once InvertIf
            if (_eventHandlers.Count == 0)
            {
                _unsubscribe(this);
                _isSubscribed = false;
            }
        }

        public Task<TContextResult> HandleAsync(TMachine handler)
        {
            Subscribe(handler);
            return handler.Resolve();
        }
    }
}