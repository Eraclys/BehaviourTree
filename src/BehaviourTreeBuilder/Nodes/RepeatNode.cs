namespace BehaviourTreeBuilder.Nodes
{
    public sealed class RepeatNode : DecoratorNode
    {
        public int RepeatCount { get; }

        public RepeatNode(int repeatCount)
        {
            RepeatCount = repeatCount;
        }
    }
}
