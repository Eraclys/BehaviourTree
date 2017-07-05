using System;
using System.Collections.Generic;

namespace BehaviourTree
{
    public sealed class RandomSelection : IBehaviour
    {
        private static readonly Random Random = new Random();

        private readonly IBehaviour[] _children;

        public RandomSelection(params IBehaviour[] children)
        {
            _children = children;
        }

        public BehaviourStatus Tick()
        {
            var choices = new List<IBehaviour>(_children);

            while (choices.Count > 0)
            {
                var nextChoiceIndex = Random.Next(0, choices.Count);
                var childStatus = choices[nextChoiceIndex].Tick();
                choices.RemoveAt(nextChoiceIndex);

                if (childStatus != BehaviourStatus.Failure)
                {
                    return childStatus;
                }
            }

            return BehaviourStatus.Failure;
        }
    }
}