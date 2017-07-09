using System;

namespace BehaviourTree
{
    public abstract class BtTree : BaseBtBehaviour
    {
        private IBtBehaviour _child;

        protected void SetBehaviour(IBtBehaviour behaviour)
        {
            _child = behaviour;
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            if (_child == null)
            {
                throw new InvalidOperationException("Tree is not defined. Use SetBehaviour.");
            }

            return _child.Tick(context);
        }

        protected override void DoReset()
        {
            _child.Reset();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _child.Dispose();
            }
        }
    }
}
