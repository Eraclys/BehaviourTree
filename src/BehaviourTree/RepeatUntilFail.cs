namespace BehaviourTree
{
    public class RepeatUntilFail : Decorator
    {
        public RepeatUntilFail(IBehaviour child) : base(child)
        {
        }

        public override void OnChildStopped(IBehaviour child, bool success)
        {
            if (success)
            {
                child.Start();
            }
            else
            {
                RaiseStopped(false);
            }
        }
    }
}
