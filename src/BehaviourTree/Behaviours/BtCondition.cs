namespace BehaviourTree.Behaviours
{
    public abstract class BtCondition : BaseBtBehaviour
    {
        protected override BehaviourStatus DoTick(BtContext context)
        {
            return Condition(context) ? BehaviourStatus.Succeeded : BehaviourStatus.Failed;
        }

        protected abstract bool Condition(BtContext context);
    }
}
