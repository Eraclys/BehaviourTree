using System;

namespace BehaviourTree.Composites
{
    public sealed class SimpleParallel<TContext> : CompositeBehaviour<TContext>
    {
        private readonly IBehaviour<TContext> _first;
        private readonly IBehaviour<TContext> _second;
        private BehaviourStatus _firstStatus;
        private BehaviourStatus _secondStatus;
        private readonly Func<TContext, BehaviourStatus> _behave;
        public readonly SimpleParallelPolicy Policy;

        public SimpleParallel(SimpleParallelPolicy policy, IBehaviour<TContext> first, IBehaviour<TContext> second) : this("SimpleParallel", policy, first, second)
        {

        }

        public SimpleParallel(string name, SimpleParallelPolicy policy, IBehaviour<TContext> first, IBehaviour<TContext> second) : base(name, new[]{first, second})
        {
            Policy = policy;
            _first = first;
            _second = second;
            _behave = policy == SimpleParallelPolicy.BothMustSucceed ? (Func<TContext, BehaviourStatus>)BothMustSucceedBehaviour : OnlyOneMustSucceedBehaviour;
        }

        private BehaviourStatus OnlyOneMustSucceedBehaviour(TContext context)
        {
            if (_firstStatus == BehaviourStatus.Succeeded || _secondStatus == BehaviourStatus.Succeeded)
            {
                return BehaviourStatus.Succeeded;
            }

            if (_firstStatus == BehaviourStatus.Failed && _secondStatus == BehaviourStatus.Failed)
            {
                return BehaviourStatus.Failed;
            }

            return BehaviourStatus.Running;
        }

        private BehaviourStatus BothMustSucceedBehaviour(TContext context)
        {
            if (_firstStatus == BehaviourStatus.Succeeded && _secondStatus == BehaviourStatus.Succeeded)
            {
                return BehaviourStatus.Succeeded;
            }

            if (_firstStatus == BehaviourStatus.Failed || _secondStatus == BehaviourStatus.Failed)
            {
                return BehaviourStatus.Failed;
            }

            return BehaviourStatus.Running;
        }

        protected override BehaviourStatus Update(TContext context)
        {
            if (Status != BehaviourStatus.Running)
            {
                _firstStatus = _first.Tick(context);
                _secondStatus = _second.Tick(context);
            }
            else
            {
                if (_firstStatus == BehaviourStatus.Ready || _firstStatus == BehaviourStatus.Running)
                {
                    _firstStatus = _first.Tick(context);
                }

                if (_secondStatus == BehaviourStatus.Ready || _secondStatus == BehaviourStatus.Running)
                {
                    _secondStatus = _second.Tick(context);
                }
            }

            return _behave(context);
        }

        protected override void DoReset(BehaviourStatus status)
        {
            _firstStatus = BehaviourStatus.Ready;
            _secondStatus = BehaviourStatus.Ready;
            base.DoReset(status);
        }

        public override void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<SimpleParallel<TContext>> typedVisitor)
            {
                typedVisitor.Visit(this);
                return;
            }

            base.Accept(visitor);
        }
    }
}
