using System;

namespace BehaviourTree.Demo.GameEngine
{
    public sealed class GameRunner
    {
        private readonly Engine _engine;
        private readonly Timer _timer = new Timer();

        public GameRunner(Engine engine)
        {
            _engine = engine;
        }

        public void Run(Func<bool> shouldStop)
        {
            const int ticksPerSecond = 25;
            const int skipTicks = 1000 / ticksPerSecond;
            const int maxFrameskip = 10;

            _timer.Start();

            var nextGameTick = _timer.GetElapsedMilliseconds();

            while (!shouldStop())
            {
                var loops = 0;

                while (_timer.GetElapsedMilliseconds() > nextGameTick && loops < maxFrameskip)
                {
                    _engine.Update(_timer.GetElapsedMilliseconds());
                    nextGameTick += skipTicks;
                    loops++;
                }

                var elapsedMilliseconds = _timer.GetElapsedMilliseconds();
                var interpolation = (elapsedMilliseconds - nextGameTick + skipTicks) / (float) skipTicks;

                _engine.Render(elapsedMilliseconds, interpolation);
            }

            _timer.Stop();
        }
    }
}