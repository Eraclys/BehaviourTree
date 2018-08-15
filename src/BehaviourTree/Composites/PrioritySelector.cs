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
            for (var i = 0; i < Children.Length; i++)
            {
                var childStatus = Children[i].Tick(context);

                if (childStatus != BehaviourStatus.Failed)
                {
                    for (var j = i+1; j < Children.Length; j++)
                    {
                        Children[j].Reset();
                    }

                    return childStatus;
                }
            }

            return BehaviourStatus.Failed;
        }
    }
}
