using BehaviourTree.Demo.Components;
using BehaviourTree.Demo.GameEngine;

namespace BehaviourTree.Demo.Nodes
{
    public sealed class RenderableNode : Node
    {
        public RenderComponent RenderComponent;
        public PositionComponent PositionComponent;
    }
}
