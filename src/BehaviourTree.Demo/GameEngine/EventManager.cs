using System;
using System.Collections.Generic;

namespace BehaviourTree.Demo.GameEngine
{
    public sealed class EventManager : IEventManager
    {
        private readonly Engine _engine;
        private readonly Dictionary<Type, HashSet<object>> _eventListeners = new Dictionary<Type, HashSet<object>>();

        public EventManager(Engine engine)
        {
            _engine = engine;
        }

        public void PublishEvent<TEvent>(TEvent @event)
        {
            if (!_eventListeners.TryGetValue(typeof(TEvent), out var listeners))
            {
                return;
            }

            foreach (IEventListener<TEvent> listener in listeners)
            {
                listener.Handle(_engine, @event);
            }
        }

        public void SubscribeToEvent<TEvent>(IEventListener<TEvent> eventListener)
        {
            var eventType = typeof(TEvent);

            if (!_eventListeners.TryGetValue(eventType, out var listeners))
            {
                _eventListeners[eventType] = listeners = new HashSet<object>();
            }

            listeners.Add(eventListener);
        }

        public void UnsubscribeFromEvent<TEvent>(IEventListener<TEvent> eventListener)
        {
            var eventType = typeof(TEvent);

            if (_eventListeners.TryGetValue(eventType, out var listeners))
            {
                listeners.Remove(eventListener);
            }
        }
    }
}
