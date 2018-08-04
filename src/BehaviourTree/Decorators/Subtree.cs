namespace BehaviourTree.Decorators
{
    public sealed class SubTree : DecoratorBehaviour
    {
        public SubTree(string name, IBehaviour child) : base(name, child)
        {
        }

        protected override BehaviourStatus Update(BtContext context)
        {
            return Child.Tick(context);
        }
    }
}
