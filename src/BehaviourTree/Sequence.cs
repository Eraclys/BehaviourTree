namespace BehaviourTree
{
    public sealed class Sequence : IBehaviour
    {
        private readonly IBehaviour[] _children;

        public Sequence(params IBehaviour[] children)
        {
            _children = children;
        }

        public BehaviourStatus Tick()
        {
            for (var index = 0; index < _children.Length; index++)
            {
                var child = _children[index];
                var childStatus = child.Tick();

                if (childStatus != BehaviourStatus.Success)
                {
                    return childStatus;
                }
            }

            return BehaviourStatus.Success;
        }
    }
}