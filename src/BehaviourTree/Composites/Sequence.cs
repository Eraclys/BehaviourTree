namespace BehaviourTree.Composites
{
    public class Sequence<TContext> : CompositeBehaviour<TContext>
    {
        private int _currentChildIndex;

        public Sequence(IBehaviour<TContext>[] children) : this("Sequence", children)
        {
        }

        public Sequence(string name, IBehaviour<TContext>[] children) : base(name, children)
        {
        }

        protected virtual IBehaviour<TContext> GetChild(int index)
        {
            return Children[index];
        }

        protected override BehaviourStatus Update(TContext context)
        {
            do
            {
                var childStatus = GetChild(_currentChildIndex).Tick(context);

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
    }
}
