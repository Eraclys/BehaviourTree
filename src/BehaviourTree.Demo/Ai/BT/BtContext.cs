using BehaviourTree.Demo.GameEngine;

namespace BehaviourTree.Demo.Ai.BT
{
    public sealed class BtContext : IClock
    {
        private readonly long _timeStampInMilliseconds;

        public BtContext(Entity agent, Engine engine, long timeStampInMilliseconds)
        {
            _timeStampInMilliseconds = timeStampInMilliseconds;
            Agent = agent;
            Engine = engine;
        }

        public Entity Agent { get; }

        public Engine Engine { get; }

        public long GetTimeStampInMilliseconds()
        {
            return _timeStampInMilliseconds;
        }
    }
}