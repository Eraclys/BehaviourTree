using System;

namespace BehaviourTree
{
    public abstract class BaseBehaviour : IBehaviour
    {
        protected string Name;
        protected BehaviourStatus Status { get; private set; } = BehaviourStatus.Ready;

        protected BaseBehaviour(string name)
        {
            Name = name;
        }

        public BehaviourStatus Tick(BtContext context)
        {
            if (Status == BehaviourStatus.Ready)
            {
                OnInitialize();
            }

            Status = Update(context);

            if (Status == BehaviourStatus.Ready)
            {
                throw new InvalidOperationException("Ready status should not be returned by Behaviour Update Method");
            }

            if (Status != BehaviourStatus.Running)
            {
                OnTerminate(Status);
            }

            return Status;
        }

        public void Reset()
        {
            if (Status != BehaviourStatus.Ready)
            {
                DoReset(Status);
                Status = BehaviourStatus.Ready;
            }
        }

        protected abstract BehaviourStatus Update(BtContext context);

        protected virtual void OnTerminate(BehaviourStatus status)
        {
        }

        protected virtual void OnInitialize()
        {
        }

        protected virtual void DoReset(BehaviourStatus status)
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