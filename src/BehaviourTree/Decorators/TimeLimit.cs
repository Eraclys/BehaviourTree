using System;

namespace BehaviourTree.Decorators
{
    public sealed class TimeLimit<TContext> : DecoratorBehaviour<TContext> where TContext : IClock
    {
        private readonly long _timeLimitInTicks;
        private long? _initialTimestamp;

        public TimeLimit(IBehaviour<TContext> child, int timeLimitInMilliseconds) : this("TimeLimit", child, timeLimitInMilliseconds)
        {
        }

        public TimeLimit(string name, IBehaviour<TContext> child, int timeLimitInMilliseconds) : base(name, child)
        {
            _timeLimitInTicks = TimeSpan.FromMilliseconds(timeLimitInMilliseconds).Ticks;
        }

        protected override BehaviourStatus Update(TContext context)
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

        protected override void DoReset(BehaviourStatus status)
        {
            _initialTimestamp = null;
            base.DoReset(status);
        }
    }
}
