using System;

namespace BehaviourTreeBuilder.Nodes
{
    public sealed class ConditionNode<TContext> : Node
    {
        public ConditionNode(Func<TContext, bool> predicate)
        {
            Predicate = predicate;
        }

        public Func<TContext, bool> Predicate { get; }
    }
}