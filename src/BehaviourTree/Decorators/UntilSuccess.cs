namespace BehaviourTree.Decorators
{
    public sealed class UntilSuccess<TContext> : DecoratorBehaviour<TContext>
    {
        public UntilSuccess(IBehaviour<TContext> child) : this("UntilSuccess", child)
        {
        }

        public UntilSuccess(string name, IBehaviour<TContext> child) : base(name, child)
        {
        }

        protected override BehaviourStatus Update(TContext context)
        {
            var childStatus = Child.Tick(context);

            return childStatus == BehaviourStatus.Succeeded ? BehaviourStatus.Succeeded : BehaviourStatus.Running;
        }
    }
}