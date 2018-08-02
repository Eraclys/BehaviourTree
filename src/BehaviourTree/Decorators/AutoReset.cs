namespace BehaviourTree.Decorators
{
    public sealed class AutoReset : DecoratorBehaviour
    {
        public AutoReset(IBehaviour child) : base(child)
        {
        }

        protected override BehaviourStatus Update(BtContext context)
        {
            return Child.Tick(context);
        }

        protected override void OnTerminate(BehaviourStatus status)
        {
            Child.Reset();
        }
    }
}
