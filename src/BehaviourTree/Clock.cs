using System;

namespace BehaviourTree
{
    public sealed class Clock : IClock
    {
        public long GetTimeStampInMilliseconds()
        {
            return TimeSpan.FromTicks(DateTime.UtcNow.Ticks).Milliseconds;
        }
    }
}
