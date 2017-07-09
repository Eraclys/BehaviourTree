using System;

namespace BehaviourTree
{
    public abstract class BtSubTree : BaseBtBehaviour, IBtDecorator
    {
        public IBtBehaviour Child { get; private set; }

        protected void SetBehaviour(IBtBehaviour behaviour)
        {
            Child = behaviour;
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            if (Child == null)
            {
                throw new InvalidOperationException("Tree is not defined. Use SetBehaviour.");
            }

            return Child.Tick(context);
        }

        protected override void DoReset()
        {
            Child.Reset();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Child.Dispose();
            }
        }
    }
}
