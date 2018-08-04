using BehaviourTree.Composites;

namespace BehaviourTreeBuilder.Nodes
{
    public sealed class SimpleParallelNode : CompositeNode
    {
        public SimpleParallelPolicy Policy { get; }

        public SimpleParallelNode(SimpleParallelPolicy policy)
        {
            Policy = policy;
        }
    }
}