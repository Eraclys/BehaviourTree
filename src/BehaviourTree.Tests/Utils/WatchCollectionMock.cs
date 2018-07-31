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

        public IBtBehaviour[] Behaviours => _children.Cast<IBtBehaviour>().ToArray();

        public int NbOfChildrenCalled => _children.Count(x => x.WasCalled);

        public bool AllChildrenCalled => NbOfChildrenCalled == _children.Length;
    }
}
