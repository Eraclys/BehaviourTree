namespace BehaviourTree
{
    public class RepeatUntilFail : IBehaviour
    {
        private readonly IBehaviour _behaviour;

        public RepeatUntilFail(IBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public BehaviourStatus Tick()
        {
            while (true)
            {
                var childStatus = _behaviour.Tick();

                if (childStatus == BehaviourStatus.Success)
                {
                    continue;
                }

                return childStatus;
            }
        }
    }
}
