using BehaviourTree.Demo.Components;
using BehaviourTree.Demo.GameEngine;

namespace BehaviourTree.Demo.Nodes
{
    public sealed class StaminaBarNode : Node
    {
        public PositionComponent PositionComponent;
        public RenderComponent RenderComponent;
        public StaminaBarComponent StaminaBarComponent;
        public StaminaComponent StaminaComponent;
    }
}