using System.Drawing;
using System.Numerics;

namespace BehaviourTree.Demo.UI
{
    public sealed class AnimatedSprite : IRenderable
    {
        public int TotalDurationInMilliseconds { get; set; }
        private readonly Image[] _frames;

        public AnimatedSprite(int totalDurationInMilliseconds, params Image[] frames)
        {
            TotalDurationInMilliseconds = totalDurationInMilliseconds;
            _frames = frames;
        }

        public Size Size { get; private set; }

        public void Render(Graphics graphics, Vector2 position, long ellapsedMilliseconds, float interpolation)
        {
            var currentFrame = GetNextFrame(ellapsedMilliseconds);
            Size = currentFrame.Size;
            var newCoordinates = new Point((int)position.X - currentFrame.Width / 2, (int)position.Y - currentFrame.Height / 2);
            graphics.DrawImage(currentFrame, newCoordinates);
        }

        private Image GetNextFrame(long ellapsedMilliseconds)
        {
            var durationPerFrame = TotalDurationInMilliseconds / _frames.Length;

            if (durationPerFrame == 0)
            {
                return _frames[0];
            }

            var amount = ellapsedMilliseconds / durationPerFrame;
            var index = amount % _frames.Length;

            return _frames[index];
        }
    }
}