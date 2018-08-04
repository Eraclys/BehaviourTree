using System;

namespace BehaviourTree.Composites
{
    public sealed class SimpleParallel : CompositeBehaviour
    {
        private readonly IBehaviour _first;
        private readonly IBehaviour _second;
        private BehaviourStatus _firstStatus;
        private BehaviourStatus _secondStatus;
        private readonly Func<BtContext, BehaviourStatus> _behave;

        public enum Policy
        {
            BothMustSucceed,
            OnlyOneMustSucceed
        }

        public SimpleParallel(Policy policy, IBehaviour first, IBehaviour second) : base(new[]{first, second})
        {
            _first = first;
            _second = second;
            _behave = policy == Policy.BothMustSucceed ? (Func<BtContext, BehaviourStatus>)BothMustSucceedBehaviour : OnlyOneMustSucceedBehaviour;
        }

        private BehaviourStatus OnlyOneMustSucceedBehaviour(BtContext context)
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

        private BehaviourStatus BothMustSucceedBehaviour(BtContext context)
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

        protected override BehaviourStatus Update(BtContext context)
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
    }
}
