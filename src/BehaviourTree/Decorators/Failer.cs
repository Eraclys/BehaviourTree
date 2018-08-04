namespace BehaviourTree.Decorators
{
    public sealed class Failer : DecoratorBehaviour
    {
        public Failer(IBehaviour child) : this("Failer", child)
        {
        }

        public Failer(string name, IBehaviour child) : base(name, child)
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
