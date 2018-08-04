using BehaviourTree.Composites;

namespace BehaviourTree.Tests.Utils
{
    public sealed class MockCompositeBehaviour : CompositeBehaviour<MockContext>
    {
        public MockCompositeBehaviour(IBehaviour<MockContext>[] children) : base("MockCompositeBehaviour", children)
        {
        }

        public int InitializeCallCount { get; private set; }
        public int UpdateCallCount { get; private set; }
        public int TerminateCallCount { get; private set; }
        public int ResetCount { get; private set; }

        public BehaviourStatus TerminateStatus { get; private set; }
        public BehaviourStatus ResetStatus { get; private set; }

        protected override BehaviourStatus Update(MockContext context)
        {
            BehaviourStatus lastStatus = BehaviourStatus.Ready;

            foreach (var child in Children)
            {
                lastStatus = child.Tick(context);
            }

            UpdateCallCount++;
            return lastStatus;
        }

        protected override void OnTerminate(BehaviourStatus status)
        {
            base.OnTerminate(status);
            TerminateCallCount++;
            TerminateStatus = status;
        }

        protected override void DoReset(BehaviourStatus status)
        {
            base.DoReset(status);
            ResetStatus = status;
            ResetCount++;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            InitializeCallCount++;
        }
    }
}
