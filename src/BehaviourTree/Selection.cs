namespace BehaviourTree
{
    public sealed class Selection : IBehaviour
    {
        private readonly IBehaviour[] _children;

        public Selection(params IBehaviour[] children)
        {
            _children = children;
        }

        public BehaviourStatus Tick()
        {
            for (var index = 0; index < _children.Length; index++)
            {
                var child = _children[index];
                var childStatus = child.Tick();

                if (childStatus != BehaviourStatus.Failure)
                {
                    return childStatus;
                }
            }

            return BehaviourStatus.Failure;
        }
    }
}
