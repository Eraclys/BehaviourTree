using BehaviourTree.Demo.Components;
using BehaviourTree.Demo.GameEngine;
using BehaviourTree.Demo.Nodes;
using System.Linq;

namespace BehaviourTree.Demo.EventListeners
{
    public sealed class OnEntityRemovedClearTargets : IEventListener<EntityRemoved>
    {
        public void Handle(Engine engine, EntityRemoved @event)
        {
            var nodes = engine.GetNodes<TargetNode>();

            foreach (var node in nodes.Where(node => node.TargetEntityComponent.TargetId == @event.EntityId))
            {
                node.Entity.RemoveComponent<TargetEntityComponent>();
            }
        }
    }
}
