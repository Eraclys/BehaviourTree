using BehaviourTree.Demo.Components;
using BehaviourTree.Demo.GameEngine;

namespace BehaviourTree.Demo.Nodes
{
    public sealed class HealthBarNode : Node
    {
        public PositionComponent PositionComponent;
        public RenderComponent RenderComponent;
        public HealthBarComponent HealthBarComponent;
        public HealthComponent HealthComponent;
    }
}