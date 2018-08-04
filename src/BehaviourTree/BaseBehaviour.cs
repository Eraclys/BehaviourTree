using System;

namespace BehaviourTree
{
    public abstract class BaseBehaviour<TContext> : IBehaviour<TContext>
    {
        public string Name { get; }
        public BehaviourStatus Status { get; private set; } = BehaviourStatus.Ready;

        protected BaseBehaviour(string name)
        {
            Name = name;
        }

        public BehaviourStatus Tick(TContext context)
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

        protected abstract BehaviourStatus Update(TContext context);

        protected virtual void OnTerminate(BehaviourStatus status)
        {
        }

        protected virtual void OnInitialize()
        {
        }

        protected virtual void DoReset(BehaviourStatus status)
        {
        }

        public virtual void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<BaseBehaviour<TContext>> typedVisitor)
            {
                typedVisitor.Visit(this);
            }
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