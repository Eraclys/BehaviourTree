using System;

namespace BehaviourTree.Behaviours
{
    public class BtAction : BaseBtBehaviour
    {
        private readonly Func<ElaspedTicks, BtContext, BehaviourStatus> _action;

        public BtAction(Func<ElaspedTicks, BtContext, BehaviourStatus> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks, BtContext context)
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
