namespace BehaviourTree
{
    public abstract class Decorator : BaseBehaviour, IParentBehaviour
    {
        private readonly IBehaviour _child;

        protected Decorator(IBehaviour child)
        {
            _child = child;
            _child.SetParent(this);
        }

        protected override void DoStart()
        {
            _child.Start();
        }

        protected override void DoStop()
        {
            _child.Stop();
        }

        public abstract void OnChildStopped(IBehaviour child, bool success);
    }
}