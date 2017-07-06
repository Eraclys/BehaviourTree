namespace BehaviourTree
{
    public interface IBehaviour
    {
        BehaviourStatus CurrentStatus { get; }
        void Start();
        void Stop();
        void SetParent(IBehaviourNode parent);
    }
}
