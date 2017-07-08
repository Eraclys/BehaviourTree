using System;

namespace BehaviourTree
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
    }
}