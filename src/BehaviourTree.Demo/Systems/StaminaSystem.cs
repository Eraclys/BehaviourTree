using BehaviourTree.Demo.GameEngine;
using BehaviourTree.Demo.Nodes;

namespace BehaviourTree.Demo.Systems
{
    public sealed class StaminaSystem : IterativeSystem<StaminaNode>
    {
        private long? _previousTimeStamp;
        private long _delta;
        private const double StaminaGainFrequencyInMilliseconds = 50;

        public StaminaSystem(Engine engine) : base(engine)
        {
            
        }

        public override void Update(long ellapsedMilliseconds)
        {
            if (_previousTimeStamp == null)
            {
                _previousTimeStamp = ellapsedMilliseconds;
            }

            _delta = ellapsedMilliseconds - _previousTimeStamp.Value;

            if (_delta > StaminaGainFrequencyInMilliseconds)
            {
                base.Update(ellapsedMilliseconds);
                _previousTimeStamp = ellapsedMilliseconds;
            }
        }

        protected override void UpdateNode(StaminaNode node, long ellapsedMilliseconds)
        {
            node.StaminaComponent.IncreaseBy(_delta / StaminaGainFrequencyInMilliseconds);
        }
    }
}
