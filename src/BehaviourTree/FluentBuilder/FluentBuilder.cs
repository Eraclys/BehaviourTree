using System;
using System.Collections.Generic;

namespace BehaviourTree.FluentBuilder
{
    public static class FluentBuilder
    {
        public static FluentBuilder<T> Create<T>()
        {
            return new FluentBuilder<T>();
        }
    }

    public sealed class FluentBuilder<TContext>
    {
        private readonly Stack<CompositeBehaviourBuilder<TContext>> _parentNodeStack = new Stack<CompositeBehaviourBuilder<TContext>>();
        private BehaviourBuilder<TContext> _currentBehaviourBuilder;


        public FluentBuilder<TContext> End()
        {
            _currentBehaviourBuilder = _parentNodeStack.Pop();
            return this;
        }

        public FluentBuilder<TContext> PushComposite(CreateCompositeBehaviour<TContext> behaviourFactory)
        {
            var newNode = new CompositeBehaviourBuilder<TContext>
            {
                Factory = behaviourFactory
            };

            if (_parentNodeStack.Count > 0)
            {
                var parentNode = _parentNodeStack.Peek();
                parentNode.Children.Add(newNode);
            }

            _parentNodeStack.Push(newNode);

            return this;
        }

        public FluentBuilder<TContext> PushLeaf(CreateBehaviour<TContext> behaviourFactory)
        {
            var parentNode = _parentNodeStack.Peek();
            parentNode.Children.Add(new LeafBehaviourBuilder<TContext>{Factory = behaviourFactory});

            return this;
        }

        public IBehaviour<TContext> Build()
        {
            if (_currentBehaviourBuilder == null)
            {
                throw new InvalidOperationException("Tree must contain at least one node");
            }

            return _currentBehaviourBuilder.Build();
        }
    }
}
