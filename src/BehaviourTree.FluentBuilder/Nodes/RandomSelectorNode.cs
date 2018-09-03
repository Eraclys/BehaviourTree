namespace BehaviourTree.FluentBuilder.Nodes
{
    public sealed class RandomSelectorNode : CompositeNode
    {
        public IRandomProvider RandomProvider { get; }

        public RandomSelectorNode(IRandomProvider randomProvider)
        {
            RandomProvider = randomProvider;
        }
    }
}