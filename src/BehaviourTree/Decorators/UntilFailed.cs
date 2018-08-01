namespace BehaviourTree.Decorators
{
    public sealed class UntilFailed : DecoratorBehaviour
    {
        public UntilFailed(IBehaviour child) : base(child)
        {
        }

        protected override BehaviourStatus Update(BtContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviourStatus.Failed)
            {
                return BehaviourStatus.Succeeded;
            }

            return BehaviourStatus.Running;
        }
    }
}
