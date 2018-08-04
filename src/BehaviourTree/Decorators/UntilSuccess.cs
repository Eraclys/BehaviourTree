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

            if (childStatus == BehaviourStatus.Succeeded)
            {
                return BehaviourStatus.Succeeded;
            }

            return BehaviourStatus.Running;
        }

        public override void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<UntilSuccess<TContext>> typedVisitor)
            {
                typedVisitor.Visit(this);
                return;
            }

            base.Accept(visitor);
        }
    }
}