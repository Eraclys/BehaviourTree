namespace BehaviourTree.Decorators
{
    public sealed class UntilFailed<TContext> : DecoratorBehaviour<TContext>
    {
        public UntilFailed(IBehaviour<TContext> child) : this("UntilFailed", child)
        {
        }

        public UntilFailed(string name, IBehaviour<TContext> child) : base(name, child)
        {
        }

        protected override BehaviourStatus Update(TContext context)
        {
            var childStatus = Child.Tick(context);

            return childStatus == BehaviourStatus.Failed ? BehaviourStatus.Succeeded : BehaviourStatus.Running;
        }
    }
}
