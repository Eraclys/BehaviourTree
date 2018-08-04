namespace BehaviourTree.Decorators
{
    public sealed class Inverter : DecoratorBehaviour
    {
        public Inverter(IBehaviour child) : this("Inverter", child)
        {
        }

        public Inverter(string name, IBehaviour child) : base(name, child)
        {
        }

        protected override BehaviourStatus Update(BtContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviourStatus.Failed)
            {
                return BehaviourStatus.Succeeded;
            }

            if (childStatus == BehaviourStatus.Succeeded)
            {
                return BehaviourStatus.Failed;
            }

            return childStatus;
        }
    }
}
