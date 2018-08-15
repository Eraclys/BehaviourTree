using System;
using System.Collections.Generic;
using System.Linq;

namespace BehaviourTree.Demo.GameEngine
{
    public sealed class FamilyManager
    {
        private readonly EntityManager _entityManager;
        private readonly Dictionary<Type, IFamily> _families = new Dictionary<Type, IFamily>();

        public FamilyManager(EntityManager entityManager)
        {
            _entityManager = entityManager;
            _entityManager.EntityAdded += EntityManager_EntityAdded;
            _entityManager.EntityRemoved += EntityManager_EntityRemoved;
        }

        public IEnumerable<T> GetNodes<T>() where T : Node
        {
            var nodeType = typeof(T);

            if (!_families.TryGetValue(nodeType, out var family))
            {
                family = new ComponentMatchingFamily(nodeType);
                _families[nodeType] = family;

                foreach (var entity in _entityManager.GetAllEntities())
                {
                    family.NewEntity(entity);
                }
            }

            return new ImmutableCollection<T>(family.GetNodes().Cast<T>());
        }

        private void EntityManager_EntityAdded(object sender, Entity e)
        {
            e.ComponentAdded += Entity_ComponentAdded;
            e.ComponentRemoved += Entity_ComponentRemoved;

            foreach (var family in _families.Values)
            {
                family.NewEntity(e);
            }
        }

        private void EntityManager_EntityRemoved(object sender, Entity e)
        {
            foreach (var family in _families.Values)
            {
                family.RemoveEntity(e);
            }

            e.ComponentAdded -= Entity_ComponentAdded;
            e.ComponentRemoved -= Entity_ComponentRemoved;
        }

        private void Entity_ComponentRemoved(object sender, IComponent e)
        {
            foreach (var family in _families.Values)
            {
                family.ComponentRemovedFromEntity((Entity)sender, e.GetType());
            }
        }

        private void Entity_ComponentAdded(object sender, IComponent e)
        {
            foreach (var family in _families.Values)
            {
                family.ComponentAddedToEntity((Entity)sender, e.GetType());
            }
        }
    }
}
