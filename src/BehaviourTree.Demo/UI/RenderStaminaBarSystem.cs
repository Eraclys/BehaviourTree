using BehaviourTree.Demo.GameEngine;
using BehaviourTree.Demo.Nodes;
using System.Collections.Generic;
using System.Drawing;

namespace BehaviourTree.Demo.UI
{
    public sealed class RenderStaminaBarSystem : IRenderSystem
    {
        private readonly Graphics _graphics;
        private readonly IEnumerable<StaminaBarNode> _renderableNodes;

        private const int EdgeSize = 2;
        private const int Offset = EdgeSize / 2;
        private const int Thickness = 7;

        private readonly Pen _edgePen = new Pen(Color.DarkGray, EdgeSize);
        private readonly Brush _fillBrush = new SolidBrush(Color.Blue);

        public RenderStaminaBarSystem(Engine engine, Graphics graphics)
        {
            _graphics = graphics;
            _renderableNodes = engine.GetNodes<StaminaBarNode>();
        }

        public void Render(long ellapsedMilliseconds, float interpolation)
        {
            foreach (var node in _renderableNodes)
            {
                var position = node.PositionComponent.GetInterpolatedPosition(interpolation);
                var spriteSize = node.RenderComponent.View.Size;
                var maxStamina = node.StaminaComponent.MaxStamina;
                var stamina = node.StaminaComponent.Stamina;

                var drawXPosition = (int)(position.X - spriteSize.Width);
                var drawYPosition = (int)(position.Y - spriteSize.Height - 10);
                var maxStaminaSize = spriteSize.Width * 2;
                var staminaSize = (int)(maxStaminaSize * (stamina / maxStamina));

                _graphics.DrawRectangle(_edgePen, drawXPosition, drawYPosition, maxStaminaSize, Thickness);
                _graphics.FillRectangle(_fillBrush, drawXPosition + Offset, drawYPosition + Offset, staminaSize - EdgeSize, Thickness - EdgeSize);
            }
        }
    }
}