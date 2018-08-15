using BehaviourTree.Demo.Ai.BT;
using BehaviourTree.Demo.GameEngine;
using BehaviourTree.Demo.Nodes;

namespace BehaviourTree.Demo.Systems
{
    public sealed class AiSystem : IterativeSystem<BtNode>
    {
        public AiSystem(Engine engine) : base(engine)
        {
        }

        protected override void UpdateNode(BtNode node, long ellapsedMilliseconds)
        {
            // TODO: use context pool
            var context = new BtContext(node.Entity, Engine, ellapsedMilliseconds);

            node.BehaviourComponent.BehaviourTree.Tick(context);
        }
    }
}
