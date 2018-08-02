using System;

namespace BehaviourTree.Decorators
{
    public sealed class TimeLimit : DecoratorBehaviour
    {
        private readonly long _timeLimitInTicks;
        private long? _initialTimestamp;

        public TimeLimit(IBehaviour child, int timeLimitInMilliseconds) : base(child)
        {
            _timeLimitInTicks = TimeSpan.FromMilliseconds(timeLimitInMilliseconds).Ticks;
        }

        protected override BehaviourStatus Update(BtContext context)
        {
            var currentTimeStamp = context.GetTimeStamp();

            if (_initialTimestamp == null)
            {
                _initialTimestamp = currentTimeStamp;
            }

            var elapsedTicks = currentTimeStamp - _initialTimestamp;

            if (elapsedTicks >= _timeLimitInTicks)
            {
                return BehaviourStatus.Failed;
            }

            return Child.Tick(context);
        }

        protected override void OnTerminate(BehaviourStatus status)
        {
            _initialTimestamp = null;
        }
    }
}
