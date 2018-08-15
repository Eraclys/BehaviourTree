using BehaviourTree.Demo.GameEngine;
using BehaviourTree.Demo.UI;

namespace BehaviourTree.Demo.Components
{
    public sealed class RenderComponent : IComponent
    {
        public RenderComponent(IRenderable view)
        {
            View = view;
        }

        public readonly IRenderable View;
    }
}
