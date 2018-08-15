using System;
using System.Collections.Generic;
using System.Linq;

namespace BehaviourTree.Demo.GameEngine
{
    public class Entity
    {
        private readonly Dictionary<Type, IComponent> _components = new Dictionary<Type, IComponent>();

        public Entity(int id)
        {
            Id = id;
        }

        public event EventHandler<IComponent> ComponentAdded;
        public event EventHandler<IComponent> ComponentRemoved;

        public int Id { get; }

        public Entity AddComponent(IComponent component)
        {
            _components[component.GetType()] = component;
            OnComponentAdded(component);
            return this;
        }

        public Entity RemoveComponent(Type componentType)
        {
            if (_components.TryGetValue(componentType, out var component))
            {
                OnComponentRemoved(component);
            }

            return this;
        }

        public Entity RemoveComponent<T>()
        {
            return RemoveComponent(typeof(T));
        }

        public bool HasComponent(Type componentType)
        {
            return _components.ContainsKey(componentType);
        }

        public bool HasComponent<T>()
        {
            return HasComponent(typeof(T));
        }

        public IComponent GetComponent(Type componentType)
        {
            return _components[componentType];
        }

        public T GetComponent<T>()
        {
            _components.TryGetValue(typeof(T), out var component);
            return (T) component;
        }

        public IComponent[] GetComponents()
        {
            return _components.Values.ToArray();
        }

        protected virtual void OnComponentAdded(IComponent component)
        {
            ComponentAdded?.Invoke(this, component);
        }

        protected virtual void OnComponentRemoved(IComponent component)
        {
            ComponentRemoved?.Invoke(this, component);
        }
    }
}