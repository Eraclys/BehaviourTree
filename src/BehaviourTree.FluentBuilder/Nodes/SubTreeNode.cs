namespace BehaviourTree.FluentBuilder.Nodes
{
    public sealed class SubTreeNode<TContext> : Node
    {
        public IBehaviour<TContext> SubBehaviour { get; }

        public SubTreeNode(IBehaviour<TContext> subBehaviour)
        {
            SubBehaviour = subBehaviour;
        }
    }
}
