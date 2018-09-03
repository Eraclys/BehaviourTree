using System;

namespace BehaviourTree.Tests.Utils
{
    public sealed class MockContext : IClock
    {
        private long _timestamp;

        public void AddMilliseconds(int milliseconds)
        {
            _timestamp += TimeSpan.FromMilliseconds(milliseconds).Ticks;
        }

        public long GetTimeStampInMilliseconds()
        {
            return _timestamp;
        }
    }
}
