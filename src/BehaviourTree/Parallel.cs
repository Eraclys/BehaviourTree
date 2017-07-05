namespace BehaviourTree
{
    public sealed class Parallel : IBehaviour
    {
        private readonly int _nbRequiredToSuccess;
        private readonly int _nbRequiredToFail;
        private readonly IBehaviour[] _children;

        public Parallel(params IBehaviour[] children)
            : this(children.Length, 1, children)
        {
        }

        public Parallel(int nbRequiredToSuccess, int nbRequiredToFail, params IBehaviour[] children)
        {
            _nbRequiredToSuccess = nbRequiredToSuccess;
            _nbRequiredToFail = nbRequiredToFail;
            _children = children;
        }

        public BehaviourStatus Tick()
        {
            var successCount = 0;
            var failureCount = 0;

            for (var index = 0; index < _children.Length; index++)
            {
                var child = _children[index];
                var childStatus = child.Tick();

                if (childStatus == BehaviourStatus.Success)
                {
                    successCount++;
                }

                if (childStatus == BehaviourStatus.Failure)
                {
                    failureCount++;
                }
            }

            if (successCount >= _nbRequiredToSuccess)
            {
                return BehaviourStatus.Success;
            }

            if (failureCount >= _nbRequiredToFail)
            {
                return BehaviourStatus.Success;
            }

            return BehaviourStatus.Running;
        }
    }
}