namespace BehaviourTree.Composites
{
    public sealed class PrioritySequence<TContext> : CompositeBehaviour<TContext>
    {
        public PrioritySequence(IBehaviour<TContext>[] children) : this("PrioritySequence", children)
        {
        }

        public PrioritySequence(string name, IBehaviour<TContext>[] children) : base(name, children)
        {
        }

        protected override BehaviourStatus Update(TContext context)
        {
            for (var i = 0; i < Children.Length; i++)
            {
                var childStatus = Children[i].Tick(context);

                if (childStatus != BehaviourStatus.Succeeded)
                {
                    for (var j = i + 1; j < Children.Length; j++)
                    {
                        Children[j].Reset();
                    }

                    return childStatus;
                }
            }

            return BehaviourStatus.Succeeded;
        }
    }
}
