namespace BehaviourTree.FluentBuilder.Nodes
{
    public sealed class RandomNode : DecoratorNode
    {
        public RandomNode(double threshold, IRandomProvider randomProvider)
        {
            Threshold = threshold;
            RandomProvider = randomProvider;
        }

        public double Threshold { get; }
        public IRandomProvider RandomProvider { get; }
    }
}
