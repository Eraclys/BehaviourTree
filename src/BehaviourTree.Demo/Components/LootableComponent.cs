using BehaviourTree.Demo.GameEngine;

namespace BehaviourTree.Demo.Components
{
    public sealed class LootableComponent : IComponent
    {
        public LootableComponent(int quantity = 1)
        {
            Quantity = quantity;
        }

        public int Quantity { get; private set; }

        public int Loot(int amount)
        {
            int removed;

            if (amount > Quantity)
            {
                Quantity = 0;
                removed = Quantity;
            }
            else
            {
                Quantity -= amount;
                removed = amount;
            }

            return removed;
        }

        public int LootAll()
        {
            return Loot(Quantity);
        }
    }
}
