using System.Drawing;
using System.Numerics;

namespace BehaviourTree.Demo.UI
{
    public interface IRenderable
    {
        void Render(Graphics graphics, Vector2 position, long ellapsedMilliseconds, float interpolation);
        Size Size { get; }
    }
}