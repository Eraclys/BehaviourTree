namespace BehaviourTree
{
    public sealed class Success : Decorator
    {
        public Success(IBehaviour child) : base(child)
        {
        }

        public override void OnChildStopped(IBehaviour child, bool success)
        {
            RaiseStopped(true);
        }
    }
}
