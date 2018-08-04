namespace BehaviourTree.Decorators
{
    public sealed class AutoReset : DecoratorBehaviour
    {
        public AutoReset(IBehaviour child) : this("AutoReset", child)
        {
        }

        public AutoReset(string name, IBehaviour child) : base(name, child)
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
