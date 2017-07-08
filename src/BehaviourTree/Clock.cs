using System;
using System.Runtime.InteropServices;

namespace BehaviourTree
{
    public sealed class Clock : IClock
    {
        // "Frequency" stores the frequency of the high-resolution performance counter,
        // if one exists. Otherwise it will store TicksPerSecond.
        // The frequency cannot change while the system is running,
        // so we only need to initialize it once.

        private static readonly bool IsHighResolution;

        static Clock()
        {
            long frequency;
            IsHighResolution = SafeNativeMethods.QueryPerformanceFrequency(out frequency);
        }

        public long GetTimeStamp()
        {
            if (IsHighResolution)
            {
                long timestamp;

                SafeNativeMethods.QueryPerformanceCounter(out timestamp);

                return timestamp;
            }

            return DateTime.UtcNow.Ticks;
        }

        public void Dispose()
        {
        }
    }
}
