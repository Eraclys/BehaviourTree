namespace BehaviourTree.Composites
{
    public sealed class PrioritySelector<TContext> : CompositeBehaviour<TContext>
    {
        public PrioritySelector(IBehaviour<TContext>[] children) : this("PrioritySelector", children)
        {
        }

        public PrioritySelector(string name, IBehaviour<TContext>[] children) : base(name, children)
        {
        }

        protected override BehaviourStatus Update(TContext context)
        {
            for (int i = 0; i < Children.Length; i++)
            {
                var childStatus = Children[i].Tick(context);

                if (childStatus != BehaviourStatus.Failed)
                {
                    return childStatus;
                }
            }

            return BehaviourStatus.Failed;
        }

        public override void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<PrioritySelector<TContext>> typedVisitor)
            {
                typedVisitor.Visit(this);
                return;
            }

            base.Accept(visitor);
        }
    }
}
