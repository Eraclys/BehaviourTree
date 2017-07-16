using System;

namespace BehaviourTree.Decorators
{
    public sealed class BtCooldown : BaseBtDecorator
    {
        private readonly long _cooldownTimeInTicks;
        private long _cooldownStartedTimestamp;
        private bool _onCooldown;

        public BtCooldown(IBtBehaviour child, int cooldownTimeInMilliseconds) : base(child)
        {
            _cooldownTimeInTicks = TimeSpan.FromMilliseconds(cooldownTimeInMilliseconds).Ticks;
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            return _onCooldown ? CooldownBehaviour(context) : RegularBehaviour(context);
        }

        private BehaviourStatus RegularBehaviour(BtContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviourStatus.Succeeded)
            {
                EnterCooldown(context);
            }

            return childStatus;
        }

        private BehaviourStatus CooldownBehaviour(BtContext context)
        {
            var currentTimeStamp = context.GetTimeStamp();

            var elapsedTicks = currentTimeStamp - _cooldownStartedTimestamp;

            if (elapsedTicks >= _cooldownTimeInTicks)
            {
                ExitCooldown();

                return RegularBehaviour(context);
            }

            return BehaviourStatus.Failed;
        }

        private void ExitCooldown()
        {
            _onCooldown = false;
            _cooldownStartedTimestamp = 0;
        }

        private void EnterCooldown(BtContext context)
        {
            _onCooldown = true;
            _cooldownStartedTimestamp = context.GetTimeStamp();
        }
    }
}
