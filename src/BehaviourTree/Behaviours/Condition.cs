using System;

namespace BehaviourTree.Behaviours
{
    // ReSharper disable once ClassCanBeSealed.Global
    public class Condition<TContext> : IBehaviour<TContext>
    {
        private readonly string _name;
        private readonly Func<TContext, bool> _predicate;

        public Condition(Func<TContext, bool> predicate) : this(null, predicate)
        {

        }

        public Condition(string name, Func<TContext, bool> predicate)
        {
            _name = name ?? "Condition";
            _predicate = predicate;
        }

        public BehaviourStatus Tick(TContext context)
        {
            return _predicate(context) ? BehaviourStatus.Succeeded : BehaviourStatus.Failed;
        }

        public void Reset()
        {
        }

        public void Dispose()
        {
        }
    }
}
