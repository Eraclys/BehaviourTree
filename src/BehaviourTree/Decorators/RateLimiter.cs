using System;

namespace BehaviourTree.Decorators
{
    public sealed class RateLimiter : DecoratorBehaviour
    {
        private readonly long _intervalInTicks;
        private long? _previousTimestamp;
        private BehaviourStatus _previousChildStatus;

        public RateLimiter(IBehaviour child, int intervalInMilliseconds) : this("RateLimiter", child, intervalInMilliseconds)
        {
        }

        public RateLimiter(string name, IBehaviour child, int intervalInMilliseconds) : base(name, child)
        {
            _intervalInTicks = TimeSpan.FromMilliseconds(intervalInMilliseconds).Ticks;
        }

        protected override BehaviourStatus Update(BtContext context)
        {
            var currentTimeStamp = context.GetTimeStamp();

            var elapsedTicks = currentTimeStamp - _previousTimestamp;

            if (_previousTimestamp == null || elapsedTicks >= _intervalInTicks)
            {
                _previousChildStatus = Child.Tick(context);

                if (_previousChildStatus != BehaviourStatus.Running)
                {
                    _previousTimestamp = currentTimeStamp;
                }
            }

            return _previousChildStatus;
        }
    }
}
