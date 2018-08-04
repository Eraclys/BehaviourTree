using System;

namespace BehaviourTree
{
    public interface IBehaviour<in TContext> : IDisposable
    {
        string Name { get; }
        BehaviourStatus Status { get; }
        BehaviourStatus Tick(TContext context);
        void Reset();
        void Accept(IVisitor visitor);
    }
}