using System;

namespace BehaviourTree.Decorators
{
    public abstract class BaseBtDecorator : BaseBtBehaviour
    {
        protected readonly IBtBehaviour Child;

        protected BaseBtDecorator(IBtBehaviour child)
        {
            Child = child ?? throw new ArgumentNullException(nameof(child));
        }

        protected override void DoReset()
        {
            Child.Reset();
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            Child.Dispose();
        }
    }
}