using BehaviourTree.Demo.Components;
using BehaviourTree.Demo.GameEngine;
using System;
using System.Drawing;
using System.Numerics;

namespace BehaviourTree.Demo.UI
{
    public sealed class BotView : IRenderable
    {
        private readonly Entity _botEntity;
        private readonly AnimatedSprite _moveRightSprite;
        private readonly AnimatedSprite _moveLeftSprite;
        private readonly AnimatedSprite _moveUpSprite;
        private readonly AnimatedSprite _moveDownSprite;
        private AnimatedSprite _currentSprite;

        public BotView(Entity botEntity)
        {
            _botEntity = botEntity;

            const int animationSpeed = 1000;

            _moveRightSprite= new AnimatedSprite(
                animationSpeed,
                Assets.BotRight1,
                Assets.BotRight2,
                Assets.BotRight3,
                Assets.BotRight4);

            _moveLeftSprite = new AnimatedSprite(
                animationSpeed,
                Assets.BotLeft1,
                Assets.BotLeft2,
                Assets.BotLeft3,
                Assets.BotLeft4);

            _moveUpSprite = new AnimatedSprite(
                animationSpeed,
                Assets.BotUp1,
                Assets.BotUp2,
                Assets.BotUp3,
                Assets.BotUp4);

            _moveDownSprite = new AnimatedSprite(
                animationSpeed,
                Assets.BotDown1,
                Assets.BotDown2,
                Assets.BotDown3,
                Assets.BotDown4);

            _currentSprite = _moveDownSprite;
        }

        public Size Size => _currentSprite.Size;

        public void Render(Graphics graphics, Vector2 position, long ellapsedMilliseconds, float interpolation)
        {
            SetCurrentSprite();

            _currentSprite.Render(graphics, position, ellapsedMilliseconds, interpolation);
        }

        private void SetCurrentSprite()
        {
            var movementComponent = _botEntity.GetComponent<MovementComponent>();

            var velocity = movementComponent?.Velocity ?? Vector2.Zero;
            var absXVelocity = Math.Abs(velocity.X);
            var absYVelocity = Math.Abs(velocity.Y);

            _currentSprite.TotalDurationInMilliseconds = 1000;

            if (absXVelocity > absYVelocity)
            {
                _currentSprite = velocity.X > 0 ? _moveRightSprite : _moveLeftSprite;
            }
            else if (absYVelocity > absXVelocity)
            {
                _currentSprite = velocity.Y > 0 ? _moveDownSprite : _moveUpSprite;
            }
            else
            {
                _currentSprite.TotalDurationInMilliseconds = 0;
            }
        }
    }
}
