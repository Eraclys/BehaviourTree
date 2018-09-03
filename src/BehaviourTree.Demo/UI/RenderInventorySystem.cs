using BehaviourTree.Demo.Components;
using BehaviourTree.Demo.GameEngine;
using BehaviourTree.Demo.Nodes;
using System.Collections.Generic;
using System.Drawing;

namespace BehaviourTree.Demo.UI
{
    public sealed class RenderInventorySystem : IRenderSystem
    {
        private readonly Graphics _graphics;
        private readonly Font _font;
        private readonly IEnumerable<InventoryNode> _renderableNodes;
        private readonly Brush _textBrush = new SolidBrush(Color.White);
        private readonly int _textOffset = Assets.MaxIconWidth + 2;

        public RenderInventorySystem(Engine engine, Graphics graphics, Font font)
        {
            _graphics = graphics;
            _font = font;
            _renderableNodes = engine.GetNodes<InventoryNode>();
        }

        public void Render(long ellapsedMilliseconds, float interpolation)
        {
            foreach (var node in _renderableNodes)
            {
                RenderInventory(node, interpolation);
            }
        }

        private void RenderInventory(InventoryNode node, float interpolation)
        {
            var position = node.PositionComponent.GetInterpolatedPosition(interpolation);
            var spriteSize = node.RenderComponent.View.Size;
            var inventory = node.InventoryComponent;

            var drawXPosition = position.X + spriteSize.Width + 5;
            var drawYPosition = position.Y - spriteSize.Height;

            RenderInventoryIcon(
                drawXPosition,
                drawYPosition,
                Assets.LootInventoryIcon,
                inventory.Count(ItemTypes.Axe));

            RenderInventoryIcon(
                drawXPosition,
                drawYPosition + Assets.MaxIconHeight,
                Assets.LootInventoryIcon,
                inventory.Count(ItemTypes.Pickaxe));

            RenderInventoryIcon(
                drawXPosition,
                drawYPosition + Assets.MaxIconHeight * 2,
                Assets.StoneInventoryIcon,
                inventory.Count(ItemTypes.Stone));

            RenderInventoryIcon(
                drawXPosition,
                drawYPosition + Assets.MaxIconHeight * 3,
                Assets.TreeInventoryIcon,
                inventory.Count(ItemTypes.Wood));

            RenderInventoryIcon(
                drawXPosition,
                drawYPosition + Assets.MaxIconHeight * 4,
                Assets.FoodInventoryIcon,
                inventory.Count(ItemTypes.Food));
        }

        private void RenderInventoryIcon(float x, float y, Image icon, int count)
        {
            _graphics.DrawImage(icon, x, y);
            _graphics.DrawString($"x{count}", _font, _textBrush, x + _textOffset, y);
        }
    }
}