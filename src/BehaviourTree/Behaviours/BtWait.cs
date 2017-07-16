using System;

namespace BehaviourTree.Behaviours
{
    public sealed class BtWait : BaseBtBehaviour
    {
        private readonly long _waitTimeInTicks;
        private long _lastTimestamp;
        private long _totalElapsedTicks;

        public BtWait(int waitTimeInMilliseconds)
        {
            _waitTimeInTicks = TimeSpan.FromMilliseconds(waitTimeInMilliseconds).Ticks;
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            var currentTimeStamp = context.GetTimeStamp();

            if (_lastTimestamp != 0)
            {
                var elapsedTicks = currentTimeStamp - _lastTimestamp;

                _totalElapsedTicks += elapsedTicks;
            }

            _lastTimestamp = currentTimeStamp;

            if (_totalElapsedTicks >= _waitTimeInTicks)
            {
                return BehaviourStatus.Succeeded;
            }

            return BehaviourStatus.Running;
        }

        protected override void DoReset()
        {
            _lastTimestamp = 0;
            _totalElapsedTicks = 0;
        }
    }
}
