using System;

namespace BehaviourTree.Composites
{
    public sealed class PrioritySequence : CompositeBehaviour
    {
        public PrioritySequence(IBehaviour[] children) : base(children)
        {
        }

        protected override BehaviourStatus Update(BtContext context)
        {
            for (int i = 0; i < Children.Length; i++)
            {
                var childStatus = Children[i].Tick(context);

                if (childStatus != BehaviourStatus.Succeeded)
                {
                    return childStatus;
                }
            }

            return BehaviourStatus.Succeeded;
        }
    }
}
