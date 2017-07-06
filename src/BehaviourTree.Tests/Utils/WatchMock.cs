namespace BehaviourTree.Tests.Utils
{
    internal sealed class WatchMock : Decorator
    {
        public bool? IsSuccess { get; private set; }
        public int OnChildStoppedCount { get; private set; }

        public WatchMock(IBehaviour child) : base(child)
        {
        }

        public override void OnChildStopped(IBehaviour child, bool success)
        {
            OnChildStoppedCount++;
            IsSuccess = success;

            RaiseStopped(success);
        }
    }
}

