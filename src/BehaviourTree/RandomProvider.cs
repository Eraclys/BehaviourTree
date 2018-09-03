using System;

namespace BehaviourTree
{
    public sealed class RandomProvider : IRandomProvider
    {
        private static readonly Random Random = new Random();

        public double NextRandomDouble()
        {
            return Random.NextDouble();
        }
    }
}