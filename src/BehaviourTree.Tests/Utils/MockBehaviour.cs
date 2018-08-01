namespace BehaviourTree.Tests.Utils
{
    internal sealed class MockBehaviour : BaseBehaviour
    {
        public int InitializeCallCount { get; private set; }
        public int UpdateCallCount { get; private set; }
        public int TerminateCallCount { get; private set; }

        public BehaviourStatus TerminateStatus { get; private set; }
        public BehaviourStatus ReturnStatus { get; set; }

        protected override BehaviourStatus Update(BtContext context)
        {
            UpdateCallCount++;
            return ReturnStatus;
        }

        protected override void OnTerminate(BehaviourStatus status)
        {
            TerminateCallCount++;
            TerminateStatus = status;
        }

        protected override void OnInitialize()
        {
            InitializeCallCount++;
        }
    }
}
