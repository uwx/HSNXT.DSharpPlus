using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DSharpPlus.Interactivity
{
    internal class VerifyEventCollection<T, TEventArgs> : ISubscribable<T> where T : IAsyncVerifyMachine<TEventArgs>
    {
        private readonly Action<VerifyEventCollection<T, TEventArgs>> _subscribe;
        private readonly Action<VerifyEventCollection<T, TEventArgs>> _unsubscribe;
        private readonly IList<T> _eventHandlers = new List<T>();
        private bool _isSubscribed = false;

        public VerifyEventCollection(
            Action<VerifyEventCollection<T, TEventArgs>> subscribe, 
            Action<VerifyEventCollection<T, TEventArgs>> unsubscribe
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

        public void Subscribe(T handler)
        {
            _eventHandlers.Add(handler);
			
            // ReSharper disable once InvertIf
            if (!_isSubscribed)
            {
                _isSubscribed = true;
                _subscribe(this);
            }
        }

        public void Unsubscribe(T handler)
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
    }
}