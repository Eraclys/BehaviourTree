namespace BehaviourTree.Composites
{
    public sealed class BtSelection : BaseBtComposite
    {
        public BtSelection(params IBtBehaviour[] children) : base(children){}

        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks)
        {
            for (var index = 0; index < Children.Length; index++)
            {
                var child = Children[index];
                var childStatus = child.Tick(elaspedTicks);

                if (childStatus != BehaviourStatus.Failed)
                {
                    return childStatus;
                }
            }

            return BehaviourStatus.Failed;
        }
    }
}
