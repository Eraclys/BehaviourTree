using System;

namespace BehaviourTree.Behaviours
{
    public class BtAction : BaseBtBehaviour
    {
        private readonly Func<BtContext, BehaviourStatus> _action;

        public BtAction(Func<BtContext, BehaviourStatus> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            return _action(context);
        }
    }
}
