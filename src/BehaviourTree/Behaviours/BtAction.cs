using System;

namespace BehaviourTree.Behaviours
{
    public class BtAction<TContext> : BaseBtBehaviour<TContext>
    {
        private readonly Func<ElaspedTicks, TContext, BehaviourStatus> _action;

        public BtAction(Func<ElaspedTicks, TContext, BehaviourStatus> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks, TContext context)
        {
            return _action(elaspedTicks, context);
        }

        protected override void DoReset()
        {
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}
