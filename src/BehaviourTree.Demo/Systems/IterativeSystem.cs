using BehaviourTree.Demo.GameEngine;
using System.Collections.Generic;

namespace BehaviourTree.Demo.Systems
{
    public abstract class IterativeSystem<TNodeType> : ISystem where TNodeType : Node
    {
        private readonly IEnumerable<TNodeType> _nodes;
        protected Engine Engine { get; }

        protected IterativeSystem(Engine engine)
        {
            _nodes = engine.GetNodes<TNodeType>();
            Engine = engine;

        }

        public virtual void Update(long ellapsedMilliseconds)
        {
            foreach (var node in _nodes)
            {
                UpdateNode(node, ellapsedMilliseconds);
            }
        }

        protected abstract void UpdateNode(TNodeType node, long ellapsedMilliseconds);
    }
}