namespace BehaviourTree.Composites
{
    public sealed class Sequence<TContext> : CompositeBehaviour<TContext>
    {
        private int _currentChildIndex;

        public Sequence(IBehaviour<TContext>[] children) : this("Sequence", children)
        {
        }

        public Sequence(string name, IBehaviour<TContext>[] children) : base(name, children)
        {
        }

        protected override BehaviourStatus Update(TContext context)
        {
            do
            {
                var childStatus = Children[_currentChildIndex].Tick(context);

                if (childStatus != BehaviourStatus.Succeeded)
                {
                    return childStatus;
                }

            } while (++_currentChildIndex < Children.Length);

            return BehaviourStatus.Succeeded;
        }

        protected override void DoReset(BehaviourStatus status)
        {
            _currentChildIndex = 0;
            base.DoReset(status);
        }

        public override void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<Sequence<TContext>> typedVisitor)
            {
                typedVisitor.Visit(this);
                return;
            }

            base.Accept(visitor);
        }
    }
}
