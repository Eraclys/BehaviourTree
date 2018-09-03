using BehaviourTree.Demo.GameEngine;
using BehaviourTree.Demo.Nodes;
using System.Collections.Generic;
using System.Drawing;

namespace BehaviourTree.Demo.UI
{
    public sealed class RenderHealthBarSystem : IRenderSystem
    {
        private readonly Graphics _graphics;
        private readonly IEnumerable<HealthBarNode> _renderableNodes;

        private const int EdgeSize = 2;
        private const int Offset = EdgeSize / 2;
        private const int Thickness = 7;

        private readonly Pen _edgePen = new Pen(Color.DarkGray, EdgeSize);
        private readonly Brush _fillBrush = new SolidBrush(Color.Red);

        public RenderHealthBarSystem(Engine engine, Graphics graphics)
        {
            _graphics = graphics;
            _renderableNodes = engine.GetNodes<HealthBarNode>();
        }

        public void Render(long ellapsedMilliseconds, float interpolation)
        {
            foreach (var node in _renderableNodes)
            {
                var position = node.PositionComponent.GetInterpolatedPosition(interpolation);
                var spriteSize = node.RenderComponent.View.Size;
                var maxHealth = node.HealthComponent.MaxHealth;
                var health = node.HealthComponent.Health;

                var drawXPosition = (int)(position.X - spriteSize.Width);
                var drawYPosition = (int)(position.Y - spriteSize.Height);
                var maxHealthSize = spriteSize.Width * 2;
                var healthSize = (int)(maxHealthSize * (health / maxHealth));

                _graphics.DrawRectangle(_edgePen, drawXPosition, drawYPosition, maxHealthSize, Thickness);
                _graphics.FillRectangle(_fillBrush, drawXPosition + Offset, drawYPosition + Offset, healthSize - EdgeSize, Thickness - EdgeSize);
            }
        }
    }
}
