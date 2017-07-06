namespace BehaviourTree
{
    public sealed class Selection : Composite
    {
        private int _currentIndex;

        public Selection(params IBehaviour[] children) : base(children)
        {
        }

        protected override void DoStart()
        {
            _currentIndex = -1;

            StartNextChild();
        }

        protected override void DoStop()
        {
            Children[_currentIndex].Stop();
        }

        private void StartNextChild()
        {
            if (++_currentIndex < Children.Length)
            {
                Children[_currentIndex].Start();
            }
            else
            {
                RaiseStopped(false);
            }
        }

        public override void OnChildStopped(IBehaviour child, bool success)
        {
            if (!success)
            {
                if (CurrentStatus == BehaviourStatus.StopRequested)
                {
                    RaiseStopped(false);
                }
                else
                {
                    StartNextChild();
                }
            }
            else
            {
                RaiseStopped(true);
            }
        }
    }
}
