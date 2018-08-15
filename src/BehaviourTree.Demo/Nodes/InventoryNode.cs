using BehaviourTree.Demo.Components;
using BehaviourTree.Demo.GameEngine;

namespace BehaviourTree.Demo.Nodes
{
    public sealed class InventoryNode : Node
    {
        public PositionComponent PositionComponent;
        public RenderComponent RenderComponent;
        public InventoryComponent InventoryComponent;
    }
}