namespace BehaviourTree.Decorators
{
    public sealed class Succeeder<TContext> : DecoratorBehaviour<TContext>
    {
        public Succeeder(IBehaviour<TContext> child) : this("Succeeder", child)
        {
        }

        public Succeeder(string name, IBehaviour<TContext> child) : base(name, child)
        {
        }

        protected override BehaviourStatus Update(TContext context)
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
