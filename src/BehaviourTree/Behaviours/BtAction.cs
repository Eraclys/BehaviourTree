using System;

namespace BehaviourTree.Behaviours
{
    public class BtAction : BaseBtBehaviour
    {
        private readonly Func<ElaspedTicks, BehaviourStatus> _action;

        public BtAction(Func<ElaspedTicks, BehaviourStatus> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks)
        {
            return _action(elaspedTicks);
        }

        protected override void DoReset()
        {
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}
