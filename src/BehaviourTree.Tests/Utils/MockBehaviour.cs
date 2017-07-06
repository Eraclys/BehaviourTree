using System.Collections.Generic;

namespace BehaviourTree.Tests.Utils
{
    internal class MockBehaviour : BaseBehaviour
    {
        private readonly bool _returnSuccess;

        private BehaviourStatus _currentStatus;

        public override BehaviourStatus CurrentStatus
        {
            get => _currentStatus;
            protected set
            {
                _currentStatus = value;
                StatusChanges.Add(value);
            }
        }

        public IList<BehaviourStatus> StatusChanges { get; } = new List<BehaviourStatus>();
        public int DoStartCount { get; private set; }
        public int DoStopCount { get; private set; }

        public MockBehaviour(
            BehaviourStatus currentStatus,
            bool returnSuccess)
        {
            _currentStatus = currentStatus;
            _returnSuccess = returnSuccess;
        }

        protected override void DoStart()
        {
            DoStartCount++;
        }

        protected override void DoStop()
        {
            DoStopCount++;
            RaiseStopped(_returnSuccess);
        }
    }
}
