using System;
using BehaviourTree;

namespace BehaviourTreeBuilder.Nodes
{
    public sealed class ActionNode : Node
    {
        public ActionNode(Func<BtContext, BehaviourStatus> action)
        {
            Action = action;
        }

        public Func<BtContext, BehaviourStatus> Action { get; }
    }
}
