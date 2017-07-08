using System;

namespace BehaviourTree
{
    public interface IBtBehaviour : IDisposable
    {
        BehaviourStatus Status { get; }

        BehaviourStatus Tick(ElaspedTicks elaspedTicks);

        void Reset();
    }
}
