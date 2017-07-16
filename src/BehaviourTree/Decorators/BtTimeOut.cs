using System;

namespace BehaviourTree.Decorators
{
    public sealed class BtTimeOut : BaseBtDecorator
    {
        private readonly long _timeoutInTicks;
        private long _initialTimestamp;

        public BtTimeOut(IBtBehaviour child, int timeoutInMilliseconds) : base(child)
        {
            _timeoutInTicks = TimeSpan.FromMilliseconds(timeoutInMilliseconds).Ticks;
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            var currentTimeStamp = context.GetTimeStamp();

            if (_initialTimestamp == 0)
            {
                _initialTimestamp = currentTimeStamp;
            }

            var elapsedTicks = currentTimeStamp - _initialTimestamp;

            if (elapsedTicks >= _timeoutInTicks)
            {
                return BehaviourStatus.Failed;
            }

            return Child.Tick(context);
        }

        protected override void DoReset()
        {
            _initialTimestamp = 0;
            Child.Reset();
        }
    }
}
