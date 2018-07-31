namespace BehaviourTree.Composites
{
    public sealed class BtPrioritySelection : BaseBtComposite
    {
        public BtPrioritySelection(params IBtBehaviour[] children) : base(children) { }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            for (var index = 0; index < Children.Length; index++)
            {
                var child = Children[index];
                var childStatus = child.Tick(context);

                if (childStatus != BehaviourStatus.Failed)
                {
                    // reset further nodes
                    for (var j = index + 1; j < Children.Length; j++)
                    {
                        Children[j].Reset();
                    }

                    return childStatus;
                }

                // reset on fail since we always want to re-evaluate
                child.Reset();
            }

            return BehaviourStatus.Failed;
        }
    }
}
