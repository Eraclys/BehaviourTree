namespace BehaviourTree.Decorators
{
    public sealed class Failer<TContext> : DecoratorBehaviour<TContext>
    {
        public Failer(IBehaviour<TContext> child) : this("Failer", child)
        {
        }

        public Failer(string name, IBehaviour<TContext> child) : base(name, child)
        {
        }

        protected override BehaviourStatus Update(TContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviourStatus.Succeeded || childStatus == BehaviourStatus.Failed)
            {
                return BehaviourStatus.Failed;
            }

            return childStatus;
        }

        public override void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<Failer<TContext>> typedVisitor)
            {
                typedVisitor.Visit(this);
                return;
            }

            base.Accept(visitor);
        }
    }
}
