using BehaviourTree.Demo.GameEngine;

namespace BehaviourTree.Demo.Components
{
    public sealed class ItemComponent : IComponent
    {
        public readonly ItemTypes ItemType;

        public ItemComponent(ItemTypes itemType)
        {
            ItemType = itemType;
        }
    }
}
