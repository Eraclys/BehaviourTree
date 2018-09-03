using System;
using System.Collections.Generic;
using BehaviourTree.FluentBuilder.Nodes;

namespace BehaviourTree.FluentBuilder
{
    public static class FluentBuilder
    {
        public static FluentBuilder<T> Create<T>() where T : IClock, IRandomProvider
        {
            return new FluentBuilder<T>(new NodeToBehaviourMapper<T>());
        }
    }

    public sealed class FluentBuilder<TContext>
    {
        private readonly INodeToBehaviourMapper<TContext> _nodeToBehaviourMapper;
        private readonly Stack<Node> _parentNodeStack = new Stack<Node>();
        private Node _currentNode;

        public FluentBuilder(INodeToBehaviourMapper<TContext> nodeToBehaviourMapper)
        {
            _nodeToBehaviourMapper = nodeToBehaviourMapper;
        }

        public FluentBuilder<TContext> End()
        {
            _currentNode = _parentNodeStack.Pop();
            return this;
        }

        public FluentBuilder<TContext> Push(Node node)
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

        public IBehaviour<TContext> Build()
        {
            if (_currentNode == null)
            {
                throw new InvalidOperationException("Tree must contain at least one node");
            }

            return _nodeToBehaviourMapper.Map(_currentNode);
        }
    }
}
