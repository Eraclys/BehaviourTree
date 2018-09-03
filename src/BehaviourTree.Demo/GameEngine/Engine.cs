using System.Collections.Generic;

namespace BehaviourTree.Demo.GameEngine
{
    public sealed class Engine : IEntityManager, IEventManager
    {
        private readonly EntityManager _entityManager;
        private readonly FamilyManager _familyManager;
        private readonly EventManager _eventManager;
        private readonly IList<ISystem> _systems = new List<ISystem>();
        private readonly IList<IRenderSystem> _renderSystems = new List<IRenderSystem>();

        public Engine()
        {
            _entityManager = new EntityManager();
            _familyManager = new FamilyManager(_entityManager);
            _eventManager = new EventManager(this);
            _entityManager.EntityAdded += EntityManager_EntityAdded;
            _entityManager.EntityRemoved += EntityManager_EntityRemoved;
        }

        public Entity NewEntity()
        {
            return _entityManager.NewEntity();
        }

        public Entity GetEntityById(int id)
        {
            return _entityManager.GetEntityById(id);
        }

        public void RemoveEntity(int id)
        {
            _entityManager.RemoveEntity(id);
        }

        public void AddSystem(ISystem system)
        {
            _systems.Add(system);
        }

        public void RemoveSystem(ISystem system)
        {
            _systems.Remove(system);
        }

        public void AddRenderSystem(IRenderSystem system)
        {
            _renderSystems.Add(system);
        }

        public void RemoveRenderSystem(IRenderSystem system)
        {
            _renderSystems.Remove(system);
        }

        public IEnumerable<T> GetNodes<T>() where T : Node
        {
            return _familyManager.GetNodes<T>();
        }

        public void Update(long ellapsedMilliseconds)
        {
            foreach (var system in _systems)
            {
                system.Update(ellapsedMilliseconds);
            }
        }

        public void Render(long ellapsedMilliseconds, float interpolation)
        {
            foreach (var system in _renderSystems)
            {
                system.Render(ellapsedMilliseconds, interpolation);
            }
        }

        public void PublishEvent<TEvent>(TEvent @event)
        {
            _eventManager.PublishEvent(@event);
        }

        public void SubscribeToEvent<TEvent>(IEventListener<TEvent> eventListener)
        {
            _eventManager.SubscribeToEvent(eventListener);
        }

        public void UnsubscribeFromEvent<TEvent>(IEventListener<TEvent> eventListener)
        {
            _eventManager.UnsubscribeFromEvent(eventListener);
        }

        private void EntityManager_EntityRemoved(object sender, Entity e)
        {
            _eventManager.PublishEvent(new EntityRemoved(e.Id));
        }

        private void EntityManager_EntityAdded(object sender, Entity e)
        {
            _eventManager.PublishEvent(new EntityAdded(e.Id));
        }
    }
}
