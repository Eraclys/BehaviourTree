using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BehaviourTreeBuilder.Nodes
{
    public abstract class CompositeNode : Node, ICanAddChild
    {
        private readonly IList<Node> _children = new List<Node>();

        public void AddChild(Node node)
        {
            _children.Add(node);
        }

        public ReadOnlyCollection<Node> Children => new ReadOnlyCollection<Node>(_children);
    }
}