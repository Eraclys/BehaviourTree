namespace BehaviourTree
{
    public interface IParentBehaviour : IBehaviour
    {
        void OnChildStopped(IBehaviour child, bool success);
    }
}