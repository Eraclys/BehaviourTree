using System;

namespace BehaviourTree.Behaviours
{
    public sealed class BtWait : BaseBtBehaviour
    {
        private readonly long _waitTimeInTicks;
        private long _initialTimestamp;

        public BtWait(int waitTimeInMilliseconds)
        {
            _waitTimeInTicks = TimeSpan.FromMilliseconds(waitTimeInMilliseconds).Ticks;
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            var currentTimeStamp = context.GetTimeStamp();

            if (_initialTimestamp == 0)
            {
                _initialTimestamp = currentTimeStamp;
            }

            var elapsedTicks = currentTimeStamp - _initialTimestamp;

            if (elapsedTicks >= _waitTimeInTicks)
            {
                return BehaviourStatus.Succeeded;
            }

            return BehaviourStatus.Running;
        }

        protected override void DoReset()
        {
            _initialTimestamp = 0;
        }
    }
}
