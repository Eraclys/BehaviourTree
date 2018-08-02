using System;

namespace BehaviourTree
{
    public abstract class BaseBehaviour : IBehaviour
    {
        private BehaviourStatus _status = BehaviourStatus.Ready;

        public BehaviourStatus Tick(BtContext context)
        {
            if (_status == BehaviourStatus.Ready)
            {
                OnInitialize();
            }

            _status = Update(context);

            if (_status == BehaviourStatus.Ready)
            {
                throw new InvalidOperationException("Ready status should not be returned by Behaviour Update Method");
            }

            if (_status != BehaviourStatus.Running)
            {
                OnTerminate(_status);
            }

            return _status;
        }

        public void Reset()
        {
            if (_status != BehaviourStatus.Ready)
            {
                DoReset(_status);
                _status = BehaviourStatus.Ready;
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