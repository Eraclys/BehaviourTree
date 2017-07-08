using System;

namespace BehaviourTree
{
    public sealed class BehaviourTreeRunner : IDisposable
    {
        private readonly BtContext _context;
        private readonly IBtBehaviour _behaviourTree;

        public BehaviourTreeRunner(IBtBehaviour behaviourTree)
            : this(behaviourTree, new BtContext())
        {
        }

        public BehaviourTreeRunner(IBtBehaviour behaviourTree, BtContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _behaviourTree = behaviourTree ?? throw new ArgumentNullException(nameof(behaviourTree));
        }

        public BehaviourStatus Tick()
        {
            var behaviourStatus = _behaviourTree.Tick(_context);

            if (behaviourStatus == BehaviourStatus.Succeeded || behaviourStatus == BehaviourStatus.Failed)
            {
                _behaviourTree.Reset();
            }

            return behaviourStatus;
        }

        public void Dispose()
        {
            _behaviourTree.Dispose();
        }
    }
}
