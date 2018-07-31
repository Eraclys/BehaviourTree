using System.Collections.Generic;

namespace BehaviourTree.Tests.Utils
{

    internal sealed class MockBtBehaviour : BaseBtBehaviour
    {
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

        public BehaviourStatus ReturnStatus { get; set; }

        public int DoTickCount { get; private set; }
        public int DoResetCount { get; private set; }
        public bool WasCalled => (DoTickCount + DoResetCount) > 0;

        public IList<BehaviourStatus> StatusChanges { get; private set; } = new List<BehaviourStatus>();

        public MockBtBehaviour(
            BehaviourStatus currentStatus,
            BehaviourStatus returnStatus)
        {
            _currentStatus = currentStatus;
            ReturnStatus = returnStatus;
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            DoTickCount++;
            return ReturnStatus;
        }

        protected override void DoReset()
        {
            DoResetCount++;
            StatusChanges = new List<BehaviourStatus>();
        }
    }
}
