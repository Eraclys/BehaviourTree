namespace BehaviourTree.Decorators
{
    public sealed class BtSucceeder<TContext> : BaseBtDecorator<TContext>
    {
        public BtSucceeder(IBtBehaviour<TContext> child) : base(child){}

        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks, TContext context)
        {
            var childStatus = Child.Tick(elaspedTicks, context);

            if (childStatus == BehaviourStatus.Succeeded || childStatus == BehaviourStatus.Failed)
            {
                return BehaviourStatus.Succeeded;
            }

            return childStatus;
        }
    }
}
