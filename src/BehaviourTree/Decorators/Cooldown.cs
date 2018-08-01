using System;

namespace BehaviourTree.Decorators
{
    public sealed class Cooldown : DecoratorBehaviour
    {
        private readonly long _cooldownTimeInTicks;
        private long _cooldownStartedTimestamp;

        public bool OnCooldown { get; private set; }

        public Cooldown(IBehaviour child, int cooldownTimeInMilliseconds) : base(child)
        {
            _cooldownTimeInTicks = TimeSpan.FromMilliseconds(cooldownTimeInMilliseconds).Ticks;
        }

        protected override BehaviourStatus Update(BtContext context)
        {
            return OnCooldown ? CooldownBehaviour(context) : RegularBehaviour(context);
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
            OnCooldown = false;
            _cooldownStartedTimestamp = 0;
        }

        private void EnterCooldown(BtContext context)
        {
            OnCooldown = true;
            _cooldownStartedTimestamp = context.GetTimeStamp();
        }
    }
}
