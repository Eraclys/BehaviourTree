using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BehaviourTree.Demo.GameEngine
{
    public sealed class ComponentMatchingFamily : IFamily
    {
        private readonly Type _nodeType;
        private readonly Dictionary<int, Node> _entityNodeLookup = new Dictionary<int, Node>();
        private readonly Dictionary<Type, FieldInfo> _componentTypeToFieldInfoLookup;
        private readonly List<Node> _nodes = new List<Node>();

        public ComponentMatchingFamily(Type nodeType)
        {
            _nodeType = nodeType;
            _componentTypeToFieldInfoLookup = nodeType
                .GetFields(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => typeof(IComponent).IsAssignableFrom(x.FieldType))
                .ToDictionary(x => x.FieldType, x => x);
        }

        public IEnumerable<Node> GetNodes()
        {
            return _nodes;
        }

        public void NewEntity(Entity entity)
        {
            AddIfMatch(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            RemoveIfMatch(entity);
        }

        public void ComponentAddedToEntity(Entity entity, Type componentType)
        {
            AddIfMatch(entity);
        }

        public void ComponentRemovedFromEntity(Entity entity, Type componentType)
        {
            if (_componentTypeToFieldInfoLookup.ContainsKey(componentType))
            {
                RemoveIfMatch(entity);
            }
        }

        private void AddIfMatch(Entity entity)
        {
            if (_entityNodeLookup.ContainsKey(entity.Id))
            {
                return;
            }

            if (!_componentTypeToFieldInfoLookup.Keys.All(entity.HasComponent))
            {
                return;
            }

            var node = (Node) Activator.CreateInstance(_nodeType);
            node.Entity = entity;

            foreach (var componentType in _componentTypeToFieldInfoLookup.Keys)
            {
                var fieldInfo = _componentTypeToFieldInfoLookup[componentType];

                fieldInfo.SetValue(node, entity.GetComponent(componentType));
            }

            _entityNodeLookup[entity.Id] = node;
            _nodes.Add(node);
        }

        private void RemoveIfMatch(Entity entity)
        {
            if (!_entityNodeLookup.TryGetValue(entity.Id, out var node))
            {
                return;
            }

            _entityNodeLookup.Remove(entity.Id);
            _nodes.Remove(node);
        }
    }
}
