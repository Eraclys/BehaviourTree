namespace BehaviourTree
{
    public sealed class Invert : Decorator
    {
        public Invert(IBehaviour child) : base(child)
        {
        }

        public override void OnChildStopped(IBehaviour child, bool success)
        {
            RaiseStopped(!success);
        }
    }
}