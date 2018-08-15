using System;
using System.Collections.Generic;

namespace BehaviourTree.Demo.GameEngine
{
    public class EntityManager : IEntityManager
    {
        private int _maxId;
        private readonly Dictionary<int, Entity> _entities = new Dictionary<int, Entity>();
        
        public event EventHandler<Entity> EntityAdded;
        public event EventHandler<Entity> EntityRemoved;

        public Entity GetEntityById(int id)
        {
            _entities.TryGetValue(id, out var entity);

            return entity;
        }

        public Entity NewEntity()
        {
            var entity = new Entity(_maxId++);

            _entities.Add(entity.Id, entity);
            OnEntityAdded(entity);

            return entity;
        }

        public void RemoveEntity(int id)
        {
            if (!_entities.TryGetValue(id, out var entity))
            {
                return;
            }

            _entities.Remove(id);
            OnEntityRemoved(entity);
        }

        public IEnumerable<Entity> GetAllEntities()
        {
            return _entities.Values;
        }

        protected virtual void OnEntityAdded(Entity component)
        {
            EntityAdded?.Invoke(this, component);
        }

        protected virtual void OnEntityRemoved(Entity component)
        {
            EntityRemoved?.Invoke(this, component);
        }
    }
}
