using System;

namespace BehaviourTree
{
    public abstract class BaseBtBehaviour : IBtBehaviour
    {
        public virtual BehaviourStatus Status { get; protected set; }

        public virtual BehaviourStatus Tick(BtContext context)
        {
            if (Status == BehaviourStatus.Ready)
            {
                Status = BehaviourStatus.Running;
            }

            if (Status == BehaviourStatus.Running)
            {
                Status = DoTick(context);
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

        protected abstract BehaviourStatus DoTick(BtContext context);

        protected virtual void DoReset()
        {
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}