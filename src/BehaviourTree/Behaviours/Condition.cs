using System;

namespace BehaviourTree.Behaviours
{
    // ReSharper disable once ClassCanBeSealed.Global
    public class Condition : IBehaviour
    {
        private readonly string _name;
        private readonly Func<BtContext, bool> _predicate;

        public Condition(Func<BtContext, bool> predicate) : this(null, predicate)
        {

        }

        public Condition(string name, Func<BtContext, bool> predicate)
        {
            _name = name ?? "Condition";
            _predicate = predicate;
        }

        public BehaviourStatus Tick(BtContext context)
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
