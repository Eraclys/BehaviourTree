using BehaviourTree.Demo.GameEngine;
using BehaviourTree.Demo.Nodes;
using System.Drawing;

namespace BehaviourTree.Demo.Systems
{
    public sealed class MoveSystem : IterativeSystem<MoveNode>
    {
        private readonly Size _boardSize;

        public MoveSystem(Engine engine, Size boardSize) : base(engine)
        {
            _boardSize = boardSize;
        }

        protected override void UpdateNode(MoveNode node, long ellapsedMilliseconds)
        {
            var positionComponent = node.PositionComponent;
            var movementComponent = node.MovementComponent;

            positionComponent.PreviousPosition = positionComponent.Position;

            positionComponent.Position.X += movementComponent.Velocity.X;
            positionComponent.Position.Y += movementComponent.Velocity.Y;

            if (positionComponent.Position.X < 0)
            {
                positionComponent.Position.X = 0;
            }

            if (positionComponent.Position.Y < 0)
            {
                positionComponent.Position.Y = 0;
            }

            if (positionComponent.Position.X > _boardSize.Width)
            {
                positionComponent.Position.X = _boardSize.Width;
            }

            if (positionComponent.Position.Y > _boardSize.Height)
            {
                positionComponent.Position.Y = _boardSize.Height;
            }
        }
    }
}
