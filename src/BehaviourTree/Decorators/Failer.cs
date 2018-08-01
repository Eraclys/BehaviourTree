namespace BehaviourTree.Decorators
{
    public sealed class Failer : DecoratorBehaviour
    {
        public Failer(IBehaviour child) : base(child)
        {
        }

        protected override BehaviourStatus Update(BtContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviourStatus.Succeeded || childStatus == BehaviourStatus.Failed)
            {
                return BehaviourStatus.Failed;
            }

            return childStatus;
        }
    }
}
