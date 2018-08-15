using BehaviourTree.Demo.Events;
using BehaviourTree.Demo.GameEngine;
using BehaviourTree.Demo.Nodes;

namespace BehaviourTree.Demo.Systems
{
    public sealed class HealthSystem : IterativeSystem<HealthNode>
    {
        private long? _previousTimeStamp;
        private long _delta;
        private const double HpLossFrequencyInMilliseconds = 300;

        public HealthSystem(Engine engine) : base(engine)
        {
        }

        public override void Update(long ellapsedMilliseconds)
        {
            if (_previousTimeStamp == null)
            {
                _previousTimeStamp = ellapsedMilliseconds;
            }

            _delta = ellapsedMilliseconds - _previousTimeStamp.Value;

            if (_delta > HpLossFrequencyInMilliseconds)
            {
                base.Update(ellapsedMilliseconds);
                _previousTimeStamp = ellapsedMilliseconds;
            }
        }

        protected override void UpdateNode(HealthNode node, long ellapsedMilliseconds)
        {
            var healthComponent = node.HealthComponent;

            var previousHealth = healthComponent.Health;
            healthComponent.ReduceBy(_delta / HpLossFrequencyInMilliseconds);

            if (previousHealth > 0 && healthComponent.Health == 0)
            {
                Engine.PublishEvent(new HealthReachedZero(node.Entity.Id));
            }
        }
    }
}
