namespace BehaviourTreeBuilder.Nodes
{
    public sealed class WaitNode : Node
    {
        public int WaitTimeInMilliseconds { get; }

        public WaitNode(int waitTimeInMilliseconds)
        {
            WaitTimeInMilliseconds = waitTimeInMilliseconds;
        }
    }
}
