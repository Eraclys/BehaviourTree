using System;

namespace BehaviourTree.Behaviours
{
    public sealed class Wait : BaseBehaviour
    {
        private readonly long _waitTimeInTicks;
        private long? _initialTimestamp;

        public Wait(int waitTimeInMilliseconds)
        {
            _waitTimeInTicks = TimeSpan.FromMilliseconds(waitTimeInMilliseconds).Ticks;
        }

        protected override BehaviourStatus Update(BtContext context)
        {
            var currentTimeStamp = context.GetTimeStamp();

            if (_initialTimestamp == null)
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

        protected override void OnTerminate(BehaviourStatus status)
        {
            _initialTimestamp = null;
        }
    }
}
