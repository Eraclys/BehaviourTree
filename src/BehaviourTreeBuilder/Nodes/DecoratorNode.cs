using System;

namespace BehaviourTreeBuilder.Nodes
{
    public abstract class DecoratorNode : Node, ICanAddChild
    {
        public Node Child { get; private set; }

        public void AddChild(Node node)
        {
            if (Child != null)
            {
                throw new Exception($"Decorator nodes can only have one child: {node.Name}");
            }

            Child = node;
        }
    }
}