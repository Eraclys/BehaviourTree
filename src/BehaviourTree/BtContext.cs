using System;
using System.Collections.Concurrent;

namespace BehaviourTree
{
    public sealed class BtContext
    {
        private readonly IClock _clock;

        public BtContext() : this(new Clock())
        {
        }

        public BtContext(IClock clock)
        {
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }

        private readonly ConcurrentDictionary<string, object> _values = new ConcurrentDictionary<string, object>();

        public T GetValue<T>(string key)
        {
            return (T)_values[key];
        }

        public void Set<T>(string key, T value)
        {
            _values[key] = value;
        }

        public long GetTimeStamp()
        {
            return _clock.GetTimeStamp();
        }
    }
}
