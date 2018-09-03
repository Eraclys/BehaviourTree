namespace BehaviourTree.FluentBuilder.Nodes
{
    public sealed class RandomSequenceNode : CompositeNode
    {
        public IRandomProvider RandomProvider { get; }

        public RandomSequenceNode(IRandomProvider randomProvider)
        {
            RandomProvider = randomProvider;
        }
    }
}
