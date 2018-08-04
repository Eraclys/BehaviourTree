namespace BehaviourTree.Composites
{
    public sealed class Sequence : CompositeBehaviour
    {
        private int _currentChildIndex;

        public Sequence(IBehaviour[] children) : base(children)
        {
        }

        protected override BehaviourStatus Update(BtContext context)
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
    }
}
