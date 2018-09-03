using BehaviourTree.Demo.Components;
using BehaviourTree.Demo.Events;
using BehaviourTree.Demo.GameEngine;
using BehaviourTree.Demo.UI;

namespace BehaviourTree.Demo.EventListeners
{
    public sealed class OnCharacterDeathAddTombstone : IEventListener<HealthReachedZero>
    {
        public void Handle(Engine engine, HealthReachedZero @event)
        {
            var entity = engine.GetEntityById(@event.EntityId);

            if (entity == null)
            {
                return;
            }

            var positionComponent = entity.GetComponent<PositionComponent>();

            engine.NewEntity()
                .AddComponent(new PositionComponent(positionComponent.Position))
                .AddComponent(new RenderComponent( new StaticImage(Assets.Tombstone)));
        }
    }
}
