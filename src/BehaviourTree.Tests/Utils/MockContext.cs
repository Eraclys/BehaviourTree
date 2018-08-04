using System;

namespace BehaviourTree.Tests.Utils
{
    public sealed class MockContext : IClock
    {
        private long _timestamp = 0;

        public void AddMilliseconds(int milliseconds)
        {
            _timestamp += TimeSpan.FromMilliseconds(milliseconds).Ticks;
        }

        public long GetTimeStamp()
        {
            return _timestamp;
        }
    }
}
