using System;

namespace BehaviourTree
{
    public interface IBtBehaviour<in TContext> : IDisposable
    {
        BehaviourStatus Status { get; }

        BehaviourStatus Tick(ElaspedTicks elaspedTicks, TContext context);

        void Reset();
    }
}
