using System;

namespace BehaviourTree
{
    public abstract class BaseBtBehaviour : IBtBehaviour
    {
        public virtual BehaviourStatus Status { get; protected set; }

        public BehaviourStatus Tick(ElaspedTicks elaspedTicks, BtContext context)
        {
            if (Status == BehaviourStatus.Ready || Status == BehaviourStatus.Running)
            {
                if (Status == BehaviourStatus.Ready)
                {
                    Status = BehaviourStatus.Running;
                }

                Status = DoTick(elaspedTicks, context);
            }

            return Status;
        }


        public void Reset()
        {
            if (Status == BehaviourStatus.Ready)
            {
                return;
            }

            DoReset();
            Status = BehaviourStatus.Ready;
        }

        protected abstract BehaviourStatus DoTick(ElaspedTicks elaspedTicks, BtContext context);
        protected abstract void DoReset();

        protected abstract void Dispose(bool disposing);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseBtBehaviour()
        {
            Dispose(false);
        }
    }
}