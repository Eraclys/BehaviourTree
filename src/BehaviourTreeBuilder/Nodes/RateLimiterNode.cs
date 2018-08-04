namespace BehaviourTreeBuilder.Nodes
{
    public sealed class RateLimiterNode : DecoratorNode
    {
        public RateLimiterNode(int intervalInMilliseconds)
        {
            IntervalInMilliseconds = intervalInMilliseconds;
        }

        public int IntervalInMilliseconds { get; }
    }
}
