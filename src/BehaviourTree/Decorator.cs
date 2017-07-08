using System;

namespace BehaviourTree
{
    public abstract class Decorator : BaseBehaviour
    {
        protected readonly IBehaviour Child;

        protected Decorator(IBehaviour child)
        {
            Child = child ?? throw new ArgumentNullException(nameof(child));
        }

        protected override void DoReset()
        {
            Child.Reset();
        }
    }
}