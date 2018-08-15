using System;

namespace BehaviourTree.Behaviours
{
    // ReSharper disable once ClassCanBeSealed.Global
    public class Condition<TContext> : BaseBehaviour<TContext>
    {
        private readonly Func<TContext, bool> _predicate;

        public Condition(Func<TContext, bool> predicate) : this(null, predicate)
        {
        }

        public Condition(string name, Func<TContext, bool> predicate) : base(name ?? "Condition")
        {
            _predicate = predicate;
        }

        protected override BehaviourStatus Update(TContext context)
        {
            return _predicate(context) ? BehaviourStatus.Succeeded : BehaviourStatus.Failed;
        }
    }
}
