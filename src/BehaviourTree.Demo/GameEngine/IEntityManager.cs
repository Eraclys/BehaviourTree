namespace BehaviourTree.Demo.GameEngine
{
    public interface IEntityManager
    {
        Entity NewEntity();
        Entity GetEntityById(int id);
        void RemoveEntity(int id);
    }
}