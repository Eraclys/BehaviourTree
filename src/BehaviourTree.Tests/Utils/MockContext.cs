using System;

namespace BehaviourTree.Tests.Utils
{
    public sealed class MockContext : IClock, IRandomProvider
    {
        private long _timestamp;
        private double _randomValue;

        public void AddMilliseconds(int milliseconds)
        {
            _timestamp += TimeSpan.FromMilliseconds(milliseconds).Ticks;
        }

        public long GetTimeStampInMilliseconds()
        {
            return _timestamp;
        }

        public double NextRandomDouble()
        {
            return _randomValue;
        }

        public void SetNextRandomDouble(double value)
        {
            _randomValue = value;
        }
    }
}
