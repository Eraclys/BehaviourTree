using BehaviourTree.Composites;

namespace BehaviourTreeBuilder.Nodes
{
    public sealed class SimpleParallelNode : CompositeNode
    {
        public SimpleParallel.Policy Policy { get; }

        public SimpleParallelNode(SimpleParallel.Policy policy)
        {
            Policy = policy;
        }
    }
}