using BehaviourTree.Composites;

namespace BehaviourTree.FluentBuilder.Nodes
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