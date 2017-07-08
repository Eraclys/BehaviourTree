namespace BehaviourTree.Decorators
{
    public class BtFailer : BaseBtDecorator
    {
        public BtFailer(IBtBehaviour child)
            : base(new BtNot(new BtSucceeder(child)))
        {
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            return Child.Status;
        }
    }
}
