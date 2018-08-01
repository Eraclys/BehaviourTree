namespace BehaviourTree.Decorators
{
    public sealed class Succeeder : DecoratorBehaviour
    {
        public Succeeder(IBehaviour child) : base(child)
        {
        }

        protected override BehaviourStatus Update(BtContext context)
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
