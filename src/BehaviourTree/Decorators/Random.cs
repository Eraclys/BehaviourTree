using System;

namespace BehaviourTree.Decorators
{
    public sealed class Random<TContext> : DecoratorBehaviour<TContext> where TContext : IRandomProvider
    {
        public Random(IBehaviour<TContext> child, double threshold) : this("Random", child, threshold)
        {
        }

        public Random(string name, IBehaviour<TContext> child, double threshold) : base(name, child)
        {
            if (threshold <= 0 || threshold > 1)
            {
                throw new ArgumentException(
                    "Threshold value must be between 0 (exclusive) and 100 (inclusive)",
                    nameof(threshold));
            }

            Threshold = threshold;
        }

        public double Threshold { get; }

        protected override BehaviourStatus Update(TContext context)
        {
            var randomValue = context.NextRandomDouble();

            if (randomValue >= Threshold)
            {
                return Child.Tick(context);
            }

            return BehaviourStatus.Failed;
        }
    }
}
