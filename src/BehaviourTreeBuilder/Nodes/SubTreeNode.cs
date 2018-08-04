using BehaviourTree;

namespace BehaviourTreeBuilder.Nodes
{
    public sealed class SubTreeNode : Node
    {
        public IBehaviour SubBehaviour { get; }

        public SubTreeNode(IBehaviour subBehaviour)
        {
            SubBehaviour = subBehaviour;
        }
    }
}
