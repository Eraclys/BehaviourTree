using System;

namespace BehaviourTree.Decorators
{
    public sealed class BtRegulator : BaseBtDecorator
    {
        private readonly long _intervalInTicks;
        private long _previousTimestamp;

        public BtRegulator(IBtBehaviour child, int intervalInMilliseconds) : base(child)
        {
            _intervalInTicks = TimeSpan.FromMilliseconds(intervalInMilliseconds).Ticks;
        }

        public override BehaviourStatus Tick(BtContext context)
        {
            Status = DoTick(context);

            return Status;
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            if (IsTimeToRun(context))
            {
                return Child.Tick(context);
            }

            return Child.Status;
        }

        private bool IsTimeToRun(BtContext context)
        {
            var currentTimeStamp = context.GetTimeStamp();

            var elapsedTicks = currentTimeStamp - _previousTimestamp;

            if (elapsedTicks >= _intervalInTicks)
            {
                _previousTimestamp = currentTimeStamp;
                return true;
            }

            return false;
        }

        protected override void DoReset()
        {
            _previousTimestamp = 0;
            Child.Reset();
        }
    }
}
