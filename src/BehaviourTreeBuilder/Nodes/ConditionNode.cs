using System;
using BehaviourTree;

namespace BehaviourTreeBuilder.Nodes
{
    public sealed class ConditionNode : Node
    {
        public ConditionNode(Func<BtContext, bool> predicate)
        {
            Predicate = predicate;
        }

        public Func<BtContext, bool> Predicate { get; }
    }
}