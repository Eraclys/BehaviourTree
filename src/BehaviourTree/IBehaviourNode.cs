namespace BehaviourTree
{
    public interface IBehaviourNode : IBehaviour
    {
        void OnChildStopped(IBehaviour child, bool success);
    }
}