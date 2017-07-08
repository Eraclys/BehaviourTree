using System.Linq;

namespace BehaviourTree.Tests.Utils
{
    internal sealed class WatchCollectionMock
    {
        private readonly MockBehaviour[] _children;

        public WatchCollectionMock(params MockBehaviour[] children)
        {
            _children = children;
        }

        public IBehaviour[] Behaviours => _children.Cast<IBehaviour>().ToArray();

        public int NbOfChildrenCalled => _children.Count(x => x.Status != BehaviourStatus.Ready);

        public bool AllChildrenCalled => NbOfChildrenCalled == _children.Length;
    }
}
