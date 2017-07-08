namespace BehaviourTree
{
    public abstract class BaseBehaviour : IBehaviour
    {
        public virtual BehaviourStatus Status { get; protected set; }

        public BehaviourStatus Tick(ElaspedTicks elaspedTicks)
        {
            if (Status == BehaviourStatus.Ready || Status == BehaviourStatus.Running)
            {
                if (Status == BehaviourStatus.Ready)
                {
                    Status = BehaviourStatus.Running;
                }

                Status = DoTick(elaspedTicks);
            }

            return Status;
        }


        public void Reset()
        {
            if (Status == BehaviourStatus.Ready)
            {
                return;
            }

            Status = BehaviourStatus.Ready;
            DoReset();
        }

        protected abstract BehaviourStatus DoTick(ElaspedTicks elaspedTicks);
        protected abstract void DoReset();
    }
}