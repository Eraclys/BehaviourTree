namespace BehaviourTree
{
    public sealed class Success : IBehaviour
    {
        private readonly IBehaviour _child;

        public Success(IBehaviour child)
        {
            _child = child;
        }

        public BehaviourStatus Tick()
        {
            var childStatus = _child.Tick();

            if (childStatus == BehaviourStatus.Success ||
                childStatus == BehaviourStatus.Failure)
            {
                return BehaviourStatus.Success;
            }

            return childStatus;
        }
    }
}
