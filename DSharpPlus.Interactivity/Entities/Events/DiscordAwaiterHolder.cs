using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using DSharpPlus.EventArgs;

namespace DSharpPlus.Interactivity
{
    internal class DiscordAwaiterHolder<TMachine, TEventArgs, TContextResult>
        where TMachine : DiscordEventAwaiter<TEventArgs, TContextResult>
        where TEventArgs : DiscordEventArgs // not necessary, but is here for consistency
        where TContextResult : InteractivityContext
    {
        /// <summary>
        /// <p>
        ///     To be triggered with a <see cref="DiscordAwaiterHolder{TMachine,TEventArgs,TContextResult}"/> when the
        ///     event handler for the event representing that instance is to be registered or unregistered.
        /// </p>
        /// <p>
        ///     Implementations should add or remove an event handler pointing to
        ///     <see cref="DiscordAwaiterHolder{TMachine,TEventArgs,TContextResult}"/>.<see cref="DiscordAwaiterHolder{TMachine,TEventArgs,TContextResult}.HandleAsync"/>
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
        public delegate void SubscribeAction(DiscordAwaiterHolder<TMachine, TEventArgs, TContextResult> self);

        private readonly SubscribeAction _subscribe;
        private readonly SubscribeAction _unsubscribe;
        private readonly IList<TMachine> _eventHandlers = new List<TMachine>();
        
        private bool _isSubscribed;

        public DiscordAwaiterHolder(SubscribeAction subscribe, SubscribeAction unsubscribe)
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