namespace BehaviourTree.Composites
{
    public sealed class PrioritySelector : CompositeBehaviour
    {
        public PrioritySelector(IBehaviour[] children) : base(children)
        {
        }

        protected override BehaviourStatus Update(BtContext context)
        {
            for (int i = 0; i < Children.Length; i++)
            {
                var childStatus = Children[i].Tick(context);

                if (childStatus != BehaviourStatus.Failed)
                {
                    return childStatus;
                }
            }

            return BehaviourStatus.Failed;
        }
    }
}
