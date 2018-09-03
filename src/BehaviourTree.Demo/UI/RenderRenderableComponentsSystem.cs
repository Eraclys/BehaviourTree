using BehaviourTree.Demo.GameEngine;
using BehaviourTree.Demo.Nodes;
using System.Collections.Generic;
using System.Drawing;

namespace BehaviourTree.Demo.UI
{
    public sealed class RenderRenderableComponentsSystem : IRenderSystem
    {
        private readonly Graphics _graphics;
        private readonly IEnumerable<RenderableNode> _renderableNodes;

        public RenderRenderableComponentsSystem(Engine engine, Graphics graphics)
        {
            _graphics = graphics;
            _renderableNodes = engine.GetNodes<RenderableNode>();
        }

        public void Render(long ellapsedMilliseconds, float interpolation)
        {
            foreach (var node in _renderableNodes)
            {
                node.RenderComponent.View.Render(
                    _graphics,
                    node.PositionComponent.GetInterpolatedPosition(interpolation),
                    ellapsedMilliseconds,
                    interpolation);
            }
        }
    }
}