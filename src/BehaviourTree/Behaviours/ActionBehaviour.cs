using System;

namespace BehaviourTree.Behaviours
{
    public sealed class ActionBehaviour : BaseBehaviour
    {
        private readonly Func<BtContext, BehaviourStatus> _action;

        public ActionBehaviour(string name, Func<BtContext, BehaviourStatus> action) : base(name)
        {
            _action = action;
        }

        protected override BehaviourStatus Update(BtContext context)
        {
            return _action(context);
        }
    }
}
