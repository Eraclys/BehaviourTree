using System;

namespace BehaviourTree
{
    public interface IBehaviour : IDisposable
    {
        BehaviourStatus Tick(BtContext context);
    }
}