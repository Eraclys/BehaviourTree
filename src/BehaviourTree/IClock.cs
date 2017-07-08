using System;

namespace BehaviourTree
{
    public interface IClock : IDisposable
    {
        long GetTimeStamp();
    }
}
