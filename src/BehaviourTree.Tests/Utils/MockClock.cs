using System;

namespace BehaviourTree.Tests.Utils
{
    public sealed class MockClock : IClock
    {
        private long _timestamp = 0;

        public IClock AddMilliseconds(int milliseconds)
        {
            _timestamp += TimeSpan.FromMilliseconds(milliseconds).Ticks;
            return this;
        }

        public long GetTimeStamp()
        {
            return _timestamp;
        }
    }
}
