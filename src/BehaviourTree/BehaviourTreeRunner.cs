using System;

namespace BehaviourTree
{
    public sealed class BehaviourTreeRunner : IDisposable
    {
        private readonly IBtBehaviour _behaviourTree;
        private readonly IClock _stopwatch;
        private long _lastTimeStamp;

        public BehaviourTreeRunner(IBtBehaviour behaviourTree)
            : this(behaviourTree, new Clock())
        {
        }

        public BehaviourTreeRunner(IBtBehaviour behaviourTree, IClock stopwatch)
        {
            _behaviourTree = behaviourTree ?? throw new ArgumentNullException(nameof(behaviourTree));
            _stopwatch = stopwatch ?? throw new ArgumentNullException(nameof(stopwatch));
            _lastTimeStamp = _stopwatch.GetTimeStamp();
        }

        public BehaviourStatus Tick()
        {
            var behaviourStatus = _behaviourTree.Tick(GetElapsedTicks());

            if (behaviourStatus == BehaviourStatus.Succeeded || behaviourStatus == BehaviourStatus.Failed)
            {
                _behaviourTree.Reset();
            }

            return behaviourStatus;
        }

        private ElaspedTicks GetElapsedTicks()
        {
            var currentTimeStamp = _stopwatch.GetTimeStamp();
            var elapsedDiff = currentTimeStamp - _lastTimeStamp;
            _lastTimeStamp = currentTimeStamp;

            return ElaspedTicks.From(elapsedDiff);
        }

        public void Dispose()
        {
            _behaviourTree.Dispose();
            _stopwatch.Dispose();
        }
    }
}
