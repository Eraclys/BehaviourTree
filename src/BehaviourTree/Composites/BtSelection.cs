namespace BehaviourTree.Composites
{
    public sealed class BtSelection<TContext> : BaseBtComposite<TContext>
    {
        public BtSelection(params IBtBehaviour<TContext>[] children) : base(children){}

        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks, TContext context)
        {
            for (var index = 0; index < Children.Length; index++)
            {
                var child = Children[index];
                var childStatus = child.Tick(elaspedTicks, context);

                if (childStatus != BehaviourStatus.Failed)
                {
                    return childStatus;
                }
            }

            return BehaviourStatus.Failed;
        }
    }
}
