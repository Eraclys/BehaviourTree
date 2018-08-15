namespace BehaviourTree.Demo.GameEngine
{
    public sealed class EntityAdded
    {
        public int EntityId { get; }

        public EntityAdded(int entityId)
        {
            EntityId = entityId;
        }
    }
}