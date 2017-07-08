namespace BehaviourTree.Decorators
{
    public sealed class Succeeder : Decorator
    {
        public Succeeder(IBehaviour child) : base(child){}

        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks)
        {
            var childStatus = Child.Tick(elaspedTicks);

            if (childStatus == BehaviourStatus.Succeeded || childStatus == BehaviourStatus.Failed)
            {
                return BehaviourStatus.Succeeded;
            }

            return childStatus;
        }
    }
}
