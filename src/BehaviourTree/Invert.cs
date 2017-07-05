namespace BehaviourTree
{
    public sealed class Invert : IBehaviour
    {
        private readonly IBehaviour _child;

        public Invert(IBehaviour child)
        {
            _child = child;
        }

        public BehaviourStatus Tick()
        {
            var childStatus = _child.Tick();

            if (childStatus == BehaviourStatus.Success)
            {
                return BehaviourStatus.Failure;
            }

            if (childStatus == BehaviourStatus.Failure)
            {
                return BehaviourStatus.Success;
            }

            return childStatus;
        }
    }
}