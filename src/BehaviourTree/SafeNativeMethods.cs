using System.Runtime.InteropServices;

namespace BehaviourTree
{
    internal static class SafeNativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern bool QueryPerformanceCounter(out long value);

        [DllImport("kernel32.dll")]
        public static extern bool QueryPerformanceFrequency(out long value);
    }
}
