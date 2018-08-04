namespace BehaviourTree.FluentBuilder.Nodes
{
    public sealed class CooldownNode : DecoratorNode
    {
        public int CooldownTimeInMilliseconds { get; }

        public CooldownNode(int cooldownTimeInMilliseconds)
        {
            CooldownTimeInMilliseconds = cooldownTimeInMilliseconds;
        }
    }
}
