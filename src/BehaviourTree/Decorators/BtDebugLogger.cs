using System;
using System.Diagnostics;

namespace BehaviourTree.Decorators
{
    public sealed class BtDebugLogger : BaseBtDecorator
    {
        public BtDebugLogger(IBtBehaviour child) : base(child)
        {
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            var behaviourStatus = Child.Tick(context);
            Debug.WriteLine(this.ToFriendlyString());
            return behaviourStatus;
        }

        protected override void DoReset()
        {
            Child.Reset();
            Debug.WriteLine($"Reset {Environment.NewLine}");
        }
    }
}