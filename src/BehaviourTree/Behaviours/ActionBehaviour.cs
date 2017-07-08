using System;

namespace BehaviourTree.Behaviours
{
    public class ActionBehaviour : BaseBehaviour
    {
        private readonly Func<ElaspedTicks, BehaviourStatus> _action;

        public ActionBehaviour(Func<ElaspedTicks, BehaviourStatus> action)
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
    }
}
