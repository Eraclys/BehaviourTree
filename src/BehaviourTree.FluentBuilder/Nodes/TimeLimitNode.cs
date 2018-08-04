namespace BehaviourTree.FluentBuilder.Nodes
{
    public sealed class TimeLimitNode : DecoratorNode
    {
        public int TimeLimitInMilliseconds { get; }

        public TimeLimitNode(int timeLimitInMilliseconds)
        {
            TimeLimitInMilliseconds = timeLimitInMilliseconds;
        }
    }
}
