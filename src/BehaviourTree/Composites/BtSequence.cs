namespace BehaviourTree.Composites
{
    public sealed class BtSequence : BaseBtComposite
    {
        public BtSequence(params IBtBehaviour[] children) : base(children){}

        protected override BehaviourStatus DoTick(BtContext context)
        {
            for (var index = 0; index < Children.Length; index++)
            {
                var childStatus = Children[index].Tick(context);

                if (childStatus != BehaviourStatus.Succeeded)
                {
                    return childStatus;
                }
            }

            return BehaviourStatus.Succeeded;
        }
    }
}