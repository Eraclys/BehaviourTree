using System;
using System.Diagnostics;

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
            if (Status == BehaviourStatus.Ready)
            {
                _lastTimestamp = context.GetTimeStamp();
                _totalElapsedTicks = 0;
            }

            _totalElapsedTicks += GetElapsedTicksSinceLastRun(context);

            Debug.WriteLine(TimeSpan.FromTicks(_totalElapsedTicks).Milliseconds);

            if (_totalElapsedTicks >= _waitTimeInTicks)
            {
                return BehaviourStatus.Succeeded;
            }

            return BehaviourStatus.Running;
        }

        private long GetElapsedTicksSinceLastRun(BtContext context)
        {
            var currentTimeStamp = context.GetTimeStamp();;
            var elapsedTicks = currentTimeStamp - _lastTimestamp;
            _lastTimestamp = currentTimeStamp;

            return elapsedTicks;
        }
    }
}
