using System;

namespace BehaviourTree.Decorators
{
    public abstract class BaseBtDecorator<TContext> : BaseBtBehaviour<TContext>
    {
        protected readonly IBtBehaviour<TContext> Child;

        protected BaseBtDecorator(IBtBehaviour<TContext> child)
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