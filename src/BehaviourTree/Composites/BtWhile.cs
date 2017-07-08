namespace BehaviourTree.Composites
{
    public sealed class BtWhile : BaseBtComposite
    {
        private readonly IBtBehaviour _condition;
        private readonly IBtBehaviour _action;

        public BtWhile(IBtBehaviour condition, IBtBehaviour action) : base(new[] { condition, action })
        {
            _condition = condition;
            _action = action;
        }

        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks, BtContext context)
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
