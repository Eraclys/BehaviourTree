using System.Drawing;
using System.Numerics;

namespace BehaviourTree.Demo.UI
{
    public sealed class StaticImage : IRenderable
    {
        private readonly Image _image;

        public StaticImage(Image image)
        {
            _image = image;
        }

        public Size Size => _image.Size;

        public void Render(Graphics graphics, Vector2 position, long ellapsedMilliseconds, float interpolation)
        {
            var xPos = position.X - _image.Width / 2f;
            var yPos = position.Y - _image.Height / 2f;

            graphics.DrawImage(_image, xPos, yPos);
        }
    }
}