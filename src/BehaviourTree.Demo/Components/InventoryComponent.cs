using BehaviourTree.Demo.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BehaviourTree.Demo.Components
{
    public sealed class InventoryComponent : IComponent
    {
        private readonly Dictionary<ItemTypes, int> _items = new Dictionary<ItemTypes, int>();
        public IEnumerable<KeyValuePair<ItemTypes, int>> Items => _items.ToList();

        public bool Has(ItemTypes itemType, int quantity = 1)
        {
            return _items.TryGetValue(itemType, out var possessed) && possessed >= quantity;
        }

        public void Remove(ItemTypes itemType, int quantity = 1)
        {
            if (!_items.ContainsKey(itemType))
            {
                return;
            }

            var possessed = _items[itemType];
            _items[itemType] = Math.Max(0, possessed - quantity);
        }

        public void Add(ItemTypes itemType, int quantity = 1)
        {
            _items.TryGetValue(itemType, out var possessed);
            _items[itemType] = possessed + quantity;
        }

        public int Count(ItemTypes itemType)
        {
            _items.TryGetValue(itemType, out var possessed);
            return possessed;
        }
    }
}
