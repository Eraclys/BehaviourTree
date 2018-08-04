using System;
using System.Collections.Generic;
using BehaviourTree;
using BehaviourTreeBuilder.Nodes;

namespace BehaviourTreeBuilder
{
    public sealed class FluentBuilder
    {
        private readonly INodeToBehaviourMapper _nodeToBehaviourMapper;
        private readonly Stack<Node> _parentNodeStack = new Stack<Node>();
        private Node _currentNode;

        public FluentBuilder() : this(new NodeToBehaviourMapper())
        {

        }

        public FluentBuilder(INodeToBehaviourMapper nodeToBehaviourMapper)
        {
            _nodeToBehaviourMapper = nodeToBehaviourMapper;
        }

        public FluentBuilder End()
        {
            _currentNode = _parentNodeStack.Pop();
            return this;
        }

        public FluentBuilder Push(Node node)
        {
            if (node is ICanAddChild)
            {
                if (_parentNodeStack.Count > 0)
                {
                    var parentNode = (ICanAddChild)_parentNodeStack.Peek();
                    parentNode.AddChild(node);
                }

                _parentNodeStack.Push(node);
            }
            else
            {
                var parentNode = (ICanAddChild)_parentNodeStack.Peek();
                parentNode.AddChild(node);
            }

            return this;
        }

        public IBehaviour Build()
        {
            if (_currentNode == null)
            {
                throw new InvalidOperationException("Tree must contain at least one node");
            }

            return _nodeToBehaviourMapper.Map(_currentNode);
        }
    }
}
