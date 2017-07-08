namespace BehaviourTree.Composites
{
    public sealed class BtSimpleParallel : BaseBtComposite
    {
        private readonly SimpleParallelOption _option;

        public BtSimpleParallel(IBtBehaviour first, IBtBehaviour second, SimpleParallelOption option = SimpleParallelOption.BothMustSucceed) : base(new[]{first, second})
        {
            _option = option;
        }

        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks)
        {
            int successCount = 0;

            for (var index = 0; index < Children.Length; index++)
            {
                var child = Children[index];
                var childStatus = child.Tick(elaspedTicks);

                if (childStatus == BehaviourStatus.Failed)
                {
                    return BehaviourStatus.Failed;
                }

                if (childStatus == BehaviourStatus.Succeeded)
                {
                    if (_option == SimpleParallelOption.BothMustSucceed && index == 0)
                    {
                        return BehaviourStatus.Succeeded;
                    }

                    successCount++;
                }
            }

            if (successCount == 2)
            {
                return BehaviourStatus.Succeeded;
            }

            return BehaviourStatus.Running;
        }
    }
}