using BehaviourTree.Decorators;

namespace BehaviourTree.Tests.Utils
{
    public sealed class MockDecoratorBehaviour : DecoratorBehaviour<MockContext>
    {
        public MockDecoratorBehaviour(IBehaviour<MockContext> child) : base("MockDecoratorBehaviour", child)
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
            ReturnStatus = Child.Tick(context);
            return ReturnStatus;
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
