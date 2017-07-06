namespace BehaviourTree.Tests.Utils
{
    internal class TerminateImediatlyMockBehaviour : MockBehaviour
    {
        public TerminateImediatlyMockBehaviour(BehaviourStatus currentStatus, bool returnSuccess) : base(currentStatus, returnSuccess)
        {
        }

        protected override void DoStart()
        {
            base.DoStart();

            Stop();
        }
    }
}