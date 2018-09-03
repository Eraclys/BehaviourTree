namespace BehaviourTree.FluentBuilder.Nodes
{
    public sealed class RandomNode : DecoratorNode
    {
        public RandomNode(double threshold)
        {
            Threshold = threshold;
        }

        public double Threshold { get; }
    }
}
