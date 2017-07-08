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

        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks)
        {
            if (_condition.Status == BehaviourStatus.Succeeded)
            {
                _condition.Reset();
            }

            _condition.Tick(elaspedTicks);

            if (_condition.Status == BehaviourStatus.Succeeded)
            {
                return _action.Tick(elaspedTicks);
            }

            return _condition.Status;
        }
    }
}
