namespace BehaviourTree.Decorators
{
    public sealed class AutoReset<TContext> : DecoratorBehaviour<TContext>
    {
        public AutoReset(IBehaviour<TContext> child) : this("AutoReset", child)
        {
        }

        public AutoReset(string name, IBehaviour<TContext> child) : base(name, child)
        {
        }

        protected override BehaviourStatus Update(TContext context)
        {
            return Child.Tick(context);
        }

        protected override void OnTerminate(BehaviourStatus status)
        {
            Child.Reset();
        }
    }
}
