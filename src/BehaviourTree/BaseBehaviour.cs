namespace BehaviourTree
{
    public abstract class BaseBehaviour : IBehaviour
    {
        private IBehaviourNode _parent;

        public virtual BehaviourStatus CurrentStatus { get; protected set; } = BehaviourStatus.Inactive;

        public void Start()
        {
            if (CurrentStatus != BehaviourStatus.Inactive)
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

        public void SetParent(IBehaviourNode parent)
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