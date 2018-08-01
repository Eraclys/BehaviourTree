namespace BehaviourTree.Decorators
{
    public sealed class BtUntilFailed : BaseBtDecorator
    {
        public BtUntilFailed(IBtBehaviour child) : base(child)
        {
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviourStatus.Succeeded)
            {
                Child.Reset();
            }

            if (childStatus == BehaviourStatus.Failed)
            {
                return BehaviourStatus.Succeeded;
            }

            return BehaviourStatus.Running;
        }
    }
}
