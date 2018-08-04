namespace BehaviourTree.Composites
{
    public sealed class Selector : CompositeBehaviour
    {
        private int _currentChildIndex;

        public Selector(IBehaviour[] children) : base(children)
        {
        }

        protected override BehaviourStatus Update(BtContext context)
        {
            do
            {
                var childStatus = Children[_currentChildIndex].Tick(context);

                if (childStatus != BehaviourStatus.Failed)
                {
                    return childStatus;
                }

            } while (++_currentChildIndex < Children.Length);

            return BehaviourStatus.Failed;
        }

        protected override void DoReset(BehaviourStatus status)
        {
            _currentChildIndex = 0;
            base.DoReset(status);
        }
    }
}
