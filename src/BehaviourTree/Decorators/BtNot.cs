namespace BehaviourTree.Decorators
{
    public sealed class BtNot : BaseBtDecorator
    {
        public BtNot(IBtBehaviour child) : base(child) { }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviourStatus.Succeeded)
            {
                return BehaviourStatus.Failed;
            }

            if (childStatus == BehaviourStatus.Failed)
            {
                return BehaviourStatus.Succeeded;
            }

            return childStatus;
        }
    }
}