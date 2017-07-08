namespace BehaviourTree.Decorators
{
    public class BtFailer<TContext> : BaseBtDecorator<TContext>
    {
        public BtFailer(IBtBehaviour<TContext> child)
            : base(new BtNot<TContext>(new BtSucceeder<TContext>(child)))
        {
        }

        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks, TContext context)
        {
            return Child.Status;
        }
    }
}
