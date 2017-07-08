namespace BehaviourTree.Composites
{
    public sealed class BtWhile<TContext> : BaseBtComposite<TContext>
    {
        private readonly IBtBehaviour<TContext> _condition;
        private readonly IBtBehaviour<TContext> _action;

        public BtWhile(IBtBehaviour<TContext> condition, IBtBehaviour<TContext> action) : base(new[] { condition, action })
        {
            _condition = condition;
            _action = action;
        }

        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks, TContext context)
        {
            if (_condition.Status == BehaviourStatus.Succeeded)
            {
                _condition.Reset();
            }

            _condition.Tick(elaspedTicks, context);

            if (_condition.Status == BehaviourStatus.Succeeded)
            {
                return _action.Tick(elaspedTicks, context);
            }

            return _condition.Status;
        }
    }
}
