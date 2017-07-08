namespace BehaviourTree.Composites
{
    public sealed class BtSequence : BaseBtComposite
    {
        public BtSequence(params IBtBehaviour[] children) : base(children){}

        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks)
        {
            for (var index = 0; index < Children.Length; index++)
            {
                var child = Children[index];
                var childStatus = child.Tick(elaspedTicks);

                if (childStatus != BehaviourStatus.Succeeded)
                {
                    return childStatus;
                }
            }

            return BehaviourStatus.Succeeded;
        }
    }
}