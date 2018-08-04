namespace BehaviourTree.Decorators
{
    public sealed class SubTree<TContext> : DecoratorBehaviour<TContext>
    {
        public SubTree(string name, IBehaviour<TContext> child) : base(name, child)
        {
        }

        protected override BehaviourStatus Update(TContext context)
        {
            return Child.Tick(context);
        }

        public override void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<SubTree<TContext>> typedVisitor)
            {
                typedVisitor.Visit(this);
                return;
            }

            base.Accept(visitor);
        }
    }
}
