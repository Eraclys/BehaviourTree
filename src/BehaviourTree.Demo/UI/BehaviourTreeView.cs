using BehaviourTree.Composites;
using BehaviourTree.Decorators;
using BehaviourTree.Demo.Ai.BT;
using System;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace BehaviourTree.Demo.UI
{
    public sealed class BehaviourTreeView : IRenderable
    {
        private readonly IBehaviour<BtContext> _behaviour;
        private readonly Font _font;
        private int _index;

        public BehaviourTreeView(
            IBehaviour<BtContext> behaviour,
            Font font,
            Size mapSize)
        {
            _behaviour = behaviour;
            _font = font;
            Size = new Size(mapSize.Width / 3, mapSize.Height);
        }

        public void Render(Graphics graphics, Vector2 position, long ellapsedMilliseconds, float interpolation)
        {
            graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(new Point(0,0), Size));
            RenderBehaviourTree(graphics, 0, _behaviour);
            _index = 0;
        }

        public Size Size { get; }

        private void RenderBehaviourTree(Graphics graphics, int depth, IBehaviour<BtContext> behaviour)
        {
            RenderBehaviourTree(graphics, depth, (dynamic)behaviour);
        }

        private void RenderBehaviourTree(Graphics graphics, int depth, CompositeBehaviour<BtContext> obj)
        {
            RenderInternal(graphics, depth, obj);

            var childDepth = depth + 1;

            foreach (var child in obj.Children)
            {
                RenderBehaviourTree(graphics, childDepth, child);
            }
        }

        private void RenderBehaviourTree(Graphics graphics, int depth, DecoratorBehaviour<BtContext> obj)
        {
            RenderInternal(graphics, depth, obj);
            RenderBehaviourTree(graphics, ++depth, obj.Child);
        }

        private void RenderBehaviourTree(Graphics graphics, int depth, BaseBehaviour<BtContext> obj)
        {
            RenderInternal(graphics, depth, obj);
        }

        private void RenderInternal(Graphics graphics, int depth, IBehaviour<BtContext> obj)
        {
            var indentation = GetIndentation(depth);
            var name = GetName(obj);
            var color = GetColor(obj.Status);
            var nodeExpression = $"{indentation}{name}";

            graphics.DrawString(
                nodeExpression,
                _font,
                color,
                depth*4,
                _font.Height* _index++);
        }

        private static readonly Brush ReadyBrush = new SolidBrush(Color.DarkGray);
        private static readonly Brush RunningBrush = new SolidBrush(Color.Yellow);
        private static readonly Brush SuccessBrush = new SolidBrush(Color.Green);
        private static readonly Brush FailureBrush = new SolidBrush(Color.Red);

        private static Brush GetColor(BehaviourStatus status)
        {
            switch (status)
            {
                case BehaviourStatus.Ready: return ReadyBrush;
                case BehaviourStatus.Running: return RunningBrush;
                case BehaviourStatus.Succeeded: return SuccessBrush;
                case BehaviourStatus.Failed: return FailureBrush;
                default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        private static string GetIndentation(int depth)
        {
            return string.Join(string.Empty, Enumerable.Repeat("   ", depth));
        }

        private static string GetName(IBehaviour<BtContext> obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.Name))
            {
                return obj.Name;
            }

            var type = obj.GetType();

            // TODO: check for generic

            return type.Name;
        }
    }
}
