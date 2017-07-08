using System.Linq;

namespace BehaviourTree.Tests.Utils
{
    internal sealed class WatchCollectionMock
    {
        private readonly MockBtBehaviour[] _children;

        public WatchCollectionMock(params MockBtBehaviour[] children)
        {
            _children = children;
        }

        public IBtBehaviour<MockContext>[] Behaviours => _children.Cast<IBtBehaviour<MockContext>>().ToArray();

        public int NbOfChildrenCalled => _children.Count(x => x.Status != BehaviourStatus.Ready);

        public bool AllChildrenCalled => NbOfChildrenCalled == _children.Length;
    }
}
