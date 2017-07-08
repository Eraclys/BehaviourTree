namespace BehaviourTree.Decorators
{
    public sealed class BtNot<TContext> : BaseBtDecorator<TContext>
    {
        public BtNot(IBtBehaviour<TContext> child) : base(child) { }

        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks, TContext context)
        {
            var childStatus = Child.Tick(elaspedTicks, context);

            if (childStatus == BehaviourStatus.Succeeded)
            {
                return BehaviourStatus.Failed;
            }

            if (childStatus == BehaviourStatus.Failed)
            {
                return BehaviourStatus.Succeeded;
            }

            return childStatus;
        }
    }
}