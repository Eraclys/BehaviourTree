using System.Collections.Concurrent;

namespace BehaviourTree
{
    public sealed class BtContext
    {
        private readonly ConcurrentDictionary<string, object> _values = new ConcurrentDictionary<string, object>();

        public T GetValue<T>(string key)
        {
            return (T)_values[key];
        }

        public void Set<T>(string key, T value)
        {
            _values[key] = value;
        }
    }
}
