namespace BehaviourTree.Decorators
{
    public sealed class BtSucceeder : BaseBtDecorator
    {
        public BtSucceeder(IBtBehaviour child) : base(child){}

        protected override BehaviourStatus DoTick(BtContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviourStatus.Succeeded || childStatus == BehaviourStatus.Failed)
            {
                return BehaviourStatus.Succeeded;
            }

            return childStatus;
        }
    }
}
