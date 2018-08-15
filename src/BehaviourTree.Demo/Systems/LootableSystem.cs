using BehaviourTree.Demo.GameEngine;
using BehaviourTree.Demo.Nodes;

namespace BehaviourTree.Demo.Systems
{
    public sealed class LootableSystem : IterativeSystem<LootableNode>
    {
        public LootableSystem(Engine engine) : base(engine)
        {
        }

        protected override void UpdateNode(LootableNode node, long ellapsedMilliseconds)
        {
            if (node.LootableComponent.Quantity == 0)
            {
                Engine.RemoveEntity(node.Entity.Id);
            }
        }
    }
}
