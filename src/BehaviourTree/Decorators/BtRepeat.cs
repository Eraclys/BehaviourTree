using System;

namespace BehaviourTree.Decorators
{
    public sealed class BtRepeat : BaseBtDecorator
    {
        private readonly int _repeatCount;
        private int _currentCount;

        public BtRepeat(IBtBehaviour child, int repeatCount) : base(child)
        {
            if (repeatCount < 1)
            {
                throw new ArgumentException("repeatCount must be at least one", nameof(repeatCount));
            }

            _repeatCount = repeatCount;
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            if (Status == BehaviourStatus.Running &&
                Child.Status == BehaviourStatus.Succeeded &&
                _currentCount < _repeatCount)
            {
                Child.Reset();
            }

            var childStatus = Child.Tick(context);

            if (childStatus == BehaviourStatus.Succeeded)
            {
                _currentCount++;

                if (_currentCount < _repeatCount)
                {
                    return BehaviourStatus.Running;
                }
            }

            return childStatus;
        }
    }
}
