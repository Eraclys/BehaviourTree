namespace BehaviourTree.Demo.Events
{
    public sealed class HealthReachedZero
    {
        public int EntityId { get; }

        public HealthReachedZero(int entityId)
        {
            EntityId = entityId;
        }
    }
}
