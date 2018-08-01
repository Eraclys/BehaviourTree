namespace BehaviourTree.Decorators
{
    public sealed class UntilSuccess : DecoratorBehaviour
    {
        public UntilSuccess(IBehaviour child) : base(child)
        {
        }

        protected override BehaviourStatus Update(BtContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviourStatus.Succeeded)
            {
                return BehaviourStatus.Succeeded;
            }

            return BehaviourStatus.Running;
        }
    }
}