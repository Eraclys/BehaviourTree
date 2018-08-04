using System;

namespace BehaviourTree.Behaviours
{
    // ReSharper disable once ClassCanBeSealed.Global
    public class Condition<TContext> : IBehaviour<TContext>
    {
        public string Name { get; }
        public BehaviourStatus Status { get; private set; }
        private readonly Func<TContext, bool> _predicate;

        public Condition(Func<TContext, bool> predicate) : this(null, predicate)
        {

        }

        public Condition(string name, Func<TContext, bool> predicate)
        {
            Name = name ?? "Condition";
            _predicate = predicate;
        }

        public BehaviourStatus Tick(TContext context)
        {
            Status = _predicate(context) ? BehaviourStatus.Succeeded : BehaviourStatus.Failed;
            return Status;
        }

        public void Reset()
        {
        }

        public void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<Condition<TContext>> typedVisitor)
            {
                typedVisitor.Visit(this);
            }
        }

        public void Dispose()
        {
        }
    }
}
