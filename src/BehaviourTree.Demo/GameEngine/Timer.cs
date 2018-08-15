using System.Diagnostics;

namespace BehaviourTree.Demo.GameEngine
{
    public sealed class Timer
    {
        private readonly Stopwatch _stopwatch;

        public Timer()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Reset();
        }

        public long GetElapsedMilliseconds() => _stopwatch.ElapsedMilliseconds;

        public void Start()
        {
            if (_stopwatch.IsRunning)
            {
                return;
            }

            _stopwatch.Reset();
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }
    }
}