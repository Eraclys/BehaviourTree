namespace BehaviourTree.Demo.GameEngine
{
    public sealed class EntityRemoved
    {
        public int EntityId { get; }

        public EntityRemoved(int entityId)
        {
            EntityId = entityId;
        }
    }
}
