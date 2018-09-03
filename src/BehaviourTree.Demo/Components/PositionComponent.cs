using System.Numerics;
using BehaviourTree.Demo.GameEngine;

namespace BehaviourTree.Demo.Components
{
    public sealed class PositionComponent : IComponent
    {
        public PositionComponent(Vector2 initialPosition)
        {
            Position = initialPosition;
            PreviousPosition = initialPosition;
        }

        public Vector2 PreviousPosition;
        public Vector2 Position;

        public Vector2 GetInterpolatedPosition(float interpolation)
        {
            var posX = PreviousPosition.X + interpolation * (Position.X - PreviousPosition.X);
            var posY = PreviousPosition.Y + interpolation * (Position.Y - PreviousPosition.Y);

            return new Vector2(posX, posY);
        }
    }
}
