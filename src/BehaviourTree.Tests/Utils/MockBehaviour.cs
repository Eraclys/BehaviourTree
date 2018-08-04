namespace BehaviourTree.Tests.Utils
{
    internal sealed class MockBehaviour : BaseBehaviour<MockContext>
    {
        public MockBehaviour() : base("MockBehaviour")
        {
        }

        public int InitializeCallCount { get; private set; }
        public int UpdateCallCount { get; private set; }
        public int TerminateCallCount { get; private set; }
        public int ResetCount { get; private set; }

        public BehaviourStatus TerminateStatus { get; private set; }
        public BehaviourStatus ResetStatus { get; private set; }
        public BehaviourStatus ReturnStatus { get; set; }

        protected override BehaviourStatus Update(MockContext context)
        {
            UpdateCallCount++;
            return ReturnStatus;
        }

        protected override void OnTerminate(BehaviourStatus status)
        {
            TerminateCallCount++;
            TerminateStatus = status;
        }

        protected override void DoReset(BehaviourStatus status)
        {
            ResetStatus = status;
            ResetCount++;
        }

        protected override void OnInitialize()
        {
            InitializeCallCount++;
        }
    }
}
