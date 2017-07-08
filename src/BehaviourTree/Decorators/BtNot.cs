namespace BehaviourTree.Decorators
{
    public sealed class BtNot : BaseBtDecorator
    {
        public BtNot(IBtBehaviour child) : base(child) { }

        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks)
        {
            var childStatus = Child.Tick(elaspedTicks);

            if (childStatus == BehaviourStatus.Succeeded)
            {
                return BehaviourStatus.Failed;
            }

            if (childStatus == BehaviourStatus.Failed)
            {
                return BehaviourStatus.Succeeded;
            }

            return childStatus;
        }
    }
}