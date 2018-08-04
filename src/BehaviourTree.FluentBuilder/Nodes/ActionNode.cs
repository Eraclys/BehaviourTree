using System;

namespace BehaviourTree.FluentBuilder.Nodes
{
    public sealed class ActionNode<TContext> : Node
    {
        public ActionNode(Func<TContext, BehaviourStatus> action)
        {
            Action = action;
        }

        public Func<TContext, BehaviourStatus> Action { get; }
    }
}
