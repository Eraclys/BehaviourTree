namespace BehaviourTree.Decorators
{
    public sealed class Inverter<TContext> : DecoratorBehaviour<TContext>
    {
        public Inverter(IBehaviour<TContext> child) : this("Inverter", child)
        {
        }

        public Inverter(string name, IBehaviour<TContext> child) : base(name, child)
        {
        }

        protected override BehaviourStatus Update(TContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviourStatus.Failed)
            {
                return BehaviourStatus.Succeeded;
            }

            if (childStatus == BehaviourStatus.Succeeded)
            {
                return BehaviourStatus.Failed;
            }

            return childStatus;
        }

        public override void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<Inverter<TContext>> typedVisitor)
            {
                typedVisitor.Visit(this);
                return;
            }

            base.Accept(visitor);
        }
    }
}
