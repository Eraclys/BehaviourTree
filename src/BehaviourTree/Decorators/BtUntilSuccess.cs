namespace BehaviourTree.Decorators
{
    public sealed class BtUntilSuccess : BaseBtDecorator
    {
        public BtUntilSuccess(IBtBehaviour child) : base(child)
        {
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviourStatus.Failed)
            {
                Child.Reset();
            }

            if (childStatus == BehaviourStatus.Succeeded)
            {
                return BehaviourStatus.Succeeded;
            }

            return BehaviourStatus.Running;
        }
    }
}