using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BehaviourTree.Demo.GameEngine
{
    public sealed class ImmutableCollection<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _inner;

        public ImmutableCollection(IEnumerable<T> inner)
        {
            _inner = inner;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _inner.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
