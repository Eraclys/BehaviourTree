namespace BehaviourTree
{
    public abstract class Composite : BaseBehaviour, IBehaviourNode
    {
        protected IBehaviour[] Children { get; }

        protected Composite(IBehaviour[] children)
        {
            Children = children;

            foreach (var child in children)
            {
                child.SetParent(this);
            }
        }

        public abstract void OnChildStopped(IBehaviour child, bool success);
    }
}