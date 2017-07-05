namespace BehaviourTree
{
    public sealed class Parallel : Composite
    {
        private readonly int _nbRequiredToSuccess;
        private readonly int _nbRequiredToFail;
        private int _runningCount;
        private int _successCount;
        private int _failureCount;

        public Parallel(int nbRequiredToSuccess, int nbRequiredToFail, params IBehaviour[] children) : base(children)
        {
            _nbRequiredToSuccess = nbRequiredToSuccess;
            _nbRequiredToFail = nbRequiredToFail;
        }

        protected override void DoStart()
        {
            _runningCount = 0;
            _successCount = 0;
            _failureCount = 0;

            foreach (var child in Children)
            {
                _runningCount++;
                child.Start();
            }
        }

        protected override void DoStop()
        {
            foreach (var child in Children)
            {
                child.Stop();
            }
        }

        public override void OnChildStopped(IBehaviour child, bool success)
        {
            _runningCount--;

            if (success)
            {
                _successCount++;
            }
            else
            {
                _failureCount++;
            }

            var totalCounts = _runningCount + _successCount + _failureCount;
            var allChildrenStarted = totalCounts == Children.Length;

            if (allChildrenStarted && _runningCount == 0)
            {
                if (_successCount >= _nbRequiredToSuccess)
                {
                    RaiseStopped(true);
                }

                if (_failureCount >= _nbRequiredToFail)
                {
                    RaiseStopped(false);
                }
            }
        }
    }
}