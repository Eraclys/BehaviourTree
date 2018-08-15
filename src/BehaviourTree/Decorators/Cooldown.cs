namespace BehaviourTree.Decorators
{
    public sealed class Cooldown<TContext> : DecoratorBehaviour<TContext> where TContext : IClock
    {
        public readonly long CooldownTimeInMilliseconds;
        private long _cooldownStartedTimestamp;

        public bool OnCooldown { get; private set; }

        public Cooldown(IBehaviour<TContext> child, int cooldownTimeInMilliseconds) : this("Cooldown", child, cooldownTimeInMilliseconds)
        {
        }

        public Cooldown(string name, IBehaviour<TContext> child, int cooldownTimeInMilliseconds) : base(name, child)
        {
            CooldownTimeInMilliseconds = cooldownTimeInMilliseconds;
        }

        protected override BehaviourStatus Update(TContext context)
        {
            return OnCooldown ? CooldownBehaviour(context) : RegularBehaviour(context);
        }

        private BehaviourStatus RegularBehaviour(TContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviourStatus.Succeeded)
            {
                EnterCooldown(context);
            }

            return childStatus;
        }

        private BehaviourStatus CooldownBehaviour(TContext context)
        {
            var currentTimeStamp = context.GetTimeStampInMilliseconds();

            var elapsedMilliseconds = currentTimeStamp - _cooldownStartedTimestamp;

            if (elapsedMilliseconds >= CooldownTimeInMilliseconds)
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

        private void EnterCooldown(TContext context)
        {
            OnCooldown = true;
            _cooldownStartedTimestamp = context.GetTimeStampInMilliseconds();
        }
    }
}
