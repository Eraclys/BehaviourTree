namespace BehaviourTree.Decorators
{
    public sealed class BtAutoReset : BaseBtDecorator
    {
        public BtAutoReset(IBtBehaviour child) : base(child)
        {
        }

        public override BehaviourStatus Tick(BtContext context)
        {
            Status = DoTick(context);

            return Status;
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            var behaviourStatus = Child.Tick(context);

            if (behaviourStatus == BehaviourStatus.Succeeded || behaviourStatus == BehaviourStatus.Failed)
            {
                Reset();
            }

            return behaviourStatus;
        }
    }
}
