using System;

namespace BehaviourTree
{
    public sealed class Clock : IClock
    {
        public long GetTimeStamp()
        {
            return DateTime.UtcNow.Ticks;
        }
    }
}
