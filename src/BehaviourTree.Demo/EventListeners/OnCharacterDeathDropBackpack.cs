using BehaviourTree.Demo.Components;
using BehaviourTree.Demo.Events;
using BehaviourTree.Demo.GameEngine;
using BehaviourTree.Demo.UI;

namespace BehaviourTree.Demo.EventListeners
{
    public sealed class OnCharacterDeathDropBackpack : IEventListener<HealthReachedZero>
    {
        public void Handle(Engine engine, HealthReachedZero @event)
        {
            var entity = engine.GetEntityById(@event.EntityId);

            var inventoryComponent = entity?.GetComponent<InventoryComponent>();
            var positionComponent = entity?.GetComponent<PositionComponent>();

            if (inventoryComponent == null || positionComponent == null)
            {
                return;
            }

            foreach (var item in inventoryComponent.Items)
            {
                inventoryComponent.Remove(item.Key, item.Value);

                engine
                    .NewEntity()
                    .AddComponent(new ItemComponent(item.Key))
                    .AddComponent(new LootableComponent(item.Value))
                    .AddComponent(new RenderComponent(new StaticImage(Assets.Loot)))
                    .AddComponent(new PositionComponent(positionComponent.Position));
            }
        }
    }
}
