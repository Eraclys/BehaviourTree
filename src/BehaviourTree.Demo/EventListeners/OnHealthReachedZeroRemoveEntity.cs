using BehaviourTree.Demo.Events;
using BehaviourTree.Demo.GameEngine;

namespace BehaviourTree.Demo.EventListeners
{
    public sealed class OnHealthReachedZeroRemoveEntity : IEventListener<HealthReachedZero>
    {
        public void Handle(Engine engine, HealthReachedZero @event)
        {
            engine.RemoveEntity(@event.EntityId);
        }
    }
}