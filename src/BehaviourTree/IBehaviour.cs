using System;

namespace BehaviourTree
{
    public interface IBehaviour<TContext> : IDisposable
    {
        BehaviourStatus Tick(TContext context);
        void Reset();
    }
}