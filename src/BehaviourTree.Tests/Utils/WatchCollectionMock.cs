using System.Collections.Generic;
using System.Linq;

namespace BehaviourTree.Tests.Utils
{
    internal sealed class WatchCollectionMock
    {
        private readonly IList<WatchMock> _children = new List<WatchMock>();

        public WatchCollectionMock(params IBehaviour[] children)
        {
            foreach (var child in children)
            {
                _children.Add(new WatchMock(child));
            }
        }

        public IBehaviour[] Behaviours => _children.Cast<IBehaviour>().ToArray();

        public int NbOfChildrenCalled => _children.Sum(x => x.OnChildStoppedCount);

        public bool AllChildrenCalled => NbOfChildrenCalled == _children.Count;
    }
}