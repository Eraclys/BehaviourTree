using System.Collections.Generic;

namespace BehaviourTree.Tests.Utils
{

    internal sealed class MockBtBehaviour : BaseBtBehaviour<MockContext>
    {
        private readonly BehaviourStatus _returnStatus;

        private BehaviourStatus _currentStatus;

        public override BehaviourStatus Status
        {
            get => _currentStatus;
            protected set
            {
                _currentStatus = value;
                StatusChanges.Add(value);
            }
        }

        public int DoTickCount { get; private set; }
        public int DoResetCount { get; private set; }

        public IList<BehaviourStatus> StatusChanges { get; private set; } = new List<BehaviourStatus>();

        public MockBtBehaviour(
            BehaviourStatus currentStatus,
            BehaviourStatus returnStatus)
        {
            _currentStatus = currentStatus;
            _returnStatus = returnStatus;
        }

        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks, MockContext context)
        {
            DoTickCount++;
            return _returnStatus;
        }

        protected override void DoReset()
        {
            DoResetCount++;
            StatusChanges = new List<BehaviourStatus>();
            DoTickCount = 0;
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}
