namespace BehaviourTree
{
    public abstract class BaseBehaviour : IBehaviour
    {
        private IParentBehaviour _parent;

        public BehaviourStatus CurrentStatus { get; private set; } = BehaviourStatus.Inactive;

        public void Start()
        {
            if (CurrentStatus == BehaviourStatus.Active)
            {
                return;
            }

            CurrentStatus = BehaviourStatus.Active;

            DoStart();
        }

        public void Stop()
        {
            if (CurrentStatus != BehaviourStatus.Active)
            {
                return;
            }

            CurrentStatus = BehaviourStatus.StopRequested;

            DoStop();
        }

        protected abstract void DoStart();
        protected abstract void DoStop();

        public void SetParent(IParentBehaviour parent)
        {
            _parent = parent;
        }

        protected void RaiseStopped(bool success)
        {
            CurrentStatus = BehaviourStatus.Inactive;

            _parent?.OnChildStopped(this, success);
        }
    }
}