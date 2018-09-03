using System;

namespace BehaviourTree.Decorators
{
    public sealed class Random<TContext> : DecoratorBehaviour<TContext>
    {
        private readonly IRandomProvider _randomProvider;

        public Random(IBehaviour<TContext> child, double threshold, IRandomProvider randomProvider = null)
            : this("Random", child, threshold, randomProvider)
        {
        }

        public Random(string name, IBehaviour<TContext> child, double threshold, IRandomProvider randomProvider = null) : base(name, child)
        {
            if (threshold <= 0 || threshold > 1)
            {
                throw new ArgumentException(
                    "Threshold value must be between 0 (exclusive) and 100 (inclusive)",
                    nameof(threshold));
            }

            _randomProvider = randomProvider ?? RandomProvider.Default;

            Threshold = threshold;
        }

        public double Threshold { get; }

        protected override BehaviourStatus Update(TContext context)
        {
            var randomValue = _randomProvider.NextRandomDouble();

            if (randomValue >= Threshold)
            {
                return Child.Tick(context);
            }

            return BehaviourStatus.Failed;
        }
    }
}
