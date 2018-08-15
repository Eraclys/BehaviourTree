using BehaviourTree.Demo.Components;
using BehaviourTree.Demo.GameEngine;

namespace BehaviourTree.Demo.Nodes
{
    public sealed class ItemNode : Node
    {
        public ItemComponent ItemComponent;
        public PositionComponent PositionComponent;
    }
}
